namespace Main
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            L_EventName = new Label();
            L_TimeLeft = new Label();
            EventTimeLeftUpdater = new System.Windows.Forms.Timer(components);
            L_TimeLeftContext = new Label();
            Pic_Brawler1 = new PictureBox();
            Pic_Brawler2 = new PictureBox();
            Pic_Brawler3 = new PictureBox();
            Pic_Brawler4 = new PictureBox();
            Pic_Brawler5 = new PictureBox();
            L_Brawler1 = new Label();
            L_Brawler2 = new Label();
            L_Brawler3 = new Label();
            L_Brawler4 = new Label();
            L_Brawler5 = new Label();
            VotesProgress = new ProgressBar();
            L_VotesSentSubtext = new Label();
            L_VotesVoted = new Label();
            L_VotesSummit = new Label();
            L_VotesPercent = new Label();
            L_Version = new Label();
            BTN_Refresh = new Button();
            L_Status = new Label();
            AutoUpdater = new System.Windows.Forms.Timer(components);
            ChkBox_AutoRefresh = new CheckBox();
            Link_About = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler5).BeginInit();
            SuspendLayout();
            // 
            // L_EventName
            // 
            L_EventName.AutoSize = true;
            L_EventName.Font = new Font("Determination Mono Web", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            L_EventName.Location = new Point(12, 10);
            L_EventName.Margin = new Padding(5, 0, 5, 0);
            L_EventName.Name = "L_EventName";
            L_EventName.Size = new Size(31, 33);
            L_EventName.TabIndex = 0;
            L_EventName.Text = " ";
            // 
            // L_TimeLeft
            // 
            L_TimeLeft.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_TimeLeft.Location = new Point(715, 10);
            L_TimeLeft.Margin = new Padding(4, 0, 4, 0);
            L_TimeLeft.Name = "L_TimeLeft";
            L_TimeLeft.RightToLeft = RightToLeft.No;
            L_TimeLeft.Size = new Size(175, 33);
            L_TimeLeft.TabIndex = 1;
            L_TimeLeft.Text = "0:00:00";
            L_TimeLeft.TextAlign = ContentAlignment.TopRight;
            // 
            // EventTimeLeftUpdater
            // 
            EventTimeLeftUpdater.Tick += EventTimeLeftUpdater_Tick;
            // 
            // L_TimeLeftContext
            // 
            L_TimeLeftContext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_TimeLeftContext.AutoSize = true;
            L_TimeLeftContext.Location = new Point(622, 23);
            L_TimeLeftContext.Margin = new Padding(4, 0, 4, 0);
            L_TimeLeftContext.Name = "L_TimeLeftContext";
            L_TimeLeftContext.RightToLeft = RightToLeft.No;
            L_TimeLeftContext.Size = new Size(175, 33);
            L_TimeLeftContext.TabIndex = 2;
            L_TimeLeftContext.Text = "Time left:";
            L_TimeLeftContext.TextAlign = ContentAlignment.TopRight;
            // 
            // Pic_Brawler1
            // 
            Pic_Brawler1.Location = new Point(14, 87);
            Pic_Brawler1.Margin = new Padding(4, 3, 4, 3);
            Pic_Brawler1.Name = "Pic_Brawler1";
            Pic_Brawler1.Size = new Size(170, 170);
            Pic_Brawler1.TabIndex = 3;
            Pic_Brawler1.TabStop = false;
            // 
            // Pic_Brawler2
            // 
            Pic_Brawler2.Location = new Point(190, 87);
            Pic_Brawler2.Margin = new Padding(4, 3, 4, 3);
            Pic_Brawler2.Name = "Pic_Brawler2";
            Pic_Brawler2.Size = new Size(170, 170);
            Pic_Brawler2.TabIndex = 4;
            Pic_Brawler2.TabStop = false;
            // 
            // Pic_Brawler3
            // 
            Pic_Brawler3.Location = new Point(366, 87);
            Pic_Brawler3.Margin = new Padding(4, 3, 4, 3);
            Pic_Brawler3.Name = "Pic_Brawler3";
            Pic_Brawler3.Size = new Size(170, 170);
            Pic_Brawler3.TabIndex = 5;
            Pic_Brawler3.TabStop = false;
            // 
            // Pic_Brawler4
            // 
            Pic_Brawler4.Location = new Point(542, 87);
            Pic_Brawler4.Margin = new Padding(4, 3, 4, 3);
            Pic_Brawler4.Name = "Pic_Brawler4";
            Pic_Brawler4.Size = new Size(170, 170);
            Pic_Brawler4.TabIndex = 6;
            Pic_Brawler4.TabStop = false;
            // 
            // Pic_Brawler5
            // 
            Pic_Brawler5.Location = new Point(718, 87);
            Pic_Brawler5.Margin = new Padding(4, 3, 4, 3);
            Pic_Brawler5.Name = "Pic_Brawler5";
            Pic_Brawler5.Size = new Size(170, 170);
            Pic_Brawler5.TabIndex = 7;
            Pic_Brawler5.TabStop = false;
            // 
            // L_Brawler1
            // 
            L_Brawler1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_Brawler1.AutoSize = true;
            L_Brawler1.Location = new Point(14, 69);
            L_Brawler1.Margin = new Padding(4, 0, 4, 0);
            L_Brawler1.Name = "L_Brawler1";
            L_Brawler1.RightToLeft = RightToLeft.No;
            L_Brawler1.Size = new Size(31, 33);
            L_Brawler1.TabIndex = 8;
            L_Brawler1.Text = " ";
            L_Brawler1.TextAlign = ContentAlignment.TopRight;
            // 
            // L_Brawler2
            // 
            L_Brawler2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_Brawler2.AutoSize = true;
            L_Brawler2.Location = new Point(190, 69);
            L_Brawler2.Margin = new Padding(4, 0, 4, 0);
            L_Brawler2.Name = "L_Brawler2";
            L_Brawler2.RightToLeft = RightToLeft.No;
            L_Brawler2.Size = new Size(31, 33);
            L_Brawler2.TabIndex = 9;
            L_Brawler2.Text = " ";
            L_Brawler2.TextAlign = ContentAlignment.TopRight;
            // 
            // L_Brawler3
            // 
            L_Brawler3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_Brawler3.AutoSize = true;
            L_Brawler3.Location = new Point(366, 69);
            L_Brawler3.Margin = new Padding(4, 0, 4, 0);
            L_Brawler3.Name = "L_Brawler3";
            L_Brawler3.RightToLeft = RightToLeft.No;
            L_Brawler3.Size = new Size(31, 33);
            L_Brawler3.TabIndex = 10;
            L_Brawler3.Text = " ";
            L_Brawler3.TextAlign = ContentAlignment.TopRight;
            // 
            // L_Brawler4
            // 
            L_Brawler4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_Brawler4.AutoSize = true;
            L_Brawler4.Location = new Point(542, 69);
            L_Brawler4.Margin = new Padding(4, 0, 4, 0);
            L_Brawler4.Name = "L_Brawler4";
            L_Brawler4.RightToLeft = RightToLeft.No;
            L_Brawler4.Size = new Size(31, 33);
            L_Brawler4.TabIndex = 11;
            L_Brawler4.Text = " ";
            L_Brawler4.TextAlign = ContentAlignment.TopRight;
            // 
            // L_Brawler5
            // 
            L_Brawler5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_Brawler5.AutoSize = true;
            L_Brawler5.Location = new Point(718, 69);
            L_Brawler5.Margin = new Padding(4, 0, 4, 0);
            L_Brawler5.Name = "L_Brawler5";
            L_Brawler5.RightToLeft = RightToLeft.No;
            L_Brawler5.Size = new Size(31, 33);
            L_Brawler5.TabIndex = 12;
            L_Brawler5.Text = " ";
            L_Brawler5.TextAlign = ContentAlignment.TopRight;
            // 
            // VotesProgress
            // 
            VotesProgress.ForeColor = Color.Red;
            VotesProgress.Location = new Point(12, 320);
            VotesProgress.Margin = new Padding(4, 3, 4, 3);
            VotesProgress.Maximum = 2000000;
            VotesProgress.Name = "VotesProgress";
            VotesProgress.Size = new Size(878, 27);
            VotesProgress.Step = 1;
            VotesProgress.Style = ProgressBarStyle.Continuous;
            VotesProgress.TabIndex = 13;
            // 
            // L_VotesSentSubtext
            // 
            L_VotesSentSubtext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_VotesSentSubtext.AutoSize = true;
            L_VotesSentSubtext.Location = new Point(12, 302);
            L_VotesSentSubtext.Margin = new Padding(4, 0, 4, 0);
            L_VotesSentSubtext.Name = "L_VotesSentSubtext";
            L_VotesSentSubtext.RightToLeft = RightToLeft.No;
            L_VotesSentSubtext.Size = new Size(175, 33);
            L_VotesSentSubtext.TabIndex = 14;
            L_VotesSentSubtext.Text = "votes sent";
            L_VotesSentSubtext.TextAlign = ContentAlignment.TopRight;
            // 
            // L_VotesVoted
            // 
            L_VotesVoted.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_VotesVoted.AutoSize = true;
            L_VotesVoted.Location = new Point(12, 269);
            L_VotesVoted.Margin = new Padding(4, 0, 4, 0);
            L_VotesVoted.Name = "L_VotesVoted";
            L_VotesVoted.RightToLeft = RightToLeft.No;
            L_VotesVoted.Size = new Size(31, 33);
            L_VotesVoted.TabIndex = 15;
            L_VotesVoted.Text = "0";
            L_VotesVoted.TextAlign = ContentAlignment.TopRight;
            // 
            // L_VotesSummit
            // 
            L_VotesSummit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_VotesSummit.Location = new Point(622, 269);
            L_VotesSummit.Margin = new Padding(4, 0, 4, 0);
            L_VotesSummit.Name = "L_VotesSummit";
            L_VotesSummit.RightToLeft = RightToLeft.No;
            L_VotesSummit.Size = new Size(268, 33);
            L_VotesSummit.TabIndex = 16;
            L_VotesSummit.Text = "0";
            L_VotesSummit.TextAlign = ContentAlignment.TopRight;
            // 
            // L_VotesPercent
            // 
            L_VotesPercent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_VotesPercent.Location = new Point(628, 302);
            L_VotesPercent.Margin = new Padding(4, 0, 4, 0);
            L_VotesPercent.Name = "L_VotesPercent";
            L_VotesPercent.RightToLeft = RightToLeft.No;
            L_VotesPercent.Size = new Size(263, 16);
            L_VotesPercent.TabIndex = 17;
            L_VotesPercent.Text = "0,00%";
            L_VotesPercent.TextAlign = ContentAlignment.TopRight;
            // 
            // L_Version
            // 
            L_Version.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_Version.AutoSize = true;
            L_Version.Location = new Point(12, 389);
            L_Version.Margin = new Padding(4, 0, 4, 0);
            L_Version.Name = "L_Version";
            L_Version.RightToLeft = RightToLeft.No;
            L_Version.Size = new Size(111, 33);
            L_Version.TabIndex = 18;
            L_Version.Text = "v1.0.3";
            L_Version.TextAlign = ContentAlignment.TopRight;
            // 
            // BTN_Refresh
            // 
            BTN_Refresh.Location = new Point(652, 362);
            BTN_Refresh.Margin = new Padding(4, 3, 4, 3);
            BTN_Refresh.Name = "BTN_Refresh";
            BTN_Refresh.Size = new Size(238, 43);
            BTN_Refresh.TabIndex = 22;
            BTN_Refresh.Text = "Refresh";
            BTN_Refresh.UseVisualStyleBackColor = true;
            BTN_Refresh.Click += BTN_Refresh_Click;
            // 
            // L_Status
            // 
            L_Status.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_Status.AutoSize = true;
            L_Status.ForeColor = Color.Lime;
            L_Status.Location = new Point(12, 351);
            L_Status.Margin = new Padding(4, 0, 4, 0);
            L_Status.Name = "L_Status";
            L_Status.RightToLeft = RightToLeft.No;
            L_Status.Size = new Size(79, 33);
            L_Status.TabIndex = 20;
            L_Status.Text = "Idle";
            L_Status.TextAlign = ContentAlignment.TopRight;
            // 
            // AutoUpdater
            // 
            AutoUpdater.Tick += AutoUpdater_Tick;
            // 
            // ChkBox_AutoRefresh
            // 
            ChkBox_AutoRefresh.AutoSize = true;
            ChkBox_AutoRefresh.Location = new Point(420, 368);
            ChkBox_AutoRefresh.Margin = new Padding(4, 3, 4, 3);
            ChkBox_AutoRefresh.Name = "ChkBox_AutoRefresh";
            ChkBox_AutoRefresh.Size = new Size(226, 37);
            ChkBox_AutoRefresh.TabIndex = 21;
            ChkBox_AutoRefresh.Text = "Auto-refresh";
            ChkBox_AutoRefresh.UseVisualStyleBackColor = true;
            ChkBox_AutoRefresh.CheckedChanged += ChkBox_AutoRefresh_CheckedChanged;
            // 
            // Link_About
            // 
            Link_About.AutoSize = true;
            Link_About.Location = new Point(137, 389);
            Link_About.Margin = new Padding(4, 0, 4, 0);
            Link_About.Name = "Link_About";
            Link_About.Size = new Size(271, 33);
            Link_About.TabIndex = 19;
            Link_About.TabStop = true;
            Link_About.Text = "About tracker...";
            Link_About.LinkClicked += Link_About_LinkClicked;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(16F, 33F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(902, 417);
            Controls.Add(Link_About);
            Controls.Add(ChkBox_AutoRefresh);
            Controls.Add(L_Status);
            Controls.Add(BTN_Refresh);
            Controls.Add(L_Version);
            Controls.Add(L_VotesPercent);
            Controls.Add(L_VotesSummit);
            Controls.Add(L_VotesVoted);
            Controls.Add(L_VotesSentSubtext);
            Controls.Add(VotesProgress);
            Controls.Add(L_Brawler5);
            Controls.Add(L_Brawler4);
            Controls.Add(L_Brawler3);
            Controls.Add(L_Brawler2);
            Controls.Add(L_Brawler1);
            Controls.Add(Pic_Brawler5);
            Controls.Add(Pic_Brawler4);
            Controls.Add(Pic_Brawler3);
            Controls.Add(Pic_Brawler2);
            Controls.Add(Pic_Brawler1);
            Controls.Add(L_TimeLeftContext);
            Controls.Add(L_TimeLeft);
            Controls.Add(L_EventName);
            Font = new Font("Determination Mono Web", 24F, FontStyle.Regular, GraphicsUnit.Point, 163);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Brawl Stars Community Tracker";
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler2).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler3).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler4).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pic_Brawler5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_EventName;
        private Label L_TimeLeft;
        private System.Windows.Forms.Timer EventTimeLeftUpdater;
        private Label L_TimeLeftContext;
        private PictureBox Pic_Brawler1;
        private PictureBox Pic_Brawler2;
        private PictureBox Pic_Brawler3;
        private PictureBox Pic_Brawler4;
        private PictureBox Pic_Brawler5;
        private Label L_Brawler1;
        private Label L_Brawler2;
        private Label L_Brawler3;
        private Label L_Brawler4;
        private Label L_Brawler5;
        private ProgressBar VotesProgress;
        private Label L_VotesSentSubtext;
        private Label L_VotesVoted;
        private Label L_VotesSummit;
        private Label L_VotesPercent;
        private Label L_Version;
        private Button BTN_Refresh;
        private Label L_Status;
        private System.Windows.Forms.Timer AutoUpdater;
        private CheckBox ChkBox_AutoRefresh;
        private LinkLabel Link_About;
    }
}