using System.IO;
using System.Text;
using System.Windows;

namespace EventTrackerWPF.Librarbies
{
    public class SaveSystem
    {
        public long Gems { get; set; }

        public const string SaveFileName = "SaveData.dat";

        public void Load()
        {
            if (!File.Exists(SaveFileName.ToLower()))
            {
                UseDefaultSettings();
                return;
            }

            try
            {
                string Data = Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(SaveFileName.ToLower())));

                foreach (var Line in Data.Split(';'))
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
                    else Value = Convert.ChangeType(ConfigParts[1], Property.PropertyType);

                    Property.SetValue(this, Value);
                }
            }
            catch (Exception Exc)
            {
                var Message = new AlertMessage()
                {
                    Title = "Error",
                    Description = "Error while loading save! The program will now use defaults instead.\n" +
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

        public void Save()
        {
            string Data = string.Empty;

            foreach (var Property in typeof(SaveSystem).GetProperties())
            {
                var Value = Property.GetValue(this);
                Data += ($"{Property.Name}={Value};");
            }

            using (var Writer = new StreamWriter(SaveFileName, false))
            {
                Writer.Write(Convert.ToBase64String(Encoding.UTF8.GetBytes(Data)));
            }
        }

        public void UseDefaultSettings()
        {
            Gems = 10;
        }
    }
}
