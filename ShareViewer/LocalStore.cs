using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace ShareViewer
{
    public class LocalStore
    {
        //gets 'Inhalt.txt' from local ExtraFolder  
        //and converts to list of strings
        public static List<String> GetDataDaysListing(AppUserSettings appUserSettings)
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

    }
}
