using DiscordRPC;
using Main.Models;
using Main.Others;
using Main.Properties;
using System.Drawing.Text;
using System.Net;
using System.Runtime.InteropServices;

namespace Main
{
    public partial class MortisTheMortalForm : Form
    {
        public PrivateFontCollection FontColl = new PrivateFontCollection();

        public FormatPrefs PrefOption = FormatPrefs.None;

        public long LastUpdatedPointSeconds = 0;

        public bool FetchedCorrectly = false;

        public long BootupTime = DateTimeOffset.Now.ToUnixTimeSeconds();

        public Font? LilitaOne;
        public Font? DeterminationMono;

        public List<EventData>? Data;

        public double MortisiKills = 0;
        public double MortisiDeaths = 0;

        public double MortisiKillsOld = 0;
        public double MortisiDeathsOld = 0;

        private static readonly DiscordRpcClient DiscordClient = new DiscordRpcClient(ClientID.Discord);

        public MortisTheMortalForm()
        {
            InitializeComponent();
        }

        private void MortisTheMortalForm_Load(object sender, EventArgs e)
        {
            InitializeTimer();
            LoadFont();
            InitializeRPC();
            FetchData();
        }

        private async void FetchData()
        {
            try
            {
                L_LastUpdated.Text = "Loading...";
                Data = await Utils.FetchDataMortisiEvent();

                FetchedCorrectly = true;
            }
            catch (Exception Exc)
            {
                L_LastUpdated.Text = "Load failed!!!";
                FetchedCorrectly = false;
                switch (Exc)
                {
                    case HttpRequestException:
                        {
                            if (Exc.HResult is -2147467259)
                            {
                                MessageBox.Show("No internet connection! Check your internet connection and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show($"An unknown error occurred! This might have happened because of connectivity issues.\n\nException: {Exc.GetType().Name}, HResult: {Exc.HResult}\nMessage: {Exc.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        }
                    case TaskCanceledException:
                        {
                            if (Exc.HResult is -2146233029)
                            {
                                MessageBox.Show("Cannot fetch news API -- the wait operation timed out.\nIt might have broken itself or is being down for the time being...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show($"An unknown error occurred! This might have happened because of connectivity issues.\n\nException: {Exc.GetType().Name}, HResult: {Exc.HResult}\nMessage: {Exc.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        }
                    default:
                        {
                            MessageBox.Show($"ERROR HAS HAPPEN. I REPEAT, ERROR HAS HAPPEN.\nException: {Exc.GetType().Name}, HResult: {Exc.HResult}\nMessage: {Exc.Message}", "Uh oh!!!!?!?!?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            {
                                var ErrorMsg = MessageBox.Show("Couldn't fetch data. Would you want to try again?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                if (ErrorMsg == DialogResult.Yes)
                                {
                                    // This is funny
                                    FetchData();
                                }
                            }
                        }
                        break;
                }
            }
            finally
            {
                if (Data != null)
                {
                    for (int Idx = 0; Idx < Data.Count; Idx++)
                    {
                        for (int Jdx = 0; Jdx < Data[Idx].Milestones.Count; Jdx++)
                        {
                            if (Jdx == 0 && Data[Idx].Progress < Data[Idx].Milestones[Jdx].BarPercent)
                            {
                                // Formula explained here
                                var StartPercent = 0;
                                var EndPercent = Data[Idx].Milestones[Jdx].BarPercent;

                                var StartCount = 0;
                                var EndCount = Utils.SimpleTextToNumber(Data[Idx].Milestones[Jdx].MilestoneLabel);

                                var RangePercent = EndPercent - StartPercent;
                                var ProgressToRangePercent = Data[Idx].Progress - StartPercent;

                                var RangeAmount = EndCount - StartCount;

                                if (Idx == 1)
                                {
                                    MortisiKills = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);
                                }
                                else
                                {
                                    MortisiDeaths = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);
                                }
                                break;
                            }
                            else if (Jdx == Data[Idx].Milestones.Count - 1 && Data[Idx].Progress > Data[Idx].Milestones[Jdx].BarPercent)
                            {
                                // This one is peculiar because this indicates that you have reached the final milestone...
                                double StartishCount = Utils.SimpleTextToNumber(Data[Idx].Milestones[Jdx - 1].MilestoneLabel);

                                var StartPercent = Data[Idx].Milestones[Jdx - 1].BarPercent;
                                var EndPercent = Data[Idx].Milestones[Jdx].BarPercent;

                                var StartCount = Utils.SimpleTextToNumber(Data[Idx].Milestones[Jdx].MilestoneLabel);

                                var RangePercent = EndPercent - StartPercent;
                                var ProgressToRangePercent = Data[Idx].Progress - EndPercent;

                                var RangeAmount = StartCount - StartishCount;

                                if (Idx == 1)
                                {
                                    MortisiKills = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);
                                }
                                else
                                {
                                    MortisiDeaths = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);
                                }
                                break;
                            }
                            else if (Jdx > 0 && Data[Idx].Progress > Data[Idx].Milestones[Jdx].BarPercent && Data[Idx].Progress <= Data[Idx].Milestones[Jdx + 1].BarPercent)
                            {
                                var StartPercent = Data[Idx].Milestones[Jdx].BarPercent;
                                var EndPercent = Data[Idx].Milestones[Jdx + 1].BarPercent;

                                var StartCount = Utils.SimpleTextToNumber(Data[Idx].Milestones[Jdx].MilestoneLabel);
                                var EndCount = Utils.SimpleTextToNumber(Data[Idx].Milestones[Jdx + 1].MilestoneLabel);

                                var RangePercent = EndPercent - StartPercent;
                                var ProgressToRangePercent = Data[Idx].Progress - StartPercent;

                                var RangeAmount = EndCount - StartCount;

                                if (Idx == 1)
                                {
                                    MortisiKills = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);
                                }
                                else
                                {
                                    MortisiDeaths = StartCount + RangeAmount * (ProgressToRangePercent / RangePercent);
                                }
                                break;
                            }
                        }
                    }
                }
            }

            UpdateLastRefresh();
            PopulateUI();
            SetPresenceMessage("Mortis kills: " + Utils.Beautify(MortisiKills, PrefOption), "Mortis deaths: " + Utils.Beautify(MortisiDeaths, PrefOption));
        }
        private void UpdateLastRefresh()
        {
            if ((MortisiKillsOld != MortisiKills || MortisiDeathsOld != MortisiDeaths) && FetchedCorrectly)
            {
                LastUpdatedPointSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();

                MortisiKillsOld = MortisiKills;
                MortisiDeathsOld = MortisiDeaths;
                L_LastUpdated.Text = "Last updated: " +
                    DateTimeOffset.FromUnixTimeSeconds(LastUpdatedPointSeconds).ToLocalTime().ToString("d/M/yyyy H:mm:ss [Pi]zz").Replace("[Pi]", "GMT");
            }
        }

        private void PopulateUI()
        {
            L_MortosKillCount.Text = Utils.Beautify(MortisiKills, PrefOption);
            L_MortosDieCount.Text = Utils.Beautify(MortisiDeaths, PrefOption);

            L_TotalKillsPercent.Text = Utils.BeautifyPercentage(MortisiKills / EventGoal.MortisiEvent);
            L_TotalDiesPercent.Text = Utils.BeautifyPercentage(MortisiDeaths / EventGoal.MortisiEvent);

            ProgBar_MortosKills.Value = (int)(
                MortisiKills >= EventGoal.MortisiEvent ?
                    ProgBar_MortosKills.Maximum : MortisiKills);

            ProgBar_MortisDies.Value = (int)(
                MortisiDeaths >= EventGoal.MortisiEvent ?
                    ProgBar_MortisDies.Maximum : MortisiDeaths);

            BTN_CopyAll.Enabled = true;
        }

        private void LoadFont()
        {
            LoadInternalFont(Resources.Fnt_LilitaOne_Regular);
            LilitaOne = new Font(FontColl.Families[0], 12);

            LoadInternalFont(Resources.Fnt_DeterminationMonoWebNew);
            DeterminationMono = new Font(FontColl.Families[0], 12);

            var LilitaFontSize12 = new Font(LilitaOne.FontFamily, 12);
            var LilitaFontSize20 = new Font(LilitaOne.FontFamily, 20);
            var LilitaFontSize32 = new Font(LilitaOne.FontFamily, 32);
            var LilitaFontSize36 = new Font(LilitaOne.FontFamily, 36);

            L_Title.Font = LilitaFontSize32;
            L_TimeLeft.Font = LilitaFontSize20;

            L_SplitContainer.Font = LilitaFontSize12;

            L_TimeLeftLabel.Font = LilitaFontSize12;

            BTN_Refresh.Font = LilitaFontSize12;
            Chk_AutoRefresh.Font = LilitaFontSize12;

            Chk_Altfont.Font = DeterminationMono;

            L_Version.Font = LilitaFontSize12;

            L_NumberFormatting.Font = LilitaFontSize12;
            Rdi_PrefNone.Font = LilitaFontSize12;
            Rdi_PrefShortened.Font = LilitaFontSize12;
            Rdi_PrefShortenedMore.Font = LilitaFontSize12;

            L_MortosKillCount.Font = LilitaFontSize36;
            L_MortosDieCount.Font = LilitaFontSize36;

            L_MortosKillCountSub.Font = LilitaFontSize12;
            L_MortosDieCountSub.Font = LilitaFontSize12;

            L_TotalKillsPercent.Font = LilitaFontSize12;
            L_TotalDiesPercent.Font = LilitaFontSize12;

            L_LastUpdated.Font = LilitaFontSize12;

            BTN_CopyAll.Font = LilitaFontSize12;
        }

        private void InitializeRPC()
        {
            try
            {
                DiscordClient.Initialize();
            }
            catch (Exception Exc)
            {
                var ErrorMessage =
                    MessageBox.Show($"Couldn't initialize Discord RPC! Do you want to proceed without it?\nError code: {Exc.GetType().Name}\nMessage: {Exc.Message}", "No RPC?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (ErrorMessage == DialogResult.Yes)
                {
                    InitializeRPC();
                }
                else DiscordClient.Dispose();
            }
            finally
            {
                if (DiscordClient.IsInitialized)
                {
                    DiscordClient.SetPresence(new()
                    {
                        Type = ActivityType.Watching,
                        Details = "Loading data...",
                        State = "Waiting...",
                        Buttons = [new() { Label = "chip", Url = "https://www.youtube.com/watch?v=WIRK_pGdIdA" }]
                    });
                }
            }
        }

        private void SetPresenceMessage(string Details, string State)
        {
            DiscordClient.UpdateDetails(Details);
            DiscordClient.UpdateState(State);
        }

        private void LoadInternalFont(byte[] FontData)
        {
            IntPtr FontPtr = Marshal.AllocCoTaskMem(FontData.Length);
            Marshal.Copy(FontData, 0, FontPtr, FontData.Length);
            uint Dummy = 0;

            FontColl.AddMemoryFont(FontPtr, FontData.Length);

            LoadFontIntoMemory.AddFontMemResourceEx(FontPtr, (uint)FontData.Length, 0, ref Dummy);

            Marshal.FreeCoTaskMem(FontPtr);
        }

        private void InitializeTimer()
        {
            ProgBar_TimeLeft.Maximum = (int)(EventTime.EventEndEpochTime - EventTime.EventStartEpochTime);
            Timer_TimeLeftCountdown.Enabled = true;
        }

        private void ShowAboutLabel()
        {
            L_Version.Text = "About tracker...";
        }

        private void HideAboutLabel()
        {
            L_Version.Text = "v1.0.7";
        }

        private void DisplayAboutTracker()
        {
            MessageBox.Show("v1.0.7 -- updated on 3/7/2025\n\nMade by somerandostuff & xale, thankyou for the contributions!\nPS: I Madeth this Packagethed Binary goodness, in just a Measly Four hours. If only I can lock in like this in the future...\n\nUptime: " + Utils.FormatTime(PrefOption, TimeSpan.FromSeconds(DateTimeOffset.Now.ToUnixTimeSeconds() - BootupTime)), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Chk_Altfont_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Altfont.Checked)
            {
                if (DeterminationMono != null)
                {
                    L_MortosKillCount.Font = new Font(DeterminationMono.FontFamily, 36);
                    L_MortosDieCount.Font = new Font(DeterminationMono.FontFamily, 36);
                    L_MortosKillCountSub.Font = new Font(DeterminationMono.FontFamily, 12);
                    L_MortosDieCountSub.Font = new Font(DeterminationMono.FontFamily, 12);
                }
                else
                {
                    MessageBox.Show("Monospace font error occurred! This function will now be unchecked.\nYou may restart the application if needed because this usually fixes the problem.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Chk_Altfont.Checked = false;
                }
            }
            else
            {
                if (LilitaOne != null)
                {
                    L_MortosKillCount.Font = new Font(LilitaOne.FontFamily, 36);
                    L_MortosDieCount.Font = new Font(LilitaOne.FontFamily, 36);
                    L_MortosKillCountSub.Font = new Font(LilitaOne.FontFamily, 12);
                    L_MortosDieCountSub.Font = new Font(LilitaOne.FontFamily, 12);
                }
                else
                {
                    MessageBox.Show("Font error occurred, impossible!!!\nBut the application is gonna use a fallback font anyway.", "How?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    L_MortosKillCount.Font = new Font("Arial", 36, FontStyle.Bold);
                    L_MortosDieCount.Font = new Font("Arial", 36, FontStyle.Bold);
                    L_MortosKillCountSub.Font = new Font("Arial", 12);
                    L_MortosDieCountSub.Font = new Font("Arial", 12);
                }
            }
        }

        private void Timer_TimeLeftCountdown_Tick(object sender, EventArgs e)
        {
            var TimeCount = Utils.GetTimeLeft();
            ProgBar_TimeLeft.Value = ProgBar_TimeLeft.Maximum - (int)TimeCount.TotalSeconds;

            L_TimeLeft.Text = Utils.FormatTime(PrefOption, TimeCount);
        }

        private void L_Version_Click(object sender, EventArgs e)
        {
            DisplayAboutTracker();
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

        private void L_Version_MouseEnter(object sender, EventArgs e)
        {
            ShowAboutLabel();
        }

        private void L_Version_MouseLeave(object sender, EventArgs e)
        {
            HideAboutLabel();
        }

        private void BTN_CopyAll_Click(object sender, EventArgs e)
        {
            string Header = "# COMMUNITY EVENT REPORT";
            string BodyMortisiKills = "**Mortis takedowns**: " + Utils.Beautify(MortisiKills, PrefOption);
            string BodyMortisiDeaths = "**Mortis deaths**: " + Utils.Beautify(MortisiDeaths, PrefOption);
            string Footer = "-# Last updated: " + DateTimeOffset.FromUnixTimeSeconds(LastUpdatedPointSeconds).ToLocalTime().ToString("d/M/yyyy H:mm:ss [Pi]zz").Replace("[Pi]", "GMT");

            try
            {
                Clipboard.SetText(Header + '\n' + BodyMortisiKills + '\n' + BodyMortisiDeaths + '\n' + Footer);
                MessageBox.Show("Copied to clipboard!", "Nice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Exc)
            {
                MessageBox.Show("Copy fail! This might be because the tracker is updating its data in the process, or your device simply just refused to copy it.\n\nClick on the number again to copy again.\n\nException code: " + Exc, "Um...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                BTN_Refresh.Enabled = true;
                Timer_Refresh.Enabled = false;
            }
        }

        private void Timer_Refresh_Tick(object sender, EventArgs e)
        {
            if (DateTimeOffset.Now.Minute % 4 == 3 && DateTimeOffset.Now.Second == 0 &&
            DateTimeOffset.Now.ToUnixTimeSeconds() - LastUpdatedPointSeconds >= 60)
            {
                FetchData();
            }
        }
    }
}
