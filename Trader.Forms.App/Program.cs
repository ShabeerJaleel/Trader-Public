using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trader.Forms.App
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            ServicePointManager.DefaultConnectionLimit = 1000;
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            
            Application.Run(new MainForm());
        }
    }
}
