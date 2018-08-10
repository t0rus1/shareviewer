using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareViewer
{
    internal static class AlternateData
    {

        //internal static void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        //{
        //    Console.WriteLine("Output from other process");
        //    Console.WriteLine(e.Data);
        //}

        //internal static void cmd_Error(object sender, DataReceivedEventArgs e)
        //{
        //    Console.WriteLine("Error from other process");
        //    Console.WriteLine(e.Data);
        //}

        // Reads extended, hidden data associated with a file
        // see https://stackoverflow.com/questions/13172129/store-metadata-outside-of-file-any-standard-approach-on-modern-windows?noredirect=1&lq=1
        // and https://stackoverflow.com/questions/5367557/how-to-parse-command-line-output-from-c?noredirect=1&lq=1
        //
        internal static void SetHiddenData(string notes, string fileName, string hideName,
            DataReceivedEventHandler cmd_DataReceived, DataReceivedEventHandler cmd_Error)
        {
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            cmdStartInfo.RedirectStandardOutput = true;
            cmdStartInfo.RedirectStandardError = true;
            cmdStartInfo.RedirectStandardInput = true;
            cmdStartInfo.UseShellExecute = false;
            cmdStartInfo.CreateNoWindow = true;

            Process cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.ErrorDataReceived += cmd_Error;
            cmdProcess.OutputDataReceived += cmd_DataReceived;
            cmdProcess.EnableRaisingEvents = true;
            cmdProcess.Start();
            cmdProcess.BeginOutputReadLine();
            cmdProcess.BeginErrorReadLine();

            //if notes contain newlines, we have to execute an echo command for each line
            foreach (string item in notes.Split(new char[] { '\r','\n' }))
            {
                string line = item;
                if (line == "")
                {
                    line += ".";
                }
                string cmdToRun = $"echo {line} >>\"{fileName}:{hideName}.txt\"";  //e.g. "ping bing.com"
                cmdProcess.StandardInput.WriteLine(cmdToRun);

            }
            cmdProcess.StandardInput.WriteLine("exit");                 

            cmdProcess.WaitForExit();
        }


    }
}
