using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ShareViewer.Properties;

namespace ShareViewer
{
    internal static class Helper
    {
        internal static MainForm GetMainForm()
        {
            return (MainForm)Application.OpenForms["MainForm"];
        }

        internal static Control  GetMainFormControl(string controlName)
        {
            return GetMainForm().Controls.Find(controlName, true)[0];
        }

        internal static Properties.Settings UserSettings()
        {
            return Settings.Default;
        }

        //place message on status strip
        internal static void Status(string msg)
        {
            var form = GetMainForm();
            if (form.statusStrip.Items.ContainsKey("stripText"))
            {
                form.statusStrip.Items["stripText"].Text = msg;
            }
            //form.stripText.Text = msg;
        }

        internal static void UpdateAllTableProgress(string msg)
        {
            var form = GetMainForm();
            form.labelGenNewAllTables.Text = msg;
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
        internal static void InitProgressCountdown(string progressBarName, string partnerLabel, int count)
        {
            var form = GetMainForm();

            var progressBar = (ProgressBar)form.Controls.Find(progressBarName, true).FirstOrDefault();
            progressBar.Maximum = count;
            progressBar.Value = count;
            progressBar.Visible = true;
            var busyLabel = (Label)form.Controls.Find(partnerLabel, true).FirstOrDefault();
            busyLabel.Visible = true;
        }

        internal static void UpdateHolidayHash(ref Dictionary<string, string> holidayHash)
        {
            holidayHash.Clear();
            var aus = UserSettings();
            foreach (string day in aus.Holidays)
            {
                var keyDay = day.Split('=')[0].Trim();
                var valName = day.Split('=')[1].Trim();
                holidayHash.Add(keyDay, valName);
            }
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
            //var form = GetMainForm();
            //var lb = (ListBox)form.Controls.Find(listBoxName, true).FirstOrDefault();
            var lb = (ListBox)GetMainFormControl(listBoxName);
            //note: '\x2714' = ✔
            //note: item may have a leading ✔
            lb.SelectedIndex = lb.Items.IndexOf(item);
            totalItems = lb.Items.Count;
            return totalItems - lb.SelectedItems.Count;
        }

        //counts number of data files which need to be downloaded
        internal static int UntickedDayDataEntries(string listBoxName)
        {
            var lb = (ListBox)GetMainFormControl(listBoxName);
            int unTicked = 0;
            foreach (string item in lb.Items)
            {
                if (!item.StartsWith("\x2714"))  unTicked++;
            }
            return unTicked;
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
            form.stripText.Text = msg;
            Log(level, msg);
        }

        //log to log file only
        internal static void Log(string level, string msg)
        {
            switch (level.ToLower())
            {
                case "error":
                    Program.log.Error(msg);
                    break;
                case "warn":
                    Program.log.Warn(msg);
                    break;
                case "info":
                    Program.log.Info(msg);
                    break;
                case "debug":
                    Program.log.Debug(msg);
                    break;
                case "fatal":
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
        internal static void HoldWhileDownloadingDayData(bool hold)
        {
            var form = GetMainForm();
            ((Button)form.Controls.Find("buttonDayDataDownload", true)[0]).Enabled = !hold;
            ((Button)form.Controls.Find("buttonNewShareList", true)[0]).Enabled = !hold;
            ((Button)form.Controls.Find("buttonDays", true)[0]).Enabled = !hold;
            ((GroupBox)form.Controls.Find("groupBoxSource", true)[0]).Enabled = !hold;
            ((Button)form.Controls.Find("buttonNewAllTables", true)[0]).Enabled = !hold;
            //((Button)form.Controls.Find("buttonAddToAllTables", true)[0]).Enabled = !hold;
            ((TextBox)form.Controls.Find("textBoxShareNumSearch", true)[0]).Enabled = !hold;

            ((MonthCalendar)form.Controls.Find("calendarFrom", true)[0]).Enabled = !hold;
            ((MonthCalendar)form.Controls.Find("calendarTo", true)[0]).Enabled = !hold;
            ((NumericUpDown)form.Controls.Find("daysBack", true)[0]).Enabled = !hold;

        }

        //disable / enable some buttons
        internal static void HoldWhileGeneratingNewAllTables(bool hold, bool topUp)
        {
            var form = GetMainForm();
            ((Button)form.Controls.Find("buttonDayDataDownload", true)[0]).Enabled = !hold;
            ((Button)form.Controls.Find("buttonDays", true)[0]).Enabled = !hold;
            ((Button)form.Controls.Find("buttonNewShareList", true)[0]).Enabled = !hold;  

            ((ListBox)form.Controls.Find("listBoxShareList", true)[0]).Enabled = !hold;
            ((TextBox)form.Controls.Find("textBoxShareNumSearch", true)[0]).Enabled = !hold;

            ((GroupBox)form.Controls.Find("groupBoxSource", true)[0]).Enabled = !hold;
            if (hold)
            {
                //allow this button to be disabled only
                ((Button)form.Controls.Find("buttonNewAllTables", true)[0]).Enabled = false;
                //label text must be based on state of button
                ((LinkLabel)form.Controls.Find("linkLabelAllowNew", true)[0]).Text = "unlock";
                //single day reload link label can only be made invisible
                //((LinkLabel)form.Controls.Find("linkLabelSingleDayLoad", true)[0]).Visible = false;
            }
            //((Button)form.Controls.Find("buttonAddToAllTables", true)[0]).Enabled = !hold; 

            ((MonthCalendar)form.Controls.Find("calendarFrom", true)[0]).Enabled = !hold;
            ((MonthCalendar)form.Controls.Find("calendarTo", true)[0]).Enabled = !hold;
            ((NumericUpDown)form.Controls.Find("daysBack", true)[0]).Enabled = !hold; 

            //make progressBar and paired label visible/not
            ((ProgressBar)form.Controls.Find("progressBarGenNewAllTables", true)[0]).Visible = hold;
            ((Label)form.Controls.Find("labelGenNewAllTables", true)[0]).Visible = hold;
            ((Button)form.Controls.Find("buttonBusyAllTables", true)[0]).Visible = !topUp && hold;

        }

        internal static bool UserAbortsAllTableGeneration()
        {
            var form = GetMainForm();
            Button btn = ((Button)form.Controls.Find("buttonBusyAllTables", true)[0]);
            return (btn.Text.StartsWith("Stopping"));
        }

        internal static void ContinueEnableAllTableGeneration()
        {
            var form = GetMainForm();
            Button btn = ((Button)form.Controls.Find("buttonBusyAllTables", true)[0]);
            btn.Text = "Busy generating All-Tables...Click to Abort";
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
        }

        internal static ICollection<AllTable> DeserializeAllTable<AllTable>(FileStream fs)
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

        internal static ICollection<Overview> DeserializeOverview<Overview>(FileStream fs)
        {
            BinaryFormatter bf = new BinaryFormatter();
            List<Overview> list = new List<Overview>();
            while (fs.Position != fs.Length)
            {
                //deserialize each object in the file
                var deserialized = (Overview)bf.Deserialize(fs);
                //add individual object to a list
                list.Add(deserialized);
            }
            //return the list of objects
            return list;
        }

        internal static void SerializeOverviewRecord(FileStream fs, Overview overviewRec)
        {
            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, overviewRec);
            }
            catch (SerializationException e)
            {
                Helper.Log("Error", "Failed to serialize. Reason: " + e.Message);
                throw;
            }
        }

        //given a date, construct a file name like "YYYY_MM_DD.TXT"
        internal static string BuildDayDataFilename(DateTime date)
        {
            //return date.ToShortDateString().Replace("/", "_") + ".TXT";
            return date.ToString("yyyy_MM_dd") + ".TXT";

        }

        internal static string GetCompressedDate(DateTime date)
        {
            //return date.ToShortDateString().Replace("/", "").Substring(2); // YYMMDD
            return date.ToString("yyMMdd"); // culture independent
        }
        
        //grabs run of digits at end of string, returns as an integer
        internal static int GetDigitsAtEnd(string line)
        {
            int digitsNum = 0;
            Match regMatch = Regex.Match(line, @".+\s(\d+)$");
            if (regMatch.Success)
            {
                digitsNum = Convert.ToInt16(regMatch.Groups[1].Value);
            }
            return digitsNum;
        }

        internal static Share CreateShareFromLine(string line)
        {
            Match regMatch = Regex.Match(line, @"(.+)\s(\d+)$");
            if (regMatch.Success)
            {
                var shareName = regMatch.Groups[1].Value.TrimEnd();
                var shareNum = Convert.ToInt16(regMatch.Groups[2].Value);
                return new Share(shareName, shareNum);
            }
            else
            {
                return new Share("???", 0);
            }
        }

        internal static int ComputeTimeBand(string trade)
        {
            //Compute timeband (timband 1 is from 09:00:00 - 09:04:59)
            //The timebands in practice run from 1 to 104 each day ie 09:00:00 to 17:39:59
            Match regMatch = Regex.Match(trade, @"((\d{2}):(\d{2}):(\d{2}));([\d,]+);(\d+);(\d+)");
            if (regMatch.Success)
            {
                int hr, min, sec, totalsecs; ;
                hr = Convert.ToInt16(regMatch.Groups[2].Value);
                min = Convert.ToInt16(regMatch.Groups[3].Value);
                sec = Convert.ToInt16(regMatch.Groups[4].Value);
                if ((hr < 9 || hr > 17) || (min > 59) || (sec > 59))
                {
                    return 0;// indicate not in allowable band
                }
                else
                {
                    totalsecs = 3600 * hr + 60 * min + sec;
                    int tb = (totalsecs / (9 * 3600)) + (totalsecs % (9 * 3600)) / 300;
                    if (tb > 0 && tb < 105)
                    {
                        return tb;
                    }
                    else
                    {
                        return 0; // indicate not in allowable band
                    }
                }
            }
            else
            {
                //throw (new FormatException($"could not extract timeband from trade '{trade}'"));
                return 0; // indicate error
            }
        }

        //Computes (inclusive) number of trading days between passed in dates
        //If both days are the same (trading) date, it returns 1
        //If either day is a weekend day or holiday, they get skipped over and do not add to the count
        //If both dates are trading days and they are adjacent, 2 is returned
        internal static int ComputeTradingSpanInclusive(DateTime fromDate, DateTime toDate)
        {
            int days = 0;
            DateTime runDate = fromDate;
            while (runDate <= toDate)
            {
                DayOfWeek dow = runDate.DayOfWeek;
                if (dow != DayOfWeek.Saturday && dow != DayOfWeek.Sunday && !IsHoliday(runDate) )
                {
                    days++;
                }
                runDate = runDate.AddDays(1);
            }
            return days;
        }

        internal static int ComputeNumberOfHolidays(DateTime fromDate, DateTime toDate, Dictionary<string,string> holidayHash, ref List<string> spannedHols)
        {
            int holiDays = 0;
            DateTime runDate = fromDate;
            while (runDate <= toDate)
            {
                DayOfWeek dow = runDate.DayOfWeek;
                if (IsHoliday(runDate))
                {
                    holiDays++;
                    //var holidayKey = runDate.ToShortDateString(); // YYYY/MM/DD in ZA
                    var holidayKey = runDate.ToString("yyyy/MM/dd"); // force it to be so regardless of culture
                    var holidayEntry = $"{holidayHash[holidayKey]} {holidayKey}";
                    spannedHols.Add(holidayEntry);
                }
                runDate = runDate.AddDays(1);
            }
            return holiDays;
        }

        internal static void MarkHolidaysInCalendars(ref MonthCalendar fromCal, ref MonthCalendar toCal, Dictionary<string, string> holidayHash)
        {
            DateTime[] bolds = new DateTime[24];

            DateTime runDate = fromCal.SelectionStart;
            int holIndex = 0;
            while (runDate <= toCal.SelectionStart)
            {
                DayOfWeek dow = runDate.DayOfWeek;
                if (IsHoliday(runDate))
                {
                    if (holIndex < 24)
                    {
                        bolds[holIndex] = runDate;
                        bolds[holIndex] = runDate;
                        holIndex++;
                    }
                    else
                    {
                        LogStatus("Error", "Reached limit for number of holidays (24)");
                        break;
                    }
                }
                runDate = runDate.AddDays(1);
            }
            fromCal.BoldedDates = bolds;
            toCal.BoldedDates = bolds;
        }


        private static bool IsHoliday(DateTime runDate)
        {
            //string dateStr = runDate.ToShortDateString(); // YYYY/MM/DD in ZA culture
            string dateStr = runDate.ToString("yyyy/MM/dd"); // culture independant
            return GetMainForm().HolidayHash.ContainsKey(dateStr);
        }

        //given an endDate, compute how many actual days we need to go back such that we encompass
        //the given number of trading days
        internal static int ActualDaysBackToEncompassTradingDays(DateTime endDate, int tradingDays)
        {
            int estimatedDaysback = ((tradingDays * 7) / 5);

            var computedTradingDays = ComputeTradingSpanInclusive(endDate.AddDays(-estimatedDaysback), endDate);
            var diff = computedTradingDays - tradingDays;
            while (diff != 0)
            {
                if (diff > 0)
                {
                    //computed Trading days too much, move start day up by 1
                    estimatedDaysback--;

                }
                else if (diff < 0)
                {
                    //computed Trading days too few, move start day back by 1
                    estimatedDaysback++;
                }
                computedTradingDays = ComputeTradingSpanInclusive(endDate.AddDays(-estimatedDaysback), endDate);
                diff = computedTradingDays - tradingDays;
            }

            //we now have a correct number of days back, but it might be a day or two further back than needed
            //if we've gone back too far to a sunday, move a day forward
            var landingDay = endDate.AddDays(-estimatedDaysback).DayOfWeek;
            if (landingDay == DayOfWeek.Sunday)
            {
                estimatedDaysback--;
            }
            else if (landingDay == DayOfWeek.Saturday)
            {
                //else if we've gone back too far to a saturday, move 2 days forward
                estimatedDaysback -= 2;
            }

            return estimatedDaysback;

        }

        internal static string Repeat(string value, int count)
        {
            return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }

        internal static bool IsTradingDay(DateTime runDate)
        {
            return (runDate.DayOfWeek != DayOfWeek.Saturday) && (runDate.DayOfWeek != DayOfWeek.Sunday) && !IsHoliday(runDate);
        }

        //given a string of the form YYMMDD, return a DateTime
        internal static DateTime ConvertCompressedDateToDateTime(string lastDayOnHand)
        {
            var Year = Convert.ToInt16("20" + lastDayOnHand.Substring(0, 2));
            var Month = Convert.ToInt16(lastDayOnHand.Substring(2, 2));
            var Day = Convert.ToInt16(lastDayOnHand.Substring(4, 2));

            return new DateTime(Year, Month, Day);

        }


    }
}
