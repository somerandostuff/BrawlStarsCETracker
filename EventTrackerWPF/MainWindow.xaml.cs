using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;
using System.Windows.Media;
using System.Globalization;
using EventTrackerWPF.Librarbies;
using System.Media;
using EventTrackerWPF.CustomElements;
using System.Windows.Media.Animation;

namespace EventTrackerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Timers.Timer TickDisplayer = new System.Timers.Timer(16 + 2/3);
        private SoundLibrarby SoundIndexer = new SoundLibrarby();

        private Settings Settings = new Settings();

        double Count = 0;
        double CountDisp = 0;

        double TestTime = 0;
        double TestTimeDisp = 0;

        public MainWindow()
        {
            InitializeComponent();

            TickDisplayer.Elapsed += TickDisplayer_Tick;
            TickDisplayer.AutoReset = true;
            TickDisplayer.Start();

            SoundIndexer.LoadSounds(new Dictionary<string, string>
            {
                { "YOUR_TAKING_TOO_LONG", "SFX/YOUR_TAKING_TOO_LONG.wav" },
                { "jackenlaugh", "SFX/jack_o_lantern_laugh.wav" },
                { "btn_click", "SFX/menu_decision.wav" }
            });

            Settings.Load();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MarqueeEffect.StartMarquee(Txt_SplashMarquee, ElemWidth: Txt_SplashMarquee.ActualWidth, SpeedSeconds: 20);
        }

        private void ChangeUIBarSize(BarType BarType, double Length)
        {
            var BarOffset = GetBarOffset(BarType);
            switch (BarType)
            {
                case BarType.None:
                default:
                    break;
                case BarType.Tracker:
                    {
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
                    break;
                case BarType.TimeLeft:
                    {
                        if (Length <= 0)
                        {
                            Length = 0;
                            DisableAllElemsInGrid(TimeBarFront);
                        }
                        else EnableAllElemsInGrid(TimeBarFront);

                        if (Length > 400) Length = 400;

                        if (Length <= 398 && Length > 0)
                        {
                            Img_ProgressiveStrike.Visibility = Visibility.Visible;
                        }
                        else Img_ProgressiveStrike.Visibility = Visibility.Hidden;

                        Img_ActiveTimeBarMid.Width = Length;
                        Img_ActiveTimeBarRight.Margin = new Thickness(BarOffset + Length, Img_ActiveTimeBarRight.Margin.Top, Img_ActiveTimeBarRight.Margin.Right, Img_ActiveTimeBarRight.Margin.Bottom);
                        Img_ProgressiveStrike.Margin = new Thickness(BarOffset - 2 + Length, Img_ProgressiveStrike.Margin.Top, Img_ProgressiveStrike.Margin.Right, Img_ProgressiveStrike.Margin.Bottom);
                    }
                    break;
            }
        }

        private double GetBarOffset(BarType BarType)
        {
            double BarOffset = 0;
            switch (BarType)
            {
                case BarType.None:
                default:
                    break;
                case BarType.Tracker:
                    BarOffset = Img_ActiveBarLeft.Margin.Left + Img_ActiveBarLeft.Width - 2;
                    break;
                case BarType.TimeLeft:
                    BarOffset = Img_ActiveTimeBarLeft.Margin.Left + Img_ActiveTimeBarLeft.Width - 3;
                    break;
            }
            return BarOffset;
        }

        private void BTN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChangeUIBarSize(BarType.Tracker, Convert.ToDouble(Tb_TextboxBarMid.Text));
        }

        private void BTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
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
            TestTime += 1;
            SoundIndexer.PlaySoundID("btn_click");
        }

        private void TickDisplayer_Tick(object? Sender, EventArgs Event)
        {
            CalculateDisplay();

            Dispatcher.Invoke(() =>
            {
                L_Count.Text = CountDisp.ToString("#,##0");
                ChangeUIBarSize(BarType.Tracker, CountDisp / 100);
                ChangeUIBarSize(BarType.TimeLeft, TestTimeDisp / 1);
            });
        }

        private void CalculateDisplay()
        {
            CountDisp += (Count - CountDisp) * .3;
            TestTimeDisp += (TestTime - TestTimeDisp) * .3;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Save();
        }

        private void Teststs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid_BTN_UpdateCount.IsEnabled = (Grid_BTN_UpdateCount.IsEnabled ? false : true);
            SoundIndexer.PlaySoundID("btn_click");
        }
    }

    public enum BarType
    {
        None,
        Tracker,
        TimeLeft
    }
}
