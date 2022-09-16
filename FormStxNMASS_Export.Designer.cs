namespace NSEAllocation
{
    partial class FormStxNMASS_Export
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
            this.cmbSegment = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMerge = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbSegment
            // 
            this.cmbSegment.FormattingEnabled = true;
            this.cmbSegment.Location = new System.Drawing.Point(418, 26);
            this.cmbSegment.Name = "cmbSegment";
            this.cmbSegment.Size = new System.Drawing.Size(121, 21);
            this.cmbSegment.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Select Segment";
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(574, 24);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(117, 23);
            this.btnMerge.TabIndex = 23;
            this.btnMerge.Text = "Import from RMS";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // FormStxNMASS_Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 450);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.cmbSegment);
            this.Controls.Add(this.label1);
            this.Name = "FormStxNMASS_Export";
            this.Text = "FormStxNMASS_Export";
            this.Load += new System.EventHandler(this.FormStxNMASS_Export_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSegment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMerge;
    }
}