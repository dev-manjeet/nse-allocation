namespace NSEAllocation
{
    partial class frmMSEICashEqv
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
            this.pnlWait = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCashEqulantShowResult = new System.Windows.Forms.Button();
            this.btnCashEqulaintSubmit = new System.Windows.Forms.Button();
            this.txtCashEqulaint = new System.Windows.Forms.TextBox();
            this.lblCashEq = new System.Windows.Forms.Label();
            this.btnuploadCashEqulaint = new System.Windows.Forms.Button();
            this.pnlWait.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWait
            // 
            this.pnlWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWait.Controls.Add(this.lblTitle);
            this.pnlWait.Controls.Add(this.progressBar1);
            this.pnlWait.Location = new System.Drawing.Point(375, 165);
            this.pnlWait.Margin = new System.Windows.Forms.Padding(50);
            this.pnlWait.Name = "pnlWait";
            this.pnlWait.Size = new System.Drawing.Size(466, 121);
            this.pnlWait.TabIndex = 9;
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.btnCashEqulantShowResult);
            this.panel3.Controls.Add(this.btnCashEqulaintSubmit);
            this.panel3.Controls.Add(this.txtCashEqulaint);
            this.panel3.Controls.Add(this.lblCashEq);
            this.panel3.Controls.Add(this.btnuploadCashEqulaint);
            this.panel3.Location = new System.Drawing.Point(12, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1222, 100);
            this.panel3.TabIndex = 8;
            // 
            // btnCashEqulantShowResult
            // 
            this.btnCashEqulantShowResult.Location = new System.Drawing.Point(969, 39);
            this.btnCashEqulantShowResult.Name = "btnCashEqulantShowResult";
            this.btnCashEqulantShowResult.Size = new System.Drawing.Size(119, 23);
            this.btnCashEqulantShowResult.TabIndex = 10;
            this.btnCashEqulantShowResult.Text = "Show Result in CSV";
            this.btnCashEqulantShowResult.UseVisualStyleBackColor = true;
            // 
            // btnCashEqulaintSubmit
            // 
            this.btnCashEqulaintSubmit.Location = new System.Drawing.Point(870, 39);
            this.btnCashEqulaintSubmit.Name = "btnCashEqulaintSubmit";
            this.btnCashEqulaintSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnCashEqulaintSubmit.TabIndex = 7;
            this.btnCashEqulaintSubmit.Text = "Submit";
            this.btnCashEqulaintSubmit.UseVisualStyleBackColor = true;
            this.btnCashEqulaintSubmit.Click += new System.EventHandler(this.btnCashEqulaintSubmit_Click);
            // 
            // txtCashEqulaint
            // 
            this.txtCashEqulaint.Location = new System.Drawing.Point(413, 39);
            this.txtCashEqulaint.Name = "txtCashEqulaint";
            this.txtCashEqulaint.Size = new System.Drawing.Size(339, 20);
            this.txtCashEqulaint.TabIndex = 6;
            // 
            // lblCashEq
            // 
            this.lblCashEq.AutoSize = true;
            this.lblCashEq.Location = new System.Drawing.Point(211, 39);
            this.lblCashEq.Name = "lblCashEq";
            this.lblCashEq.Size = new System.Drawing.Size(184, 13);
            this.lblCashEq.TabIndex = 5;
            this.lblCashEq.Text = "Select File Cash and Cash Equivalent";
            // 
            // btnuploadCashEqulaint
            // 
            this.btnuploadCashEqulaint.Location = new System.Drawing.Point(763, 39);
            this.btnuploadCashEqulaint.Name = "btnuploadCashEqulaint";
            this.btnuploadCashEqulaint.Size = new System.Drawing.Size(75, 23);
            this.btnuploadCashEqulaint.TabIndex = 4;
            this.btnuploadCashEqulaint.Text = "Upload";
            this.btnuploadCashEqulaint.UseVisualStyleBackColor = true;
            this.btnuploadCashEqulaint.Click += new System.EventHandler(this.btnuploadCashEqulaint_Click);
            // 
            // frmMSEICashEqv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 450);
            this.Controls.Add(this.pnlWait);
            this.Controls.Add(this.panel3);
            this.Name = "frmMSEICashEqv";
            this.Text = "frmMSEICashEqv";
            this.Load += new System.EventHandler(this.frmMSEICashEqv_Load);
            this.pnlWait.ResumeLayout(false);
            this.pnlWait.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWait;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCashEqulantShowResult;
        private System.Windows.Forms.Button btnCashEqulaintSubmit;
        private System.Windows.Forms.TextBox txtCashEqulaint;
        private System.Windows.Forms.Label lblCashEq;
        private System.Windows.Forms.Button btnuploadCashEqulaint;
    }
}