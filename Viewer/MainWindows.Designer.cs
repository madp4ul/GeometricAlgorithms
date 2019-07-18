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
            GeometricAlgorithms.MonoGame.Forms.Scene scene1 = new GeometricAlgorithms.MonoGame.Forms.Scene();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindows));
            this.viewer = new GeometricAlgorithms.MonoGame.Forms.Viewer3D();
            this.SuspendLayout();
            // 
            // viewer
            // 
            this.viewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewer.Location = new System.Drawing.Point(12, 12);
            this.viewer.Name = "viewer";
            scene1.Camera = null;
            scene1.Drawables = ((System.Collections.Generic.List<GeometricAlgorithms.MonoGame.Forms.Drawables.IDrawable>)(resources.GetObject("scene1.Drawables")));
            this.viewer.Scene = scene1;
            this.viewer.Size = new System.Drawing.Size(810, 613);
            this.viewer.TabIndex = 0;
            this.viewer.Load += new System.EventHandler(this.Viewer3D_Load);
            // 
            // MainWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 637);
            this.Controls.Add(this.viewer);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "MainWindows";
            this.Text = "Point Viewer";
            this.ResumeLayout(false);

        }

        #endregion

        private MonoGame.Forms.Viewer3D viewer;
    }
}

