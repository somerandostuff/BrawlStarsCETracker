using System.Windows;
using System.Windows.Threading;

namespace EventTrackerWPF.Librarbies
{
    public class CutsceneManager
    {
        private readonly Queue<CutsceneEvent> Events = new();
        private bool CutsceneIsActive = false;

        private DispatcherTimer Timer = new();
        private CutsceneEvent CurrentEvent = new();

        public void AddEvent(CutsceneEvent CutsceneEvent)
        {
            Events.Enqueue(CutsceneEvent);
        }
        
        public void Start()
        {
            if (CutsceneIsActive) return;
            CutsceneIsActive = true;
            ProcessNextEvent();
        }

        // Fine... I'm just gonna overload the function...
        public void Reset()
        {
            Timer.Stop();
            Timer.Tick -= Timer_Tick;
            Events.Clear();
            CutsceneIsActive = false;
        }

        public void Reset(bool KeepEvents)
        {
            Timer.Stop();
            Timer.Tick -= Timer_Tick;
            if (!KeepEvents) Events.Clear();
            CutsceneIsActive = false;
        }

        private void ProcessNextEvent()
        {
            if (Events.Count == 0)
            {
                CutsceneIsActive = false;
                return;
            }    
            
            CurrentEvent = Events.Dequeue();
            Timer = new DispatcherTimer
            {
                Interval = CurrentEvent.Delay
            };
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object? Sender, EventArgs Event)
        {
            Timer.Stop();
            Timer.Tick -= Timer_Tick;
            CurrentEvent.Action?.Invoke();
            ProcessNextEvent();
        }
    }

    public class CutsceneEvent
    {
        public TimeSpan Delay { get; set; }
        public Action? Action { get; set; }
    }
}
