using EventTrackerWPF.CustomElements;
using System;
using System.Collections.Generic;
using SysDrawing = System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WinMedia = System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EventTrackerWPF.Librarbies
{
    public class Common
    {
        public static async Task<FetchResponse> CheckForEvent()
        {
            using (var Client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10)} )
            {
                var Response = await Client.GetAsync(BrawlFeedLinks.NewsAPI);

                if (Response.IsSuccessStatusCode)
                {
                    string JSONContent = await Response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(JSONContent))
                    {
                        var Content = JsonDocument.Parse(JSONContent);

                        for (int Attempt = 0; Attempt < 5; Attempt++)
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

        public static void CreateAlert(AlertMessage Message)
        {
            var AlertWindow = new AlertWindow(Message);

            AlertWindow.Width = Message.Width;
            AlertWindow.Height = Message.Height;

            AlertWindow.ShowDialog();
        }

        public const string VersionNumber = "1.1.0";
        public const string LastUpdatedDate = "dd/mm/yyyy";

        public static readonly Uri ResourcePath = new("pack://application:,,,/");

        public const string DiscordClientID = "1385626712842305618";

        // Just in case.
        public static readonly WinMedia.FontFamily LilitaOneRes = new(ResourcePath, "Assets/#Lilita One");
        public static readonly WinMedia.FontFamily DeterminationMonoRes = new(ResourcePath, "Assets/#Determination Mono Web");

        public static BitmapImage RedButton_ViewMode = new BitmapImage(new Uri(ResourcePath, "Assets/Red Button.png"));
        public static BitmapImage GreenButton_ViewMode = new BitmapImage(new Uri(ResourcePath, "Assets/Green Button.png"));

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
                        EndResult.Replace("<DAY>",   LocalizationLib.Strings["TID_DAYS_LONG_SINGULAR"])
                                 .Replace("<DAYS>",  LocalizationLib.Strings["TID_DAYS_LONG"])
                                 .Replace("<HOUR>",  LocalizationLib.Strings["TID_HOURS_LONG_SINGULAR"])
                                 .Replace("<HOURS>", LocalizationLib.Strings["TID_HOURS_LONG"])
                                 .Replace("<MIN>",   LocalizationLib.Strings["TID_MINUTES_LONG_SINGULAR"])
                                 .Replace("<MINS>",  LocalizationLib.Strings["TID_MINUTES_LONG"])
                                 .Replace("<SEC>",   LocalizationLib.Strings["TID_SECONDS_LONG_SINGULAR"])
                                 .Replace("<SECS>",  LocalizationLib.Strings["TID_SECONDS_LONG"]);
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
    }
    public class BrawlFeedLinks
    {
        // W/O Parameters...
        public const string NewsAPI = "https://brawlstars.inbox.supercell.com/data/en/news/content.json";
        public const string News = "https://brawlstars.inbox.supercell.com";
        public const string PollAPI = "https://brawlstars-api.inbox.supercell.com/poll-api/poll/?pollId=";
    }

    public class EventData
    {
        public int HTTPStatusCode { get; set; } 
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
