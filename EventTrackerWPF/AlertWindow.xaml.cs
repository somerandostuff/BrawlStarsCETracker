using EventTrackerWPF.Librarbies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventTrackerWPF
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
        public AlertWindow(AlertMessage Message)
        {
            InitializeComponent();

            // Common.UseCustomFont(MainGrid, Settings.MainFontFamily, Settings.AltFontFamily);

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InstantiateDialog(Message);
        }

        private void AlertWindowTopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void InstantiateDialog(AlertMessage Message)
        {
            Txt_Caption.Text = Message.Title ?? " ";
            Txt_Content.Text = Message.Description ?? " ";

            if (string.IsNullOrWhiteSpace(Message.RedButton))
            {
                ShowSingleButton();
                BTN_SingleOK.MouseLeftButtonUp += Message.BlueButtonFunc;

                BTN_SingleOK_Text.Text = Message.BlueButton ?? " ";
            }
            else
            {
                ShowYesNoButtons();
                BTN_Cancel.MouseLeftButtonUp += Message.RedButtonFunc;
                BTN_OK.MouseLeftButtonUp += Message.BlueButtonFunc;

                BTN_Cancel_Text.Text = Message.RedButton ?? " ";
                BTN_OK_Text.Text = Message.BlueButton ?? " ";
            }
        }

        private void ShowYesNoButtons()
        {
            AlertWindowButtons.Visibility = Visibility.Visible;
            AlertWindowButtonSingle.Visibility = Visibility.Hidden;
        }

        private void ShowSingleButton()
        {
            AlertWindowButtons.Visibility = Visibility.Hidden;
            AlertWindowButtonSingle.Visibility = Visibility.Visible;
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
