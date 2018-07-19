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
    internal partial class InputBox : Form
    {
        internal string returnValue;

        internal InputBox(string title, string inputPrompt)
        {
            InitializeComponent();
            this.Text = title;
            labelInput.Text = inputPrompt;
            buttonOk.Enabled = false;
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            returnValue = ((TextBox)sender).Text;
            buttonOk.Enabled = returnValue.Length > 0;
        }

    }
}
