namespace NSEAllocation
{
    partial class Frm_NSE_Cash_Equivalent
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.BtnExport = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.grdViewFile = new System.Windows.Forms.DataGridView();
            this.btnConvertDateWise = new System.Windows.Forms.Button();
            this.txtfileupload = new System.Windows.Forms.TextBox();
            this.lbltxt = new System.Windows.Forms.Label();
            this.cmbFileSeq = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnupload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dateLastweek = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlWait = new System.Windows.Forms.Panel();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlWait.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 68);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(437, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(676, 6);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(145, 23);
            this.BtnExport.TabIndex = 7;
            this.BtnExport.Text = "Download CSV";
            this.BtnExport.UseVisualStyleBackColor = true;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(570, 5);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 4;
            this.btnShow.Text = "Show Record";
            this.btnShow.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(763, 48);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // grdViewFile
            // 
            this.grdViewFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdViewFile.Location = new System.Drawing.Point(236, 36);
            this.grdViewFile.Name = "grdViewFile";
            this.grdViewFile.Size = new System.Drawing.Size(503, 164);
            this.grdViewFile.TabIndex = 10;
            // 
            // btnConvertDateWise
            // 
            this.btnConvertDateWise.Location = new System.Drawing.Point(870, 7);
            this.btnConvertDateWise.Name = "btnConvertDateWise";
            this.btnConvertDateWise.Size = new System.Drawing.Size(145, 23);
            this.btnConvertDateWise.TabIndex = 7;
            this.btnConvertDateWise.Text = "Convert into Date wise";
            this.btnConvertDateWise.UseVisualStyleBackColor = true;
            this.btnConvertDateWise.Click += new System.EventHandler(this.btnConvertDateWise_Click);
            // 
            // txtfileupload
            // 
            this.txtfileupload.Location = new System.Drawing.Point(236, 10);
            this.txtfileupload.Name = "txtfileupload";
            this.txtfileupload.Size = new System.Drawing.Size(503, 20);
            this.txtfileupload.TabIndex = 6;
            // 
            // lbltxt
            // 
            this.lbltxt.AutoSize = true;
            this.lbltxt.Location = new System.Drawing.Point(163, 13);
            this.lbltxt.Name = "lbltxt";
            this.lbltxt.Size = new System.Drawing.Size(56, 13);
            this.lbltxt.TabIndex = 5;
            this.lbltxt.Text = "Select File";
            // 
            // cmbFileSeq
            // 
            this.cmbFileSeq.AutoCompleteCustomSource.AddRange(new string[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06"});
            this.cmbFileSeq.FormattingEnabled = true;
            this.cmbFileSeq.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06"});
            this.cmbFileSeq.Location = new System.Drawing.Point(398, 5);
            this.cmbFileSeq.Name = "cmbFileSeq";
            this.cmbFileSeq.Size = new System.Drawing.Size(112, 21);
            this.cmbFileSeq.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select Last Week Date";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(27, 37);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1098, 284);
            this.dataGridView2.TabIndex = 10;
            // 
            // btnupload
            // 
            this.btnupload.Location = new System.Drawing.Point(763, 7);
            this.btnupload.Name = "btnupload";
            this.btnupload.Size = new System.Drawing.Size(75, 23);
            this.btnupload.TabIndex = 4;
            this.btnupload.Text = "Upload";
            this.btnupload.UseVisualStyleBackColor = true;
            this.btnupload.Click += new System.EventHandler(this.btnupload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(314, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "File Sequence ";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(120, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(222, 25);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Please Wait............";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dateLastweek);
            this.panel2.Controls.Add(this.cmbFileSeq);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Controls.Add(this.BtnExport);
            this.panel2.Controls.Add(this.btnShow);
            this.panel2.Location = new System.Drawing.Point(50, 270);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1147, 331);
            this.panel2.TabIndex = 10;
            // 
            // dateLastweek
            // 
            this.dateLastweek.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateLastweek.Location = new System.Drawing.Point(183, 8);
            this.dateLastweek.Name = "dateLastweek";
            this.dateLastweek.Size = new System.Drawing.Size(88, 20);
            this.dateLastweek.TabIndex = 13;
            this.dateLastweek.Value = new System.DateTime(2022, 7, 14, 0, 0, 0, 0);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.grdViewFile);
            this.panel1.Controls.Add(this.btnConvertDateWise);
            this.panel1.Controls.Add(this.txtfileupload);
            this.panel1.Controls.Add(this.lbltxt);
            this.panel1.Controls.Add(this.btnupload);
            this.panel1.Location = new System.Drawing.Point(52, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1147, 212);
            this.panel1.TabIndex = 9;
            // 
            // pnlWait
            // 
            this.pnlWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWait.Controls.Add(this.lblTitle);
            this.pnlWait.Controls.Add(this.progressBar1);
            this.pnlWait.Location = new System.Drawing.Point(328, 311);
            this.pnlWait.Margin = new System.Windows.Forms.Padding(50);
            this.pnlWait.Name = "pnlWait";
            this.pnlWait.Size = new System.Drawing.Size(464, 121);
            this.pnlWait.TabIndex = 11;
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(55, 8);
            this.txtToken.Multiline = true;
            this.txtToken.Name = "txtToken";
            this.txtToken.ReadOnly = true;
            this.txtToken.Size = new System.Drawing.Size(1142, 38);
            this.txtToken.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Token";
            // 
            // Frm_NSE_Cash_Equivalent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 606);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlWait);
            this.Name = "Frm_NSE_Cash_Equivalent";
            this.Text = "Frm_NSE_Cash_Equivalent";
            this.Load += new System.EventHandler(this.Frm_NSE_Cash_Equivalent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdViewFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlWait.ResumeLayout(false);
            this.pnlWait.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.DataGridView grdViewFile;
        private System.Windows.Forms.Button btnConvertDateWise;
        private System.Windows.Forms.TextBox txtfileupload;
        private System.Windows.Forms.Label lbltxt;
        private System.Windows.Forms.ComboBox cmbFileSeq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnupload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dateLastweek;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlWait;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label3;
    }
}