using System.IO;
using System.Text;
using System.Windows;

namespace EventTrackerWPF.Librarbies
{
    public static class SaveSystem
    {
        public static long Gems { get; set; }
        public static Dictionary<long, double> TrackedResults { get; set; } = [];
        public static bool Egg { get; set; } = false;

        public static int Eggs { get; set; } = 0;

        // How "TrackedResults" work:
        // long value   = timepoint tracked in seconds (UTC)
        // double value = recorded event score in that timepoint

        private const string SaveFileName = "SaveData.dat";

        public static void Load()
        {
            if (!File.Exists(SaveFileName.ToLower()))
            {
                UseDefaultSettings();
                return;
            }

            string Data = string.Empty;

            try
            {
                Data = Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(SaveFileName.ToLower())));

                foreach (var Line in Data.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries))
                {
                    var ConfigParts = Line.Split('=', 2);
                    if (ConfigParts.Length != 2) continue;

                    var Property = typeof(SaveSystem).GetProperty(ConfigParts[0]);
                    if (Property == null) continue;

                    object Value = ConfigParts[1];

                    if (Property.PropertyType.IsEnum)
                    {
                        Value = Enum.Parse(Property.PropertyType, ConfigParts[1]);
                    }
                    else if (Property.PropertyType.IsGenericType && Property.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                    {
                        Value = TextToDictionary(ConfigParts[1]);
                    }
                    else Value = Convert.ChangeType(ConfigParts[1], Property.PropertyType);

                    Property.SetValue(null, Value);
                }
            }
            catch (Exception Exc)
            {
                var Message = new AlertMessage()
                {
                    Width = 1200,
                    Height = 675,

                    Title = "Error",
                    Description = "Error while loading save! The program will now use defaults instead.\n\n" +
                    "Exception type: " + Exc.GetType() + "\n" +
                    "Message: " + Exc.Message + "\n" +
                    "HResult: " + Exc.HResult + "\n" +
                    "String data:\n" + Data,

                    BlueButton = "OK",
                    BlueButtonFunc = (No, thing) => { MainWindow.SoundIndexer.PlaySoundID("btn_click"); }
                };

                Common.CreateAlert(Message);
                UseDefaultSettings();
                Save();
            }
        }

        public static void Save()
        {
            string Data = string.Empty;

            foreach (var Property in typeof(SaveSystem).GetProperties())
            {
                var Value = Property.GetValue(null);

                if (Property.PropertyType.IsGenericType && Property.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    Data += $"{Property.Name}={DictionaryToText((Dictionary<long, double>) Value!)}\n";
                }
                else Data += $"{Property.Name}={Value}\n";
            }

            using (var Writer = new StreamWriter(SaveFileName, false, Encoding.UTF8))
            {
                Writer.Write(Convert.ToBase64String(Encoding.UTF8.GetBytes(Data)));
            }
        }

        public static void UseDefaultSettings()
        {
            Gems = 10;
            TrackedResults = [];
            Egg = false;
        }

        private static string DictionaryToText(Dictionary<long, double> Dict)
        {
            return string.Join(';', Dict.Select(Kvp => $"{Kvp.Key},{Kvp.Value}"));
        }

        private static Dictionary<long, double> TextToDictionary(string Data)
        {
            var Dict = new Dictionary<long, double>();
            if (string.IsNullOrWhiteSpace(Data)) return Dict;

            foreach (var Pair in Data.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                var Parts = Pair.Split(',');
                if
                    (Parts.Length == 2 && long.TryParse(Parts[0], out var Time) &&
                                          double.TryParse(Parts[1], out var Value))
                {
                    Dict[Time] = Value;
                }
            }
            return Dict;
        }
    }
}
