namespace GeometricAlgorithms.Viewer
{
    partial class MainWindows
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.geometricAlgorithmViewer1 = new GeometricAlgorithms.Viewer.GeometricAlgorithmViewer();
            this.SuspendLayout();
            // 
            // geometricAlgorithmViewer1
            // 
            this.geometricAlgorithmViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.geometricAlgorithmViewer1.Location = new System.Drawing.Point(12, 12);
            this.geometricAlgorithmViewer1.Name = "geometricAlgorithmViewer1";
            this.geometricAlgorithmViewer1.Size = new System.Drawing.Size(562, 523);
            this.geometricAlgorithmViewer1.TabIndex = 0;
            // 
            // MainWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 547);
            this.Controls.Add(this.geometricAlgorithmViewer1);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "MainWindows";
            this.Text = "Point Viewer";
            this.ResumeLayout(false);

        }

        #endregion

        private GeometricAlgorithmViewer geometricAlgorithmViewer1;
    }
}

