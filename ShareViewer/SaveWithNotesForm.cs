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
    public partial class SaveWithNotesForm : Form
    {
        string initialNotes;

        public SaveWithNotesForm(string initNotes)
        {
            InitializeComponent();

            initialNotes = initNotes;
        }

        private void SaveWithNotesForm_Load(object sender, EventArgs e)
        {
            textBoxNotes.Text = initialNotes;
        }

        private void linkLabelSummarizeParams_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var summary = new StringBuilder();
            var aus = Helper.UserSettings();

            summary.Append(aus.ParamsLazyShare.Summarize());
            summary.Append("\r\n");
            summary.Append(aus.ParamsSlowPrice.Summarize());
            summary.Append("\r\n");
            summary.Append(aus.ParamsDirectionAndTurning.Summarize());
            summary.Append("\r\n");
            summary.Append(aus.ParamsFiveMinsGradientFigure.Summarize());
            summary.Append("\r\n");
            summary.Append(aus.ParamsMakeHighLine.Summarize());
            summary.Append("\r\n");
            summary.Append(aus.ParamsMakeLowLine.Summarize());
            summary.Append("\r\n");
            summary.Append(aus.ParamsMakeSlowVolume.Summarize());
            summary.Append("\r\n");
            summary.Append(aus.ParamsSlowVolFigSVFac.Summarize());
            summary.Append("\r\n");
            summary.Append(aus.ParamsSlowVolFigSVFbd.Summarize());
            summary.Append("\r\n");
            textBoxNotes.Text += summary.ToString();

        }
    }
}
