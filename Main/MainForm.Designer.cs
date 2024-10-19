namespace Main
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            BTN_Load = new Button();
            L_ProgressLabel = new Label();
            L_ProgressCount = new Label();
            L_Version = new Label();
            L_Percent = new Label();
            L_LastUpd = new Label();
            BTN_About = new Button();
            Chk_AutoRefresh = new CheckBox();
            L_Status = new Label();
            SuspendLayout();
            // 
            // BTN_Load
            // 
            BTN_Load.Location = new Point(326, 113);
            BTN_Load.Name = "BTN_Load";
            BTN_Load.Size = new Size(117, 31);
            BTN_Load.TabIndex = 2;
            BTN_Load.Text = "Load/Refresh";
            BTN_Load.UseVisualStyleBackColor = true;
            BTN_Load.Click += BTN_Load_Click;
            // 
            // L_ProgressLabel
            // 
            L_ProgressLabel.AutoSize = true;
            L_ProgressLabel.Location = new Point(12, 9);
            L_ProgressLabel.Name = "L_ProgressLabel";
            L_ProgressLabel.Size = new Size(170, 19);
            L_ProgressLabel.TabIndex = 3;
            L_ProgressLabel.Text = "Approximated progress";
            // 
            // L_ProgressCount
            // 
            L_ProgressCount.Font = new Font("Lilita One", 39.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            L_ProgressCount.ForeColor = Color.Lime;
            L_ProgressCount.Location = new Point(12, 28);
            L_ProgressCount.Name = "L_ProgressCount";
            L_ProgressCount.Size = new Size(443, 60);
            L_ProgressCount.TabIndex = 4;
            L_ProgressCount.Text = "0";
            L_ProgressCount.TextAlign = ContentAlignment.TopRight;
            // 
            // L_Version
            // 
            L_Version.AutoSize = true;
            L_Version.Location = new Point(12, 125);
            L_Version.Name = "L_Version";
            L_Version.Size = new Size(47, 19);
            L_Version.TabIndex = 5;
            L_Version.Text = "v1.0.1";
            // 
            // L_Percent
            // 
            L_Percent.Font = new Font("Lilita One", 12F);
            L_Percent.Location = new Point(313, 88);
            L_Percent.Name = "L_Percent";
            L_Percent.Size = new Size(130, 22);
            L_Percent.TabIndex = 6;
            L_Percent.Text = "(0%)";
            L_Percent.TextAlign = ContentAlignment.TopRight;
            // 
            // L_LastUpd
            // 
            L_LastUpd.Location = new Point(12, 88);
            L_LastUpd.Name = "L_LastUpd";
            L_LastUpd.Size = new Size(295, 22);
            L_LastUpd.TabIndex = 7;
            L_LastUpd.Text = "Last refreshed: N/A";
            L_LastUpd.TextAlign = ContentAlignment.TopCenter;
            // 
            // BTN_About
            // 
            BTN_About.Location = new Point(245, 113);
            BTN_About.Name = "BTN_About";
            BTN_About.Size = new Size(75, 31);
            BTN_About.TabIndex = 8;
            BTN_About.Text = "About";
            BTN_About.UseVisualStyleBackColor = true;
            BTN_About.Click += BTN_About_Click;
            // 
            // Chk_AutoRefresh
            // 
            Chk_AutoRefresh.AutoSize = true;
            Chk_AutoRefresh.Location = new Point(121, 121);
            Chk_AutoRefresh.Name = "Chk_AutoRefresh";
            Chk_AutoRefresh.Size = new Size(118, 23);
            Chk_AutoRefresh.TabIndex = 9;
            Chk_AutoRefresh.Text = "Auto-refresh";
            Chk_AutoRefresh.UseVisualStyleBackColor = true;
            Chk_AutoRefresh.CheckedChanged += Chk_AutoRefresh_CheckedChanged;
            // 
            // L_Status
            // 
            L_Status.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            L_Status.Location = new Point(273, 9);
            L_Status.Name = "L_Status";
            L_Status.Size = new Size(170, 19);
            L_Status.TabIndex = 10;
            L_Status.Text = "  ";
            L_Status.TextAlign = ContentAlignment.TopRight;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(455, 155);
            Controls.Add(L_Status);
            Controls.Add(Chk_AutoRefresh);
            Controls.Add(BTN_About);
            Controls.Add(L_LastUpd);
            Controls.Add(L_Version);
            Controls.Add(L_ProgressLabel);
            Controls.Add(BTN_Load);
            Controls.Add(L_ProgressCount);
            Controls.Add(L_Percent);
            Font = new Font("Lilita One", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Brawl Stars Community event Tracker (Basic)";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BTN_Load;
        private Label L_ProgressLabel;
        private Label L_ProgressCount;
        private Label L_Version;
        private Label L_Percent;
        private Label L_LastUpd;
        private Button BTN_About;
        private CheckBox Chk_AutoRefresh;
        private Label L_Status;
    }
}
