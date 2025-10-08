using System.IO;
using System.Windows;

namespace EventTrackerWPF.Librarbies
{
    public class LocalizationLib
    {
        public static Dictionary<string, string>? Strings { get; private set; }
        public static void Load(string FilePath) => Strings = LoadLocalization(FilePath);

        public static Dictionary<string, string> LoadLocalization(string FilePath)
        {
            var LocDict = new Dictionary<string, string>();
            foreach (var Line in File.ReadLines(FilePath).Skip(1))
            {
                var Parts = Line.Split(',', 2);

                Parts[0] = Parts[0].Replace("\"", string.Empty);
                Parts[1] = Parts[1].Replace("\"", string.Empty);

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
    }
}
