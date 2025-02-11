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
            List<Brawler> Brawlers = new List<Brawler>();

            using (var Client = new HttpClient())
            {
                byte Index = 0;

                var Content = JsonDocument.Parse(await Client.GetStringAsync(BrawlFeedLinks.NewsAPI));

                if (Content != null)
                {
                    for (byte Tries = 0; Tries < 5; Tries++)
                    {
                        var TryFindEventData = Content.RootElement
                                              .GetProperty("entries")
                                              .GetProperty("eventEntries")[Tries];

                        if (TryFindEventData.TryGetProperty("ctas", out TryFindEventData))
                        {
                            Index = Tries;

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
                                  .GetProperty("eventEntries")[Index + 1];

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
        }

        public static TimeSpan GetTimeLeft()
        {
            var Seconds = EventTime.EventEndEpochTime - DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            return Seconds > 0 ? TimeSpan.FromSeconds(Seconds) : TimeSpan.Zero;
        }
    }
}
