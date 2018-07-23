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

        //skip 1st informational line of sharelist and sweep thru the rest of the shares
        //deleting existing All-Tables
        private static void DeleteAllTablesPlusAudits(string atPath, string[] allShares)
        {
            Helper.Log("Info", $"deleting all *.at files");
            var victims = allShares.Skip(1);
            foreach (string share in victims)
            {
                Match m = Regex.Match(share, @"(.+)\s(\d+)$");
                if (m.Success)
                {
                    var shareName = m.Groups[1].Value.TrimEnd();
                    var shareNum = Convert.ToInt16(m.Groups[2].Value);
                    var allTableFile = atPath + @"\" + $"alltable_{shareNum}.at";
                    File.Delete(allTableFile);
                    var auditFile = atPath + @"\Audit\" + $"{shareNum.ToString("000")}.txt";
                    File.Delete(auditFile);
                }
            }
            Helper.Log("Info", $"{victims.Count()} '.at' files deleted");
        }

        private static void GenerateSingleShareAllTable(CancellationToken ct, string allTableFile, DateTime startDate, int tradingSpan, Dictionary<string, Trade> tradeHash)
        {
            Helper.Log("Info", $"creating all-table for:\n{allTableFile}");

            //var oldestDate = newestDate.AddDays(-backSpan);
            //var runDate = newestDate.AddDays(-backSpan);
            var runDate = startDate.AddDays(0);
            
            using (FileStream fs = new FileStream(allTableFile,FileMode.Create)) 
            {
                //do the first 2 rows right away (they are special)
                var atRec = AllTableFactory.InitialRow(0, "YYMMDD", "Day", "TimeFrom", "TimeTo");
                Helper.SerializeAllTableRecord(fs, atRec);
                atRec = AllTableFactory.InitialRow(1, "", "", "", "");
                Helper.SerializeAllTableRecord(fs, atRec);

                int rowNum = 2;
                int tradingDays = 0;
                double yesterPrice = 0;
                double lastPrice = 0;
                while (!ct.IsCancellationRequested && tradingDays < tradingSpan)
                {
                    if (Helper.IsTradingDay(runDate))
                    {
                        //each day has 104 five-minute bands from 09h00 to 17h40 - save each one as an 'at' record to disk
                        var rowDate = runDate.ToShortDateString().Replace("/", "").Substring(2); //YYMMDD
                        var rowDay = runDate.DayOfWeek.ToString().Substring(0, 3);
                        lastPrice = yesterPrice;
                        for (int timeBand = 0; timeBand < 104; timeBand++)
                        {
                            int minsIntoDay = 9 * 60 + 5*timeBand;
                            int hr = minsIntoDay / 60;
                            int min = minsIntoDay % (60*hr);

                            string timeFrom = hr.ToString("00") + ":" + min.ToString("00") + ":00";
                            string timeTo = hr.ToString("00") + ":" + (min + 4).ToString("00") + ":59";

                            atRec = AllTableFactory.InitialRow(rowNum++, rowDate, rowDay, timeFrom, timeTo);
                            atRec.FP = lastPrice;
                            // now fill from passed in tradeHash
                            lastPrice = FillAllTableRowFromTradehash(atRec,timeBand+1,tradeHash, lastPrice);
                            //save AllTable row record to disk
                            Helper.SerializeAllTableRecord(fs, atRec);
                        }
                        yesterPrice = lastPrice;
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

        //build the dictionary which helps us in filling the alltables with price and volume info
        private static Dictionary<string, Trade> BuildTradeHash(CancellationToken ct, string shareName, int shareNum, DateTime startDate, int tradingSpan)
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

            //we need to use all day-data trading files in the range
            var runDate = startDate.AddDays(0); // start at startDate with a new runDate object
            int dayOffset = 0; int tradingDayCounter = 0;
            while (!ct.IsCancellationRequested && tradingDayCounter < tradingSpan)
            {
                //ASSUME all trades inside the daydata file are dated the same as indicated by the file name
                var tradeDate = runDate.ToShortDateString().Replace("/", "").Substring(2); //YYMMDD
                //does a data file exist for this day?
                var dayFilename = Helper.BuildDayDataFilename(runDate);
                var dayFile = Helper.GetAppUserSettings().ExtraFolder + $"\\{dayFilename}";
                if (File.Exists(dayFile))
                {
                    AddUpdateTradeHash(shareName, shareNum, tradeHash, tradeDate, dayFile, dayOffset);
                }
                else
                {
                    if (Helper.IsTradingDay(runDate))
                    {
                        Helper.LogStatus("Warn", $"{dayFilename} not present !!!");
                    }
                }
                if (Helper.IsTradingDay(runDate))
                {
                    tradingDayCounter++;
                }
                dayOffset++;
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
            string tradeDate, string dayFile, int dayOffset)
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
                                    var newTrade = new Trade(shareNum, tradeDate, line);
                                    var bandNum = Helper.ComputeTimeBand(line); // + 104*dayOffset;
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

            } //using FileStream
        }


        //Iterates over the allShares array passed in and instantiates queued async Tasks 
        //which individually create an AllTable file for each share
        private static void GenerateAllTables(DateTime startDate, int tradingSpan, string atPath, string[] allShares)
        {
            var appUserSettings = Helper.GetAppUserSettings();
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
                        var tradeHash = BuildTradeHash(ct, shareName, shareNum, startDate, tradingSpan);
                        //save for audit purposes (TODO: perhaps rather leave up to user to generate singly, on demand?)
                        SaveTradehashAudit(tradeHash,shareName, shareNum);
                        //generate an all-table
                        GenerateSingleShareAllTable(ct, allTableFile, startDate, tradingSpan,tradeHash);                         
                    }, tokenSource.Token);
                    
                    var awaiter = genTask.GetAwaiter();
                    awaiter.OnCompleted(() =>
                    {
                        sharesDone++;
                        Helper.SetProgressBar("progressBarGenNewAllTables", sharesDone, numShares);
                        var progressMsg = $"All-Table done. (Share {shareName})";
                        Helper.UpdateNewAllTableGenerationProgress(progressMsg);
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
                                var msg = $"All-Table generation completed, {sharesDone} shares processed.";
                                Helper.LogStatus("Info", msg);
                                MessageBox.Show(msg, "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            //re-enable buttons, hide progress bar etc
                            Helper.HoldWhileGeneratingNewAllTables(false);
                        }    

                    });
                }
            }
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

        //Entrypoint for the generation of a complete batch of fresh new AllTables
        //based on the ShareList, creating one AllTable for each share, populated with initial values.
        internal static void GenerateNewAllTables(DateTime startDate, int tradingSpan)
        {
            var appUserSettings = Helper.GetAppUserSettings();
            var sharelistPath = appUserSettings.ExtraFolder;
            var alltablesPath = appUserSettings.AllTablesFolder;

            if (!Directory.Exists(alltablesPath))
            {
                Directory.CreateDirectory(alltablesPath);
            }

            string[] allSharesArray;
            try
            {
                //load ShareList
                allSharesArray = File.ReadAllLines(sharelistPath + @"\ShareList.txt");
            }
            catch (Exception)
            {
                MessageBox.Show("ShareList not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DeleteAllTablesPlusAudits(alltablesPath, allSharesArray);
            GenerateAllTables(startDate, tradingSpan, alltablesPath, allSharesArray);
        }



    }
}
