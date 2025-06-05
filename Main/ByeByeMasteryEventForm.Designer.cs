namespace Main
{
    partial class ByeByeMasteryEventForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ByeByeMasteryEventForm));
            L_Title = new Label();
            L_Version = new Label();
            ProgBar_TimeLeft = new ProgressBar();
            L_TimeLeft = new Label();
            L_SplitContainer = new Label();
            Picbox_MasteryPoint = new PictureBox();
            L_PointsCount = new Label();
            Progbar_ToNextGoal = new ProgressBar();
            L_StartCount = new Label();
            L_EndCount = new Label();
            Timer_TimeLeftCountdown = new System.Windows.Forms.Timer(components);
            Rdi_PrefNone = new RadioButton();
            Rdi_PrefShortened = new RadioButton();
            Rdi_PrefShortenedMore = new RadioButton();
            L_TimeLeftLabel = new Label();
            L_OrPercentage = new Label();
            L_PercentageToNextMilestone = new Label();
            BTN_Refresh = new Button();
            Chk_AutoRefresh = new CheckBox();
            Timer_Refresh = new System.Windows.Forms.Timer(components);
            L_LastUpdated = new Label();
            ((System.ComponentModel.ISupportInitialize)Picbox_MasteryPoint).BeginInit();
            SuspendLayout();
            // 
            // L_Title
            // 
            L_Title.AutoSize = true;
            L_Title.Font = new Font("Lilita One", 32F);
            L_Title.Location = new Point(12, 9);
            L_Title.Name = "L_Title";
            L_Title.Size = new Size(542, 49);
            L_Title.TabIndex = 0;
            L_Title.Text = "Community Event May 2025";
            // 
            // L_Version
            // 
            L_Version.Location = new Point(490, 58);
            L_Version.Name = "L_Version";
            L_Version.Size = new Size(53, 19);
            L_Version.TabIndex = 1;
            L_Version.Text = "v1.0.6";
            L_Version.TextAlign = ContentAlignment.TopRight;
            L_Version.Click += L_Version_Click;
            L_Version.MouseEnter += L_Version_MouseEnter;
            L_Version.MouseLeave += L_Version_MouseLeave;
            // 
            // ProgBar_TimeLeft
            // 
            ProgBar_TimeLeft.Location = new Point(560, 54);
            ProgBar_TimeLeft.Maximum = 1900800;
            ProgBar_TimeLeft.Name = "ProgBar_TimeLeft";
            ProgBar_TimeLeft.Size = new Size(276, 23);
            ProgBar_TimeLeft.Step = 1;
            ProgBar_TimeLeft.TabIndex = 2;
            // 
            // L_TimeLeft
            // 
            L_TimeLeft.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_TimeLeft.Font = new Font("Lilita One", 20F);
            L_TimeLeft.Location = new Point(620, 12);
            L_TimeLeft.Name = "L_TimeLeft";
            L_TimeLeft.Size = new Size(224, 42);
            L_TimeLeft.TabIndex = 4;
            L_TimeLeft.Text = "00:00:00:00";
            L_TimeLeft.TextAlign = ContentAlignment.MiddleRight;
            // 
            // L_SplitContainer
            // 
            L_SplitContainer.AutoSize = true;
            L_SplitContainer.Location = new Point(-59, 70);
            L_SplitContainer.Name = "L_SplitContainer";
            L_SplitContainer.Size = new Size(1269, 19);
            L_SplitContainer.TabIndex = 5;
            L_SplitContainer.Text = "______________________________________________________________________________________________________________________________";
            // 
            // Picbox_MasteryPoint
            // 
            Picbox_MasteryPoint.BackgroundImageLayout = ImageLayout.Zoom;
            Picbox_MasteryPoint.Image = Properties.Resources.Img_MasteryPoint;
            Picbox_MasteryPoint.Location = new Point(772, 101);
            Picbox_MasteryPoint.Name = "Picbox_MasteryPoint";
            Picbox_MasteryPoint.Size = new Size(64, 76);
            Picbox_MasteryPoint.SizeMode = PictureBoxSizeMode.StretchImage;
            Picbox_MasteryPoint.TabIndex = 6;
            Picbox_MasteryPoint.TabStop = false;
            // 
            // L_PointsCount
            // 
            L_PointsCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_PointsCount.Font = new Font("Lilita One", 48F);
            L_PointsCount.Location = new Point(0, 101);
            L_PointsCount.Name = "L_PointsCount";
            L_PointsCount.Size = new Size(780, 76);
            L_PointsCount.TabIndex = 7;
            L_PointsCount.Text = "0";
            L_PointsCount.TextAlign = ContentAlignment.MiddleRight;
            L_PointsCount.Click += L_PointsCount_Click;
            // 
            // Progbar_ToNextGoal
            // 
            Progbar_ToNextGoal.Location = new Point(12, 203);
            Progbar_ToNextGoal.Maximum = 1000000000;
            Progbar_ToNextGoal.Name = "Progbar_ToNextGoal";
            Progbar_ToNextGoal.Size = new Size(824, 35);
            Progbar_ToNextGoal.Step = 1;
            Progbar_ToNextGoal.TabIndex = 8;
            // 
            // L_StartCount
            // 
            L_StartCount.Location = new Point(12, 241);
            L_StartCount.Name = "L_StartCount";
            L_StartCount.Size = new Size(360, 26);
            L_StartCount.TabIndex = 9;
            L_StartCount.Text = "0";
            // 
            // L_EndCount
            // 
            L_EndCount.Location = new Point(476, 241);
            L_EndCount.Name = "L_EndCount";
            L_EndCount.Size = new Size(360, 26);
            L_EndCount.TabIndex = 10;
            L_EndCount.Text = "0";
            L_EndCount.TextAlign = ContentAlignment.TopRight;
            // 
            // Timer_TimeLeftCountdown
            // 
            Timer_TimeLeftCountdown.Enabled = true;
            Timer_TimeLeftCountdown.Tick += Timer_TimeLeftCountdown_Tick;
            // 
            // Rdi_PrefNone
            // 
            Rdi_PrefNone.AutoSize = true;
            Rdi_PrefNone.Checked = true;
            Rdi_PrefNone.Location = new Point(558, 320);
            Rdi_PrefNone.Name = "Rdi_PrefNone";
            Rdi_PrefNone.Size = new Size(62, 23);
            Rdi_PrefNone.TabIndex = 103;
            Rdi_PrefNone.TabStop = true;
            Rdi_PrefNone.Text = "None";
            Rdi_PrefNone.UseVisualStyleBackColor = true;
            Rdi_PrefNone.CheckedChanged += Rdi_PrefNone_CheckedChanged;
            // 
            // Rdi_PrefShortened
            // 
            Rdi_PrefShortened.AutoSize = true;
            Rdi_PrefShortened.Location = new Point(626, 320);
            Rdi_PrefShortened.Name = "Rdi_PrefShortened";
            Rdi_PrefShortened.Size = new Size(97, 23);
            Rdi_PrefShortened.TabIndex = 104;
            Rdi_PrefShortened.Text = "Shortened";
            Rdi_PrefShortened.UseVisualStyleBackColor = true;
            Rdi_PrefShortened.CheckedChanged += Rdi_PrefShortened_CheckedChanged;
            // 
            // Rdi_PrefShortenedMore
            // 
            Rdi_PrefShortenedMore.AutoSize = true;
            Rdi_PrefShortenedMore.Location = new Point(729, 320);
            Rdi_PrefShortenedMore.Name = "Rdi_PrefShortenedMore";
            Rdi_PrefShortenedMore.Size = new Size(107, 23);
            Rdi_PrefShortenedMore.TabIndex = 105;
            Rdi_PrefShortenedMore.Text = "Shortened+";
            Rdi_PrefShortenedMore.UseVisualStyleBackColor = true;
            Rdi_PrefShortenedMore.CheckedChanged += Rdi_PrefShortenedMore_CheckedChanged;
            // 
            // L_TimeLeftLabel
            // 
            L_TimeLeftLabel.AutoSize = true;
            L_TimeLeftLabel.Location = new Point(560, 28);
            L_TimeLeftLabel.Name = "L_TimeLeftLabel";
            L_TimeLeftLabel.Size = new Size(73, 19);
            L_TimeLeftLabel.TabIndex = 3;
            L_TimeLeftLabel.Text = "Time left:";
            // 
            // L_OrPercentage
            // 
            L_OrPercentage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_OrPercentage.Location = new Point(499, 180);
            L_OrPercentage.Name = "L_OrPercentage";
            L_OrPercentage.Size = new Size(337, 27);
            L_OrPercentage.TabIndex = 14;
            L_OrPercentage.Text = "Loading...";
            L_OrPercentage.TextAlign = ContentAlignment.TopRight;
            // 
            // L_PercentageToNextMilestone
            // 
            L_PercentageToNextMilestone.Location = new Point(378, 241);
            L_PercentageToNextMilestone.Name = "L_PercentageToNextMilestone";
            L_PercentageToNextMilestone.Size = new Size(92, 26);
            L_PercentageToNextMilestone.TabIndex = 15;
            L_PercentageToNextMilestone.Text = "0%";
            L_PercentageToNextMilestone.TextAlign = ContentAlignment.TopCenter;
            // 
            // BTN_Refresh
            // 
            BTN_Refresh.Location = new Point(12, 314);
            BTN_Refresh.Name = "BTN_Refresh";
            BTN_Refresh.Size = new Size(75, 29);
            BTN_Refresh.TabIndex = 101;
            BTN_Refresh.Text = "Refresh";
            BTN_Refresh.UseVisualStyleBackColor = true;
            BTN_Refresh.Click += BTN_Refresh_Click;
            // 
            // Chk_AutoRefresh
            // 
            Chk_AutoRefresh.AutoSize = true;
            Chk_AutoRefresh.Location = new Point(93, 321);
            Chk_AutoRefresh.Name = "Chk_AutoRefresh";
            Chk_AutoRefresh.Size = new Size(118, 23);
            Chk_AutoRefresh.TabIndex = 102;
            Chk_AutoRefresh.Text = "Auto-refresh";
            Chk_AutoRefresh.UseVisualStyleBackColor = true;
            Chk_AutoRefresh.CheckedChanged += Chk_AutoRefresh_CheckedChanged;
            // 
            // Timer_Refresh
            // 
            Timer_Refresh.Interval = 1000;
            Timer_Refresh.Tick += Timer_Refresh_Tick;
            // 
            // L_LastUpdated
            // 
            L_LastUpdated.AutoSize = true;
            L_LastUpdated.Location = new Point(12, 180);
            L_LastUpdated.Name = "L_LastUpdated";
            L_LastUpdated.Size = new Size(132, 19);
            L_LastUpdated.TabIndex = 16;
            L_LastUpdated.Text = "Last updated: IDK";
            // 
            // ByeByeMasteryEventForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(848, 355);
            Controls.Add(L_TimeLeftLabel);
            Controls.Add(L_LastUpdated);
            Controls.Add(Chk_AutoRefresh);
            Controls.Add(BTN_Refresh);
            Controls.Add(L_PercentageToNextMilestone);
            Controls.Add(Rdi_PrefShortenedMore);
            Controls.Add(Rdi_PrefShortened);
            Controls.Add(Rdi_PrefNone);
            Controls.Add(L_EndCount);
            Controls.Add(L_StartCount);
            Controls.Add(Progbar_ToNextGoal);
            Controls.Add(Picbox_MasteryPoint);
            Controls.Add(L_PointsCount);
            Controls.Add(ProgBar_TimeLeft);
            Controls.Add(L_TimeLeft);
            Controls.Add(L_Version);
            Controls.Add(L_Title);
            Controls.Add(L_SplitContainer);
            Controls.Add(L_OrPercentage);
            Font = new Font("Lilita One", 12F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "ByeByeMasteryEventForm";
            Text = "RIP Masteries";
            Load += ByeByeMasteryEventForm_Load;
            ((System.ComponentModel.ISupportInitialize)Picbox_MasteryPoint).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_Title;
        private Label L_Version;
        private ProgressBar ProgBar_TimeLeft;
        private Label L_TimeLeft;
        private Label L_SplitContainer;
        private PictureBox Picbox_MasteryPoint;
        private Label L_PointsCount;
        private ProgressBar Progbar_ToNextGoal;
        private Label L_StartCount;
        private Label L_EndCount;
        private System.Windows.Forms.Timer Timer_TimeLeftCountdown;
        private RadioButton Rdi_PrefNone;
        private RadioButton Rdi_PrefShortened;
        private RadioButton Rdi_PrefShortenedMore;
        private Label L_TimeLeftLabel;
        private Label L_OrPercentage;
        private Label L_PercentageToNextMilestone;
        private Button BTN_Refresh;
        private CheckBox Chk_AutoRefresh;
        private System.Windows.Forms.Timer Timer_Refresh;
        private Label L_LastUpdated;
    }
}