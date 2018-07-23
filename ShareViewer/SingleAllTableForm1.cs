using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private ICollection<AllTable> atRows;

        private VerticalMode _verticalMode = VerticalMode.FiveMinly;
        private string _shareDescriptor;
        private BindingSource dgViewBindingSource = new BindingSource();

        private bool _loaded = false;
        private bool _loadingCols = false;
        private bool _changingColumns = false;

        // CALCULATION RESULTS
        // we need these to be form-scoped
        internal TextBox calcAuditTextBox;
        internal Param calcLazyShareParams;

        //CALCULATIONS properties (so that we can bind to a Property grid)
        //get our initial calculation parameters from user settings
        private Param currentLazyShareParams = Helper.GetAppUserSettings().ParamsLazyShare;
        internal Param CurrentLazyShareParams { get => currentLazyShareParams; set => currentLazyShareParams = value; }


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
                this.Text += "Date Range unknown - Please close form & re-generate!";
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
            BindDatagridView(VerticalMode.FiveMinly);
            SelectInitialColumnsToView();
            InstallDataGridViewColumns();
            listBoxVariables.DataSource = Calculations.CalculationNames;

            dgView.Focus();

            //show initial take on whether share is currently considered lazy
            var auditLines = new string[] { "" };
            labelLazy.Visible = Calculations.LazyShare(atRows.ToArray(), 9362, 10401, CurrentLazyShareParams.Setting, out auditLines);

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

        private void BindDatagridView(VerticalMode vertMode)
        {
            //var bindingSource1 = new BindingSource();
            using (FileStream fs = new FileStream(_allTableFilename, FileMode.Open))
            {
                atRows = Helper.DeserializeList<AllTable>(fs);
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
            dgView.DataSource = dgViewBindingSource;

            dgView.Focus();

        }

        //fired when user selects/deselects an item in the lh listbox (listBoxCols)
        //whereby he wants to add or remove the corresponding datagridview column
        private void listBoxCols_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loaded && !_changingColumns) InstallDataGridViewColumns();
        }

        //fired when user changes textbox at the top of the 'Row' column
        //see ColumnWithNumericHeader being added in InstallDataGridViewColumns below
        private void onRowWanted(object sender, EventArgs e)
        {
            short rowNum = 0;
            if (short.TryParse(((TextBox)sender).Text, out rowNum)) {
                //Helper.Log("Debug", $"{dgViewBindingSource.Position}");
                dgViewBindingSource.Position = rowNum;
                dgView.FirstDisplayedScrollingRowIndex = rowNum;
            }
           
        }

        private void InstallDataGridViewColumns()
        {
            dgView.Columns.Clear();
            foreach (string item in listBoxCols.SelectedItems)
            {
                if (item.Substring(3) == "Row")
                {
                    dgView.Columns.Add(
                        new ColumnWithNumericHeader(onRowWanted)
                        {
                            Name = "Row", //item,
                            DataPropertyName = item.Substring(3),
                            ToolTipText = AllTable.PropNameToHint(item.Substring(3))
                        });
                }
                else
                {
                    //column name includes a column number prefix, so strip it for the DataPropertyName
                    dgView.Columns.Add(
                        new DataGridViewTextBoxColumn()
                        {
                            Name = item,
                            DataPropertyName = item.Substring(3),
                            ToolTipText = AllTable.PropNameToHint(item.Substring(3))
                        });
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
            var dlgResult = form.ShowDialog();
            var viewName = form.returnValue.Replace(",", ""); // no commas allowed in view name!
            if (dlgResult == DialogResult.OK)
            {
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

        private void HandleCalculationClick(object sender, EventArgs e)
        {
            string calculation = (string)((Button)sender).Tag;
            var auditLines = new string[] { "" };
            switch (calculation)
            {
                case "Identify Lazy Shares":
                    Calculations.LazyShare(atRows.ToArray(), 9362, 10401, calcLazyShareParams.Setting, out auditLines);
                    calcAuditTextBox.Lines = auditLines;
                    break;
                case "Make Slow (Five minutes Prices) SP":
                    break;
                case "Make Five minutes Price Gradients":
                    break;
                case "Find direction and Turning":
                    break;
                case "Find Five minutes Gradients Figure PGF":
                    break;
                case "Related volume Figure (RPGFV) of biggest PGF)":
                    break;
                case "Make High Line HL":
                    break;
                case "Make Low Line":
                    break;
                case "Make Slow Volumes":
                    break;
                case "Slow Volume Figure SVFac":
                    break;
                case "Slow Volume Figure SVFbd":
                    break;
                default:
                    break;
            }
        }

        // User saves newly edited parameters
        // TODO: The Sharelist.txt file which hold names of Shares needs to be expanded
        // such that newly calculated share properties (e.g. share is Lazy) can be attached.
        // Perhaps instead of being a plain text file it can be a serialized list of Share objects
        private void HandleParameterSaveClick(object sender, EventArgs e)
        {
            var aus = Helper.GetAppUserSettings();
            aus.ParamsLazyShare = calcLazyShareParams;
            aus.Save();
            stripText.Text = "Parameter saved";
        }

        // CALCULATION HANDLING
        private PropertyGrid BuildPropertyGridParams(Param param)
        {
            var pg = new PropertyGrid();
            pg.Size = new Size(150, groupBoxParams.Height - 20);
            pg.Location = new Point(20, 12);
            pg.SelectedObject = param;
            pg.PropertyValueChanged += OnParamSettingChange;
            return pg;
        }

        // Check that new param setting remains within allowed bounds
        private void OnParamSettingChange(object sender, EventArgs e)
        {
            var pg = (PropertyGrid)sender;
            var param = ((Param)pg.SelectedObject);
            var newVal = param.Setting;
            var lowerLimit = param.From;
            var upperLimit = param.To;
            stripText.Text = "";
            if (newVal < lowerLimit)
            {
                param.Setting = lowerLimit;
                stripText.Text = $"No lower than {lowerLimit} !!!";
            }
            else if (newVal > upperLimit)
            {
                param.Setting = upperLimit;
                stripText.Text = $"No higher than {upperLimit} !!!";
            }
        }

        private Button[] BuildCalculateAndSaveButtons(string calculation)
        {
            var buttons = new Button[2];

            var btnCalc = new Button();
            btnCalc.Size = new Size(60, 100);
            btnCalc.Location = new Point(170 + 20, 16);
            btnCalc.Text = "Calculate";
            btnCalc.Tag = calculation;
            btnCalc.Click += HandleCalculationClick;
            buttons[0] = btnCalc;

            var btnSave = new Button();
            btnSave.Size = new Size(60, 50);
            btnSave.Location = new Point(170 + 20, 120);
            btnSave.Text = "Apply and Save";
            btnSave.Tag = calculation;
            btnSave.Click += HandleParameterSaveClick;
            buttons[1] = btnSave;

            return buttons;
        }

        private TextBox BuildAuditTextBox(string[] auditOutcome)
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
            string calculation = ((ListBox)sender).Text;
            groupBoxParams.Controls.Clear();
            groupBoxParams.Text = calculation;

            //NB: the string comparands in the switch statement below must allign with 
            //    Calculations.CalculationNames in Calculations.cs

            //SetView(calculation);
            //Invoke view by same name
            int wantedViewIndex = comboBoxViews.Items.IndexOf(calculation);
            if (wantedViewIndex != -1)
            {
                comboBoxViews.SelectedIndex = wantedViewIndex;
            }

            switch (calculation)
            {
                case "Identify Lazy Shares":
                    //show a bound params property grid
                    calcLazyShareParams = new Param(
                        currentLazyShareParams.From, currentLazyShareParams.To, currentLazyShareParams.Setting);
                    var propGridParams = BuildPropertyGridParams(calcLazyShareParams);
                    var btnPair = BuildCalculateAndSaveButtons(calculation);
                    calcAuditTextBox = BuildAuditTextBox(new string[] { "Adjust setting Z then press 'Calculate' to re-evaluate" } );
                    //add params property grid and calc button to groupBox panel
                    groupBoxParams.Controls.Add(propGridParams);
                    groupBoxParams.Controls.Add(btnPair[0]);
                    groupBoxParams.Controls.Add(btnPair[1]);
                    groupBoxParams.Controls.Add(calcAuditTextBox);
                    //move to row 9362 (10 days from end of range)
                    dgViewBindingSource.Position = 9362;
                    dgView.FirstDisplayedScrollingRowIndex = 9362;
                    break;

                case "Make Slow (Five minutes) Prices SP":
                    break;
                case "Make Five minutes Price Gradients":
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
