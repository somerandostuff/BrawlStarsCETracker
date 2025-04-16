namespace Main
{
    partial class DeltaruneTomorrow
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
            L_TimeLeftLabel = new Label();
            L_DaysLabel = new Label();
            Timer_TimeLeft = new System.Windows.Forms.Timer(components);
            L_Days = new Label();
            L_Hours = new Label();
            L_Minutes = new Label();
            L_Seconds = new Label();
            L_HoursLabel = new Label();
            L_MinutesLabel = new Label();
            L_SecondsLabel = new Label();
            SuspendLayout();
            // 
            // L_TimeLeftLabel
            // 
            L_TimeLeftLabel.Font = new Font("Determination Mono Web", 12F);
            L_TimeLeftLabel.Location = new Point(12, 9);
            L_TimeLeftLabel.Name = "L_TimeLeftLabel";
            L_TimeLeftLabel.Size = new Size(319, 16);
            L_TimeLeftLabel.TabIndex = 0;
            L_TimeLeftLabel.Text = "Time left";
            L_TimeLeftLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // L_DaysLabel
            // 
            L_DaysLabel.AutoSize = true;
            L_DaysLabel.Font = new Font("Determination Mono Web", 12F);
            L_DaysLabel.Location = new Point(52, 48);
            L_DaysLabel.Name = "L_DaysLabel";
            L_DaysLabel.Size = new Size(39, 16);
            L_DaysLabel.TabIndex = 1;
            L_DaysLabel.Text = "days";
            // 
            // Timer_TimeLeft
            // 
            Timer_TimeLeft.Enabled = true;
            Timer_TimeLeft.Interval = 4;
            Timer_TimeLeft.Tick += Timer_TimeLeft_Tick;
            // 
            // L_Days
            // 
            L_Days.AutoSize = true;
            L_Days.Location = new Point(12, 35);
            L_Days.Name = "L_Days";
            L_Days.Size = new Size(47, 33);
            L_Days.TabIndex = 2;
            L_Days.Text = "00";
            // 
            // L_Hours
            // 
            L_Hours.AutoSize = true;
            L_Hours.Location = new Point(92, 35);
            L_Hours.Name = "L_Hours";
            L_Hours.Size = new Size(47, 33);
            L_Hours.TabIndex = 3;
            L_Hours.Text = "00";
            // 
            // L_Minutes
            // 
            L_Minutes.AutoSize = true;
            L_Minutes.Location = new Point(172, 35);
            L_Minutes.Name = "L_Minutes";
            L_Minutes.Size = new Size(47, 33);
            L_Minutes.TabIndex = 4;
            L_Minutes.Text = "00";
            // 
            // L_Seconds
            // 
            L_Seconds.AutoSize = true;
            L_Seconds.Location = new Point(252, 35);
            L_Seconds.Name = "L_Seconds";
            L_Seconds.Size = new Size(47, 33);
            L_Seconds.TabIndex = 5;
            L_Seconds.Text = "00";
            // 
            // L_HoursLabel
            // 
            L_HoursLabel.AutoSize = true;
            L_HoursLabel.Font = new Font("Determination Mono Web", 12F);
            L_HoursLabel.Location = new Point(133, 48);
            L_HoursLabel.Name = "L_HoursLabel";
            L_HoursLabel.Size = new Size(31, 16);
            L_HoursLabel.TabIndex = 6;
            L_HoursLabel.Text = "hrs";
            // 
            // L_MinutesLabel
            // 
            L_MinutesLabel.AutoSize = true;
            L_MinutesLabel.Font = new Font("Determination Mono Web", 12F);
            L_MinutesLabel.Location = new Point(212, 48);
            L_MinutesLabel.Name = "L_MinutesLabel";
            L_MinutesLabel.Size = new Size(39, 16);
            L_MinutesLabel.TabIndex = 7;
            L_MinutesLabel.Text = "mins";
            // 
            // L_SecondsLabel
            // 
            L_SecondsLabel.AutoSize = true;
            L_SecondsLabel.Font = new Font("Determination Mono Web", 12F);
            L_SecondsLabel.Location = new Point(292, 47);
            L_SecondsLabel.Name = "L_SecondsLabel";
            L_SecondsLabel.Size = new Size(39, 16);
            L_SecondsLabel.TabIndex = 8;
            L_SecondsLabel.Text = "secs";
            // 
            // DeltaruneTomorrow
            // 
            AutoScaleDimensions = new SizeF(16F, 33F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(343, 90);
            Controls.Add(L_SecondsLabel);
            Controls.Add(L_MinutesLabel);
            Controls.Add(L_HoursLabel);
            Controls.Add(L_DaysLabel);
            Controls.Add(L_Seconds);
            Controls.Add(L_Minutes);
            Controls.Add(L_Hours);
            Controls.Add(L_Days);
            Controls.Add(L_TimeLeftLabel);
            Font = new Font("Determination Mono Web", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(7, 6, 7, 6);
            MaximizeBox = false;
            Name = "DeltaruneTomorrow";
            Text = "Deltarune tomorrow";
            Load += DeltaruneTomorrow_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_TimeLeftLabel;
        private Label L_DaysLabel;
        private System.Windows.Forms.Timer Timer_TimeLeft;
        private Label L_Days;
        private Label L_Hours;
        private Label L_Minutes;
        private Label L_Seconds;
        private Label L_HoursLabel;
        private Label L_MinutesLabel;
        private Label L_SecondsLabel;
    }
}