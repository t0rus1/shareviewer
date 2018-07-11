using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Core;

namespace ShareViewer
{
    internal static class Helper
    {
        internal static MainForm GetMainForm()
        {
            return (MainForm)Application.OpenForms["MainForm"];
        }
        
        //place message on status strip
        internal static void Status(string msg)
        {
            var form = GetMainForm();
            form.statusStrip.Items["stripText"].Text = msg;
        }


        internal static void SetProgressBar(string progressBarName, double numerator, double denominator)
        {
            var form = GetMainForm();

            var progressBar = (ProgressBar)form.Controls.Find(progressBarName, true).FirstOrDefault();
            progressBar.Visible = true;

            if (denominator > 0)
            {
                progressBar.Value = Convert.ToInt16(100 * (numerator / denominator));
            }
        }

        //initialize a progressbar countdown, make it and its partner Label visible as well
        internal static void InitProgressCountdown(string progressBarName,string partnerLabel, int count)
        {
            var form = GetMainForm();

            var progressBar = (ProgressBar)form.Controls.Find(progressBarName, true).FirstOrDefault();
            progressBar.Maximum = count;
            progressBar.Value = count;
            progressBar.Visible = true;
            var busyLabel = (Label)form.Controls.Find(partnerLabel, true).FirstOrDefault();
            busyLabel.Visible = true;
        }

        //decrement a progressbar and hide it when it hits zero
        internal static void DecrementProgressCountdown(string progressBarName, string partnerLabel)
        {
            var form = GetMainForm();

            var progressBar = (ProgressBar)form.Controls.Find(progressBarName, true).FirstOrDefault();

            if (progressBar.Value > 0)
            {
                progressBar.Value -= 1;
                if (progressBar.Value == 0)
                {
                    progressBar.Visible = false;
                    var busyLabel = (Label)form.Controls.Find(partnerLabel, true).FirstOrDefault();
                    busyLabel.Visible = false;
                }
            }
        }

        //selects passed in item and returns number of items remaining unselected.
        //also returns totalItems as an out parameter
        internal static int MarkListboxItem(string listBoxName, string item, out int totalItems)
        {
            var form = GetMainForm();

            var lb = (ListBox)form.Controls.Find(listBoxName, true).FirstOrDefault();
            //note: '\x2714' = ✔
            //note: item may have a leading ✔
            lb.SelectedIndex = lb.Items.IndexOf(item);
            totalItems = lb.Items.Count;
            return totalItems - lb.SelectedItems.Count;
        }

        internal static void SetProgressBarVisibility(string progressBarName, bool visible)
        {
            var form = GetMainForm();
            var progressBar = (ProgressBar)form.Controls.Find(progressBarName, true).FirstOrDefault();
            progressBar.Visible = visible;
        }

        //place message on status strip and log to logfile
        internal static void LogStatus(string level, string msg)
        {
            var form = (MainForm)Application.OpenForms["MainForm"];
            form.statusStrip.Items["stripText"].Text = msg;
            Log(level, msg);
        }

        //log to log file only
        internal static void Log(string level, string msg)
        {
            switch (level)
            {
                case "Error":
                    Program.log.Error(msg);
                    break;
                case "Warn":
                    Program.log.Warn(msg);
                    break;
                case "Info":
                    Program.log.Info(msg);
                    break;
                case "Debug":
                    Program.log.Debug(msg);
                    break;
                case "Fatal":
                    Program.log.Fatal(msg);
                    break;
                default:
                    Program.log.Info(msg);
                    break;
            }

        }

        internal static void ListBoxClearAndScrollToBottom(ListBox lb)
        {
            //force scroll to bottom
            lb.SelectedIndex = lb.Items.Count - 1;
            lb.ClearSelected();
        }

        internal static void FileDeleteIfExists(string pathAndFile)
        {
            if (File.Exists(pathAndFile))
            {
                File.Delete(pathAndFile);
            }
        }

        /// Move file, optionally either silently doing nothing or overwriting destination if it already exists
        internal static void FileMoveConditionally(string fromPath, string toPath, bool overwrite)
        {
            if (File.Exists(fromPath))
            {
                if (File.Exists(toPath))
                {
                    if (overwrite) File.Delete(toPath); else return;
                }
                File.Move(fromPath, toPath);
            } // exits silently if not found
        }

        //disable / enable some buttons
        internal static void HoldMajorActivity(bool hold)
        {
            var form = GetMainForm();
            ((Button)form.Controls.Find("buttonDayDataDownload", true)[0]).Enabled = !hold;
            //((Button)form.Controls.Find("buttonNewShareList", true)[0]).Enabled = !hold;
            ((GroupBox)form.Controls.Find("groupBoxSource", true)[0]).Enabled = !hold;

            ((MonthCalendar)form.Controls.Find("calendarFrom", true)[0]).Enabled = !hold;
            ((MonthCalendar)form.Controls.Find("calendarTo", true)[0]).Enabled = !hold;
            ((NumericUpDown)form.Controls.Find("daysBack", true)[0]).Enabled = !hold;

        }

        internal static void SerializeAllTableRecord(FileStream fs, AllTable atRec)
        {
            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, atRec);
            }
            catch (SerializationException e)
            {
                Helper.Log("Error", "Failed to serialize. Reason: " + e.Message);
                throw;
            }
            //finally
            //{
            //    fs.Close();
            //}
        }

        internal static ICollection<AllTable> DeserializeList<AllTable>(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            List<AllTable> list = new List<AllTable>();
            while (fs.Position != fs.Length)
            {
                //deserialize each object in the file
                var deserialized = (AllTable)bf.Deserialize(fs);
                //add individual object to a list
                list.Add(deserialized);
            }
            //return the list of objects
            return list;
        }




    }
}
