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

        private void SaveSelectedSharesList(string fileName)
        {
            //selected items only, stripped of trailing share numbers
            //so that the sharelist can be used even thru new All-Table generations
            List<string> selecteds = new List<string>();
            foreach (string shareLine in listBoxRepairShares.SelectedItems)
            {
                string cleanShare = Regex.Replace(shareLine, @"[\s\d]+$", "");
                selecteds.Add(cleanShare);
            }
            File.WriteAllLines(fileName, selecteds.ToArray());
        }


        //save selected shares in a file
        private void linkLabelSaveShareList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (listBoxRepairShares.SelectedItems.Count > 0)
            {
                var saveFileDlg = new SaveFileDialog();
                saveFileDlg.Filter = "Saved Share Selections|*.shl";
                saveFileDlg.Title = "Save Named Share Selection";
                saveFileDlg.ShowDialog();

                if (saveFileDlg.FileName != "")
                {
                    SaveSelectedSharesList(saveFileDlg.FileName);
                }

            }


        }

        // (re)select shares based on previosuly saved selection list
        private void linkLabelLoadSelections_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Saved Share Selections|*.shl";
            openFileDlg.Title = "Choose a Saved Share Selection";
            openFileDlg.ShowDialog();

            if (openFileDlg.FileName != "")
            {
                //get our selections
                var selecteds = File.ReadAllLines(openFileDlg.FileName);
                //clear current shares listbox of selections
                listBoxRepairShares.ClearSelected();
                //proceed to select ones we can find

                foreach (string wantedShare in selecteds)
                {
                    //try to find wantedShare in main listbox
                    int lbIndex = 0;
                    foreach (string item in listBoxRepairShares.Items)
                    {
                        if (item.StartsWith(wantedShare))
                        {
                            listBoxRepairShares.SetSelected(lbIndex, true);
                            break;
                        }
                        lbIndex++;
                    }
                }

            }

        }

        private void AllTableRepairForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            var helpForm = new HelpForm("Help_GenSelectedAllTables.html");
            helpForm.Show();
        }
    }
}
