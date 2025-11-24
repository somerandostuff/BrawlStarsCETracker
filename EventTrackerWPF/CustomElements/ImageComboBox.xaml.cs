using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace EventTrackerWPF.CustomElements
{
    /// <summary>
    /// Interaction logic for ImageComboBox.xaml
    /// </summary>
    public partial class ImageComboBox : UserControl
    {
        private ICollectionView? ItemsView;

        public ImageComboBox()
        {
            InitializeComponent();
            PART_TextBox.TextChanged += PART_TextBox_TextChanged;
            Loaded += ImageComboBox_Loaded;
        }

        private void ImageComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshCollectionView();
        }

        #region Properties

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(ImageComboBox),
                new PropertyMetadata(null, OnItemsSourceChanged));

        public IEnumerable? ItemsSource { get => (IEnumerable?)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

        private static void OnItemsSourceChanged(DependencyObject Obj, DependencyPropertyChangedEventArgs Event)
        {
            var Control = (ImageComboBox)Obj;
            Control.PART_Combo.ItemsSource = (IEnumerable?)Event.NewValue;
            Control.RefreshCollectionView();
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(ImageComboBox),
                new PropertyMetadata(null, OnSelectedItemChanged));

        public object? SelectedItem { get => GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }
        
        private static void OnSelectedItemChanged(DependencyObject Obj, DependencyPropertyChangedEventArgs Event)
        {
            var Control = (ImageComboBox)Obj;
            if (Event.NewValue != null)
            {
                Control.PART_TextBox.Text = Event.NewValue.ToString() ?? string.Empty;
            }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ImageComboBox),
                new PropertyMetadata(string.Empty));

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

        public static readonly DependencyProperty ImageForTextBoxProperty =
            DependencyProperty.Register(nameof(ImageForTextBox), typeof(ImageSource), typeof(ImageComboBox),
                new PropertyMetadata(null));

        public ImageSource? ImageForTextBox { get => (ImageSource?)GetValue(ImageForTextBoxProperty); set => SetValue(ImageForTextBoxProperty, value); }

        public static readonly DependencyProperty ButtonImageProperty =
            DependencyProperty.Register(nameof(ButtonImage), typeof(ImageSource), typeof(ImageComboBox),
                new PropertyMetadata(null));

        public ImageSource? ButtonImage { get => (ImageSource?)GetValue(ButtonImageProperty); set => SetValue(ButtonImageProperty, value); }

        #endregion

        private void RefreshCollectionView()
        {
            if (ItemsSource == null)
            { 
                ItemsView = null!;
                return;
            }

            ItemsView = CollectionViewSource.GetDefaultView(ItemsSource);
            if (ItemsView != null)
                ItemsView.Filter = FilterPredicate;
        }

        private bool FilterPredicate(object Item)
        {
            if (string.IsNullOrWhiteSpace(PART_TextBox.Text))
                return true;

            if (Item == null) return false;

            var Text = PART_TextBox.Text;

            return Item.ToString()?.IndexOf(Text, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        private void PART_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = PART_TextBox.Text;
            ItemsView?.Refresh();
        }

        private void PART_Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RefreshCollectionView();

            PART_Combo.Visibility = Visibility.Visible;

            PART_Combo.IsDropDownOpen = !PART_Combo.IsDropDownOpen;

            if (PART_Combo.IsDropDownOpen)
            {
                PART_TextBox.Focus();
            }
        }

        private void PART_Combo_DropDownClosed(object sender, EventArgs e)
        {
            PART_Combo.Visibility = Visibility.Collapsed;
        }

        private void Part_Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PART_Combo.SelectedItem != null)
            {
                SelectedItem = PART_Combo.SelectedItem;
                PART_TextBox.Text = SelectedItem.ToString() ?? string.Empty;
                PART_Combo.IsDropDownOpen = false;
            }
        }
    }
}
