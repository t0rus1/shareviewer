using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShareViewer
{
    public partial class AllTableRepairForm : Form
    {
        public AllTableRepairForm()
        {
            InitializeComponent();
        }

        private void AllTableRepairForm_Load(object sender, EventArgs e)
        {
            listBoxRepairShares.DataSource = LocalStore.ShareListByName();
            listBoxRepairShares.ClearSelected();
        }
    }
}
