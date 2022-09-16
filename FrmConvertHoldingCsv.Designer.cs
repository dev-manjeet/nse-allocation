namespace NSEAllocation
{
    partial class FrmConvertHoldingCsv
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.weekdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lastweekdate = new System.Windows.Forms.DateTimePicker();
            this.cmbSeq = new System.Windows.Forms.ComboBox();
            this.btnConvertCSV = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.weekdate);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lastweekdate);
            this.panel2.Controls.Add(this.cmbSeq);
            this.panel2.Controls.Add(this.btnConvertCSV);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(57, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1124, 100);
            this.panel2.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(837, 28);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(108, 22);
            this.dateTimePicker1.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(729, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "File Submit Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(520, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "File Sequence No.";
            // 
            // weekdate
            // 
            this.weekdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weekdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.weekdate.Location = new System.Drawing.Point(358, 32);
            this.weekdate.Margin = new System.Windows.Forms.Padding(4);
            this.weekdate.Name = "weekdate";
            this.weekdate.Size = new System.Drawing.Size(108, 22);
            this.weekdate.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(288, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "week date";
            // 
            // lastweekdate
            // 
            this.lastweekdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastweekdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.lastweekdate.Location = new System.Drawing.Point(117, 32);
            this.lastweekdate.Margin = new System.Windows.Forms.Padding(4);
            this.lastweekdate.Name = "lastweekdate";
            this.lastweekdate.Size = new System.Drawing.Size(108, 22);
            this.lastweekdate.TabIndex = 12;
            // 
            // cmbSeq
            // 
            this.cmbSeq.FormattingEnabled = true;
            this.cmbSeq.Location = new System.Drawing.Point(638, 29);
            this.cmbSeq.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSeq.Name = "cmbSeq";
            this.cmbSeq.Size = new System.Drawing.Size(70, 21);
            this.cmbSeq.TabIndex = 11;
            // 
            // btnConvertCSV
            // 
            this.btnConvertCSV.Location = new System.Drawing.Point(963, 29);
            this.btnConvertCSV.Name = "btnConvertCSV";
            this.btnConvertCSV.Size = new System.Drawing.Size(120, 23);
            this.btnConvertCSV.TabIndex = 9;
            this.btnConvertCSV.Text = "Convert Into CSV File";
            this.btnConvertCSV.UseVisualStyleBackColor = true;
            this.btnConvertCSV.Click += new System.EventHandler(this.btnConvertCSV_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Last week date";
            // 
            // FrmConvertHoldingCsv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 450);
            this.Controls.Add(this.panel2);
            this.Name = "FrmConvertHoldingCsv";
            this.Text = "Convert Holding File Csv";
            this.Load += new System.EventHandler(this.FrmConvertHoldingCsv_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnConvertCSV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker lastweekdate;
        private System.Windows.Forms.ComboBox cmbSeq;
        private System.Windows.Forms.DateTimePicker weekdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
    }
}