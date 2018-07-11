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

namespace ShareViewer
{
    internal static class LocalStore
    {
        //gets 'Inhalt.txt' from local ExtraFolder  
        //and converts to list of strings
        internal static List<String> GetDataDaysListing(AppUserSettings appUserSettings)
        {

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
        internal static void TickOffListboxFileItems(string listBoxName, string extraFolder)
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
        }

        //generate a new ShareList file
        internal static List<ShareListItem> GenerateShareList(AppUserSettings appUserSettings, string sourceName)
        {
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

        internal static List<String> WriteShareList(AppUserSettings appUserSettings, List<ShareListItem> shareList, string basedOnDate)
        {
            var shares = from s in shareList
                         select s.ToString();

            var shareStrings = shares.ToList();
            shareStrings.Insert(0, $"[ShareList based on {basedOnDate}]");

            File.WriteAllLines(appUserSettings.ExtraFolder + @"\ShareList.txt", shareStrings);

            return shareStrings;
        }

        internal static String[] ReadShareList(AppUserSettings appUserSettings)
        {
            var shareListFilename = appUserSettings.ExtraFolder + @"\ShareList.txt";
            if (File.Exists(shareListFilename)) {
                return File.ReadAllLines(shareListFilename);
            }
            return new string[] { };

        }

        internal static void GetDayDataRange(AppUserSettings appUserSettings, out DateTime newest, out DateTime oldest)
        {
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

        private static void GenerateSingleShareAllTable(string allTableFile, DateTime newestDate, int backSpan)
        {
            Helper.Log("Info", $"creating {allTableFile}");

            var oldestDate = newestDate.AddDays(-backSpan);
            var runDate = newestDate.AddDays(-backSpan);
            
            using (FileStream fs = new FileStream(allTableFile,FileMode.Create)) 
            {
                //do the first 2 rows right away (they are special)
                var atRec = AllTableFactory.InitialRow(0, "YYMMDD", "Day", "TimeFrom", "TimeTo");
                Helper.SerializeAllTableRecord(fs, atRec);
                atRec = AllTableFactory.InitialRow(1, "", "", "", "");
                Helper.SerializeAllTableRecord(fs, atRec);

                int rowNum = 2;
                while (runDate <= newestDate)
                {
                    //new day
                    var rowDate = runDate.ToShortDateString().Replace("/", "");
                    var rowDay = runDate.DayOfWeek.ToString().Substring(0, 3);

                    //each day has 104 five-minute bands from 09h00 to 17h40
                    for (int timeBand = 0; timeBand < 104; timeBand++)
                    {
                        int minsIntoDay = 9 * 60 + 5*timeBand;
                        int hr = minsIntoDay / 60;
                        int min = minsIntoDay % (60*hr);

                        string timeFrom = hr.ToString("00") + ":" + min.ToString("00") + ":00";
                        string timeTo = hr.ToString("00") + ":" + (min + 4).ToString("00") + ":59";

                        atRec = AllTableFactory.InitialRow(rowNum++, rowDate, rowDay, timeFrom, timeTo);
                        Helper.SerializeAllTableRecord(fs, atRec);
                    }
                    runDate = runDate.AddDays(1);
                }

            }
        }

        //skip 1st informational line of sharelist and sweep thru the rest of the shares
        //deleting existing All-Tables
        private static void DeleteAllAllTables(string atPath, string[] allShares)
        {
            foreach (string share in allShares.Skip(1))
            {
                Match m = Regex.Match(share, @"(.+)\s(\d+)$");
                if (m.Success)
                {
                    var shareName = m.Groups[1].Value.TrimEnd();
                    var shareNum = Convert.ToInt16(m.Groups[2].Value);
                    var allTableFile = atPath + @"\" + $"at_{shareNum}.at";
                    Helper.Log("Info", $"deleting {allTableFile}");
                    File.Delete(allTableFile);
                }
            }
        }

        private static void GenerateAllAllTables(DateTime newestDate, int backSpan, string atPath, string[] allShares)
        {
            //skip 1st informational line of sharelist and sweep thru the rest of the shares
            //building new All-table files
            foreach (string share in allShares.Skip(1))
            {
                Match m = Regex.Match(share, @"(.+)\s(\d+)$");
                if (m.Success)
                {
                    var shareName = m.Groups[1].Value.TrimEnd();
                    var shareNum = Convert.ToInt16(m.Groups[2].Value);
                    var allTableFile = atPath + @"\" + $"alltable_{shareNum}.at";

                    GenerateSingleShareAllTable(allTableFile, newestDate, backSpan);

                }
            }
        }

        //Sweep thru the ShareList, creating one AllTable for each share, populated with initial values
        internal static void GenerateNewAllTables(AppUserSettings appUserSettings, DateTime newestDate, int backSpan)
        {
            var slPath = appUserSettings.ExtraFolder;
            var atPath = appUserSettings.AllTablesFolder;

            if (!Directory.Exists(atPath))
            {
                Directory.CreateDirectory(atPath);
            }

            string[] allShares;
            try
            {
                //load ShareList
                allShares = File.ReadAllLines(slPath + @"\ShareList.txt");
            }
            catch (Exception)
            {
                MessageBox.Show("ShareList not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DeleteAllAllTables(atPath, allShares);
            GenerateAllAllTables(newestDate, backSpan, atPath, allShares);
        }


    }
}
