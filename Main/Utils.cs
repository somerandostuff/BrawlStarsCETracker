using Main.Models;
using Main.Others;
using System.Text.Json;

namespace Main
{
    public class Utils
    {
        public static async Task<EventData?> FetchData()
        {
            EventData Data = new EventData();
            using (var Client = new HttpClient())
            {
                var Content = JsonDocument.Parse(await Client.GetStringAsync(BrawlFeedLinks.NewsAPI));
                if (Content != null)
                {
                    for (int Tries = 0; Tries < 5; Tries++)
                    {
                        var EventData = Content.RootElement
                                              .GetProperty("events")[Tries];
                        Data.Pregress = EventData
                                       .GetProperty("tracker")
                                       .GetProperty("progress")
                                       .GetDecimal();

                        if (EventData.TryGetProperty("milestones", out EventData))
                        {
                            int TotalEventMilestones = EventData.GetArrayLength();
                            // 6 might be the maximum amount of milestones that can be displayed on screen
                            if (TotalEventMilestones < 6)
                            {
                                Data.Milestones.Add(new() { BarPercent = 0, MilestoneLabel = "0B" });
                            }
                            for (int Idx = 0; Idx < TotalEventMilestones; Idx++)
                            {
                                Milestone Milestone = new Milestone();

                                Milestone.MilestoneLabel = EventData[Idx].GetProperty("label").GetString();
                                Milestone.BarPercent = EventData[Idx].GetProperty("progress").GetByte();

                                Data.Milestones.Add(Milestone);
                            }
                            break;
                        }
                    }
                    return Data;
                }
                else return null;
            }
        }

        public static TimeSpan GetTimeLeft()
        {
            var Seconds = EventTime.EventEndEpochTime - DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return Seconds > 0 ? TimeSpan.FromSeconds(Seconds) : TimeSpan.Zero;
        }

        public static TimeSpan GetTimeLeftUntilDeltaruneIsReleased()
        {
            var Seconds = EventTime.DeltaruneReleaseTime - DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return Seconds > 0 ? TimeSpan.FromSeconds(Seconds) : TimeSpan.Zero;
        }

        /*
            EventData_202502 Data = new EventData_202502();
            List<Brawler> Brawlers = new List<Brawler>();

            using (var Client = new HttpClient())
            {
                byte NewsIndex = 0;

                var Content = JsonDocument.Parse(await Client.GetStringAsync(BrawlFeedLinks.NewsAPI));

                if (Content != null)
                {
                    for (byte Tries = 0; Tries < 5; Tries++)

                    {
                        var TryFindEventData = Content.RootElement
                                              .GetProperty("entries")
                                              .GetProperty("eventEntries")[Tries];

                        Data.EventMilestone = TryFindEventData
                                             .GetProperty("tracker")
                                             .GetProperty("progress")
                                             .GetInt32();

                        if (TryFindEventData.TryGetProperty("ctas", out TryFindEventData))
                        {
                            NewsIndex = Tries;

                            Data.PollID = TryFindEventData
                                         .GetProperty("6kjQMmOUtIi7L2adA3YlUI")
                                         .GetProperty("url")
                                         .GetProperty("id").GetString();
                            break;
                        }
                    }
                }
                else return null;
                if (Data.PollID == null) return null;

                var NewsPollData = Content.RootElement
                                  .GetProperty("entries")
                                  .GetProperty("eventEntries")[NewsIndex + 1];

                Data.PollTitle = NewsPollData.GetProperty("pollTitle").GetString();
                Data.CampaignID = NewsPollData.GetProperty("targeting").GetProperty("campaignId").GetString();
                Data.VotesGoal = Convert.ToUInt64(Data.CampaignID?.Split("-").Last());

                var PollOptionsProperty = NewsPollData.GetProperty("options");
                Data.AvailablePollChoices = PollOptionsProperty.GetArrayLength();

                foreach (var Option in PollOptionsProperty.EnumerateArray())
                {
                    Brawler NewBrawler = new Brawler();
                    NewBrawler.BrawlerName = Option.GetProperty("title").GetString();
                    NewBrawler.BrawlerImagePath =
                        Option.GetProperty("image")
                              .GetProperty("small")
                              .GetProperty("path")
                              .GetString()?.Split("?")[0] + $"?w=170&h=170&f=center&fit=fill";

                    NewBrawler.BrawlerImage = await Client.GetByteArrayAsync(BrawlFeedLinks.News + NewBrawler.BrawlerImagePath);

                    Brawlers.Add(NewBrawler);
                }

                var PollData = JsonDocument.Parse(await Client.GetStringAsync(BrawlFeedLinks.PollAPI + Data.PollID));
                Data.VotesSent = PollData.RootElement
                                         .GetProperty("data")
                                         .GetProperty("options")[0]
                                         .GetProperty("pollTotalVotes").GetUInt64();

                Data.Brawlers = Brawlers;

                return Data;
            }
         */
    }
}
