namespace NSEAllocation
{
    partial class FrmMSEI
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHoldingShowResult = new System.Windows.Forms.Button();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.txtfileupload = new System.Windows.Forms.TextBox();
            this.lbltxt = new System.Windows.Forms.Label();
            this.btnuploadHold = new System.Windows.Forms.Button();
            this.pnlWait = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.pnlWait.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnHoldingShowResult);
            this.panel1.Controls.Add(this.btnsubmit);
            this.panel1.Controls.Add(this.txtfileupload);
            this.panel1.Controls.Add(this.lbltxt);
            this.panel1.Controls.Add(this.btnuploadHold);
            this.panel1.Location = new System.Drawing.Point(87, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1102, 100);
            this.panel1.TabIndex = 5;
            // 
            // btnHoldingShowResult
            // 
            this.btnHoldingShowResult.Location = new System.Drawing.Point(970, 39);
            this.btnHoldingShowResult.Name = "btnHoldingShowResult";
            this.btnHoldingShowResult.Size = new System.Drawing.Size(120, 23);
            this.btnHoldingShowResult.TabIndex = 9;
            this.btnHoldingShowResult.Text = "Show Result in CSV";
            this.btnHoldingShowResult.UseVisualStyleBackColor = true;
            // 
            // btnsubmit
            // 
            this.btnsubmit.Location = new System.Drawing.Point(870, 39);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(75, 23);
            this.btnsubmit.TabIndex = 7;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.UseVisualStyleBackColor = true;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // txtfileupload
            // 
            this.txtfileupload.Location = new System.Drawing.Point(418, 39);
            this.txtfileupload.Name = "txtfileupload";
            this.txtfileupload.Size = new System.Drawing.Size(339, 20);
            this.txtfileupload.TabIndex = 6;
            // 
            // lbltxt
            // 
            this.lbltxt.AutoSize = true;
            this.lbltxt.Location = new System.Drawing.Point(267, 39);
            this.lbltxt.Name = "lbltxt";
            this.lbltxt.Size = new System.Drawing.Size(134, 13);
            this.lbltxt.TabIndex = 5;
            this.lbltxt.Text = "Select File Trading Holding";
            // 
            // btnuploadHold
            // 
            this.btnuploadHold.Location = new System.Drawing.Point(763, 39);
            this.btnuploadHold.Name = "btnuploadHold";
            this.btnuploadHold.Size = new System.Drawing.Size(75, 23);
            this.btnuploadHold.TabIndex = 4;
            this.btnuploadHold.Text = "Upload";
            this.btnuploadHold.UseVisualStyleBackColor = true;
            this.btnuploadHold.Click += new System.EventHandler(this.btnuploadHold_Click);
            // 
            // pnlWait
            // 
            this.pnlWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWait.Controls.Add(this.lblTitle);
            this.pnlWait.Controls.Add(this.progressBar1);
            this.pnlWait.Location = new System.Drawing.Point(394, 392);
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
            // FrmMSEI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1253, 527);
            this.Controls.Add(this.pnlWait);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMSEI";
            this.Text = "FrmMSEI";
            this.Load += new System.EventHandler(this.FrmMSEI_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlWait.ResumeLayout(false);
            this.pnlWait.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnHoldingShowResult;
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.TextBox txtfileupload;
        private System.Windows.Forms.Label lbltxt;
        private System.Windows.Forms.Button btnuploadHold;
        private System.Windows.Forms.Panel pnlWait;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}