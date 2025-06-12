namespace Main.Models.OldEventDatas
{
    public class EventData_202504
    {
        // My personal favorites are "Pregress Bar" & "Quich Checkpoint Mode"
        public decimal? Pregress { get; set; }

        public List<Milestone_202504> Milestones { get; set; } = [];
    }

    public class Milestone_202504
    {
        public byte BarPercent { get; set; }
        public string? MilestoneLabel { get; set; }
    }
}
