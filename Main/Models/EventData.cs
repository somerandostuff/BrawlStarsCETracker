namespace Main.Models
{
    public class EventData
    {
        public double Progress { get; set; }
        public List<Milestone> Milestones { get; set; } = [];
    }

    public class Milestone
    {
        public double BarPercent { get; set; }
        public string? MilestoneLabel { get; set; }
    }
}
