using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace YesingRunes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var processes = Process.GetProcessesByName("YesingRunes");
            if (processes.Length > 1) Environment.Exit(0);
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "./Data") ||
                !File.Exists(Directory.GetCurrentDirectory() + "./Data/version.json"))
            {
                Process buildTools = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = Directory.GetCurrentDirectory() + "./BuildTools.exe"
                    }
                };
                buildTools.Start();
                buildTools.WaitForExit();
            }
        }
    }
}
