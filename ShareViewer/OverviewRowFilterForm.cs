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
    public partial class OverviewRowFilterForm : Form
    {
        Dictionary<string, OverviewFilter> _filters;
        BindingSource dgViewBindingSource = new BindingSource();
        Action<object, FormClosingEventArgs> _onClosing;
        Action<object, EventArgs> _onApply;
        bool _loading = true;

        public OverviewRowFilterForm(Dictionary<string,OverviewFilter> filters, 
                Action<object, FormClosingEventArgs> onClosing, Action<object,EventArgs> onApply)
        {
            InitializeComponent();
            _onClosing = onClosing;
            _onApply = onApply;
            _filters = filters;
        }

        private void OverviewRowFilterForm_Load(object sender, EventArgs e)
        {
            dgViewFilters.AutoGenerateColumns = false;
            dgViewFilters.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            InstallGridViewColumns();
            dgViewFilters.RowHeadersWidth = 10;
            //LoadGrid();
            _loading = false;
        }



        private void InstallGridViewColumns()
        {
            dgViewFilters.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Variable",
                DataPropertyName = "PropName",
                ReadOnly = true,
            });
            dgViewFilters.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Compare",
                DataPropertyName = "CompareKind",
                ValueType = typeof(Comparison),
                ReadOnly = true
            });
            dgViewFilters.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Value",
                DataPropertyName = "Comparand",
            });
            //checkbox column
            dgViewFilters.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "Apply",
                DataPropertyName = "Apply",
            });
        }

        public void LoadGrid()
        {
            dgViewFilters.DataSource = null;
            dgViewBindingSource.Clear();
            foreach (OverviewFilter filter in _filters.Values)
            {
                dgViewBindingSource.Add(filter);
            }
            dgViewFilters.DataSource = dgViewBindingSource;
        }

        private void OverviewRowFilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            _onClosing(sender, e);
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            ((Button)sender).Tag = checkBoxLazies.Checked ? "ExcludeLazies" : "";
            _onApply(sender, e);
        }

        private void dgViewFilters_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgViewFilters.Columns[e.ColumnIndex].DataPropertyName == "CompareKind")
            {
                //toggle the comparison kind
                OverviewFilter ovFilter = ((OverviewFilter)dgViewFilters.Rows[e.RowIndex].DataBoundItem);
                if (ovFilter.CompareKind == Comparison.GreaterThan)
                {
                    ovFilter.CompareKind = Comparison.LessThan;
                }
                else
                {
                    ovFilter.CompareKind = Comparison.GreaterThan;
                }
            }

        }

        private void dgViewFilters_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            stripText.Text = e.Exception.Message;
            e.Cancel = true;
        }

        private void dgViewFilters_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //stripText.Text = "";
        }

        private void linkLabelSave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_filters.Count > 0)
            {
                var saveFileDlg = new SaveFileDialog();
                saveFileDlg.Filter = "Saved Overview Row Filters|*.orf";
                saveFileDlg.Title = "Save Named Filter";
                saveFileDlg.ShowDialog();

                if (saveFileDlg.FileName != "")
                {
                    using (FileStream fs = (FileStream)saveFileDlg.OpenFile())
                    {
                        foreach (var item in _filters.Values)
                        {
                            Helper.SerializeFilterRecord(fs, item);
                        }
                        stripText.Text = $"Filter saved";
                    }
                }

            }
        }


        private void linkLabelLoad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog1.FileName);

                _filters.Clear();
                using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open))
                {
                    var ourFilters = (List<OverviewFilter>)Helper.DeserializeFilters<OverviewFilter>(fs);
                    foreach (OverviewFilter item in ourFilters)
                    {
                        _filters.Add(item.PropName, item);
                    }
                    stripText.Text = $"{fileInfo.Name} loaded";
                }

                LoadGrid();
            }

        }

        private void stripText_Click(object sender, EventArgs e)
        {

        }
    }
}
