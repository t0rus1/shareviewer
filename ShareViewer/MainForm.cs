using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareViewer
{
    public partial class MainForm : Form
    {

        AppUserSettings appUserSettings;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            //Get existing setting values from user.config file. These may not exist initially
            //in which case default values will be gotten from attributes on the class
            //An example location of the settings file, 'user.config' (after appUserSetting.Save() has been called) is:
            //C:\Users\User\AppData\Local\ShareViewer\ShareViewer.exe_Url_03nbdbcxshupknkk45nsaba23z5qq403\1.0.0.0\user.config
            appUserSettings = new AppUserSettings();

            CheckExtraFolderSettings();
            CheckAllTableFolderSettings();

            BindFormProperties();
            InitializeShareViewer();
            Helper.LogStatus("Info", "Ready");
        }

        //instantiate, load and bind app user settings
        private void CheckExtraFolderSettings()
        {
            if (appUserSettings.ExtraFolder.Equals("Default"))
            {
                //not been set yet. set it to default and save.
                appUserSettings.ExtraFolder = Environment.CurrentDirectory + @"\Extra";
                appUserSettings.Save();
            }
            try
            {
                if (!Directory.Exists(appUserSettings.ExtraFolder))
                {
                    Directory.CreateDirectory(appUserSettings.ExtraFolder);
                    Helper.LogStatus("Warn", $"ExtraFolder {appUserSettings.ExtraFolder} created");
                }
                else
                {
                    Helper.LogStatus("Info", $"ExtraFolder {appUserSettings.ExtraFolder} existing");
                }
            }
            catch (Exception e)
            {
                string msg = "Exception thrown in OnLoad: CheckExtraFolderSettings";
                Helper.Status(msg);
                Program.log.Error(msg);
                Program.log.Error(e.Message);
                MessageBox.Show($"Error relating to the 'ExtraFolder' setting:\n{e.Message}");
                //throw;
            }

        }

        private void CheckAllTableFolderSettings()
        {
            if (appUserSettings.AllTablesFolder.Equals("Default"))
            {
                //not been set yet. set it to default and save.
                appUserSettings.AllTablesFolder = Environment.CurrentDirectory + @"\AllTables";
                appUserSettings.Save();
            }
            try
            {
                if (!Directory.Exists(appUserSettings.AllTablesFolder))
                {
                    Directory.CreateDirectory(appUserSettings.AllTablesFolder);
                    Helper.LogStatus("Warn", $"AllTablesFolder {appUserSettings.AllTablesFolder} created");
                }
                else
                {
                    Helper.LogStatus("Info", $"AllTablesFolder {appUserSettings.AllTablesFolder} existing");
                }
            }
            catch (Exception e)
            {
                string msg = "Exception thrown in OnLoad: CheckAllTableFolderSettings";
                Helper.Status(msg);
                Program.log.Error(msg);
                Program.log.Error(e.Message);
                MessageBox.Show($"Error relating to the 'AllTablesFolder' setting:\n{e.Message}");
                //throw;
            }

        }


        private void BindFormProperties()
        {
            //bind selected form properties to the settings object to allow these form properties 
            //to be changed at runtime via a settings dialog (TODO)
            //we are binding form BackColor to our settings to exemplify how its done
            this.DataBindings.Add(new Binding("BackColor", appUserSettings, "BackgroundColor"));

        }

        //initialize the app
        private void InitializeShareViewer()
        {
            //don't allow future dates
            calendarFrom.MaxDate = DateTime.Now;
            calendarTo.MaxDate = DateTime.Now;
            //set From date initially to 100 days back
            calendarFrom.SetDate(DateTime.Now.AddDays(-100));
            //load ShareList
            listBoxShareList.DataSource = LocalStore.ReadShareList(appUserSettings);
        }


        private void OnClose(object sender, FormClosingEventArgs e)
        {
            Program.log.Info("App Closed");
        }

        private void DaysBackChanged(object sender, EventArgs e)
        {
            Double minusDays = -(Double)((NumericUpDown)sender).Value;

            try
            {
                calendarFrom.SetDate(DateTime.Today.AddDays(minusDays));
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show(exc.Message,"Calendar");
            }
        }

        //gets data days listing from the approropriate source (local/internet)
        private List<string> GetDaysListingPerSource()
        {
            List<string> dataDaysList;
            var whichSource = groupBoxSource.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            if (whichSource.Text.Equals("internet"))
            {
                //retrieve 'inhalt.txt' file from shares site and bind left list box to lines within the file
                dataDaysList = ShareSite.GetDataDaysListing(appUserSettings, textBoxUsername.Text, textBoxPassword.Text);
            }
            else
            {
                //read local inhalt file
                dataDaysList = LocalStore.GetDataDaysListing(appUserSettings);
            }

            buttonNewShareList.Enabled = (dataDaysList.Count > 0);

            return dataDaysList;
        }

        private bool WithinDateRange(string entry)
        {
            var firstDay = calendarFrom.SelectionStart;
            var lastDay = calendarTo.SelectionEnd;
            // parse entry by plucking date field from Inhalt.txt line:
            // 2017_02_02.TXT 25410711 02.02.2017 22:30:42

            Match m = Regex.Match(entry, @"(\d{4})_(\d{2})_(\d{2}).TXT");
            if (m.Success)
            {
                try
                {
                    var year = Convert.ToInt16(m.Groups[1].Value);
                    var month = Convert.ToInt16(m.Groups[2].Value);
                    var day = Convert.ToInt16(m.Groups[3].Value);

                    DateTime entryDate = new DateTime(year, month, day);

                    return (entryDate >= firstDay && entryDate <= lastDay);

                }
                catch (Exception e)
                {
                    Helper.LogStatus("Warn",$"entry {entry} skipped");
                    //throw;
                }
            }
            return false;
        }

        //'Connect/Refresh' button click handler
        private void OnLogin(object sender, EventArgs e)
        {
            var whichSource = groupBoxSource.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            if (whichSource.Text.Equals("internet") && (textBoxUsername.Text.Length == 0 || textBoxPassword.Text.Length == 0))
            {
                MessageBox.Show("Please enter both username and password.", "Credentials Needed");
            }
            else
            {
                listBoxInhalt.DataSource = GetDaysListingPerSource().Where((entry) => WithinDateRange(entry)).ToList();
                LocalStore.TickOffListboxFileItems("listBoxInhalt", appUserSettings.ExtraFolder);

                Helper.ListBoxClearAndScrollToBottom(listBoxInhalt);
                buttonDayDataDownload.Enabled = listBoxInhalt.Items.Count > 0;

            }
        }

        //radiobutton source changed 
        private void radioButtonSource_CheckedChanged(object sender, EventArgs e)
        {
            listBoxInhalt.DataSource = GetDaysListingPerSource().Where((entry) => WithinDateRange(entry)).ToList();
            LocalStore.TickOffListboxFileItems("listBoxInhalt", appUserSettings.ExtraFolder);

            Helper.ListBoxClearAndScrollToBottom(listBoxInhalt);
            buttonDayDataDownload.Enabled = listBoxInhalt.Items.Count > 0;

        }

        private void DownloadDayDataBtnClicked(object sender, EventArgs e)
        {
            var whichSource = groupBoxSource.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            if (textBoxUsername.Text.Length == 0 || textBoxPassword.Text.Length == 0)
            {
                MessageBox.Show("Please enter both username and password.", "Credentials Needed");
            }
            else
            {
                if ((MessageBox.Show($"Download {listBoxInhalt.Items.Count} files?", "Confirmation required",  
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
                {

                    Helper.InitProgressCountdown("progressBarDownload", "labelBusyDownload", listBoxInhalt.Items.Count);
                    listBoxInhalt.ClearSelected();
                    Helper.HoldMajorActivity(true); //reversed once all files downloaded
                    // downloads take place as async tasks
                    ShareSite.DownloadDayDataFiles(appUserSettings, textBoxUsername.Text, textBoxPassword.Text, listBoxInhalt.Items);
                }
            }
        }

        private void FromDateChanged(object sender, DateRangeEventArgs e)
        {
            buttonDayDataDownload.Enabled = false;
            listBoxInhalt.DataSource = null;
        }

        private void ToDateChanged(object sender, DateRangeEventArgs e)
        {
            buttonDayDataDownload.Enabled = false;
            listBoxInhalt.DataSource = null;
        }

        private void OnOpenExplorer(object sender, MouseEventArgs e)
        {
            Process.Start("explorer.exe", appUserSettings.ExtraFolder);
        }

        private void OnOpenLogfileFolderButton(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Environment.CurrentDirectory);
        }

        private void onNewShareListBtn(object sender, EventArgs e)
        {
            if ((listBoxInhalt.SelectedItems.Count == 1)) //  && (listBoxInhalt.SelectedIndex == (listBoxInhalt.Items.Count - 1)))
            {
                //we do a Regex match since there may be a leading tick in front of file name
                var item = (String)listBoxInhalt.SelectedItem;
                Match m = Regex.Match(item, @"(\d{4}_\d{2}_\d{2}.TXT)");
                if (m.Success)
                {
                    string selectedDayFile = m.Groups[1].Value;
                    if ((MessageBox.Show($"Generate a NEW Share List from the selected file's data?\nThis will overwrite the current one and discard existing All-tables)?",
                    $"Generation from {selectedDayFile}",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
                    {
                        var sharesList = LocalStore.GenerateShareList(appUserSettings, selectedDayFile);

                        listBoxShareList.DataSource = LocalStore.WriteShareList(appUserSettings, sharesList);
                    }
                }
                else
                {
                    Helper.LogStatus("Error", $"Unable to extract file name from {item}");
                }
            }
            else
            {
                MessageBox.Show("Please select a RECENT file (only 1) from left hand side", "Share List Generation");
            }
        }

        private void OnInhaltClicked(object sender, EventArgs e)
        {
            //if (listBoxInhalt.Items.Count > 0 && listBoxShareList.Items.Count == 0)
            //{
            //    buttonNewShareList.Enabled = true;
            //}
        }
    }
}
