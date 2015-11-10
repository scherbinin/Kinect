using System.Windows.Forms;

namespace Main
{
    partial class MainForm
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
            this.flowViewer1 = new Main.View.FlowView();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnLoadRecord = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpCamera1 = new System.Windows.Forms.TabPage();
            this.tbCamera2 = new System.Windows.Forms.TabPage();
            this.flowViewer2 = new Main.View.FlowView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.efwefToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSmoothOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.смещенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tpCamera1.SuspendLayout();
            this.tbCamera2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowViewer1
            // 
            this.flowViewer1.Device = null;
            this.flowViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowViewer1.Location = new System.Drawing.Point(3, 3);
            this.flowViewer1.Name = "flowViewer1";
            this.flowViewer1.Size = new System.Drawing.Size(696, 281);
            this.flowViewer1.TabIndex = 16;
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecord.Location = new System.Drawing.Point(12, 341);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 18;
            this.btnRecord.Text = "Запись";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnLoadRecord
            // 
            this.btnLoadRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadRecord.Location = new System.Drawing.Point(637, 341);
            this.btnLoadRecord.Name = "btnLoadRecord";
            this.btnLoadRecord.Size = new System.Drawing.Size(75, 23);
            this.btnLoadRecord.TabIndex = 19;
            this.btnLoadRecord.Text = "Отладка";
            this.btnLoadRecord.UseVisualStyleBackColor = true;
            this.btnLoadRecord.Click += new System.EventHandler(this.btnLoadRecord_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpCamera1);
            this.tabControl1.Controls.Add(this.tbCamera2);
            this.tabControl1.Location = new System.Drawing.Point(12, 22);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(710, 313);
            this.tabControl1.TabIndex = 20;
            // 
            // tpCamera1
            // 
            this.tpCamera1.Controls.Add(this.flowViewer1);
            this.tpCamera1.Location = new System.Drawing.Point(4, 22);
            this.tpCamera1.Name = "tpCamera1";
            this.tpCamera1.Padding = new System.Windows.Forms.Padding(3);
            this.tpCamera1.Size = new System.Drawing.Size(702, 287);
            this.tpCamera1.TabIndex = 0;
            this.tpCamera1.Text = "Камера 1";
            this.tpCamera1.UseVisualStyleBackColor = true;
            // 
            // tbCamera2
            // 
            this.tbCamera2.Controls.Add(this.flowViewer2);
            this.tbCamera2.Location = new System.Drawing.Point(4, 22);
            this.tbCamera2.Name = "tbCamera2";
            this.tbCamera2.Padding = new System.Windows.Forms.Padding(3);
            this.tbCamera2.Size = new System.Drawing.Size(702, 287);
            this.tbCamera2.TabIndex = 1;
            this.tbCamera2.Text = "Камера 2";
            this.tbCamera2.UseVisualStyleBackColor = true;
            // 
            // flowViewer2
            // 
            this.flowViewer2.Device = null;
            this.flowViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowViewer2.Location = new System.Drawing.Point(3, 3);
            this.flowViewer2.Name = "flowViewer2";
            this.flowViewer2.Size = new System.Drawing.Size(696, 281);
            this.flowViewer2.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.efwefToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(725, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // efwefToolStripMenuItem
            // 
            this.efwefToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSmoothOptions,
            this.смещенияToolStripMenuItem});
            this.efwefToolStripMenuItem.Name = "efwefToolStripMenuItem";
            this.efwefToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.efwefToolStripMenuItem.Text = "Настройки";
            // 
            // menuItemSmoothOptions
            // 
            this.menuItemSmoothOptions.Name = "menuItemSmoothOptions";
            this.menuItemSmoothOptions.Size = new System.Drawing.Size(152, 22);
            this.menuItemSmoothOptions.Text = "Сглаживание";
            this.menuItemSmoothOptions.Click += new System.EventHandler(this.menuItemSmoothOptions_Click);
            // 
            // смещенияToolStripMenuItem
            // 
            this.смещенияToolStripMenuItem.Name = "смещенияToolStripMenuItem";
            this.смещенияToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.смещенияToolStripMenuItem.Text = "Смещения";
            this.смещенияToolStripMenuItem.Click += new System.EventHandler(this.смещенияToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 375);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnLoadRecord);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Конвертер";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tpCamera1.ResumeLayout(false);
            this.tbCamera2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private View.FlowView flowViewer1;
        private Button btnRecord;
        private Button btnLoadRecord;
        private TabControl tabControl1;
        private TabPage tpCamera1;
        private TabPage tbCamera2;
        private View.FlowView flowViewer2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem efwefToolStripMenuItem;
        private ToolStripMenuItem menuItemSmoothOptions;
        private ToolStripMenuItem смещенияToolStripMenuItem;
    }
}

