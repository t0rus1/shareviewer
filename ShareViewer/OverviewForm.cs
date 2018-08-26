using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        List<Overview> sharesOverviewPreFiltering = new List<Overview>();
        OverviewRowFilterForm rowFilterForm;
        Dictionary<string, OverviewFilter> rowFilters = new Dictionary<string, OverviewFilter>();

        bool _loaded = false;
        bool _loadingCols = false;
        bool _changingColumns = false;
        bool _sharesBeenDiscarded = false;
        bool _FullRecalcNeeded = false;
        string _curOverviewLoadname = "";

        internal TextBox calcAuditTextBox;
        //CURRENT properties
        internal LazyShareParam CurrLazyShareParam { get => Helper.GetAppUserSettings().ParamsLazyShare; }
        internal SlowPriceParam CurrSlowPriceParam { get => Helper.GetAppUserSettings().ParamsSlowPrice; }
        internal DirectionAndTurningParam CurrDirectionAndTurningParam { get => Helper.GetAppUserSettings().ParamsDirectionAndTurning; }
        internal FiveMinsGradientFigureParam CurrFiveMinsGradientFigureParam { get => Helper.GetAppUserSettings().ParamsFiveMinsGradientFigure; }
        internal MakeHighLineParam CurrHighLineParam { get => Helper.GetAppUserSettings().ParamsMakeHighLine; }
        internal MakeLowLineParam CurrLowLineParam { get => Helper.GetAppUserSettings().ParamsMakeLowLine;  }
        internal MakeSlowVolumeParam CurrSlowVolumeParam { get => Helper.GetAppUserSettings().ParamsMakeSlowVolume; }
        internal SlowVolFigSVFacParam CurrSlowVolFigSVFacParam { get => Helper.GetAppUserSettings().ParamsSlowVolFigSVFac; }
        internal SlowVolFigSVFbdParam CurrSlowVolFigSVFbdParam { get => Helper.GetAppUserSettings().ParamsSlowVolFigSVFbd; }

        //CALCULATION properties (we bind these to a property grid)
        LazyShareParam calcLazyShareParam;
        SlowPriceParam calcSlowPriceParam;
        DirectionAndTurningParam calcDirectionAndTurningParam;
        FiveMinsGradientFigureParam calcFiveMinsGradientFigureParam;
        MakeHighLineParam calcHighLineParam;
        MakeLowLineParam calcLowLineParam;
        private MakeSlowVolumeParam calcSlowVolumeParam;
        private SlowVolFigSVFacParam calcSlowVolFigSVFacParam;
        private SlowVolFigSVFbdParam calcSlowVolFigSVFbd;

        internal LazyShareParam CalcLazyShareParam { get => calcLazyShareParam; set => calcLazyShareParam = value; }
        internal SlowPriceParam CalcSlowPriceParam { get => calcSlowPriceParam; set => calcSlowPriceParam = value; }
        internal DirectionAndTurningParam CalcDirectionAndTurningParam { get => calcDirectionAndTurningParam; set => calcDirectionAndTurningParam = value; }
        internal FiveMinsGradientFigureParam CalcFiveMinsGradientFigureParam { get => calcFiveMinsGradientFigureParam; set => calcFiveMinsGradientFigureParam = value; }
        internal MakeHighLineParam CalcHighLineParam { get => calcHighLineParam; set => calcHighLineParam = value; }
        internal MakeLowLineParam CalcLowLineParam { get => calcLowLineParam; set => calcLowLineParam = value; }
        internal MakeSlowVolumeParam CalcSlowVolumeParam { get => calcSlowVolumeParam; set => calcSlowVolumeParam = value; }
        internal SlowVolFigSVFacParam CalcSlowVolFigSVFacParam { get => calcSlowVolFigSVFacParam; set => calcSlowVolFigSVFacParam = value; }
        internal SlowVolFigSVFbdParam CalcSlowVolFigSVFbdParam { get => calcSlowVolFigSVFbd; set => calcSlowVolFigSVFbd = value; }

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
            dgOverview.EnableHeadersVisualStyles = false;
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
            if (_FullRecalcNeeded)
            {
                stripText.Text = $"FULL RECALC overview, {share.Number} {share.Name} ...";
            }
            else
            {
                stripText.Text = $"Compiling overview, {share.Number} {share.Name} ...";
            }
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
                            SortMode = DataGridViewColumnSortMode.Programmatic,
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
                            SortMode = DataGridViewColumnSortMode.Programmatic,
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


        //Fills the sharesOverview list of Overview objects(a form field)
        internal void GenerateOverview(Action<Share> progress)
        {
            var allTablesFolder = Helper.GetAppUserSettings().AllTablesFolder;
            var shareLines = LocalStore.CreateShareArrayFromShareList();
            AllTable[] atSegment = new AllTable[10402];

            sharesOverview.Clear();

            foreach (string line in shareLines)
            {
                var share = Helper.CreateShareFromLine(line);
                var atFilename = allTablesFolder + $@"\alltable_{share.Number}.at";


                if (_FullRecalcNeeded)
                {
                    //load up the full AllTable
                    atSegment = AllTable.GetAllTableRows(atFilename, 10402);
                    Calculations.PerformShareCalculations(share, ref atSegment); //also stores laziness in Col_2 row 0
                    Calculations.Row1Calcs(share, ref atSegment);
                    AllTable.SaveAllTable(atFilename, ref atSegment);
                    //then the Overview
                    //instantiate an Overview object and initialize it - includes grabbing the Lazy status from Col_2 row 0
                    Overview oview = OverviewCalcs.CreateInitialOverviewForShare(share, atSegment[1]);
                    OverviewCalcs.PerformOverviewCalcs(share, ref oview, atSegment);
                    sharesOverview.Add(oview);
                }
                else
                {
                    // No calcs needed, we must build an Overview object from row 1 of the AllTable
                    AllTable atRow1 = AllTable.GetSingleAllTableRow(atFilename, 1);
                    Overview oview = OverviewCalcs.CreateInitialOverviewForShare(share, atRow1);
                    OverviewCalcs.OverviewFromLastRow(share, ref oview, atRow1);
                    sharesOverview.Add(oview);
                }

                progress(share);

            }

            _FullRecalcNeeded = false;

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
                    if (aus.ParamsLazyShare.DiffersFrom(CalcLazyShareParam))
                    {
                        aus.ParamsLazyShare = CalcLazyShareParam;
                        aus.Save();
                        toolStripCalcs.Text = $"Changed '{calculation}' parameter saved. You should RE-CALCULATE.";
                        _FullRecalcNeeded = true;
                    }
                    break;
                case "Make Slow (Five minutes) Prices SP":
                    if (aus.ParamsSlowPrice.DiffersFrom(CalcSlowPriceParam))
                    {
                        aus.ParamsSlowPrice = CalcSlowPriceParam;
                        aus.Save();
                        toolStripCalcs.Text = $"Changed '{calculation}' parameter saved. You should RE-CALCULATE.";
                        _FullRecalcNeeded = true;
                    }
                    break;
                case "Make Five minutes Price Gradients PG":
                    //no params to save
                    break;
                case "Find direction and Turning":
                    if (aus.ParamsDirectionAndTurning.DiffersFrom(CalcDirectionAndTurningParam))
                    {
                        aus.ParamsDirectionAndTurning = CalcDirectionAndTurningParam;
                        aus.Save();
                        toolStripCalcs.Text = $"Changed '{calculation}' parameter saved. You should RE-CALCULATE.";
                        _FullRecalcNeeded = true;
                    }
                    break;
                case "Find Five minutes Gradients Figure PGF":
                    if (aus.ParamsFiveMinsGradientFigure.DiffersFrom(calcFiveMinsGradientFigureParam))
                    {
                        aus.ParamsFiveMinsGradientFigure = CalcFiveMinsGradientFigureParam;
                        aus.Save();
                        toolStripCalcs.Text = $"Changed '{calculation}' parameter saved. You should RE-CALCULATE.";
                        _FullRecalcNeeded = true;
                    }
                    break;

                case "Related volume Figure (RPGFV) of biggest PGF":
                    toolStripCalcs.Text = $"{calculation} Parameter saved";
                    _FullRecalcNeeded = true;
                    break;
                case "Make High Line HL":
                    if (aus.ParamsMakeHighLine.DiffersFrom(calcHighLineParam))
                    {
                        aus.ParamsMakeHighLine = CalcHighLineParam;
                        aus.Save();
                        toolStripCalcs.Text = $"{calculation} Parameter saved";
                        _FullRecalcNeeded = true;
                    }
                    break;
                case "Make Low Line LL":
                    if (aus.ParamsMakeLowLine.DiffersFrom(calcLowLineParam))
                    {
                        aus.ParamsMakeLowLine = CalcLowLineParam;
                        aus.Save();
                        toolStripCalcs.Text = $"{calculation} Parameter saved";
                        _FullRecalcNeeded = true;
                    }
                    break;
                case "Make Slow Volumes SV":
                    if (aus.ParamsMakeSlowVolume.DiffersFrom(calcSlowVolumeParam))
                    {
                        aus.ParamsMakeSlowVolume = CalcSlowVolumeParam;
                        aus.Save();
                        toolStripCalcs.Text = $"{calculation} Parameter saved";
                        _FullRecalcNeeded = true;
                    }
                    break;
                case "Slow Volume Figure SVFac":
                    if (aus.ParamsSlowVolFigSVFac.DiffersFrom(calcSlowVolFigSVFacParam)) {
                        aus.ParamsSlowVolFigSVFac = CalcSlowVolFigSVFacParam;
                        aus.Save();
                        toolStripCalcs.Text = $"{calculation} Parameter saved";
                        _FullRecalcNeeded = true;
                    }
                    break;
                case "Slow Volume Figure SVFbd":
                    if (aus.ParamsSlowVolFigSVFbd.DiffersFrom(calcSlowVolFigSVFbd))
                    {
                        aus.ParamsSlowVolFigSVFbd = CalcSlowVolFigSVFbdParam;
                        aus.Save();
                        toolStripCalcs.Text = $"{calculation} Parameter saved";
                        _FullRecalcNeeded = true;
                    }
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
        private void listBoxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            toolStripCalcs.Text = "";
            groupBoxParams.Controls.Clear();
            string calculation = ((ListBox)sender).Text;
            groupBoxParams.Text = calculation;

            //(re)-enable the views combobox if user moves off the calculations
            //Note: the Views combo-box is disabled while a user is doing a calculation
            comboBoxViews.Enabled = calculation.StartsWith("*");

            //Invoke view by same name
            //int wantedViewIndex = comboBoxViews.Items.IndexOf(calculation);
            //if (wantedViewIndex != -1)
            //{
            //    comboBoxViews.SelectedIndex = wantedViewIndex;
            //}

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
                    CalcLazyShareParam = new LazyShareParam(CurrLazyShareParam.Setting);
                    var propGridLazy = LazyShareUI.PropertyGridParams(CalcLazyShareParam, groupBoxParams.Height - 20);
                    var btnPairLazy = LazyShareUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridLazy);
                    groupBoxParams.Controls.Add(btnPairLazy[0]);
                    groupBoxParams.Controls.Add(btnPairLazy[1]);
                    break;

                case "Make Slow (Five minutes) Prices SP":
                    //show a bound params property grid with init values taken from current SlowPriceParam settings
                    CalcSlowPriceParam = new SlowPriceParam(CurrSlowPriceParam.Z, CurrSlowPriceParam.Ya, CurrSlowPriceParam.Yb, CurrSlowPriceParam.Yc, CurrSlowPriceParam.Yd);
                    var propGridSlow = SlowPriceUI.PropertyGridParams(CalcSlowPriceParam, groupBoxParams.Height - 20);
                    var btnPairSlow = SlowPriceUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridSlow);
                    groupBoxParams.Controls.Add(btnPairSlow[0]);
                    groupBoxParams.Controls.Add(btnPairSlow[1]);
                    break;
                case "Make Five minutes Price Gradients PG":
                    var propGridPg = FiveMinutesPriceGradientsUI.PropertyGridParams(groupBoxParams.Height - 20);
                    var btnPg = FiveMinutesPriceGradientsUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridPg);
                    groupBoxParams.Controls.Add(btnPg[0]);
                    groupBoxParams.Controls.Add(btnPg[1]);
                    break;
                case "Find direction and Turning":
                    CalcDirectionAndTurningParam = new DirectionAndTurningParam(CurrDirectionAndTurningParam.Z);
                    var propGridDandT = DirectionAndTurningUI.PropertyGridParams(CalcDirectionAndTurningParam, groupBoxParams.Height - 20);
                    var btnPairDandT = DirectionAndTurningUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    groupBoxParams.Controls.Add(propGridDandT);
                    groupBoxParams.Controls.Add(btnPairDandT[0]);
                    groupBoxParams.Controls.Add(btnPairDandT[1]);
                    break;
                case "Find Five minutes Gradients Figure PGF":
                    CalcFiveMinsGradientFigureParam = new FiveMinsGradientFigureParam(
                        CurrFiveMinsGradientFigureParam.Z,
                        CurrFiveMinsGradientFigureParam.X,
                        CurrFiveMinsGradientFigureParam.Y
                        );
                    var propGridFiveMinGradFig = FiveMinsGradientFigureUI.PropertyGridParams(CalcFiveMinsGradientFigureParam, groupBoxParams.Height - 20);
                    var btnPairFiveMinGradFig = FiveMinsGradientFigureUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    groupBoxParams.Controls.Add(propGridFiveMinGradFig);
                    groupBoxParams.Controls.Add(btnPairFiveMinGradFig[0]);
                    groupBoxParams.Controls.Add(btnPairFiveMinGradFig[1]);
                    break;
                case "Related volume Figure (RPGFV) of biggest PGF":
                    var propGridRv = RelatedVolumeFigureOfBiggestPGFUI.PropertyGridParams(groupBoxParams.Height - 20);
                    var btnRv = RelatedVolumeFigureOfBiggestPGFUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridRv);
                    groupBoxParams.Controls.Add(btnRv[0]);
                    groupBoxParams.Controls.Add(btnRv[1]);
                    break;
                case "Make High Line HL":
                    CalcHighLineParam = new MakeHighLineParam(CurrHighLineParam.Z);
                    var propGridHL = MakeHighLineParamUI.PropertyGridParams(CalcHighLineParam, groupBoxParams.Height - 20);
                    var btnPairHL = MakeHighLineParamUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridHL);
                    groupBoxParams.Controls.Add(btnPairHL[0]);
                    groupBoxParams.Controls.Add(btnPairHL[1]);
                    break;
                case "Make Low Line LL":
                    CalcLowLineParam = new MakeLowLineParam(CurrLowLineParam.Z);
                    var propGridLL = MakeLowLineParamUI.PropertyGridParams(CalcLowLineParam, groupBoxParams.Height - 20);
                    var btnPairLL = MakeLowLineParamUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridLL);
                    groupBoxParams.Controls.Add(btnPairLL[0]);
                    groupBoxParams.Controls.Add(btnPairLL[1]);
                    break;
                case "Make Slow Volumes SV":
                    CalcSlowVolumeParam = new MakeSlowVolumeParam(
                        CurrSlowVolumeParam.YMin, CurrSlowVolumeParam.YMax, CurrSlowVolumeParam.Ya,
                        CurrSlowVolumeParam.Yb, CurrSlowVolumeParam.Yc, CurrSlowVolumeParam.Yd, CurrSlowVolumeParam.X);
                    var propGridSV = MakeSlowVolumeUI.PropertyGridParams(CalcSlowVolumeParam, groupBoxParams.Height - 20);
                    var btnPairSV = MakeSlowVolumeUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridSV);
                    groupBoxParams.Controls.Add(btnPairSV[0]);
                    groupBoxParams.Controls.Add(btnPairSV[1]);
                    break;
                case "Slow Volume Figure SVFac":
                    CalcSlowVolFigSVFacParam = new SlowVolFigSVFacParam(
                        CurrSlowVolFigSVFacParam.X,
                        CurrSlowVolFigSVFacParam.Y,
                        CurrSlowVolFigSVFacParam.Z,
                        CurrSlowVolFigSVFacParam.W);
                    var propGridSVFac = SlowVolFigSVFacUI.PropertyGridParams(CalcSlowVolFigSVFacParam, groupBoxParams.Height - 20);
                    var btnPairSVFac = SlowVolFigSVFacUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridSVFac);
                    groupBoxParams.Controls.Add(btnPairSVFac[0]);
                    groupBoxParams.Controls.Add(btnPairSVFac[1]);
                    break;
                case "Slow Volume Figure SVFbd":
                    CalcSlowVolFigSVFbdParam = new SlowVolFigSVFbdParam(CurrSlowVolFigSVFbdParam.Z,CurrSlowVolFigSVFbdParam.Y,CurrSlowVolFigSVFbdParam.W);
                    var propGridSVFbd = SlowVolFigSVFbdUI.PropertyGridParams(CalcSlowVolFigSVFbdParam, groupBoxParams.Height - 20);
                    var btnPairSVFbd = SlowVolFigSVFbdUI.CalcAndSaveBtns(calculation, null, HandleParameterSaveClick);
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridSVFbd);
                    groupBoxParams.Controls.Add(btnPairSVFbd[0]);
                    groupBoxParams.Controls.Add(btnPairSVFbd[1]);
                    break;

                default:
                    break;
            }
        }

        //re do all calculationsa and populate grid afresh
        private void buttonCalcAll_Click(object sender, EventArgs e)
        {
            //disallow user to view notes
            _curOverviewLoadname = "";
            linkLabelNotes.Enabled = false;

            //this re-introduces discarded shares
            string msg;
            if (_FullRecalcNeeded)
            {
                msg = "A FULL RECALCULATION within All-Tables will be performed since one or more Calculation parameters were changed.\nTakes a while...";
                var dlgResult = MessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dlgResult == DialogResult.Yes)
                {
                    GenerateOverviewAndBindDataGrid();
                }
            }
            else
            {
                msg = "Compile from EXISTING All-Tables?\n(Relatively quick)...";
                var dlgResult = MessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (dlgResult == DialogResult.Yes)
                {
                    GenerateOverviewAndBindDataGrid();
                }
            }
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
                        toolStripCalcs.Text = "Tip: Rt-Click on Column Headers to build a composite filter.";
                        Cursor.Current = Cursors.Default;
                        dgOverview.Focus();
                        //disallow user to view notes
                        _curOverviewLoadname = "";
                        linkLabelNotes.Enabled = false;
                        //set window title
                        this.Text = $"Viewing a quick-saved overview dated {fileInfo.LastWriteTime.ToLocalTime()}.";
                    }
                }
            }


        }

        //this callback gets called when user closes the floating filter form
        private void FilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            rowFilterForm.Hide();
        }

        private void FilterForm_ApplyFilters(object sender, EventArgs e)
        {
            if (sharesOverviewPreFiltering.Count > 0)
            {
                //ensure we always start with the (previously saved) unfiltered overview
                sharesOverview.Clear();
                foreach (var item in sharesOverviewPreFiltering) { sharesOverview.Add(item); }
            }

            //Apply filters to the overviews as per current state in the floating filters form
            //Successive application
            foreach (string filterProp in rowFilters.Keys)
            {
                OverviewFilter filter = rowFilters[filterProp];
                if (filter.Apply)
                {
                    //this will do the filtering of the shares:
                    OverviewFilter.ApplyFilterToList(filter, ref sharesOverview);
                    //mark column to indicate filtering is active
                    if (dgOverview.Columns[filter.ColumnHeader].Name == filter.ColumnHeader) 
                    {
                        dgOverview.Columns[filter.ColumnHeader].HeaderCell.Style.BackColor = Color.Azure;
                    }
                }
                else
                {
                    if (dgOverview.Columns[filter.ColumnHeader].Name == filter.ColumnHeader)
                    {
                        //unmark column to clear it if it was previously marked
                        dgOverview.Columns[filter.ColumnHeader].HeaderCell.Style.BackColor = Control.DefaultBackColor;
                    }
                }
            }
            //rebind grid
            dgViewBindingSource.Clear();
            foreach (Overview overview in sharesOverview)
            {
                dgViewBindingSource.Add(overview);
            }
            dgOverview.DataSource = null;
            dgOverview.DataSource = dgViewBindingSource;
            stripText.Text = $"Filters applied";
        }

        private void OfferFiltering(object sender, DataGridViewCellMouseEventArgs e)
        {
            //grab name of column from overview
            string propName = dgOverview.Columns[e.ColumnIndex].DataPropertyName;
            string colHeader = dgOverview.Columns[e.ColumnIndex].Name;
            if (propName == "ShareName" || propName == "Lazy") return; // only offer filtering on numeric columns

            //pop up filter form and show state of current filters
            if (rowFilterForm == null)
            {
                //make copy of unfiltered overviews so we can restore
                sharesOverviewPreFiltering.Clear();
                foreach (var item in sharesOverview) { sharesOverviewPreFiltering.Add(item); }
                //create the floating filter form
                rowFilterForm = new OverviewRowFilterForm(rowFilters, FilterForm_FormClosing, FilterForm_ApplyFilters);
            }
            rowFilterForm.Show();

            //is this column already in rowfilters?
            if (!rowFilters.Keys.Any(k=>k == propName))
            {
                //nope, so add it
                rowFilters.Add(propName, new OverviewFilter(propName, Comparison.GreaterThan, colHeader));
                rowFilterForm.LoadGrid();
            }
        }


        private void dgOverview_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Filtering
            if (e.Button == MouseButtons.Right)
            {
                if (dgOverview.Rows.Count > 1)
                {
                    OfferFiltering(sender, e);
                }
                return;
            }

            //Sorting
            bool sortHandled = true;
            var col = dgOverview.Columns[e.ColumnIndex];
            var sortDirection = col.HeaderCell.SortGlyphDirection;
            bool GoAscending = (sortDirection == SortOrder.Descending || sortDirection == SortOrder.None);

            switch (col.DataPropertyName)
            {
                case "Lazy":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.Lazy)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.Lazy))  dgViewBindingSource.Add(ov);
                    }
                    break;
                case "ShareName":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.ShareName)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.ShareName)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDayVol":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDayVol)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDayVol)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastPrice":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastPrice)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastPrice)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "DayBeforePrice":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.DayBeforePrice)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.DayBeforePrice)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "PriceFactor":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.PriceFactor)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.PriceFactor)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastPGc":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastPGc)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastPGc)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastPGd":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastPGd)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastPGd)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "BigLastDayPGa":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.BigLastDayPGa)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.BigLastDayPGa)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "BigLastDayPGF":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.BigLastDayPGF)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.BigLastDayPGF)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDHLFPc":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDHLFPc)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDHLFPc)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDHLFPd":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDHLFPd)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDHLFPd)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDLLFPc":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDLLFPc)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDLLFPc)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDLLFPd":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDLLFPd)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDLLFPd)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPGa":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPGa)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPGa)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPGb":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPGb)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPGb)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPGc":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPGc)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPGc)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPtsVola":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPtsVola)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPtsVola)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPtsVolb":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPtsVolb)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPtsVolb)) dgViewBindingSource.Add(ov)  ;
                    }
                    break;
                case "LastDaySumOfPtsVolc":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPtsVolc)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPtsVolc)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPtsVold":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPtsVold)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPtsVold)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPtsHLc":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPtsHLc)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPtsHLc)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPtsHLd":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPtsHLd)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPtsHLd)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPtsVolaa":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPtsVolaa)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPtsVolaa)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "LastDaySumOfPtsVolbb":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.LastDaySumOfPtsVolbb)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.LastDaySumOfPtsVolbb)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "SumOfSumCols64_79":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.SumOfSumCols64_79)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.SumOfSumCols64_79)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "SumOfSumCols64_79trf":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.SumOfSumCols64_79trf)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.SumOfSumCols64_79trf)) dgViewBindingSource.Add(ov);
                    }
                    break;
                case "ShareNumber":
                    dgViewBindingSource.Clear();
                    if (GoAscending)
                    {
                        foreach (Overview ov in sharesOverview.OrderBy(x => x.ShareNumber)) dgViewBindingSource.Add(ov);
                    }
                    else
                    {
                        foreach (Overview ov in sharesOverview.OrderByDescending(x => x.ShareNumber)) dgViewBindingSource.Add(ov);
                    }
                    break;

                default:
                    sortHandled = false;
                    break;
            }
            if (sortHandled)
            {
                col.HeaderCell.SortGlyphDirection = GoAscending ? SortOrder.Ascending : SortOrder.Descending;
                dgOverview.DataSource = dgViewBindingSource;
            }

        }


        private void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Helper.Log("Info","cmd_DataReceived:");
            Helper.Log("Info", e.Data);
        }

        private void cmd_Error(object sender, DataReceivedEventArgs e)
        {
            Helper.Log("Error","cmd_Error");
            Helper.Log("Info", e.Data);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "ShareViewer Overview|*.ovw";
            saveFileDlg.Title = dgOverview.SelectedRows.Count == 0 ? "Save Named Overview" : "Save SELECTED rows as Named overview";
            saveFileDlg.ShowDialog();

            if (saveFileDlg.FileName != "")
            {
                string withNotes = "";
                string selected = "";
                int overviewsCount = 0;
                using (FileStream fs = (FileStream)saveFileDlg.OpenFile())
                {
                    //any 'selected' rows?
                    if (dgOverview.SelectedRows.Count == 0)
                    {
                        //nope
                        foreach (Overview item in sharesOverview)
                        {
                            Helper.SerializeOverviewRecord(fs, item);
                            overviewsCount++;
                        }
                    }
                    else
                    {
                        selected = "(Selected) ";
                        foreach (DataGridViewRow row in dgOverview.SelectedRows)
                        {
                            if (row.Selected)
                            {
                                Helper.SerializeOverviewRecord(fs, (Overview)row.DataBoundItem);
                                overviewsCount++;
                            }
                        }
                    }
                    if (overviewsCount > 0)
                    {
                        //save notes in Alternate Data Stream for .ovw file
                        // see https://stackoverflow.com/questions/13172129/store-metadata-outside-of-file-any-standard-approach-on-modern-windows?noredirect=1&lq=1

                        var rangeFrom = Helper.GetAppUserSettings().AllTableDataStart;
                        var tradingSpan = Helper.GetAppUserSettings().AllTableTradingSpan;
                        var initNotes = $"{selected}Shares Overview from '{rangeFrom}' for {tradingSpan} trading days";
                        var addNotesForm = new SaveWithNotesForm(initNotes);
                        var dlgResult = addNotesForm.ShowDialog();
                        if (dlgResult == DialogResult.Yes)
                        {
                            withNotes = " (with notes)";
                            AlternateData.SetHiddenData(addNotesForm.textBoxNotes.Text, saveFileDlg.FileName, "hidden", cmd_DataReceived, cmd_Error);
                            //allow user to view notes just saved
                            _curOverviewLoadname = saveFileDlg.FileName;
                            linkLabelNotes.Enabled = true;
                        }

                    }
                }
                toolStripCalcs.Text = $"Saved {overviewsCount} overviews to {saveFileDlg.FileName}{withNotes}";
            }

        }

        private void loadNamedOverviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //StreamReader sr = new StreamReader(openFileDialog1.FileName);
                //MessageBox.Show(sr.ReadToEnd());
                //sr.Close();
                FileInfo fileInfo = new FileInfo(openFileDialog1.FileName);                

                Cursor.Current = Cursors.WaitCursor;
                dgViewBindingSource.Clear();

                using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open))
                {
                    //slurp in previously saved file
                    sharesOverview = (List<Overview>)Helper.DeserializeOverview<Overview>(fs);

                    BindDatagridView();

                    stripText.Text = $"{sharesOverview.Count} shares.";
                    Cursor.Current = Cursors.Default;
                    dgOverview.Focus();
                    toolStripOverviewNameLabel.Text = openFileDialog1.FileName;
                    toolStripCalcs.Text = "Tip: Rt-Click on Column Headers to build a composite filter.";
                    //allow user to view notes
                    _curOverviewLoadname = openFileDialog1.FileName;
                    linkLabelNotes.Enabled = true;
                    this.Text = $"Viewing an overview named {fileInfo.Name}, dated {fileInfo.LastWriteTime.ToLocalTime()}.";

                }


            }
        }

        private void dgOverview_SelectionChanged(object sender, EventArgs e)
        {
            linkLabelDeleteSelected.Enabled = (dgOverview.SelectedRows.Count > 0);
        }

        private void linkLabelDeleteSelected_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var numSelected = dgOverview.SelectedRows.Count;
            if (numSelected > 0)
            {
                var dlgResult = MessageBox.Show($"Discard {numSelected} shares?", "Confirm Discard", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dlgResult == DialogResult.Yes)
                {
                    int overviewsCount = 0;
                    foreach (DataGridViewRow row in dgOverview.SelectedRows)
                    {
                        if (row.Selected)
                        {
                            sharesOverview.Remove((Overview)row.DataBoundItem);
                            overviewsCount++;
                        }
                    }
                    if (overviewsCount > 0)
                    {
                        int newCount = 0;
                        dgViewBindingSource.Clear();
                        foreach (Overview overview in sharesOverview)
                        {
                            dgViewBindingSource.Add(overview);
                            newCount++;
                        }
                        dgOverview.DataSource = null;
                        dgOverview.DataSource = dgViewBindingSource;
                        stripText.Text = $"{overviewsCount} shares discarded. {newCount} remain.";
                    }
                }
            }
            Cursor.Current = Cursors.Default;
            dgOverview.Focus();
        }

        private void linkLabelNotes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_curOverviewLoadname != "")
            {
                //open notepad to see notes
                Process.Start("notepad.exe", _curOverviewLoadname + ":hidden.txt");
            }

        }

        private void dgOverview_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Overview ov = (Overview)dgOverview.Rows[e.RowIndex].DataBoundItem;
                var summary = LocalStore.GetAllTableSummaryForShare(ov.ShareNumber);
                var msg = "In the All-Table for this share currently:";
                msg += $"\n\nLast day: {summary.LastDay}";
                MessageBox.Show(msg, $"{ov.ShareName}");
            }
        }

        private void saveShareListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //31 chars for ShareName then ShareNum
            MessageBox.Show("Not yet implemented...");
        }
    }
}
