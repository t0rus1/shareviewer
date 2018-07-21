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

        //Calculations
        Param lazyShareParams;

        internal Param LazyShareParams { get => lazyShareParams; set => lazyShareParams = value; }

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
            foreach (string item in Helper.GetAppUserSettings().AllTableViews)
            {
                string viewName = item.Split(',')[0];
                comboBoxViews.Items.Add(viewName);
            }

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

            dgView.Focus();

            _loaded = true;
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

        private void comboBoxViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changingColumns = true;
            string viewName = ((ComboBox)sender).Text;
            if ( viewName == "Initial")
            {
                SelectInitialColumnsToView();
            }
            else
            {
                //get definition from User settings - its name is the first value in the csv list
                var viewDefinition = Helper.GetAppUserSettings().AllTableViews.Find(v => v.StartsWith(viewName));
                SelectColumnsToView(viewDefinition);
            }
            _changingColumns = false;
            InstallDataGridViewColumns();
        }

        private void linkLabelSaveView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new InputBox("Save column arrangement as a view", "Name this view");
            var dlgResult = form.ShowDialog();
            var viewName = form.returnValue;
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
            }

        }

        private void linkLabelLock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabelLock.Text = linkLabelLock.Text == "lock view" ? "unlock view" : "lock view";
            listBoxCols.Enabled = !listBoxCols.Enabled;
        }

        // CALCULATION HANDLING
        private void listBoxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxParams.Controls.Clear();

            groupBoxParams.Text = ((ListBox)sender).Text;
            string calculation = groupBoxParams.Text.ToLower();

            var ds = dgView.DataSource;

            switch (calculation)
            {
                case "delete lazy shares":

                    //move to row 9362 (10 days from end of range)
                    dgViewBindingSource.Position = 9362;
                    dgView.FirstDisplayedScrollingRowIndex = 9362;
                    //show params property grid
                    LazyShareParams = new Param(1000, 1000000, 50000);
                    groupBoxParams.Controls.Add(PreparePropertyGridForParams(LazyShareParams));
                    //populate calculation and results
                    var isLazy = Calculations.LazyShare(atRows.ToArray(), 9362, 10401, LazyShareParams.Setting);
                    break;

                default:
                    break;
            }

        }

        private PropertyGrid PreparePropertyGridForParams(Param param)
        {
            var pg = new PropertyGrid();
            pg.Size = new Size(300, groupBoxParams.Height - 20);
            pg.Location = new Point(5, 12);
            pg.SelectedObject = param;
            return pg;
        }
    }
}
