namespace NSEAllocation
{
    partial class frmSTXImportCC01
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtfile = new System.Windows.Forms.TextBox();
            this.lblCashEq = new System.Windows.Forms.Label();
            this.btnupload = new System.Windows.Forms.Button();
            this.cmbSegment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(901, 23);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnCashEqulaintSubmit_Click);
            // 
            // txtfile
            // 
            this.txtfile.Location = new System.Drawing.Point(444, 23);
            this.txtfile.Name = "txtfile";
            this.txtfile.Size = new System.Drawing.Size(339, 20);
            this.txtfile.TabIndex = 10;
            // 
            // lblCashEq
            // 
            this.lblCashEq.AutoSize = true;
            this.lblCashEq.Location = new System.Drawing.Point(364, 26);
            this.lblCashEq.Name = "lblCashEq";
            this.lblCashEq.Size = new System.Drawing.Size(56, 13);
            this.lblCashEq.TabIndex = 9;
            this.lblCashEq.Text = "Select File";
            // 
            // btnupload
            // 
            this.btnupload.Location = new System.Drawing.Point(794, 23);
            this.btnupload.Name = "btnupload";
            this.btnupload.Size = new System.Drawing.Size(75, 23);
            this.btnupload.TabIndex = 8;
            this.btnupload.Text = "Upload";
            this.btnupload.UseVisualStyleBackColor = true;
            this.btnupload.Click += new System.EventHandler(this.btnupload_Click);
            // 
            // cmbSegment
            // 
            this.cmbSegment.FormattingEnabled = true;
            this.cmbSegment.Location = new System.Drawing.Point(176, 20);
            this.cmbSegment.Name = "cmbSegment";
            this.cmbSegment.Size = new System.Drawing.Size(121, 21);
            this.cmbSegment.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Select Segment";
            // 
            // frmSTXImportCC01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 504);
            this.Controls.Add(this.cmbSegment);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtfile);
            this.Controls.Add(this.lblCashEq);
            this.Controls.Add(this.btnupload);
            this.Name = "frmSTXImportCC01";
            this.Text = "Import CC01 (Stoxkart)";
            this.Load += new System.EventHandler(this.frmSTXImportCC01_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtfile;
        private System.Windows.Forms.Label lblCashEq;
        private System.Windows.Forms.Button btnupload;
        private System.Windows.Forms.ComboBox cmbSegment;
        private System.Windows.Forms.Label label1;
    }
}