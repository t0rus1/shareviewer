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
            var initCols = new List<String>() { "Row", "Date", "Day", "TimeFrom", "TimeTo", "F", "FV" };
            var initIndices = new List<int>() { 0, 2, 4, 6, 7, 8, 48 };

            //dgView.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Row", DataPropertyName = "Row" });
            foreach (var col in initIndices)
            {
                listBoxCols.SelectedIndex = col;
            }

        }

        private void SingleAllTableForm_Load(object sender, EventArgs e)
        {
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
    }


}
