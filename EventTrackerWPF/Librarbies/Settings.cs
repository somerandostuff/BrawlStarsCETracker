using System.Drawing;
using System.IO;
using System.Windows;

namespace EventTrackerWPF.Librarbies
{
    public static class Settings
    {
        public static int MaxFPS { get; set; } = 60;
        public static string? Lang { get; set; } = "EN";
        public static bool AutoRefresh { get; set; } = false;
        public static bool AlternareFont { get; set; } = false;
        public static FormatPrefs FormatPref { get; set; } = FormatPrefs.None;
        public static ViewModes ViewMode { get; set; } = ViewModes.Simple;
        public static bool ShowMilestoneProgress { get; set; } = false;
        public static bool SuperSecretSetting { get; set; } = false;
        public static BackgroundThemes SelectedTheme { get; set; }
        public static bool UseOldIcon { get; set; } = false;
        public static bool EnableAnimations { get; set; } = true;
        //public static Font? MainFontFamily { get; set; }
        //public static Font? AltFontFamily { get; set; }

        public const string SettingsFileName = "SETTINGS.txt";

        public static void Load()
        {
            if (!File.Exists(SettingsFileName.ToLower()))
            {
                UseDefaultSettings();
                return;
            }

            foreach (var Line in File.ReadAllLines(SettingsFileName.ToLower()))
            {
                var ConfigParts = Line.Split('=', 2);
                if (ConfigParts.Length != 2) continue;

                var Property = typeof(Settings).GetProperty(ConfigParts[0]);
                if (Property == null) continue;

                try
                {
                    object Value = ConfigParts[1];
                    if (Property.PropertyType.IsEnum)
                    {
                        Value = Enum.Parse(Property.PropertyType, ConfigParts[1]);
                    }
                    else if (Property.PropertyType == typeof(Font))
                    {
                        MessageBox.Show("IT IS A FONT");
                    }
                    else Value = Convert.ChangeType(ConfigParts[1], Property.PropertyType);

                    Property.SetValue(Property.PropertyType, Value);
                }
                catch (Exception Exc)
                {
                    var Message = new AlertMessage()
                    {
                        Title = "Error",
                        Description = "Error while loading settings! The program will now use defaults instead.\n" +
                        "Exception type: " + Exc.GetType() + "\n" +
                        "Message: " + Exc.Message + "\n" +
                        "HResult: " + Exc.HResult + "\n",

                        BlueButton = "OK",
                        BlueButtonFunc = (Err, or) => { MainWindow.SoundIndexer.PlaySoundID("btn_click"); }
                    };

                    Common.CreateAlert(Message);
                    UseDefaultSettings();
                    Save();
                }
            }
        }

        public static void Save()
        {
            using (var Writer = new StreamWriter(SettingsFileName, false))
            {
                foreach (var Property in typeof(Settings).GetProperties())
                {
                    var Value = Property.GetValue(Property.Name);
                    Writer.WriteLine($"{Property.Name}={Value}");
                }
            }
        }

        public static void UseDefaultSettings()
        {
            AutoRefresh = false;
            AlternareFont = false;
            FormatPref = FormatPrefs.None;
            SuperSecretSetting = false;
            EnableAnimations = true;
            Lang = "EN";
        }

        public static void UseDefaultLanguageAndSave()
        {
            Lang = "EN";
            Save();
        }
    }
    public enum FormatPrefs
    {
        None,
        LongText,
        ShortText
    }

    public enum ViewModes
    { 
        Simple,
        Detailed,
        TooMuchStuff
    }
}