using System.IO;
using System.Windows;

namespace EventTrackerWPF.Librarbies
{
    public class Settings
    {
        public int MaxFPS { get; set; } = 60;
        public string? Lang { get; set; } = "EN";
        public bool AutoRefresh { get; set; } = false;
        public bool AlternareFont { get; set; } = false;
        public FormatPrefs FormatPrefs { get; set; } = FormatPrefs.None;
        public bool SuperSecretSetting { get; set; } = false;
        public BackgroundThemes SelectedTheme { get; set; }
        public bool EnableAnimations { get; set; } = true;

        public const string SettingsFileName = "SETTINGS.txt";

        public void Load()
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
                    else Value = Convert.ChangeType(ConfigParts[1], Property.PropertyType);

                    Property.SetValue(this, Value);
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

        public void Save()
        {
            using (var Writer = new StreamWriter(SettingsFileName, false))
            {
                foreach (var Property in typeof(Settings).GetProperties())
                {
                    var Value = Property.GetValue(this);
                    Writer.WriteLine($"{Property.Name}={Value}");
                }
            }
        }

        public void UseDefaultSettings()
        {
            AutoRefresh = false;
            AlternareFont = false;
            FormatPrefs = FormatPrefs.None;
            SuperSecretSetting = false;
            EnableAnimations = true;
            Lang = "EN";
        }
    }
    public enum FormatPrefs
    {
        None = 0,
        LongText = 1,
        ShortText = 2
    }
}