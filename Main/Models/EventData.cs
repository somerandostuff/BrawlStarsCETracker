using System.Text.Json;

namespace Main.Models
{
    public class EventData
    {
        // My personal favorites are "Pregress Bar" & "Quich Checkpoint Mode"
        public decimal? Pregress { get; set; }

        public List<Milestone> Milestones { get; set; } = [];
    }

    public class Milestone
    {
        public byte BarPercent { get; set; }
        public string? MilestoneLabel { get; set; }
    }
}
