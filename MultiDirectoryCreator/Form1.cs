using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiDirectoryCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            rootPath.Text = baseDirectory;
            threads.Value = 1;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
             w = File.AppendText("log.txt");
        }

        private void Start_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            if (running == true)
                return;

            running = true;
            testDir = 0;
            runThread = new System.Threading.Thread(new System.Threading.ThreadStart(runTasks));
            runThread.Start();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            if (running == false)
                return;

            running = false;
            runThread.Abort();
        }

        private void runTasks()
        {
            while (true)
            {
                List<Task> TaskList = new List<Task>();

                for (int i = 0; i < this.threads.Value; i++)
                {                    
                    var LastTask = new Task(process);
                    LastTask.Start();
                    TaskList.Add(LastTask);
                }

                Task.WaitAll(TaskList.ToArray());
                testDir++;
             }
        }

        private void process()
        {
            string dirCreate = baseDirectory + @"\" + testDir.ToString() + @"\" + testDir.ToString() + @"\" + testDir.ToString() + @"\" + testDir.ToString() + @"\" + Guid.NewGuid().ToString();
            try
            {
                DirectoryInfo dInfo = Directory.CreateDirectory(dirCreate);
                Console.WriteLine("Directory info: {0} {1}", dirCreate, dInfo.Attributes.ToString());
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine("Interrupted: {0}", ex.Message);
                Log("Interrupted", w);
            }
            catch (ThreadAbortException ex)
            {
                Console.WriteLine("Aborted: {0}", ex.Message);
                Log("Aborted", w);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Caught IOException: {0}", ex.Message);
                Log(ex.Message, w);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught Exception: {0}", ex.Message);
                Log(ex.Message, w);
            }
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            DialogResult result = BaseDir.ShowDialog();
            if (result == DialogResult.OK)
            {
                baseDirectory = BaseDir.SelectedPath;
                rootPath.Text = baseDirectory;
            }
        }

        private void TextChanged_Click(object sender, EventArgs e)
        {
            baseDirectory = rootPath.Text;
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine("\nLog Entry : {0} {1} {2} {3}",
                DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString(),
                logMessage,
                "-------------------------------");
            w.Flush();
        }

        private string baseDirectory = "C:\\TEMP";
        static private System.Threading.Thread runThread;
        private Boolean running = false;
        private StreamWriter w;
        private int testDir = 0;
    }
}