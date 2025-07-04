namespace Main
{
    partial class MortisTheMortalForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MortisTheMortalForm));
            L_TimeLeftLabel = new Label();
            ProgBar_TimeLeft = new ProgressBar();
            L_TimeLeft = new Label();
            L_Title = new Label();
            L_SplitContainer = new Label();
            Pnl_MortosKills = new Panel();
            L_TotalKillsPercent = new Label();
            L_MortosKillCountSub = new Label();
            ProgBar_MortosKills = new ProgressBar();
            L_MortosKillCount = new Label();
            Pnl_MortosDies = new Panel();
            L_TotalDiesPercent = new Label();
            L_MortosDieCountSub = new Label();
            ProgBar_MortisDies = new ProgressBar();
            L_MortosDieCount = new Label();
            L_NumberFormatting = new Label();
            Chk_Altfont = new CheckBox();
            Chk_AutoRefresh = new CheckBox();
            BTN_Refresh = new Button();
            Rdi_PrefShortenedMore = new RadioButton();
            Rdi_PrefShortened = new RadioButton();
            Rdi_PrefNone = new RadioButton();
            Timer_TimeLeftCountdown = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            L_Version = new Label();
            BTN_CopyAll = new Button();
            L_LastUpdated = new Label();
            Timer_Refresh = new System.Windows.Forms.Timer(components);
            Pnl_MortosKills.SuspendLayout();
            Pnl_MortosDies.SuspendLayout();
            SuspendLayout();
            // 
            // L_TimeLeftLabel
            // 
            L_TimeLeftLabel.AutoSize = true;
            L_TimeLeftLabel.BackColor = Color.Black;
            L_TimeLeftLabel.Font = new Font("Tahoma", 12F);
            L_TimeLeftLabel.Location = new Point(561, 19);
            L_TimeLeftLabel.Name = "L_TimeLeftLabel";
            L_TimeLeftLabel.Size = new Size(78, 19);
            L_TimeLeftLabel.TabIndex = 8;
            L_TimeLeftLabel.Text = "Time left:";
            // 
            // ProgBar_TimeLeft
            // 
            ProgBar_TimeLeft.BackColor = Color.Black;
            ProgBar_TimeLeft.Location = new Point(561, 44);
            ProgBar_TimeLeft.Margin = new Padding(3, 4, 3, 4);
            ProgBar_TimeLeft.Maximum = 1809000;
            ProgBar_TimeLeft.Name = "ProgBar_TimeLeft";
            ProgBar_TimeLeft.Size = new Size(276, 23);
            ProgBar_TimeLeft.Step = 1;
            ProgBar_TimeLeft.TabIndex = 7;
            // 
            // L_TimeLeft
            // 
            L_TimeLeft.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_TimeLeft.BackColor = Color.Black;
            L_TimeLeft.Font = new Font("Tahoma", 20F);
            L_TimeLeft.Location = new Point(621, 3);
            L_TimeLeft.Name = "L_TimeLeft";
            L_TimeLeft.Size = new Size(224, 42);
            L_TimeLeft.TabIndex = 9;
            L_TimeLeft.Text = "0:00:00";
            L_TimeLeft.TextAlign = ContentAlignment.MiddleRight;
            // 
            // L_Title
            // 
            L_Title.Anchor = AnchorStyles.Top;
            L_Title.BackColor = Color.Black;
            L_Title.Font = new Font("Tahoma", 32F);
            L_Title.Location = new Point(12, 9);
            L_Title.Name = "L_Title";
            L_Title.Size = new Size(543, 71);
            L_Title.TabIndex = 6;
            L_Title.Text = "Mortis VS. ALL (July 2025)";
            L_Title.TextAlign = ContentAlignment.TopCenter;
            // 
            // L_SplitContainer
            // 
            L_SplitContainer.AutoSize = true;
            L_SplitContainer.BackColor = Color.Black;
            L_SplitContainer.Location = new Point(-59, 61);
            L_SplitContainer.Name = "L_SplitContainer";
            L_SplitContainer.Size = new Size(1143, 19);
            L_SplitContainer.TabIndex = 10;
            L_SplitContainer.Text = "______________________________________________________________________________________________________________________________";
            // 
            // Pnl_MortosKills
            // 
            Pnl_MortosKills.BackColor = Color.Black;
            Pnl_MortosKills.Controls.Add(L_TotalKillsPercent);
            Pnl_MortosKills.Controls.Add(L_MortosKillCountSub);
            Pnl_MortosKills.Controls.Add(ProgBar_MortosKills);
            Pnl_MortosKills.Controls.Add(L_MortosKillCount);
            Pnl_MortosKills.Location = new Point(0, 83);
            Pnl_MortosKills.Name = "Pnl_MortosKills";
            Pnl_MortosKills.Size = new Size(425, 146);
            Pnl_MortosKills.TabIndex = 11;
            // 
            // L_TotalKillsPercent
            // 
            L_TotalKillsPercent.Anchor = AnchorStyles.Top;
            L_TotalKillsPercent.BackColor = Color.Black;
            L_TotalKillsPercent.ForeColor = Color.White;
            L_TotalKillsPercent.Location = new Point(12, 118);
            L_TotalKillsPercent.Name = "L_TotalKillsPercent";
            L_TotalKillsPercent.Size = new Size(407, 19);
            L_TotalKillsPercent.TabIndex = 3;
            L_TotalKillsPercent.Text = "0%";
            L_TotalKillsPercent.TextAlign = ContentAlignment.TopCenter;
            // 
            // L_MortosKillCountSub
            // 
            L_MortosKillCountSub.Anchor = AnchorStyles.Top;
            L_MortosKillCountSub.BackColor = Color.Black;
            L_MortosKillCountSub.Location = new Point(12, 70);
            L_MortosKillCountSub.Name = "L_MortosKillCountSub";
            L_MortosKillCountSub.Size = new Size(407, 19);
            L_MortosKillCountSub.TabIndex = 2;
            L_MortosKillCountSub.Text = "takedowns";
            L_MortosKillCountSub.TextAlign = ContentAlignment.TopCenter;
            // 
            // ProgBar_MortosKills
            // 
            ProgBar_MortosKills.BackColor = Color.Black;
            ProgBar_MortosKills.Location = new Point(12, 92);
            ProgBar_MortosKills.MarqueeAnimationSpeed = 50;
            ProgBar_MortosKills.Maximum = 300000000;
            ProgBar_MortosKills.Name = "ProgBar_MortosKills";
            ProgBar_MortosKills.Size = new Size(407, 23);
            ProgBar_MortosKills.Step = 1;
            ProgBar_MortosKills.TabIndex = 0;
            // 
            // L_MortosKillCount
            // 
            L_MortosKillCount.Anchor = AnchorStyles.Top;
            L_MortosKillCount.BackColor = Color.Black;
            L_MortosKillCount.Font = new Font("Tahoma", 36F);
            L_MortosKillCount.ForeColor = Color.Lime;
            L_MortosKillCount.Location = new Point(12, 0);
            L_MortosKillCount.Name = "L_MortosKillCount";
            L_MortosKillCount.Size = new Size(407, 77);
            L_MortosKillCount.TabIndex = 1;
            L_MortosKillCount.Text = "--";
            L_MortosKillCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Pnl_MortosDies
            // 
            Pnl_MortosDies.BackColor = Color.Black;
            Pnl_MortosDies.Controls.Add(L_TotalDiesPercent);
            Pnl_MortosDies.Controls.Add(L_MortosDieCountSub);
            Pnl_MortosDies.Controls.Add(ProgBar_MortisDies);
            Pnl_MortosDies.Controls.Add(L_MortosDieCount);
            Pnl_MortosDies.Location = new Point(425, 83);
            Pnl_MortosDies.Name = "Pnl_MortosDies";
            Pnl_MortosDies.Size = new Size(425, 146);
            Pnl_MortosDies.TabIndex = 12;
            // 
            // L_TotalDiesPercent
            // 
            L_TotalDiesPercent.Anchor = AnchorStyles.Top;
            L_TotalDiesPercent.BackColor = Color.Black;
            L_TotalDiesPercent.ForeColor = Color.White;
            L_TotalDiesPercent.Location = new Point(8, 118);
            L_TotalDiesPercent.Name = "L_TotalDiesPercent";
            L_TotalDiesPercent.Size = new Size(407, 19);
            L_TotalDiesPercent.TabIndex = 4;
            L_TotalDiesPercent.Text = "0%";
            L_TotalDiesPercent.TextAlign = ContentAlignment.TopCenter;
            // 
            // L_MortosDieCountSub
            // 
            L_MortosDieCountSub.Anchor = AnchorStyles.Top;
            L_MortosDieCountSub.BackColor = Color.Black;
            L_MortosDieCountSub.Location = new Point(8, 70);
            L_MortosDieCountSub.Name = "L_MortosDieCountSub";
            L_MortosDieCountSub.Size = new Size(407, 19);
            L_MortosDieCountSub.TabIndex = 5;
            L_MortosDieCountSub.Text = "deaths";
            L_MortosDieCountSub.TextAlign = ContentAlignment.TopCenter;
            // 
            // ProgBar_MortisDies
            // 
            ProgBar_MortisDies.BackColor = Color.Black;
            ProgBar_MortisDies.Location = new Point(8, 92);
            ProgBar_MortisDies.Maximum = 300000000;
            ProgBar_MortisDies.Name = "ProgBar_MortisDies";
            ProgBar_MortisDies.Size = new Size(407, 23);
            ProgBar_MortisDies.Step = 1;
            ProgBar_MortisDies.TabIndex = 3;
            // 
            // L_MortosDieCount
            // 
            L_MortosDieCount.Anchor = AnchorStyles.Top;
            L_MortosDieCount.BackColor = Color.Black;
            L_MortosDieCount.Font = new Font("Tahoma", 36F);
            L_MortosDieCount.ForeColor = Color.Red;
            L_MortosDieCount.Location = new Point(6, 0);
            L_MortosDieCount.Name = "L_MortosDieCount";
            L_MortosDieCount.Size = new Size(407, 77);
            L_MortosDieCount.TabIndex = 4;
            L_MortosDieCount.Text = "--";
            L_MortosDieCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // L_NumberFormatting
            // 
            L_NumberFormatting.AutoSize = true;
            L_NumberFormatting.BackColor = Color.Black;
            L_NumberFormatting.Location = new Point(558, 294);
            L_NumberFormatting.Name = "L_NumberFormatting";
            L_NumberFormatting.Size = new Size(151, 19);
            L_NumberFormatting.TabIndex = 108;
            L_NumberFormatting.Text = "Number formatting:";
            // 
            // Chk_Altfont
            // 
            Chk_Altfont.AutoSize = true;
            Chk_Altfont.BackColor = Color.Black;
            Chk_Altfont.Font = new Font("Consolas", 12F);
            Chk_Altfont.Location = new Point(217, 319);
            Chk_Altfont.Margin = new Padding(3, 4, 3, 4);
            Chk_Altfont.Name = "Chk_Altfont";
            Chk_Altfont.Size = new Size(100, 23);
            Chk_Altfont.TabIndex = 111;
            Chk_Altfont.Text = "Alt font";
            Chk_Altfont.UseVisualStyleBackColor = false;
            Chk_Altfont.CheckedChanged += Chk_Altfont_CheckedChanged;
            // 
            // Chk_AutoRefresh
            // 
            Chk_AutoRefresh.AutoSize = true;
            Chk_AutoRefresh.BackColor = Color.Black;
            Chk_AutoRefresh.Location = new Point(93, 317);
            Chk_AutoRefresh.Margin = new Padding(3, 4, 3, 4);
            Chk_AutoRefresh.Name = "Chk_AutoRefresh";
            Chk_AutoRefresh.Size = new Size(117, 23);
            Chk_AutoRefresh.TabIndex = 110;
            Chk_AutoRefresh.Text = "Auto-refresh";
            Chk_AutoRefresh.UseVisualStyleBackColor = false;
            Chk_AutoRefresh.CheckedChanged += Chk_AutoRefresh_CheckedChanged;
            // 
            // BTN_Refresh
            // 
            BTN_Refresh.BackColor = Color.Transparent;
            BTN_Refresh.Location = new Point(12, 310);
            BTN_Refresh.Margin = new Padding(3, 4, 3, 4);
            BTN_Refresh.Name = "BTN_Refresh";
            BTN_Refresh.Size = new Size(75, 28);
            BTN_Refresh.TabIndex = 109;
            BTN_Refresh.Text = "Refresh";
            BTN_Refresh.UseVisualStyleBackColor = false;
            // 
            // Rdi_PrefShortenedMore
            // 
            Rdi_PrefShortenedMore.AutoSize = true;
            Rdi_PrefShortenedMore.BackColor = Color.Black;
            Rdi_PrefShortenedMore.Location = new Point(729, 315);
            Rdi_PrefShortenedMore.Margin = new Padding(3, 4, 3, 4);
            Rdi_PrefShortenedMore.Name = "Rdi_PrefShortenedMore";
            Rdi_PrefShortenedMore.Size = new Size(111, 23);
            Rdi_PrefShortenedMore.TabIndex = 114;
            Rdi_PrefShortenedMore.Text = "Shortened+";
            Rdi_PrefShortenedMore.UseVisualStyleBackColor = false;
            Rdi_PrefShortenedMore.CheckedChanged += Rdi_PrefShortenedMore_CheckedChanged;
            // 
            // Rdi_PrefShortened
            // 
            Rdi_PrefShortened.AutoSize = true;
            Rdi_PrefShortened.BackColor = Color.Black;
            Rdi_PrefShortened.Location = new Point(626, 315);
            Rdi_PrefShortened.Margin = new Padding(3, 4, 3, 4);
            Rdi_PrefShortened.Name = "Rdi_PrefShortened";
            Rdi_PrefShortened.Size = new Size(99, 23);
            Rdi_PrefShortened.TabIndex = 113;
            Rdi_PrefShortened.Text = "Shortened";
            Rdi_PrefShortened.UseVisualStyleBackColor = false;
            Rdi_PrefShortened.CheckedChanged += Rdi_PrefShortened_CheckedChanged;
            // 
            // Rdi_PrefNone
            // 
            Rdi_PrefNone.AutoSize = true;
            Rdi_PrefNone.BackColor = Color.Black;
            Rdi_PrefNone.Checked = true;
            Rdi_PrefNone.Location = new Point(558, 315);
            Rdi_PrefNone.Margin = new Padding(3, 4, 3, 4);
            Rdi_PrefNone.Name = "Rdi_PrefNone";
            Rdi_PrefNone.Size = new Size(64, 23);
            Rdi_PrefNone.TabIndex = 112;
            Rdi_PrefNone.TabStop = true;
            Rdi_PrefNone.Text = "None";
            Rdi_PrefNone.UseVisualStyleBackColor = false;
            Rdi_PrefNone.CheckedChanged += Rdi_PrefNone_CheckedChanged;
            // 
            // Timer_TimeLeftCountdown
            // 
            Timer_TimeLeftCountdown.Tick += Timer_TimeLeftCountdown_Tick;
            // 
            // L_Version
            // 
            L_Version.Anchor = AnchorStyles.Top;
            L_Version.BackColor = Color.Black;
            L_Version.Font = new Font("Tahoma", 12F);
            L_Version.Location = new Point(338, 317);
            L_Version.Name = "L_Version";
            L_Version.Size = new Size(167, 19);
            L_Version.TabIndex = 107;
            L_Version.Text = "v1.0.7.1";
            L_Version.TextAlign = ContentAlignment.TopCenter;
            L_Version.Click += L_Version_Click;
            L_Version.MouseEnter += L_Version_MouseEnter;
            L_Version.MouseLeave += L_Version_MouseLeave;
            // 
            // BTN_CopyAll
            // 
            BTN_CopyAll.Enabled = false;
            BTN_CopyAll.Location = new Point(260, 235);
            BTN_CopyAll.Name = "BTN_CopyAll";
            BTN_CopyAll.Size = new Size(328, 28);
            BTN_CopyAll.TabIndex = 4;
            BTN_CopyAll.Text = "EVIL AND INTIMIDATING COPY BUTTON";
            BTN_CopyAll.UseVisualStyleBackColor = true;
            BTN_CopyAll.Click += BTN_CopyAll_Click;
            // 
            // L_LastUpdated
            // 
            L_LastUpdated.Location = new Point(12, 266);
            L_LastUpdated.Name = "L_LastUpdated";
            L_LastUpdated.Size = new Size(826, 28);
            L_LastUpdated.TabIndex = 115;
            L_LastUpdated.Text = "   ";
            L_LastUpdated.TextAlign = ContentAlignment.TopCenter;
            // 
            // Timer_Refresh
            // 
            Timer_Refresh.Interval = 1000;
            Timer_Refresh.Tick += Timer_Refresh_Tick;
            // 
            // MortisTheMortalForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(850, 351);
            Controls.Add(L_LastUpdated);
            Controls.Add(BTN_CopyAll);
            Controls.Add(L_NumberFormatting);
            Controls.Add(Chk_Altfont);
            Controls.Add(Chk_AutoRefresh);
            Controls.Add(BTN_Refresh);
            Controls.Add(Rdi_PrefShortenedMore);
            Controls.Add(Rdi_PrefShortened);
            Controls.Add(Rdi_PrefNone);
            Controls.Add(L_Version);
            Controls.Add(Pnl_MortosDies);
            Controls.Add(Pnl_MortosKills);
            Controls.Add(L_TimeLeftLabel);
            Controls.Add(ProgBar_TimeLeft);
            Controls.Add(L_TimeLeft);
            Controls.Add(L_SplitContainer);
            Controls.Add(L_Title);
            Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.HighlightText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MortisTheMortalForm";
            Text = "MORTIS FRENZY OR SOMETHING COMMUNITY EVENT COOL!!!";
            Load += MortisTheMortalForm_Load;
            Pnl_MortosKills.ResumeLayout(false);
            Pnl_MortosDies.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_TimeLeftLabel;
        private ProgressBar ProgBar_TimeLeft;
        private Label L_TimeLeft;
        private Label L_Title;
        private Label L_SplitContainer;
        private Panel Pnl_MortosKills;
        private Panel Pnl_MortosDies;
        private ProgressBar ProgBar_MortosKills;
        private Label L_MortosKillCount;
        private Label L_MortosKillCountSub;
        private Label L_MortosDieCountSub;
        private Label L_MortosDieCount;
        private ProgressBar ProgBar_MortisDies;
        private Label L_NumberFormatting;
        private CheckBox Chk_Altfont;
        private CheckBox Chk_AutoRefresh;
        private Button BTN_Refresh;
        private RadioButton Rdi_PrefShortenedMore;
        private RadioButton Rdi_PrefShortened;
        private RadioButton Rdi_PrefNone;
        private Label L_TotalKillsPercent;
        private Label L_TotalDiesPercent;
        private System.Windows.Forms.Timer Timer_TimeLeftCountdown;
        private System.Windows.Forms.Timer timer2;
        private Label L_Version;
        private Button BTN_CopyAll;
        private Label L_LastUpdated;
        private System.Windows.Forms.Timer Timer_Refresh;
    }
}