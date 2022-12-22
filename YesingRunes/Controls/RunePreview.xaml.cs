using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YesingRunes.Models;

namespace YesingRunes.Controls
{
    /// <summary>
    /// Interaction logic for RunePreview.xaml
    /// </summary>
    public partial class RunePreview : UserControl
    {
        YesingRunePage RunePage;
        public RunePreview(YesingRunePage runePage)
        {
            RunePage = runePage;

            InitializeComponent();

            NameTextBlock.Text = runePage.Name;
            var uri = new Uri(Directory.GetCurrentDirectory() + "./Images/Champs/Aatrox.png"); // This for some reason is needed?, also Directory.GetCurrentDirectory() is needed because base dir is System32 or smthn
            ChampImage.Source = new BitmapImage(uri);
        }
        public RunePreview()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
