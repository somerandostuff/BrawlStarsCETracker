using Main.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class DeltaruneTomorrow : Form
    {
        public DeltaruneTomorrow()
        {
            InitializeComponent();
        }

        private void DeltaruneTomorrow_Load(object sender, EventArgs e)
        {
            SoundPlayer GasterDingsSound = new SoundPlayer(Resources.Snd_MysteryGo);
            GasterDingsSound.Play();
        }

        private void Timer_TimeLeft_Tick(object sender, EventArgs e)
        {
            var TimeCount = Utils.GetTimeLeftUntilDeltaruneIsReleased();

            L_Days.Text = TimeCount.Days.ToString("00");
            L_Hours.Text = TimeCount.Hours.ToString("00");
            L_Minutes.Text = TimeCount.Minutes.ToString("00");
            L_Seconds.Text = TimeCount.Seconds.ToString("00");
        }
    }
}
