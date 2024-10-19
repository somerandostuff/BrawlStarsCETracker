using System.Media;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Main
{
    public partial class MainForm : Form
    {
        string API_Route = "https://brawlstars.inbox.supercell.com/data/en/news/content.json";
        decimal DecimalValue = 0;
        ulong Count = 0, MaxCount = 0;
        Regex RegexAllTexts = new Regex(@"[\D]");
        Regex RegexAllNumbs = new Regex(@"[\d]");
        public MainForm()
        {
            InitializeComponent();
        }

        private void BTN_Load_Click(object sender, EventArgs e)
        {
            Fetcher();
        }

        public async void Fetcher()
        {
            L_Status.Text = "Fetching...";
            using (var Client = new HttpClient())
            {
                bool GetProgress, GetCountProgress;
                string Content = await Client.GetStringAsync(API_Route);
                JsonDocument JSONedDoc = JsonDocument.Parse(Content);
                JsonElement JSONedDoc_DecimalValueChild, JSONedDoc_AbbreviationChild;
                try
                {
                    for (int Tries = 0; Tries < 5; Tries++) // this is just to "check 5 times" by scanning 5 diff areas since it's also the max areas anyway
                    {
                        GetProgress = JSONedDoc.RootElement
                                    .GetProperty("entries")
                                    .GetProperty("eventEntries")[Tries]
                                    .TryGetProperty("tracker", out JSONedDoc_DecimalValueChild);
                        if (GetProgress == true)
                        {
                            JSONedDoc_DecimalValueChild.GetProperty("progress").TryGetDecimal(out DecimalValue);

                            #region EXPERIMENTAL CODE - MIGHT CHANGE LATER

                            GetCountProgress = JSONedDoc.RootElement
                                        .GetProperty("entries")
                                        .GetProperty("eventEntries")[Tries]
                                        .TryGetProperty("milestones", out JSONedDoc_AbbreviationChild);
                            if (GetCountProgress == true)
                            {
                                string GetAbbreviation = RegexAllNumbs.Replace(JSONedDoc_AbbreviationChild[2].GetProperty("label").GetString()!, string.Empty);
                                int GetPercentMile = JSONedDoc_AbbreviationChild[2].GetProperty("progress").GetInt32();
                                int GetAbbreviatedNumber = Convert.ToInt32(RegexAllTexts.Replace(JSONedDoc_AbbreviationChild[2].GetProperty("label").GetString()!, string.Empty));
                                switch (GetAbbreviation)
                                {
                                    case "B":
                                        MaxCount = (ulong)GetAbbreviatedNumber * 1_000_000_000 / (ulong)GetPercentMile * 100;
                                        break;
                                    case "M":

                                        MaxCount = (ulong)GetAbbreviatedNumber * 1_000_000 / (ulong)GetPercentMile * 100;
                                        break;
                                    case "T":
                                        MaxCount = (ulong)GetAbbreviatedNumber * 1_000_000_000_000 / (ulong)GetPercentMile * 100;
                                        break;
                                    default:
                                        MaxCount = 0;
                                        break;
                                }
                            }
                            #endregion

                            break;
                        }
                        else
                        {
                            if (Tries == 5)
                            {
                                MessageBox.Show("Data is unreachable!", "OOOOPS!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Data is unreachable!", "OOOOPS!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (DecimalValue > 0)
                {
                    if (MaxCount == 0)
                    {
                        MaxCount = 20_000_000_000;
                    }
                    Count = (ulong)(DecimalValue / 100 * MaxCount);
                    if (Count % 10_000 == 0)
                    {
                        if (Count >= 1_000_000 && Count <= 999_999_999)
                        {
                            L_ProgressCount.Text = ((decimal)Count / 1_000_000).ToString("#.##") + " million";
                        }
                        else if (Count >= 1_000_000_000 && Count <= 999_999_999_999)
                        {
                            L_ProgressCount.Text = ((decimal)Count / 1_000_000_000).ToString("#.##") + " billion";
                        }
                        else
                        {
                            L_ProgressCount.Text = Count.ToString("N0");
                        }
                    }
                    else
                    {
                        L_ProgressCount.Text = Count.ToString("N0");
                    }
                    L_Percent.Text = "(" + DecimalValue.ToString() + "%)";
                    L_LastUpd.Text = "Last refreshed: " + DateTime.Now.ToString("d/M/yyyy H:mm:ss");
                    BTN_Load.Enabled = false;

                    if (Chk_AutoRefresh.Checked == false)
                    {
                        L_Status.Text = "  ";
                    }
                }
                else
                {
                    MessageBox.Show("Couldn't find anything from API!\nOh well...", "OOOOPS!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    L_Status.Text = "Error!";
                    return;
                }
                if (DecimalValue >= 100)
                {
                    PlayFinishSound();
                    BTN_Load.Enabled = false;
                    MessageBox.Show("Event completed, GG! :)", "CONGRATULATIONS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (Chk_AutoRefresh.Checked == false)
                    {
                        MessageBox.Show("Fetching was successful!", "Nice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void PlayFinishSound()
        {
            Stream SoundStream = Properties.Resources.Snd_WorldRecord;
            SoundPlayer Player = new SoundPlayer(SoundStream);
            Player.Play();
        }

        private void BTN_About_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Simple community event tracker.\nMade by c&bt w/ help from xale", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer Refresher = new();
            Refresher.Interval = 60 * 1000;
            Refresher.Tick += AutoUpdaterAndButtonEnabler;
            Refresher.Start();
        }

        private void AutoUpdaterAndButtonEnabler(object? sender, EventArgs e)
        {
            AutoUpdaterChild();
        }

        private void AutoUpdaterChild()
        {
            // Fetch once every 30mins (at minute 28 and minute 58)
            if ((DateTime.Now.Minute == 28 || DateTime.Now.Minute == 58) && DecimalValue < 100)
            {
                if (Chk_AutoRefresh.Checked == false)
                {
                    BTN_Load.Enabled = true;
                }
                Fetcher();
                L_Status.Text = "Updated! Next update in 30min";
            }

            // Status updater
            // 58: 30
            // 59: 29
            // 0: 28
            // 1: 27

            if (DateTime.Now.Minute == 59)
            {
                L_Status.Text = "Next update in 29min";
                return;
            }
            if (DateTime.Now.Minute >= 0 && DateTime.Now.Minute <= 27)
            {
                L_Status.Text = "Next update in " + (28 - DateTime.Now.Minute) + "min";
                return;
            }
            if (DateTime.Now.Minute >= 29 && DateTime.Now.Minute <= 57)
            {
                L_Status.Text = "Next update in " + (58 - DateTime.Now.Minute) + "min";
                return;
            }
        }

        private void Chk_AutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_AutoRefresh.Checked == true)
            {
                MessageBox.Show("Auto-updater enabled! It'll auto-update when the minute is at 28 or 58.\n(Because the API updates once every 30mins and the update minute ends with 7 so yeah)", "Auto-update function enabled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BTN_Load.Enabled = false;
                if (Count == 0)
                {
                    Fetcher();
                }
                AutoUpdaterChild();
            }
            else
            {
                BTN_Load.Enabled = true;
                L_Status.Text = "  ";
            }
        }
    }
}
