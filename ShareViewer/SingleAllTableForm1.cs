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
    public partial class SingleAllTableForm : Form
    {
        private string _allTableFilename;


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

            //bind datagrid view
            ICollection<AllTable> atRows;
            dgView.AutoGenerateColumns = false;
            var bindingSource1 = new BindingSource();
            using (FileStream fs = new FileStream(_allTableFilename, FileMode.Open))
            {
                atRows = Helper.DeserializeList<AllTable>(fs);
                foreach (AllTable item in atRows)
                {
                    bindingSource1.Add(item);
                }
            }
            dgView.DataSource = bindingSource1;
            AddInitialColumnsToView();
            HighightMondayRows();

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

        private void HighightMondayRows()
        {
            //determine which column the 'Row' is in
            int colIndex = 0;
            bool rowFound = false;
            foreach (var col in dgView.Columns)
            {
                if (col is DataGridViewTextBoxColumn)
                {
                    if (((DataGridViewTextBoxColumn)col).DataPropertyName == "Row")
                    {
                        rowFound = true;
                        break;
                    }
                }
                colIndex++;
            }
            if (!rowFound) return;

            foreach (DataGridViewRow row in dgView.Rows)
            {
                int rowNumCellValue = Convert.ToInt16(row.Cells[colIndex].Value);
                if (((rowNumCellValue - 2) % 104) == 0)
                {
                    row.HeaderCell.Style.BackColor = Color.Wheat;
                }
            }
        }

        private void buttonNextDay_Click(object sender, EventArgs e)
        {
            //scroll gridview by 104 rows            
            try
            {
                dgView.FirstDisplayedScrollingRowIndex += 104;
            }
            catch (Exception)
            {
            }
        }

        private void buttonPrevDay_Click(object sender, EventArgs e)
        {
            //scroll gridview by 104 rows            
            try
            {
                dgView.FirstDisplayedScrollingRowIndex -= 104;
            }
            catch (Exception)
            {
            }
        }

        private void buttonSaveView_Click(object sender, EventArgs e)
        {
            //save currently selected colums to Usersettings under a name

        }
    }
 }
