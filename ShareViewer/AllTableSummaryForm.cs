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
    public partial class AllTableSummaryForm : Form
    {
        BindingSource dgViewBindingSource = new BindingSource();
        List<AllTableSummary> allSummaries = new List<AllTableSummary>();

        public AllTableSummaryForm()
        {
            InitializeComponent();
        }

        private void AllTableSummaryForm_Load(object sender, EventArgs e)
        {
            // generate summary data and bind to form
            dgViewSummary.AutoGenerateColumns = false;
            dgViewSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;  // ColumnHeader;  //AllCells;

            InstallGridViewColumns();

            dgViewBindingSource.Clear();

            stripText.Text = "";
            var task = Task.Run(() => GenerateAllTableSummaryForGrid(displayProgress, allSummaries));
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                foreach (AllTableSummary summary in allSummaries)
                {
                    dgViewBindingSource.Add(summary);
                }
                dgViewSummary.DataSource = dgViewBindingSource;
                stripText.Text = $"{allSummaries.Count} shares summarised.";
            });

        }

        internal void displayProgress(int counter)
        {
            stripText.Text = $"Preparing summary {counter} ...";
        }

        private void InstallGridViewColumns()
        {
            dgViewSummary.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    Name="Share",
                    DataPropertyName = "TheShare",
                });
            dgViewSummary.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    Name = "Earliest Day",
                    DataPropertyName = "FirstDay",
                });
            dgViewSummary.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    Name = "Latest Day",
                    DataPropertyName = "LastDay",
                });
            dgViewSummary.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    Name = "Trading Days",
                    DataPropertyName = "NumberOfTradingDays",
                });
            dgViewSummary.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    Name = "Last Price",
                    DataPropertyName = "Last Price",
                });
            dgViewSummary.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    Name = "Total Volume",
                    DataPropertyName = "TotalVolume",
                });
            dgViewSummary.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    Name = "Avg Daily Volume",
                    DataPropertyName = "AverageDailyVolume",
                });
            dgViewSummary.Columns.Add(
                new DataGridViewTextBoxColumn()
                {
                    Name = "Last Day Volume",
                    DataPropertyName = "LastDayVolume",
                });
        }


        internal void GenerateAllTableSummaryForGrid(Action<int> progress, List<AllTableSummary> summaries)
        {
            var allTablesFolder = Helper.GetAppUserSettings().AllTablesFolder;
            var shareLines = LocalStore.CreateShareArrayFromShareList().Skip(1);
            int shareCount = 0;
            foreach (string line in shareLines)
            {
                var share = Helper.CreateShareFromLine(line);
                string allTableFilename = allTablesFolder + $"\\alltable_{share.Number}.at";

                //determine First Day, Last Day and number of trading days by inspecting the All-Table file
                using (FileStream fs = new FileStream(allTableFilename, FileMode.Open))
                {
                    //read in entire all-table
                    var atRows = Helper.DeserializeList<AllTable>(fs).Skip(2).ToArray();
                    var numRows = atRows.Count();

                    var shareSummary = new AllTableSummary(share);
                    shareSummary.FirstDay = atRows[0].Date;
                    shareSummary.LastDay = atRows[numRows - 1].Date;
                    shareSummary.LastPrice = atRows[numRows - 1].FP;
                    shareSummary.TotalVolume = (uint)atRows.Sum(x => x.FV);
                    shareSummary.AverageDailyVolume = Convert.ToUInt32(104*atRows.Average(x => x.FV));
                    shareSummary.LastDayVolume = (uint)atRows.Skip(numRows - 104).Take(14).Sum(x => x.FV);
                    shareSummary.NumberOfTradingDays = (atRows.Count()) / 104;

                    summaries.Add(shareSummary);
                }
                shareCount++;
                //if (shareCount == 10) break;
                if (progress != null) progress(shareCount);
            }

        }

        private void dgViewSummary_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var row = dgViewSummary.Rows[e.RowIndex];
            AllTableSummary item = (AllTableSummary)row.DataBoundItem;

            //row.DefaultCellStyle.ForeColor = Color.Red;
            if (item != null && item.LastPrice==0)
            {
                row.Cells["Last Price"].Style.ForeColor = Color.Red;
            }
            if (item != null && item.TotalVolume == 0)
            {
                row.Cells["Total Volume"].Style.ForeColor = Color.Red;
            }
            if (item != null && item.LastDayVolume == 0)
            {
                row.Cells["Last Day Volume"].Style.ForeColor = Color.Red;
            }

        }
    }
}
