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

        public OverviewForm()
        {
            InitializeComponent();
        }

        private void OverviewForm_Load(object sender, EventArgs e)
        {
            dgOverview.AutoGenerateColumns = false;
            dgOverview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgOverview.AllowUserToResizeColumns = true;

            InstallOverviewColumns();

            dgViewBindingSource.Clear();
            stripText.Text = "";
            LoadGrid();
        }

        private void LoadGrid()
        {
            var task = Task.Run(() => GenerateOverviewForGrid(displayProgress, sharesOverview));
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                foreach (Overview overview in sharesOverview)
                {
                    dgViewBindingSource.Add(overview);
                }
                dgOverview.DataSource = dgViewBindingSource;
                stripText.Text = $"{sharesOverview.Count} shares.";
            });
        }


        internal void displayProgress(int counter)
        {
            stripText.Text = $"Preparing overview, share {counter} ...";
        }

        private void InstallOverviewColumns()
        {
            dgOverview.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Lazy", DataPropertyName = "Lazy", ReadOnly = true, SortMode= DataGridViewColumnSortMode.Automatic });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Share", DataPropertyName = "ShareName", ReadOnly=true, SortMode = DataGridViewColumnSortMode.Automatic });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ΣVolumes", DataPropertyName = "SumOfVolumes", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastPrice", DataPropertyName = "LastPrice", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PriorPrice", DataPropertyName = "DayBeforePrice", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PriceFactor", DataPropertyName = "PriceFactor", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastPGc", DataPropertyName = "LastPGc", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastPGd", DataPropertyName = "LastPGd", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "BigLastDayPGa", DataPropertyName = "BigLastDayPGa", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "BigLastDayPGF", DataPropertyName = "BigLastDayPGF", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDHLFPc", DataPropertyName = "LastDHLFPc", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDHLFPd", DataPropertyName = "LastDHLFPd", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "#", DataPropertyName = "Unused1", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDLLFPc", DataPropertyName = "LastDLLFPc", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDLLFPd", DataPropertyName = "LastDLLFPd", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPGa", DataPropertyName = "LastDaySumOfPGa", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPGb", DataPropertyName = "LastDaySumOfPGb", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPGc", DataPropertyName = "LastDaySumOfPGc", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPtsVola", DataPropertyName = "LastDaySumOfPtsVola", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPtsVolb", DataPropertyName = "LastDaySumOfPtsVolb", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPtsVolc", DataPropertyName = "LastDaySumOfPtsVolc", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPtsVolLastDay", DataPropertyName = "LastDaySumOfPtsVolLastDay", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPtsHLc", DataPropertyName = "LastDaySumOfPtsHLc", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPtsHLastDay", DataPropertyName = "LastDaySumOfPtsHLastDay", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPtsVolaa", DataPropertyName = "LastDaySumOfPtsVolaa", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "LastDayΣPtsVolbb", DataPropertyName = "LastDaySumOfPtsVolbb", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ΣOfΣCols64_79", DataPropertyName = "SumOfSumCols64_79", ReadOnly = true });
            dgOverview.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ΣOfΣCols64_79trf", DataPropertyName = "SumOfSumCols64_79trf", ReadOnly = true });

            //dgOverview.ColumnHeaderMouseDoubleClick += OnColumnHeaderClicked;

        }

        //private void OnColumnHeaderClicked(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    dgOverview.Sort(dgOverview.Columns[e.ColumnIndex], ListSortDirection.Ascending);
        //}

        private Overview CreateOverviewForShare(Share share, AllTable[] atSegment)
        {
            var lazyShareParams = Helper.GetAppUserSettings().ParamsLazyShare;

            Overview oview = new Overview(share.Name);
            oview.Lazy = Calculations.isLazyLast10Days(atSegment, lazyShareParams);
            //now shrink the segment to just the last 105 rows (last band from penultimate day and full 104 bands of the last day)


            return oview;
        }

        //Fills the sharesOverview list of Overview objects (a form field) 
        internal void GenerateOverviewForGrid(Action<int> progress, List<Overview> sharesOverview)
        {
            var allTablesFolder = Helper.GetAppUserSettings().AllTablesFolder;
            var shareLines = LocalStore.CreateShareArrayFromShareList().Skip(1);
            AllTable[] atSegment = new AllTable[105];

            sharesOverview.Clear();

            int shareCounter = 0;
            foreach (string line in shareLines)
            {
                var share = Helper.CreateShareFromLine(line);
                progress(share.Number);
                var atFilename = allTablesFolder + $@"\alltable_{share.Number}.at";
                //grab last 10 days (needed for Lazy calculation) thereafter only the last 105 rows are needed
                atSegment = Helper.GetAllTableSegment(atFilename, 9362, 1040); // 10297, 105);
                Overview oview = CreateOverviewForShare(share, atSegment);
                sharesOverview.Add(oview);
                if (++shareCounter == 40) break;
            }

        }

    }
}
