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
            while (!runThreadStopped)
                Thread.Sleep(1000);
        }

        private void runTasks()
        {
            runThreadStopped = false;
            Log("Started", w);
            try
            {
                while (running)
                {
                    List<Task> TaskList = new List<Task>();

                    for (int i = 0; i < this.threads.Value; i++)
                    {
                        var LastTask = new Task(process);
                        LastTask.Start();
                        TaskList.Add(LastTask);
                    }

                    Task.WaitAll(TaskList.ToArray());
                    Thread.Sleep(2000);
                    testDir++;
                }
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
                Log(ex, w);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught Exception: {0}", ex.Message);
                Log(ex, w);
            }
            runThreadStopped = true;
            Log("Stopped", w);
        }

        private void process()
        {
            string entryCreate = Guid.NewGuid().ToString();
            string dirCreate = baseDirectory + @"\" + testDir.ToString() + @"\" + testDir.ToString() + @"\" + testDir.ToString() + @"\" + testDir.ToString();
            string fileCreate = dirCreate + @"\" + entryCreate;
            // Create a string array with the lines of text
            string[] lines = { "First line", "Second line", "Third line" };
            bool r = true;
            try
            {
                Directory.CreateDirectory(dirCreate);
                //r = InternalCreateDirectory(dirCreate, dirCreate);
                Console.WriteLine("Created Directory: {0}", dirCreate);
            }
            catch (ThreadInterruptedException ex)
            {
                Console.WriteLine("Interrupted: {0}", ex.Message);
                Log("Interrupted", w);
                return;
            }
            catch (ThreadAbortException ex)
            {
                Console.WriteLine("Aborted: {0}", ex.Message);
                Log("Aborted", w);
                return;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Caught IOException: {0}", ex.Message);
                string msg = "Failed to create " + dirCreate + " for entry " + entryCreate;
                Log(ex, msg, w);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught Exception: {0}", ex.Message);
                string msg = "Failed to create " + dirCreate + " for entry " + entryCreate;
                Log(ex, msg, w);
                return;
            }

            if (r == false)
            {
                string msg = "Failed to create " + dirCreate + " for entry " + entryCreate;
                Console.WriteLine("Failed to write entry: {0}", msg);
                Log(msg, w);
                return;
            }

            try
            {
                using (StreamWriter outputFile = new StreamWriter(fileCreate))
                {
                    foreach (string line in lines)
                    {
                        outputFile.WriteLine(line);
                    }
                }
                FileAttributes fdInfo = File.GetAttributes(fileCreate);
                Console.WriteLine("Wrote File: {0} {1}", fileCreate, fdInfo.ToString());
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
                Log(ex, w);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught Exception: {0}", ex.Message);
                Log(ex, w);
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
            w.WriteLine("{0} {1}:{2} {3} {4}",
                AppDomain.GetCurrentThreadId(),
                DateTime.Now.ToShortDateString(),
                DateTime.Now.ToLongTimeString(),
                logMessage,
                "-------------------------------");
            w.Flush();
        }

        public static void Log(Exception ex, TextWriter w)
        {
            w.WriteLine("{0} {1}:{2} {3} {4} {5}",
                AppDomain.GetCurrentThreadId(),
                DateTime.Now.ToShortDateString(),
                DateTime.Now.ToLongTimeString(),
                ex.Message,
                ex.StackTrace,
                "-------------------------------");
            w.Flush();
        }

        public static void Log(Exception ex, String msg, TextWriter w)
        {
            w.WriteLine("{0} {1}:{2} {3} {4} {5} {6}",
                AppDomain.GetCurrentThreadId(),
                DateTime.Now.ToShortDateString(),
                DateTime.Now.ToLongTimeString(),
                msg,
                ex.Message,
                ex.StackTrace,
                "-------------------------------");
            w.Flush();
        }

        internal static bool IsDirectorySeparator(char c)
        {
            return (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar);
        }

        private static int GetRootLength(string path)
        {
            int i = 0;
            int length = path.Length;

            if (length >= 1 && (IsDirectorySeparator(path[0])))
            {
                // handles UNC names and directories off current drive's root.
                i = 1;
                if (length >= 2 && (IsDirectorySeparator(path[1])))
                {
                    i = 2;
                    int n = 2;
                    while (i < length && ((path[i] != Path.DirectorySeparatorChar && path[i] != Path.AltDirectorySeparatorChar) || --n > 0)) i++;
                }
            }
            else if (length >= 2 && path[1] == Path.VolumeSeparatorChar)
            {
                // handles A:\foo.
                i = 2;
                if (length >= 3 && (IsDirectorySeparator(path[2]))) i++;
            }
            return i;
        }

        static bool InternalExists(String path)
        {
            FileAttributes attributes;
            try
            {
                attributes = File.GetAttributes(path);
            }
            catch (IOException ex)
            {
                return false;
            }

            return (attributes == FileAttributes.Directory);
        }

        internal const int MaxLongPath = short.MaxValue;

        static bool IsPathTooLong(string fullPath)
        {
            return fullPath.Length >= MaxLongPath;
        }

        static bool InternalCreateDirectory(String fullPath, String path)
        {
            int length = fullPath.Length;

            // We need to trim the trailing slash or the code will try to create 2 directories of the same name.
            if (length >= 2 && IsDirectorySeparator(fullPath[length - 1]))
                length--;

            int lengthRoot = GetRootLength(fullPath);

            // For UNC paths that are only // or /// 
            if (length == 2 && IsDirectorySeparator(fullPath[1]))
                throw new IOException("IO.IO_CannotCreateDirectory");

            // We can save a bunch of work if the directory we want to create already exists.  This also
            // saves us in the case where sub paths are inaccessible (due to ERROR_ACCESS_DENIED) but the
            // final path is accessable and the directory already exists.  For example, consider trying
            // to create c:\Foo\Bar\Baz, where everything already exists but ACLS prevent access to c:\Foo
            // and c:\Foo\Bar.  In that case, this code will think it needs to create c:\Foo, and c:\Foo\Bar
            // and fail to due so, causing an exception to be thrown.  This is not what we want.
            if (InternalExists(fullPath))
            {
                return true;
            }

            List<string> stackDir = new List<string>();

            // Attempt to figure out which directories don't exist, and only
            // create the ones we need.  Note that InternalExists may fail due
            // to Win32 ACL's preventing us from seeing a directory, and this
            // isn't threadsafe.

            bool somepathexists = false;

            if (length > lengthRoot)
            { // Special case root (fullpath = X:\\)
                int i = length - 1;
                while (i >= lengthRoot && !somepathexists)
                {
                    String dir = fullPath.Substring(0, i + 1);

                    if (!InternalExists(dir)) // Create only the ones missing
                        stackDir.Add(dir);
                    else
                        somepathexists = true;

                    while (i > lengthRoot && fullPath[i] != Path.DirectorySeparatorChar && fullPath[i] != Path.AltDirectorySeparatorChar) i--;
                    i--;
                }
            }

            int count = stackDir.Count;

            bool r = true;
            int firstError = 0;
            int currentError = 0;
            String errorString = path;
            // If all the security checks succeeded create all the directories
            while (stackDir.Count > 0)
            {
                String name = stackDir[stackDir.Count - 1];
                stackDir.RemoveAt(stackDir.Count - 1);
                if (IsPathTooLong(name))
                    throw new PathTooLongException("IO.PathTooLong");

                DirectoryInfo info;

                r = true;
                try
                {
                    info = Directory.CreateDirectory(name);
                }
                catch (IOException ex)
                {
                    r = false;
                    currentError = -1;
                }
                catch (Exception ex)
                {
                    r = false;
                }

                if (!r && (firstError == 0))
                {
                    if (currentError != -1)
                        firstError = -1;
                    else
                    {
                        // If there's a file in this directory's place, or if we have ERROR_ACCESS_DENIED when checking if the directory already exists throw.
                        if (File.Exists(name) || (!Directory.Exists(name)))
                        {
                            firstError = -1;
                        }
                    }
                }
                if (stackDir.Count != 0)
                    r = true;
            }

            // Only throw an exception if creating the exact directory we 
            // wanted failed to work correctly.
            if (!r && (firstError != 0))
            {
                return false;
            }

            return true;
        }

        private string baseDirectory = "C:\\TEMP";
        static private System.Threading.Thread runThread;
        private Boolean running = false;
        private Boolean runThreadStopped = true;
        private StreamWriter w;
        private int testDir = 0;
    }
}