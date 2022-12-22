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
            var uri = new Uri(Directory.GetCurrentDirectory() + "./Data/Images/Champs/Aatrox.png"); // This for some reason is needed?, also Directory.GetCurrentDirectory() is needed because base dir is System32 or smthn
            ChampImage.Source = new BitmapImage(uri);
        }
        public RunePreview()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            EditButton.Visibility = Visibility.Visible;
            EquipButton.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            EditButton.Visibility = Visibility.Hidden;
            EquipButton.Visibility = Visibility.Hidden;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EquipButton_Click(object sender, RoutedEventArgs e)
        {
            Utils.EquipPage();
        }
    }
}
