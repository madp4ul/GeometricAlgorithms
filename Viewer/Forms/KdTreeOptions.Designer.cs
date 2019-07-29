namespace GeometricAlgorithms.Viewer.Forms
{
    partial class KdTreeOptions
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
            this.btnApplySettings = new System.Windows.Forms.Button();
            this.pointsPerTreeLeafNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.radiusSeachPage = new System.Windows.Forms.TabPage();
            this.kNearestSearchPage = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.closestPointCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.generationSettingsPage = new System.Windows.Forms.TabPage();
            this.radiusQueryControl = new GeometricAlgorithms.Viewer.Forms.KdTreeControls.RadiusQueryControl();
            ((System.ComponentModel.ISupportInitialize)(this.pointsPerTreeLeafNumericUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.radiusSeachPage.SuspendLayout();
            this.kNearestSearchPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closestPointCountNumericUpDown)).BeginInit();
            this.generationSettingsPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApplySettings
            // 
            this.btnApplySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApplySettings.Location = new System.Drawing.Point(9, 53);
            this.btnApplySettings.Name = "btnApplySettings";
            this.btnApplySettings.Size = new System.Drawing.Size(92, 23);
            this.btnApplySettings.TabIndex = 0;
            this.btnApplySettings.Text = "Apply";
            this.btnApplySettings.UseVisualStyleBackColor = true;
            this.btnApplySettings.Click += new System.EventHandler(this.BtnApplySettings_Click);
            // 
            // pointsPerTreeLeafNumericUpDown
            // 
            this.pointsPerTreeLeafNumericUpDown.Location = new System.Drawing.Point(107, 20);
            this.pointsPerTreeLeafNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.pointsPerTreeLeafNumericUpDown.Name = "pointsPerTreeLeafNumericUpDown";
            this.pointsPerTreeLeafNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.pointsPerTreeLeafNumericUpDown.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Points per tree leaf";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.radiusSeachPage);
            this.tabControl1.Controls.Add(this.kNearestSearchPage);
            this.tabControl1.Controls.Add(this.generationSettingsPage);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(370, 140);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // radiusSeachPage
            // 
            this.radiusSeachPage.Controls.Add(this.radiusQueryControl);
            this.radiusSeachPage.Location = new System.Drawing.Point(4, 22);
            this.radiusSeachPage.Name = "radiusSeachPage";
            this.radiusSeachPage.Padding = new System.Windows.Forms.Padding(3);
            this.radiusSeachPage.Size = new System.Drawing.Size(362, 114);
            this.radiusSeachPage.TabIndex = 1;
            this.radiusSeachPage.Text = "In Radius Seach";
            this.radiusSeachPage.UseVisualStyleBackColor = true;
            // 
            // kNearestSearchPage
            // 
            this.kNearestSearchPage.Controls.Add(this.button1);
            this.kNearestSearchPage.Controls.Add(this.button2);
            this.kNearestSearchPage.Controls.Add(this.label4);
            this.kNearestSearchPage.Controls.Add(this.closestPointCountNumericUpDown);
            this.kNearestSearchPage.Location = new System.Drawing.Point(4, 22);
            this.kNearestSearchPage.Name = "kNearestSearchPage";
            this.kNearestSearchPage.Size = new System.Drawing.Size(362, 114);
            this.kNearestSearchPage.TabIndex = 2;
            this.kNearestSearchPage.Text = "Number Of Closest Search";
            this.kNearestSearchPage.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(90, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(9, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Start Search";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Number of closest points";
            // 
            // closestPointCountNumericUpDown
            // 
            this.closestPointCountNumericUpDown.Location = new System.Drawing.Point(138, 16);
            this.closestPointCountNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.closestPointCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.closestPointCountNumericUpDown.Name = "closestPointCountNumericUpDown";
            this.closestPointCountNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.closestPointCountNumericUpDown.TabIndex = 4;
            this.closestPointCountNumericUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // generationSettingsPage
            // 
            this.generationSettingsPage.Controls.Add(this.btnApplySettings);
            this.generationSettingsPage.Controls.Add(this.pointsPerTreeLeafNumericUpDown);
            this.generationSettingsPage.Controls.Add(this.label1);
            this.generationSettingsPage.Location = new System.Drawing.Point(4, 22);
            this.generationSettingsPage.Name = "generationSettingsPage";
            this.generationSettingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.generationSettingsPage.Size = new System.Drawing.Size(362, 114);
            this.generationSettingsPage.TabIndex = 0;
            this.generationSettingsPage.Text = "Tree Generation Settings";
            this.generationSettingsPage.UseVisualStyleBackColor = true;
            // 
            // radiusQueryControl
            // 
            this.radiusQueryControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radiusQueryControl.Location = new System.Drawing.Point(6, 7);
            this.radiusQueryControl.Name = "radiusQueryControl";
            this.radiusQueryControl.QueryData = null;
            this.radiusQueryControl.Size = new System.Drawing.Size(350, 101);
            this.radiusQueryControl.TabIndex = 0;
            this.radiusQueryControl.Viewer = null;
            // 
            // KdTreeOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 147);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Name = "KdTreeOptions";
            this.Text = "KdTreeSettings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KdTreeSettings_FormClosed);
            this.Load += new System.EventHandler(this.KdTreeOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pointsPerTreeLeafNumericUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.radiusSeachPage.ResumeLayout(false);
            this.kNearestSearchPage.ResumeLayout(false);
            this.kNearestSearchPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closestPointCountNumericUpDown)).EndInit();
            this.generationSettingsPage.ResumeLayout(false);
            this.generationSettingsPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnApplySettings;
        private System.Windows.Forms.NumericUpDown pointsPerTreeLeafNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage radiusSeachPage;
        private System.Windows.Forms.TabPage kNearestSearchPage;
        private System.Windows.Forms.TabPage generationSettingsPage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown closestPointCountNumericUpDown;
        private KdTreeControls.RadiusQueryControl radiusQueryControl;
    }
}