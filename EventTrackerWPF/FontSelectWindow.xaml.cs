using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace EventTrackerWPF
{
    /// <summary>
    /// Interaction logic for FontSelectWindow.xaml
    /// </summary>
    public partial class FontSelectWindow : Window
    {
        private List<string> FontNames = new List<string>();

        public FontSelectWindow()
        {
            InitializeComponent();
        }

        private void AlertWindowTopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // DragMove();
        }

        private void BTN_Cancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SoundIndexer.PlaySoundID("btn_click");
            Close();
        }

        private void BTN_OK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.SoundIndexer.PlaySoundID("btn_click");
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var SystemFonts = Fonts.SystemFontFamilies;
            foreach (FontFamily Font in SystemFonts)
            {
                FontNames.Add(Font.Source);
            }
        }
    }
}
