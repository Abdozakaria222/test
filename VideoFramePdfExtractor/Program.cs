using System;
using System.Windows.Forms;

namespace VideoFramePdfExtractor
{
    internal static class Program
    {
        /// <summary>
        ///  Main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
