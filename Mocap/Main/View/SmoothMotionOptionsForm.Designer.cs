namespace Main
{
    partial class SmoothMotionOptionsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSmoothHard = new System.Windows.Forms.Button();
            this.btnSmothLight = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txbCorrection = new System.Windows.Forms.MaskedTextBox();
            this.txbJitRadius = new System.Windows.Forms.MaskedTextBox();
            this.txbMaxRadius = new System.Windows.Forms.MaskedTextBox();
            this.txbPrediction = new System.Windows.Forms.MaskedTextBox();
            this.txbSmooth = new System.Windows.Forms.MaskedTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Корректировка:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Радиус дрожания:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Радиус макс. отклонения:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Глубина интерполяции:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Величина сглаживания:";
            // 
            // btnSmoothHard
            // 
            this.btnSmoothHard.Location = new System.Drawing.Point(6, 19);
            this.btnSmoothHard.Name = "btnSmoothHard";
            this.btnSmoothHard.Size = new System.Drawing.Size(75, 23);
            this.btnSmoothHard.TabIndex = 10;
            this.btnSmoothHard.Text = "Грубо";
            this.btnSmoothHard.UseVisualStyleBackColor = true;
            this.btnSmoothHard.Click += new System.EventHandler(this.btnSmoothHard_Click);
            // 
            // btnSmothLight
            // 
            this.btnSmothLight.Location = new System.Drawing.Point(131, 19);
            this.btnSmothLight.Name = "btnSmothLight";
            this.btnSmothLight.Size = new System.Drawing.Size(75, 23);
            this.btnSmothLight.TabIndex = 11;
            this.btnSmothLight.Text = "Плавно";
            this.btnSmothLight.UseVisualStyleBackColor = true;
            this.btnSmothLight.Click += new System.EventHandler(this.btnSmothLight_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSmoothHard);
            this.groupBox1.Controls.Add(this.btnSmothLight);
            this.groupBox1.Location = new System.Drawing.Point(15, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 52);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Задать стандартные параметры";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txbSmooth);
            this.groupBox2.Controls.Add(this.txbPrediction);
            this.groupBox2.Controls.Add(this.txbMaxRadius);
            this.groupBox2.Controls.Add(this.txbJitRadius);
            this.groupBox2.Controls.Add(this.txbCorrection);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 160);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры сглаживания";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(449, 246);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(530, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txbCorrection
            // 
            this.txbCorrection.Location = new System.Drawing.Point(189, 25);
            this.txbCorrection.Mask = "00";
            this.txbCorrection.Name = "txbCorrection";
            this.txbCorrection.Size = new System.Drawing.Size(100, 20);
            this.txbCorrection.TabIndex = 10;
            this.txbCorrection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbCorrection.MouseEnter += new System.EventHandler(this.txbCorrection_MouseEnter);
            this.txbCorrection.MouseLeave += new System.EventHandler(this.txbCorrection_MouseLeave);
            // 
            // txbJitRadius
            // 
            this.txbJitRadius.Location = new System.Drawing.Point(189, 51);
            this.txbJitRadius.Mask = "00";
            this.txbJitRadius.Name = "txbJitRadius";
            this.txbJitRadius.Size = new System.Drawing.Size(100, 20);
            this.txbJitRadius.TabIndex = 11;
            this.txbJitRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbJitRadius.MouseEnter += new System.EventHandler(this.txbJitRadius_MouseEnter);
            this.txbJitRadius.MouseLeave += new System.EventHandler(this.txbJitRadius_MouseLeave);
            // 
            // txbMaxRadius
            // 
            this.txbMaxRadius.Location = new System.Drawing.Point(189, 77);
            this.txbMaxRadius.Mask = "00";
            this.txbMaxRadius.Name = "txbMaxRadius";
            this.txbMaxRadius.Size = new System.Drawing.Size(100, 20);
            this.txbMaxRadius.TabIndex = 12;
            this.txbMaxRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbMaxRadius.MouseEnter += new System.EventHandler(this.txbMaxRadius_MouseEnter);
            this.txbMaxRadius.MouseLeave += new System.EventHandler(this.txbMaxRadius_MouseLeave);
            // 
            // txbPrediction
            // 
            this.txbPrediction.Location = new System.Drawing.Point(189, 103);
            this.txbPrediction.Mask = "00";
            this.txbPrediction.Name = "txbPrediction";
            this.txbPrediction.Size = new System.Drawing.Size(100, 20);
            this.txbPrediction.TabIndex = 13;
            this.txbPrediction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbPrediction.MouseEnter += new System.EventHandler(this.txbPrediction_MouseEnter);
            this.txbPrediction.MouseLeave += new System.EventHandler(this.txbPrediction_MouseLeave);
            // 
            // txbSmooth
            // 
            this.txbSmooth.Location = new System.Drawing.Point(189, 129);
            this.txbSmooth.Mask = "00";
            this.txbSmooth.Name = "txbSmooth";
            this.txbSmooth.Size = new System.Drawing.Size(100, 20);
            this.txbSmooth.TabIndex = 14;
            this.txbSmooth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txbSmooth.MouseEnter += new System.EventHandler(this.txbSmooth_MouseEnter);
            this.txbSmooth.MouseLeave += new System.EventHandler(this.txbSmooth_MouseLeave);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblDescription);
            this.groupBox3.Location = new System.Drawing.Point(334, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(264, 213);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Пояснения:";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(6, 23);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(252, 180);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Наведите курсор на поле ввода для получения справки";
            // 
            // SmoothMotionOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 281);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SmoothMotionOptionsForm";
            this.ShowInTaskbar = false;
            this.Text = "Параметры сглаживания";
            this.Load += new System.EventHandler(this.SmoothMotionOptionsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSmoothHard;
        private System.Windows.Forms.Button btnSmothLight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MaskedTextBox txbSmooth;
        private System.Windows.Forms.MaskedTextBox txbPrediction;
        private System.Windows.Forms.MaskedTextBox txbMaxRadius;
        private System.Windows.Forms.MaskedTextBox txbJitRadius;
        private System.Windows.Forms.MaskedTextBox txbCorrection;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblDescription;
    }
}