using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Threading;
using MaterialSkin;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace AccomRepo
{
    public partial class mainForm : MaterialSkin.Controls.MaterialForm
    {
        public ChromiumWebBrowser chromeBrowser;
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        
        public mainForm()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(2000);

            InitializeComponent();
            InitializeChromium();

            t.Abort();
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component`
            chromeBrowser = new ChromiumWebBrowser("http://192.168.1.106:8001/");
            // Add it to the form and fill it to the form window.
            this.panel1.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

            chromeBrowser.MenuHandler = new MyCustomMenuHandler();

            DownloadHandler downloadHandler = new DownloadHandler();
            chromeBrowser.DownloadHandler = downloadHandler;
        }

        public void StartForm()
        {
            try
            {
                SplashScreen frm = new SplashScreen();

                Application.Run(frm);
            }
            catch (ThreadAbortException ex)
            {
                Thread.ResetAbort();
            }
        }

    }
}
