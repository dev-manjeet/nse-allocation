namespace NSEAllocation
{
    partial class FrmMSEIBankAccBalance
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
            this.btnBankBalanceShowResult = new System.Windows.Forms.Button();
            this.btnBankAccBalSubmit = new System.Windows.Forms.Button();
            this.txtBankBal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBankBalUpld = new System.Windows.Forms.Button();
            this.pnlWait = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel2.SuspendLayout();
            this.pnlWait.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.btnBankBalanceShowResult);
            this.panel2.Controls.Add(this.btnBankAccBalSubmit);
            this.panel2.Controls.Add(this.txtBankBal);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnBankBalUpld);
            this.panel2.Location = new System.Drawing.Point(49, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1113, 100);
            this.panel2.TabIndex = 6;
            // 
            // btnBankBalanceShowResult
            // 
            this.btnBankBalanceShowResult.Location = new System.Drawing.Point(969, 37);
            this.btnBankBalanceShowResult.Name = "btnBankBalanceShowResult";
            this.btnBankBalanceShowResult.Size = new System.Drawing.Size(120, 23);
            this.btnBankBalanceShowResult.TabIndex = 10;
            this.btnBankBalanceShowResult.Text = "Show Result in CSV";
            this.btnBankBalanceShowResult.UseVisualStyleBackColor = true;
            // 
            // btnBankAccBalSubmit
            // 
            this.btnBankAccBalSubmit.Location = new System.Drawing.Point(870, 39);
            this.btnBankAccBalSubmit.Name = "btnBankAccBalSubmit";
            this.btnBankAccBalSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnBankAccBalSubmit.TabIndex = 7;
            this.btnBankAccBalSubmit.Text = "Submit";
            this.btnBankAccBalSubmit.UseVisualStyleBackColor = true;
            this.btnBankAccBalSubmit.Click += new System.EventHandler(this.btnBankAccBalSubmit_Click);
            // 
            // txtBankBal
            // 
            this.txtBankBal.Location = new System.Drawing.Point(413, 39);
            this.txtBankBal.Name = "txtBankBal";
            this.txtBankBal.Size = new System.Drawing.Size(339, 20);
            this.txtBankBal.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select File Bank Account Balance";
            // 
            // btnBankBalUpld
            // 
            this.btnBankBalUpld.Location = new System.Drawing.Point(763, 39);
            this.btnBankBalUpld.Name = "btnBankBalUpld";
            this.btnBankBalUpld.Size = new System.Drawing.Size(75, 23);
            this.btnBankBalUpld.TabIndex = 4;
            this.btnBankBalUpld.Text = "Upload";
            this.btnBankBalUpld.UseVisualStyleBackColor = true;
            this.btnBankBalUpld.Click += new System.EventHandler(this.btnBankBalUpld_Click);
            // 
            // pnlWait
            // 
            this.pnlWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWait.Controls.Add(this.lblTitle);
            this.pnlWait.Controls.Add(this.progressBar1);
            this.pnlWait.Location = new System.Drawing.Point(374, 197);
            this.pnlWait.Margin = new System.Windows.Forms.Padding(50);
            this.pnlWait.Name = "pnlWait";
            this.pnlWait.Size = new System.Drawing.Size(464, 121);
            this.pnlWait.TabIndex = 8;
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
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 68);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(437, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // FrmMSEIBankAccBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 450);
            this.Controls.Add(this.pnlWait);
            this.Controls.Add(this.panel2);
            this.Name = "FrmMSEIBankAccBalance";
            this.Text = "FrmMSEIBankAccBalance";
            this.Load += new System.EventHandler(this.FrmMSEIBankAccBalance_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlWait.ResumeLayout(false);
            this.pnlWait.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBankBalanceShowResult;
        private System.Windows.Forms.Button btnBankAccBalSubmit;
        private System.Windows.Forms.TextBox txtBankBal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBankBalUpld;
        private System.Windows.Forms.Panel pnlWait;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}