using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareViewer
{
    public enum VerticalMode {
        FiveMinly,Hourly,Daily,Weekly
    }

    public partial class SingleAllTableForm : Form
    {
        private string _allTableFilename;
        //private ICollection<AllTable> atRows;
        //private List<AllTable> atRows;
        private AllTable[] atRows;

        private VerticalMode _verticalMode = VerticalMode.FiveMinly;
        private string _shareDescriptor;
        private BindingSource dgViewBindingSource = new BindingSource();

        private bool _loaded = false;
        private bool _loadingCols = false;
        private bool _changingColumns = false;

        // CALCULATIONS

        // we need these to be form-scoped
        internal TextBox calcAuditTextBox;

        //CURRENT properties, gotten from user settings
        internal LazyShareParam CurrLazyShareParam { get => Helper.GetAppUserSettings().ParamsLazyShare; }
        internal SlowPriceParam CurrSlowPriceParam { get => Helper.GetAppUserSettings().ParamsSlowPrice;  }
        internal DirectionAndTurningParam CurrDirectionAndTurningParam { get => Helper.GetAppUserSettings().ParamsDirectionAndTurning; }
        internal FiveMinsGradientFigureParam CurrFiveMinsGradientFigureParam { get => Helper.GetAppUserSettings().ParamsFiveMinsGradientFigure; }

        //CALCULATION properties (we bind these to a property grid)
        private LazyShareParam calcLazyShareParam;
        internal LazyShareParam CalcLazyShareParam { get => calcLazyShareParam; set => calcLazyShareParam = value; }

        private SlowPriceParam calcSlowPriceParam;
        internal SlowPriceParam CalcSlowPriceParam { get => calcSlowPriceParam; set => calcSlowPriceParam = value; }

        private DirectionAndTurningParam calcDirectionAndTurningParam;
        internal DirectionAndTurningParam CalcDirectionAndTurningParam { get => calcDirectionAndTurningParam; set => calcDirectionAndTurningParam = value; }

        private FiveMinsGradientFigureParam calcFiveMinsGradientFigureParam;
        internal FiveMinsGradientFigureParam CalcFiveMinsGradientFigureParam { get => calcFiveMinsGradientFigureParam; set => calcFiveMinsGradientFigureParam = value; }



        //ctor
        public SingleAllTableForm(string allTableFilename, string shareDesc)
        {
            InitializeComponent();
            _allTableFilename = allTableFilename;
            _shareDescriptor = shareDesc;
        }

        //finds the index number of the named column currently in the grid if possible
        //NOTE, assumes the grid columns already present
        private int DetermineColumnIndex(string colName)
        {
            int colIndex = 0;
            bool rowFound = false;
            foreach (var col in dgView.Columns)
            {
                if (col is DataGridViewTextBoxColumn)
                {
                    if (((DataGridViewTextBoxColumn)col).DataPropertyName == colName)
                    {
                        rowFound = true;
                        break;
                    }
                }
                colIndex++;
            }
            if (rowFound)
            {
                return colIndex;
            }
            else
            {
                return -1;
            }
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

        //used before DatagridView has columns
        private void SelectInitialColumnsToView()
        {
            _loadingCols = true;
            listBoxCols.SelectedIndices.Clear();
            foreach (var col in AllTable.InitialViewIndices())
            {
                listBoxCols.SelectedIndex = col;
            }
            _loadingCols = false;
        }

        private void SingleAllTableForm_Load(object sender, EventArgs e)
        {
            var periodStart = Helper.GetAppUserSettings().AllTableDataStart;
            var tradingSpan = Helper.GetAppUserSettings().AllTableTradingSpan;
            if (periodStart == "")
            {
                //this.Text += "Date Range unknown! (this can occur after an App update)";
            }
            else
            {
                this.Text += $"from {periodStart} for {tradingSpan} trading days";
            }

            //put sharename prominent
            labelShareDesc.Text = _shareDescriptor;

            //load up combobox for views from user settings
            LoadViewsComboBox("");

            //load up the all-Table 'columns' listbox
            var columnNames = new List<String>();
            int i = 1;
            foreach (var prop in typeof(AllTable).GetProperties())
            {
                var colName = $"{i.ToString().PadRight(3, ' ')}{prop.Name}";
                columnNames.Add(colName);
                i++;
            }
            listBoxCols.DataSource = columnNames;

            dgView.AutoGenerateColumns = false;
            dgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;  //AllCells;
            BindDatagridView(VerticalMode.FiveMinly);
            SelectInitialColumnsToView();
            InstallDataGridViewColumns();
            listBoxVariables.DataSource = Calculations.CalculationNames;

            dgView.Focus();

            //access calculation parameters (in order just to load them)
            var dontCare1 = CurrLazyShareParam;
            var dontCare2 = CurrSlowPriceParam;

            //show initial take on whether share is currently considered lazy
            var auditLines = new string[] { "" };
            labelLazy.Visible = Calculations.LazyShare(atRows.ToArray(), CurrLazyShareParam, 9362, 10401, out auditLines);

            //mark row 1 with a standout colour
            dgView.Rows[1].DefaultCellStyle.ForeColor = Color.Blue;
            dgView.Rows[1].DefaultCellStyle.BackColor = Color.Azure;

            _loaded = true;
        }

        //Retrieve views from User settings and load the Views Combobox
        private void LoadViewsComboBox(string selectItem)
        {
            comboBoxViews.Items.Clear();
            foreach (string item in Helper.GetAppUserSettings().AllTableViews)
            {
                string viewName = item.Split(',')[0];
                int viewIndex = comboBoxViews.Items.Add(viewName);
                if (viewName == selectItem)
                {
                    comboBoxViews.SelectedIndex = viewIndex;
                }
            }
        }

        //(Re)bind DataGridView to possibly new data in the AllTable array of rows
        private void BindDataGridViewToResults(VerticalMode vertMode)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgViewBindingSource.Clear();
            if (vertMode == VerticalMode.FiveMinly)
            {
                foreach (AllTable item in atRows)
                {
                    dgViewBindingSource.Add(item);
                }
            }
            else if (vertMode == VerticalMode.Hourly)
            {
                foreach (AllTable item in atRows)
                {
                    if (item.TimeFrom.EndsWith("00:00"))
                    {
                        dgViewBindingSource.Add(item);
                    }
                }
            }
            else if (vertMode == VerticalMode.Daily)
            {
                foreach (AllTable item in atRows)
                {
                    if (item.TimeTo.EndsWith("17:39:59"))
                    {
                        dgViewBindingSource.Add(item);
                    }
                }
            }
            else if (vertMode == VerticalMode.Weekly)
            {
                foreach (AllTable item in atRows.Skip(2))
                {
                    if (item.Day == "Fri" && item.TimeTo.EndsWith("17:39:59"))
                    {
                        dgViewBindingSource.Add(item);
                    }
                }
            }
            //set the datasource
            _verticalMode = vertMode;
            dgView.DataSource = null;
            dgView.DataSource = dgViewBindingSource;

            Cursor.Current = Cursors.Default;
            dgView.Focus();

        }

        private void BindDatagridView(VerticalMode vertMode)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgViewBindingSource.Clear();

            using (FileStream fs = new FileStream(_allTableFilename, FileMode.Open))
            {
                //slurp in the previously saved file of AllTable records
                atRows = Helper.DeserializeList<AllTable>(fs).ToArray();
                //copy row 10401 (if possible) to row 1 as per Gunther
                Helper.CopySourceToTargetAllTableRow(atRows, 10401, 1);

                atRows[1].Row = 1;

                if (vertMode == VerticalMode.FiveMinly)
                {
                    foreach (AllTable item in atRows)
                    {
                        dgViewBindingSource.Add(item);
                    }
                }
                else if (vertMode == VerticalMode.Hourly)
                {
                    foreach (AllTable item in atRows)
                    {
                        if (item.TimeFrom.EndsWith("00:00"))
                        {
                            dgViewBindingSource.Add(item);
                        }
                    }
                }
                else if (vertMode == VerticalMode.Daily)
                {
                    foreach (AllTable item in atRows)
                    {
                        if (item.TimeTo.EndsWith("17:39:59"))
                        {
                            dgViewBindingSource.Add(item);
                        }
                    }
                }
                else if (vertMode == VerticalMode.Weekly)
                {
                    foreach (AllTable item in atRows.Skip(2))
                    {
                        if (item.Day == "Fri" && item.TimeTo.EndsWith("17:39:59"))
                        {
                            dgViewBindingSource.Add(item);
                        }
                    }
                }
            }

            //set the datasource
            _verticalMode = vertMode;
            dgView.DataSource = null;
            dgView.DataSource = dgViewBindingSource;

            Cursor.Current = Cursors.Default;
            dgView.Focus();

        }

        //fired when user selects/deselects an item in the lh listbox (listBoxCols)
        //whereby he wants to add or remove the corresponding datagridview column
        private void listBoxCols_SelectedIndexChanged(object sender, EventArgs e)
        {
            //don't react while shift key is being depressed
            if ((Control.ModifierKeys & Keys.Shift) != 0) return;

            if (_loaded && !_changingColumns) InstallDataGridViewColumns();
        }

        //fired when user changes textbox at the top of the 'Row' column
        //see ColumnWithNumericHeader being added in InstallDataGridViewColumns below
        private void onRowWanted(object sender, EventArgs e)
        {
            var rowNumTextBox = (TextBox)sender;

            stripText.Text = "";
            if (determineVerticalMode() != VerticalMode.FiveMinly)
            {
                stripText.Text = "Row-Find works only for 5-minute bands";
                rowNumTextBox.Text = "";
                return;
            }

            short rowNum = 0;
            if (short.TryParse(rowNumTextBox.Text, out rowNum)) {
                //Helper.Log("Debug", $"{dgViewBindingSource.Position}");
                if (rowNum < dgView.RowCount)
                {
                    try
                    {
                        dgViewBindingSource.Position = rowNum;
                        dgView.FirstDisplayedScrollingRowIndex = rowNum;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                    }
                }
            }
           
        }

        private void InstallDataGridViewColumns()
        {
            dgView.Columns.Clear();
            foreach (string item in listBoxCols.SelectedItems)
            {
                var colName = item.Substring(3);
                if (colName == "Row")
                {
                    dgView.Columns.Add(
                        new ColumnWithNumericHeader(onRowWanted)
                        {
                            Name = "Row",
                            DataPropertyName = colName, 
                            ToolTipText = AllTable.PropNameToHint(colName),
                        });
                }
                else
                {
                    //column name includes a column number prefix, so strip it for the DataPropertyName
                    dgView.Columns.Add(
                        new DataGridViewTextBoxColumn()
                        {
                            Name = item,
                            DataPropertyName = colName, // item.Substring(3),
                            ToolTipText = AllTable.PropNameToHint(colName)  //item.Substring(3))
                        });
                    dgView.Columns[item].DefaultCellStyle.Format = AllTable.NameToFormat(colName);
                }
            }
        }

        //user wants to fill the All-Table with trading data
        private void OnCalcAllNew(object sender, EventArgs e)
        {
            //sweep thru all Day-Data files. Dates are taken from the DataGrid

        }

        private void buttonInitialView_Click(object sender, EventArgs e)
        {
            SelectInitialColumnsToView();
            //HighightMondayRows();
        }

        //private void HighightMondayRows()
        //{
        //    if (verticalMode != VerticalMode.FiveMinly) return;

        //    int indexOfRow = DetermineColumnIndex("Row");
        //    if (indexOfRow == -1) return;

        //    foreach (DataGridViewRow row in dgView.Rows)
        //    {
        //        int rowNumCellValue = Convert.ToInt16(row.Cells[indexOfRow].Value);
        //        if (((rowNumCellValue - 2) % 104) == 0)
        //        {
        //            row.HeaderCell.Style.BackColor = Color.Wheat;
        //        }
        //    }
        //}


        private void radio5mins_CheckedChanged(object sender, EventArgs e)
        {
            //revert the databinding to initial full set of rows
            BindDatagridView(VerticalMode.FiveMinly);
        }

        private void radioHours_CheckedChanged(object sender, EventArgs e)
        {
            BindDatagridView(VerticalMode.Hourly);
        }

        private void radioDays_CheckedChanged(object sender, EventArgs e)
        {
            BindDatagridView(VerticalMode.Daily);
        }

        private void radioWeekly_CheckedChanged(object sender, EventArgs e)
        {
            BindDatagridView(VerticalMode.Weekly);
        }

        private VerticalMode determineVerticalMode()
        {
            if (radioHours.Checked)
            {
                return VerticalMode.Hourly;
            }
            else if (radioDays.Checked)
            {
                return VerticalMode.Daily;
            }
            else if (radioWeekly.Checked) {
                return VerticalMode.Weekly;
            }
            else
            {
                return VerticalMode.FiveMinly;
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
            if (viewName.StartsWith("*** Choose a Calculation ***".Substring(0,3))) return;

            _changingColumns = true;
            if (viewName == "Initial")
            {
                SelectInitialColumnsToView(); // selects listBocCols items, not comboBoxViews items
            }
            else
            {
                //get definition from User settings - its name is the first value in the csv list
                //var viewDefinition = Helper.GetAppUserSettings().AllTableViews.Find(v => v.StartsWith(viewName));
                string viewDefinition = null;
                foreach (var item in Helper.GetAppUserSettings().AllTableViews)
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
                        "View not Found",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            _changingColumns = false;
            //get the right columns into the grid
            InstallDataGridViewColumns();
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
                foreach (var item in aus.AllTableViews)
                {
                    if (item.StartsWith(viewName)) {
                        aus.AllTableViews.Remove(item);
                        break;
                    }
                }
                aus.AllTableViews.Add(viewStr);
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
                    SaveAllTable();
                    break;
                case "Make Five minutes Price Gradients PG":
                    SaveAllTable();
                    break;
                case "Find direction and Turning":
                    aus.ParamsDirectionAndTurning = CalcDirectionAndTurningParam;
                    aus.Save();
                    SaveAllTable();
                    break;
                case "Find Five minutes Gradients Figure PGF":
                    aus.ParamsFiveMinsGradientFigure = CalcFiveMinsGradientFigureParam;
                    aus.Save();
                    SaveAllTable();
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

        private void SaveAllTable()
        {
            using (FileStream fs = new FileStream(_allTableFilename, FileMode.Create))
            {
                foreach (AllTable item in atRows)
                {
                    Helper.SerializeAllTableRecord(fs, item);
                }
            }
            stripText.Text = $"Saved {_allTableFilename}";
        }

        internal void HandleCalculationClick(object sender, EventArgs e)
        {
            string calculation = (string)((Button)sender).Tag;
            var auditLines = new string[] { "" };
            Cursor.Current = Cursors.WaitCursor;
            switch (calculation)
            {
                case "Identify Lazy Shares":
                    Calculations.LazyShare(atRows, CalcLazyShareParam, 9362, 10401,  out auditLines);
                    calcAuditTextBox.Lines = auditLines;
                    break;
                case "Make Slow (Five minutes) Prices SP":
                    Calculations.MakeSlowPrices(ref atRows, CalcSlowPriceParam, 1, 10401, out auditLines);
                    calcAuditTextBox.Lines = auditLines;
                    // atRows must be re-bound to the DataGridView
                    BindDataGridViewToResults(determineVerticalMode());
                    break;
                case "Make Five minutes Price Gradients PG":
                    Calculations.MakeFiveMinutesPriceGradients(ref atRows, 1, 10401, out auditLines);
                    calcAuditTextBox.Lines = auditLines;
                    // atRows must be re-bound to the DataGridView
                    BindDataGridViewToResults(determineVerticalMode());
                    break;
                case "Find direction and Turning":
                    Calculations.FindDirectionAndTurning(ref atRows, CalcDirectionAndTurningParam, 10298, 10401, out auditLines);
                    calcAuditTextBox.Lines = auditLines;
                    // atRows must be re-bound to the DataGridView
                    BindDataGridViewToResults(determineVerticalMode());
                    break;
                case "Find Five minutes Gradients Figure PGF":
                    Calculations.FindFiveMinsGradientsFigurePGF(ref atRows, CalcFiveMinsGradientFigureParam, 10298, 10401, out auditLines);
                    calcAuditTextBox.Lines = auditLines;
                    // atRows must be re-bound to the DataGridView
                    BindDataGridViewToResults(determineVerticalMode());
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
            Cursor.Current = Cursors.Default;
        }

        private TextBox AuditTextBox(string[] auditOutcome)
        {
            var auditBox = new TextBox();
            auditBox.Location = new Point(270, 12);
            auditBox.Size = new Size(400, groupBoxParams.Height - 20);
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

        //User has chosen a Calculation. Show Parameters apprpriate for the calculation
        //and invoke a View by the same name as that of the Calculation
        private void listBoxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var affirmNumRows = $"There are {atRows.Count()} records in this All-Table";
            stripText.Text = affirmNumRows;

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

            MediateCalculation(calculation);

        }

        private void MediateCalculation(string calculation)
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
                    //move to row 9362 (10 days from end of range)
                    dgViewBindingSource.Position = 9362;
                    dgView.FirstDisplayedScrollingRowIndex = 9362;
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
                    //move to row 9362 (10 days from end of range)
                    //dgViewBindingSource.Position = 9362;
                    //dgView.FirstDisplayedScrollingRowIndex = 9362;
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
                    CalcDirectionAndTurningParam = new DirectionAndTurningParam(CurrDirectionAndTurningParam.From,CurrDirectionAndTurningParam.To, CurrDirectionAndTurningParam.PGcThreshold, CurrDirectionAndTurningParam.Z);
                    var propGridDandT = DirectionAndTurningUI.PropertyGridParams(CalcDirectionAndTurningParam, groupBoxParams.Height - 20);
                    var btnPairDandT = DirectionAndTurningUI.CalcAndSaveBtns(calculation, HandleCalculationClick, HandleParameterSaveClick);
                    calcAuditTextBox = AuditTextBox(new string[] { "Adjust setting then press 'Calculate' to (re)evaluate" });
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridDandT);
                    groupBoxParams.Controls.Add(btnPairDandT[0]);
                    groupBoxParams.Controls.Add(btnPairDandT[1]);
                    groupBoxParams.Controls.Add(calcAuditTextBox);
                    //move to row 10298 (09:00:00 of last day)
                    dgViewBindingSource.Position = 10298;
                    dgView.FirstDisplayedScrollingRowIndex = 10298;
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
                        CurrFiveMinsGradientFigureParam.Y);
                    var propGridFiveMinsPGF = FiveMinsGradientFigureUI.PropertyGridParams(CalcFiveMinsGradientFigureParam, groupBoxParams.Height - 20);
                    var btnPairFiveMinsPGF = FiveMinsGradientFigureUI.CalcAndSaveBtns(calculation, HandleCalculationClick, HandleParameterSaveClick);
                    calcAuditTextBox = AuditTextBox(new string[] { "Adjust setting then press 'Calculate' to (re)evaluate" });
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridFiveMinsPGF);
                    groupBoxParams.Controls.Add(btnPairFiveMinsPGF[0]);
                    groupBoxParams.Controls.Add(btnPairFiveMinsPGF[1]);
                    groupBoxParams.Controls.Add(calcAuditTextBox);
                    //move to row 10298 (09:00:00 of last day)
                    dgViewBindingSource.Position = 10298;
                    dgView.FirstDisplayedScrollingRowIndex = 10298;
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
