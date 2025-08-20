using System.Windows.Threading;

namespace EventTrackerWPF.Librarbies
{
    public class CutsceneManager
    {
        private readonly Queue<CutsceneEvent> Events = new();
        private DispatcherTimer Timer = new();
        private CutsceneEvent CurrentEvent = new();

        public void AddEvent(CutsceneEvent CutsceneEvent)
        {
            Events.Enqueue(CutsceneEvent);
        }

        private void ProcessNextEvent()
        {
            if (Events.Count == 0) return;
            
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
