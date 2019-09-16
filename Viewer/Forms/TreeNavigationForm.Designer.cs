namespace GeometricAlgorithms.Viewer.Forms
{
    partial class TreeNavigationForm
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
            this.btnToRoot = new System.Windows.Forms.Button();
            this.btnToParent = new System.Windows.Forms.Button();
            this.lbCurrent = new System.Windows.Forms.Label();
            this.pnChildren = new System.Windows.Forms.Panel();
            this.btnDebug = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnToRoot
            // 
            this.btnToRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToRoot.Location = new System.Drawing.Point(12, 12);
            this.btnToRoot.Name = "btnToRoot";
            this.btnToRoot.Size = new System.Drawing.Size(220, 23);
            this.btnToRoot.TabIndex = 0;
            this.btnToRoot.Text = "Go to root";
            this.btnToRoot.UseVisualStyleBackColor = true;
            this.btnToRoot.Click += new System.EventHandler(this.BtnToRoot_Click);
            // 
            // btnToParent
            // 
            this.btnToParent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToParent.Location = new System.Drawing.Point(12, 41);
            this.btnToParent.Name = "btnToParent";
            this.btnToParent.Size = new System.Drawing.Size(220, 23);
            this.btnToParent.TabIndex = 1;
            this.btnToParent.Text = "Go to parent";
            this.btnToParent.UseVisualStyleBackColor = true;
            this.btnToParent.Click += new System.EventHandler(this.BtnToParent_Click);
            // 
            // lbCurrent
            // 
            this.lbCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCurrent.Location = new System.Drawing.Point(12, 70);
            this.lbCurrent.Name = "lbCurrent";
            this.lbCurrent.Size = new System.Drawing.Size(157, 23);
            this.lbCurrent.TabIndex = 2;
            this.lbCurrent.Text = "label1";
            this.lbCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnChildren
            // 
            this.pnChildren.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnChildren.AutoScroll = true;
            this.pnChildren.Location = new System.Drawing.Point(12, 99);
            this.pnChildren.Name = "pnChildren";
            this.pnChildren.Size = new System.Drawing.Size(220, 335);
            this.pnChildren.TabIndex = 3;
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(175, 70);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(56, 23);
            this.btnDebug.TabIndex = 4;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.BtnDebug_Click);
            // 
            // TreeNavigationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 456);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.pnChildren);
            this.Controls.Add(this.lbCurrent);
            this.Controls.Add(this.btnToParent);
            this.Controls.Add(this.btnToRoot);
            this.Name = "TreeNavigationForm";
            this.Text = "TreeNavigationForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TreeNavigationForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnToRoot;
        private System.Windows.Forms.Button btnToParent;
        private System.Windows.Forms.Label lbCurrent;
        private System.Windows.Forms.Panel pnChildren;
        private System.Windows.Forms.Button btnDebug;
    }
}