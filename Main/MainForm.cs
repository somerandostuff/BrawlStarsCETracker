using Main.Others;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class MainForm : Form
    {
        ulong KillsCached = 0;

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            int FunValue = Random.Shared.Next(0, 10);
            if (FunValue == 6)
            {
                BTN_FunButton.Visible = true;
            }

            LoadData();
        }
        private async void LoadData()
        {
            L_Status.Text = "Fetching...";
            var Data = await Utils.FetchData();
            ulong Kills = 0;
            uint EndlessStarrDropsGotten = 0;

            if (Data != null)
            {
                L_Status.Text = " ";

                int CurrentMilestoneIndex =
                    (int)Math.Floor(Data.Pregress.GetValueOrDefault() / (100 / (Data.Milestones.Count - 1)));

                string? MilestoneStartLabel =
                    Data.Milestones[CurrentMilestoneIndex].MilestoneLabel?.Split("B")[0];

                if (!string.IsNullOrWhiteSpace(MilestoneStartLabel))
                {
                    ulong MilestoneStart = 1_000_000_000 * ulong.Parse(MilestoneStartLabel!);

                    Kills =
                        (ulong)
                            (MilestoneStart +
                            1_000_000_000 *
                            (Data.Pregress.GetValueOrDefault() -
                             Data.Milestones[CurrentMilestoneIndex].BarPercent) * (Data.Milestones.Count - 1) / 100);
                }
                else
                {
                    MessageBox.Show("Couldn't recognize milestone!!!", "ALERT!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (Kills < 10_000_000_000)
                    ProgBar_KillCountTo10B.Value = (int)(Kills / 1_000);
                else ProgBar_KillCountTo10B.Value = ProgBar_KillCountTo10B.Maximum;

                L_KillCount.Text = Kills.ToString("#,##0");

                if (KillsCached != 0 && Chk_AutoRefresh.Checked)
                {
                    L_KillsAdded.Text = $"+{(Kills - KillsCached) / 30}";
                }

                if (Kills > 10_000_000_000)
                {
                    EndlessStarrDropsGotten = (uint)(Kills - 10_000_000_000) / 1_000_000_000 * 10;
                    L_BonusStarrs.Text = $"(+{EndlessStarrDropsGotten:#,##0} starr drops!)";
                }

                KillsCached = Kills;

            }
            else
            {
                MessageBox.Show("Nothing was found.", "Oh...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void LoadDataForAutoRefresh()
        {
            if (DateTime.UtcNow.Minute == 29 || DateTime.UtcNow.Minute == 59)
            {
                LoadData();
                L_Status.Text = "Next update in: 30min";
            }
            else
            {
                L_Status.Text = "Next update in: ";
                if (DateTime.UtcNow.Minute >= 0 && DateTime.UtcNow.Minute <= 28)
                {
                    L_Status.Text += $"{29 - DateTime.UtcNow.Minute}min";
                }
                else if (DateTime.UtcNow.Minute >= 30 && DateTime.UtcNow.Minute <= 58)
                {
                    L_Status.Text += $"{59 - DateTime.UtcNow.Minute}min";
                }
                else
                {
                    L_Status.Text += "N/A";
                }
            }
        }

        private void Timer_TimeLeft_Tick(object sender, EventArgs e)
        {
            var TimeCount = Utils.GetTimeLeft();
            ProgBar_TimeLeft.Value = ProgBar_TimeLeft.Maximum - (int)TimeCount.TotalSeconds;

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

        private void BTN_FunButton_Click(object sender, EventArgs e)
        {
            BTN_FunButton.Visible = false;

            DeltaruneTomorrow DtForm = new DeltaruneTomorrow();
            DtForm.Show();
        }

        private void Timer_AutoRefresh_Tick(object sender, EventArgs e)
        {
            LoadDataForAutoRefresh();
        }
        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LinkL_About_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("v1.0.4 (beta) -- updated on 16/4/2025\nMight contain lots of bugs because undocumented API moments lol\n\nMade by somerandostuff & xale, thankyou for the contributions!", "About tracker", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void L_KillCount_Click(object sender, EventArgs e)
        {
            if (KillsCached > 0)
            {
                string ClipboardText = $"# COMMUNITY EVENT REPORT\nWe're at around {KillsCached:#,##0} takedowns right now.";
                Clipboard.SetText(ClipboardText);

                MessageBox.Show("Copied to clipboard! Now paste it everywhere or something.", "Nice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Chk_AutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_AutoRefresh.Checked)
            {
                MessageBox.Show("Auto refresh has been enabled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BTN_Refresh.Enabled = false;
                Timer_AutoRefresh.Enabled = true;
                LoadDataForAutoRefresh();
            }
            else
            {
                BTN_Refresh.Enabled = true;
                Timer_AutoRefresh.Enabled = false;
                L_Status.Text = " ";
            }
        }
    }
}
