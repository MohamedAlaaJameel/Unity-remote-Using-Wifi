using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unity_remote_Using_Wifi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private  async void runProgBTN_Click(object sender, EventArgs e)
        {
            bool isread = false;
            string ip = string.Empty;
            richTextBox1.Clear();

            ADB("disconnect");

            Process proc = ADB("shell ifconfig wlan0");
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                if (line.Contains("inet addr:"))
                {
                    isread = true;
                       ip = line.Trim().Remove(0, "inet addr:".Length).Split(' ').First();
                    richTextBox1.Text+=("your phone IP= "+ip)+"\n";
                    break;
                }
            }
            if (!isread)
            {
                richTextBox1.Clear();
                richTextBox1.Text = "Error";
                MessageBox.Show("Attach USB cable , Enable USB debugging, Connect to the same WIFI.");
                return;
            }
            ADB("tcpip 5555");
            await Task.Delay(3000);
            proc=ADB("connect "+ip);
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                richTextBox1.Text+=(line) + "\n"; ;
            }
            proc =ADB("devices");
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                richTextBox1.Text+=(line) + "\n"; ;
            }

            //
        }

        private static Process ADB(string args)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ADB.exe",
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            return proc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string link = @"https://www.linkedin.com/in/mohamedalaa8/";
            Process.Start(new ProcessStartInfo("cmd", "/c start " + link) { CreateNoWindow = true, UseShellExecute = false });
            label1.Text = "mohmmeuud@gmail.com";
        }

       
    }
}
