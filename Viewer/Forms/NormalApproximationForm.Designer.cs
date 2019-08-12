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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rbUseFaces
            // 
            this.rbUseFaces.AutoSize = true;
            this.rbUseFaces.Location = new System.Drawing.Point(138, 17);
            this.rbUseFaces.Name = "rbUseFaces";
            this.rbUseFaces.Size = new System.Drawing.Size(73, 17);
            this.rbUseFaces.TabIndex = 0;
            this.rbUseFaces.TabStop = true;
            this.rbUseFaces.Text = "Use faces";
            this.rbUseFaces.UseVisualStyleBackColor = true;
            this.rbUseFaces.CheckedChanged += new System.EventHandler(this.RbUseFaces_CheckedChanged);
            // 
            // rbUsePositions
            // 
            this.rbUsePositions.AutoSize = true;
            this.rbUsePositions.Location = new System.Drawing.Point(138, 40);
            this.rbUsePositions.Name = "rbUsePositions";
            this.rbUsePositions.Size = new System.Drawing.Size(110, 17);
            this.rbUsePositions.TabIndex = 1;
            this.rbUsePositions.TabStop = true;
            this.rbUsePositions.Text = "Use positions only";
            this.rbUsePositions.UseVisualStyleBackColor = true;
            this.rbUsePositions.CheckedChanged += new System.EventHandler(this.RbUsePositions_CheckedChanged);
            // 
            // btnApproximateNormals
            // 
            this.btnApproximateNormals.Location = new System.Drawing.Point(138, 72);
            this.btnApproximateNormals.Name = "btnApproximateNormals";
            this.btnApproximateNormals.Size = new System.Drawing.Size(141, 23);
            this.btnApproximateNormals.TabIndex = 2;
            this.btnApproximateNormals.Text = "Start Approximation";
            this.btnApproximateNormals.UseVisualStyleBackColor = true;
            this.btnApproximateNormals.Click += new System.EventHandler(this.BtnApproximateNormals_Click);
            // 
            // btnUseApproximateNormals
            // 
            this.btnUseApproximateNormals.Location = new System.Drawing.Point(138, 109);
            this.btnUseApproximateNormals.Name = "btnUseApproximateNormals";
            this.btnUseApproximateNormals.Size = new System.Drawing.Size(141, 33);
            this.btnUseApproximateNormals.TabIndex = 3;
            this.btnUseApproximateNormals.Text = "Use approximated normals";
            this.btnUseApproximateNormals.UseVisualStyleBackColor = true;
            this.btnUseApproximateNormals.Click += new System.EventHandler(this.BtnUseApproximateNormals_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 46);
            this.label1.TabIndex = 4;
            this.label1.Text = "Replace mesh normals with approximated normals";
            // 
            // NormalApproximationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 168);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUseApproximateNormals);
            this.Controls.Add(this.btnApproximateNormals);
            this.Controls.Add(this.rbUsePositions);
            this.Controls.Add(this.rbUseFaces);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NormalApproximationForm";
            this.Opacity = 0.97D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Normal Approximation";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NormalApproximationForm_FormClosed);
            this.Load += new System.EventHandler(this.NormalApproximationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbUseFaces;
        private System.Windows.Forms.RadioButton rbUsePositions;
        private System.Windows.Forms.Button btnApproximateNormals;
        private System.Windows.Forms.Button btnUseApproximateNormals;
        private System.Windows.Forms.Label label1;
    }
}