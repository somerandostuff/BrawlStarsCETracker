using System.IO;
using System.Windows;

namespace EventTrackerWPF.Librarbies
{
    public class Settings
    {
        public bool AutoRefresh { get; set; } = false;
        public bool AlternareFont { get; set; } = false;
        public FormatPrefs FormatPrefs { get; set; } = FormatPrefs.None;
        public bool SuperSecretSetting { get; set; } = false;
        public string? ThemeName { get; set; }
        public bool EnableAnimations { get; set; } = false;

        public void Load()
        {
            if (!File.Exists("SETTINGS.txt".ToLower()))
            {
                UseDefaultSettings();
                return;
            }

            foreach (var Line in File.ReadAllLines("SETTINGS.txt".ToLower()))
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
                    MessageBox.Show("Error while loading settings! The program will now use default settings instead.\n" +
                        "Exception type: " + Exc.GetType() + "\n" +
                        "Message: " + Exc.Message + "\n" +
                        "HResult: " + Exc.HResult + "\n", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    UseDefaultSettings();
                    Save();
                }
            }
        }

        public void Save()
        {
            using (var Writer = new StreamWriter("SETTINGS.txt", false))
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
        }
    }
    public enum FormatPrefs
    {
        None = 0,
        LongText = 1,
        ShortText = 2
    }
}