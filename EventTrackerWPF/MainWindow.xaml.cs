using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;
using System.Windows.Media;
using System.Globalization;
using EventTrackerWPF.Librarbies;

namespace EventTrackerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Timers.Timer TickDisplayer = new System.Timers.Timer(16 + 2/3);

        double BarOffset = 0;

        double Count = 0;
        double CountDisp = 0;

        public MainWindow()
        {
            InitializeComponent();

            TickDisplayer.Elapsed += TickDisplayer_Tick;
            TickDisplayer.AutoReset = true;
            TickDisplayer.Start();
        }

        private void ChangeUIBarSize(double Length)
        {
            GetBarOffset();

            if (Length <= 0)
            {
                Length = 0;
                DisableAllElemsInGrid(ProgBarFront);
            }
            else EnableAllElemsInGrid(ProgBarFront);

            if (Length > 1000) Length = 1000;

            Img_ActiveBarMid.Width = Length;
            Img_ActiveBarRight.Margin = new Thickness(BarOffset + Length, Img_ActiveBarRight.Margin.Top, Img_ActiveBarRight.Margin.Right, Img_ActiveBarRight.Margin.Bottom);
        }

        private void GetBarOffset()
        {
            if (BarOffset <= 0)
            {
                BarOffset = Img_ActiveBarLeft.Margin.Left + Img_ActiveBarLeft.Width - 5;
            }
        }

        private void BTN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChangeUIBarSize(Convert.ToDouble(Tb_TextboxBarMid.Text));
        }

        private void BTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AnimateButtonGrid(Grid_BTN1);
        }

        // ONE DAY i will make this work.
        private void AnimateButtonGrid(Grid GridArea)
        {
            foreach (UIElement Element in GridArea.Children)
            {
                
            }
        }
        private void DisableAllElemsInGrid(Grid GridArea)
        {
            foreach (UIElement Element in GridArea.Children)
            {
                Element.Visibility = Visibility.Hidden;
            }
        }
        private void EnableAllElemsInGrid(Grid GridArea)
        {
            foreach (UIElement Element in GridArea.Children)
            {
                Element.Visibility = Visibility.Visible;
            }
        }

        private void Grid_BTN_UpdateCount_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_BTN_UpdateCount_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Count += Random.Shared.Next(0, 100);
        }

        private void TickDisplayer_Tick(object? Sender, EventArgs Event)
        {
            CalculateDisplay();

            Dispatcher.Invoke(() =>
            {
                L_Count.Text = CountDisp.ToString("#,##0");
                ChangeUIBarSize(CountDisp / 100);
            });
        }

        private void CalculateDisplay()
        {
            CountDisp += (Count - CountDisp) * .3;
        }

        private void DrawTextShakyOutlined_Initialized(object sender, EventArgs e)
        {

        }
    }
}
