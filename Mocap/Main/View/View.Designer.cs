namespace Main.View
{
    partial class FlowView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.SplitContainer splitContainer1;
            this.VideoPicBox = new System.Windows.Forms.PictureBox();
            this.SkiletPicBox = new System.Windows.Forms.PictureBox();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkiletPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Enabled = false;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(this.VideoPicBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(this.SkiletPicBox);
            splitContainer1.Size = new System.Drawing.Size(643, 220);
            splitContainer1.SplitterDistance = 331;
            splitContainer1.SplitterWidth = 1;
            splitContainer1.TabIndex = 0;
            // 
            // VideoPicBox
            // 
            this.VideoPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoPicBox.Location = new System.Drawing.Point(0, 0);
            this.VideoPicBox.Name = "VideoPicBox";
            this.VideoPicBox.Size = new System.Drawing.Size(329, 218);
            this.VideoPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.VideoPicBox.TabIndex = 0;
            this.VideoPicBox.TabStop = false;
            // 
            // SkiletPicBox
            // 
            this.SkiletPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SkiletPicBox.Location = new System.Drawing.Point(0, 0);
            this.SkiletPicBox.Name = "SkiletPicBox";
            this.SkiletPicBox.Size = new System.Drawing.Size(309, 218);
            this.SkiletPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SkiletPicBox.TabIndex = 0;
            this.SkiletPicBox.TabStop = false;
            // 
            // FlowView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(splitContainer1);
            this.Name = "FlowView";
            this.Size = new System.Drawing.Size(643, 220);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkiletPicBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox VideoPicBox;
        private System.Windows.Forms.PictureBox SkiletPicBox;

    }
}
