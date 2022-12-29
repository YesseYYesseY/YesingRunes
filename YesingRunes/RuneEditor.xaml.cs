using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YesingRunes.Models;

namespace YesingRunes
{
    /// <summary>
    /// Interaction logic for RuneEditor.xaml
    /// </summary>
    public partial class RuneEditor : Window
    {
        YesingRunePage Page;
        int selectedLeftPath;
        int selectedRightPath;
        public RuneEditor(YesingRunePage page)
        {
            Page = page;
            InitializeComponent();
            SetLeftPath(page.PrimaryRunePath);
        }

        void SetLeftPath(int pathId)
        {
            selectedLeftPath = pathId;
        }
    }
}
