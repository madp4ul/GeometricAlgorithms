namespace GeometricAlgorithms.Viewer.Forms
{
    partial class KdTreeQueriesForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageRadiusQuery = new System.Windows.Forms.TabPage();
            this.tabPageNearestQuery = new System.Windows.Forms.TabPage();
            this.kdTreeConfigurationControl = new GeometricAlgorithms.Viewer.Forms.KdTreeControls.KdTreeConfigurationControl();
            this.radiusQueryControl = new GeometricAlgorithms.Viewer.Forms.KdTreeControls.RadiusQueryControl();
            this.nearestQueryControl = new GeometricAlgorithms.Viewer.Forms.KdTreeControls.NearestQueryControl();
            this.tabControl.SuspendLayout();
            this.tabPageRadiusQuery.SuspendLayout();
            this.tabPageNearestQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageRadiusQuery);
            this.tabControl.Controls.Add(this.tabPageNearestQuery);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(250, 141);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabPageRadiusQuery
            // 
            this.tabPageRadiusQuery.Controls.Add(this.radiusQueryControl);
            this.tabPageRadiusQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageRadiusQuery.Name = "tabPageRadiusQuery";
            this.tabPageRadiusQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRadiusQuery.Size = new System.Drawing.Size(242, 115);
            this.tabPageRadiusQuery.TabIndex = 0;
            this.tabPageRadiusQuery.Text = "Points in Radius Query";
            this.tabPageRadiusQuery.UseVisualStyleBackColor = true;
            // 
            // tabPageNearestQuery
            // 
            this.tabPageNearestQuery.Controls.Add(this.nearestQueryControl);
            this.tabPageNearestQuery.Location = new System.Drawing.Point(4, 22);
            this.tabPageNearestQuery.Name = "tabPageNearestQuery";
            this.tabPageNearestQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNearestQuery.Size = new System.Drawing.Size(242, 115);
            this.tabPageNearestQuery.TabIndex = 1;
            this.tabPageNearestQuery.Text = "Nearest Points Query";
            this.tabPageNearestQuery.UseVisualStyleBackColor = true;
            // 
            // kdTreeConfigurationControl
            // 
            this.kdTreeConfigurationControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kdTreeConfigurationControl.KdTree = null;
            this.kdTreeConfigurationControl.Location = new System.Drawing.Point(16, 159);
            this.kdTreeConfigurationControl.Name = "kdTreeConfigurationControl";
            this.kdTreeConfigurationControl.Size = new System.Drawing.Size(242, 106);
            this.kdTreeConfigurationControl.TabIndex = 1;
            // 
            // radiusQueryControl
            // 
            this.radiusQueryControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radiusQueryControl.Location = new System.Drawing.Point(6, 6);
            this.radiusQueryControl.Name = "radiusQueryControl";
            this.radiusQueryControl.QueryData = null;
            this.radiusQueryControl.Size = new System.Drawing.Size(230, 103);
            this.radiusQueryControl.TabIndex = 0;
            // 
            // nearestQueryControl
            // 
            this.nearestQueryControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nearestQueryControl.Location = new System.Drawing.Point(6, 6);
            this.nearestQueryControl.Name = "nearestQueryControl";
            this.nearestQueryControl.QueryData = null;
            this.nearestQueryControl.Size = new System.Drawing.Size(233, 103);
            this.nearestQueryControl.TabIndex = 0;
            // 
            // KdTreeQueriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 271);
            this.Controls.Add(this.kdTreeConfigurationControl);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KdTreeQueriesForm";
            this.Opacity = 0.97D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Query Kd-Tree";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KdTreeSettings_FormClosed);
            this.Load += new System.EventHandler(this.KdTreeOptions_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageRadiusQuery.ResumeLayout(false);
            this.tabPageNearestQuery.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageRadiusQuery;
        private System.Windows.Forms.TabPage tabPageNearestQuery;
        private KdTreeControls.RadiusQueryControl radiusQueryControl;
        private KdTreeControls.NearestQueryControl nearestQueryControl;
        private KdTreeControls.KdTreeConfigurationControl kdTreeConfigurationControl;
    }
}