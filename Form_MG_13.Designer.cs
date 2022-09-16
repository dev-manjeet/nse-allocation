namespace NSEAllocation
{
    partial class Form_MG_13
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtfile = new System.Windows.Forms.TextBox();
            this.lblCashEq = new System.Windows.Forms.Label();
            this.btnupload = new System.Windows.Forms.Button();
            this.cmbSegment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMerge = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnMerge);
            this.panel1.Controls.Add(this.btnSubmit);
            this.panel1.Controls.Add(this.txtfile);
            this.panel1.Controls.Add(this.lblCashEq);
            this.panel1.Controls.Add(this.btnupload);
            this.panel1.Controls.Add(this.cmbSegment);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(75, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1085, 133);
            this.panel1.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(845, 28);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 15;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtfile
            // 
            this.txtfile.Location = new System.Drawing.Point(388, 28);
            this.txtfile.Name = "txtfile";
            this.txtfile.Size = new System.Drawing.Size(339, 20);
            this.txtfile.TabIndex = 14;
            // 
            // lblCashEq
            // 
            this.lblCashEq.AutoSize = true;
            this.lblCashEq.Location = new System.Drawing.Point(326, 31);
            this.lblCashEq.Name = "lblCashEq";
            this.lblCashEq.Size = new System.Drawing.Size(56, 13);
            this.lblCashEq.TabIndex = 13;
            this.lblCashEq.Text = "Select File";
            // 
            // btnupload
            // 
            this.btnupload.Location = new System.Drawing.Point(745, 26);
            this.btnupload.Name = "btnupload";
            this.btnupload.Size = new System.Drawing.Size(75, 23);
            this.btnupload.TabIndex = 12;
            this.btnupload.Text = "Upload";
            this.btnupload.UseVisualStyleBackColor = true;
            this.btnupload.Click += new System.EventHandler(this.btnupload_Click);
            // 
            // cmbSegment
            // 
            this.cmbSegment.FormattingEnabled = true;
            this.cmbSegment.Location = new System.Drawing.Point(143, 28);
            this.cmbSegment.Name = "cmbSegment";
            this.cmbSegment.Size = new System.Drawing.Size(121, 21);
            this.cmbSegment.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Segment";
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(745, 68);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 23);
            this.btnMerge.TabIndex = 16;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // Form_MG_13
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 450);
            this.Controls.Add(this.panel1);
            this.Name = "Form_MG_13";
            this.Text = "Form_MG_13";
            this.Load += new System.EventHandler(this.Form_MG_13_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbSegment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtfile;
        private System.Windows.Forms.Label lblCashEq;
        private System.Windows.Forms.Button btnupload;
        private System.Windows.Forms.Button btnMerge;
    }
}