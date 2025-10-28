namespace EventTrackerWPF.Librarbies
{
    public class SoundLibrarby
    {
        private SoundEngine SFXEngine = new SoundEngine();
        private readonly Dictionary<string, string> SoundPaths = [];

        public void LoadSounds(Dictionary<string, string> SoundFiles)
        {
            foreach (var Kvp in SoundFiles)
            {
                SoundPaths[Kvp.Key] = Kvp.Value;
            }
        }

        public void PlaySoundID(string SoundID)
        {
            SFXEngine.PlaySound(SoundPaths[SoundID]);
        }

        public void PlayLoopingSoundID(string SoundID)
        {
            SFXEngine.PlaySoundLoop(SoundID, SoundPaths[SoundID]);
        }

        public void StopLoopingSoundID(string SoundID)
        {
            SFXEngine.StopSoundLoop(SoundID);
        }
    }
}