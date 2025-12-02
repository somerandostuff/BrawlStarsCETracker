using DiscordRPC;
using EventTrackerWPF.CustomElements;
using EventTrackerWPF.Librarbies;
using System.Diagnostics;
using System.Globalization;
using System.Media;
using System.Net.Security;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using SysTimer = System.Timers;
using WinForms = System.Windows.Forms;

namespace EventTrackerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SysTimer.Timer DisplayTicker = new SysTimer.Timer(1000 / 30);
        private readonly SysTimer.Timer ClockTicker = new SysTimer.Timer(125);
        private readonly SysTimer.Timer SplashTextTicker = new SysTimer.Timer(TimeSpan.FromMinutes(5));
        private readonly SysTimer.Timer AutoRefreshTicker = new SysTimer.Timer(TimeSpan.FromMinutes(1));

        public static SoundLibrarby SoundIndexer = new SoundLibrarby();

        private readonly CutsceneManager CutsceneManager = new CutsceneManager();

        List<Storyboard> RunningStoryboards = [];
        List<Image> RunningImages = [];

        Stack<Grid> NavigatedMenus = [];

        double Count = 0, EstimatedCount = 0, CountDisp = 0;

        double StartCount = 0, EndCount = 0;

        double StartPercent = 0, EndPercent = 0;

        double RangePercent, ProgressToRangePercent = 0;

        double EndGoal = 1;

        double RangeAmount = 0;

        // TODO: make mock data, then make a button that switch through every milestone

        double GemsDisp = 0;

        double Siner = 0;

        bool AutofetchedSuccessfully = false;

        public EventData? Data = null;

        List<string> SplashTextKeys = [];

        Array AllFormatPreferences = Enum.GetValues(typeof(FormatPrefs));
        Array AllViewModes = Enum.GetValues(typeof(ViewModes));

        private const double ProgressMaxWidth = 1258;
        private double LastWidthResultView = double.NaN;
        private double LastProcTranslateX = double.NaN;
        private double LastRightTranslateX = double.NaN;
        private const double UpdateEpsilon = 0.5;
        private readonly List<Grid> LanguageMenuButtonAreas = [];
        private readonly List<MouseButtonEventHandler?> LanguageMenuButtonHandlers = [];


        // Probably will be repurposed later to use on everything instead of just languages
        // Starts from 0 but will be displayed as 1 and so forth
        private int CurrentLanguageMenuPage = 0;
        private int LanguageMenuPages = 0;
        private int MaxButtonsPerPage = 0;

        public MainWindow()
        {
            Settings.Load();
            SaveSystem.Load();

            LocalizationLib.Locales = LocalizationLib.LoadLocales();
            LocalizationLib.Load(LocalizationLib.Locales.First(Q => Q.LocaleID == Settings.Lang).FilePath!);

            SoundIndexer.LoadSounds(new Dictionary<string, string>
            {
                { "YOUR_TAKING_TOO_LONG", "SFX/YOUR_TAKING_TOO_LONG.wav" },
                { "jackenlaugh", "SFX/jack_o_lantern_laugh.wav" },
                { "btn_click_oneshot", "SFX/menu_decision.wav" },
                { "btn_click", "SFX/menu_click_08.wav" },
                { "btn_go_back", "SFX/menu_go_back_01.wav" },
                { "btn_dismiss", "SFX/menu_dismiss_01.wav" },
                { "lancer", "SFX/snd_splat.wav" },
                { "SWOON", "SFX/snd_swoon.wav" },
                { "SWOON_IMPACT", "SFX/snd_swoon_fall.wav" },
                { "mus_man", "SFX/man.wav" },
                { "egg", "SFX/snd_egg.wav" }
            });

            InitializeComponent();

            MaxButtonsPerPage = LanguageList.Children.Count;
            LanguageMenuButtonAreas = [..LanguageList.Children.Cast<Grid>()];
            LanguageMenuPages = (int)Math.Ceiling((double)LocalizationLib.Locales.Count / MaxButtonsPerPage);


            LanguageMenuButtonHandlers = Enumerable.Repeat<MouseButtonEventHandler?>(null, MaxButtonsPerPage).ToList();

            // Common.UseCustomFont(MainGrid, Settings.MainFontFamily, Settings.AltFontFamily);

            ChangeView(Settings.ViewMode);
            ToggleIconStyle();

            DisplayTicker.Elapsed += DisplayTicker_Tick;
            DisplayTicker.AutoReset = true;
            DisplayTicker.Start();

            ClockTicker.Elapsed += ClockTicker_Tick;
            ClockTicker.AutoReset = true;
            ClockTicker.Start();

            SplashTextTicker.Elapsed += SplashTextTicker_Tick;
            SplashTextTicker.AutoReset = true;

            AutoRefreshTicker.Elapsed += AutoRefreshTicker_Tick;
            AutoRefreshTicker.AutoReset = true;

            LoadTitleSplashTexts();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // MarqueeEffect.StartMarquee(NewsMarquee_Txt, ElemWidth: NewsMarquee_Txt.ActualWidth, SpeedSeconds: 10);

            SetViewMode(Settings.ViewMode);
            SetNumberPreference(Settings.FormatPref);

            if (Settings.SelectedTheme == 0) ThemeSelect(BackgroundThemes.Default);
            else ThemeSelect(Settings.SelectedTheme);

            if (SaveSystem.Eggs > 0) BTN_Egg.Visibility = Visibility.Visible;

            Count = SaveSystem.SavedEventScore;
            EndGoal = SaveSystem.EndGoal;
            RangeAmount = SaveSystem.ToNextGoal;

            StartCount = SaveSystem.MilestoneStart;
            EndCount = SaveSystem.MilestoneEnd;

            Chk_EnableAnimations.IsChecked = Settings.EnableAnimations;
            Chk_UseAltFont.IsChecked = Settings.AlternareFont;

            DynCounter_Num_CanMono.FontFamily = !Settings.AlternareFont ? Common.LilitaOneRes : Common.DeterminationMonoRes;

            Txt_LangName.Text = LocalizationLib.Locales.First(Q => Q.LocaleID == Settings.Lang).LangName ?? string.Empty;

            Txt_AboutBuild.Text = Txt_AboutBuild.Text.Replace("<VERSION>", Common.VersionNumber);
            Txt_EventEndsIn.Text = Txt_EventEndsIn.Text.Replace("<TIME>", Common.FormatTime(FormatPrefs.LongText, TimeSpan.FromSeconds(0)));
            Txt_BuildDate.Text = Txt_BuildDate.Text.Replace("<DATE>", Common.LastUpdatedDate.ToString(LocalizationLib.Strings["TID_DATETIME_NOW_TOSTRING_FORMAT_DATEONLY"], new CultureInfo(LocalizationLib.Strings["TID_CULTUREINFO"])));

            // InitializeRPC();
        }

        private void PopulateLanguageSelectorView()
        {
            var LangsToShow = LocalizationLib.Locales
                .Skip(CurrentLanguageMenuPage * MaxButtonsPerPage)
                .Take(MaxButtonsPerPage)
                .ToList();

            if (LocalizationLib.Locales.Count <= 16)
                LanguageSelectUI_Page.Visibility = Visibility.Collapsed;
            else
            {
                LanguageSelectUI_Page.Visibility = Visibility.Visible;
                LanguageSelectUI_PageText.Text = LocalizationLib.Strings["TID_PAGE"]
                                                                .Replace("<CURRENT>", (CurrentLanguageMenuPage + 1).ToString("#,##0"))
                                                                .Replace("<MAX_PAGES>", LanguageMenuPages.ToString("#,##0"));
            }

            for (int Index = 0; Index < MaxButtonsPerPage; Index++)
            {
                if (Index < LangsToShow.Count)
                {
                    // Use once, then throw away
                    int LocalIndex = Index;

                    var Area = LanguageMenuButtonAreas[LocalIndex];

                    if (LanguageMenuButtonHandlers[LocalIndex] != null)
                        Area.MouseLeftButtonUp -= LanguageMenuButtonHandlers[LocalIndex]!;

                    MouseButtonEventHandler Handler = (S, E) =>
                    {
                        SoundIndexer.PlaySoundID("btn_click");
                        var Message = new AlertMessage()
                        {
                            Title = LocalizationLib.Strings["TID_CONFIRM_LANGUAGE_CHANGE_TITLE"],
                            Description = LocalizationLib.Strings["TID_CONFIRM_LANGUAGE_CHANGE_DESC"],

                            RedButton = LocalizationLib.Strings["TID_CANCEL"],
                            RedButtonFunc = (No, pe) => { SoundIndexer.PlaySoundID("btn_click"); },

                            BlueButton = LocalizationLib.Strings["TID_OK"],
                            BlueButtonFunc = (Be, pis) => { Settings.Lang = LangsToShow[LocalIndex].LocaleID; Application.Current.Shutdown(); WinForms.Application.Restart(); }
                        };
                        Common.CreateAlert(Message);
                    };

                    LanguageMenuButtonHandlers[LocalIndex] = Handler;
                    Area.MouseLeftButtonUp += Handler;

                    var Image = Area.Children.OfType<Image>().First();
                    var TextArea = Area.Children.OfType<DrawTextOutlined>().First();

                    Area.Visibility = Visibility.Visible;
                    if (LangsToShow[LocalIndex].LocaleID == Settings.Lang)
                    {
                        Image.Source = new BitmapImage(new Uri(Common.ResourcePath, "/Assets/Green Button.png"));
                        Area.IsEnabled = false;
                        Area.Opacity = 1;
                    }
                    TextArea.Text = LangsToShow[Index].LangName ?? string.Empty;
                }
                else
                {
                    LanguageMenuButtonAreas[Index].Visibility = Visibility.Collapsed;
                    if (LanguageMenuButtonHandlers[Index] != null)
                    {
                        LanguageMenuButtonAreas[Index].MouseLeftButtonUp -= LanguageMenuButtonHandlers[Index]!;
                        LanguageMenuButtonHandlers[Index] = null;
                    }
                }
            }
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
                case BackgroundThemes.BlackNWhite:
                    {
                        BlackNWhiteThemeGrid.Visibility = Visibility.Visible;
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

        private void SplashTextTicker_Tick(object? sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(ChangeSplashText);
        }

        private void ChangeSplashText()
        {
            Title = LocalizationLib.Strings["TID_APPTITLE"]
                .Replace("<SPLASH>", LocalizationLib.Strings[SplashTextKeys[Random.Shared.Next(0, SplashTextKeys.Count)]])
                .Replace("<NUM>", Random.Shared.Next(0, 1001).ToString())
                .Replace("<DATE>", DateTime.Now.ToString
                    (LocalizationLib.Strings["TID_DATETIME_NOW_TOSTRING_FORMAT_DATEONLY"], new CultureInfo(LocalizationLib.Strings["TID_CULTUREINFO"])));
        }

        public async void FetchData()
        {
            Txt_Status.Text = LocalizationLib.Strings["TID_STATUS_FETCHING"];

            // Put async later when using real HTTP requests. This is using mock data for now.
            await Task.Delay(500);

            try
            {
                Data = Common.FetchData();
            }
            catch (Exception EX)
            {
                var Message = new AlertMessage()
                {
                    Width = 1200,
                    Height = 675,
                    Title = LocalizationLib.Strings["TID_GENERIC_ERROR_LABEL"],
                    Description =
                        LocalizationLib.Strings["TID_GENERIC_TRYCATCH_ERROR_MESSAGE"]
                                .Replace("<MESSAGE>", LocalizationLib.Strings["TID_GENERIC_TRYCATCH_ERROR_DESC"])
                                .Replace("<EXC_NAME>", EX.GetType().Name)
                                .Replace("<HRESULT>", EX.HResult.ToString()
                                .Replace("<MESSAGE>", EX.Message)),
                    BlueButton = LocalizationLib.Strings["TID_OK"],
                    BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); }
                };
                Common.CreateAlert(Message);

                Txt_Status.Text = LocalizationLib.Strings["TID_STATUS_FETCHING_FAIL"];

                return;
            }
            finally
            {
                if (Data != null && Data.FetchStatus != FetchResponse.NotAvailable)
                {
                    Txt_Status.Text = string.Empty;
                    EndGoal = Common.SimpleTextToNumber(Data.Milestones.Last().CountLabel);
                    for (int Idx = 0; Idx < Data.Milestones.Count; Idx++)
                    {
                        if (Idx == 0 && Data.Progress < Data.Milestones[Idx].ProgressPercent)
                        {
                            StartPercent = 0;
                            EndPercent = Data.Milestones[Idx].ProgressPercent;

                            StartCount = 0;
                            EndCount = Common.SimpleTextToNumber(Data.Milestones[Idx].CountLabel);

                            RangePercent = EndPercent - StartPercent;
                            ProgressToRangePercent = Data.Progress - StartPercent;

                            RangeAmount = EndCount - StartCount;

                            Count = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);

                            break;
                        }
                        else if (Idx == Data.Milestones.Count - 1 && Data.Progress > Data.Milestones[Idx].ProgressPercent)
                        {
                            // Since the event has already reached the last milestone, 
                            // we set the count to infinity (endless mode)
                            // but keep the scaling consistent with the previous milestone

                            double Start_ishCount = Common.SimpleTextToNumber(Data.Milestones[Idx - 1].CountLabel);

                            StartPercent = Data.Milestones[Idx - 1].ProgressPercent;
                            EndPercent = Data.Milestones[Idx].ProgressPercent;

                            StartCount = Common.SimpleTextToNumber(Data.Milestones[Idx].CountLabel);
                            EndCount = double.PositiveInfinity;

                            RangePercent = EndPercent - StartPercent;
                            ProgressToRangePercent = Data.Progress - EndPercent;

                            RangeAmount = StartCount - Start_ishCount;

                            Count = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);

                            break;
                        }
                        else if (Idx > 0 && Data.Progress > Data.Milestones[Idx - 1].ProgressPercent && Data.Progress <= Data.Milestones[Idx].ProgressPercent)
                        {
                            StartPercent = Data.Milestones[Idx - 1].ProgressPercent;
                            EndPercent = Data.Milestones[Idx].ProgressPercent;

                            StartCount = Common.SimpleTextToNumber(Data.Milestones[Idx - 1].CountLabel);
                            EndCount = Common.SimpleTextToNumber(Data.Milestones[Idx].CountLabel);

                            RangePercent = EndPercent - StartPercent;
                            ProgressToRangePercent = Data.Progress - StartPercent;

                            RangeAmount = EndCount - StartCount;

                            Count = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);

                            break;
                        }
                    }

                    Count = Math.Round(Count);
                    RangeAmount = Math.Round(RangeAmount);

                    SaveSystem.EndGoal = EndGoal;
                    SaveSystem.SavedEventScore = Count;
                    SaveSystem.ToNextGoal = RangeAmount;

                    SaveSystem.MilestoneStart = StartCount;
                    SaveSystem.MilestoneEnd = EndCount;

                    AutofetchedSuccessfully = Common.LogResult(Count);

                    CalculateAverage();
                }
                else Swoon();
            }
        }

        private void CalculateAverage()
        {
            if (SaveSystem.TrackedResults.Count > 1)
            {
                var CountDiff = SaveSystem.TrackedResults.Last().Value - SaveSystem.TrackedResults.First().Value;
                var TimeDiff = SaveSystem.TrackedResults.Last().Key - SaveSystem.TrackedResults.First().Key;

                EstimatedCount = CountDiff / TimeDiff;

                Txt_Approximation.Text = LocalizationLib.Strings["TID_PROGRESS_SPEED"]
                                                        .Replace("<SPEED>", Common.Beautify(EstimatedCount, Settings.FormatPref))
                                                        .Replace("<TIME_UNIT>", LocalizationLib.Strings["TID_SECONDS_LONG_SINGULAR"]);

                Txt_ETA.Text = LocalizationLib.Strings["TID_ESTIMATED_TIME"]
                                                .Replace("<TIME>", Common.FormatTime(Settings.FormatPref, TimeSpan.FromSeconds((EndCount - Count) / EstimatedCount)));
            }
        }

        private void DisplayTicker_Tick(object? Sender, EventArgs Event)
        {
            CalculateDisplay();

            // This will be for UI-only updates (like displaying numbers and such)
            Dispatcher.Invoke(() =>
            {
                DynCounter_Num_CanMono.Text = Common.Beautify(CountDisp, Settings.FormatPref);
                MoneyCount.Text = Common.Beautify(GemsDisp, FormatPrefs.ShortText);
                Txt_ProgressPercent.Text = Common.BeautifyPercentage(CountDisp / EndGoal * 100) + "%";
                ProcProgressBar(CountDisp / EndGoal * 100);
                UpdateDynCounterPos();

                if (StartCount > 0 && EndCount > 0)
                {
                    if (Settings.ShowMilestoneProgress)
                    {
                        Txt_MilestoneLeft.Text = Common.Beautify(StartCount, Settings.FormatPref);
                        Txt_MilestoneRight.Text = Common.Beautify(EndCount, Settings.FormatPref);
                    }
                }

                if (Count > EndGoal)
                {
                    Txt_NextMilestone.Text =
                        LocalizationLib.Strings["TID_POINTS_TO_NEXT_MILESTONE_ALL_DONE"];
                }
                else
                {
                    Txt_NextMilestone.Text =
                        LocalizationLib.Strings["TID_POINTS_TO_NEXT_MILESTONE"]
                                       .Replace("<POINTS>", Common.Beautify(EndCount - CountDisp, Settings.FormatPref));
                }
            });

            // Man
            Dispatcher.Invoke(() =>
            {
                if (RoomMan.Visibility == Visibility.Visible)
                {
                    StopAllThemeAnimations();

                    // Leaf animations go here because in DELTARUNE source code Toby used sin/cos to move the leaves
                    // and these luxuries aren't supported by WPF XAML...
                    if (Settings.EnableAnimations)
                    {
                        Siner += 1;
                    }
                    else Siner = 0;

                    RoomMan_Leaf1.X = Math.Sin(Siner / 24) * 8;
                    RoomMan_Leaf1.Y = Math.Cos(Siner / 40) * 8;

                    RoomMan_Leaf2.X = Math.Sin(Siner / 28) * 4;
                    RoomMan_Leaf2.Y = Math.Cos(Siner / 48) * 4;

                    if (Siner >= (840 * 60) || Siner <= 0) Siner = 0;
                }
            });
        }

        private void ClockTicker_Tick(object? Sender, ElapsedEventArgs Event)
        {
            Dispatcher.Invoke(() =>
            {
                Txt_SystemClock.Text = DateTime.Now.ToString(LocalizationLib.Strings["TID_DATETIME_NOW_TOSTRING_FORMAT_DATEANDTIME"], new CultureInfo(LocalizationLib.Strings["TID_CULTUREINFO"]));
            });
        }

        private void AutoRefreshTicker_Tick(object? sender, ElapsedEventArgs e)
        {
            AutoRefresh();
        }

        private void AutoRefresh()
        {
            if ((DateTimeOffset.UtcNow.Minute >= 26 && DateTimeOffset.UtcNow.Minute <= 34) ||
                 DateTimeOffset.UtcNow.Minute >= 56 || DateTimeOffset.UtcNow.Minute <= 04)
            {
                if (!AutofetchedSuccessfully)
                {
                    FetchData();
                }
            }
            else
            {
                AutofetchedSuccessfully = false;
            }
        }

        private void CalculateDisplay()
        {
            if (double.IsInfinity(Count))
            {
                CountDisp = Common.ConvertToInfinity(Count);
            }
            else
            {
                CountDisp += (Count - CountDisp) * .275;
            }

            if (double.IsInfinity(SaveSystem.Gems))
            {
                GemsDisp = Common.ConvertToInfinity(SaveSystem.Gems);
            }
            else GemsDisp += (SaveSystem.Gems - GemsDisp) * .2325;
        }

        private void UpdateDynCounterPos()
        {
            var TextSize = DynCounter_Num_CanMono.MeasureTextSize();

            double MinImageX = DynCounter_Img.Width;
            double MaxImageX = (DynCounter.Width + DynCounter_Img.Width) / 2;

            double MaxNumberX = DynCounter.Width - MinImageX;

            double Offset = TextSize.Width / 2;

            DynCounter_ImgGrid.Width = Math.Max(MinImageX, MaxImageX - Offset);
            DynCounter_NumGrid.Width = Math.Min(DynCounter.Width - DynCounter_ImgGrid.Width, MaxNumberX);

            DynCounter_Dummy.Width = TextSize.Width + DynCounter_Img.Width;
        }

        private void ChangeView(ViewModes Mode)
        {
            Settings.ViewMode = Mode;
            switch (Mode)
            {
                case ViewModes.Simple:
                    {
                        SimpleView.Visibility = Visibility.Visible;
                        DetailedView.Visibility = Visibility.Collapsed;
                        TooMuchStuffView.Visibility = Visibility.Collapsed;
                    }
                    break;
                case ViewModes.Detailed:
                    {
                        SimpleView.Visibility = Visibility.Visible;
                        DetailedView.Visibility = Visibility.Visible;
                        TooMuchStuffView.Visibility = Visibility.Collapsed;
                    }
                    break;
                case ViewModes.TooMuchStuff:
                    {
                        SimpleView.Visibility = Visibility.Visible;
                        DetailedView.Visibility = Visibility.Visible;
                        TooMuchStuffView.Visibility = Visibility.Visible;
                    }
                    break;
                default:
                    break;
            }
        }

        private void LoadTitleSplashTexts()
        {
            SplashTextTicker.Stop();
            SplashTextKeys = LocalizationLib.Strings.Keys.Where(Q => Q.StartsWith("TID_SPLASHTEXT_")).ToList();
            SplashTextTicker.Start();

            Dispatcher.Invoke(ChangeSplashText);
        }

        private void ChangeNumberPreference(bool NavLeft)
        {
            var CurrentIndex = Array.IndexOf(AllFormatPreferences, Settings.FormatPref);
            var NewIndex = 0;
            if (NavLeft)
            {
                NewIndex = CurrentIndex - 1;
                if (NewIndex < 0) NewIndex = AllFormatPreferences.Length - 1;
            }
            else
            {
                NewIndex = CurrentIndex + 1;
                if (NewIndex >= AllFormatPreferences.Length) NewIndex = 0;
            }
            SetNumberPreference((FormatPrefs)AllFormatPreferences.GetValue(NewIndex)!);
        }

        private void ChangeViewMode(bool NavLeft)
        {
            var CurrentIndex = Array.IndexOf(AllViewModes, Settings.ViewMode);
            var NewIndex = 0;
            if (NavLeft)
            {
                NewIndex = CurrentIndex - 1;
                if (NewIndex < 0) NewIndex = AllViewModes.Length - 1;
            }
            else
            {
                NewIndex = CurrentIndex + 1;
                if (NewIndex >= AllViewModes.Length) NewIndex = 0;
            }
            SetViewMode((ViewModes)AllViewModes.GetValue(NewIndex)!);
        }

        private void SetNumberPreference(FormatPrefs Pref)
        {
            Settings.FormatPref = Pref;
            switch (Pref)
            {
                case FormatPrefs.None:
                    FormatPrefUI_PrefText.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_NORMAL"];
                    Txt_ChangeNumFormat.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_NORMAL_EXAMPLE"];
                    FormatPref_Example1.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_NORMAL_EXAMPLE"];
                    FormatPref_Example2.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_NORMAL_EXAMPLE2"];
                    break;
                case FormatPrefs.LongText:
                    FormatPrefUI_PrefText.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_SHORTENED"];
                    Txt_ChangeNumFormat.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_SHORTENED_EXAMPLE"];
                    FormatPref_Example1.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_SHORTENED_EXAMPLE"];
                    FormatPref_Example2.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_SHORTENED_EXAMPLE2"];
                    break;
                case FormatPrefs.ShortText:
                    FormatPrefUI_PrefText.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_SUPERSHORTENED"];
                    Txt_ChangeNumFormat.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_EXT_SHORTENED_EXAMPLE"];
                    FormatPref_Example1.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_EXT_SHORTENED_EXAMPLE"];
                    FormatPref_Example2.Text = LocalizationLib.Strings["TID_NUM_FORMAT_PREF_EXT_SHORTENED_EXAMPLE2"];
                    break;
                default:
                    break;
            }
        }

        private void SetViewMode(ViewModes Mode)
        {
            ChangeView(Mode);
            switch (Mode)
            {
                case ViewModes.Simple:
                    ViewSelect_Nav_PrefText.Text = LocalizationLib.Strings["TID_VIEWMODE_SIMPLE"];
                    break;
                case ViewModes.Detailed:
                    ViewSelect_Nav_PrefText.Text = LocalizationLib.Strings["TID_VIEWMODE_DETAILED"];
                    break;
                case ViewModes.TooMuchStuff:
                    ViewSelect_Nav_PrefText.Text = LocalizationLib.Strings["TID_VIEWMODE_OVERCOMPLICATED"];
                    break;
                default:
                    break;
            }
        }

        private void ToggleIconStyle()
        {
            if (Settings.UseOldIcon)
            {
                LogosCorner_NewStyle.Visibility = Visibility.Collapsed;
                LogosCorner_OldStyle.Visibility = Visibility.Visible;
            }
            else
            {
                LogosCorner_OldStyle.Visibility = Visibility.Collapsed;
                LogosCorner_NewStyle.Visibility = Visibility.Visible;
            }
        }

        private void ProcProgressBar(double Percentage)
        {
            var WidthResult = ProgressMaxWidth * Percentage / 100.0;
            var WidthResultView = Math.Round(WidthResult * 100) / 100.0;

            if (WidthResult <= 0)
            {
                WidthResult = 0;
                ProgressBar_FLeft.Visibility = Visibility.Hidden;
                ProgressBar_FMid.Visibility = Visibility.Hidden;
                ProgressBar_FRight.Visibility = Visibility.Hidden;
            }
            else
            {
                ProgressBar_FLeft.Visibility = Visibility.Visible;
                ProgressBar_FMid.Visibility = Visibility.Visible;
                ProgressBar_FRight.Visibility = Visibility.Visible;
            }

            if (WidthResult >= ProgressMaxWidth) WidthResult = ProgressMaxWidth;

            if (WidthResultView <= 0 || WidthResultView >= ProgressMaxWidth)
                ProgressBar_FProc.Visibility = Visibility.Collapsed;
            else
                ProgressBar_FProc.Visibility = Visibility.Visible;

            if (ProgressBar_FMid.RenderTransform is not ScaleTransform)
            {
                ProgressBar_FMid.Width = ProgressMaxWidth;
                ProgressBar_FMid.RenderTransformOrigin = new Point(0, 0.5);
                ProgressBar_FMid.RenderTransform = new ScaleTransform(1.0, 1.0);
            }

            if (ProgressBar_FProc.RenderTransform is not TranslateTransform)
            {
                ProgressBar_FProc.RenderTransform = new TranslateTransform();
            }

            if (ProgressBar_FRight.RenderTransform is not TranslateTransform)
            {
                ProgressBar_FRight.RenderTransform = new TranslateTransform();
            }

            // Avoid layout thrashing: only update when value changed sufficiently
            if (double.IsNaN(LastWidthResultView) || Math.Abs(WidthResultView - LastWidthResultView) > UpdateEpsilon)
            {
                var Scale = ProgressMaxWidth == 0 ? 0 : (WidthResult / ProgressMaxWidth);
                ((ScaleTransform)ProgressBar_FMid.RenderTransform).ScaleX = Math.Max(0, Math.Min(1, Scale));
                LastWidthResultView = WidthResultView;
            }

            var ProcTranslateX = WidthResult;
            if (double.IsNaN(LastProcTranslateX) || Math.Abs(ProcTranslateX - LastProcTranslateX) > UpdateEpsilon)
            {
                ((TranslateTransform)ProgressBar_FProc.RenderTransform).X = ProcTranslateX;
                LastProcTranslateX = ProcTranslateX;
            }

            var RightTranslateX = WidthResult;
            if (double.IsNaN(LastRightTranslateX) || Math.Abs(RightTranslateX - LastRightTranslateX) > UpdateEpsilon)
            {
                ((TranslateTransform)ProgressBar_FRight.RenderTransform).X = RightTranslateX;
                LastRightTranslateX = RightTranslateX;
            }
        }

        #region Controls

        private void BTN_Refresh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            FetchData();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Save();
            SaveSystem.Save();
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
                RedButtonFunc = (Re, set) => { SaveSystem.Gems = 0; Count = 0; SoundIndexer.PlaySoundID("btn_click"); },

                BlueButton = "Free gems",
                BlueButtonFunc = (Ba, lls) => { SaveSystem.Gems += Random.Shared.Next(1, 184); SoundIndexer.PlaySoundID("btn_click"); SaveSystem.Save(); }
            };

            SoundIndexer.PlaySoundID("btn_click");
            Common.CreateAlert(Message);
        }
        private void BTN_About_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            NavigateTo(AboutUI);
        }

        private void BTN_GoToFanPolicyLink_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            string URL = LocalizationLib.Strings["TID_DISCLAIMER_FAN_CONTENT_POLICY_LINK"];
            Process.Start(new ProcessStartInfo(URL) { UseShellExecute = true });
        }

        private void BTN_ViewModeInfo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var Message = new AlertMessage()
            {
                Title = LocalizationLib.Strings["TID_VIEWMODE_INFO_TITLE"],
                Description = LocalizationLib.Strings["TID_VIEWMODE_INFO_DESC"],

                BlueButton = LocalizationLib.Strings["TID_OK"],
                BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); }
            };
            SoundIndexer.PlaySoundID("btn_click");
            Common.CreateAlert(Message);
        }

        private void BTN_ViewMode_Simple_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            ChangeView(ViewModes.Simple);
        }

        private void BTN_ViewMode_Detailed_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            ChangeView(ViewModes.Detailed);
        }

        private void BTN_ViewMode_TooMuchStuff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            ChangeView(ViewModes.TooMuchStuff);
        }
        private void BTN_LogoToggler_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            Settings.UseOldIcon = !Settings.UseOldIcon ? true : false;
            ToggleIconStyle();
        }

        private void BTN_EventInfoTab_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            NavigateTo(EventResultsHistoryUI);
        }
        private void Chk_AutoRefresh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            Settings.AutoRefresh = Chk_AutoRefresh.IsChecked;
            BTN_Refresh.IsEnabled = !Chk_AutoRefresh.IsChecked;

        if (Chk_AutoRefresh.IsChecked)
            {
                var Message = new AlertMessage()
                {
                    Title = LocalizationLib.Strings["TID_AUTOREFRESH_ENABLED_TITLE"],
                    Description = LocalizationLib.Strings["TID_AUTOREFRESH_ENABLED_DESC"],

                    BlueButton = LocalizationLib.Strings["TID_OK"],
                    BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); AutoRefresh(); }
                };
                Common.CreateAlert(Message);
            }
        }
        private void BTN_GoToGitHubRepo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            string URL = LocalizationLib.Strings["TID_ABOUT_GITHUB_REPO_LINK"];
            Process.Start(new ProcessStartInfo(URL) { UseShellExecute = true });
        }

        // WIP
        private void BTN_FontchangerrrrrMAIN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var FontDialog = new WinForms.FontDialog();

            WinForms.DialogResult Result = FontDialog.ShowDialog();

            if (Result == WinForms.DialogResult.OK)
            {
                MessageBox.Show("Selected font: " + FontDialog);

                Common.UseCustomFont(MainGrid, FontDialog.Font, null);
            }
        }

        private void Chk_UseAltFont_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            Settings.AlternareFont = Chk_UseAltFont.IsChecked;

            DynCounter_Num_CanMono.FontFamily = !Settings.AlternareFont ? Common.LilitaOneRes : Common.DeterminationMonoRes;
        }
        private void BTN_SuperSecretSettings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // 1 in 50 chance
            if (Random.Shared.Next(0, 50) == 6)
            {
                NavigateTo(RoomMan);
                SoundIndexer.PlayLoopingSoundID("mus_man");

                Title = string.Empty;
                SplashTextTicker.Stop();
            }
            else
            {
                SoundIndexer.PlaySoundID("btn_click");
                NavigateTo(SuperSecretSettingsUI);
            }
        }

        private void RoomMan_Tree_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!SaveSystem.Egg)
            {
                var Message = new AlertMessage()
                {
                    Title = string.Empty,
                    Description = LocalizationLib.Strings["TID_MYSTERY_ROOM_DIALOG_0"],

                    BlueButton = LocalizationLib.Strings["TID_OK"],
                    BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); }
                };

                Common.CreateAlert(Message);
            }
            else
            {
                var Message = new AlertMessage()
                {
                    Title = string.Empty,
                    Description = LocalizationLib.Strings["TID_MYSTERY_ROOM_DIALOG_0_DONE"],

                    BlueButton = LocalizationLib.Strings["TID_OK"],
                    BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); }
                };

                Common.CreateAlert(Message);
            }
        }

        private void RoomMan_BTN_Man_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!SaveSystem.Egg)
            {
                var Message3Yes = new AlertMessage()
                {
                    Title = string.Empty,
                    Description = LocalizationLib.Strings["TID_MYSTERY_ROOM_DIALOG_3_YES"],

                    BlueButton = LocalizationLib.Strings["TID_OK"],
                    BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); }
                };

                var Message3No = new AlertMessage()
                {
                    Title = string.Empty,
                    Description = LocalizationLib.Strings["TID_MYSTERY_ROOM_DIALOG_3_NO"],

                    BlueButton = LocalizationLib.Strings["TID_OK"],
                    BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); }
                };

                var Message2 = new AlertMessage()
                {
                    Title = string.Empty,
                    Description = LocalizationLib.Strings["TID_MYSTERY_ROOM_DIALOG_2"],

                    BlueButton = LocalizationLib.Strings["TID_MYSTERY_ROOM_DIALOG_2_YES"],
                    BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("egg"); Common.CreateAlert(Message3Yes); SaveSystem.Eggs++; SaveSystem.Egg = true; BTN_Egg.Visibility = Visibility.Visible; },
                    RedButton = LocalizationLib.Strings["TID_MYSTERY_ROOM_DIALOG_2_NO"],
                    RedButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); Common.CreateAlert(Message3No); SaveSystem.Egg = true;}
                };

                var Message1 = new AlertMessage()
                {
                    Title = string.Empty,
                    Description = LocalizationLib.Strings["TID_MYSTERY_ROOM_DIALOG_1"],

                    BlueButton = LocalizationLib.Strings["TID_OK"],
                    BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); Common.CreateAlert(Message2); }
                };

                Common.CreateAlert(Message1);
            }
            else
            {
                var Message4 = new AlertMessage()
                {
                    Title = string.Empty,
                    Description = LocalizationLib.Strings["TID_MYSTERY_ROOM_DIALOG_4"],

                    BlueButton = LocalizationLib.Strings["TID_OK"],
                    BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); }
                };
                Common.CreateAlert(Message4);
            }
        }

        private void BTN_ChangeNumberFormat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            NavigateTo(FormatPrefUI);
        }

        private void BTN_FormatPref_GoLeft_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            ChangeNumberPreference(NavLeft: true);
        }

        private void BTN_FormatPref_GoRight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            ChangeNumberPreference(NavLeft: false);
        }

        private void BTN_ViewSelect_Nav_GoLeft_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            ChangeViewMode(NavLeft: true);
        }

        private void ViewSelect_Nav_GoRight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            ChangeViewMode(NavLeft: false);
        }
        private void BTN_ChangeLanguage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            PopulateLanguageSelectorView();
            NavigateTo(LanguageSelectUI);
        }
        private void BTN_LangSelect_GoLeft_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            CurrentLanguageMenuPage = CurrentLanguageMenuPage <= 0 ? 0 : CurrentLanguageMenuPage - 1;
            PopulateLanguageSelectorView();
        }

        private void BTN_LangSelect_GoRight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            CurrentLanguageMenuPage = CurrentLanguageMenuPage >= LanguageMenuPages - 1 ? LanguageMenuPages - 1 : CurrentLanguageMenuPage + 1;
            PopulateLanguageSelectorView();
        }
        private void BTN_Egg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("egg");
            var Message4 = new AlertMessage()
            {
                Title = string.Empty,
                Description = LocalizationLib.Strings["TID_MYSTERY_EGGS_OBTAINED"].Replace("<COUNT>", Common.Beautify(SaveSystem.Eggs, Settings.FormatPref)),

                BlueButton = LocalizationLib.Strings["TID_OK"],
                BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); }
            };
            Common.CreateAlert(Message4);
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
            if (NavigatedMenus.Count > 0)
            {
                var ClosingMenu = NavigatedMenus.Pop();

                ClosingMenu.Visibility = Visibility.Collapsed;
                if (NavigatedMenus.Count == 0)
                {
                    MainMenuUI.Visibility = Visibility.Visible;
                    NavButtons.Visibility = Visibility.Collapsed;
                }
                else NavigatedMenus.Last().Visibility = Visibility.Visible;

                if (ClosingMenu == RoomMan)
                {
                    ThemeSelect(Settings.SelectedTheme);
                    SoundIndexer.StopLoopingSoundID("mus_man");

                    SplashTextTicker.Start();
                    ChangeSplashText();
                }
            }
        }

        private void GoHome()
        {
            if (RoomMan.Visibility == Visibility.Visible)
            {
                ThemeSelect(Settings.SelectedTheme);
                SoundIndexer.StopLoopingSoundID("mus_man");

                SplashTextTicker.Start();
                ChangeSplashText();
            }

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

        private void DynCounterNum_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SoundIndexer.PlaySoundID("btn_click");
            var Message = new AlertTextboxMessage()
            {
                Title = LocalizationLib.Strings["TID_COPY_TO_SHARE_PROMPT_TITLE"],
                Description = LocalizationLib.Strings["TID_COPY_TO_SHARE_PROMPT_DESC"],

                TextboxContent = LocalizationLib.Strings["TID_COPY_TO_SHARE_PROMPT_CONTENT_GENERIC"]
                                                .Replace("<SCORE>", Common.Beautify(Count, Settings.FormatPref)),

                BlueButton = LocalizationLib.Strings["TID_COPY_TO_SHARE_PROMPT_CONFIRM"],
                BlueButtonFunc = (Be, pis) => { SoundIndexer.PlaySoundID("btn_click"); },
                BlueButtonCopiesTextboxContent = true
            };
            Common.CreateTextboxAlert(Message);
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

        #region Cutscene(s)

        private void Swoon()
        {
            CutsceneManager.AddEvent(new()
            {
                Delay = TimeSpan.Zero,
                Action = () =>
                {
                    SoundIndexer.PlaySoundID("SWOON");
                }
            });
            CutsceneManager.AddEvent(new(){
               Delay = TimeSpan.FromSeconds(0.2),
               Action = () =>
               {
                   SwoonCutscene.Visibility = Visibility.Visible;
                   ThemeSelect(BackgroundThemes.BlackNWhite);
                   SplashTextTicker.Stop();
                   Title = string.Empty;
                   Settings.AutoRefresh = false;
               }
            });
            CutsceneManager.AddEvent(new()
            {
                Delay = TimeSpan.FromSeconds(2),
                Action = () =>
                {
                    SoundIndexer.PlaySoundID("SWOON_IMPACT");
                }
            });
            CutsceneManager.AddEvent(new()
            {
                Delay = TimeSpan.FromSeconds(0.2),
                Action = () =>
                {
                    SwoonImage.Visibility = Visibility.Collapsed;
                    UserInterfaces.Visibility = Visibility.Collapsed;
                }
            });

            CutsceneManager.Start();
        }

        #endregion

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && NavigatedMenus.Count > 0)
            {
                GoBack();
                SoundIndexer.PlaySoundID("btn_go_back");
                return;
            }
        }
    }

    public enum BackgroundThemes
    {
        Default,
        Brawloween2023,
        Angels,
        BlackNWhite = 666
    }
}