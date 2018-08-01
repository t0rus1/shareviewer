using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using ShareViewer;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Threading;

namespace ShareViewer
{
    internal static class LocalStore
    {
        //gets 'Inhalt.txt' from local ExtraFolder  
        //and converts to list of strings
        internal static List<String> GetDataDaysListing()
        {
            Properties.Settings appUserSettings = Helper.GetAppUserSettings();

            var localFilename = appUserSettings.ExtraFolder + @"\" + appUserSettings.DataDaysListingFilename;

            if (File.Exists(localFilename))
            {
                var fileInfo = new FileInfo(localFilename);

                try
                {
                    Helper.LogStatus("Info", $"{appUserSettings.DataDaysListingFilename} retrieved locally. (created {fileInfo.CreationTime.ToShortDateString()} @ {fileInfo.CreationTime.ToShortTimeString()})");
                    return new List<string>(File.ReadAllLines(localFilename));
                }
                catch (Exception e)
                {
                    Helper.LogStatus("Error", $"Exception: {e.Message}");
                    MessageBox.Show($"Error opening/reading {localFilename}\n{e.Message}","Local Store Error");
                }
            }
            else
            {
                Helper.LogStatus("Warn", $"{localFilename} not found|does not exist.");
            }
            return new List<String>() { };

        }

        //confirms the presence in the ExtraFolder of each file items in the list box
        //and returns number ticked
        internal static int TickOffListboxFileItems(string listBoxName, string extraFolder)
        {
            char tick = '\x2714'; //✔
            string tickStr = $"{tick}";

            var form = Helper.GetMainForm();
            var lbox = (ListBox)form.Controls.Find(listBoxName, true).First();
            var unticked = lbox.Items;

            //make a new 'ticked' list for binding to listbox
            List<string> ticked = new List<string>();
            foreach (string item in unticked)
            {
                //2017_01_02.TXT
                var dataFilename =  extraFolder + @"\" + item.Substring(0, 14);
                string newItem;
                if (File.Exists(dataFilename)) {
                    newItem = item[0] != tick ? tickStr + item : item;
                }
                else
                {
                    newItem = item;
                }
                ticked.Add(newItem);
            }
            //now databind new list
            lbox.DataSource = ticked;
            return ticked.Count;
        }

        //generate a new ShareList file
        internal static List<ShareListItem> GenerateShareList(string sourceName)
        {
            var appUserSettings = Helper.GetAppUserSettings();
            var shareList = new List<ShareListItem>();

            var sourceFilename = appUserSettings.ExtraFolder + @"\" + sourceName;
            if (File.Exists(sourceFilename))
            {
                Helper.LogStatus("Info", $"Generating new ShareList from '{sourceName}'");
                using (FileStream fs = File.Open(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (BufferedStream bs = new BufferedStream(fs))
                    {
                        using (StreamReader sr = new StreamReader(bs))
                        {
                            Dictionary<string,int> shares = new Dictionary<string,int>();
                            string line;
                            int lineCount = 0;
                            int shareCount = 0;
                            while ((line = sr.ReadLine()) != null)
                            {
                                lineCount++;                                
                                //WERTPAPIER;04.07.2018;TELECOM ITALIA RNC;120471.ETR
                                Match m = Regex.Match(line, @"^WERTPAPIER;\d{2}\.\d{2}\.\d{4};(.+);\d+\.ETR$");
                                if (m.Success) {
                                    shareCount++;
                                    shares[m.Groups[1].Value] = shareCount;
                                }
                                //if (lineCount % 10 == 0) Helper.Status($"{lineCount} lines read...");
                            }
                            Helper.LogStatus("Info",$"{lineCount} lines inspected, {shareCount} unique share names found");
                            int shareNum = 0;
                            foreach (var shareName in shares.Keys)
                            {
                                shareList.Add(new ShareListItem(++shareNum, shareName));
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show($"The file {sourceName} was not found in {appUserSettings.ExtraFolder}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return shareList;

        }

        internal static List<String> WriteShareList(List<ShareListItem> shareList, string basedOnDate)
        {
            var appUserSettings = Helper.GetAppUserSettings();

            var shares = from s in shareList
                         select s.ToString();

            var shareStrings = shares.ToList();
            shareStrings.Insert(0, $"[ShareList based on {basedOnDate}]");

            File.WriteAllLines(appUserSettings.ExtraFolder + @"\ShareList.txt", shareStrings);

            return shareStrings;
        }

        internal static String[] ReadShareList()
        {
            var appUserSettings = Helper.GetAppUserSettings();
            var shareListFilename = appUserSettings.ExtraFolder + @"\ShareList.txt";
            if (File.Exists(shareListFilename)) {
                return File.ReadAllLines(shareListFilename);
            }
            return new string[] { };

        }

        internal static void GetDayDataRange(out DateTime newest, out DateTime oldest)
        {
            var appUserSettings = Helper.GetAppUserSettings();
            var directory = new DirectoryInfo(appUserSettings.ExtraFolder);

            var fileInfos = directory.GetFiles("????_??_??.TXT");
            if (fileInfos.Count() > 0)
            {
                var fileInfoNewest = fileInfos.OrderByDescending(f => f.Name).First();
                Match m = Regex.Match(fileInfoNewest.Name, @"(\d{4})_(\d{2})_(\d{2}).TXT");
                if (m.Success)
                {
                    var yr = Convert.ToInt16(m.Groups[1].Value);
                    var mo = Convert.ToInt16(m.Groups[2].Value);
                    var dy = Convert.ToInt16(m.Groups[3].Value);
                    newest = new DateTime(yr, mo, dy);

                    var fileInfoOldest = fileInfos.OrderBy(f => f.Name).First();
                    m = Regex.Match(fileInfoOldest.Name, @"(\d{4})_(\d{2})_(\d{2}).TXT");
                    if (m.Success)
                    {
                        yr = Convert.ToInt16(m.Groups[1].Value);
                        mo = Convert.ToInt16(m.Groups[2].Value);
                        dy = Convert.ToInt16(m.Groups[3].Value);
                        oldest = new DateTime(yr, mo, dy);
                    }
                    else
                    {
                        throw new Exception("Unable to determine oldest day-data date.");
                    }
                }
                else
                {
                    throw new Exception("Unable to determine most recent day-data date.");
                }
            }
            else
            {
                newest = DateTime.MinValue; // indicates no data files found
                oldest = DateTime.MinValue;
            }
        }

        // Skip 1st informational line of sharelist and sweep thru the rest of the shares
        // either deleting early part of existing All-Tables or the entire file (depending on passed in topUp parameter)
        // If topUpOnly is true, data in the All-Tables is effectively 'shuffled up'.
        // If topUpOnly is false, each All-Table is deleted.
        // This will leave an All-Table to which data must be appended (or else which must be written anew,
        // which is akin to appending to a zero size file)
        // Also, once complete, there will in general be a tail bit of the toupInfo object for whose dates have
        // values of 'alreadyHave' remaining false. 
        // These will be the dates for which new All-Table records will need to be appended
        private static void PrepareAllTables(ref TopupInformation topupInfo, DateTime startDate, 
            int tradingSpan, string[] allShares, bool topUpOnly, Action<int> progress)
        {
            var atPath = Helper.GetAppUserSettings().AllTablesFolder;

            Helper.Log("Info", $"deleting/preparing all *.at files");
            var shares = allShares.Skip(1);
            foreach (string share in shares)
            {
                Match m = Regex.Match(share, @"(.+)\s(\d+)$");
                if (m.Success)
                {
                    var shareName = m.Groups[1].Value.TrimEnd();
                    var shareNum = Convert.ToInt16(m.Groups[2].Value);
                    var allTableFile = atPath + @"\" + $"alltable_{shareNum}.at";
                    var tmpFile = allTableFile + ".tmp";
                    if (File.Exists(allTableFile))
                    {
                        if (topUpOnly)
                        {
                            //prepare a 'new' all-table file by skipping over data in the existing all-table which has fallen 
                            //out of range, retaining the run of records to the end (for subsequent appending to with new data)
                            //essentially chopping off the first bit.
                            using (FileStream fs1 = new FileStream(allTableFile, FileMode.Open))
                            {
                                //read entire existing alltable file into memory (can be 10400 records!)
                                var oldRows = Helper.DeserializeList<AllTable>(fs1).Skip(2).ToList();

                                //skip to first record in oldRows holding the first wanted date, then start writing to a NEW version
                                using (FileStream fs2 = new FileStream(tmpFile, FileMode.Create))
                                {
                                    //do the first 2 rows right away (they are special)
                                    Helper.SerializeAllTableRecord(fs2, AllTableFactory.InitialRow(0, "YYMMDD", "Day", "TimeFrom", "TimeTo"));
                                    Helper.SerializeAllTableRecord(fs2, AllTableFactory.InitialRow(1, "", "", "", ""));

                                    int rowNum = 2;
                                    foreach (AllTable at in oldRows)
                                    {
                                        // must this old row be kept?
                                        var topupInfoKey = $"{at.Date},{shareNum}";
                                        // key may not be found if previously the date was processed and subsequently was
                                        // classified a public holiday. (it would have had no trading, all bands empty anyway)
                                        if (topupInfo.DatesData.ContainsKey(topupInfoKey) &&  topupInfo.DatesData[topupInfoKey].Wanted)  
                                        {
                                            //yes, so append it to tmp file
                                            at.Row = rowNum;
                                            at.F = rowNum - 1;
                                            Helper.SerializeAllTableRecord(fs2, at);
                                            //note that we now have data for the date (will be repeatedly done for 104 such bands)
                                            topupInfo.DatesData[topupInfoKey].AlreadyHave = true;
                                            topupInfo.LastRow[shareNum] = at.Row;
                                            rowNum++;
                                        }
                                    }
                                }
                            }
                            //delete old alltable and replace with smaller new one, to which new data will be appended
                            if (File.Exists(tmpFile))
                            {
                                File.Delete(allTableFile);
                                File.Move(tmpFile, allTableFile);
                                progress(shareNum);
                            }
                        }
                        else
                        {
                            File.Delete(allTableFile);
                        }
                    }
                    var auditFile = atPath + @"\Audit\" + $"{shareNum.ToString("000")}.txt";
                    if (File.Exists(auditFile))
                    {
                        if (topUpOnly)
                        {
                            //TODO: decide if Audit file should be peserved and allowed to grow indefinitely
                            File.Delete(auditFile);
                        }
                        else
                        {
                            File.Delete(auditFile);
                        }
                    }
                }
            }
            Helper.Log("Info", $"{shares.Count()} '.at' files participating");
        }

        private static void LogHeaderForGenerateSingleAllTable(string allTableFile, DateTime startDate, bool topUpOnly, string reloadDate)
        {
            if (!topUpOnly && reloadDate == "")
            {
                Helper.Log("Info", $"Creating All-Table for:\n{allTableFile}");
            }
            else
            {
                if (topUpOnly && reloadDate == "")
                {
                    Helper.Log("Info", $"Topping up All-Table for:\n{allTableFile}");
                }
                else
                {
                    if (reloadDate != "")
                    {
                        Helper.Log("Info", $"Reloading All-Table\n{allTableFile} for {reloadDate}");
                    }
                    else
                    {
                        Helper.Log("Error", "GenerateSingleAllTable - don't know what to do!");
                    }
                }
            }
        }

        //Creates from scratch, or 'tops up', or reloads a single day of, an All-Table for a single share for a period 
        //spanning tradingSpan trading days starting at startDate. 
        //If topupOnly, then a leading number of days get 'skipped' (since the AllTable is assumed to already have the data in it). 
        //If reloadOffset is non zero, we skip that number of AllTable records into the AllTable file and then overwrite
        //the succeeding 104 bands (i.e. one days worth)
        //The passed in tradeHash holds the raw trading information needed to create each AllTable record. 
        //tradeHash: Key: 'YYMMDD,bandNum' e.g. "180722,1" band 1 is from 09:00:00 to 09:04:59 
        //           Value: Trade object with properties shareNum,tradeDate,line
        private static void GenerateSingleAllTable(CancellationToken ct, string allTableFile, int shareNum, DateTime startDate, 
            int tradingSpan, Dictionary<string, Trade> tradeHash, bool topUpOnly, TopupInformation topupInfo, string reloadDate)
        {
            LogHeaderForGenerateSingleAllTable(allTableFile, startDate, topUpOnly, reloadDate);

            AllTable atRec;
            var runDate = startDate.AddDays(0);

            //Create or Append to AllTable file?
            FileMode mode;
            if (reloadDate.Length == 6)
            {
                // we are reloading... reloadDate = YYMMDD
                mode = FileMode.Open;
            }
            else
            {
                //more usual case 
                mode = topUpOnly ? FileMode.Append : FileMode.Create;
            }

            using (FileStream fs = new FileStream(allTableFile, mode))
            {
                int rowNum = 0;
                if (reloadDate.Length == 6)
                {
                    //RELOAD of a single day
                    // advance the seek position to the start of the day we want to overwrite
                    var bf = new BinaryFormatter();
                    while (fs.Position != fs.Length)
                    {
                        var lastPos = fs.Position;
                        var testAt = (AllTable)bf.Deserialize(fs);
                        rowNum++;
                        if (testAt.Date == reloadDate)
                        {
                            fs.Position = lastPos; // back up
                            rowNum--;
                            break; // and go on to writing out 104 bands
                        }
                    }
                    if (fs.Position == fs.Length)
                    {
                        Helper.LogStatus("Error", $"Could not find reloadDate '{reloadDate}' records in All-Table {allTableFile}");
                        fs.Dispose();
                        return;
                    }
                }
                else
                {
                    if (topUpOnly)
                    {
                        //we're appending...
                        //no need to write rows 1 and 2 if this is a topup
                        //get starting RowNum to use from passed in TopupInformation
                        rowNum = topupInfo.LastRow[shareNum] + 1;
                    }
                    else
                    {
                        //we're creating from scratch
                        //do the first 2 rows right away (they are special)
                        atRec = AllTableFactory.InitialRow(rowNum++, "YYMMDD", "Day", "TimeFrom", "TimeTo");
                        Helper.SerializeAllTableRecord(fs, atRec);
                        atRec = AllTableFactory.InitialRow(rowNum++, "", "", "", "");
                        Helper.SerializeAllTableRecord(fs, atRec);
                    }
                }

                //now consider every day in the trading span, but if topUpOnly, skip over those we already have.
                int tradingDays = 0; double yesterPrice = 0; double lastPrice = 0;
                while (!ct.IsCancellationRequested && tradingDays < tradingSpan)
                {
                    if (Helper.IsTradingDay(runDate))
                    {
                        var rowDate = runDate.ToShortDateString().Replace("/", "").Substring(2); //YYMMDD
                        var topupInfoKey = $"{rowDate},{shareNum}";
                        if (!topUpOnly || !topupInfo.DatesData[topupInfoKey].AlreadyHave)
                        {
                            //each day has 104 five-minute bands from 09h00 to 17h40 - save each one as an 'at' record to disk
                            var rowDay = runDate.DayOfWeek.ToString().Substring(0, 3);
                            lastPrice = yesterPrice;
                            for (int timeBand = 0; timeBand < 104; timeBand++)
                            {
                                int minsIntoDay = 9 * 60 + 5 * timeBand;
                                int hr = minsIntoDay / 60;
                                int min = minsIntoDay % (60 * hr);

                                string timeFrom = hr.ToString("00") + ":" + min.ToString("00") + ":00";
                                string timeTo = hr.ToString("00") + ":" + (min + 4).ToString("00") + ":59";

                                atRec = AllTableFactory.InitialRow(rowNum++, rowDate, rowDay, timeFrom, timeTo);
                                atRec.FP = lastPrice;
                                // now fill from passed in tradeHash
                                lastPrice = FillAllTableRowFromTradehash(atRec, timeBand + 1, tradeHash, lastPrice);
                                //save AllTable row record to disk - this will be overwriting of reloadDate has been set
                                Helper.SerializeAllTableRecord(fs, atRec);
                            }
                            yesterPrice = lastPrice;
                        }
                        tradingDays++;
                    }
                    runDate = runDate.AddDays(1);
                }

            }
            if (ct.IsCancellationRequested)
            {
                //delete the All-Table file just saved
                try
                {
                    File.Delete(allTableFile);
                }
                catch (Exception ex)
                {
                }
            }
        }

        //updates prices and volumes from hash for given alltable and if able to find 
        //trades for the band, returns price just assigned
        //else returns the price it carried in
        private static double FillAllTableRowFromTradehash(AllTable atRec, int timeBand, 
            Dictionary<string, Trade> tradeHash, double carryInPrice)
        {
            //find the AllTable price and volume data we need for this date & band in the tradehash
            string key = $"{atRec.Date},{timeBand}";
            if (tradeHash.ContainsKey(key))
            {                
                atRec.FP = tradeHash[key].Price;
                atRec.FV = tradeHash[key].Volume;
                return atRec.FP; ;
            }
            return carryInPrice;
        }

        //Build the dictionary for a particular share and date span which helps us in filling the alltables with price and volume info
        //We must also take note of the dates for which we already hold computed all-table records
        private static Dictionary<string, Trade> BuildTradeHash(CancellationToken ct, string shareName, int shareNum, 
            DateTime startDate, int tradingSpan, TopupInformation tradingDatesInfo)
        {
            //traverse Extra folder opening each day-data file which falls into the date range.
            //read each line and skip over trades which do not belong to passed in share name
            //as soon as a desired share is encountered on a WERTPAPIER leading line, note the date.
            //for the subsequent trade untils a new WERTPAPIER is encountered, add/update entries to the Hash
            //day-data file excerpt:
            //...
            //WERTPAPIER; 09.07.2018; TMC CONTENT GR.AG INH.SF1; 121527.ETR
            //09:18:21; 0,19; 3400; 3400
            //17:35:39; 0,182; 0; 3400
            //WERTPAPIER; 09.07.2018; TMC CONTENT GR.AG INH.SF1; 121527.FFM
            //08:17:25; 0,142; 0; 0
            //14:16:22; 0,16; 2000; 2000
            //...
            //...

            var tradeHash = new Dictionary<string, Trade>();

            Helper.Log("Info", $"Building Trade Hash for {shareName} ({shareNum})");

            var runDate = startDate.AddDays(0); // start at startDate with a new runDate object
            int tradingDayCounter = 0;
            //cover the entire span of days, but we skip forward if we already have data
            while (!ct.IsCancellationRequested && tradingDayCounter < tradingSpan)
            {
                string dateTest = runDate.ToShortDateString().Replace("/", "");
                if (Helper.IsTradingDay(runDate))
                {
                    //ASSUME all trades inside the daydata file are dated the same as indicated by the file name
                    var tradeDate = dateTest.Substring(2); //YYMMDD
                    //does a data file exist for this day?
                    var dayFilename = Helper.BuildDayDataFilename(runDate);
                    var dayFile = Helper.GetAppUserSettings().ExtraFolder + $"\\{dayFilename}";
                    if (File.Exists(dayFile))
                    {
                        //skip opening up the datafile if we already have the data in the All-Table
                        var topUpInfoKey = $"{tradeDate},{shareNum}";
                        if (!tradingDatesInfo.DatesData[topUpInfoKey].AlreadyHave) {
                            AddUpdateTradeHash(shareName, shareNum, tradeHash, tradeDate, dayFile);
                        }
                    }
                    tradingDayCounter++;
                }
                runDate = runDate.AddDays(1);
            }
            if (ct.IsCancellationRequested)
            {
                Helper.Log("Info", $"Trade Hash build for {shareName} ({shareNum}) CANCELLED");
            }
            else
            {
                Helper.Log("Info", $"Trade Hash for {shareName} ({shareNum}) has {tradeHash.Count} entries");
            }
            return tradeHash;
        }

        //updates/adds to tradehash for passed in shareName and a day's trades file
        private static void AddUpdateTradeHash(string shareName, int shareNum, Dictionary<string, Trade> tradeHash, 
            string tradeDate, string dayFile)
        {
            using (FileStream fs = File.Open(dayFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        bool ourTrades = false;
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            //look for lines starting WERTPAPIER and containing share name
                            if (line.StartsWith("WERTPAPIER"))
                            {
                                string pattern = @"^WERTPAPIER;\d{2}\.\d{2}\.\d{4};" + shareName + @";\d+\.(ETR|FFM)$";
                                ourTrades = Regex.Match(line, pattern).Success;
                            }
                            else
                            {
                                //ignore all trades not belonging to the share of interest
                                if (ourTrades)
                                {
                                    //one of our trades, build/update a Trade object eg 13:29:55;4,7;1069;1884
                                    var bandNum = Helper.ComputeTimeBand(line); // returns 0 if out of band
                                    //ignore the trade if not in our time bands
                                    if (bandNum != 0)
                                    {
                                        var newTrade = new Trade(shareNum, tradeDate, line);
                                        var hashKey = $"{tradeDate},{bandNum}";
                                        if (tradeHash.ContainsKey(hashKey))
                                        {
                                            tradeHash[hashKey].Price = newTrade.Price;
                                            tradeHash[hashKey].Volume += newTrade.Volume;
                                            //and for Audit, addd this new ticker
                                            tradeHash[hashKey].Tickers.Add(line);
                                        }
                                        else
                                        {
                                            tradeHash.Add(hashKey, newTrade);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            } //using FileStream
        }


        //Iterates over the allShares array passed in and instantiates queued async Tasks 
        //which individually create an AllTable file for each share
        //NOTE: if topUpOnly then existing allTable files must be preserved and simply appended to
        //      if reloadOnly then just one day's worth of timebands within the existing all-table must be overwritten
        private static void UpdateAllTables(TopupInformation topUpInfo, DateTime startDate, int tradingSpan, 
            string[] allShares, bool topUpOnly, string reloadDate)
        {
            var appUserSettings = Helper.GetAppUserSettings();
            var atPath = appUserSettings.AllTablesFolder;

            //skip 1st informational line of sharelist and sweep thru the rest of the shares
            //building new All-table files
            int numShares = allShares.Count() - 1;
            int sharesDone = 0;
            foreach (string share in allShares.Skip(1))
            {
                Match m = Regex.Match(share, @"(.+)\s(\d+)$");
                if (m.Success)
                {
                    var shareName = m.Groups[1].Value.TrimEnd();
                    var shareNum = Convert.ToInt16(m.Groups[2].Value);
                    var allTableFile = atPath + @"\" + $"alltable_{shareNum}.at";

                    var tokenSource = new CancellationTokenSource();
                    TaskMaster.CtsStack.Push(tokenSource);
                    CancellationToken ct = tokenSource.Token;

                    Helper.Log("Info", $"Queuing All-Table job for share {shareNum}...");
                    var genTask = Task.Run(() =>
                    {
                        //build the dictionary which helps us in filling the alltables with price and volume info
                        var tradeHash = BuildTradeHash(ct, shareName, shareNum, startDate, tradingSpan, topUpInfo);
                        //save for audit purposes (TODO: perhaps rather leave up to user to generate singly, on demand?)
                        SaveTradehashAudit(tradeHash, shareName, shareNum);
                        
                        //generate an all-table
                        GenerateSingleAllTable(ct, allTableFile, shareNum, startDate, tradingSpan, tradeHash, topUpOnly, topUpInfo, reloadDate);

                    }, tokenSource.Token);
                    
                    var awaiter = genTask.GetAwaiter();
                    awaiter.OnCompleted(() =>
                    {
                        sharesDone++;
                        Helper.SetProgressBar("progressBarGenNewAllTables", sharesDone, numShares);
                        var progressMsg = $"All-Table done. (Share {shareName})";
                        Helper.UpdateAllTableProgress(progressMsg);
                        Helper.Log("Info", progressMsg);
                        if (sharesDone == numShares)
                        {
                            //end of run, ALL shares done (or task was cancelled)
                            var btnBusy = (Button)Helper.GetMainFormControl("buttonBusyAllTables");
                            if (btnBusy.Text.StartsWith("Stopping"))
                            {
                                //run tasks were cancelled
                                var msg = "All-Tables Run was cancelled.";
                                Helper.LogStatus("Info",msg);
                                Helper.ContinueEnableAllTableGeneration();
                                MessageBox.Show(msg, "CANCELLED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                //normal end of run, store date range now held in the AllTables
                                appUserSettings.AllTableDataStart = startDate.ToShortDateString();
                                appUserSettings.AllTableTradingSpan = tradingSpan.ToString();
                                appUserSettings.Save();
                                var msg = $"All-Table updates completed, {sharesDone} shares processed.";
                                Helper.LogStatus("Info", msg);
                                MessageBox.Show(msg, "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            //re-enable buttons, hide progress bar etc
                            Helper.HoldWhileGeneratingNewAllTables(false,topUpOnly);
                        }    

                    });
                }
            }
        }

        //Create an object with a dictionary enabling a quick lookup of whether a date and share "YYMMDD,shareNum" as stored in all-tables) 
        //is within the needed span. ('want')
        //The dictionary will also be used to note whether we already 'have' the data for the date/share in the All-Tables
        //Finally, also stores last row number as well as last date for each share
        //Initially 'wants' the data for every trading day in the range, and sets 'have' to false
        internal static TopupInformation InitializeTopupInfo(DateTime startDate, int tradingSpan, string[] allShares)
        {
            Helper.Log("Info", $"InitializeTopupInfo...{startDate.ToShortDateString()} for {tradingSpan} days for {allShares.Count()-1} shares");
            var topupInfo = new TopupInformation();

            foreach (string shareLine in allShares.Skip(1))
            {
                //extract share number from each shareLine (1st line is a heading, so skip it)
                int shareNum = Helper.GetDigitsAtEnd(shareLine);
                if (shareNum != 0)
                {
                    var runDate = startDate.AddDays(0); // start at startDate with a new runDate object
                    int tradingDayCounter = 0;
                    while (tradingDayCounter < tradingSpan)
                    {
                        if (Helper.IsTradingDay(runDate))
                        {
                            string compressedDate = runDate.ToShortDateString().Replace("/", "").Substring(2); // YYMMDD
                            string dateShareKey = $"{compressedDate},{shareNum}";
                            topupInfo.DatesData[dateShareKey] = new WantHaveInfo(true, false);
                            topupInfo.LastDate[shareNum] = compressedDate;
                            topupInfo.LastRow[shareNum] = 0; // must be set right later
                            tradingDayCounter++;
                        }
                        runDate = runDate.AddDays(1);
                    }
                }
                else
                {
                    Helper.Log("Error", $"Unable to extract share number from '{shareLine}'");
                }
            }
            Helper.Log("Info", $"topupInfo.DatesData has {topupInfo.DatesData.Keys.Count} keys. LastDate has {topupInfo.LastDate.Keys.Count} keys, LastRow has {topupInfo.LastRow.Keys.Count} keys.");
            return topupInfo;
        }

        private static void SaveTradehashAudit(Dictionary<String, Trade> tradeHash, string shareName, short shareNum)
        {
            var appUserSettings = Helper.GetAppUserSettings();
            var auditPath = appUserSettings.AllTablesFolder + @"\Audit";
            Directory.CreateDirectory(auditPath);

            var auditFile = auditPath + @"\" + $"{shareNum.ToString("000")}.txt";
            using (StreamWriter sw = new StreamWriter(auditFile, false))
            {
                sw.WriteLine($"{shareName} ({shareNum})");
                foreach (string key in tradeHash.Keys)
                {
                    var trade = tradeHash[key];
                    sw.WriteLine(trade.DateAndBand());
                    sw.WriteLine(trade.AllTickers());
                }                
            };
        }

        internal static string[] CreateShareArrayFromShareList()
        {
            string[] allSharesArray = new string[] { };
            var appUserSettings = Helper.GetAppUserSettings();
            var sharelistPath = appUserSettings.ExtraFolder;
            var shareListFilePath = sharelistPath + @"\ShareList.txt";

            try
            {
                allSharesArray = File.ReadAllLines(shareListFilePath);
            }
            catch (FileNotFoundException exc)
            {
                MessageBox.Show($"ShareList file {shareListFilePath} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return allSharesArray;
        }

        //Entrypoint for the generation/update of a complete batch of fresh new AllTables
        //based on the ShareList, creating one AllTable for each share, populated with initial values.
        internal static void RefreshNewAllTables(DateTime startDate, int tradingSpan, string[] allSharesArray, 
                                                        bool topUpOnly, string reloadDate)
        {
            //initialize the topupInfo structure which will hold the info we need to enable us to do a topup run
            var topupInfo = LocalStore.InitializeTopupInfo(startDate, tradingSpan, allSharesArray);

            if (reloadDate=="")
            {
                //prepare trimmed AllTable files (if topping up) and delete Audit files
                var task = Task.Run(() => PrepareAllTables(ref topupInfo, startDate, tradingSpan, allSharesArray, topUpOnly, PrepareAllTablesProgress));
                var awaiter = task.GetAwaiter();
                awaiter.OnCompleted(() => UpdateAllTables(topupInfo, startDate, tradingSpan, allSharesArray, topUpOnly, ""));
            }
            else
            {
                UpdateAllTables(topupInfo, startDate, tradingSpan, allSharesArray, false, reloadDate);
            }

        }

        internal static void PrepareAllTablesProgress(int shareNum)
        {
            Helper.Status($"Preparing All-Table for share {shareNum}...");
        }

        // accesses AllTable for passed in share number and determines the 
        // Firstday,Lastday and NumberOfTradingDays which it returns in an AllTableSummary object
        internal static AllTableSummary GetAllTableSummaryForShare(int shareNum)
        {
            var allTablesFolder = Helper.GetAppUserSettings().AllTablesFolder;
            string allTableFilename = allTablesFolder + $"\\alltable_{shareNum}.at";

            var shareSummary = new AllTableSummary(new Share("dontcare", 1));

            //determine First Day, Last Day and number of trading days by inspecting the All-Table file
            using (FileStream fs = new FileStream(allTableFilename, FileMode.Open))
            {
                //read in entire all-table
                var atRows = Helper.DeserializeList<AllTable>(fs).ToArray();

                shareSummary.FirstDay = atRows[2].Date;
                shareSummary.LastDay = atRows[atRows.Count() - 1].Date;
                shareSummary.NumberOfTradingDays = (atRows.Count() - 2) / 104;
            }

            return shareSummary;

        }



    }
}
