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
        private VerticalMode verticalMode = VerticalMode.FiveMinly;

        public SingleAllTableForm(string allTableFilename)
        {
            InitializeComponent();
            _allTableFilename = allTableFilename;
        }

        private void AddInitialColumnsToView()
        {

            var initCols = new List<String>() { "Row", "Date", "Day", "TimeFrom", "TimeTo", "F", "FP", "FV" };
            var initIndices = new List<int>() { 0, 2, 4, 6, 7, 8, 10, 48 };

            listBoxCols.SelectedIndices.Clear();
            foreach (var col in initIndices)
            {
                listBoxCols.SelectedIndex = col;
            }

        }

        private void SingleAllTableForm_Load(object sender, EventArgs e)
        {
            var periodStart = Helper.GetAppUserSettings().AllTableDataStart;
            var tradingSpan = Helper.GetAppUserSettings().AllTableTradingSpan;
            if (periodStart == "")
            {
                this.Text += "Date Range unknown - Please close form & re-generate!";
                buttonInitialView.Enabled = false;
            }
            else
            {
                this.Text += $"from {periodStart} for {tradingSpan} trading days";
                buttonInitialView.Enabled = true;
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

            AddInitialColumnsToView();
            HighightMondayRows();

            dgView.Focus();
        }

        private void BindDatagridView(VerticalMode vertMode)
        {
            ICollection<AllTable> atRows;
            var bindingSource1 = new BindingSource();
            using (FileStream fs = new FileStream(_allTableFilename, FileMode.Open))
            {
                atRows = Helper.DeserializeList<AllTable>(fs);
                if (vertMode == VerticalMode.FiveMinly)
                {
                    foreach (AllTable item in atRows)
                    {
                        bindingSource1.Add(item);
                    }
                }
                else if (vertMode == VerticalMode.Hourly)
                {
                    foreach (AllTable item in atRows)
                    {
                        if (item.TimeFrom.EndsWith("00:00"))
                        {
                            bindingSource1.Add(item);
                        }
                    }
                }
                else if (vertMode == VerticalMode.Daily)
                {
                    foreach (AllTable item in atRows)
                    {
                        if (item.TimeTo.EndsWith("17:39:59"))
                        {
                            bindingSource1.Add(item);
                        }
                    }
                }
                else if (vertMode == VerticalMode.Weekly)
                {
                    foreach (AllTable item in atRows.Skip(2))
                    {
                        if (item.Day == "Fri" && item.TimeTo.EndsWith("17:39:59"))
                        {
                            bindingSource1.Add(item);
                        }
                    }
                }
            }

            verticalMode = vertMode;
            dgView.DataSource = bindingSource1;
            dgView.Focus();

        }

        private void listBoxCols_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgView.Columns.Clear();
            foreach (string item in listBoxCols.SelectedItems)
            {
                //column name includes a column number prefix, so strip it for the DataPropertyName
                dgView.Columns.Add(new DataGridViewTextBoxColumn() { Name = item, DataPropertyName = item.Substring(3) });
            }
        }

        //user wants to fill the All-Table with trading data
        private void OnCalcAllNew(object sender, EventArgs e)
        {
            //sweep thru all Day-Data files. Dates are taken from the DataGrid

        }

        private void buttonInitialView_Click(object sender, EventArgs e)
        {
            AddInitialColumnsToView();
            HighightMondayRows();
        }

        //finds the index number of the named column currently in the grid if possible
        private int DetermineColumn(string colName)
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

        private void HighightMondayRows()
        {
            if (verticalMode != VerticalMode.FiveMinly) return;

            int indexOfRow = DetermineColumn("Row");
            if (indexOfRow == -1) return;

            foreach (DataGridViewRow row in dgView.Rows)
            {
                int rowNumCellValue = Convert.ToInt16(row.Cells[indexOfRow].Value);
                if (((rowNumCellValue - 2) % 104) == 0)
                {
                    row.HeaderCell.Style.BackColor = Color.Wheat;
                }
            }
        }


        private void buttonSaveView_Click(object sender, EventArgs e)
        {
            //save currently selected colums to Usersettings under a name

        }

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
    }
}
