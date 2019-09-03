namespace GeometricAlgorithms.Viewer.Forms
{
    partial class TreeFaceApproximationForm
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
            this.numericMaxPointsPerLeaf = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartApproximation = new System.Windows.Forms.Button();
            this.btnInspectTree = new System.Windows.Forms.Button();
            this.btnUseFaces = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbShowOutsideSamples = new System.Windows.Forms.CheckBox();
            this.cbShowInsideSamples = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxPointsPerLeaf)).BeginInit();
            this.SuspendLayout();
            // 
            // numericMaxPointsPerLeaf
            // 
            this.numericMaxPointsPerLeaf.Location = new System.Drawing.Point(138, 16);
            this.numericMaxPointsPerLeaf.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericMaxPointsPerLeaf.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMaxPointsPerLeaf.Name = "numericMaxPointsPerLeaf";
            this.numericMaxPointsPerLeaf.Size = new System.Drawing.Size(112, 20);
            this.numericMaxPointsPerLeaf.TabIndex = 0;
            this.numericMaxPointsPerLeaf.TabStop = false;
            this.numericMaxPointsPerLeaf.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericMaxPointsPerLeaf.ValueChanged += new System.EventHandler(this.NumericMaxPointsPerLeaf_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Maximum points per leaf";
            // 
            // btnStartApproximation
            // 
            this.btnStartApproximation.Location = new System.Drawing.Point(138, 42);
            this.btnStartApproximation.Name = "btnStartApproximation";
            this.btnStartApproximation.Size = new System.Drawing.Size(112, 23);
            this.btnStartApproximation.TabIndex = 2;
            this.btnStartApproximation.Text = "Start approximation";
            this.btnStartApproximation.UseVisualStyleBackColor = true;
            this.btnStartApproximation.Click += new System.EventHandler(this.BtnStartApproximation_Click);
            // 
            // btnInspectTree
            // 
            this.btnInspectTree.Location = new System.Drawing.Point(138, 189);
            this.btnInspectTree.Name = "btnInspectTree";
            this.btnInspectTree.Size = new System.Drawing.Size(112, 23);
            this.btnInspectTree.TabIndex = 3;
            this.btnInspectTree.Text = "Inspect Tree";
            this.btnInspectTree.UseVisualStyleBackColor = true;
            this.btnInspectTree.Click += new System.EventHandler(this.BtnInspectTree_Click);
            // 
            // btnUseFaces
            // 
            this.btnUseFaces.Location = new System.Drawing.Point(138, 137);
            this.btnUseFaces.Name = "btnUseFaces";
            this.btnUseFaces.Size = new System.Drawing.Size(112, 23);
            this.btnUseFaces.TabIndex = 4;
            this.btnUseFaces.Text = "Use faces";
            this.btnUseFaces.UseVisualStyleBackColor = true;
            this.btnUseFaces.Click += new System.EventHandler(this.BtnUseFaces_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 60);
            this.label3.TabIndex = 10;
            this.label3.Text = "Replace positions and faces with approximated mesh. Normals will be lost";
            // 
            // cbShowOutsideSamples
            // 
            this.cbShowOutsideSamples.AutoSize = true;
            this.cbShowOutsideSamples.Location = new System.Drawing.Point(138, 100);
            this.cbShowOutsideSamples.Name = "cbShowOutsideSamples";
            this.cbShowOutsideSamples.Size = new System.Drawing.Size(131, 17);
            this.cbShowOutsideSamples.TabIndex = 12;
            this.cbShowOutsideSamples.Text = "Show outside samples";
            this.cbShowOutsideSamples.UseVisualStyleBackColor = true;
            this.cbShowOutsideSamples.CheckedChanged += new System.EventHandler(this.CbShowOutsideSamples_CheckedChanged);
            // 
            // cbShowInsideSamples
            // 
            this.cbShowInsideSamples.AutoSize = true;
            this.cbShowInsideSamples.Checked = true;
            this.cbShowInsideSamples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowInsideSamples.Location = new System.Drawing.Point(138, 77);
            this.cbShowInsideSamples.Name = "cbShowInsideSamples";
            this.cbShowInsideSamples.Size = new System.Drawing.Size(124, 17);
            this.cbShowInsideSamples.TabIndex = 11;
            this.cbShowInsideSamples.Text = "Show inside samples";
            this.cbShowInsideSamples.UseVisualStyleBackColor = true;
            this.cbShowInsideSamples.CheckedChanged += new System.EventHandler(this.CbShowInsideSamples_CheckedChanged);
            // 
            // TreeFaceApproximationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 228);
            this.Controls.Add(this.cbShowOutsideSamples);
            this.Controls.Add(this.cbShowInsideSamples);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUseFaces);
            this.Controls.Add(this.btnInspectTree);
            this.Controls.Add(this.btnStartApproximation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericMaxPointsPerLeaf);
            this.Name = "TreeFaceApproximationForm";
            this.Text = "TreeFaceApproximationForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TreeFaceApproximationForm_FormClosed);
            this.Load += new System.EventHandler(this.TreeFaceApproximationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxPointsPerLeaf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericMaxPointsPerLeaf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartApproximation;
        private System.Windows.Forms.Button btnInspectTree;
        private System.Windows.Forms.Button btnUseFaces;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbShowOutsideSamples;
        private System.Windows.Forms.CheckBox cbShowInsideSamples;
    }
}