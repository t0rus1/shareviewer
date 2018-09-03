using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareViewer
{
    public partial class AutoOpsForm : Form
    {

        private List<AutoOpsEntry> opsTable = new List<AutoOpsEntry>();

        public AutoOpsForm()
        {
            InitializeComponent();
        }

        private void timerAuto_Tick(object sender, EventArgs e)
        {
            var dt = DateTime.Now;
            labelDate.Text = dt.ToLongDateString();
            labelTime.Text = dt.ToLongTimeString();
        }

        private void AutoOpsForm_Load(object sender, EventArgs e)
        {
            dgOps.ReadOnly = true;
            dgOps.AutoGenerateColumns = true;
            dgOps.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgOps.EnableHeadersVisualStyles = false;
            dgOps.AllowUserToResizeColumns = true;

            LoadOpsTable();
            BindDataGrid();
            ShowNumberOfShares();
            ShowCurrentDate();
            ShowLastIntake();
            SetInsteadDate();

        }

        private void SetInsteadDate()
        {
            DateTime.TryParse(Helper.UserSettings().AllTableDataEnd, out DateTime dtLast);

            // bump dtLast one trading day on and set InsteadDate
            //while also preventing it from being moved further
            dtInsteadDate.Value = dtLast.AddDays(1);
            while (!Helper.IsTradingDay(dtInsteadDate.Value))
            {
                dtInsteadDate.Value = dtInsteadDate.Value.AddDays(1);
            }
            dtInsteadDate.MinDate = dtInsteadDate.Value;
            dtInsteadDate.MaxDate = dtInsteadDate.Value;

        }

        private void ShowLastIntake()
        {
            //show last intake
            DateTime.TryParse(Helper.UserSettings().AllTableDataEnd, out DateTime dtLastIntake);
            labelLastDate.Text = dtLastIntake.ToLongDateString(); ;
            ShowUpToDateness(dtLastIntake);
        }

        private void ShowCurrentDate()
        {
            //show current date
            labelDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        private void ShowNumberOfShares()
        {
            //show number of shares
            stripText.Text = $"{bindingSource1.Count} shares...";
        }

        private void ShowUpToDateness(DateTime dtLastIntake)
        {
            //up to date status?
            int daysInclusive = Helper.ComputeTradingSpanInclusive(dtLastIntake, DateTime.Today);
            int daysBehind = daysInclusive - 1;
            if (daysBehind == 0)
            {
                labelUpToDateStatus.Text = "Fully up to date, (including today!)";
            }
            else if (daysBehind > 1)
            {
                labelUpToDateStatus.Text = $"Data intake is up to {daysBehind} days behind. You may catch up 1 day at a time, or recreate a complete new set of All-Tables";
            }
            else if (daysBehind == 1)
            {
                labelUpToDateStatus.Text = "Go into Auto mode to await|download and process today's data";
            }
        }

        private void BindDataGrid()
        {
            foreach (AutoOpsEntry item in opsTable)
            {
                bindingSource1.Add(item);
            }
            dgOps.DataSource = bindingSource1;
        }

        private void LoadOpsTable()
        {
            var allShares = LocalStore.ShareListByName();
            foreach (string shareLine in allShares)
            {
                Match m = Regex.Match(shareLine, @"(.+)\s(\d+)$");
                if (m.Success)
                {
                    var shareName = m.Groups[1].Value.TrimEnd();
                    var shareNum = Convert.ToInt32(m.Groups[2].Value);

                    var oe = new AutoOpsEntry()
                    {
                        ShareName = shareName,
                        ShareNum = shareNum,
                        AllTabled = false,
                        Calculated = false,
                        Overviewed = false,
                        RamAt = "no",
                        TradeInfo = "no"
                    };
                    opsTable.Add(oe);
                }
            }
        }

        //Do the necessary (if applicable and allowable) to go into Auto Mode
        private void MediateAutoMode()
        {
            //if already inAuto  mode, skip over
            if (!timerAuto.Enabled)
            {
                //if last data intake was more than 1 trading day back, issue warning and disallow
                DateTime.TryParse(Helper.UserSettings().AllTableDataEnd, out DateTime dtLast);
                //determine trading day span between last data and today (inclusive)
                int daySpan = Helper.ComputeTradingSpanInclusive(dtLast, DateTime.Today);
                int daysBehind = daySpan - 1;
                if (daysBehind == 0)
                {
                    var msg = $"You are already up to date.\nNo Automatic processing is necessary.";
                    MessageBox.Show(msg, "Please Note", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // TODO: possibly allow it?
                }
                if (daysBehind > 1)
                {
                    var msg = $"You are up to {daysBehind} days behind on Intake.\nPlease remain in Manual Mode and process each 'behind' trading day in turn.";
                    MessageBox.Show(msg, "Warning: Data Intake is behind", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // We are only 1 trading day behind, so 
                GoAutoMode();
            }
        }

        private void GoAutoMode()
        {
            stripText.Text = "Waiting in Auto mode...";
            dtInsteadDate.Enabled = false;
            buttonCatchUp.Enabled = false;
            dtInsteadDate.Visible = false;
            buttonCatchUp.Visible = false;
            radioButtonManual.Checked = false;
            radioButtonAuto.Checked = true;
            timerAuto.Enabled = true;
        }

        private void GoManualMode()
        {
            timerAuto.Enabled = false;
            labelTime.Text = "";
            stripText.Text = "Now in Manual mode";
            dtInsteadDate.Enabled = true;
            buttonCatchUp.Enabled = true;
            dtInsteadDate.Visible = true;
            buttonCatchUp.Visible = true;
            radioButtonManual.Checked = true;
            radioButtonAuto.Checked = false;
        }



        //The radio button pair are not in Auto mode, so we must control their states
        //explicitly
        private void radioButtonAuto_Click(object sender, EventArgs e)
        {
            MediateAutoMode();
        }

        //The radio button pair are not in Auto mode, so we must control their states
        //explicitly
        private void radioButtonManual_Click(object sender, EventArgs e)
        {
            //when enabled, we can always go into manual mode
            timerAuto.Enabled = false;
            GoManualMode();
        }
    }
}
