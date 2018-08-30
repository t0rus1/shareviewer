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
    public partial class HelpForm : Form
    {
        string htmlDocName;

        public HelpForm(string htmlDocName)
        {
            InitializeComponent();
            this.htmlDocName = htmlDocName;
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            string applicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string myFile = Path.Combine(applicationDirectory, htmlDocName);
            webMain.Url = new Uri("file:///" + myFile);
        }

    }
}
