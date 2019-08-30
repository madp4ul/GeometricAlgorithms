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
            this.cbMinimizeBranches = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericContainerScale = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericPointsPerLeaf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericContainerScale)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonApplyPointsPerLeaf
            // 
            this.buttonApplyPointsPerLeaf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplyPointsPerLeaf.Location = new System.Drawing.Point(105, 100);
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
            this.numericPointsPerLeaf.Location = new System.Drawing.Point(105, 21);
            this.numericPointsPerLeaf.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericPointsPerLeaf.Name = "numericPointsPerLeaf";
            this.numericPointsPerLeaf.Size = new System.Drawing.Size(112, 20);
            this.numericPointsPerLeaf.TabIndex = 1;
            // 
            // cbMinimizeBranches
            // 
            this.cbMinimizeBranches.AutoSize = true;
            this.cbMinimizeBranches.Checked = true;
            this.cbMinimizeBranches.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMinimizeBranches.Location = new System.Drawing.Point(105, 77);
            this.cbMinimizeBranches.Name = "cbMinimizeBranches";
            this.cbMinimizeBranches.Size = new System.Drawing.Size(113, 17);
            this.cbMinimizeBranches.TabIndex = 2;
            this.cbMinimizeBranches.Text = "Minimize branches";
            this.cbMinimizeBranches.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Points per leaf";
            // 
            // numericContainerScale
            // 
            this.numericContainerScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericContainerScale.DecimalPlaces = 2;
            this.numericContainerScale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericContainerScale.Location = new System.Drawing.Point(105, 48);
            this.numericContainerScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericContainerScale.Name = "numericContainerScale";
            this.numericContainerScale.Size = new System.Drawing.Size(112, 20);
            this.numericContainerScale.TabIndex = 4;
            this.numericContainerScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Container scale";
            // 
            // KdTreeConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericContainerScale);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbMinimizeBranches);
            this.Controls.Add(this.numericPointsPerLeaf);
            this.Controls.Add(this.buttonApplyPointsPerLeaf);
            this.Name = "KdTreeConfigurationControl";
            this.Size = new System.Drawing.Size(229, 146);
            this.Load += new System.EventHandler(this.KdTreeConfigurationControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericPointsPerLeaf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericContainerScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonApplyPointsPerLeaf;
        private System.Windows.Forms.NumericUpDown numericPointsPerLeaf;
        private System.Windows.Forms.CheckBox cbMinimizeBranches;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericContainerScale;
        private System.Windows.Forms.Label label2;
    }
}
