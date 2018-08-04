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
        public const String Version = "0.0.7";
        internal Properties.Settings appUserSettings;
        bool initializing = true;
        bool SuppressDaysBackChangeHandling = false; // when true, suppresses OnChangehandling
        bool SuppressFromDateChangeHandling = false;
        bool SuppressToDateChangeHandling = false;
        internal Dictionary<String, String> HolidayHash = new Dictionary<string, string>() { };
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Text = "ShareViewer v" + Version;

            appUserSettings = Properties.Settings.Default;

            CheckExtraFolderSettings();
            CheckAllTableFolderSettings();
            CheckAllTableViewsSettings(); // these views not used on main form, but best done upfront
            CheckOverviewViewsSettings(); // these views not used on main form, but best done upfront
            CheckHolidaysSettings(); // ditto
            Calculations.InitializeAllShareCalculationParameters(appUserSettings);

            BindFormProperties();
            InitializeShareViewer();
            initializing = false;
            Helper.LogStatus("Info", "Ready");
        }

        private void CheckAllTableViewsSettings()
        {
            if (appUserSettings.AllTableViews == null) 
            {
                //not been set yet. set it to default and save.
                appUserSettings.AllTableViews = new System.Collections.Specialized.StringCollection();
                appUserSettings.Save();
            }
        }

        private void CheckOverviewViewsSettings()
        {
            if (appUserSettings.OverviewViews == null)
            {
                //not been set yet. set it to default and save.
                appUserSettings.OverviewViews = new System.Collections.Specialized.StringCollection();
                appUserSettings.Save();
            }
        }

        private void CheckHolidaysSettings()
        {
            if (appUserSettings.Holidays == null)
            {
                //not been set yet. set it to default and save.
                appUserSettings.Holidays = new System.Collections.Specialized.StringCollection();
                appUserSettings.Save();
            }
            Helper.UpdateHolidayHash(ref HolidayHash);

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
                //ensure subfolder 'Audit' exists beneath AllTables folder (not settable!)
                var auditPath = appUserSettings.AllTablesFolder + @"\Audit";
                if (!Directory.Exists(auditPath))
                {
                    Directory.CreateDirectory(auditPath);
                    Helper.LogStatus("Warn", $"Audit path {auditPath} created");
                }
                else
                {
                    Helper.LogStatus("Info", $"Audit path {auditPath} existing");
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
            daysBack.Maximum = 300; // represents trading days!!
            daysBack.Minimum = 0;
            daysBack.Value = 100; // represents trading days!! Change event wont fire at this stage (which is good)
            //don't allow future dates
            calendarFrom.MaxDate = DateTime.Today;
            calendarFrom.MinDate = DateTime.Today.AddDays(-Helper.ActualDaysBackToEncompassTradingDays(DateTime.Today, 300));
            Helper.Log("Debug", $"calendarFrom MinDate= {calendarFrom.MinDate.ToShortDateString()}");

            calendarTo.MaxDate = DateTime.Today;
            //set From date initially to sync with numericUpDown TradingDays
            calendarFrom.SetDate(DateTime.Today.AddDays(-Helper.ActualDaysBackToEncompassTradingDays(DateTime.Today, 100)));
            calendarTo.SetDate(DateTime.Today);
            labelBackFrom.Text = "ending Today";
            //load ShareList
            listBoxShareList.DataSource = LocalStore.ReadShareList();
            //possibly enable the New AllTables button
            //buttonNewAllTables.Enabled = listBoxShareList.Items.Count > 0;
            buttonAddToAllTables.Enabled = listBoxShareList.Items.Count > 0; ;
            //
            labelDatafilesCount.Text = "";

        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            Program.log.Info("App Closed");
        }

        //gets data days listing from the approropriate source (local/internet)
        private List<string> GetDaysListingPerSource()
        {
            List<string> dataDaysList;
            var whichSource = groupBoxSource.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            if (whichSource.Text.Equals("internet"))
            {
                //retrieve 'inhalt.txt' file from shares site and bind left list box to lines within the file
                dataDaysList = ShareSite.GetDataDaysListing(textBoxUsername.Text, textBoxPassword.Text);
            }
            else
            {
                //read local inhalt file
                dataDaysList = LocalStore.GetDataDaysListing();
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
                    Helper.LogStatus("Warn", $"entry {entry} skipped");
                    //throw;
                }
            }
            return false;
        }

        //'Show Inhalt' button click handler
        private void OnLogin(object sender, EventArgs e)
        {
            ShowDataOnHand(false);
        }

        private void ShowDataOnHand(bool forceLocal)
        {
            if (!forceLocal)
            {
                //decide whether to use local inahlt file or download a fresh one from the internet
                var whichSource = groupBoxSource.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
                if (whichSource.Text.Equals("internet") && (textBoxUsername.Text.Length == 0 || textBoxPassword.Text.Length == 0))
                {
                    MessageBox.Show("Please enter both username and password.", "Credentials Needed");
                    return;
                }
                listBoxInhalt.DataSource = GetDaysListingPerSource().Where((entry) => WithinDateRange(entry)).ToList();
            }
            else
            {
                //use local inhalt file otherwise
                listBoxInhalt.DataSource = LocalStore.GetDataDaysListing().Where((entry) => WithinDateRange(entry)).ToList();
            }
            var numFilesTicked = LocalStore.TickOffListboxFileItems("listBoxInhalt", appUserSettings.ExtraFolder);
            Helper.ListBoxClearAndScrollToBottom(listBoxInhalt);           
            buttonDayDataDownload.Enabled = listBoxInhalt.Items.Count > 0;
            labelDatafilesCount.Text = $"{numFilesTicked} files local";
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
                if ((MessageBox.Show($"Download (up to) {listBoxInhalt.Items.Count} files?", "Confirmation required",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
                {

                    Helper.InitProgressCountdown("progressBarDownload", "labelBusyDownload", listBoxInhalt.Items.Count);
                    listBoxInhalt.ClearSelected();
                    Helper.HoldWhileDownloadingDayData(true); //reversed once all files downloaded
                    // downloads take place as async tasks
                    ShareSite.DownloadDayDataFiles(textBoxUsername.Text, textBoxPassword.Text, listBoxInhalt.Items);
                }
            }
        }

        private void FromDateChanged(object sender, DateRangeEventArgs e)
        {
            if (SuppressFromDateChangeHandling)
            {
                SuppressFromDateChangeHandling = false;
                return;
            }

            buttonDayDataDownload.Enabled = false;
            listBoxInhalt.DataSource = null;

            SuppressDaysBackChangeHandling = true;
            daysBack.Value = Helper.ComputeTradingSpanDayCount(calendarFrom.SelectionStart, calendarTo.SelectionStart);
            SuppressDaysBackChangeHandling = false;
            ShowHolidaysSpanned();
            if (!initializing) ShowDataOnHand(true);

            //adjust ToDate Minimum to be same days as currently selected FromDay
            calendarTo.MinDate = calendarFrom.SelectionStart;

        }

        private void ToDateChanged(object sender, DateRangeEventArgs e)
        {
            if (SuppressToDateChangeHandling)
            {
                SuppressToDateChangeHandling = false;
                return;
            }
      
            buttonDayDataDownload.Enabled = false;
            listBoxInhalt.DataSource = null;
            if (calendarTo.SelectionStart.ToShortDateString() == DateTime.Today.ToShortDateString())
            {
                labelBackFrom.Text = "ending Today";
            }
            else
            {
                labelBackFrom.Text = "ending " + calendarTo.SelectionStart.ToShortDateString();
            }

            //recalc number of trading days back
            SuppressDaysBackChangeHandling = true;
            daysBack.Value = Helper.ComputeTradingSpanDayCount(calendarFrom.SelectionStart, calendarTo.SelectionStart);
            SuppressDaysBackChangeHandling = false;
            ShowHolidaysSpanned();
            if (!initializing) ShowDataOnHand(true);

            //adjust calendarFrom such its Maximum date cannot exceed ToDate
            calendarFrom.MaxDate = calendarTo.SelectionStart;

        }



        //trading days back changed (up down spinner or enter pressed in it's textbox)
        private void DaysBackChanged(object sender, EventArgs e)
        {
            if (SuppressDaysBackChangeHandling)
            {
                //don't respond if we are programmatically setting this control
                return;
            }

            int tradingDays = (Int16)((NumericUpDown)sender).Value;
            int actualDays = Helper.ActualDaysBackToEncompassTradingDays(calendarTo.SelectionStart, tradingDays);

            try
            {
                SuppressFromDateChangeHandling = true;
                calendarFrom.SetDate(calendarTo.SelectionStart.AddDays(-actualDays));
                ShowHolidaysSpanned();
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show(exc.Message, "Calendar");
            }
            if (!initializing) ShowDataOnHand(true);
        }

        //Populates right hand listbox with names and dates of Holidays spanned by the period.
        //Also boldens these dates in both calendars
        private void ShowHolidaysSpanned()
        {
            List<String> holsSpanned = new List<string>();
            var numHolidays = Helper.ComputeNumberOfHolidays(calendarFrom.SelectionStart, calendarTo.SelectionStart,HolidayHash,ref holsSpanned);
            Helper.MarkHolidaysInCalendars(ref calendarFrom, ref calendarTo, HolidayHash);
            labelHolidays.Text = $"Holidays in the period: {numHolidays}";
            listBoxSpannedHolidays.DataSource = holsSpanned;
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
                        var sharesList = LocalStore.GenerateShareList(selectedDayFile);

                        listBoxShareList.DataSource = LocalStore.WriteShareList(sharesList, selectedDayFile);
                        //buttonNewAllTables.Enabled = listBoxShareList.Items.Count > 0;
                        buttonAddToAllTables.Enabled = listBoxShareList.Items.Count > 0; ;
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
        }

        private bool BleatForDataFilesListEmpty()
        {
            if (listBoxInhalt.Items.Count == 0)
            {
                string dataFilesBtnText = buttonLogin.Text;
                MessageBox.Show($"The 'Datafiles for period' list is empty.\nPlease select the source (Internet or Local)\nthen click the '{dataFilesBtnText}' button\nin order to confirm that data is on hand.",
                    "Precaution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool BleatForDataFilesNeeded()
        {
            //warn user if data files need to be downloaded
            int missingCount = Helper.UntickedDayDataEntries("listBoxInhalt");
            if (missingCount > 0)
            {
                var msg = $"Not all data files have been downloaded for the requested period";
                MessageBox.Show(msg, $"Downloads needed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool BleatForSpan()
        {
            //firstly, daysBack span MUST BE 100 days
            if (daysBack.Value > 100)
            {
                MessageBox.Show("The trading days span MAY NOT be set to more 100 days for this operation", "Add new Data",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return true;
            }

            var wantedStartDate = Helper.GetCompressedDate(calendarFrom.SelectionStart);
            var wantedEndDate = Helper.GetCompressedDate(calendarTo.SelectionStart);

            var onhandShareSummary = LocalStore.GetAllTableSummaryForShare(1);
            var firstDayOnHand = onhandShareSummary.FirstDay;
            var lastDayOnHand = onhandShareSummary.LastDay;

            if (String.Compare(wantedStartDate,firstDayOnHand,true) < 0)
            {
                var msg = $"The requested date span ({wantedStartDate} - {wantedEndDate}) starts at an EARLIER date than that which is currently held in the All-Tables.\n\n" +
                          $"Either create a NEW 100 day set of All-Tables (with required date span) or set the start date later than '{wantedStartDate}'";
                MessageBox.Show(msg, "Overlay not allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                //OK. The requested date span starts late enough. 
                //Does it end too early?
                if (String.Compare(wantedEndDate,lastDayOnHand) < 0)
                {
                    //yes
                    var msg = $"The requested date span ({wantedStartDate} - {wantedEndDate}) ends at an EARLIER date than that which is currently held in the All-Tables ({lastDayOnHand}).\n\n" +
                              $"This will mean already processed days will have to be re processed again later.";
                    MessageBox.Show(msg, "All-Tables data overlay Protection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
                else
                {
                    if (String.Compare(wantedEndDate, lastDayOnHand) == 0)
                    {
                        var msg = $"We already have data in the All-Tables up until {wantedEndDate} !";
                        MessageBox.Show(msg, "All-Tables data overlay Protection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                    else
                    {
                        //finally, we can tell user how many new days data he is trying to add
                        var lastDayOnHandDT = Helper.ConvertCompressedDateToDateTime(lastDayOnHand);
                        int addDays = (calendarTo.SelectionStart - lastDayOnHandDT).Days;
                        var msg = $"Add new trading data for the {addDays} calendar day(s) beyond last data on hand in the All-Tables ({lastDayOnHand}) ?";
                        if (MessageBox.Show(msg, "Add New Data to All-Tables", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.OK)
                        {
                            return false; // user can proceed to next check
                        }
                        return true; // user cancelled, so dont proceed
                    }

                }
            }
        }

        private void CreateOrTopupAllTables(bool topUp)
        {
            //prepare structures needed for the run
            //ensure AlTables subfolder exists
            var alltablesPath = Helper.GetAppUserSettings().AllTablesFolder;
            if (!Directory.Exists(alltablesPath)) Directory.CreateDirectory(alltablesPath);
            //get the All-Shares list into array form
            var allShareArray = LocalStore.CreateShareArrayFromShareList();

            if (allShareArray.Count() > 0)
            {
                DateTime startDate = calendarFrom.SelectionStart;
                DateTime endDate = calendarTo.SelectionStart;
                int tradingSpan = Helper.ComputeTradingSpanDayCount(startDate, endDate);

                string createOrTopup = topUp ? "OVERLAY" : "CREATE";
                var msg = $"{createOrTopup} All-tables for the {tradingSpan} trading days up to {endDate.ToShortDateString()} (inclusive)?";
                string preserveOrNew = topUp ? "Existing data in the range will be preserved, NEW data will be added." : "All-Table data will be created from scratch.";
                var extraMsg = $"\n\n{preserveOrNew}\n\nWarning: This may take time!";
                if ((MessageBox.Show(msg + extraMsg, "All-Tables",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
                {
                    //put some buttons on hold, make progress bar visible etc..
                    Helper.HoldWhileGeneratingNewAllTables(true,topUp);
                    Helper.Log("Info", Helper.Repeat("==========", 8));
                    Helper.Log("Info", msg);
                    LocalStore.RefreshNewAllTables(startDate, tradingSpan, allShareArray, topUp, ""); //will queue up a lot of tasks!
                }
            }
        }

        // CREATE ALL NEW ALL_TABLES
        private void OnMakeNewAllTables(object sender, EventArgs e)
        {
            if (BleatForDataFilesListEmpty()) return;
            if (BleatForDataFilesNeeded()) return;

            CreateOrTopupAllTables(false); // not a top up, full blown set of new all-tables

        }

        // TOP UP ALL-TABLES
        private void OnTopupAllTables(object sender, EventArgs e)
        {
            if (BleatForDataFilesListEmpty()) return;
            if (BleatForDataFilesNeeded()) return;

            if (BleatForSpan()) return;

            CreateOrTopupAllTables(true); // just a top up

        }

        //user wants to look at a single share's All-Table in details
        private void OnShareDoubleClicked(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedIndex == 0) return;

            string shareItem = ((ListBox)sender).SelectedItem.ToString();
            Match m = Regex.Match(shareItem, @"(.+)\s(\d+)$");
            if (m.Success)
            {
                string shareName = m.Groups[1].Value;
                string shareNum = m.Groups[2].Value;
                string allTableFilename = appUserSettings.AllTablesFolder + $"\\alltable_{shareNum}.at";
                if (File.Exists(allTableFilename))
                {
                    var AtShareForm = new SingleAllTableForm(allTableFilename,shareName);
                    AtShareForm.Text = $"[{shareNum}] {shareName}";
                    AtShareForm.Show();
                }
                else
                {
                    MessageBox.Show($"All table for share {shareNum} not found.\nBe sure to generate it.", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
        }

        //Allow user to abort the run
        private void buttonBusyAllTables_Click(object sender, EventArgs e)
        {
            var dlgResult = MessageBox.Show("Stop generating All-Tables?\nWarning: this will delete all existing All-Table files",
                "Abort", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                ((Button)sender).Text = "Stopping... please allow time to fully stop";
                //cancel all running tasks
                while (TaskMaster.CtsStack.Count > 0)
                {
                    var cts = TaskMaster.CtsStack.Pop();
                    cts.Cancel();
                }
            }

        }

        //user has typed in some text but has not hit enter, rather has hit this button
        private void buttonDays_Click(object sender, EventArgs e)
        {

            int tradingDays = Convert.ToInt16(daysBack.Value);
            int actualDays = Helper.ActualDaysBackToEncompassTradingDays(calendarTo.SelectionStart, tradingDays);
            try
            {
                SuppressFromDateChangeHandling = true;
                calendarFrom.SetDate(calendarTo.SelectionStart.AddDays(-actualDays));
            }
            catch (ArgumentException exc)
            {
                MessageBox.Show(exc.Message, "Calendar");
            }
            ShowDataOnHand(true);

        }

        //fires on text changed
        private void OnSearchForShare(object sender, EventArgs e)
        {
            string quest = ((TextBox)sender).Text;
            for (int i = 0; i < listBoxShareList.Items.Count; i++)
            {
                if (listBoxShareList.Items[i].ToString().EndsWith(" " + quest))
                {
                    listBoxShareList.SelectedIndex = i;
                    break;
                }
            }
        }

        //ensure only digits are typed into share search textbox
        private void textBoxShareNumSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == '\r')
            {
                OnShareDoubleClicked(listBoxShareList, new EventArgs());
            }

        }

        private void monthCalendarHolidays_DateSelected(object sender, DateRangeEventArgs e)
        {
            string holidayDate = ((MonthCalendar)sender).SelectionStart.ToShortDateString();
            //look for possible presence in holidays list already
            foreach (string item in listBoxHolidays.Items)
            {
                if (item.StartsWith(holidayDate))
                {
                    listBoxHolidays.SelectedItem = item;
                    stripText.Text = "Day already included!";
                    return;
                }
            }
            labelAffirmDate.Text = holidayDate;
        }

        private void buttonHolidayAdd_Click(object sender, EventArgs e)
        {
            var selectedDate = monthCalendarHolidays.SelectionStart;
            //ensure the day is not a week-end day
            if (selectedDate.DayOfWeek == DayOfWeek.Saturday || selectedDate.DayOfWeek == DayOfWeek.Sunday)
            {
                stripText.Text = "Only trading days (week-days) may be added to the Holidays list!";
                //textBoxHolidayName.Text = "";
                //labelAffirmDate.Text = "";
                return;
            }

            string holidayDate = monthCalendarHolidays.SelectionStart.ToShortDateString();
            
            string holidayName = textBoxHolidayName.Text;
            if (holidayName.Trim().Length > 0)
            {
                var item = $"{holidayDate} = {holidayName}";
                listBoxHolidays.Items.Add(item);
                buttonSaveHolidays.Visible = true;
                textBoxHolidayName.Text = "";
                labelAffirmDate.Text = "";
            }
        }

        private void buttonRemoveHoliday_Click(object sender, EventArgs e)
        {
            int selectedHolIndex = listBoxHolidays.SelectedIndex;
            if (selectedHolIndex != -1)
            {
                listBoxHolidays.Items.RemoveAt(selectedHolIndex);
                buttonSaveHolidays.Visible = true;
            }
        }

        private void buttonSaveHolidays_Click(object sender, EventArgs e)
        {
            var aus = Helper.GetAppUserSettings();
            aus.Holidays.Clear();
            foreach (string item in listBoxHolidays.Items)
            {
                aus.Holidays.Add(item);
            }
            aus.Save();
            Helper.UpdateHolidayHash(ref HolidayHash);
            buttonSaveHolidays.Visible = false;
            textBoxHolidayName.Text = "";
            labelAffirmDate.Text = "";
            stripText.Text = "Holidays saved.";
        }

        //react to switching Mainform tabs
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var aus = Helper.GetAppUserSettings();
            if (((TabControl)sender).SelectedTab.Text=="Calendar")
            {
                //load the list box
                listBoxHolidays.Items.Clear();
                foreach (string item in aus.Holidays)
                {
                    listBoxHolidays.Items.Add(item);
                }
                buttonSaveHolidays.Visible = false;
            }
        }

        private void linkLabelSummary_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var summaryForm = new AllTableSummaryForm();
            summaryForm.Show();
        }

        private void linkLabelAllowNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            buttonNewAllTables.Enabled = !buttonNewAllTables.Enabled;
            //label text must be based on state of button
            linkLabelAllowNew.Text = buttonNewAllTables.Enabled ? "lock" : "unlock";
        }

        //Reveal the linkLabelSingleDayLoad button if both calendars are indicating the same, single day
        private void calendarFrom_DateSelected(object sender, DateRangeEventArgs e)
        {
            linkLabelSingleDayLoad.Visible = (DateTime.Compare(e.Start,calendarTo.SelectionStart) == 0);
            linkLabelSingleDayLoad.Enabled = linkLabelSingleDayLoad.Visible;
            buttonAddToAllTables.Enabled = !linkLabelSingleDayLoad.Visible;
        }

        //Reveal the linkLabelSingleDayLoad button if both calendars are indicating the same, single day
        private void calendarTo_DateSelected(object sender, DateRangeEventArgs e)
        {
            linkLabelSingleDayLoad.Visible = (DateTime.Compare(e.Start, calendarFrom.SelectionStart) == 0);
            linkLabelSingleDayLoad.Enabled = linkLabelSingleDayLoad.Visible;
            buttonAddToAllTables.Enabled = !linkLabelSingleDayLoad.Visible;
        }

        //Single Day reload
        private void linkLabelSingleDayLoad_Click(object sender, EventArgs e)
        {
            //user want to reload for a single day
            var reloadDate = Helper.GetCompressedDate(calendarTo.SelectionStart);
            var onhandSummary = LocalStore.GetAllTableSummaryForShare(1);

            if ((String.Compare(onhandSummary.FirstDay,reloadDate) <= 0) && 
                (String.Compare(onhandSummary.LastDay, reloadDate) >= 0))
            {
                //check for data file availability
                var path = Helper.GetAppUserSettings().ExtraFolder;
                var yy = reloadDate.Substring(0, 2);
                var mm = reloadDate.Substring(2, 2);
                var dd = reloadDate.Substring(4, 2);
                var pattern = $"20{yy}_{mm}_{dd}.TXT";
                if (!Directory.EnumerateFiles(path,pattern).Any()) {
                    var msg = $"The required Datafile '{pattern}' is not present, cannot reload!";
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //we may proceed with RE-load
                    var dlgResult = MessageBox.Show($"Re-load the AllTables with data from this day: '{reloadDate}' ?",
                        "Single Day Re-Load", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.Yes)
                    {
                        var allShareArray = LocalStore.CreateShareArrayFromShareList();
                        if (allShareArray.Count() > 0)
                        {
                            linkLabelSingleDayLoad.Enabled = false;
                            DateTime startDate = calendarFrom.SelectionStart;
                            DateTime endDate = calendarTo.SelectionStart;
                            int tradingSpan = Helper.ComputeTradingSpanDayCount(startDate, endDate);
                            if (tradingSpan == 1)
                            {
                                LocalStore.RefreshNewAllTables(startDate, tradingSpan, allShareArray, false, reloadDate);
                            }
                        }
                    }
                    else
                    {
                        //reset calendars
                        calendarFrom.SetDate(DateTime.Today.AddDays(-Helper.ActualDaysBackToEncompassTradingDays(DateTime.Today, 100)));
                        calendarTo.SetDate(DateTime.Today);
                        labelBackFrom.Text = "ending Today";
                        linkLabelSingleDayLoad.Enabled = false;
                        linkLabelSingleDayLoad.Visible = false;
                    }
                }
            }
            else
            {
                var msg = $"Reload Date '{reloadDate}' is not in current All-Table range ('{onhandSummary.FirstDay}' -> '{onhandSummary.LastDay}')";
                MessageBox.Show(msg,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void buttonOverview_Click(object sender, EventArgs e)
        {
            var overviewForm = new OverviewForm();
            overviewForm.Show();
        }
    }
}
