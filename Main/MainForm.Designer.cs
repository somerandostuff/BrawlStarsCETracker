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
            L_EventTitle = new Label();
            L_TimeLeft = new Label();
            L_TimeLeftLabel = new Label();
            ProgBar_TimeLeft = new ProgressBar();
            Timer_TimeLeft = new System.Windows.Forms.Timer(components);
            L_KillCountTitle = new Label();
            L_KillCount = new Label();
            L_KillCountTitle2 = new Label();
            ProgBar_KillCountTo10B = new ProgressBar();
            L_MilestoneTag0 = new Label();
            L_MilestoneTag1 = new Label();
            L_MilestoneTag2 = new Label();
            L_MilestoneTag3 = new Label();
            L_MilestoneTag4 = new Label();
            L_MilestoneTag5 = new Label();
            L_MilestoneTag6 = new Label();
            L_MilestoneTag7 = new Label();
            L_MilestoneTag8 = new Label();
            L_MilestoneTag9 = new Label();
            L_MilestoneTag10 = new Label();
            L_MilestoneTag10C = new Label();
            L_MilestoneTag9C = new Label();
            L_MilestoneTag8C = new Label();
            L_MilestoneTag7C = new Label();
            L_MilestoneTag6C = new Label();
            L_MilestoneTag5C = new Label();
            L_MilestoneTag4C = new Label();
            L_MilestoneTag3C = new Label();
            L_MilestoneTag2C = new Label();
            L_MilestoneTag1C = new Label();
            L_MilestoneTag0C = new Label();
            L_MilestoneTag10Bil = new Label();
            L_MilestoneTag9Bil = new Label();
            L_MilestoneTag8Bil = new Label();
            L_MilestoneTag7Bil = new Label();
            L_MilestoneTag6Bil = new Label();
            L_MilestoneTag5Bil = new Label();
            L_MilestoneTag4Bil = new Label();
            L_MilestoneTag3Bil = new Label();
            L_MilestoneTag2Bil = new Label();
            L_MilestoneTag1Bil = new Label();
            L_BonusStarrs = new Label();
            BTN_Refresh = new Button();
            Chk_AutoRefresh = new CheckBox();
            BTN_FunButton = new Button();
            L_KillsAdded = new Label();
            Timer_AutoRefresh = new System.Windows.Forms.Timer(components);
            L_Version = new Label();
            LinkL_About = new LinkLabel();
            L_Status = new Label();
            SuspendLayout();
            // 
            // L_EventTitle
            // 
            L_EventTitle.AutoSize = true;
            L_EventTitle.Location = new Point(12, 9);
            L_EventTitle.Margin = new Padding(6, 0, 6, 0);
            L_EventTitle.Name = "L_EventTitle";
            L_EventTitle.Size = new Size(375, 37);
            L_EventTitle.TabIndex = 0;
            L_EventTitle.Text = "Community Event April 23";
            // 
            // L_TimeLeft
            // 
            L_TimeLeft.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_TimeLeft.Location = new Point(486, 9);
            L_TimeLeft.Name = "L_TimeLeft";
            L_TimeLeft.Size = new Size(186, 37);
            L_TimeLeft.TabIndex = 1;
            L_TimeLeft.Text = "0:00:00:00";
            L_TimeLeft.TextAlign = ContentAlignment.TopRight;
            // 
            // L_TimeLeftLabel
            // 
            L_TimeLeftLabel.AutoSize = true;
            L_TimeLeftLabel.Font = new Font("Lilita One", 12F);
            L_TimeLeftLabel.Location = new Point(411, 19);
            L_TimeLeftLabel.Name = "L_TimeLeftLabel";
            L_TimeLeftLabel.Size = new Size(73, 19);
            L_TimeLeftLabel.TabIndex = 2;
            L_TimeLeftLabel.Text = "Time left:";
            // 
            // ProgBar_TimeLeft
            // 
            ProgBar_TimeLeft.Location = new Point(411, 49);
            ProgBar_TimeLeft.MarqueeAnimationSpeed = 50;
            ProgBar_TimeLeft.Maximum = 864000;
            ProgBar_TimeLeft.Name = "ProgBar_TimeLeft";
            ProgBar_TimeLeft.Size = new Size(262, 17);
            ProgBar_TimeLeft.Step = 1;
            ProgBar_TimeLeft.TabIndex = 3;
            ProgBar_TimeLeft.Value = 686868;
            // 
            // Timer_TimeLeft
            // 
            Timer_TimeLeft.Enabled = true;
            Timer_TimeLeft.Interval = 1;
            Timer_TimeLeft.Tick += Timer_TimeLeft_Tick;
            // 
            // L_KillCountTitle
            // 
            L_KillCountTitle.AutoSize = true;
            L_KillCountTitle.Font = new Font("Lilita One", 12F);
            L_KillCountTitle.Location = new Point(12, 69);
            L_KillCountTitle.Name = "L_KillCountTitle";
            L_KillCountTitle.Size = new Size(150, 19);
            L_KillCountTitle.TabIndex = 4;
            L_KillCountTitle.Text = "Approximated count";
            // 
            // L_KillCount
            // 
            L_KillCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_KillCount.Font = new Font("Lilita One", 36F);
            L_KillCount.ForeColor = Color.Lime;
            L_KillCount.Location = new Point(12, 88);
            L_KillCount.Name = "L_KillCount";
            L_KillCount.Size = new Size(657, 58);
            L_KillCount.TabIndex = 5;
            L_KillCount.Text = "0";
            L_KillCount.TextAlign = ContentAlignment.TopCenter;
            L_KillCount.Click += L_KillCount_Click;
            // 
            // L_KillCountTitle2
            // 
            L_KillCountTitle2.AutoSize = true;
            L_KillCountTitle2.Font = new Font("Lilita One", 12F);
            L_KillCountTitle2.Location = new Point(296, 140);
            L_KillCountTitle2.Name = "L_KillCountTitle2";
            L_KillCountTitle2.Size = new Size(84, 19);
            L_KillCountTitle2.TabIndex = 6;
            L_KillCountTitle2.Text = "takedowns";
            // 
            // ProgBar_KillCountTo10B
            // 
            ProgBar_KillCountTo10B.Location = new Point(12, 162);
            ProgBar_KillCountTo10B.MarqueeAnimationSpeed = 50;
            ProgBar_KillCountTo10B.Maximum = 10000000;
            ProgBar_KillCountTo10B.Name = "ProgBar_KillCountTo10B";
            ProgBar_KillCountTo10B.Size = new Size(660, 31);
            ProgBar_KillCountTo10B.Step = 1;
            ProgBar_KillCountTo10B.TabIndex = 7;
            // 
            // L_MilestoneTag0
            // 
            L_MilestoneTag0.AutoSize = true;
            L_MilestoneTag0.Font = new Font("Lilita One", 12F);
            L_MilestoneTag0.Location = new Point(4, 196);
            L_MilestoneTag0.Name = "L_MilestoneTag0";
            L_MilestoneTag0.Size = new Size(19, 19);
            L_MilestoneTag0.TabIndex = 8;
            L_MilestoneTag0.Text = "|";
            // 
            // L_MilestoneTag1
            // 
            L_MilestoneTag1.AutoSize = true;
            L_MilestoneTag1.Font = new Font("Lilita One", 12F);
            L_MilestoneTag1.ForeColor = Color.Yellow;
            L_MilestoneTag1.Location = new Point(68, 196);
            L_MilestoneTag1.Name = "L_MilestoneTag1";
            L_MilestoneTag1.Size = new Size(19, 19);
            L_MilestoneTag1.TabIndex = 9;
            L_MilestoneTag1.Text = "|";
            // 
            // L_MilestoneTag2
            // 
            L_MilestoneTag2.AutoSize = true;
            L_MilestoneTag2.Font = new Font("Lilita One", 12F);
            L_MilestoneTag2.ForeColor = Color.Goldenrod;
            L_MilestoneTag2.Location = new Point(134, 196);
            L_MilestoneTag2.Name = "L_MilestoneTag2";
            L_MilestoneTag2.Size = new Size(19, 19);
            L_MilestoneTag2.TabIndex = 10;
            L_MilestoneTag2.Text = "|";
            // 
            // L_MilestoneTag3
            // 
            L_MilestoneTag3.AutoSize = true;
            L_MilestoneTag3.Font = new Font("Lilita One", 12F);
            L_MilestoneTag3.ForeColor = Color.SpringGreen;
            L_MilestoneTag3.Location = new Point(200, 196);
            L_MilestoneTag3.Name = "L_MilestoneTag3";
            L_MilestoneTag3.Size = new Size(19, 19);
            L_MilestoneTag3.TabIndex = 11;
            L_MilestoneTag3.Text = "|";
            // 
            // L_MilestoneTag4
            // 
            L_MilestoneTag4.AutoSize = true;
            L_MilestoneTag4.Font = new Font("Lilita One", 12F);
            L_MilestoneTag4.ForeColor = Color.Magenta;
            L_MilestoneTag4.Location = new Point(266, 196);
            L_MilestoneTag4.Name = "L_MilestoneTag4";
            L_MilestoneTag4.Size = new Size(19, 19);
            L_MilestoneTag4.TabIndex = 12;
            L_MilestoneTag4.Text = "|";
            // 
            // L_MilestoneTag5
            // 
            L_MilestoneTag5.AutoSize = true;
            L_MilestoneTag5.Font = new Font("Lilita One", 12F);
            L_MilestoneTag5.ForeColor = Color.Yellow;
            L_MilestoneTag5.Location = new Point(332, 196);
            L_MilestoneTag5.Name = "L_MilestoneTag5";
            L_MilestoneTag5.Size = new Size(19, 19);
            L_MilestoneTag5.TabIndex = 13;
            L_MilestoneTag5.Text = "|";
            // 
            // L_MilestoneTag6
            // 
            L_MilestoneTag6.AutoSize = true;
            L_MilestoneTag6.Font = new Font("Lilita One", 12F);
            L_MilestoneTag6.ForeColor = Color.Cyan;
            L_MilestoneTag6.Location = new Point(398, 196);
            L_MilestoneTag6.Name = "L_MilestoneTag6";
            L_MilestoneTag6.Size = new Size(19, 19);
            L_MilestoneTag6.TabIndex = 14;
            L_MilestoneTag6.Text = "|";
            // 
            // L_MilestoneTag7
            // 
            L_MilestoneTag7.AutoSize = true;
            L_MilestoneTag7.Font = new Font("Lilita One", 12F);
            L_MilestoneTag7.ForeColor = Color.Yellow;
            L_MilestoneTag7.Location = new Point(464, 196);
            L_MilestoneTag7.Name = "L_MilestoneTag7";
            L_MilestoneTag7.Size = new Size(19, 19);
            L_MilestoneTag7.TabIndex = 15;
            L_MilestoneTag7.Text = "|";
            // 
            // L_MilestoneTag8
            // 
            L_MilestoneTag8.AutoSize = true;
            L_MilestoneTag8.Font = new Font("Lilita One", 12F);
            L_MilestoneTag8.ForeColor = Color.Turquoise;
            L_MilestoneTag8.Location = new Point(530, 196);
            L_MilestoneTag8.Name = "L_MilestoneTag8";
            L_MilestoneTag8.Size = new Size(19, 19);
            L_MilestoneTag8.TabIndex = 16;
            L_MilestoneTag8.Text = "|";
            // 
            // L_MilestoneTag9
            // 
            L_MilestoneTag9.AutoSize = true;
            L_MilestoneTag9.Font = new Font("Lilita One", 12F);
            L_MilestoneTag9.ForeColor = Color.Yellow;
            L_MilestoneTag9.Location = new Point(596, 196);
            L_MilestoneTag9.Name = "L_MilestoneTag9";
            L_MilestoneTag9.Size = new Size(19, 19);
            L_MilestoneTag9.TabIndex = 17;
            L_MilestoneTag9.Text = "|";
            // 
            // L_MilestoneTag10
            // 
            L_MilestoneTag10.AutoSize = true;
            L_MilestoneTag10.Font = new Font("Lilita One", 12F);
            L_MilestoneTag10.ForeColor = Color.FromArgb(128, 255, 255);
            L_MilestoneTag10.Location = new Point(661, 196);
            L_MilestoneTag10.Name = "L_MilestoneTag10";
            L_MilestoneTag10.Size = new Size(19, 19);
            L_MilestoneTag10.TabIndex = 18;
            L_MilestoneTag10.Text = "|";
            // 
            // L_MilestoneTag10C
            // 
            L_MilestoneTag10C.AutoSize = true;
            L_MilestoneTag10C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag10C.ForeColor = Color.FromArgb(128, 255, 255);
            L_MilestoneTag10C.Location = new Point(658, 202);
            L_MilestoneTag10C.Name = "L_MilestoneTag10C";
            L_MilestoneTag10C.Size = new Size(25, 19);
            L_MilestoneTag10C.TabIndex = 29;
            L_MilestoneTag10C.Text = "10";
            // 
            // L_MilestoneTag9C
            // 
            L_MilestoneTag9C.AutoSize = true;
            L_MilestoneTag9C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag9C.ForeColor = Color.Yellow;
            L_MilestoneTag9C.Location = new Point(596, 202);
            L_MilestoneTag9C.Name = "L_MilestoneTag9C";
            L_MilestoneTag9C.Size = new Size(18, 19);
            L_MilestoneTag9C.TabIndex = 28;
            L_MilestoneTag9C.Text = "9";
            // 
            // L_MilestoneTag8C
            // 
            L_MilestoneTag8C.AutoSize = true;
            L_MilestoneTag8C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag8C.ForeColor = Color.Turquoise;
            L_MilestoneTag8C.Location = new Point(530, 202);
            L_MilestoneTag8C.Name = "L_MilestoneTag8C";
            L_MilestoneTag8C.Size = new Size(18, 19);
            L_MilestoneTag8C.TabIndex = 27;
            L_MilestoneTag8C.Text = "8";
            // 
            // L_MilestoneTag7C
            // 
            L_MilestoneTag7C.AutoSize = true;
            L_MilestoneTag7C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag7C.ForeColor = Color.Yellow;
            L_MilestoneTag7C.Location = new Point(465, 202);
            L_MilestoneTag7C.Name = "L_MilestoneTag7C";
            L_MilestoneTag7C.Size = new Size(17, 19);
            L_MilestoneTag7C.TabIndex = 26;
            L_MilestoneTag7C.Text = "7";
            // 
            // L_MilestoneTag6C
            // 
            L_MilestoneTag6C.AutoSize = true;
            L_MilestoneTag6C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag6C.ForeColor = Color.FromArgb(255, 192, 255);
            L_MilestoneTag6C.Location = new Point(398, 202);
            L_MilestoneTag6C.Name = "L_MilestoneTag6C";
            L_MilestoneTag6C.Size = new Size(18, 19);
            L_MilestoneTag6C.TabIndex = 25;
            L_MilestoneTag6C.Text = "6";
            // 
            // L_MilestoneTag5C
            // 
            L_MilestoneTag5C.AutoSize = true;
            L_MilestoneTag5C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag5C.ForeColor = Color.Yellow;
            L_MilestoneTag5C.Location = new Point(333, 202);
            L_MilestoneTag5C.Name = "L_MilestoneTag5C";
            L_MilestoneTag5C.Size = new Size(17, 19);
            L_MilestoneTag5C.TabIndex = 24;
            L_MilestoneTag5C.Text = "5";
            // 
            // L_MilestoneTag4C
            // 
            L_MilestoneTag4C.AutoSize = true;
            L_MilestoneTag4C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag4C.ForeColor = Color.Magenta;
            L_MilestoneTag4C.Location = new Point(266, 202);
            L_MilestoneTag4C.Name = "L_MilestoneTag4C";
            L_MilestoneTag4C.Size = new Size(19, 19);
            L_MilestoneTag4C.TabIndex = 23;
            L_MilestoneTag4C.Text = "4";
            // 
            // L_MilestoneTag3C
            // 
            L_MilestoneTag3C.AutoSize = true;
            L_MilestoneTag3C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag3C.ForeColor = Color.SpringGreen;
            L_MilestoneTag3C.Location = new Point(201, 202);
            L_MilestoneTag3C.Name = "L_MilestoneTag3C";
            L_MilestoneTag3C.Size = new Size(17, 19);
            L_MilestoneTag3C.TabIndex = 22;
            L_MilestoneTag3C.Text = "3";
            // 
            // L_MilestoneTag2C
            // 
            L_MilestoneTag2C.AutoSize = true;
            L_MilestoneTag2C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag2C.ForeColor = Color.Goldenrod;
            L_MilestoneTag2C.Location = new Point(134, 202);
            L_MilestoneTag2C.Name = "L_MilestoneTag2C";
            L_MilestoneTag2C.Size = new Size(17, 19);
            L_MilestoneTag2C.TabIndex = 21;
            L_MilestoneTag2C.Text = "2";
            // 
            // L_MilestoneTag1C
            // 
            L_MilestoneTag1C.AutoSize = true;
            L_MilestoneTag1C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag1C.ForeColor = Color.Yellow;
            L_MilestoneTag1C.Location = new Point(69, 202);
            L_MilestoneTag1C.Name = "L_MilestoneTag1C";
            L_MilestoneTag1C.Size = new Size(15, 19);
            L_MilestoneTag1C.TabIndex = 20;
            L_MilestoneTag1C.Text = "1";
            // 
            // L_MilestoneTag0C
            // 
            L_MilestoneTag0C.AutoSize = true;
            L_MilestoneTag0C.Font = new Font("Lilita One", 12F);
            L_MilestoneTag0C.Location = new Point(4, 202);
            L_MilestoneTag0C.Name = "L_MilestoneTag0C";
            L_MilestoneTag0C.Size = new Size(19, 19);
            L_MilestoneTag0C.TabIndex = 19;
            L_MilestoneTag0C.Text = "0";
            // 
            // L_MilestoneTag10Bil
            // 
            L_MilestoneTag10Bil.AutoSize = true;
            L_MilestoneTag10Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag10Bil.ForeColor = Color.FromArgb(255, 192, 255);
            L_MilestoneTag10Bil.Location = new Point(661, 221);
            L_MilestoneTag10Bil.Name = "L_MilestoneTag10Bil";
            L_MilestoneTag10Bil.Size = new Size(20, 12);
            L_MilestoneTag10Bil.TabIndex = 39;
            L_MilestoneTag10Bil.Text = "bil.";
            // 
            // L_MilestoneTag9Bil
            // 
            L_MilestoneTag9Bil.AutoSize = true;
            L_MilestoneTag9Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag9Bil.ForeColor = Color.MediumOrchid;
            L_MilestoneTag9Bil.Location = new Point(596, 221);
            L_MilestoneTag9Bil.Name = "L_MilestoneTag9Bil";
            L_MilestoneTag9Bil.Size = new Size(20, 12);
            L_MilestoneTag9Bil.TabIndex = 38;
            L_MilestoneTag9Bil.Text = "bil.";
            // 
            // L_MilestoneTag8Bil
            // 
            L_MilestoneTag8Bil.AutoSize = true;
            L_MilestoneTag8Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag8Bil.ForeColor = Color.Turquoise;
            L_MilestoneTag8Bil.Location = new Point(530, 221);
            L_MilestoneTag8Bil.Name = "L_MilestoneTag8Bil";
            L_MilestoneTag8Bil.Size = new Size(20, 12);
            L_MilestoneTag8Bil.TabIndex = 37;
            L_MilestoneTag8Bil.Text = "bil.";
            // 
            // L_MilestoneTag7Bil
            // 
            L_MilestoneTag7Bil.AutoSize = true;
            L_MilestoneTag7Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag7Bil.ForeColor = Color.Gold;
            L_MilestoneTag7Bil.Location = new Point(465, 221);
            L_MilestoneTag7Bil.Name = "L_MilestoneTag7Bil";
            L_MilestoneTag7Bil.Size = new Size(20, 12);
            L_MilestoneTag7Bil.TabIndex = 36;
            L_MilestoneTag7Bil.Text = "bil.";
            // 
            // L_MilestoneTag6Bil
            // 
            L_MilestoneTag6Bil.AutoSize = true;
            L_MilestoneTag6Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag6Bil.ForeColor = Color.FromArgb(128, 255, 255);
            L_MilestoneTag6Bil.Location = new Point(398, 221);
            L_MilestoneTag6Bil.Name = "L_MilestoneTag6Bil";
            L_MilestoneTag6Bil.Size = new Size(20, 12);
            L_MilestoneTag6Bil.TabIndex = 35;
            L_MilestoneTag6Bil.Text = "bil.";
            // 
            // L_MilestoneTag5Bil
            // 
            L_MilestoneTag5Bil.AutoSize = true;
            L_MilestoneTag5Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag5Bil.ForeColor = Color.Red;
            L_MilestoneTag5Bil.Location = new Point(333, 221);
            L_MilestoneTag5Bil.Name = "L_MilestoneTag5Bil";
            L_MilestoneTag5Bil.Size = new Size(20, 12);
            L_MilestoneTag5Bil.TabIndex = 34;
            L_MilestoneTag5Bil.Text = "bil.";
            // 
            // L_MilestoneTag4Bil
            // 
            L_MilestoneTag4Bil.AutoSize = true;
            L_MilestoneTag4Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag4Bil.ForeColor = Color.Magenta;
            L_MilestoneTag4Bil.Location = new Point(266, 221);
            L_MilestoneTag4Bil.Name = "L_MilestoneTag4Bil";
            L_MilestoneTag4Bil.Size = new Size(20, 12);
            L_MilestoneTag4Bil.TabIndex = 33;
            L_MilestoneTag4Bil.Text = "bil.";
            // 
            // L_MilestoneTag3Bil
            // 
            L_MilestoneTag3Bil.AutoSize = true;
            L_MilestoneTag3Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag3Bil.ForeColor = Color.SpringGreen;
            L_MilestoneTag3Bil.Location = new Point(201, 221);
            L_MilestoneTag3Bil.Name = "L_MilestoneTag3Bil";
            L_MilestoneTag3Bil.Size = new Size(20, 12);
            L_MilestoneTag3Bil.TabIndex = 32;
            L_MilestoneTag3Bil.Text = "bil.";
            // 
            // L_MilestoneTag2Bil
            // 
            L_MilestoneTag2Bil.AutoSize = true;
            L_MilestoneTag2Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag2Bil.ForeColor = Color.Goldenrod;
            L_MilestoneTag2Bil.Location = new Point(134, 221);
            L_MilestoneTag2Bil.Name = "L_MilestoneTag2Bil";
            L_MilestoneTag2Bil.Size = new Size(20, 12);
            L_MilestoneTag2Bil.TabIndex = 31;
            L_MilestoneTag2Bil.Text = "bil.";
            // 
            // L_MilestoneTag1Bil
            // 
            L_MilestoneTag1Bil.AutoSize = true;
            L_MilestoneTag1Bil.Font = new Font("Lilita One", 8F);
            L_MilestoneTag1Bil.ForeColor = Color.Yellow;
            L_MilestoneTag1Bil.Location = new Point(69, 221);
            L_MilestoneTag1Bil.Name = "L_MilestoneTag1Bil";
            L_MilestoneTag1Bil.Size = new Size(20, 12);
            L_MilestoneTag1Bil.TabIndex = 30;
            L_MilestoneTag1Bil.Text = "bil.";
            // 
            // L_BonusStarrs
            // 
            L_BonusStarrs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_BonusStarrs.Font = new Font("Lilita One", 12F);
            L_BonusStarrs.ForeColor = Color.Yellow;
            L_BonusStarrs.Location = new Point(386, 140);
            L_BonusStarrs.Name = "L_BonusStarrs";
            L_BonusStarrs.Size = new Size(291, 19);
            L_BonusStarrs.TabIndex = 40;
            L_BonusStarrs.Text = "    ";
            L_BonusStarrs.TextAlign = ContentAlignment.TopRight;
            // 
            // BTN_Refresh
            // 
            BTN_Refresh.Font = new Font("Lilita One", 12F);
            BTN_Refresh.Location = new Point(12, 246);
            BTN_Refresh.Name = "BTN_Refresh";
            BTN_Refresh.Size = new Size(83, 33);
            BTN_Refresh.TabIndex = 41;
            BTN_Refresh.Text = "Refresh";
            BTN_Refresh.UseVisualStyleBackColor = true;
            BTN_Refresh.Click += BTN_Refresh_Click;
            // 
            // Chk_AutoRefresh
            // 
            Chk_AutoRefresh.AutoSize = true;
            Chk_AutoRefresh.Font = new Font("Lilita One", 12F);
            Chk_AutoRefresh.Location = new Point(101, 252);
            Chk_AutoRefresh.Name = "Chk_AutoRefresh";
            Chk_AutoRefresh.Size = new Size(118, 23);
            Chk_AutoRefresh.TabIndex = 42;
            Chk_AutoRefresh.Text = "Auto-refresh";
            Chk_AutoRefresh.UseVisualStyleBackColor = true;
            Chk_AutoRefresh.CheckedChanged += Chk_AutoRefresh_CheckedChanged;
            // 
            // BTN_FunButton
            // 
            BTN_FunButton.Font = new Font("Lilita One", 12F);
            BTN_FunButton.Location = new Point(576, 246);
            BTN_FunButton.Name = "BTN_FunButton";
            BTN_FunButton.Size = new Size(93, 33);
            BTN_FunButton.TabIndex = 666;
            BTN_FunButton.Text = "???";
            BTN_FunButton.UseVisualStyleBackColor = true;
            BTN_FunButton.Visible = false;
            BTN_FunButton.Click += BTN_FunButton_Click;
            // 
            // L_KillsAdded
            // 
            L_KillsAdded.AutoSize = true;
            L_KillsAdded.Font = new Font("Lilita One", 12F);
            L_KillsAdded.ForeColor = Color.FromArgb(255, 128, 0);
            L_KillsAdded.Location = new Point(12, 140);
            L_KillsAdded.Name = "L_KillsAdded";
            L_KillsAdded.Size = new Size(30, 19);
            L_KillsAdded.TabIndex = 44;
            L_KillsAdded.Text = "       ";
            // 
            // Timer_AutoRefresh
            // 
            Timer_AutoRefresh.Interval = 60000;
            Timer_AutoRefresh.Tick += Timer_AutoRefresh_Tick;
            // 
            // L_Version
            // 
            L_Version.Font = new Font("Lilita One", 10F);
            L_Version.Location = new Point(12, 46);
            L_Version.Name = "L_Version";
            L_Version.Size = new Size(368, 16);
            L_Version.TabIndex = 45;
            L_Version.Text = "Build 1.0.4beta";
            L_Version.TextAlign = ContentAlignment.TopRight;
            // 
            // LinkL_About
            // 
            LinkL_About.AutoSize = true;
            LinkL_About.Font = new Font("Lilita One", 12F);
            LinkL_About.Location = new Point(304, 253);
            LinkL_About.Name = "LinkL_About";
            LinkL_About.Size = new Size(114, 19);
            LinkL_About.TabIndex = 667;
            LinkL_About.TabStop = true;
            LinkL_About.Text = "About tracker...";
            LinkL_About.LinkClicked += LinkL_About_LinkClicked;
            // 
            // L_Status
            // 
            L_Status.Font = new Font("Lilita One", 12F);
            L_Status.Location = new Point(411, 69);
            L_Status.Name = "L_Status";
            L_Status.Size = new Size(262, 19);
            L_Status.TabIndex = 47;
            L_Status.Text = " ";
            L_Status.TextAlign = ContentAlignment.TopRight;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(18F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(684, 291);
            Controls.Add(L_Status);
            Controls.Add(LinkL_About);
            Controls.Add(L_Version);
            Controls.Add(L_KillsAdded);
            Controls.Add(BTN_FunButton);
            Controls.Add(Chk_AutoRefresh);
            Controls.Add(BTN_Refresh);
            Controls.Add(L_BonusStarrs);
            Controls.Add(L_MilestoneTag10Bil);
            Controls.Add(L_MilestoneTag9Bil);
            Controls.Add(L_MilestoneTag8Bil);
            Controls.Add(L_MilestoneTag7Bil);
            Controls.Add(L_MilestoneTag6Bil);
            Controls.Add(L_MilestoneTag5Bil);
            Controls.Add(L_MilestoneTag4Bil);
            Controls.Add(L_MilestoneTag3Bil);
            Controls.Add(L_MilestoneTag2Bil);
            Controls.Add(L_MilestoneTag1Bil);
            Controls.Add(L_MilestoneTag10C);
            Controls.Add(L_MilestoneTag9C);
            Controls.Add(L_MilestoneTag8C);
            Controls.Add(L_MilestoneTag7C);
            Controls.Add(L_MilestoneTag6C);
            Controls.Add(L_MilestoneTag5C);
            Controls.Add(L_MilestoneTag4C);
            Controls.Add(L_MilestoneTag3C);
            Controls.Add(L_MilestoneTag2C);
            Controls.Add(L_MilestoneTag1C);
            Controls.Add(L_MilestoneTag0C);
            Controls.Add(L_MilestoneTag10);
            Controls.Add(L_MilestoneTag9);
            Controls.Add(L_MilestoneTag8);
            Controls.Add(L_MilestoneTag7);
            Controls.Add(L_MilestoneTag6);
            Controls.Add(L_MilestoneTag5);
            Controls.Add(L_MilestoneTag4);
            Controls.Add(L_MilestoneTag3);
            Controls.Add(L_MilestoneTag2);
            Controls.Add(L_MilestoneTag1);
            Controls.Add(L_MilestoneTag0);
            Controls.Add(ProgBar_KillCountTo10B);
            Controls.Add(L_KillCountTitle2);
            Controls.Add(L_KillCount);
            Controls.Add(L_KillCountTitle);
            Controls.Add(ProgBar_TimeLeft);
            Controls.Add(L_TimeLeftLabel);
            Controls.Add(L_TimeLeft);
            Controls.Add(L_EventTitle);
            Font = new Font("Lilita One", 24F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(8, 6, 8, 6);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "#BrawlStarsP2W";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_EventTitle;
        private Label L_TimeLeft;
        private Label L_TimeLeftLabel;
        private ProgressBar ProgBar_TimeLeft;
        private System.Windows.Forms.Timer Timer_TimeLeft;
        private Label L_KillCountTitle;
        private Label L_KillCount;
        private Label L_KillCountTitle2;
        private ProgressBar ProgBar_KillCountTo10B;
        private Label L_MilestoneTag0;
        private Label L_MilestoneTag1;
        private Label L_MilestoneTag2;
        private Label L_MilestoneTag3;
        private Label L_MilestoneTag4;
        private Label L_MilestoneTag5;
        private Label L_MilestoneTag6;
        private Label L_MilestoneTag7;
        private Label L_MilestoneTag8;
        private Label L_MilestoneTag9;
        private Label L_MilestoneTag10;
        private Label L_MilestoneTag10C;
        private Label L_MilestoneTag9C;
        private Label L_MilestoneTag8C;
        private Label L_MilestoneTag7C;
        private Label L_MilestoneTag6C;
        private Label L_MilestoneTag5C;
        private Label L_MilestoneTag4C;
        private Label L_MilestoneTag3C;
        private Label L_MilestoneTag2C;
        private Label L_MilestoneTag1C;
        private Label L_MilestoneTag0C;
        private Label L_MilestoneTag10Bil;
        private Label L_MilestoneTag9Bil;
        private Label L_MilestoneTag8Bil;
        private Label L_MilestoneTag7Bil;
        private Label L_MilestoneTag6Bil;
        private Label L_MilestoneTag5Bil;
        private Label L_MilestoneTag4Bil;
        private Label L_MilestoneTag3Bil;
        private Label L_MilestoneTag2Bil;
        private Label L_MilestoneTag1Bil;
        private Label L_BonusStarrs;
        private Button BTN_Refresh;
        private CheckBox Chk_AutoRefresh;
        private Button BTN_FunButton;
        private Label L_KillsAdded;
        private System.Windows.Forms.Timer Timer_AutoRefresh;
        private Label L_Version;
        private LinkLabel LinkL_About;
        private Label L_Status;
    }
}