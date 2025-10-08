using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EventTrackerWPF.Librarbies
{
    public class Common
    {
        public static async Task<double> FetchDataSimple()
        {
            using (var Client = new HttpClient() { Timeout = TimeSpan.FromSeconds(8) })
            {
                var Response = await Client.GetAsync(BrawlFeedLinks.NewsAPI);
                if (Response.IsSuccessStatusCode)
                {
                    string JSONContent = await Response.Content.ReadAsStringAsync();
                    if (JSONContent != null)
                    {
                        var Content = JsonDocument.Parse(JSONContent);
                        {
                            for (int Tries = 0; Tries < 5; Tries++)
                            {
                                var EventData = Content.RootElement
                                                      .GetProperty("events")[Tries];

                                if (EventData.TryGetProperty("milestones", out JsonElement EventDataChild))
                                {
                                    return EventData
                                                   .GetProperty("tracker")
                                                   .GetProperty("progress")
                                                   .GetDouble();
                                }
                            }
                            return 0.0;
                        }
                    }
                    else return 0.0;
                }
                else
                {
                    MessageBox.Show("Error occurred: " + Response.StatusCode);
                    return 0.0;
                }
            }
        }

        public static void CreateAlert(AlertMessage Message)
        {
            var AlertWindow = new AlertWindow(Message);
            AlertWindow.ShowDialog();
        }
        
    }
    public class BrawlFeedLinks
    {
        // W/O Parameters...
        public const string NewsAPI = "https://brawlstars.inbox.supercell.com/data/en/news/content.json";
        public const string News = "https://brawlstars.inbox.supercell.com";
        public const string PollAPI = "https://brawlstars-api.inbox.supercell.com/poll-api/poll/?pollId=";
    }

    public class AlertMessage
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? RedButton { get; set; }
        public string? BlueButton { get; set; }
        public MouseButtonEventHandler? RedButtonFunc { get; set; }
        public MouseButtonEventHandler? BlueButtonFunc { get; set; }
    }
}
