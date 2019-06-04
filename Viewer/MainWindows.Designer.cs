namespace Viewer
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
            this.customGLControl1 = new Viewer.Rendering.CustomGLControl();
            this.SuspendLayout();
            // 
            // customGLControl1
            // 
            this.customGLControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.customGLControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.customGLControl1.BackColor = System.Drawing.Color.Black;
            this.customGLControl1.Location = new System.Drawing.Point(3, 3);
            this.customGLControl1.Name = "customGLControl1";
            this.customGLControl1.Size = new System.Drawing.Size(832, 727);
            this.customGLControl1.TabIndex = 0;
            this.customGLControl1.VSync = false;
            // 
            // MainWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 732);
            this.Controls.Add(this.customGLControl1);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "MainWindows";
            this.Text = "Point Viewer";
            this.ResumeLayout(false);

        }

        #endregion

        private Rendering.CustomGLControl customGLControl1;
    }
}

