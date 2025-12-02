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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventTrackerWPF
{
    /// <summary>
    /// Interaction logic for TextboxWindow.xaml
    /// </summary>
    public partial class TextboxWindow : Window
    {
        public TextboxWindow(AlertTextboxMessage Message)
        {
            InitializeComponent();

            InstantiateDialog(Message);
        }


        private void InstantiateDialog(AlertTextboxMessage Message)
        {
            Txt_Caption.Text = Message.Title ?? " ";
            Txt_Content.Text = Message.Description ?? " ";
            Txt_BoxContent.Text = Message.TextboxContent ?? " ";

            BTN_SingleOK.MouseLeftButtonUp += Message.BlueButtonFunc;

            BTN_SingleOK_Text.Text = Message.BlueButton ?? " ";

            if (Message.BlueButtonCopiesTextboxContent)
            {
                BTN_SingleOK.MouseLeftButtonUp += (Co, py) =>
                {
                    Clipboard.SetText(Txt_BoxContent.Text);
                };
            }
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

        private void TextboxWindowTopBarContent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // DragMove();
        }
    }
}
