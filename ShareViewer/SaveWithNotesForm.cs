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
    }
}
