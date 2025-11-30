using DiscordRPC;
using EventTrackerWPF.CustomElements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SysDrawing = System.Drawing;
using WinMedia = System.Windows.Media;

namespace EventTrackerWPF.Librarbies
{
    public partial class Common
    {
        public static async Task<FetchResponse> CheckForEvent()
        {
            using (var Client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) })
            {
                var Response = await Client.GetAsync(BrawlFeedLinks.NewsAPI);

                if (Response.IsSuccessStatusCode)
                {
                    string JSONContent = await Response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(JSONContent))
                    {
                        var Content = JsonDocument.Parse(JSONContent);

                        var EventListLength = Content.RootElement
                                        .GetProperty("events").GetArrayLength();

                        for (int Attempt = 0; Attempt < EventListLength; Attempt++)
                        {
                            var EventData = Content.RootElement
                                                   .GetProperty("events")[Attempt];

                            if (EventData.TryGetProperty("milestones", out JsonElement EventMilestones))
                            {
                                return FetchResponse.Success;
                            }
                        }
                        return FetchResponse.NotAvailable;
                    }
                }
                else
                {
                    var Message = new AlertMessage()
                    {
                        Title = LocalizationLib.Strings["TID_ERROR_FETCHDATA_TITLE"],
                        Description = LocalizationLib.Strings["TID_ERROR_FETCHDATA_HTTP_DESC"].Replace("<STATUS_CODE>", Response.StatusCode.ToString()),

                        BlueButton = LocalizationLib.Strings["TID_OK"],
                        BlueButtonFunc = (S, e) => { MainWindow.SoundIndexer.PlaySoundID("btn_click"); }
                    };

                    return FetchResponse.TimedOut;
                }
                return FetchResponse.Failure;
            }
        }

        public static EventData FetchData()
        {
            return MockData.Data;
        }

        public static void CreateAlert(AlertMessage Message)
        {
            var AlertWindow = new AlertWindow(Message);

            AlertWindow.Width = Message.Width;
            AlertWindow.Height = Message.Height;

            AlertWindow.ShowDialog();
        }

        public const string VersionNumber = "1.1.0";
        public static long LastUpdatedDateInUnixTimeSeconds = 1764166163;
        public static DateTimeOffset LastUpdatedDate = DateTimeOffset.FromUnixTimeSeconds(LastUpdatedDateInUnixTimeSeconds);

        private static readonly DiscordRpcClient DiscordClient = new DiscordRpcClient(DiscordClientID);

        public static readonly Uri ResourcePath = new("pack://application:,,,/");

        public const string DiscordClientID = "1385626712842305618";

        // Just in case.
        public static readonly WinMedia.FontFamily LilitaOneRes = new(ResourcePath, "Assets/#Lilita One");
        public static readonly WinMedia.FontFamily DeterminationMonoRes = new(ResourcePath, "Assets/#Determination Mono Web");

        // Rewrite later, this function isn't in main priority right now
        // Will do it after everything else is done to be honest
        public static void UseCustomFont(DependencyObject Parent, SysDrawing.Font? FontMain, SysDrawing.Font? FontAlt)
        {
            if (FontMain != null)
            {
                var Textboxes = CollectCustomTextboxes(Parent);

                foreach (var Textbox in Textboxes)
                {
                    if (Textbox is DrawTextOutlined Normal)
                    {
                        if (Normal.Name.Contains("_CanMono") && FontAlt != null && Settings.AlternareFont)
                        {
                            Normal.FontFamily = new WinMedia.FontFamily(FontAlt.FontFamily.Name);
                            Normal.FontStyle = GetFontStyle(FontAlt);
                            Normal.FontWeight = GetFontWeight(FontAlt);
                        }
                        if (!Normal.Name.Contains("_CanMono"))
                        {
                            Normal.FontFamily = new WinMedia.FontFamily(FontMain.FontFamily.Name);
                            Normal.FontStyle = GetFontStyle(FontMain);
                            Normal.FontWeight = GetFontWeight(FontMain);
                        }
                        continue;
                    }
                    if (Textbox is DrawTextShakyOutlined Shaked)
                    {
                        if (Shaked.Name.Contains("_CanMono") && FontAlt != null && Settings.AlternareFont)
                        {
                            Shaked.FontFamily = new WinMedia.FontFamily(FontAlt.FontFamily.Name);
                            Shaked.FontStyle = GetFontStyle(FontAlt);
                            Shaked.FontWeight = GetFontWeight(FontAlt);
                        }
                        if (!Shaked.Name.Contains("_CanMono"))
                        {
                            Shaked.FontFamily = new WinMedia.FontFamily(FontMain.FontFamily.Name);
                            Shaked.FontStyle = GetFontStyle(FontMain);
                            Shaked.FontWeight = GetFontWeight(FontMain);
                        }
                        continue;
                    }
                }
            }
        }

        public static bool LogResult(double Count)
        {
            var CurrentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            if (SaveSystem.TrackedResults.Count >= 10)
            {
                SaveSystem.TrackedResults = SaveSystem.TrackedResults.TakeLast(10).ToDictionary();
            }

            if (SaveSystem.TrackedResults.Count == 0)
            {
                SaveSystem.TrackedResults[CurrentTime] = Count;
                return true;
            }
            else
            {
                if (SaveSystem.TrackedResults.Last().Value == Count && SaveSystem.TrackedResults.Last().Key - CurrentTime >= 1800)
                {
                    SaveSystem.TrackedResults[CurrentTime] = Count;
                    return true;
                }
                else if (SaveSystem.TrackedResults.Last().Value != Count)
                {
                    SaveSystem.TrackedResults[CurrentTime] = Count;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static List<string> InitFormatter()
        {
            List<string> FormatFromE6ToE32 =
                [
                    string.Empty,
                    LocalizationLib.Strings["thousand"],
                    LocalizationLib.Strings["million"],
                    LocalizationLib.Strings["billion"],
                    LocalizationLib.Strings["trillion"],
                    LocalizationLib.Strings["quadrillion"],
                    LocalizationLib.Strings["quintillion"],
                    LocalizationLib.Strings["sestillion"],
                    LocalizationLib.Strings["septillion"],
                    LocalizationLib.Strings["octillion"],
                    LocalizationLib.Strings["nonillion"]
                ];

            List<string> FormatFromE33Prefix =
                [
                    string.Empty,
                    LocalizationLib.Strings["un"],
                    LocalizationLib.Strings["duo"],
                    LocalizationLib.Strings["tre"],
                    LocalizationLib.Strings["quattuor"],
                    LocalizationLib.Strings["quin"],
                    LocalizationLib.Strings["ses"],
                    LocalizationLib.Strings["septen"],
                    LocalizationLib.Strings["octo"],
                    LocalizationLib.Strings["novem"]
                ];

            List<string> FormatFromE33Suffix =
                [
                    LocalizationLib.Strings["decillion"],
                    LocalizationLib.Strings["vigintillion"],
                    LocalizationLib.Strings["trigintillion"],
                    LocalizationLib.Strings["quadragintillion"],
                    LocalizationLib.Strings["quinquagintillion"],
                    LocalizationLib.Strings["sesagintillion"],
                    LocalizationLib.Strings["septuagintillion"],
                    LocalizationLib.Strings["octogintillion"],
                    LocalizationLib.Strings["nonagintillion"]
                ];

            List<string> Notations = FormatFromE6ToE32;
            foreach (var Suffix in FormatFromE33Suffix)
            {
                foreach (var Prefix in FormatFromE33Prefix)
                {
                    Notations.Add(Prefix + Suffix);
                }
            }

            return Notations;
        }

        private static List<string> InitFormatterShort()
        {
            List<string> FormatFromE6ToE32 =
                [
                    string.Empty,
                    LocalizationLib.Strings["k"],
                    LocalizationLib.Strings["M"],
                    LocalizationLib.Strings["B"],
                    LocalizationLib.Strings["T"],
                    LocalizationLib.Strings["Qa"],
                    LocalizationLib.Strings["Qi"],
                    LocalizationLib.Strings["Sx"],
                    LocalizationLib.Strings["Sp"],
                    LocalizationLib.Strings["Oc"],
                    LocalizationLib.Strings["No"]
                ];

            List<string> FormatFromE33Prefix =
                [
                    string.Empty,
                    LocalizationLib.Strings["Un"],
                    LocalizationLib.Strings["Do"],
                    LocalizationLib.Strings["Tr"],
                    LocalizationLib.Strings["Qa"],
                    LocalizationLib.Strings["Qi"],
                    LocalizationLib.Strings["Sx"],
                    LocalizationLib.Strings["Sp"],
                    LocalizationLib.Strings["Oc"],
                    LocalizationLib.Strings["No"]
                ];

            List<string> FormatFromE33Suffix =
                [
                    LocalizationLib.Strings["Dec"],
                    LocalizationLib.Strings["Vgt"],
                    LocalizationLib.Strings["Trgt"],
                    LocalizationLib.Strings["Qagt"],
                    LocalizationLib.Strings["QiQgt"],
                    LocalizationLib.Strings["Sxagt"],
                    LocalizationLib.Strings["SpTgt"],
                    LocalizationLib.Strings["OcTgt"],
                    LocalizationLib.Strings["NoNgt"]
                ];

            List<string> Notations = FormatFromE6ToE32;
            foreach (var Suffix in FormatFromE33Suffix)
            {
                foreach (var Prefix in FormatFromE33Prefix)
                {
                    Notations.Add(Prefix + Suffix);
                }
            }
            return Notations;
        }

        public static string BeautifyPercentage(double Number)
        {
            if (Number < 1e13 && Number > -1e13)
            {
                return Number.ToString("#,##0.##");
            }
            else
            {
                return Number.ToString("0.######E0").Replace("∞", LocalizationLib.Strings["TID_INFINITY_SHORT"]);
            }
        }

        public static string Beautify(double Number, FormatPrefs Choice)
        {
            int FormatterIndex = 0;

            Number = Math.Round(Number);

            if (Choice != FormatPrefs.None)
            {
                List<string> Notations = [];
                switch (Choice)
                {
                    case FormatPrefs.LongText:
                        Notations = InitFormatter();
                        break;
                    case FormatPrefs.ShortText:
                        Notations = InitFormatterShort();
                        break;
                    default:
                        break;
                }


                if (Number >= double.PositiveInfinity)
                {
                    switch (Choice)
                    {
                        case FormatPrefs.LongText:
                            return LocalizationLib.Strings["TID_INFINITY"];
                        case FormatPrefs.ShortText:
                            return LocalizationLib.Strings["TID_INFINITY_SHORT"];
                        default:
                            return "+inf";
                    }
                }

                if (Number <= double.NegativeInfinity)
                {
                    switch (Choice)
                    {
                        case FormatPrefs.LongText:
                            return LocalizationLib.Strings["TID_INFINITY_N"];
                        case FormatPrefs.ShortText:
                            return LocalizationLib.Strings["TID_INFINITY_N_SHORT"];
                        default:
                            return "-inf";
                    }
                }

                if (Math.Round(Number) >= 1e6 || Math.Round(Number) <= -1e6)
                {
                    var ShortNum = Number;
                    while (Math.Round(Math.Abs(ShortNum), 3) >= 1000)
                    {
                        ShortNum /= 1000;
                        FormatterIndex++;
                    }

                    if (FormatterIndex > Notations.Count - 1)
                    {
                        return Number.ToString("0.######E0").Replace("∞", LocalizationLib.Strings["TID_INFINITY_SHORT"]);
                    }
                    else return ShortNum.ToString("0.###") + (Choice is FormatPrefs.LongText ? " " : "") + Notations[FormatterIndex];
                }
                else return Number.ToString("#,##0");
            }
            else
            {
                if (Number < 1e13 && Number > -1e13 )
                {
                    return Number.ToString("#,##0");
                }
                else
                {
                    return Number.ToString("0.######E0").Replace("∞", LocalizationLib.Strings["TID_INFINITY_SHORT"]);
                }
            }
        }

        private static void UseDefaultFont()
        {

        }

        private static FontStyle GetFontStyle(SysDrawing.Font Target)
        {
            switch (Target.Style)
            {
                case SysDrawing.FontStyle.Regular:
                default:
                    return FontStyles.Normal;
                case SysDrawing.FontStyle.Italic:
                    return FontStyles.Italic;
            }
        }

        private static FontWeight GetFontWeight(SysDrawing.Font Target)
        {
            switch (Target.Style)
            {
                case SysDrawing.FontStyle.Regular:
                default:
                    return FontWeights.Regular;
                case SysDrawing.FontStyle.Bold:
                    return FontWeights.Bold;
            }
        }

        private static List<UIElement> CollectCustomTextboxes(DependencyObject Parent)
        {
            var Textboxes = new List<UIElement>();

            for (int Index = 0; Index < WinMedia.VisualTreeHelper.GetChildrenCount(Parent); Index++)
            {
                var Child = WinMedia.VisualTreeHelper.GetChild(Parent, Index);

                if (Child is DrawTextOutlined Normal)
                {
                    Textboxes.Add(Normal);
                }
                if (Child is DrawTextShakyOutlined Shaked)
                {
                    Textboxes.Add(Shaked);
                }
                Textboxes.AddRange(CollectCustomTextboxes(Child));
            }

            return Textboxes;
        }

        public static string FormatTime(FormatPrefs PrefOption, TimeSpan TimeCount)
        {
            string EndResult = string.Empty;
            switch (PrefOption)
            {
                case FormatPrefs.None:
                default:
                    if (TimeCount >= TimeSpan.FromDays(1))
                    {
                        EndResult = string.Format
                        ("{0:D1}:{1:D2}:{2:D2}:{3:D2}", TimeCount.Days, TimeCount.Hours, TimeCount.Minutes, TimeCount.Seconds);
                    }
                    else
                    {
                        EndResult = string.Format
                        ("{0:D1}:{1:D2}:{2:D2}", TimeCount.Hours, TimeCount.Minutes, TimeCount.Seconds);
                    }
                    break;
                case FormatPrefs.LongText:
                    if (TimeCount >= TimeSpan.FromDays(1))
                    {
                        EndResult = string.Format
                        ("{0:D1} " + (TimeCount.Days == 1 || TimeCount.Days == -1 ? "<DAY> " : "<DAYS> ") +
                        (TimeCount.Hours == 0 ? string.Empty : ("{1:D1}" +
                        (TimeCount.Hours == 1 || TimeCount.Hours == -1 ? "<HOUR>" : "<HOURS>"))), TimeCount.Days, TimeCount.Hours);
                    }
                    else if (TimeCount >= TimeSpan.FromHours(1))
                    {
                        EndResult = string.Format
                        ("{0:D1} " + (TimeCount.Hours == 1 || TimeCount.Hours == -1 ? "<HOUR> " : "<HOURS> ") +
                        (TimeCount.Minutes == 0 ? string.Empty : ("{1:D1}" +
                        (TimeCount.Minutes == 1 || TimeCount.Minutes == -1 ? "<MIN>" : "<MINS>"))), TimeCount.Hours, TimeCount.Minutes);
                    }
                    else
                    {
                        EndResult = string.Format
                        ((TimeCount.Minutes == 0 ? string.Empty : ("{0:D1} " +
                        (TimeCount.Minutes == 1 || TimeCount.Minutes == -1 ? "<MIN> " : "<MINS> "))) +
                        (("{1:D1} " +
                        (TimeCount.Seconds == 1 || TimeCount.Seconds == -1 ? "<SEC>" : "<SECS>"))), TimeCount.Minutes, TimeCount.Seconds);
                    }

                    EndResult =
                        EndResult.Replace("<DAY>", LocalizationLib.Strings["TID_DAYS_LONG_SINGULAR"])
                                 .Replace("<DAYS>", LocalizationLib.Strings["TID_DAYS_LONG"])
                                 .Replace("<HOUR>", LocalizationLib.Strings["TID_HOURS_LONG_SINGULAR"])
                                 .Replace("<HOURS>", LocalizationLib.Strings["TID_HOURS_LONG"])
                                 .Replace("<MIN>", LocalizationLib.Strings["TID_MINUTES_LONG_SINGULAR"])
                                 .Replace("<MINS>", LocalizationLib.Strings["TID_MINUTES_LONG"])
                                 .Replace("<SEC>", LocalizationLib.Strings["TID_SECONDS_LONG_SINGULAR"])
                                 .Replace("<SECS>", LocalizationLib.Strings["TID_SECONDS_LONG"]);
                    break;
                case FormatPrefs.ShortText:
                    if (TimeCount >= TimeSpan.FromDays(1))
                    {
                        EndResult = string.Format
                        ("{0:D1}<D>" +
                        (TimeCount.Hours == 0 ? string.Empty : " {1:D1}<H>"), TimeCount.Days, TimeCount.Hours);
                    }
                    else if (TimeCount >= TimeSpan.FromHours(1))
                    {
                        EndResult = string.Format
                        ("{0:D1}<H>" +
                        (TimeCount.Minutes == 0 ? string.Empty : " {1:D1}<M>"), TimeCount.Hours, TimeCount.Minutes);
                    }
                    else
                    {
                        EndResult = string.Format
                        ((TimeCount.Minutes == 0 ? string.Empty : "{0:D1}<M> ") +
                        "{1:D1}<S>", TimeCount.Minutes, TimeCount.Seconds);
                    }

                    EndResult =
                        EndResult.Replace("<D>", LocalizationLib.Strings["TID_DAYS"])
                                 .Replace("<H>", LocalizationLib.Strings["TID_HOURS"])
                                 .Replace("<M>", LocalizationLib.Strings["TID_MINUTES"])
                                 .Replace("<S>", LocalizationLib.Strings["TID_SECONDS"]);
                    break;
            }

            return EndResult;
        }

        public static double SimpleTextToNumber(string? Text)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                var Match = FormattedNum().Match(Text.Trim());
                if (Match.Success)
                {
                    string Number = Match.Groups[1].Value;
                    string Suffix = Match.Groups[2].Value;

                    if (string.IsNullOrWhiteSpace(Suffix))
                    {
                        return double.Parse(Number);
                    }

                    switch (Suffix)
                    {
                        case "k":
                        case "K":
                            return double.Parse(Number) * 1e03;
                        case "M":
                            return double.Parse(Number) * 1e06;
                        case "B":
                            return double.Parse(Number) * 1e09;
                        case "T":
                            return double.Parse(Number) * 1e12;
                        case "Q":
                        default:
                            return double.Parse(Number) * 1e15;
                    }
                }
                else return double.NaN;
            }
            else return 0;
        }

        public static double ConvertToInfinity(double Number)
        {
            if (double.IsInfinity(Number))
            {
                if (Number <= double.NegativeInfinity)
                {
                    return double.NegativeInfinity;
                }
                if (Number >= double.PositiveInfinity)
                {
                    return double.PositiveInfinity;
                }
            }
            return Number;
        }

        public static void InitializeRPC()
        {
            try
            {
                DiscordClient.Initialize();
            }
            catch (Exception Exc)
            {
                var ErrorMessage = new AlertMessage()
                {
                    Title = LocalizationLib.Strings["TID_DISCORD_RPC_LOAD_FAIL_WARNING_TITLE"],
                    Description = LocalizationLib.Strings["TID_DISCORD_RPC_LOAD_FAIL_WARNING_DESC"] + $"\n\n{Exc.GetType().Name}\n{Exc.Message}",

                    BlueButton = LocalizationLib.Strings["TID_DISCORD_RPC_LOAD_FAIL_WARNING_YES"],
                    BlueButtonFunc = (Be, pis) => { InitializeRPC(); },

                    RedButton = LocalizationLib.Strings["TID_DISCORD_RPC_LOAD_FAIL_WARNING_NO"],
                    RedButtonFunc = (No, pe) => { DiscordClient.Dispose(); }
                };
            }
            finally
            {
                if (DiscordClient.IsInitialized)
                {
                    DiscordClient.SetPresence(new()
                    {
                        Type = ActivityType.Watching,
                        Details = "Loading data...",
                        State = "Waiting...",
                        Buttons = [new() { Label = "chip", Url = "https://www.youtube.com/watch?v=WIRK_pGdIdA" }]
                    });
                }
                else DiscordClient.Dispose();
            }
        }

        public static void SetPresenceMessage(string Details, string State)
        {
            if (DiscordClient.IsInitialized)
            {
                DiscordClient.UpdateDetails(Details);
                DiscordClient.UpdateState(State);
            }
        }


        [GeneratedRegex(@"^([+-]?\d*\.?\d+)\s*([a-zA-Z]+)?$")]
        private static partial Regex FormattedNum();
    }
    public class BrawlFeedLinks
    {
        // W/O Parameters...
        public const string NewsAPI = "https://brawlstars.inbox.supercell.com/data/en/news/content.json";
        public const string News = "https://brawlstars.inbox.supercell.com";
        public const string PollAPI = "https://brawlstars-api.inbox.supercell.com/poll-api/poll/?pollId=";
    }

    public static class MockData
    {
        public static readonly EventData Data = new()
        {
            HTTPStatusCode = 200,
            FetchStatus = FetchResponse.Success,
            Progress = 88.2925942216,
            Milestones =
            [
                new EventMilestone()
                {
                    ProgressPercent = 25,
                    CountLabel = "100M"
                },
                new EventMilestone()
                {
                    ProgressPercent = 50,
                    CountLabel = "300M"
                },
                new EventMilestone()
                {
                    ProgressPercent = 75,
                    CountLabel = "650M"
                },
                new EventMilestone()
                {
                    ProgressPercent = 100,
                    CountLabel = "1.25B"
                }
            ]
        };
    }

    public class EventData
    {
        public int HTTPStatusCode { get; set; }
        public FetchResponse FetchStatus { get; set; } = FetchResponse.NotAvailable;
        public double Progress { get; set; } = 0;
        public List<EventMilestone> Milestones { get; set; } = [];
    }

    public class EventMilestone
    {
        public double ProgressPercent { get; set; } = 0;
        public string CountLabel { get; set; } = string.Empty;
    }

    public enum FetchResponse
    {
        Failure = 0,
        Success = 1,
        NotAvailable = 2,
        TimedOut = 3,
    }

    public class AlertMessage
    {
        public double Width { get; set; } = 800;
        public double Height { get; set; } = 450;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? RedButton { get; set; }
        public string? BlueButton { get; set; }
        public MouseButtonEventHandler RedButtonFunc { get; set; } = (Be, pis) => { };
        public MouseButtonEventHandler BlueButtonFunc { get; set; } = (Be, pis) => { };
    }
}
