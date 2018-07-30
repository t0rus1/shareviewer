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
                    var atRows = Helper.DeserializeList<AllTable>(fs).ToArray();

                    var shareSummary = new AllTableSummary(share);
                    shareSummary.FirstDay = atRows[2].Date;
                    shareSummary.LastDay = atRows[atRows.Count() - 1].Date;
                    shareSummary.NumberOfTradingDays = (atRows.Count() - 2) / 104;

                    summaries.Add(shareSummary);
                }
                shareCount++;
                //if (shareCount == 10) break;
                if (progress != null) progress(shareCount);
            }

        }

    }
}
