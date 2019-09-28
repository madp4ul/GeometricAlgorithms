namespace GeometricAlgorithms.Viewer.Forms
{
    partial class OctreeRefinementApproximationForm
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
            this.components = new System.ComponentModel.Container();
            this.btnStartApproximation = new System.Windows.Forms.Button();
            this.numericUpDownSampleLimit = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUseFaces = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbShowOutsideSamples = new System.Windows.Forms.CheckBox();
            this.cbShowInsideSamples = new System.Windows.Forms.CheckBox();
            this.lbSamplesComputed = new System.Windows.Forms.Label();
            this.btnToggleContinuousRefinement = new System.Windows.Forms.Button();
            this.continuousStepSize = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timerContinuousRefinement = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.continuousStepSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartApproximation
            // 
            this.btnStartApproximation.Location = new System.Drawing.Point(136, 62);
            this.btnStartApproximation.Name = "btnStartApproximation";
            this.btnStartApproximation.Size = new System.Drawing.Size(122, 23);
            this.btnStartApproximation.TabIndex = 0;
            this.btnStartApproximation.Text = "Start approximation";
            this.btnStartApproximation.UseVisualStyleBackColor = true;
            this.btnStartApproximation.Click += new System.EventHandler(this.BtnStartApproximation_Click);
            // 
            // numericUpDownSampleLimit
            // 
            this.numericUpDownSampleLimit.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownSampleLimit.Location = new System.Drawing.Point(136, 19);
            this.numericUpDownSampleLimit.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericUpDownSampleLimit.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownSampleLimit.Name = "numericUpDownSampleLimit";
            this.numericUpDownSampleLimit.Size = new System.Drawing.Size(122, 20);
            this.numericUpDownSampleLimit.TabIndex = 1;
            this.numericUpDownSampleLimit.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSampleLimit.ValueChanged += new System.EventHandler(this.NumericUpDownSampleLimit_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Implicit surface samples";
            // 
            // btnUseFaces
            // 
            this.btnUseFaces.Location = new System.Drawing.Point(136, 152);
            this.btnUseFaces.Name = "btnUseFaces";
            this.btnUseFaces.Size = new System.Drawing.Size(122, 23);
            this.btnUseFaces.TabIndex = 3;
            this.btnUseFaces.Text = "Use faces";
            this.btnUseFaces.UseVisualStyleBackColor = true;
            this.btnUseFaces.Click += new System.EventHandler(this.BtnUseFaces_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 60);
            this.label3.TabIndex = 11;
            this.label3.Text = "Replace positions and faces with approximated mesh. Normals will be lost";
            // 
            // cbShowOutsideSamples
            // 
            this.cbShowOutsideSamples.AutoSize = true;
            this.cbShowOutsideSamples.Location = new System.Drawing.Point(136, 122);
            this.cbShowOutsideSamples.Name = "cbShowOutsideSamples";
            this.cbShowOutsideSamples.Size = new System.Drawing.Size(131, 17);
            this.cbShowOutsideSamples.TabIndex = 14;
            this.cbShowOutsideSamples.Text = "Show outside samples";
            this.cbShowOutsideSamples.UseVisualStyleBackColor = true;
            this.cbShowOutsideSamples.CheckedChanged += new System.EventHandler(this.CbShowOutsideSamples_CheckedChanged);
            // 
            // cbShowInsideSamples
            // 
            this.cbShowInsideSamples.AutoSize = true;
            this.cbShowInsideSamples.Checked = true;
            this.cbShowInsideSamples.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowInsideSamples.Location = new System.Drawing.Point(136, 99);
            this.cbShowInsideSamples.Name = "cbShowInsideSamples";
            this.cbShowInsideSamples.Size = new System.Drawing.Size(124, 17);
            this.cbShowInsideSamples.TabIndex = 13;
            this.cbShowInsideSamples.Text = "Show inside samples";
            this.cbShowInsideSamples.UseVisualStyleBackColor = true;
            this.cbShowInsideSamples.CheckedChanged += new System.EventHandler(this.CbShowInsideSamples_CheckedChanged);
            // 
            // lbSamplesComputed
            // 
            this.lbSamplesComputed.AutoSize = true;
            this.lbSamplesComputed.Location = new System.Drawing.Point(139, 42);
            this.lbSamplesComputed.Name = "lbSamplesComputed";
            this.lbSamplesComputed.Size = new System.Drawing.Size(104, 13);
            this.lbSamplesComputed.TabIndex = 15;
            this.lbSamplesComputed.Text = "0 samples computed";
            // 
            // btnToggleContinuousRefinement
            // 
            this.btnToggleContinuousRefinement.Location = new System.Drawing.Point(136, 273);
            this.btnToggleContinuousRefinement.Name = "btnToggleContinuousRefinement";
            this.btnToggleContinuousRefinement.Size = new System.Drawing.Size(122, 23);
            this.btnToggleContinuousRefinement.TabIndex = 16;
            this.btnToggleContinuousRefinement.Text = "Start refinement";
            this.btnToggleContinuousRefinement.UseVisualStyleBackColor = true;
            this.btnToggleContinuousRefinement.Click += new System.EventHandler(this.BtnToggleContinuousRefinement_Click);
            // 
            // continuousStepSize
            // 
            this.continuousStepSize.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.continuousStepSize.Location = new System.Drawing.Point(136, 247);
            this.continuousStepSize.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.continuousStepSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.continuousStepSize.Name = "continuousStepSize";
            this.continuousStepSize.Size = new System.Drawing.Size(120, 20);
            this.continuousStepSize.TabIndex = 17;
            this.continuousStepSize.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Continuous refinement";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Samples per update";
            // 
            // timerContinuousRefinement
            // 
            this.timerContinuousRefinement.Interval = 250;
            this.timerContinuousRefinement.Tick += new System.EventHandler(this.TimerContinuousRefinement_Tick);
            // 
            // OctreeRefinementApproximationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 312);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.continuousStepSize);
            this.Controls.Add(this.btnToggleContinuousRefinement);
            this.Controls.Add(this.lbSamplesComputed);
            this.Controls.Add(this.cbShowOutsideSamples);
            this.Controls.Add(this.cbShowInsideSamples);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUseFaces);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownSampleLimit);
            this.Controls.Add(this.btnStartApproximation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OctreeRefinementApproximationForm";
            this.Opacity = 0.97D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Refinement Approximation";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OctreeRefinementApproximationForm_FormClosed);
            this.Load += new System.EventHandler(this.OctreeRefinementApproximationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.continuousStepSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartApproximation;
        private System.Windows.Forms.NumericUpDown numericUpDownSampleLimit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUseFaces;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbShowOutsideSamples;
        private System.Windows.Forms.CheckBox cbShowInsideSamples;
        private System.Windows.Forms.Label lbSamplesComputed;
        private System.Windows.Forms.Button btnToggleContinuousRefinement;
        private System.Windows.Forms.NumericUpDown continuousStepSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timerContinuousRefinement;
    }
}