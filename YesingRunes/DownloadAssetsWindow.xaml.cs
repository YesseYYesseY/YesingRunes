using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YesingRunes
{
    /// <summary>
    /// Interaction logic for DownloadAssetsWindow.xaml
    /// </summary>
    public partial class DownloadAssetsWindow : Window
    {
        public DownloadAssetsWindow()
        {
            InitializeComponent();

            LoadLangs();
        }
        public DownloadAssetsWindow(string newVersion, bool forcedYes)
        {
            InitializeComponent();

            LoadLangs();
            if(forcedYes)
            {
                DeclineButton.IsEnabled = false;

                UpdateText.Text = "Do you want to download the latest data from League of Legends? (Required to useYesingRunes)";
            }
            VersionText.Text = newVersion;
        }

        void LoadLangs()
        {
            using (var client = new HttpClient())
            {
                var req = new HttpRequestMessage(HttpMethod.Get, "https://ddragon.leagueoflegends.com/cdn/languages.json");
                var res = client.Send(req);

                var langs = JsonSerializer.Deserialize<string[]>(res.Content.ReadAsStringAsync().Result);
                if (langs is not null)
                {
                    foreach (var lang in langs)
                    {
                        LangCombo.Items.Add(new TextBlock()
                        {
                            Text = Utils.TranslateLang.ContainsKey(lang) ? Utils.TranslateLang[lang] : lang,
                            Name = lang,
                            FontSize = 20
                        });
                    }
                }
            }
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            var sndr = sender as Button;
            if(sndr is not null)
            {
                sndr.IsEnabled = false;
                var Selected = LangCombo.SelectedItem as TextBlock;

                if (Selected is not null)
                {
                    Process buildTools = new Process()
                    {
                        StartInfo = new ProcessStartInfo()
                        {
                            FileName = Directory.GetCurrentDirectory() + "./BuildTools.exe",
                            CreateNoWindow = true,
                            ArgumentList = {
                                (Selected).Name
                            }
                        }
                    };

                    buildTools.Start();
                    buildTools.WaitForExit();
                    this.Close();
                }

            }
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
