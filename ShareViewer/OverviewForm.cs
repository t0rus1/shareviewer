using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareViewer
{
    public partial class OverviewForm : Form
    {
        BindingSource dgViewBindingSource = new BindingSource();
        List<Overview> sharesOverview = new List<Overview>();
        private bool _loaded = false;
        private bool _loadingCols = false;
        private bool _changingColumns = false;


        internal TextBox calcAuditTextBox;
        //CURRENT properties
        //get our current LazyShare parameters from user settings
        //private LazyShareParam currLazyShareParam;
        internal LazyShareParam CurrLazyShareParam { get => Helper.GetAppUserSettings().ParamsLazyShare; }
        //get our current SlowPrice parameters from user settings
        //private SlowPriceParam currSlowPriceParam;
        internal SlowPriceParam CurrSlowPriceParam { get => Helper.GetAppUserSettings().ParamsSlowPrice; }

        //CALCULATION properties (we bind these to a property grid)
        private LazyShareParam calcLazyShareParam;
        internal LazyShareParam CalcLazyShareParam { get => calcLazyShareParam; set => calcLazyShareParam = value; }
        private SlowPriceParam calcSlowPriceParam;
        internal SlowPriceParam CalcSlowPriceParam { get => calcSlowPriceParam; set => calcSlowPriceParam = value; }



        public OverviewForm()
        {
            InitializeComponent();
        }

        //used before DatagridView has columns
        private void SelectInitialColumnsToView()
        {
            _loadingCols = true;
            listBoxCols.SelectedIndices.Clear();
            foreach (var col in Overview.InitialViewIndices())
            {
                listBoxCols.SelectedIndex = col;
            }
            _loadingCols = false;
        }

        //selects listbox items which are listed in the passed comma separated list
        //note: 1st segment assumed to be a viewname! the rest are the column indices
        private void SelectColumnsToView(string viewDefinition)
        {
            _loadingCols = true;
            var columns = viewDefinition.Split(',').Skip(1);
            listBoxCols.SelectedIndices.Clear();
            foreach (string colIndex in columns)
            {
                var selIndex = Convert.ToInt16(colIndex);
                if (selIndex != -1) listBoxCols.SelectedIndex = selIndex;
            }
            _loadingCols = false;
        }


        private void OverviewForm_Load(object sender, EventArgs e)
        {

            //load up combobox for views from user settings
            LoadViewsComboBox("");

            //load up the Overview 'columns' listbox
            var columnNames = new List<String>();
            int i = 1;
            foreach (var prop in typeof(Overview).GetProperties())
            {
                var colName = $"{i.ToString().PadRight(3, ' ')}{prop.Name}";
                columnNames.Add(colName);
                i++;
            }
            listBoxCols.DataSource = columnNames;

            dgOverview.AutoGenerateColumns = false;
            dgOverview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgOverview.AllowUserToResizeColumns = true;

            //disable form until grid is loaded
            this.Enabled = false;
            BindDatagridView();

            SelectInitialColumnsToView();

            InstallOverviewColumns();
            listBoxVariables.DataSource = Calculations.CalculationNames;
            //access calculation parameters (in order just to load them)
            var dontCare1 = CurrLazyShareParam;
            var dontCare2 = CurrSlowPriceParam;

            stripText.Text = "";
            _loaded = true;
        }

        //Retrieve views from User settings and load the Views Combobox
        private void LoadViewsComboBox(string selectItem)
        {
            comboBoxViews.Items.Clear();
            foreach (string item in Helper.GetAppUserSettings().OverviewViews)
            {
                string viewName = item.Split(',')[0];
                int viewIndex = comboBoxViews.Items.Add(viewName);
                if (viewName == selectItem)
                {
                    comboBoxViews.SelectedIndex = viewIndex;
                }
            }
        }

        private void BindDatagridView()
        {
            Cursor.Current = Cursors.WaitCursor;

            dgViewBindingSource.Clear();

            var task = Task.Run(() => GenerateOverviewForGrid(displayProgress, sharesOverview));
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                foreach (Overview overview in sharesOverview)
                {
                    dgViewBindingSource.Add(overview);
                }
                dgOverview.DataSource = dgViewBindingSource;
                stripText.Text = $"{sharesOverview.Count} shares.";
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
                dgOverview.Focus();
            });
        }

        //will Hide/Unhide shares flagged as Lazy
        private void RebindDatagridView(bool hideLazy)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgViewBindingSource.Clear();

            int displayedCount = 0;
            foreach (Overview overview in sharesOverview)
            {
                if (hideLazy)
                {
                    if (!overview.Lazy)
                    {
                        dgViewBindingSource.Add(overview);
                        displayedCount++;
                    }
                }
                else
                {
                    dgViewBindingSource.Add(overview);
                    displayedCount++;
                }
            }
            dgOverview.DataSource = null;
            dgOverview.DataSource = dgViewBindingSource;
            stripText.Text = $"{displayedCount} shares displayed";
            Cursor.Current = Cursors.Default;
            dgOverview.Focus();

        }


        internal void displayProgress(Share share)
        {
            stripText.Text = $"Preparing overview, {share.Number} {share.Name} ...";
        }

        private void InstallOverviewColumns()
        {
            dgOverview.Columns.Clear();

            foreach (string item in listBoxCols.SelectedItems)
            {
                var colName = item.Substring(3);
                if (colName == "Lazy")
                {
                    dgOverview.Columns.Add(
                        new DataGridViewCheckBoxColumn()
                        {
                            Name = item,
                            DataPropertyName = colName,
                            ToolTipText = Overview.PropNameToHint(colName),
                            ReadOnly = true,
                        });
                }
                else
                {
                    //column name includes a column number prefix, so strip it for the DataPropertyName
                    dgOverview.Columns.Add(
                        new DataGridViewTextBoxColumn()
                        {
                            Name = item,
                            DataPropertyName = colName,
                            ToolTipText = Overview.PropNameToHint(colName),
                            ReadOnly = true,
                        });
                    //specify format for numbers
                    if (colName != "Share")
                    {
                        dgOverview.Columns[item].DefaultCellStyle.Format = Overview.NameToFormat(colName);
                    }
                }
            }

            //dgOverview.ColumnHeaderMouseDoubleClick += OnColumnHeaderClicked;

        }

        //private void OnColumnHeaderClicked(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    dgOverview.Sort(dgOverview.Columns[e.ColumnIndex], ListSortDirection.Ascending);
        //}

        //Here we instantiate an initial Overview object for a share and determine is Laziness
        //In order to do this, the passed in AllTable array must have the last 10 days in it
        //in other words 1040 records. Therafter, for the remaining computations, only 
        //105 AllTable records are needed, 1 + 104 (last timeband of penultimate day plus last day)
        private Overview CreateInitialOverviewForShare(Share share, AllTable[] atSegment)
        {
            var lazyShareParams = Helper.GetAppUserSettings().ParamsLazyShare;
            // col 2. Instantiate an Overview object and assign Name of share
            Overview oview = new Overview(share.Name);
            // Lazy flag
            oview.Lazy = OverviewCalcs.isLazyLast10Days(atSegment, lazyShareParams);
            return oview;
        }

        //perform calculations based on passed in 10 day (1040 band) array of All-Table objects
        private void PerformOverviewCalcs(Share share, ref Overview oview, AllTable[] atSegment)
        {
            //col 3: Sum of volumes (LastDayVolume) from row 936 to 1040 (49) 
            oview.SumOfVolumes = OverviewCalcs.SumOfVolumes(atSegment, 9 * 104, 104);  //in full AllTable it would be rows 10298 to 10401);
            //col 4: Price of row 1040 (11) 
            oview.LastPrice = atSegment[1040 - 1].FP;
            //col 5: Price of row 1040-104-1
            oview.DayBeforePrice = atSegment[1040 - 104 - 1].FP;
            //col 6: Price of row 10401 / Price of row 10297
            if (oview.DayBeforePrice > 0) { oview.PriceFactor = oview.LastPrice / oview.DayBeforePrice; }

            // col 7: Price-Gradient PGc of row 1040 (23)
            // First, we need the slow prices injected and Price gradients
            var slowPriceParams = Helper.GetAppUserSettings().ParamsSlowPrice;
            Calculations.MakeSlowPrices(ref atSegment, slowPriceParams, 0, 1040 - 1, out string[] auditSummary);
            Calculations.MakeFiveMinutesPriceGradients(ref atSegment, 1, 1040 - 1, out auditSummary);
            oview.LastPGc = atSegment[1040 - 1].PGc;
            // col 8: 
            oview.LastPGd = atSegment[1040 - 1].PGd;


        }

        //Fills the sharesOverview list of Overview objects (a form field) 
        internal void GenerateOverviewForGrid(Action<Share> progress, List<Overview> sharesOverview)
        {
            var allTablesFolder = Helper.GetAppUserSettings().AllTablesFolder;
            var shareLines = LocalStore.CreateShareArrayFromShareList().Skip(1);
            AllTable[] atSegment = new AllTable[1040];

            sharesOverview.Clear();

            int shareCounter = 0;
            foreach (string line in shareLines)
            {
                var share = Helper.CreateShareFromLine(line);
                var atFilename = allTablesFolder + $@"\alltable_{share.Number}.at";

                //grab last 10 days
                atSegment = Helper.GetAllTableSegment(atFilename, 9362, 1040); // 10297, 105);
                Overview oview = CreateInitialOverviewForShare(share, atSegment);

                //do all the overview calculations
                PerformOverviewCalcs(share, ref oview, atSegment);

                sharesOverview.Add(oview);
                progress(share);
                if (++shareCounter == 10) break;
            }

        }

        //User chooses a new view
        private void comboBoxViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            string viewName = ((ComboBox)sender).Text;
            SetView(viewName);
        }

        //Gets invoked when either user has chosen a View, or a Calculation which requires a view.
        //Select columns in listBoxCols per the view-definitions saved in User settings
        //under the passed in viewName. Therafter, actually ensures those columns are reflected
        //in the DataGridView. 
        private void SetView(string viewName)
        {
            _changingColumns = true;
            if (viewName == "Initial")
            {
                SelectInitialColumnsToView(); // selects listBocCols items, not comboBoxViews items
            }
            else
            {
                //get definition from User settings - its name is the first value in the csv list
                string viewDefinition = null;
                foreach (var item in Helper.GetAppUserSettings().OverviewViews)
                {
                    if (item.StartsWith(viewName))
                    {
                        viewDefinition = item;
                        break;
                    }
                }
                if (viewDefinition != null)
                {
                    SelectColumnsToView(viewDefinition); // selects listBoxCols items, not comboBoxViews items
                }
                else
                {
                    MessageBox.Show($"The required view\n\n'{viewName}'\n\nis not in the Views collection.\n(Please set one up and save as per the name above)",
                        "View not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            _changingColumns = false;
            //get the right columns into the grid
            InstallOverviewColumns();
        }


        private void linkLabelSaveView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new InputBox("Save column arrangement as a view", "Name this view");
            form.textBoxInput.Text = (String)comboBoxViews.SelectedItem;
            var dlgResult = form.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                var viewName = form.returnValue.Replace(",", ""); // no commas allowed in view name!
                // first the view name
                string viewStr = $"{viewName},";
                //then the columns
                foreach (int index in listBoxCols.SelectedIndices)
                {
                    viewStr += $"{index},";
                }
                viewStr = viewStr.TrimEnd(',');

                //save currently selected colums to Usersettings under a name
                var aus = Helper.GetAppUserSettings();
                //overwrite (silently) if view already present - treat as an edit
                foreach (var item in aus.OverviewViews)
                {
                    if (item.StartsWith(viewName))
                    {
                        aus.OverviewViews.Remove(item);
                        break;
                    }
                }
                aus.OverviewViews.Add(viewStr);
                aus.Save();
                //rebind comboBoxViews in order to have the new view in the drop down
                LoadViewsComboBox(viewStr);

            }
        }

        private void linkLabelLock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabelLock.Text = linkLabelLock.Text == "lock view" ? "unlock view" : "lock view";
            listBoxCols.Enabled = !listBoxCols.Enabled;
        }

        //fired when user selects/deselects an item in the lh listbox (listBoxCols)
        //whereby he wants to add or remove the corresponding datagridview column
        private void listBoxCols_SelectedIndexChanged(object sender, EventArgs e)
        {
            //don't react while shift key is being depressed
            if ((Control.ModifierKeys & Keys.Shift) != 0) return;

            if (_loaded && !_changingColumns) InstallOverviewColumns();
        }

        private void linkLabelLazy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //hide / unhide Lazy shares
            if (linkLabelLazy.Text.Contains("Show"))
            {
                //show 'em
                RebindDatagridView(false);
                linkLabelLazy.Text = "Hide Lazy Shares";
            }
            else
            {
                //hide 'em
                RebindDatagridView(true);
                linkLabelLazy.Text = "Show All Shares";
            }

        }

        internal void HandleParameterSaveClick(object sender, EventArgs e)
        {
            var aus = Helper.GetAppUserSettings();
            string calculation = (string)((Button)sender).Tag;
            switch (calculation)
            {
                case "Identify Lazy Shares":
                    aus.ParamsLazyShare = CalcLazyShareParam;
                    aus.Save();
                    stripText.Text = "Parameter saved";
                    break;
                case "Make Slow (Five minutes) Prices SP":
                    aus.ParamsSlowPrice = CalcSlowPriceParam;
                    aus.Save();
                    //SaveOverview()
                    break;
                case "Make Five minutes Price Gradients PG":
                    //SaveOverview();
                    break;
                case "Find direction and Turning":
                    break;
                case "Find Five minutes Gradients Figure PGF":
                    break;
                case "Related volume Figure (RPGFV) of biggest PGF":
                    break;
                case "Make High Line HL":
                    break;
                case "Make Low Line LL":
                    break;
                case "Make Slow Volumes SV":
                    break;
                case "Slow Volume Figure SVFac":
                    break;
                case "Slow Volume Figure SVFbd":
                    break;
                default:
                    break;
            }

        }

        private void SaveOverview(string overviewFilename)
        {
            using (FileStream fs = new FileStream(overviewFilename, FileMode.Create))
            {
                foreach (Overview item in sharesOverview)
                {
                    Helper.SerializeOverviewRecord(fs, item);
                }
            }
            stripText.Text = $"Saved {overviewFilename}";
        }

        internal void HandleCalculationClick(object sender, EventArgs e)
        {
            //string calculation = (string)((Button)sender).Tag;
            //var auditLines = new string[] { "" };
            //Cursor.Current = Cursors.WaitCursor;
            //switch (calculation)
            //{
            //    case "Identify Lazy Shares":
            //        Calculations.LazyShare(atRows, CalcLazyShareParam, 9362, 10401, out auditLines);
            //        calcAuditTextBox.Lines = auditLines;
            //        break;
            //    case "Make Slow (Five minutes) Prices SP":
            //        Calculations.MakeSlowPrices(ref atRows, CalcSlowPriceParam, 1, 10401, out auditLines);
            //        calcAuditTextBox.Lines = auditLines;
            //        // atRows must be re-bound to the DataGridView
            //        BindDataGridViewToResults(determineVerticalMode());
            //        break;
            //    case "Make Five minutes Price Gradients PG":
            //        Calculations.MakeFiveMinutesPriceGradients(ref atRows, 1, 10401, out auditLines);
            //        calcAuditTextBox.Lines = auditLines;
            //        // atRows must be re-bound to the DataGridView
            //        BindDataGridViewToResults(determineVerticalMode());
            //        break;
            //    case "Find direction and Turning":
            //        break;
            //    case "Find Five minutes Gradients Figure PGF":
            //        break;
            //    case "Related volume Figure (RPGFV) of biggest PGF":
            //        break;
            //    case "Make High Line HL":
            //        break;
            //    case "Make Low Line LL":
            //        break;
            //    case "Make Slow Volumes SV":
            //        break;
            //    case "Slow Volume Figure SVFac":
            //        break;
            //    case "Slow Volume Figure SVFbd":
            //        break;
            //    default:
            //        break;
            //}
            //Cursor.Current = Cursors.Default;
        }

        private TextBox AuditTextBox(string[] auditOutcome)
        {
            var auditBox = new TextBox();
            auditBox.Location = new Point(270, 12);
            auditBox.Size = new Size(280, groupBoxParams.Height - 20);
            //auditBox.AutoSize = true;
            auditBox.Multiline = true;
            auditBox.WordWrap = true;
            auditBox.ReadOnly = true;
            auditBox.Lines = auditOutcome;
            auditBox.Font = new Font("Lucida Console", 8, FontStyle.Regular);
            //auditBox.ForeColor = Color.IndianRed;
            auditBox.Visible = true;
            return auditBox;
        }


        //User has chosen a Calculation. Show Parameters appropriate for the calculation
        //and invoke a View by the same name as that of the Calculation
        private void listBoxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var affirmNumRows = $"There are {atRows.Count()} records in this All-Table";
            //stripText.Text = affirmNumRows;

            groupBoxParams.Controls.Clear();
            string calculation = ((ListBox)sender).Text;
            groupBoxParams.Text = calculation;

            //(re)-enable the views combobox if user moves off the calculations
            //Note: the Views combo-box is disabled while a user is doing a calculation
            comboBoxViews.Enabled = calculation.StartsWith("*");

            //Invoke view by same name
            int wantedViewIndex = comboBoxViews.Items.IndexOf(calculation);
            if (wantedViewIndex != -1)
            {
                comboBoxViews.SelectedIndex = wantedViewIndex;
            }

            MediateCalculate(calculation);

        }

        private void MediateCalculate(string calculation)
        {
            switch (calculation)
            {
                case "Identify Lazy Shares":
                    //show a bound params property grid with init values taken from current LazyShareParam settings
                    CalcLazyShareParam = new LazyShareParam(CurrLazyShareParam.From, CurrLazyShareParam.To, CurrLazyShareParam.Setting);
                    var propGridLazy = LazyShareUI.PropertyGridParams(CalcLazyShareParam, groupBoxParams.Height - 20);
                    var btnPairLazy = LazyShareUI.CalcAndSaveBtns(calculation, HandleCalculationClick, HandleParameterSaveClick);
                    calcAuditTextBox = AuditTextBox(new string[] { "Adjust settings then press 'Calculate' to (re)evaluate" });
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridLazy);
                    groupBoxParams.Controls.Add(btnPairLazy[0]);
                    groupBoxParams.Controls.Add(btnPairLazy[1]);
                    groupBoxParams.Controls.Add(calcAuditTextBox);
                    break;

                case "Make Slow (Five minutes) Prices SP":
                    //show a bound params property grid with init values taken from current SlowPriceParam settings
                    CalcSlowPriceParam = new SlowPriceParam(CurrSlowPriceParam.ZMin, CurrSlowPriceParam.ZMax,
                        CurrSlowPriceParam.YMin, CurrSlowPriceParam.YMax, CurrSlowPriceParam.Z);
                    CalcSlowPriceParam.Ya = CurrSlowPriceParam.Ya;
                    CalcSlowPriceParam.Yb = CurrSlowPriceParam.Yb;
                    CalcSlowPriceParam.Yc = CurrSlowPriceParam.Yc;
                    CalcSlowPriceParam.Yd = CurrSlowPriceParam.Yd;
                    var propGridSlow = SlowPriceUI.PropertyGridParams(CalcSlowPriceParam, groupBoxParams.Height - 20);
                    var btnPairSlow = SlowPriceUI.CalcAndSaveBtns(calculation, HandleCalculationClick, HandleParameterSaveClick);
                    calcAuditTextBox = AuditTextBox(new string[] { "Adjust settings then press 'Calculate' to (re)evaluate" });
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridSlow);
                    groupBoxParams.Controls.Add(btnPairSlow[0]);
                    groupBoxParams.Controls.Add(btnPairSlow[1]);
                    groupBoxParams.Controls.Add(calcAuditTextBox);
                    break;
                case "Make Five minutes Price Gradients PG":
                    var propGridPg = FiveMinutesPriceGradientsUI.PropertyGridParams(groupBoxParams.Height - 20);
                    var btnPg = FiveMinutesPriceGradientsUI.CalcAndSaveBtns(calculation, HandleCalculationClick, HandleParameterSaveClick);
                    calcAuditTextBox = AuditTextBox(new string[] { "There are NO parameters for this calclation. Press 'Calculate' to (re)evaluate" });
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridPg);
                    groupBoxParams.Controls.Add(btnPg[0]);
                    groupBoxParams.Controls.Add(btnPg[1]);
                    groupBoxParams.Controls.Add(calcAuditTextBox);
                    break;

                case "Find direction and Turning":
                    break;
                case "Find Five minutes Gradients Figure PGF":
                    break;
                case "Related volume Figure (RPGFV) of biggest PGF":
                    break;
                case "Make High Line HL":
                    break;
                case "Make Low Line LL":
                    break;
                case "Make Slow Volumes SV":
                    break;
                case "Slow Volume Figure SVFac":
                    break;
                case "Slow Volume Figure SVFbd":
                    break;
                default:
                    break;
            }
        }
    }
}
