using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EventTrackerWPF.CustomElements
{
    public class CheckboxAsImage : Grid
    {
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(CheckboxAsImage),
                new PropertyMetadata(null, OnAnyPropertyChanged));

        public ImageSource ImageSource { get => (ImageSource)GetValue(ImageSourceProperty); set => SetValue(ImageSourceProperty, value); }

        public static readonly DependencyProperty ImageSourceIfUncheckedProperty =
            DependencyProperty.Register(nameof(ImageSourceIfUnchecked), typeof(ImageSource), typeof(CheckboxAsImage),
                new PropertyMetadata(null, OnAnyPropertyChanged));

        public ImageSource ImageSourceIfUnchecked { get => (ImageSource)GetValue(ImageSourceIfUncheckedProperty); set => SetValue(ImageSourceIfUncheckedProperty, value); }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(CheckboxAsImage),
                new PropertyMetadata(false, OnAnyPropertyChanged));

        public bool IsChecked { get => (bool)GetValue(IsCheckedProperty); set => SetValue(IsCheckedProperty, value); }

        public CheckboxAsImage()
        {
            MouseLeftButtonUp += OnMouseLeftButtonUp;
            UpdateBgImage();
        }

        private void OnMouseLeftButtonUp(object Sender, MouseButtonEventArgs Event)
        {
            // "Is checked equals NOT Is checked"
            IsChecked = !IsChecked;
        }

        private static void OnAnyPropertyChanged(DependencyObject Dependency, DependencyPropertyChangedEventArgs Event)
        {
            if (Dependency is CheckboxAsImage Button)
            {
                Button.UpdateBgImage();
            }
        }

        private void UpdateBgImage()
        {
            var Image = IsChecked ? ImageSource : ImageSourceIfUnchecked;
            Background = Image != null ? new ImageBrush(Image) { Stretch = Stretch.Uniform } : null;
        }
    }
}
