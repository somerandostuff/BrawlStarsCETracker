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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Main
{
    public partial class ByeByeMasteryEventForm : Form
    {
        FormatPrefs PrefOption = 0;
        long LastUpdatedPointSeconds = 0;

        double MasteryPoints = 0;
        double MasteryPointsOld = 0;

        double RangePercent = 0;
        double ProgressToRangePercent = 0;

        double StartCount = 0;
        double EndCount = 0;

        double StartPercent = 0;
        double EndPercent = 0;

        double UltimateGoal = 0;

        double RangeAmount = 0;

        public ByeByeMasteryEventForm()
        {
            InitializeComponent();
        }

        private async void FetchData()
        {
            L_OrPercentage.Text = "Loading...";

            var Data = await Utils.FetchData();

            if (Data != null)
            {
                if (Data.Milestones.Count > 0)
                {
                    UltimateGoal = Utils.SimpleTextToNumber(Data.Milestones.Last().MilestoneLabel);
                    for (int Idx = 0; Idx < Data.Milestones.Count; Idx++)
                    {
                        if (Idx == 0 && Data.Progress < Data.Milestones[Idx].BarPercent)
                        {
                            // Formula explained here
                            StartPercent = 0;
                            EndPercent = Data.Milestones[Idx].BarPercent;

                            StartCount = 0;
                            EndCount = Utils.SimpleTextToNumber(Data.Milestones[Idx].MilestoneLabel);

                            RangePercent = EndPercent - StartPercent;
                            ProgressToRangePercent = Data.Progress - EndPercent;

                            RangeAmount = EndCount - StartCount;

                            MasteryPoints = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);

                            break;
                        }
                        else if (Idx == Data.Milestones.Count - 1 && Data.Progress > Data.Milestones[Idx].BarPercent)
                        {
                            // This one is peculiar because this indicates that you have reached the final milestone...
                            double StartishCount = Utils.SimpleTextToNumber(Data.Milestones[Idx - 1].MilestoneLabel);

                            StartPercent = Data.Milestones[Idx - 1].BarPercent;
                            EndPercent = Data.Milestones[Idx].BarPercent;

                            StartCount = Utils.SimpleTextToNumber(Data.Milestones[Idx].MilestoneLabel);
                            EndCount = double.PositiveInfinity;

                            RangePercent = EndPercent - StartPercent;
                            ProgressToRangePercent = Data.Progress - EndPercent;

                            RangeAmount = StartCount - StartishCount;

                            MasteryPoints = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);

                            break;
                        }
                        else if (Idx > 0)
                        {
                            StartPercent = Data.Milestones[Idx].BarPercent;
                            EndPercent = Data.Milestones[Idx + 1].BarPercent;

                            StartCount = Utils.SimpleTextToNumber(Data.Milestones[Idx].MilestoneLabel);
                            EndCount = Utils.SimpleTextToNumber(Data.Milestones[Idx + 1].MilestoneLabel);

                            RangePercent = EndPercent - StartPercent;
                            ProgressToRangePercent = Data.Progress - StartPercent;

                            RangeAmount = EndCount - StartCount;

                            MasteryPoints = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);

                            break;
                        }
                    }
                    UpdateLastRefresh();
                    PopulateUI();
                }
            }
        }

        private void UpdateLastRefresh()
        {
            if (MasteryPointsOld != MasteryPoints)
            {
                LastUpdatedPointSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
                MasteryPointsOld = MasteryPoints;

                L_LastUpdated.Text = "Last updated: " +
                    DateTimeOffset.FromUnixTimeSeconds(LastUpdatedPointSeconds).ToLocalTime().ToString("d/M/yyyy H:mm:ss");
            }
        }

        private void PopulateUI()
        {
            L_PointsCount.Text = Utils.Beautify(MasteryPoints, PrefOption);
            L_StartCount.Text = Utils.Beautify(StartCount, PrefOption);
            L_EndCount.Text = Utils.Beautify(EndCount, PrefOption);

            L_PercentageToNextMilestone.Text = (ProgressToRangePercent / RangePercent * 100).ToString("#.##") + "%";

            Progbar_ToNextGoal.Value = (int)(ProgressToRangePercent / RangePercent * 100 * 1e7);

            L_OrPercentage.Text = "... or " + (MasteryPoints / UltimateGoal * 100).ToString("0.##") + "%";
        }

        private void FormatTime(TimeSpan TimeCount)
        {
            switch (PrefOption)
            {
                case FormatPrefs.None:
                default:
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
                    break;
                case FormatPrefs.LongText:
                    if (TimeCount >= TimeSpan.FromDays(1))
                    {
                        L_TimeLeft.Text = string.Format
                            ("{0:D1} " + (TimeCount.Days == 1 || TimeCount.Days == -1 ? "day" : "days") + " " +
                             "{1:D1} " + (TimeCount.Hours == 1 || TimeCount.Hours == -1 ? "hour" : "hours"), TimeCount.Days, TimeCount.Hours);
                    }
                    else if (TimeCount >= TimeSpan.FromHours(1))
                    {
                        L_TimeLeft.Text = string.Format
                            ("{0:D1} " + (TimeCount.Hours == 1 || TimeCount.Hours == -1 ? "hour" : "hours") + " " +
                             "{1:D1} " + (TimeCount.Minutes == 1 || TimeCount.Minutes == -1 ? "min" : "mins"), TimeCount.Hours, TimeCount.Minutes);
                    }
                    else
                    {
                        L_TimeLeft.Text = string.Format
                           ("{0:D1} " + (TimeCount.Minutes == 1 || TimeCount.Minutes == -1 ? "min" : "mins") + " " +
                            "{1:D1} " + (TimeCount.Minutes == 1 || TimeCount.Minutes == -1 ? "sec" : "secs"), TimeCount.Minutes, TimeCount.Seconds);
                    }
                    break;
                case FormatPrefs.ShortText:
                    if (TimeCount >= TimeSpan.FromDays(1))
                    {
                        L_TimeLeft.Text = string.Format
                            ("{0:D1}d" + " " +
                             "{1:D1}h", TimeCount.Days, TimeCount.Hours);
                    }
                    else if (TimeCount >= TimeSpan.FromHours(1))
                    {
                        L_TimeLeft.Text = string.Format
                            ("{0:D1}h" + " " +
                             "{1:D1}m", TimeCount.Hours, TimeCount.Minutes);
                    }
                    else
                    {
                        L_TimeLeft.Text = string.Format
                           ("{0:D1}m" + " " +
                            "{1:D1}s", TimeCount.Minutes, TimeCount.Seconds);
                    }
                    break;
            }
        }

        private void ShowAboutLabel()
        {
            L_Version.Text = "About tracker...";
            L_Version.Location = new Point(429, 58);
            L_Version.Size = new Size(114, 19);
        }

        private void HideAboutLabel()
        {
            L_Version.Text = "v1.0.6";
            L_Version.Location = new Point(490, 58);
            L_Version.Size = new Size(53, 19);
        }

        private void ByeByeMasteryEventForm_Load(object sender, EventArgs e)
        {
            FetchData();
        }

        private void L_Version_MouseEnter(object sender, EventArgs e)
        {
            ShowAboutLabel();
        }

        private void L_Version_MouseLeave(object sender, EventArgs e)
        {
            HideAboutLabel();
        }

        private void L_Version_Click(object sender, EventArgs e)
        {
            HideAboutLabel();
            MessageBox.Show("v1.0.6 -- updated on 5/6/2025\n\nMade by somerandostuff & xale, thankyou for the contributions!", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Timer_TimeLeftCountdown_Tick(object sender, EventArgs e)
        {
            var TimeCount = Utils.GetTimeLeft();
            ProgBar_TimeLeft.Value = ProgBar_TimeLeft.Maximum - (int)TimeCount.TotalSeconds;

            FormatTime(TimeCount);
        }

        private void Rdi_PrefNone_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdi_PrefNone.Checked)
            {
                PrefOption = FormatPrefs.None;
                PopulateUI();
            }

        }

        private void Rdi_PrefShortened_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdi_PrefShortened.Checked)
            {
                PrefOption = FormatPrefs.LongText;
                PopulateUI();
            }
        }

        private void Rdi_PrefShortenedMore_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdi_PrefShortenedMore.Checked)
            {
                PrefOption = FormatPrefs.ShortText;
                PopulateUI();
            }
        }

        private void L_PointsCount_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("# COMMUNITY EVENT REPORT\nWe are at " + Utils.Beautify(MasteryPoints, PrefOption) + " mastery points right now.");
            MessageBox.Show("Copied to clipboard!", "Nice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BTN_Refresh_Click(object sender, EventArgs e)
        {
            FetchData();
        }

        private void Chk_AutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_AutoRefresh.Checked)
            {
                Timer_Refresh.Enabled = true;
                BTN_Refresh.Enabled = false;
                MessageBox.Show("Auto-refresh enabled!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            { 
                BTN_Refresh.Enabled = true;
                Timer_Refresh.Enabled = false;
            }
        }

        private void Timer_Refresh_Tick(object sender, EventArgs e)
        {
            // If Date Time hits minute X or Y and updater
            // do not update for more than 1 minute then trigger this
            if ((DateTimeOffset.Now.Minute == 19 || DateTimeOffset.Now.Minute == 49) &&
                DateTimeOffset.Now.ToUnixTimeSeconds() - LastUpdatedPointSeconds >= 60)
            {
                FetchData();
            }
        }
    }
}
