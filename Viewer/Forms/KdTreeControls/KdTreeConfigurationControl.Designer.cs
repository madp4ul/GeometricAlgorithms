namespace GeometricAlgorithms.Viewer.Forms.KdTreeControls
{
    partial class KdTreeConfigurationControl
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
            this.buttonApplyPointsPerLeaf = new System.Windows.Forms.Button();
            this.numericPointsPerLeaf = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericPointsPerLeaf)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonApplyPointsPerLeaf
            // 
            this.buttonApplyPointsPerLeaf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplyPointsPerLeaf.Location = new System.Drawing.Point(121, 15);
            this.buttonApplyPointsPerLeaf.Name = "buttonApplyPointsPerLeaf";
            this.buttonApplyPointsPerLeaf.Size = new System.Drawing.Size(105, 23);
            this.buttonApplyPointsPerLeaf.TabIndex = 0;
            this.buttonApplyPointsPerLeaf.Text = "Rebuild Kd-Tree";
            this.buttonApplyPointsPerLeaf.UseVisualStyleBackColor = true;
            this.buttonApplyPointsPerLeaf.Click += new System.EventHandler(this.ButtonApplyPointsPerLeaf_Click);
            // 
            // numericPointsPerLeaf
            // 
            this.numericPointsPerLeaf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericPointsPerLeaf.Location = new System.Drawing.Point(3, 16);
            this.numericPointsPerLeaf.Name = "numericPointsPerLeaf";
            this.numericPointsPerLeaf.Size = new System.Drawing.Size(112, 20);
            this.numericPointsPerLeaf.TabIndex = 1;
            // 
            // KdTreeConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericPointsPerLeaf);
            this.Controls.Add(this.buttonApplyPointsPerLeaf);
            this.Name = "KdTreeConfigurationControl";
            this.Size = new System.Drawing.Size(229, 58);
            this.Load += new System.EventHandler(this.KdTreeConfigurationControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericPointsPerLeaf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonApplyPointsPerLeaf;
        private System.Windows.Forms.NumericUpDown numericPointsPerLeaf;
    }
}
