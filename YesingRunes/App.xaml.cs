using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
            if(File.Exists(Directory.GetCurrentDirectory() + "./Data/version.json"))
            {
                var client = new HttpClient();
                var res = client.Send(new HttpRequestMessage(HttpMethod.Get, "https://ddragon.leagueoflegends.com/api/versions.json"));
                var versions = JsonSerializer.Deserialize<string[]>(res.Content.ReadAsStringAsync().Result);
                if(versions is not null && versions.Length > 0)
                {
                    if(File.ReadAllText(Directory.GetCurrentDirectory() + "./Data/version.json") == versions[0])
                    {
                        return;
                    }
                }
            }

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
