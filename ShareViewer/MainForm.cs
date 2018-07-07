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

            CheckSettings();
            BindFormProperties();
            InitializeShareViewer();
            Helper.LogStatus("Info", "Ready");
        }

        //instantiate, load and bind app user settings
        private void CheckSettings()
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
                string msg = "Exception thrown in OnLoad: CheckSettings";
                Helper.Status(msg);
                Program.log.Error(msg);
                Program.log.Error(e.Message);
                MessageBox.Show($"Error relating to the 'ExtraFolder' setting:\n{e.Message}");
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
            //set From date initially to 100 days back
            calendarFrom.SetDate(DateTime.Now.AddDays(-100));

        }


        private void OnClose(object sender, FormClosingEventArgs e)
        {
            Program.log.Info("App Closed");
        }

        private void DaysBackChanged(object sender, EventArgs e)
        {
            Double minusDays = -(Double)((NumericUpDown)sender).Value;

            calendarFrom.SetDate(DateTime.Now.AddDays(minusDays));
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
                listBoxLeft.DataSource = GetDaysListingPerSource().Where((entry) => WithinDateRange(entry)).ToList();
                Helper.ListBoxClearAndScrollToBottom(listBoxLeft);
                buttonDayDataDownload.Enabled = true;
            }
        }

        //radiobutton source changed 
        private void radioButtonSource_CheckedChanged(object sender, EventArgs e)
        {
            listBoxLeft.DataSource = GetDaysListingPerSource().Where((entry) => WithinDateRange(entry)).ToList();
            Helper.ListBoxClearAndScrollToBottom(listBoxLeft);
            buttonDayDataDownload.Enabled = true;
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
                if ((MessageBox.Show($"Download {listBoxLeft.Items.Count} files?", "Confirmation required",  
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
                {
                    buttonDayDataDownload.Enabled = false;
                    Helper.InitProgressCountdown("progressBar1",listBoxLeft.Items.Count);
                    listBoxLeft.ClearSelected();
                    ShareSite.DownloadDayDataFiles(appUserSettings, textBoxUsername.Text, textBoxPassword.Text, listBoxLeft.Items);
                }
            }
        }

        private void FromDateChanged(object sender, DateRangeEventArgs e)
        {
            buttonDayDataDownload.Enabled = false;
        }

        private void ToDateChanged(object sender, DateRangeEventArgs e)
        {
            buttonDayDataDownload.Enabled = false;
        }

        private void OnOpenExplorer(object sender, MouseEventArgs e)
        {
            Process.Start("explorer.exe", appUserSettings.ExtraFolder);
        }

        private void OnOpenLogfileFolderButton(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Environment.CurrentDirectory);
        }
    }
}
