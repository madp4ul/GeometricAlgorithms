namespace GeometricAlgorithms.Viewer.Forms
{
    partial class NormalApproximationForm
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
            this.rbUseFaces = new System.Windows.Forms.RadioButton();
            this.rbUsePositions = new System.Windows.Forms.RadioButton();
            this.btnApproximateNormals = new System.Windows.Forms.Button();
            this.btnUseApproximateNormals = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbUseFaces
            // 
            this.rbUseFaces.AutoSize = true;
            this.rbUseFaces.Location = new System.Drawing.Point(25, 12);
            this.rbUseFaces.Name = "rbUseFaces";
            this.rbUseFaces.Size = new System.Drawing.Size(73, 17);
            this.rbUseFaces.TabIndex = 0;
            this.rbUseFaces.TabStop = true;
            this.rbUseFaces.Text = "Use faces";
            this.rbUseFaces.UseVisualStyleBackColor = true;
            // 
            // rbUsePositions
            // 
            this.rbUsePositions.AutoSize = true;
            this.rbUsePositions.Location = new System.Drawing.Point(25, 35);
            this.rbUsePositions.Name = "rbUsePositions";
            this.rbUsePositions.Size = new System.Drawing.Size(110, 17);
            this.rbUsePositions.TabIndex = 1;
            this.rbUsePositions.TabStop = true;
            this.rbUsePositions.Text = "Use positions only";
            this.rbUsePositions.UseVisualStyleBackColor = true;
            // 
            // btnApproximateNormals
            // 
            this.btnApproximateNormals.Location = new System.Drawing.Point(25, 67);
            this.btnApproximateNormals.Name = "btnApproximateNormals";
            this.btnApproximateNormals.Size = new System.Drawing.Size(141, 23);
            this.btnApproximateNormals.TabIndex = 2;
            this.btnApproximateNormals.Text = "Start Approximation";
            this.btnApproximateNormals.UseVisualStyleBackColor = true;
            this.btnApproximateNormals.Click += new System.EventHandler(this.BtnApproximateNormals_Click);
            // 
            // btnUseApproximateNormals
            // 
            this.btnUseApproximateNormals.Location = new System.Drawing.Point(25, 96);
            this.btnUseApproximateNormals.Name = "btnUseApproximateNormals";
            this.btnUseApproximateNormals.Size = new System.Drawing.Size(141, 23);
            this.btnUseApproximateNormals.TabIndex = 3;
            this.btnUseApproximateNormals.Text = "Use approximated normals";
            this.btnUseApproximateNormals.UseVisualStyleBackColor = true;
            this.btnUseApproximateNormals.Click += new System.EventHandler(this.BtnUseApproximateNormals_Click);
            // 
            // NormalApproximationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 132);
            this.Controls.Add(this.btnUseApproximateNormals);
            this.Controls.Add(this.btnApproximateNormals);
            this.Controls.Add(this.rbUsePositions);
            this.Controls.Add(this.rbUseFaces);
            this.Name = "NormalApproximationForm";
            this.Text = "NormalApproximationForm";
            this.Load += new System.EventHandler(this.NormalApproximationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbUseFaces;
        private System.Windows.Forms.RadioButton rbUsePositions;
        private System.Windows.Forms.Button btnApproximateNormals;
        private System.Windows.Forms.Button btnUseApproximateNormals;
    }
}