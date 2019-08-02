namespace GeometricAlgorithms.Viewer
{
    partial class GeometricAlgorithmViewer
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
            this.viewer = new GeometricAlgorithms.MonoGame.Forms.Viewer3D();
            this.inputEventTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // viewer
            // 
            this.viewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewer.Location = new System.Drawing.Point(0, 0);
            this.viewer.Name = "viewer";
            this.viewer.Size = new System.Drawing.Size(533, 383);
            this.viewer.TabIndex = 0;
            this.viewer.Load += new System.EventHandler(this.Viewer_Load);
            // 
            // inputEventTimer
            // 
            this.inputEventTimer.Enabled = true;
            this.inputEventTimer.Interval = 33;
            this.inputEventTimer.Tick += new System.EventHandler(this.KeyEventTimer_Tick);
            // 
            // GeometricAlgorithmViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.viewer);
            this.Name = "GeometricAlgorithmViewer";
            this.Size = new System.Drawing.Size(533, 383);
            this.Resize += new System.EventHandler(this.GeometricAlgorithmViewer_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private MonoGame.Forms.Viewer3D viewer;
        private System.Windows.Forms.Timer inputEventTimer;
    }
}
