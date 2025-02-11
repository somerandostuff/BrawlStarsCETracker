using Main.Others;
using Main.Properties;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Main
{
    public partial class MainForm : Form
    {
        private PrivateFontCollection FontColl = new PrivateFontCollection();

        private Font? SmallFont;
        private Font? MediumFont;
        // private Font? LargeFont;

        public MainForm()
        {
            InitializeComponent();

            LoadExternalFont();

            Font = MediumFont;

            FetchData();

            EventTimeLeftUpdater.Enabled = true;
            EventTimeLeftUpdater.Interval = 1;

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
                        L_EventState.Text = "Extras: Double XP Event";
                        break;
                    case 30:
                    case 40:
                        L_EventState.Text = "Extras: Double XP Event + 50% Mastery Bonus";
                        break;
                    case 50:
                    case 60:
                        L_EventState.Text = "Extras: Double XP Event + 50% Mastery Bonus + Double Starr Drops";
                        break;
                    case 70:
                    case 80:
                    case 90:
                    case 100:
                        L_EventState.Text = "Extras: Double XP Event + 50% Mastery Bonus + Double Starr Drops + 100% Mastery Bonus";
                        break;
                    default:
                        break;
                }

                VotesProgress.SetState(1);
                VotesProgress.Maximum = (int)EventData.VotesGoal;
                if (EventData.VotesSent >= EventData.VotesGoal)
                {
                    VotesProgress.Value = VotesProgress.Maximum;
                    VotesProgress.SetState(2);
                }
                else
                {
                    VotesProgress.Value = (int)EventData.VotesSent;
                    VotesProgress.SetState(3);
                }

                L_VotesVoted.Text = EventData.VotesSent.ToString("#,##0");
                L_VotesSummit.Text = EventData.VotesGoal.ToString("#,##0");
                L_VotesPercent.Text = (EventData.VotesSent / (decimal)EventData.VotesGoal * 100).ToString("#.##").Replace('.', ',') + "%";

                L_Status.ForeColor = Color.FromArgb(0, 255, 0);
                L_Status.Text = "Idle";
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
            if (DateTimeOffset.UtcNow.Second == 0)
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
                AutoUpdater.Enabled = false;
                BTN_Refresh.Enabled = true;
            }
        }

        private void Link_About_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("v1.0.3 -- updated on 11/2/2025\n\nMade by somerandostuff & xale, thankyou for the contributions!", "About tracker", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
