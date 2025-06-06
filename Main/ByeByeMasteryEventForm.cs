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

        double MasteryPointsDisplay = 0;

        double PerSecond = 0;

        bool FirstLoad = false;

        // IntelliSense keeps telling me to use "Ts" as the variable name
        // This is for logging amount of points/minute estimates (up to 5)
        readonly Queue<double> Ts = [];

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
                        else if (Idx > 0 && Data.Progress > Data.Milestones[Idx].BarPercent && Data.Progress <= Data.Milestones[Idx + 1].BarPercent)
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

                    // Because this function always execute on the first load, this variable here exists
                    // so any auto-refresh functions won't break out of nowhere
                    FirstLoad = true;
                }
            }
        }

        private void UpdateLastRefresh()
        {
            if (MasteryPointsOld != MasteryPoints)
            {
                LastUpdatedPointSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();

                GetSecondAverage();

                MasteryPointsOld = MasteryPoints;

                L_LastUpdated.Text = "Last updated: " +
                    DateTimeOffset.FromUnixTimeSeconds(LastUpdatedPointSeconds).ToLocalTime().ToString("d/M/yyyy H:mm:ss");
            }
        }

        private void GetSecondAverage()
        {
            if (FirstLoad)
            {
                Ts.Enqueue(MasteryPoints - MasteryPointsOld);
                if (Ts.Count > 5)
                {
                    Ts.Dequeue();
                }

                PerSecond = Ts.Average() / 30 / 60;

                ShowApproxLabel();

                MasteryPointsDisplay = MasteryPoints;

                Timer_ConstantlyRefreshing.Enabled = true;
            }
        }

        private void PopulateUI()
        {
            if (MasteryPoints < MasteryPointsDisplay)
            {
                L_PointsCount.Text = Utils.Beautify(MasteryPointsDisplay, PrefOption);
                L_PercentageToNextMilestone.Text = ((MasteryPointsDisplay - StartCount) / (EndCount - StartCount) * 100).ToString("0.##") + "%";
                Progbar_ToNextGoal.Value =
                    (int)((MasteryPointsDisplay - StartCount) / (EndCount - StartCount) * 100 * 1e7) > Progbar_ToNextGoal.Maximum ? Progbar_ToNextGoal.Maximum : (int)((MasteryPointsDisplay - StartCount) / (EndCount - StartCount) * 100 * 1e7);

                L_OrPercentage.Text = "... or " + (MasteryPointsDisplay / UltimateGoal * 100).ToString("0.##") + "%";
            }
            else
            {
                L_PointsCount.Text = Utils.Beautify(MasteryPoints, PrefOption);
                L_PercentageToNextMilestone.Text = (ProgressToRangePercent / RangePercent * 100).ToString("0.##") + "%";
                Progbar_ToNextGoal.Value =
                    (int)(ProgressToRangePercent / RangePercent * 100 * 1e7) > Progbar_ToNextGoal.Maximum ? Progbar_ToNextGoal.Maximum : (int)(ProgressToRangePercent / RangePercent * 100 * 1e7);

                L_OrPercentage.Text = "... or " + (MasteryPoints / UltimateGoal * 100).ToString("0.##") + "%";
            }

            if (PerSecond > 0)
            {
                L_PerSecond.Text = "Approx: +" + Utils.Beautify(PerSecond, PrefOption) + " PTs/s";
            }

            L_StartCount.Text = Utils.Beautify(StartCount, PrefOption);
            L_EndCount.Text = Utils.Beautify(EndCount, PrefOption);
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

        private void ShowApproxInfoLabel()
        {
            L_PerSecond.Text = "What does this mean?";
        }

        private void ShowApproxLabel()
        {
            L_PerSecond.Text = "Approx: +" + Utils.Beautify(PerSecond, PrefOption) + " PTs/s";
        }

        private void ShowAboutLabel()
        {
            L_Version.Text = "About tracker...";
            L_Version.Location = new Point(429, 58);
            L_Version.Size = new Size(114, 19);
        }

        private void HideAboutLabel()
        {
            L_Version.Text = "v1.0.6.1";
            L_Version.Location = new Point(478, 58);
            L_Version.Size = new Size(65, 19);
        }

        private bool CheckIfFontExists(string FontName)
        {
            using (var FontTest = new Font(FontName, 12))
            {
                if (FontTest.Name == FontName)
                    return true;
                else return false;
            }
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
            MessageBox.Show("v1.0.6.1 hotfix -- updated on 6/6/2025\n\nMade by somerandostuff & xale, thankyou for the contributions!", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            try
            {
                if (PerSecond > 0)
                {
                    var Confirmation =
                        MessageBox.Show($"Use display count?\nIf you select 'No', then the original count will be used.\n\n(Original count: {Utils.Beautify(MasteryPoints, PrefOption)} points!)", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (Confirmation == DialogResult.Yes)
                    {
                        Clipboard.SetText("# COMMUNITY EVENT REPORT\nWe are at " + Utils.Beautify(MasteryPointsDisplay, PrefOption) + " mastery points right now.");
                        MessageBox.Show("Copied display count to clipboard!", "Nice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Clipboard.SetText("# COMMUNITY EVENT REPORT\nWe are at " + Utils.Beautify(MasteryPoints, PrefOption) + " mastery points right now.");
                        MessageBox.Show("Copied original count to clipboard!", "Nice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    Clipboard.SetText("# COMMUNITY EVENT REPORT\nWe are at " + Utils.Beautify(MasteryPoints, PrefOption) + " mastery points right now.");
                    MessageBox.Show("Copied to clipboard!", "Nice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Copy fail! This might be because the tracker is updating its data in the process, or your device simply just refused to copy it.\n\nClick on the number again to copy again.", "Um...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show("Auto-refresh enabled! It will refresh once every 30 minutes.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var Confirmation = MessageBox.Show("Are you sure? The estimation count will be lost.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Confirmation == DialogResult.Yes)
                {
                    L_PerSecond.Text = "";

                    BTN_Refresh.Enabled = true;
                    Timer_Refresh.Enabled = false;

                    PerSecond = 0;

                    Ts.Clear();
                    Timer_ConstantlyRefreshing.Enabled = false;
                }
                else
                {
                     Chk_AutoRefresh.Checked = true;
                }
            }
        }

        private void Timer_Refresh_Tick(object sender, EventArgs e)
        {
            // If Date Time hits minute X or Y and updater
            // do not update for more than 1 minute then trigger this
            if ((DateTimeOffset.Now.Minute == 25 || DateTimeOffset.Now.Minute == 55) &&
                DateTimeOffset.Now.ToUnixTimeSeconds() - LastUpdatedPointSeconds >= 60)
            {
                FetchData();
            }
        }

        private void Timer_ConstantlyRefreshing_Tick(object sender, EventArgs e)
        {
            MasteryPointsDisplay += PerSecond / 62.5;

            L_PointsCount.Text = Utils.Beautify(MasteryPointsDisplay, PrefOption);
            L_PercentageToNextMilestone.Text = ((MasteryPointsDisplay - StartCount) / (EndCount - StartCount) * 100).ToString("0.##") + "%";
            Progbar_ToNextGoal.Value =
                (int)((MasteryPointsDisplay - StartCount) / (EndCount - StartCount) * 100 * 1e7) > Progbar_ToNextGoal.Maximum ? Progbar_ToNextGoal.Maximum : (int)((MasteryPointsDisplay - StartCount) / (EndCount - StartCount) * 100 * 1e7);

            L_OrPercentage.Text = "... or " + (MasteryPointsDisplay / UltimateGoal * 100).ToString("0.##") + "%";
        }

        private void L_PerSecond_MouseEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(L_PerSecond.Text))
            {
                ShowApproxInfoLabel();
            }
        }

        private void L_PerSecond_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(L_PerSecond.Text))
            {
                ShowApproxLabel();
            }
        }

        private void Chk_Altfont_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Altfont.Checked)
            {
                if (CheckIfFontExists("Cascadia Code"))
                {
                    L_PointsCount.Font = new Font("Cascadia Code", 40, FontStyle.Bold);
                }
                else if (CheckIfFontExists("Courier New"))
                {
                    L_PointsCount.Font = new Font("Courier New", 40, FontStyle.Bold);
                }
                else
                {
                    MessageBox.Show("Can't use this function: you don't have Courier New or Cascadia Code installed on your machine!", ":(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Chk_Altfont.Checked = false;
                }
            }
            else
            {
                L_PointsCount.Font = new Font("Lilita One", 48);
            }
        }

        private void L_PerSecond_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(L_PerSecond.Text))
            {
                MessageBox.Show("Estimate counter calculates the amount of points per second by getting the first fetched amount of points and then getting the second fetched amount of points to find the difference between the two amount of points, then divide the diff amount to get the approximated amount of points per second. The estimations will also get improved every half hour by doing the same thing but it will calculate the average of all points per second entries thus far.\n\n(Ain't reading allat)", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
