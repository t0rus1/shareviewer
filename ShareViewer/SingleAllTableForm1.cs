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
            var periodEnd = Helper.GetAppUserSettings().AllTableDataEnd;
            if (periodStart == "" || periodEnd == "")
            {
                labelCurrentDateRange.Text = "Date Range unknown - Please close this form and re-generate new All-Tables";
                labelCurrentDateRange.ForeColor = Color.IndianRed;
                buttonInitialView.Enabled = false;
            }
            else
            {
                labelCurrentDateRange.ForeColor = Color.Green;
                labelCurrentDateRange.Text = $"This 99-day All-table data range is from: {periodStart} to {periodEnd}";
                buttonInitialView.Enabled = true;
            }


            //load up the all-Table 'columns' listbox
            var columnNames = new List<String>();
            int i = 1;
            foreach (var prop in typeof(AllTable).GetProperties())
            {
                var colName = $"{i.ToString().PadRight(3,' ')}{prop.Name}";
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
        }
    }


}
