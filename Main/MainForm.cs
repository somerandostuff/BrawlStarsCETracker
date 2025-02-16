﻿using Main.Others;
using Main.Properties;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Main
{
    public partial class MainForm : Form
    {
        private PrivateFontCollection FontColl = new PrivateFontCollection();

        private Font? SmallFont;
        private Font? MediumFont;
        // private Font? LargeFont;

        ulong OldMilestone_Persistent = 0;
        TimeSpan LastFetchedPoint = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        public MainForm()
        {
            InitializeComponent();

            LoadExternalFont();

            Font = MediumFont;

            FetchData();

            EventTimeLeftUpdater.Enabled = true;
            EventTimeLeftUpdater.Interval = 1;

            LastUpdatedUpdater.Enabled = false;

            AutoUpdater.Enabled = false;
            AutoUpdater.Interval = 1000;

            VotesProgress.SetState(3);
        }
        private async void FetchData()
        {
            L_Status.ForeColor = Color.Yellow;
            L_Status.Text = "Fetching...";

            var EventData = await Utils.FetchData();
            if (EventData != null)
            {                
                L_EventName.Text = EventData.PollTitle;
                switch (EventData.AvailablePollChoices)
                {
                    case 3:
                        {
                            // Put images at center
                            L_Brawler2.Text = EventData.Brawlers[0].BrawlerName;
                            L_Brawler3.Text = EventData.Brawlers[1].BrawlerName;
                            L_Brawler4.Text = EventData.Brawlers[2].BrawlerName;

                            Pic_Brawler2.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[0].BrawlerImage ?? [])!;
                            Pic_Brawler3.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[1].BrawlerImage ?? [])!;
                            Pic_Brawler4.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[2].BrawlerImage ?? [])!;
                            break;
                        }
                    case 1:
                        {
                            // Put images at center
                            L_Brawler3.Text = EventData.Brawlers[0].BrawlerName;

                            Pic_Brawler3.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[1].BrawlerImage ?? [])!;
                            break;
                        }
                    case 2:
                        {
                            // First image at slot 2, second image at slot 4
                            L_Brawler2.Text = EventData.Brawlers[0].BrawlerName;
                            L_Brawler4.Text = EventData.Brawlers[1].BrawlerName;

                            Pic_Brawler2.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[0].BrawlerImage ?? [])!;
                            Pic_Brawler4.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[1].BrawlerImage ?? [])!;
                            break;
                        }
                    default:
                        {
                            L_Brawler1.Text = EventData.Brawlers[0].BrawlerName;
                            L_Brawler2.Text = EventData.Brawlers[1].BrawlerName;
                            L_Brawler3.Text = EventData.Brawlers[2].BrawlerName;
                            L_Brawler4.Text = EventData.Brawlers[3].BrawlerName;

                            Pic_Brawler1.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[0].BrawlerImage ?? [])!;
                            Pic_Brawler2.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[1].BrawlerImage ?? [])!;
                            Pic_Brawler3.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[2].BrawlerImage ?? [])!;
                            Pic_Brawler4.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[3].BrawlerImage ?? [])!;

                            if (EventData.AvailablePollChoices >= 5)
                            {
                                L_Brawler5.Text = EventData.Brawlers[4].BrawlerName;
                                Pic_Brawler5.Image = (Image)new ImageConverter().ConvertFrom(EventData.Brawlers[4].BrawlerImage ?? [])!;
                            }
                            break;
                        }
                }

                switch (EventData.EventMilestone)
                {
                    case 20:
                    case 30:
                        L_EventState.Text = "Extras: Double XP Event";
                        break;
                    case 40:
                    case 50:
                        L_EventState.Text = "Extras: Double XP Event + 50% Mastery Bonus";
                        break;
                    case 60:
                    case 70:
                        L_EventState.Text = "Extras: Double XP Event + 50% Mastery Bonus + Double Starr Drops";
                        break;
                    case 80:
                    case 90:
                    case 100:
                        L_EventState.Text = "Extras: Double XP Event + 100% Mastery Bonus + Double Starr Drops";
                        break;
                    default:
                        break;
                }

                if (EventData.VotesSent - OldMilestone_Persistent > 0 && OldMilestone_Persistent != 0)
                {
                    LastFetchedPoint = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    LastUpdatedUpdater.Enabled = true;

                    // Since every reset makes the calculation just roll back to +18 billion if the votes sent deducting the last updated old counter,
                    // this check will be used inside here.
                    // You can pretty much use it after getting how much votes have added in or put it inside the check but whatever...
                    // I suckass at explaining!!! :/
                    if (EventData.VotesSent < OldMilestone_Persistent)
                    {
                        int Seed = Random.Shared.Next(0, 10);
                        if (Seed == 6) { L_AddedVotes.Text = "(Restaurant...)"; } else L_AddedVotes.Text = "(Reset...)";
                    }
                    else L_AddedVotes.Text = $"(+{EventData.VotesSent - OldMilestone_Persistent:#,##0})";

                }
                

                VotesProgress.SetState(1);
                VotesProgress.Maximum = (int)EventData.VotesGoal;
                if (EventData.VotesSent >= EventData.VotesGoal)
                {
                    if (ChkBox_AutoRefresh.Checked)
                    {
                        L_EstimatedTimeSubtext.Text = "COMPLETED!!!";
                    }
                    L_VotesVoted.ForeColor = Color.FromArgb(0, 255, 0);
                    VotesProgress.Value = VotesProgress.Maximum;
                    VotesProgress.SetState(2);
                }
                else
                {
                    L_VotesVoted.ForeColor = Color.White;
                    if (ChkBox_AutoRefresh.Checked && OldMilestone_Persistent != 0)
                    {
                        ulong VotesGainedThisMinute = EventData.VotesSent - OldMilestone_Persistent;
                        if (VotesGainedThisMinute > 0)
                        {
                            var EstimatedFinishTimeSpanByMinutes = TimeSpan.FromMinutes((EventData.VotesGoal - EventData.VotesSent) / VotesGainedThisMinute);

                            if (EstimatedFinishTimeSpanByMinutes.TotalSeconds < 60)
                            {
                                L_EstimatedTimeSubtext.Text = $"ETA: Less than a minute!";
                            }
                            else if (EstimatedFinishTimeSpanByMinutes.TotalMinutes < 60)
                            {
                                L_EstimatedTimeSubtext.Text = $"ETA: {(int)EstimatedFinishTimeSpanByMinutes.TotalMinutes}m left";
                            }
                            else if (EstimatedFinishTimeSpanByMinutes.TotalHours < 24)
                            {
                                L_EstimatedTimeSubtext.Text = $"ETA: {(int)EstimatedFinishTimeSpanByMinutes.TotalHours}h {(int)EstimatedFinishTimeSpanByMinutes.TotalMinutes % 60}m left";
                            }
                            else
                            {
                                L_EstimatedTimeSubtext.Text = $"ETA: {(long)EstimatedFinishTimeSpanByMinutes.TotalDays}d {(int)EstimatedFinishTimeSpanByMinutes.TotalHours % 24}h left";
                            }
                        }
                    }
                    else
                    {
                        L_EstimatedTimeSubtext.Text = "";
                    }

                    VotesProgress.Value = (int)EventData.VotesSent;
                    VotesProgress.SetState(3);
                }

                L_VotesVoted.Text = EventData.VotesSent.ToString("#,##0");
                L_VotesSummit.Text = EventData.VotesGoal.ToString("#,##0");
                L_VotesPercent.Text = (EventData.VotesSent / (decimal)EventData.VotesGoal * 100).ToString("0.##").Replace('.', ',') + "%";

                L_Status.ForeColor = Color.FromArgb(0, 255, 0);
                L_Status.Text = "Idle";

                // Preserve current milestone for next update
                OldMilestone_Persistent = EventData.VotesSent;

                // Re-enable when fetch successfully (since if you fetch in startup fails, it'll disable itself)
                ChkBox_AutoRefresh.Enabled = true;
            }
            else
            {
                L_Status.ForeColor = Color.Red;
                L_Status.Text = "Failed :(";
                MessageBox.Show("Cannot find data from server!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ChkBox_AutoRefresh.Enabled = false;
                ChkBox_AutoRefresh.Checked = false;

                BTN_Refresh.Enabled = true;
            }
        }
        private void LoadExternalFont()
        {
            byte[] FontData = Resources.DeterminationMonoWebNew;
            IntPtr FontPointer = Marshal.AllocCoTaskMem(FontData.Length);
            Marshal.Copy(FontData, 0, FontPointer, FontData.Length);
            uint Dummy = 0;
            FontColl.AddMemoryFont(FontPointer, Resources.DeterminationMonoWebNew.Length);
            LoadFontIntoMemory.AddFontMemResourceEx(FontPointer, (uint)Resources.DeterminationMonoWebNew.Length, IntPtr.Zero, ref Dummy);
            Marshal.FreeCoTaskMem(FontPointer);

            SmallFont = new Font(FontColl.Families[0], 12);
            MediumFont = new Font(FontColl.Families[0], 24);
            // LargeFont = new Font(FontColl.Families[0], 36);

            L_TimeLeftContext.Font = SmallFont;
            L_Version.Font = SmallFont;
            L_Status.Font = SmallFont;
            Link_About.Font = SmallFont;
            L_VotesSentSubtext.Font = SmallFont;
            L_VotesPercent.Font = SmallFont;
            L_EventState.Font = SmallFont;
            L_AddedVotes.Font = SmallFont;
            L_LastUpdatedSubtext.Font = SmallFont;
            L_EstimatedTimeSubtext.Font = SmallFont;

            L_Brawler1.Font = SmallFont;
            L_Brawler2.Font = SmallFont;
            L_Brawler3.Font = SmallFont;
            L_Brawler4.Font = SmallFont;
            L_Brawler5.Font = SmallFont;
        }

        private void EventTimeLeftUpdater_Tick(object sender, EventArgs e)
        {
            var TimeCount = Utils.GetTimeLeft();
            TimeLeftProgress.Value = 864000 - (int)TimeCount.TotalSeconds;

            if (TimeCount >= TimeSpan.FromDays(1))
            {
                L_TimeLeft.Text = string.Format
                    ("{0:D1}:{1:D2}:{2:D2}:{3:D2}", TimeCount.Days, TimeCount.Hours, TimeCount.Minutes, TimeCount.Seconds);
            }
            else
            {
                L_TimeLeft.Text = string.Format
                    ("{0:D1}:{1:D2}:{2:D2}", TimeCount.Hours, TimeCount.Minutes, TimeCount.Seconds);
            }
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            FetchData();
        }

        private void AutoUpdater_Tick(object sender, EventArgs e)
        {
            // This'll be more accurate since the last check was kinda half-assed
            // (New data appears every minute since last check since cache has "max-age=60")
            if (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - LastFetchedPoint.TotalSeconds >= 60)
            {
                FetchData();
            }
        }

        private void ChkBox_AutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBox_AutoRefresh.Checked)
            {
                AutoUpdater.Enabled = true;
                BTN_Refresh.Enabled = false;
                MessageBox.Show("Auto-updater is enabled! It'll refresh every minute.", "Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                L_EstimatedTimeSubtext.Text = "";
                AutoUpdater.Enabled = false;
                BTN_Refresh.Enabled = true;
            }
        }

        private void Link_About_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("v1.0.3 -- updated on 16/2/2025\n\nMade by somerandostuff & xale, thankyou for the contributions!", "About tracker", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LastUpdatedUpdater_Tick(object sender, EventArgs e)
        {
            TimeSpan SpanSinceLastFetched = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds()) - LastFetchedPoint;

            if (SpanSinceLastFetched.TotalSeconds < 60)
            {
                L_LastUpdatedSubtext.Text = $"Updated {(int)SpanSinceLastFetched.TotalSeconds}s ago";
            }
            else if (SpanSinceLastFetched.TotalMinutes < 60)
            {
                L_LastUpdatedSubtext.Text = $"Updated {(int)SpanSinceLastFetched.TotalMinutes}m {(int)SpanSinceLastFetched.TotalSeconds % 60}s ago";
            }
            else if (SpanSinceLastFetched.TotalHours < 24)
            {
                L_LastUpdatedSubtext.Text = $"Updated {(int)SpanSinceLastFetched.TotalHours}h {(int)SpanSinceLastFetched.TotalMinutes % 60}m ago";
            }
            else
            {
                L_LastUpdatedSubtext.Text = $"Updated {(long)SpanSinceLastFetched.TotalDays}d {(int)SpanSinceLastFetched.TotalHours % 24}h ago";
            }
        }
    }
}
