using EventTrackerWPF.CustomElements;
using EventTrackerWPF.Librarbies;
using System.Diagnostics;
using System.Globalization;
using System.Media;
using System.Net.Security;
using System.Text.Json;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using SysTimer = System.Timers;

namespace EventTrackerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SysTimer.Timer TickDisplayer = new SysTimer.Timer(1000 / 60);
        private readonly SysTimer.Timer TickerPerSecond = new SysTimer.Timer(125);
        public static SoundLibrarby SoundIndexer = new SoundLibrarby();

        List<Storyboard> RunningStoryboards = [];
        List<Image> RunningImages = [];

        Stack<Grid> NavigatedMenus = [];

        public static Settings Settings = new Settings();
        public static SaveSystem SaveSystem = new SaveSystem();

        double Count = 0;
        double CountDisp = 0;

        double TestTime = 0;
        double TestTimeDisp = 0;

        long Gems = 0;
        double GemsDisp = 0;

        long StartupUnixTime = DateTimeOffset.Now.ToUnixTimeSeconds();

        public MainWindow()
        {
            LocalizationLib.Locales = LocalizationLib.LoadLocales();
            LocalizationLib.Load(LocalizationLib.Locales.Single(Q => Q.LocaleID == Settings.Lang).FilePath!);

            InitializeComponent();

            TickDisplayer.Elapsed += TickDisplayer_Tick;
            TickDisplayer.AutoReset = true;
            TickDisplayer.Start();

            TickerPerSecond.Elapsed += TickerPerSecond_Tick;
            TickerPerSecond.AutoReset = true;
            TickerPerSecond.Start();

            SoundIndexer.LoadSounds(new Dictionary<string, string>
            {
                { "YOUR_TAKING_TOO_LONG", "SFX/YOUR_TAKING_TOO_LONG.wav" },
                { "jackenlaugh", "SFX/jack_o_lantern_laugh.wav" },
                { "btn_click_oneshot", "SFX/menu_decision.wav" },
                { "btn_click", "SFX/menu_click_08.wav" },
                { "btn_go_back", "SFX/menu_go_back_01.wav" },
                { "btn_dismiss", "SFX/menu_dismiss_01.wav" },
                { "lancer", "SFX/snd_splat.wav" }
            });

            Settings.Load();
            SaveSystem.Load();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // MarqueeEffect.StartMarquee(Txt_SplashMarquee, ElemWidth: Txt_SplashMarquee.ActualWidth, SpeedSeconds: 20);

            Gems = SaveSystem.Gems;

            if (Settings.SelectedTheme == 0)
            {
                ThemeSelect(BackgroundThemes.Default);
            }
            else
            {
                ThemeSelect(Settings.SelectedTheme);
            }

            Chk_EnableAnimations.IsChecked = Settings.EnableAnimations;
        }

        private void ThemeSelect(BackgroundThemes ThemeID)
        {
            StopAllThemeAnimations();
            foreach (Grid ThemeArea in ThemesGallery.Children)
            {
                ThemeArea.Visibility = Visibility.Collapsed;
            }
            switch (ThemeID)
            {
                case BackgroundThemes.Default:
                    {
                        BasicThemeGrid.Visibility = Visibility.Visible;
                    }
                    break;
                case BackgroundThemes.Brawloween2023:
                    {
                        BrawloweenThemeGrid.Visibility = Visibility.Visible;
                    }
                    break;
                case BackgroundThemes.Angels:
                    {
                        AvD_AngelsThemeGrid.Visibility = Visibility.Visible;
                    }
                    break;
                default:
                    break;
            }
            if (Settings.EnableAnimations == true)
            {
                switch (ThemeID)
                {
                    case BackgroundThemes.Default:
                        {
                            BasicAnimsStart();
                        }
                        break;
                    case BackgroundThemes.Brawloween2023:
                        {
                            Brawloween2025AnimsStart();
                        }
                        break;
                    case BackgroundThemes.Angels:
                        {
                            AvD_Angels_AnimsStart();
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        private void ChangeUIBarSize(BarType BarType, double Length)
        {
            // var BarOffset = GetBarOffset(BarType);
            // switch (BarType)
            // {
            //     case BarType.None:
            //     default:
            //         break;
            //     case BarType.Tracker:
            //         {
            //             if (Length <= 0)
            //             {
            //                 Length = 0;
            //                 DisableAllElemsInGrid(ProgBarFront);
            //             }
            //             else EnableAllElemsInGrid(ProgBarFront);
               
            //             if (Length > 1000) Length = 1000;
               
            //             Img_ActiveBarMid.Width = Length;
            //             Img_ActiveBarRight.Margin = new Thickness(BarOffset + Length, Img_ActiveBarRight.Margin.Top, Img_ActiveBarRight.Margin.Right, Img_ActiveBarRight.Margin.Bottom);
            //         }
            //         break;
            //     case BarType.TimeLeft:
            //         {
            //             if (Length <= 0)
            //             {
            //                 Length = 0;
            //                 DisableAllElemsInGrid(TimeBarFront);
            //             }
            //             else EnableAllElemsInGrid(TimeBarFront);
               
            //             if (Length > 400) Length = 400;
               
            //             if (Length <= 398 && Length > 0)
            //             {
            //                 Img_ProgressiveStrike.Visibility = Visibility.Visible;
            //             }
            //             else Img_ProgressiveStrike.Visibility = Visibility.Hidden;
               
            //             Img_ActiveTimeBarMid.Width = Length;
            //             Img_ActiveTimeBarRight.Margin = new Thickness(BarOffset + Length, Img_ActiveTimeBarRight.Margin.Top, Img_ActiveTimeBarRight.Margin.Right, Img_ActiveTimeBarRight.Margin.Bottom);
            //             Img_ProgressiveStrike.Margin = new Thickness(BarOffset - 2 + Length, Img_ProgressiveStrike.Margin.Top, Img_ProgressiveStrike.Margin.Right, Img_ProgressiveStrike.Margin.Bottom);
            //         }
            //         break;
            // }
        }

        private double GetBarOffset(BarType BarType)
        {
            // double BarOffset = 0;
            // switch (BarType)
            // {
            //     case BarType.None:
            //     default:
            //         break;
            //     case BarType.Tracker:
            //         BarOffset = Img_ActiveBarLeft.Margin.Left + Img_ActiveBarLeft.Width - 2;
            //         break;
            //     case BarType.TimeLeft:
            //         BarOffset = Img_ActiveTimeBarLeft.Margin.Left + Img_ActiveTimeBarLeft.Width - 3;
            //         break;
            // }
            // return BarOffset;
            return 0;
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

        private void TickDisplayer_Tick(object? Sender, EventArgs Event)
        {
            CalculateDisplay();

            // This will be for UI-only updates (like displaying numbers and such)
            Dispatcher.Invoke(() =>
            {
                MoneyCount.Text = GemsDisp.ToString("#,##0");
                DynCounter_Num.Text = CountDisp.ToString("#,##0");
                UpdateDynCounterPos();
            });
        }

        private void TickerPerSecond_Tick(object? Sender, ElapsedEventArgs Event)
        {
            Dispatcher.Invoke(() =>
            {
                ApplicationUptime.Text = "Uptime: " + TimeSpan.FromSeconds(DateTimeOffset.Now.ToUnixTimeSeconds() - StartupUnixTime);
            });
        }

        private void CalculateDisplay()
        {
            CountDisp += (Count - CountDisp) * .3;
            TestTimeDisp += (TestTime - TestTimeDisp) * .3;
            GemsDisp += (Gems - GemsDisp) * .125;
        }

        private void UpdateDynCounterPos()
        {
            var TextSize = DynCounter_Num.MeasureTextSize();

            double MinImageX = DynCounter_Img.Width;
            double MaxImageX = (DynCounter.Width + DynCounter_Img.Width) / 2;

            double MaxNumberX = DynCounter.Width - MinImageX;

            double Offset = TextSize.Width / 2;

            DynCounter_ImgGrid.Width = Math.Max(MinImageX, MaxImageX - Offset);
            DynCounter_NumGrid.Width = Math.Min(DynCounter.Width - DynCounter_ImgGrid.Width, MaxNumberX);
        }

        #region Controls

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Save();

            SaveSystem.Gems = Gems;
            SaveSystem.Save();
        }

        private void GridButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            var Message = new AlertMessage()
            {
                Title = "This is a dialog box",
                Description = "Lorem ipsum dolor sit amet etc etc etc...",
                RedButton = "Pipis",
                BlueButton = "Bepis",

                RedButtonFunc = (LAnc, Er) => { SoundIndexer.PlaySoundID("lancer"); },
                BlueButtonFunc = (Ni, ko) => { SoundIndexer.PlaySoundID("btn_click"); }
            };
            Common.CreateAlert(Message);
        }

        private void UptimeButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            var Message = new AlertMessage()
            {
                Title = "Uptime timer",
                Description = "This shows how long you have left the application running on your computer.\nIt's pretty self explainatory to be honest...",
                BlueButton = "OK",

                BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); }
            };
            Common.CreateAlert(Message);
        }
        private void BTN_GoHome(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_dismiss");
            GoHome();
        }

        private void BTN_Settings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            NavigateTo(SettingsUI);
        }
        private void BTN_GoBack(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_go_back");
            GoBack();
        }

        private void Chk_EnableAnimations_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            Settings.EnableAnimations = Chk_EnableAnimations.IsChecked;
            ThemeSelect(Settings.SelectedTheme);
        }

        private void BTN_ChangeTheme_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            NavigateTo(ThemeSelectorUI);
        }

        private void BTN_ThemeSelectorUI_Brawloween2023_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            ThemeSelect(BackgroundThemes.Brawloween2023);
            Settings.SelectedTheme = BackgroundThemes.Brawloween2023;
            GoBack();
        }

        private void BTN_ThemeSelectorUI_Basic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            ThemeSelect(BackgroundThemes.Default);
            Settings.SelectedTheme = BackgroundThemes.Default;
            GoBack();
        }

        private void BTN_ThemeSelectorUI_Angels_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            if (Settings.EnableAnimations)
            {
                var Message = new AlertMessage()
                {
                    Title = LocalizationLib.Strings["TID_PERFORMANCE_HEAVY_WARNING_TITLE"],
                    Description = LocalizationLib.Strings["TID_PERFORMANCE_HEAVY_WARNING_DESC"],
                    RedButton = LocalizationLib.Strings["TID_PERFORMANCE_HEAVY_WARNING_CANCEL"],
                    BlueButton = LocalizationLib.Strings["TID_PERFORMANCE_HEAVY_WARNING_PROCEED"],

                    RedButtonFunc = (No, pe) => { SoundIndexer.PlaySoundID("btn_click"); },
                    BlueButtonFunc = (An, gel) => { ThemeSelect(BackgroundThemes.Angels); Settings.SelectedTheme = BackgroundThemes.Angels; SoundIndexer.PlaySoundID("btn_click"); GoBack(); }
                };
                Common.CreateAlert(Message);
            }
            else
            {
                ThemeSelect(BackgroundThemes.Angels);
                Settings.SelectedTheme = BackgroundThemes.Angels;
                SoundIndexer.PlaySoundID("btn_click");
                GoBack();
            }
        }

        private void MoneyUI_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var Message = new AlertMessage()
            {
                Title = "WOW...",
                Description = "Unnecessary bloat...",

                RedButton = "Set gem to 0",
                RedButtonFunc = (Re, set) => { Gems = 0; SoundIndexer.PlaySoundID("btn_click"); },

                BlueButton = "Free gems",
                BlueButtonFunc = (Ba, lls) => { Gems += Random.Shared.Next(1, 184); Count += Random.Shared.NextInt64(0, 1000000000000); SoundIndexer.PlaySoundID("btn_click"); SaveSystem.Save(); }
            };

            SoundIndexer.PlaySoundID("btn_click");
            Common.CreateAlert(Message);
        }
        private void BTN_About_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            NavigateTo(AboutUI);
        }

        #endregion

        #region Basic Nav
        private void NavigateTo(Grid UIName)
        {
            if (NavigatedMenus.Count == 0)
            {
                MainMenuUI.Visibility = Visibility.Collapsed;
            }
            else
            {
                NavigatedMenus.Last().Visibility = Visibility.Collapsed;
            }
            UIName.Visibility = Visibility.Visible;
            NavButtons.Visibility = Visibility.Visible;
            NavigatedMenus.Push(UIName);
        }

        private void GoBack()
        {
            NavigatedMenus.Pop().Visibility = Visibility.Collapsed;
            if (NavigatedMenus.Count == 0)
            {
                MainMenuUI.Visibility = Visibility.Visible;
                NavButtons.Visibility = Visibility.Collapsed;
            }
            else NavigatedMenus.Last().Visibility = Visibility.Visible;
        }

        private void GoHome()
        {
            foreach (Grid UI in UserInterfaces.Children)
            {
                if (UI != MainMenuUI)
                {
                    UI.Visibility = Visibility.Collapsed;
                }
            }
            NavigatedMenus.Clear();
            NavButtons.Visibility = Visibility.Collapsed;
            MainMenuUI.Visibility = Visibility.Visible;
        }
        #endregion

        #region Theme Animations
        private void StopAllThemeAnimations()
        {
            foreach (var Storyboard in RunningStoryboards)
            {
                Storyboard.Stop();
            }
            RunningStoryboards.Clear();
            RunningImages.Clear();
        }
        private void Brawloween2025AnimsStart()
        {
            var Storyboard = (Storyboard)FindResource("BrawloweenTheme");
            Storyboard.Begin();

            RunningStoryboards.Add(Storyboard);
        }
        private void BasicAnimsStart()
        {
            var Storyboard = (Storyboard)FindResource("BasicTheme");
            Storyboard.Begin();

            RunningStoryboards.Add(Storyboard);
        }
        private void AvD_Angels_AnimsStart()
        {
            var StoryboardBaseRightClouds = (Storyboard)Resources["AvD_AngelsTheme_RightmostClouds"];
            var StoryboardBaseLeftClouds = (Storyboard)Resources["AvD_AngelsTheme_LeftmostClouds"];
            var StoryboardBaseBottomHighClouds = (Storyboard)Resources["AvD_AngelsTheme_BottomHighClouds"];
            var StoryboardIcons = (Storyboard)FindResource("AvD_AngelsTheme_Icons");

            RunningStoryboards.Clear();
            RunningImages.Clear();

            StoryboardIcons.Begin();
            RunningStoryboards.Add(StoryboardIcons);

            foreach (UIElement Target in RightmostClouds.Children)
            {
                if (Target is Image TargetImg)
                {
                    if (TargetImg.RenderTransform is not TransformGroup AnimGroup ||
                        AnimGroup.Children.Count < 2 ||
                        AnimGroup.Children[0] is not ScaleTransform ||
                        AnimGroup.Children[1] is not TranslateTransform)
                    {
                        var NewAnimGroup = new TransformGroup();
                        NewAnimGroup.Children.Add(new ScaleTransform());
                        NewAnimGroup.Children.Add(new TranslateTransform());

                        TargetImg.RenderTransform = NewAnimGroup;
                        TargetImg.RenderTransformOrigin = new Point(0, 0);
                    }

                    var Storyboard = StoryboardBaseRightClouds.Clone();

                    foreach (Timeline Tl in Storyboard.Children)
                    {
                        Storyboard.SetTarget(Tl, TargetImg);
                    }
                    Storyboard.Begin();

                    RunningStoryboards.Add(Storyboard);
                    RunningImages.Add(TargetImg);
                }
            }

            foreach (UIElement Target in LeftmostClouds.Children)
            {
                if (Target is Image TargetImg)
                {
                    if (TargetImg.RenderTransform is not ScaleTransform)
                        TargetImg.RenderTransform = new ScaleTransform();

                    var Storyboard = StoryboardBaseLeftClouds.Clone();

                    foreach (Timeline Tl in Storyboard.Children)
                    {
                        Storyboard.SetTarget(Tl, TargetImg);
                    }

                    Storyboard.Begin();

                    RunningStoryboards.Add(Storyboard);
                    RunningImages.Add(TargetImg);
                }
            }

            foreach (UIElement Target in BottomHighClouds.Children)
            {
                if (Target is Image TargetImg)
                {
                    if (TargetImg.RenderTransform is not TranslateTransform)
                        TargetImg.RenderTransform = new TranslateTransform();

                    var Storyboard = StoryboardBaseBottomHighClouds.Clone();

                    foreach (Timeline Tl in Storyboard.Children)
                    {
                        Storyboard.SetTarget(Tl, TargetImg);
                    }

                    Storyboard.Begin();

                    RunningStoryboards.Add(Storyboard);
                    RunningImages.Add(TargetImg);
                }
            }
        }
        #endregion

        private void BTN_GoToFanPolicyLink_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string URL = LocalizationLib.Strings["TID_DISCLAIMER_FAN_CONTENT_POLICY_LINK"];
            Process.Start(new ProcessStartInfo(URL) { UseShellExecute = true });
        }

        private void BTN_TestButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Count += Random.Shared.Next(0, 120000);
            // SoundIndexer.PlaySoundID("btn_click_oneshot");
        }
    }

    public enum BarType
    {
        None,
        Tracker,
        TimeLeft
    }

    public enum BackgroundThemes
    {
        Default,
        Brawloween2023,
        Angels
    }
}