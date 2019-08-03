namespace GeometricAlgorithms.Viewer.Forms.KdTreeControls
{
    partial class NearestQueryControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbAutoRefresh = new System.Windows.Forms.CheckBox();
            this.cbShowQueryCenter = new System.Windows.Forms.CheckBox();
            this.cbShowQueryResult = new System.Windows.Forms.CheckBox();
            this.btnStartNearestQuery = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.queryPointCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AutoRefreshTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.queryPointCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // cbAutoRefresh
            // 
            this.cbAutoRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAutoRefresh.AutoSize = true;
            this.cbAutoRefresh.Checked = true;
            this.cbAutoRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoRefresh.Location = new System.Drawing.Point(136, 78);
            this.cbAutoRefresh.Name = "cbAutoRefresh";
            this.cbAutoRefresh.Size = new System.Drawing.Size(83, 17);
            this.cbAutoRefresh.TabIndex = 15;
            this.cbAutoRefresh.Text = "Auto refresh";
            this.cbAutoRefresh.UseVisualStyleBackColor = true;
            this.cbAutoRefresh.CheckedChanged += new System.EventHandler(this.CbAutoRefresh_CheckedChanged);
            // 
            // cbShowQueryCenter
            // 
            this.cbShowQueryCenter.AutoSize = true;
            this.cbShowQueryCenter.Checked = true;
            this.cbShowQueryCenter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowQueryCenter.Location = new System.Drawing.Point(12, 44);
            this.cbShowQueryCenter.Name = "cbShowQueryCenter";
            this.cbShowQueryCenter.Size = new System.Drawing.Size(118, 17);
            this.cbShowQueryCenter.TabIndex = 14;
            this.cbShowQueryCenter.Text = "Show Query Center";
            this.cbShowQueryCenter.UseVisualStyleBackColor = true;
            this.cbShowQueryCenter.CheckedChanged += new System.EventHandler(this.CbShowQueryCenter_CheckedChanged);
            // 
            // cbShowQueryResult
            // 
            this.cbShowQueryResult.AutoSize = true;
            this.cbShowQueryResult.Checked = true;
            this.cbShowQueryResult.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowQueryResult.Location = new System.Drawing.Point(136, 44);
            this.cbShowQueryResult.Name = "cbShowQueryResult";
            this.cbShowQueryResult.Size = new System.Drawing.Size(86, 17);
            this.cbShowQueryResult.TabIndex = 13;
            this.cbShowQueryResult.Text = "Show Result";
            this.cbShowQueryResult.UseVisualStyleBackColor = true;
            this.cbShowQueryResult.CheckedChanged += new System.EventHandler(this.CbShowQueryResult_CheckedChanged);
            // 
            // btnStartNearestQuery
            // 
            this.btnStartNearestQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartNearestQuery.Location = new System.Drawing.Point(12, 74);
            this.btnStartNearestQuery.Name = "btnStartNearestQuery";
            this.btnStartNearestQuery.Size = new System.Drawing.Size(85, 23);
            this.btnStartNearestQuery.TabIndex = 12;
            this.btnStartNearestQuery.Text = "Start Query";
            this.btnStartNearestQuery.UseVisualStyleBackColor = true;
            this.btnStartNearestQuery.Click += new System.EventHandler(this.BtnStartNeaestSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Points";
            // 
            // queryPointCountNumericUpDown
            // 
            this.queryPointCountNumericUpDown.Location = new System.Drawing.Point(55, 15);
            this.queryPointCountNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.queryPointCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.queryPointCountNumericUpDown.Name = "queryPointCountNumericUpDown";
            this.queryPointCountNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.queryPointCountNumericUpDown.TabIndex = 10;
            this.queryPointCountNumericUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.queryPointCountNumericUpDown.ValueChanged += new System.EventHandler(this.QueryPointCountNumericUpDown_ValueChanged);
            // 
            // AutoRefreshTimer
            // 
            this.AutoRefreshTimer.Enabled = true;
            this.AutoRefreshTimer.Interval = 50;
            this.AutoRefreshTimer.Tick += new System.EventHandler(this.AutoRefreshTimer_Tick);
            // 
            // NearestQueryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbAutoRefresh);
            this.Controls.Add(this.cbShowQueryCenter);
            this.Controls.Add(this.cbShowQueryResult);
            this.Controls.Add(this.btnStartNearestQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.queryPointCountNumericUpDown);
            this.Name = "NearestQueryControl";
            this.Size = new System.Drawing.Size(236, 109);
            this.Load += new System.EventHandler(this.RadiusQueryControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.queryPointCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbAutoRefresh;
        private System.Windows.Forms.CheckBox cbShowQueryCenter;
        private System.Windows.Forms.CheckBox cbShowQueryResult;
        private System.Windows.Forms.Button btnStartNearestQuery;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown queryPointCountNumericUpDown;
        private System.Windows.Forms.Timer AutoRefreshTimer;
    }
}
