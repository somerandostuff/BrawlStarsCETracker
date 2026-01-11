using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows;

namespace EventTrackerWPF.Librarbies
{
    public class SoundEngine : IDisposable
    {
        private readonly IWavePlayer SoundOutput;
        private readonly MixingSampleProvider Mixer;

        private const byte MONO_CHANNEL_COUNT = 1;
        private const byte STEREO_CHANNEL_COUNT = 2;

        public static readonly SoundEngine Instance = new SoundEngine(SampleRate: 44100, ChannelCount: 2);

        private readonly Dictionary<string, ISampleProvider> ActiveLoopingSounds = [];

        public SoundEngine(int SampleRate = 44100, byte ChannelCount = STEREO_CHANNEL_COUNT)
        {
            SoundOutput = new WaveOutEvent();
            Mixer = new MixingSampleProvider
                (WaveFormat.CreateIeeeFloatWaveFormat(SampleRate, ChannelCount));
            Mixer.ReadFully = true;

            SoundOutput.Init(Mixer);
            SoundOutput.Play();
        }

        public void PlaySound(string FileName)
        {
            var Input = new AudioFileReader(FileName);
            AddMixerInput(new AutoDisposeFileReader(Input));
        }

        public void PlaySound(string FileName, uint Gain)
        {
            var Input = new AudioFileReader(FileName);
            var VolumeProvider = new VolumeSampleProvider(new AutoDisposeFileReader(Input)) { Volume = Gain / 100 };
            AddMixerInput(VolumeProvider);
        }

        public void PlaySound(string FileName, uint Gain, uint Pitch)
        {
            var Input = new AudioFileReader(FileName);
            var PitchProvider = new SmbPitchShiftingSampleProvider(new AutoDisposeFileReader(Input)) { PitchFactor = Pitch / 100 };
            var VolumeProvider = new VolumeSampleProvider(PitchProvider) { Volume = Gain / 100 };
            AddMixerInput(VolumeProvider);
        }

        public void PlaySoundLoop(string Key, string FileName)
        {
            var CachedSound = new CachedSound(FileName);
            var LoopingProvider = new LoopingCachedSoundSampleProvider(CachedSound);

            ActiveLoopingSounds[Key] = LoopingProvider;
            AddMixerInput(LoopingProvider);
        }

        public void PlaySoundLoop(string Key, string FileName, uint Gain)
        {
            var CachedSound = new CachedSound(FileName);
            var LoopingProvider = new LoopingCachedSoundSampleProvider(CachedSound);

            ISampleProvider Provider = new VolumeSampleProvider(LoopingProvider) { Volume = Gain / 100 };

            ActiveLoopingSounds[Key] = Provider;
            AddMixerInput(Provider);
        }

        public void PlaySoundLoop(string Key, string FileName, uint Gain, uint Pitch)
        {
            var CachedSound = new CachedSound(FileName);
            var LoopingProvider = new LoopingCachedSoundSampleProvider(CachedSound);

            ISampleProvider Provider = LoopingProvider;
            Provider = new SmbPitchShiftingSampleProvider(Provider) { PitchFactor = Pitch / 100 };
            Provider = new VolumeSampleProvider(Provider) { Volume = Gain / 100 };

            ActiveLoopingSounds[Key] = Provider;
            AddMixerInput(Provider);
        }

        public void StopSoundLoop(string Key)
        {
            if (ActiveLoopingSounds.TryGetValue(Key, out var ProviderTarget))
            {
                Mixer.RemoveMixerInput(ProviderTarget);
                ActiveLoopingSounds.Remove(Key);
            }
        }

        public void StopAllSoundLoops()
        {
            Mixer.RemoveAllMixerInputs();
            ActiveLoopingSounds.Clear();
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider Input)
        {
            if (Input.WaveFormat.Channels == Mixer.WaveFormat.Channels)
                return Input;
            if (Input.WaveFormat.Channels == MONO_CHANNEL_COUNT &&
                Mixer.WaveFormat.Channels == STEREO_CHANNEL_COUNT)
                return new MonoToStereoSampleProvider(Input);

            MessageBox.Show("This sound channel count has not yet been implemented." +
                "\nExpected 1 or 2, but got " + Input.WaveFormat.Channels + "!",
                "How did we get here...?", MessageBoxButton.OK, MessageBoxImage.Error);

            throw new NotImplementedException
                ("Sound channel count not yet implemented, got " +
                Input.WaveFormat.Channels + "instead of 1 or 2!");
        }

        private void AddMixerInput(ISampleProvider Input)
        {
            Mixer.AddMixerInput(ConvertToRightChannelCount(Input));
        }

        public void Dispose()
        {
            SoundOutput.Dispose();
        }
    }

    // ================================================================================ //

    public class AutoDisposeFileReader : ISampleProvider
    {
        private readonly AudioFileReader Reader;
        private bool IsDisposed;

        public WaveFormat WaveFormat { get; private set; }

        public AutoDisposeFileReader(AudioFileReader Reader)
        {
            this.Reader = Reader;
            WaveFormat = Reader.WaveFormat;
        }

        public int Read(float[] Buffer, int Offset, int Count)
        {
            if (IsDisposed) return 0;
            int Read = Reader.Read(Buffer, Offset, Count);
            if (Read == 0)
            {
                Reader.Dispose();
                IsDisposed = true;
            }
            return Read;
        }
    }

    // ================================================================================ //

    public class CachedSound
    {
        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }

        public CachedSound(string FileName)
        {
            using (var AudioFileReader = new AudioFileReader(FileName))
            {
                WaveFormat = AudioFileReader.WaveFormat;
                var WholeFile = new List<float>((int)(AudioFileReader.Length / 4));
                var ReadBuffer = new float
                    [AudioFileReader.WaveFormat.SampleRate *
                     AudioFileReader.WaveFormat.Channels];

                int SamplesRead;
                while ((SamplesRead = AudioFileReader.Read
                    (ReadBuffer, 0, ReadBuffer.Length))> 0)
                {
                    WholeFile.AddRange(ReadBuffer.Take(SamplesRead));
                }
                AudioData = WholeFile.ToArray();
            }
        }
    }

    // ================================================================================ //

    public class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound CachedSound;
        private long Position;

        public WaveFormat WaveFormat { get { return CachedSound.WaveFormat; } }

        public CachedSoundSampleProvider(CachedSound CachedSound)
        {
            this.CachedSound = CachedSound;
        }

        public int Read(float[] Buffer, int Offset, int Count)
        {
            var AvailableSamples = CachedSound.AudioData.Length - Position;
            var SamplesToCopy = Math.Min(AvailableSamples, Count);

            Array.Copy(CachedSound.AudioData, Position, Buffer, Offset, SamplesToCopy);
            Position += SamplesToCopy;
            return (int)SamplesToCopy;
        }

    }

    public class LoopingCachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound CachedSound;
        private long Position;

        public WaveFormat WaveFormat { get { return CachedSound.WaveFormat; } }

        public LoopingCachedSoundSampleProvider(CachedSound CachedSound)
        {
            this.CachedSound = CachedSound;
            Position = 0;
        }

        public int Read(float[] Buffer, int Offset, int Count)
        {
            int TotalSamplesWritten = 0;
            while (TotalSamplesWritten < Count)
            {
                var AvailableSamples = CachedSound.AudioData.Length - Position;
                var SamplesToCopy = Math.Min(AvailableSamples, Count - TotalSamplesWritten);

                Array.Copy(CachedSound.AudioData, Position, Buffer, Offset + TotalSamplesWritten, SamplesToCopy);

                Position += SamplesToCopy;
                TotalSamplesWritten += (int)SamplesToCopy;

                if (Position >= CachedSound.AudioData.Length)
                    Position = 0;
            }

            return TotalSamplesWritten;
        }
    }
}
