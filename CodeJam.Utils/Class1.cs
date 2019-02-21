using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam.Utils
{
    /// <summary>
    /// This is the object that set the standard input and output of an executable process
    /// </summary>
    public class StandardObject
    {
        string outputFileName;
        string inputFileName;
        string consoleExeFile;
        private Process process;

        public StandardObject(string outputFileName , string inputFileName , string consoleExeFile)
        {
            this.inputFileName = inputFileName;
            this.outputFileName = outputFileName;
            this.consoleExeFile = consoleExeFile;
        }

        public void Start()
        {
            process = new Process();
            process.StartInfo.FileName = consoleExeFile;
            process.StartInfo.Arguments = "< " + inputFileName + " > " + outputFileName;

            process.Start();
           
            process.Exited += Process_Exited;
            Console.ReadLine();
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            process.StandardInput.WriteLine(e.Data);
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            
        }
    }
}
