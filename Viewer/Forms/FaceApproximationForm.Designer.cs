namespace GeometricAlgorithms.Viewer.Forms
{
    partial class FaceApproximationForm
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
            this.samplesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.totalSamplesLabel = new System.Windows.Forms.Label();
            this.btnStartMarchingCubes = new System.Windows.Forms.Button();
            this.cbShowInsideSamples = new System.Windows.Forms.CheckBox();
            this.cbShowOutsideSamples = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.samplesNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // samplesNumericUpDown
            // 
            this.samplesNumericUpDown.Location = new System.Drawing.Point(105, 10);
            this.samplesNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.samplesNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.samplesNumericUpDown.Name = "samplesNumericUpDown";
            this.samplesNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.samplesNumericUpDown.TabIndex = 0;
            this.samplesNumericUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.samplesNumericUpDown.ValueChanged += new System.EventHandler(this.SamplesNumericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Samples per side";
            // 
            // totalSamplesLabel
            // 
            this.totalSamplesLabel.AutoSize = true;
            this.totalSamplesLabel.Location = new System.Drawing.Point(102, 33);
            this.totalSamplesLabel.Name = "totalSamplesLabel";
            this.totalSamplesLabel.Size = new System.Drawing.Size(72, 13);
            this.totalSamplesLabel.TabIndex = 2;
            this.totalSamplesLabel.Text = "Total samples";
            // 
            // btnStartMarchingCubes
            // 
            this.btnStartMarchingCubes.Location = new System.Drawing.Point(105, 65);
            this.btnStartMarchingCubes.Name = "btnStartMarchingCubes";
            this.btnStartMarchingCubes.Size = new System.Drawing.Size(120, 23);
            this.btnStartMarchingCubes.TabIndex = 3;
            this.btnStartMarchingCubes.Text = "Start Marching Cubes";
            this.btnStartMarchingCubes.UseVisualStyleBackColor = true;
            this.btnStartMarchingCubes.Click += new System.EventHandler(this.BtnStartMarchingCubes_Click);
            // 
            // cbShowInsideSamples
            // 
            this.cbShowInsideSamples.AutoSize = true;
            this.cbShowInsideSamples.Checked = true;
            this.cbShowInsideSamples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowInsideSamples.Location = new System.Drawing.Point(105, 109);
            this.cbShowInsideSamples.Name = "cbShowInsideSamples";
            this.cbShowInsideSamples.Size = new System.Drawing.Size(124, 17);
            this.cbShowInsideSamples.TabIndex = 4;
            this.cbShowInsideSamples.Text = "Show inside samples";
            this.cbShowInsideSamples.UseVisualStyleBackColor = true;
            this.cbShowInsideSamples.CheckedChanged += new System.EventHandler(this.CbShowInsideSamples_CheckedChanged);
            // 
            // cbShowOutsideSamples
            // 
            this.cbShowOutsideSamples.AutoSize = true;
            this.cbShowOutsideSamples.Checked = true;
            this.cbShowOutsideSamples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowOutsideSamples.Location = new System.Drawing.Point(105, 132);
            this.cbShowOutsideSamples.Name = "cbShowOutsideSamples";
            this.cbShowOutsideSamples.Size = new System.Drawing.Size(131, 17);
            this.cbShowOutsideSamples.TabIndex = 5;
            this.cbShowOutsideSamples.Text = "Show outside samples";
            this.cbShowOutsideSamples.UseVisualStyleBackColor = true;
            this.cbShowOutsideSamples.CheckedChanged += new System.EventHandler(this.CbShowOutsideSamples_CheckedChanged);
            // 
            // FaceApproximationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 168);
            this.Controls.Add(this.cbShowOutsideSamples);
            this.Controls.Add(this.cbShowInsideSamples);
            this.Controls.Add(this.btnStartMarchingCubes);
            this.Controls.Add(this.totalSamplesLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.samplesNumericUpDown);
            this.Name = "FaceApproximationForm";
            this.Text = "FaceApproximation";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FaceApproximationForm_FormClosed);
            this.Load += new System.EventHandler(this.FaceApproximationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.samplesNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown samplesNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalSamplesLabel;
        private System.Windows.Forms.Button btnStartMarchingCubes;
        private System.Windows.Forms.CheckBox cbShowInsideSamples;
        private System.Windows.Forms.CheckBox cbShowOutsideSamples;
    }
}