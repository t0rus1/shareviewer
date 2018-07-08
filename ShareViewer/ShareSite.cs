using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ShareViewer
{

    internal static class ShareSite
    {
        internal static int NumDownloadTasksActive=0;

        //gets 'Inhalt.txt' from remote Shares website 
        //and converts to list of strings
        public static List<String> GetDataDaysListing(AppUserSettings appUserSettings, String userName, String passWord)
        {

            var url = appUserSettings.SharesUrl; //eg = "http://www.bsb-software.de/rese/";
            var remoteFilename = appUserSettings.DataDaysListingFilename;
            var webResource = url + remoteFilename;
            var localFilename = appUserSettings.ExtraFolder + @"\" + remoteFilename;

            var webClient = new WebClient();
            webClient.Credentials = new NetworkCredential(userName, passWord);

            //download the file
            Helper.LogStatus("Info", $"Retrieving {remoteFilename} from {url} as user {userName} ...");

            try
            {
                //download Inhalt.txt file. Note this may fail if e.g. credentials wrong
                //in which case we dont want to have the pre-existing file disappear,
                //so initially download to tmp file
                webClient.DownloadFile(webResource, localFilename+".tmp");
                if (File.Exists(localFilename + ".tmp"))
                {
                    //successfully downloaded (NOTE the file we want currently has a '.tmp' extension)
                    //rename existing Inhalt.txt to Inhalt.txt.prior, deleting any prior version which may be there
                    Helper.FileMoveConditionally(localFilename, localFilename + ".prior", true);
                    File.Move(localFilename + ".tmp", localFilename);

                    var fileInfo = new FileInfo(localFilename);
                    Helper.LogStatus("Info", $"{remoteFilename} retrieved from internet. (created {fileInfo.CreationTime.ToShortDateString()} @ {fileInfo.CreationTime.ToShortTimeString()})");

                    return new List<string>(File.ReadAllLines(localFilename));
                }
                else
                {
                    //something went wrong
                    Helper.LogStatus("Error", $"Failed to download {remoteFilename}");
                    return new List<string>();
                }
            }
            catch (Exception e)
            {
                Helper.LogStatus("Error", $"Exception: {e.Message}");
                MessageBox.Show($"Error downloading/opening {remoteFilename}\n{e.Message}","Remote Download Error");
                return new List<String>();
            }
           
        }

        internal static void DownloadFromStack(ConcurrentStack<String> cs, AppUserSettings appUserSettings, 
            String userName, String passWord)
        {
            var url = appUserSettings.SharesUrl; //eg = "http://www.bsb-software.de/rese/";

            string item;
            if (cs.Count > 0 && cs.TryPop(out item))
            {
                //NOTE, there may be a leading 'tick' at the beginning of item
                //make sure to not let it mess up the file name!
                Match m = Regex.Match(item, @"(\d{4}_\d{2}_\d{2}.TXT) (\d+) ");
                if (m.Success)
                {
                    string targetFile = m.Groups[1].Value;    
                    Int64 targetFileReportedSize = Convert.ToInt64(m.Groups[2].Value);
                    var webResource = url + $"/{targetFile}";
                    var webClient = new WebClient();
                    webClient.Credentials = new NetworkCredential(userName, passWord);
                    var localFilename = appUserSettings.ExtraFolder + @"\" + targetFile;
                    var fileInfo = new FileInfo(localFilename);
                    if (!File.Exists(localFilename) || (fileInfo.Length < targetFileReportedSize))
                    {
                        try
                        {
                            var downloadTask = webClient.DownloadFileTaskAsync(webResource, localFilename);

                            var awaiter = downloadTask.GetAwaiter();
                            awaiter.OnCompleted(() =>
                            {
                                Helper.Log("Info", $"{targetFile} downloaded.");
                                Helper.DecrementProgressCountdown("progressBarDownload","labelBusyDownload");

                                var totalFiles = 0;
                                if (Helper.MarkListboxItem("listBoxInhalt", item, out totalFiles) == 0)
                                {
                                    Helper.Status($"Done. {totalFiles} files downloaded.");
                                }
                                //go get another (even if stack is empty, since we want to exit down below)
                                DownloadFromStack(cs, appUserSettings, userName, passWord);
                            });

                        }
                        catch (Exception e)
                        {
                            Helper.LogStatus("Error", $"Exception: {e.Message}");
                            Helper.Log("Error", "Download terminated early.");
                            cs.Clear();
                            MessageBox.Show(e.Message, "An error ocurred - download will end early.", MessageBoxButtons.OK);
                            cs.Clear();
                        }
                    }
                    else
                    {
                        Helper.Log("Warn", $"{targetFile} exists, skipping file.");
                        Helper.DecrementProgressCountdown("progressBarDownload", "labelBusyDownload");
                        var totalFiles = 0;
                        Helper.MarkListboxItem("listBoxInhalt", item, out totalFiles);
                        //go get another (even if stack is empty, since we want to exit down below)
                        DownloadFromStack(cs, appUserSettings, userName, passWord);
                    }
                }
                else
                {
                    Helper.LogStatus("Warn", $"skipping malformed entry {item}");
                    Helper.DecrementProgressCountdown("progressBarDownload", "labelBusyDownload");
                    //go get another (even if stack is empty, since we want to exit down below)
                    DownloadFromStack(cs, appUserSettings, userName, passWord);
                }

            }
            else
            {
                if (cs.Count == 0)
                {
                    //exit route... for each concurrent task                   
                    Helper.Log("Info",$"Exiting DownloadFromStack. NumDownloadTasksActive={NumDownloadTasksActive}");
                    NumDownloadTasksActive--;
                    if (NumDownloadTasksActive == 0)
                    {
                        LocalStore.TickOffListboxFileItems("listBoxInhalt", appUserSettings.ExtraFolder);
                        Helper.HoldMajorActivity(false);
                    }
                }
            }

        }

        //downloads files currently referenced in the left hand list box
        public static void DownloadDayDataFiles(AppUserSettings appUserSettings, String userName, String passWord, ICollection items)
        {
            //var url = appUserSettings.SharesUrl; //eg = "http://www.bsb-software.de/rese/";

            // put items into a concurrent stack // eg ✔2017_01_06.TXT 24332169 06.01.2017 22:30:34 (leading tick mark not always there)
            ConcurrentStack<String> stack = new ConcurrentStack<string>();
            foreach (string item in items)
            {
                stack.Push(item);
            }

            if (!stack.IsEmpty)
            {
                //get 2 concurrent download chains going
                NumDownloadTasksActive++;
                DownloadFromStack(stack, appUserSettings, userName, passWord);
                Thread.Sleep(1000);
                NumDownloadTasksActive++;
                DownloadFromStack(stack, appUserSettings, userName, passWord);
            }

        }


    }

 }
