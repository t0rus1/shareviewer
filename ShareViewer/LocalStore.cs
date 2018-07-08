using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

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

        internal static List<String> WriteShareList(AppUserSettings appUserSettings, List<ShareListItem> shareList)
        {
            var shares = from s in shareList
                         select s.ToString();

            var shareStrings = shares.ToList();

            File.WriteAllLines(appUserSettings.ExtraFolder + @"\ShareList.txt", shareStrings);

            return shareStrings;
        }

        internal static String[] ReadShareList(AppUserSettings appUserSettings)
        {
            return File.ReadAllLines(appUserSettings.ExtraFolder + @"\ShareList.txt");
        }

    }
}
