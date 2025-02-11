namespace Main.Models
{
    public class EventData
    {
        public string? PollID { get; set; }
        public string? CampaignID { get; set; }
        public string? PollTitle { get; set; }
        public int AvailablePollChoices { get; set; }
        public int EventMilestone { get; set; }
        public virtual List<Brawler> Brawlers { get; set; } = [];
        public ulong VotesSent { get; set; }
        public ulong VotesGoal { get; set; }
    }
}
