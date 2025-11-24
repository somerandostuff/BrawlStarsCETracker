using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EventTrackerWPF
{
    /// <summary>
    /// Interaction logic for FontSelectWindow.xaml
    /// </summary>
    public partial class FontSelectWindow : Window
    {
        public FontSelectWindow()
        {
            InitializeComponent();
        }

        private void AlertWindowTopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BTN_Cancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void BTN_OK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void BTN_Close_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SoundIndexer.PlaySoundID("btn_dismiss");
            Close();
        }

        private void BTN_SingleOK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
