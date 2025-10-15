using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace EventTrackerWPF.Librarbies
{
    public static class LocalizationLib
    {
        public static Dictionary<string, string> Strings { get; private set; } = [];
        public static void Load(string FilePath)
        {
            Strings = LoadLocalization(FilePath);
        }

        public const string LocalesFileName = "Localization/Locales.json";
        public static List<Locale> Locales = new List<Locale>()
        {
            new() { LocaleID = "EN", LangName = "English", FilePath = "Localization/EN.csv"},
            new() { LocaleID = "VI", LangName = "Tiếng Việt", FilePath = "Localization/VI.csv"}
        };

        public static List<Locale> LoadLocales()
        {
            try
            {
                if (!File.Exists(LocalesFileName.ToLower()))
                {
                    RestoreDefaults();
                }

                var LocalesJSON = File.ReadAllText(LocalesFileName);
                return JsonSerializer.Deserialize<List<Locale>>(LocalesJSON)!;
            }
            catch (Exception Exc)
            {
                var Message = new AlertMessage()
                {
                    Title = "UH... WHERE ARE WE?",
                    Description = $"Failed to load localization config! The application will now restore the default settings and then close.\n" +
                        "Exception type: " + Exc.GetType() + "\n" +
                        "HResult: " + Exc.HResult + "\n",

                    BlueButton = "OK",
                    BlueButtonFunc = (Err, or) => { MainWindow.Settings.UseDefaultLanguageAndSave(); MainWindow.SoundIndexer.PlaySoundID("btn_click"); }
                };

                Common.CreateAlert(Message);
                RestoreDefaults();
                throw;
            }
        }

        private static void RestoreDefaults()
        {
            using (var Writer = new StreamWriter(LocalesFileName, false, Encoding.UTF8))
            {
                Writer.WriteLine(JsonSerializer.Serialize(Locales));
            }
        }

        public static Dictionary<string, string> LoadLocalization(string FilePath)
        {
            var LocDict = new Dictionary<string, string>();
            try
            {
                foreach (var Line in File.ReadLines(FilePath).Skip(1))
                {
                    var Parts = Line.Split(',', 2);

                    if (Parts[0].StartsWith("\"") && Parts[0].EndsWith("\""))
                    {
                        Parts[0] = Parts[0].Substring(1, Parts[0].Length - 2);
                    }

                    if (Parts[1].StartsWith("\"") && Parts[1].EndsWith("\""))
                    {
                        Parts[1] = Parts[1].Substring(1, Parts[1].Length - 2);
                    }

                    if (Parts.Length >= 2)
                    {
                        Parts[0] = Parts[0].Replace("\\q", "\"");
                        Parts[1] = Parts[1].Replace("\\q", "\"");

                        Parts[0] = Parts[0].Replace("\\n", Environment.NewLine);
                        Parts[1] = Parts[1].Replace("\\n", Environment.NewLine);

                        LocDict[Parts[0]] = Parts[1];
                    }
                    else LocDict[Parts[0]] = Parts[0]; // Fallback lol
                }
                return LocDict;
            }
            catch (Exception Exc)
            {
                var Message = new AlertMessage()
                {
                    Title = "I CAN'T READ!!!",
                    Description = $"Couldn't find language data.\n" +
                    "Exception type: " + Exc.GetType() + "\n" +
                    "Message: " + Exc.Message + "\n" +
                    "HResult: " + Exc.HResult + "\n",

                    BlueButton = "OK",
                    BlueButtonFunc = (Err, or) => { MainWindow.Settings.UseDefaultLanguageAndSave(); MainWindow.SoundIndexer.PlaySoundID("btn_click"); }
                };

                Common.CreateAlert(Message);
                throw;
            }
        }

        public class Locale
        {
            public string? LocaleID { get; set; }
            public string? LangName { get; set; }
            public string? FilePath { get; set; }
        }
    }
}
