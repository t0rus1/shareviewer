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
        private bool _sharesBeenDiscarded = false;


        internal TextBox calcAuditTextBox;
        //CURRENT properties
        internal LazyShareParam CurrLazyShareParam { get => Helper.GetAppUserSettings().ParamsLazyShare; }
        internal SlowPriceParam CurrSlowPriceParam { get => Helper.GetAppUserSettings().ParamsSlowPrice; }
        internal DirectionAndTurningParam CurrDirectionAndTurningParam { get => Helper.GetAppUserSettings().ParamsDirectionAndTurning; }
        internal FiveMinsGradientFigureParam CurrFiveMinsGradientFigureParam { get => Helper.GetAppUserSettings().ParamsFiveMinsGradientFigure; }

        //CALCULATION properties (we bind these to a property grid)
        private LazyShareParam calcLazyShareParam;
        private SlowPriceParam calcSlowPriceParam;
        private DirectionAndTurningParam calcDirectionAndTurningParam;
        private FiveMinsGradientFigureParam calcFiveMinsGradientFigureParam;
        internal LazyShareParam CalcLazyShareParam { get => calcLazyShareParam; set => calcLazyShareParam = value; }
        internal SlowPriceParam CalcSlowPriceParam { get => calcSlowPriceParam; set => calcSlowPriceParam = value; }
        internal DirectionAndTurningParam CalcDirectionAndTurningParam { get => calcDirectionAndTurningParam; set => calcDirectionAndTurningParam = value; }
        internal FiveMinsGradientFigureParam CalcFiveMinsGradientFigureParam { get => calcFiveMinsGradientFigureParam; set => calcFiveMinsGradientFigureParam = value; }

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
            var rangeFrom = Helper.GetAppUserSettings().AllTableDataStart;
            var tradingSpan = Helper.GetAppUserSettings().AllTableTradingSpan;
            this.Text = $"Overview. All-Tables from '{rangeFrom}' for {tradingSpan} trading days";

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
            //this.Enabled = false;
            //BindDatagridView();

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

        private void GenerateOverviewAndBindDataGrid()
        {
            Cursor.Current = Cursors.WaitCursor;

            dgViewBindingSource.Clear();

            var task = Task.Run(() => GenerateOverview(displayProgress));
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                BindDatagridView();
                stripText.Text = $"{sharesOverview.Count} shares.";
                Cursor.Current = Cursors.Default;
                this.Enabled = true;
                dgOverview.Focus();
            });
        }

        private void BindDatagridView()
        {
            foreach (Overview overview in sharesOverview)
            {
                dgViewBindingSource.Add(overview);
            }
            dgOverview.DataSource = dgViewBindingSource;
        }

        //will Hide/Unhide shares flagged as Lazy
        //private void RebindDatagridView(bool hideLazy)
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    dgViewBindingSource.Clear();

        //    int displayedCount = 0;
        //    foreach (Overview overview in sharesOverview)
        //    {
        //        if (hideLazy)
        //        {
        //            if (!overview.Lazy)
        //            {
        //                dgViewBindingSource.Add(overview);
        //                displayedCount++;
        //            }
        //        }
        //        else
        //        {
        //            dgViewBindingSource.Add(overview);
        //            displayedCount++;
        //        }
        //    }
        //    dgOverview.DataSource = null;
        //    dgOverview.DataSource = dgViewBindingSource;
        //    stripText.Text = $"{displayedCount} shares displayed";
        //    Cursor.Current = Cursors.Default;
        //    dgOverview.Focus();

        //}

        private void DiscardLazyShares()
        {
            dgViewBindingSource.Clear();

            int priorCount = sharesOverview.Count;
            sharesOverview.RemoveAll(ov => ov.Lazy);
            int postCount = sharesOverview.Count;
            int numDiscards = priorCount - postCount;
            _sharesBeenDiscarded = (numDiscards > 0);

            foreach (Overview overview in sharesOverview)
            {
                dgViewBindingSource.Add(overview);
            }

            dgOverview.DataSource = null;
            dgOverview.DataSource = dgViewBindingSource;
            stripText.Text = $"{numDiscards} shares discarded. {postCount} shares remainining";
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
                            //ReadOnly = true,
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

        


        //Fills the sharesOverview list of Overview objects (a form field) 
        internal void GenerateOverview(Action<Share> progress)
        {
            var allTablesFolder = Helper.GetAppUserSettings().AllTablesFolder;
            var shareLines = LocalStore.CreateShareArrayFromShareList().Skip(1);
            AllTable[] atSegment = new AllTable[10401];

            sharesOverview.Clear();

            foreach (string line in shareLines)
            {
                var share = Helper.CreateShareFromLine(line);
                var atFilename = allTablesFolder + $@"\alltable_{share.Number}.at";

                //load up the full AllTable
                atSegment = AllTable.GetAllTableRows(atFilename, 10402);

                // ##### ASSUME CALCS ALREADY DONE?
                //Calculations.PerformShareCalculations(share, atSegment);
                //AllTable.SaveAllTable(atFilename, atSegment);

                //then the Overview
                Overview oview = OverviewCalcs.CreateInitialOverviewForShare(share, atSegment);
                OverviewCalcs.PerformOverviewCalcs(share, ref oview, atSegment);

                sharesOverview.Add(oview);
                progress(share);

                //TODO comment out!
                //if (++shareCounter == 20) break;

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

        //Discard LAZY shares from grid
        private void linkLabelLazy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //RebindDatagridView(true);
            DiscardLazyShares();
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
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;
                case "Make Slow (Five minutes) Prices SP":
                    aus.ParamsSlowPrice = CalcSlowPriceParam;
                    aus.Save();
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;
                case "Make Five minutes Price Gradients PG":
                    //no params to save
                    break;
                case "Find direction and Turning":
                    aus.ParamsDirectionAndTurning = CalcDirectionAndTurningParam;
                    aus.Save();
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;
                case "Find Five minutes Gradients Figure PGF":
                    aus.ParamsFiveMinsGradientFigure = CalcFiveMinsGradientFigureParam;
                    aus.Save();
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;

                case "Related volume Figure (RPGFV) of biggest PGF":
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;
                case "Make High Line HL":
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;
                case "Make Low Line LL":
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;
                case "Make Slow Volumes SV":
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;
                case "Slow Volume Figure SVFac":
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;
                case "Slow Volume Figure SVFbd":
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    break;
                default:
                    break;
            }

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
            Cursor.Current = Cursors.WaitCursor;

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

            Cursor.Current = Cursors.Default;
            dgOverview.Focus();

        }

        private void MediateCalculate(string calculation)
        {
            switch (calculation)
            {
                case "Identify Lazy Shares":
                    //show a bound params property grid with init values taken from current LazyShareParam settings
                    CalcLazyShareParam = new LazyShareParam(CurrLazyShareParam.From, CurrLazyShareParam.To, CurrLazyShareParam.Setting);
                    var propGridLazy = LazyShareUI.PropertyGridParams(CalcLazyShareParam, groupBoxParams.Height - 20);
                    var btnPairLazy = LazyShareUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //calcAuditTextBox = AuditTextBox(new string[] { "Adjust settings then press 'Calculate' to (re)evaluate" });
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridLazy);
                    groupBoxParams.Controls.Add(btnPairLazy[0]);
                    groupBoxParams.Controls.Add(btnPairLazy[1]);
                    //groupBoxParams.Controls.Add(calcAuditTextBox);
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
                    var btnPairSlow = SlowPriceUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //calcAuditTextBox = AuditTextBox(new string[] { "Adjust settings then press 'Calculate' to (re)evaluate" });
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridSlow);
                    groupBoxParams.Controls.Add(btnPairSlow[0]);
                    groupBoxParams.Controls.Add(btnPairSlow[1]);
                    //groupBoxParams.Controls.Add(calcAuditTextBox);
                    break;
                case "Make Five minutes Price Gradients PG":
                    var propGridPg = FiveMinutesPriceGradientsUI.PropertyGridParams(groupBoxParams.Height - 20);
                    var btnPg = FiveMinutesPriceGradientsUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //calcAuditTextBox = AuditTextBox(new string[] { "There are NO parameters for this calculation. Press 'Calculate' to (re)evaluate" });
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridPg);
                    groupBoxParams.Controls.Add(btnPg[0]);
                    groupBoxParams.Controls.Add(btnPg[1]);
                    //groupBoxParams.Controls.Add(calcAuditTextBox);
                    break;
                case "Find direction and Turning":
                    CalcDirectionAndTurningParam = new DirectionAndTurningParam(
                        CurrDirectionAndTurningParam.From, 
                        CurrDirectionAndTurningParam.To, 
                        CurrDirectionAndTurningParam.PGcThreshold, 
                        CurrDirectionAndTurningParam.Z);
                    var propGridDandT = DirectionAndTurningUI.PropertyGridParams(CalcDirectionAndTurningParam, groupBoxParams.Height - 20);
                    var btnPairDandT = DirectionAndTurningUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    groupBoxParams.Controls.Add(propGridDandT);
                    groupBoxParams.Controls.Add(btnPairDandT[0]);
                    groupBoxParams.Controls.Add(btnPairDandT[1]);
                    break;
                case "Find Five minutes Gradients Figure PGF":
                    CalcFiveMinsGradientFigureParam = new FiveMinsGradientFigureParam(
                        CurrFiveMinsGradientFigureParam.ZMin,
                        CurrFiveMinsGradientFigureParam.ZMax,
                        CurrFiveMinsGradientFigureParam.Z,
                        CurrFiveMinsGradientFigureParam.XMin,
                        CurrFiveMinsGradientFigureParam.XMax,
                        CurrFiveMinsGradientFigureParam.X,
                        CurrFiveMinsGradientFigureParam.YMin,
                        CurrFiveMinsGradientFigureParam.YMax,
                        CurrFiveMinsGradientFigureParam.Y
                        );
                    var propGridFiveMinGradFig = FiveMinsGradientFigureUI.PropertyGridParams(CalcFiveMinsGradientFigureParam, groupBoxParams.Height - 20);
                    var btnPairFiveMinGradFig = FiveMinsGradientFigureUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    groupBoxParams.Controls.Add(propGridFiveMinGradFig);
                    groupBoxParams.Controls.Add(btnPairFiveMinGradFig[0]);
                    groupBoxParams.Controls.Add(btnPairFiveMinGradFig[1]);
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

        //re do all calculationsa and populate grid afresh
        private void buttonCalcAll_Click(object sender, EventArgs e)
        {
            //this re-introduces discarded shares
            GenerateOverviewAndBindDataGrid();
        }

        private void dgOverview_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            var shareName = ((Overview)dgOverview.Rows[e.RowIndex].DataBoundItem).ShareName;
            var shareNum = ((Overview)dgOverview.Rows[e.RowIndex].DataBoundItem).ShareNumber;

            string allTableFilename = Helper.GetAppUserSettings().AllTablesFolder + $"\\alltable_{shareNum}.at";
            if (File.Exists(allTableFilename))
            {
                var AtShareForm = new SingleAllTableForm(allTableFilename, shareName);
                AtShareForm.Text = $"[{shareNum}] {shareName}";
                AtShareForm.Show();
            }
            else
            {
                MessageBox.Show($"All table for share {shareNum} not found.\nBe sure to generate it.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void SaveOverview(string overviewFilename)
        {
            int itemCount = 0;
            using (FileStream fs = new FileStream(overviewFilename, FileMode.Create))
            {
                foreach (Overview item in sharesOverview)
                {
                    Helper.SerializeOverviewRecord(fs, item);
                    itemCount++;
                }
            }
            stripText.Text = $"Saved {itemCount} lines.";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (dgOverview.Rows.Count > 1)
            {
                string overviewFilename = Helper.GetAppUserSettings().AllTablesFolder + $"\\overview.at";
                SaveOverview(overviewFilename);
            }
            else
            {
                stripText.Text = "Nothing to save!";
            }
        }

        private void linkLabelLoad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            string overviewFilename = Helper.GetAppUserSettings().AllTablesFolder + $"\\overview.at";
            if (File.Exists(overviewFilename))
            {
                FileInfo fileInfo = new FileInfo(overviewFilename);

                var dlgResult = MessageBox.Show(
                    $"Last saved overview was at {fileInfo.LastWriteTime.ToLocalTime()}.\nLoad this one?", 
                    "Confirm Load", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dlgResult == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    dgViewBindingSource.Clear();

                    using (FileStream fs = new FileStream(overviewFilename, FileMode.Open))
                    {
                        //slurp in previously saved file
                        sharesOverview = (List<Overview>)Helper.DeserializeOverview<Overview>(fs);

                        BindDatagridView();

                        stripText.Text = $"{sharesOverview.Count} shares.";
                        Cursor.Current = Cursors.Default;
                        dgOverview.Focus();
                    }
                }
            }


        }
    }
}
