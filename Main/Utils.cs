using Main.Models;
using Main.Others;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;

namespace Main
{
    public class Utils
    {
        public static List<string> InitFormatter()
        {
            List<string> FormatFromE6ToE32 = ["", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion"];

            List<string> FormatFromE33Prefix = ["", "un", "duo", "tre", "quattuor", "quin", "sex", "septen", "octo", "novem"];
            List<string> FormatFromE33Suffix = ["decillion", "vigintillion", "trigintillion", "quadragintillion", "quinquagintillion", "sexagintillion", "septuagintillion", "octogintillion", "nonagintillion"];

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

        public static List<string> InitFormatterShort()
        {
            List<string> FormatFromE6ToE32Short = ["", "k", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No"];

            List<string> FormatFromE33PrefixShort = ["", "Un", "Do", "Tr", "Qa", "Qi", "Sx", "Sp", "Oc", "No"];
            List<string> FormatFromE33SuffixShort = ["Dec", "Vgt", "Trgt", "Qagt", "QiQgt", "Sxagt", "SpTgt", "OcTgt", "NoNgt"];

            List<string> Notations = FormatFromE6ToE32Short;
            foreach (var Suffix in FormatFromE33SuffixShort)
            {
                foreach (var Prefix in FormatFromE33PrefixShort)
                {
                    Notations.Add(Prefix + Suffix);
                }
            }

            return Notations;
        }

        public static async Task<EventData?> FetchData()
        {
            EventData Data = new EventData();
            using (var Client = new HttpClient() { Timeout = TimeSpan.FromSeconds(8) })
            {
                var Content = JsonDocument.Parse(await Client.GetStringAsync(BrawlFeedLinks.NewsAPI));
                if (Content != null)
                {
                    for (int Tries = 0; Tries < 5; Tries++)
                    {
                        var EventData = Content.RootElement
                                              .GetProperty("events")[Tries];

                        if (EventData.TryGetProperty("milestones", out JsonElement EventDataChild))
                        {
                            Data.Progress = EventData
                                           .GetProperty("tracker")
                                           .GetProperty("progress")
                                           .GetDouble();

                            int TotalEventMilestones = EventDataChild.GetArrayLength();

                            // 6 might be the maximum amount of milestones that can be displayed on screen
                            // Edit (18/4/2025): Nevermind. They added more milestones anyway. This thing is permanent now!

                            for (int Idx = 0; Idx < TotalEventMilestones; Idx++)
                            {
                                Milestone Milestone = new Milestone();

                                Milestone.MilestoneLabel = EventDataChild[Idx].GetProperty("label").GetString();

                                Milestone.BarPercent = EventDataChild[Idx].GetProperty("progress").GetDouble();

                                Data.Milestones.Add(Milestone);
                            }
                            break;
                        }
                    }
                    return Data;
                }
                else return null;
            }
        }

        public static async Task<List<EventData>> FetchDataMortisiEvent()
        {
            var Data = new List<EventData>();
            using (var Client = new HttpClient() { Timeout = TimeSpan.FromSeconds(8) })
            {
                var Content = JsonDocument.Parse(await Client.GetStringAsync(BrawlFeedLinks.NewsAPI));
                if (Content != null)
                {
                    var EventDataRoot = Content.RootElement
                            .GetProperty("events");

                    for (int Tries = 0; Tries < 5; Tries++)
                    {
                        var EventData = EventDataRoot[Tries];

                        if (EventData.TryGetProperty("milestones", out JsonElement EventDataChild))
                        {
                            var KillProg = EventData
                               .GetProperty("tracker")
                               .GetProperty("progress")
                               .GetDouble();

                            Data.Add(new(){ Progress = KillProg });

                            EventDataChild = EventDataRoot[Tries + 1];

                            var DieProg = EventDataChild
                               .GetProperty("tracker")
                               .GetProperty("progress")
                               .GetDouble();

                            Data.Add(new() { Progress = DieProg });
                            break;
                        }
                    }
                    return Data;
                }
                else return [];
            }
        }

        public static TimeSpan GetTimeLeft()
        {
            var Seconds = EventTime.EventEndEpochTime - DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return Seconds > 0 ? TimeSpan.FromSeconds(Seconds) : TimeSpan.Zero;
        }

        public static string FormatTime(FormatPrefs PrefOption, TimeSpan TimeCount)
        {
            switch (PrefOption)
            {
                case FormatPrefs.None:
                default:
                    if (TimeCount >= TimeSpan.FromDays(1))
                    {
                        return string.Format
                        ("{0:D1}:{1:D2}:{2:D2}:{3:D2}", TimeCount.Days, TimeCount.Hours, TimeCount.Minutes, TimeCount.Seconds);
                    }
                    else
                    {
                        return string.Format
                        ("{0:D1}:{1:D2}:{2:D2}", TimeCount.Hours, TimeCount.Minutes, TimeCount.Seconds);
                    }
                case FormatPrefs.LongText:
                    if (TimeCount >= TimeSpan.FromDays(1))
                    {
                        return string.Format
                        ("{0:D1} " + (TimeCount.Days == 1 || TimeCount.Days == -1 ? "day " : "days ") +
                        (TimeCount.Hours == 0 ? string.Empty : ("{1:D1} " +
                        (TimeCount.Hours == 1 || TimeCount.Hours == -1 ? "hour" : "hours"))), TimeCount.Days, TimeCount.Hours);
                    }
                    else if (TimeCount >= TimeSpan.FromHours(1))
                    {
                        return string.Format
                        ("{0:D1} " + (TimeCount.Hours == 1 || TimeCount.Hours == -1 ? "hour " : "hours ") +
                        (TimeCount.Minutes == 0 ? string.Empty : ("{1:D1} " +
                        (TimeCount.Minutes == 1 || TimeCount.Minutes == -1 ? "min" : "mins"))), TimeCount.Hours, TimeCount.Minutes);
                    }
                    else
                    {
                        return string.Format
                        ((TimeCount.Minutes == 0 ? string.Empty : ("{0:D1} " +
                        (TimeCount.Minutes == 1 || TimeCount.Minutes == -1 ? "min " : "mins "))) +
                        (("{1:D1} " +
                        (TimeCount.Seconds == 1 || TimeCount.Seconds == -1 ? "sec" : "secs"))), TimeCount.Minutes, TimeCount.Seconds);
                    }
                case FormatPrefs.ShortText:
                    if (TimeCount >= TimeSpan.FromDays(1))
                    {
                        return string.Format
                        ("{0:D1}d" +
                        (TimeCount.Hours == 0 ? string.Empty : " {1:D1}h"), TimeCount.Days, TimeCount.Hours);
                    }
                    else if (TimeCount >= TimeSpan.FromHours(1))
                    {
                        return string.Format
                        ("{0:D1}h" +
                        (TimeCount.Minutes == 0 ? string.Empty : " {1:D1}m"), TimeCount.Hours, TimeCount.Minutes);
                    }
                    else
                    {
                        return string.Format
                        ((TimeCount.Minutes == 0 ? string.Empty : "{0:D1}m ") +
                        "{1:D1}s", TimeCount.Minutes, TimeCount.Seconds);
                    }
            }
        }

        public static string Beautify(double Number, FormatPrefs Choice)
        {
            int FormatterIndex = 0;

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

                if (Number >= 1e303 || Number == double.PositiveInfinity) // 1e303 = 1 centillion (but who caresa about that)
                {
                    if (Choice is FormatPrefs.LongText) return "Infinity";
                    else return "naneinf";
                }

                if (Number >= 1e6)
                {
                    while (Math.Floor(Number) >= 1000)
                    {
                        Number /= 1000;
                        FormatterIndex++;
                    }
                }
                else return Number.ToString("#,##0");

                if (FormatterIndex >= Notations.Count)
                {
                    if (Choice is FormatPrefs.LongText) return "Infinity";
                    else return "naneinf";
                }

                return (Math.Floor(Number * 1000) / 1000).ToString("0.###") + (Choice is FormatPrefs.LongText ? " " : "") + Notations[FormatterIndex];
            }
            else return Number.ToString("#,##0");
        }

        public static string BeautifyPercentage(double Number)
        {
            return Number.ToString("0.##" + "%");
        }

        public static double SimpleTextToNumber(string? Text)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                var Match = Regex.Match(Text.Trim(), @"^([+-]?\d*\.?\d+)\s*([a-zA-Z]+)?$");
                if (Match.Success)
                {
                    string Number = Match.Groups[1].Value;
                    string Suffix = Match.Groups[2].Value;

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

        public static string ConvertTimeSpanToDisplayText(TimeSpan Time)
        {
            if (Time.TotalSeconds < 60)
            {
                return "Less than a minute left";
            }
            else if (Time.TotalMinutes < 60)
            {
                return $"{(int)Time.TotalMinutes}m left";
            }
            else if (Time.TotalHours < 24)
            {
                return $"{(int)Time.TotalHours}h {(int)Time.TotalMinutes % 60}m left";
            }
            else
            {
                return $"{(long)Time.TotalDays}d {(int)Time.TotalHours % 24}h left";
            }
        }
    }
}
