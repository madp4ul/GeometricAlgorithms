namespace GeometricAlgorithms.MonoGame.Forms
{
    partial class Viewer3D
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer3D));
            this.monoGameControl = new GeometricAlgorithms.MonoGame.Forms.MonoGameControl();
            this.SuspendLayout();
            // 
            // monoGameControl
            // 
            this.monoGameControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monoGameControl.Location = new System.Drawing.Point(3, 3);
            this.monoGameControl.Name = "monoGameControl";
            this.monoGameControl.Size = new System.Drawing.Size(794, 444);
            this.monoGameControl.TabIndex = 0;
            this.monoGameControl.Text = "monoGameControl1";
            // 
            // Viewer3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.monoGameControl);
            this.Name = "Viewer3D";
            this.Size = new System.Drawing.Size(800, 450);
            this.ResumeLayout(false);

        }

        #endregion

        private MonoGameControl monoGameControl;
    }
}
