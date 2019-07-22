namespace GeometricAlgorithms.Viewer.Forms
{
    partial class KdTreeSettings
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
            this.generationSettingsPage = new System.Windows.Forms.TabPage();
            this.radiusSeachPage = new System.Windows.Forms.TabPage();
            this.kNearestSearchPage = new System.Windows.Forms.TabPage();
            this.searchRadiusNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStartRadiusSearch = new System.Windows.Forms.Button();
            this.btnAbortRadiusSearch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.closestPointCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pointsPerTreeLeafNumericUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.generationSettingsPage.SuspendLayout();
            this.radiusSeachPage.SuspendLayout();
            this.kNearestSearchPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchRadiusNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closestPointCountNumericUpDown)).BeginInit();
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
            this.tabControl1.Size = new System.Drawing.Size(369, 108);
            this.tabControl1.TabIndex = 5;
            // 
            // generationSettingsPage
            // 
            this.generationSettingsPage.Controls.Add(this.btnApplySettings);
            this.generationSettingsPage.Controls.Add(this.pointsPerTreeLeafNumericUpDown);
            this.generationSettingsPage.Controls.Add(this.label1);
            this.generationSettingsPage.Location = new System.Drawing.Point(4, 22);
            this.generationSettingsPage.Name = "generationSettingsPage";
            this.generationSettingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.generationSettingsPage.Size = new System.Drawing.Size(361, 82);
            this.generationSettingsPage.TabIndex = 0;
            this.generationSettingsPage.Text = "Tree Generation Settings";
            this.generationSettingsPage.UseVisualStyleBackColor = true;
            // 
            // radiusSeachPage
            // 
            this.radiusSeachPage.Controls.Add(this.btnAbortRadiusSearch);
            this.radiusSeachPage.Controls.Add(this.btnStartRadiusSearch);
            this.radiusSeachPage.Controls.Add(this.label2);
            this.radiusSeachPage.Controls.Add(this.searchRadiusNumericUpDown);
            this.radiusSeachPage.Location = new System.Drawing.Point(4, 22);
            this.radiusSeachPage.Name = "radiusSeachPage";
            this.radiusSeachPage.Padding = new System.Windows.Forms.Padding(3);
            this.radiusSeachPage.Size = new System.Drawing.Size(361, 82);
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
            this.kNearestSearchPage.Size = new System.Drawing.Size(361, 82);
            this.kNearestSearchPage.TabIndex = 2;
            this.kNearestSearchPage.Text = "Number Of Closest Search";
            this.kNearestSearchPage.UseVisualStyleBackColor = true;
            // 
            // searchRadiusNumericUpDown
            // 
            this.searchRadiusNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.searchRadiusNumericUpDown.Location = new System.Drawing.Point(52, 17);
            this.searchRadiusNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.searchRadiusNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.searchRadiusNumericUpDown.Name = "searchRadiusNumericUpDown";
            this.searchRadiusNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.searchRadiusNumericUpDown.TabIndex = 0;
            this.searchRadiusNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Radius";
            // 
            // btnStartRadiusSearch
            // 
            this.btnStartRadiusSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartRadiusSearch.Location = new System.Drawing.Point(9, 53);
            this.btnStartRadiusSearch.Name = "btnStartRadiusSearch";
            this.btnStartRadiusSearch.Size = new System.Drawing.Size(75, 23);
            this.btnStartRadiusSearch.TabIndex = 2;
            this.btnStartRadiusSearch.Text = "Start Search";
            this.btnStartRadiusSearch.UseVisualStyleBackColor = true;
            // 
            // btnAbortRadiusSearch
            // 
            this.btnAbortRadiusSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbortRadiusSearch.Location = new System.Drawing.Point(90, 53);
            this.btnAbortRadiusSearch.Name = "btnAbortRadiusSearch";
            this.btnAbortRadiusSearch.Size = new System.Drawing.Size(60, 23);
            this.btnAbortRadiusSearch.TabIndex = 3;
            this.btnAbortRadiusSearch.Text = "Cancel";
            this.btnAbortRadiusSearch.UseVisualStyleBackColor = true;
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
            // KdTreeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 115);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Name = "KdTreeSettings";
            this.Text = "KdTreeSettings";
            ((System.ComponentModel.ISupportInitialize)(this.pointsPerTreeLeafNumericUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.generationSettingsPage.ResumeLayout(false);
            this.generationSettingsPage.PerformLayout();
            this.radiusSeachPage.ResumeLayout(false);
            this.radiusSeachPage.PerformLayout();
            this.kNearestSearchPage.ResumeLayout(false);
            this.kNearestSearchPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchRadiusNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closestPointCountNumericUpDown)).EndInit();
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
        private System.Windows.Forms.Button btnStartRadiusSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown searchRadiusNumericUpDown;
        private System.Windows.Forms.Button btnAbortRadiusSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown closestPointCountNumericUpDown;
    }
}