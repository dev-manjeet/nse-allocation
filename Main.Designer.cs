namespace NSEAllocation
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nSEFileUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.holdingFileUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankBalanceFileUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bSEFileUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mSEIAPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadHoldingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadBankAccBalanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadCashEquToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cC01ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCC01ForStoxkartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMG13ForStoxkartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCC01ForSMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportNMassMCXForSMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportNMassMCXForStoxkartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportClientAllocationUADToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nSEAllocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nSEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nSEFileUploadToolStripMenuItem,
            this.bSEFileUploadToolStripMenuItem,
            this.mSEIAPIToolStripMenuItem,
            this.cC01ToolStripMenuItem,
            this.nSEAllocationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nSEFileUploadToolStripMenuItem
            // 
            this.nSEFileUploadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.holdingFileUploadToolStripMenuItem,
            this.bankBalanceFileUploadToolStripMenuItem,
            this.cashToolStripMenuItem});
            this.nSEFileUploadToolStripMenuItem.Name = "nSEFileUploadToolStripMenuItem";
            this.nSEFileUploadToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.nSEFileUploadToolStripMenuItem.Text = "NSE File Upload";
            this.nSEFileUploadToolStripMenuItem.Click += new System.EventHandler(this.nSEFileUploadToolStripMenuItem_Click);
            // 
            // holdingFileUploadToolStripMenuItem
            // 
            this.holdingFileUploadToolStripMenuItem.Name = "holdingFileUploadToolStripMenuItem";
            this.holdingFileUploadToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.holdingFileUploadToolStripMenuItem.Text = "Holding File Upload";
            this.holdingFileUploadToolStripMenuItem.Click += new System.EventHandler(this.holdingFileUploadToolStripMenuItem_Click);
            // 
            // bankBalanceFileUploadToolStripMenuItem
            // 
            this.bankBalanceFileUploadToolStripMenuItem.Name = "bankBalanceFileUploadToolStripMenuItem";
            this.bankBalanceFileUploadToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.bankBalanceFileUploadToolStripMenuItem.Text = "Bank Balance File Upload";
            this.bankBalanceFileUploadToolStripMenuItem.Click += new System.EventHandler(this.bankBalanceFileUploadToolStripMenuItem_Click);
            // 
            // cashToolStripMenuItem
            // 
            this.cashToolStripMenuItem.Name = "cashToolStripMenuItem";
            this.cashToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.cashToolStripMenuItem.Text = "Cash Equivalent File Upload";
            this.cashToolStripMenuItem.Click += new System.EventHandler(this.cashToolStripMenuItem_Click);
            // 
            // bSEFileUploadToolStripMenuItem
            // 
            this.bSEFileUploadToolStripMenuItem.Name = "bSEFileUploadToolStripMenuItem";
            this.bSEFileUploadToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.bSEFileUploadToolStripMenuItem.Text = "BSE File Upload";
            this.bSEFileUploadToolStripMenuItem.Click += new System.EventHandler(this.bSEFileUploadToolStripMenuItem_Click);
            // 
            // mSEIAPIToolStripMenuItem
            // 
            this.mSEIAPIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadHoldingToolStripMenuItem,
            this.uploadBankAccBalanceToolStripMenuItem,
            this.uploadCashEquToolStripMenuItem});
            this.mSEIAPIToolStripMenuItem.Name = "mSEIAPIToolStripMenuItem";
            this.mSEIAPIToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.mSEIAPIToolStripMenuItem.Text = "MSEI API";
            this.mSEIAPIToolStripMenuItem.Click += new System.EventHandler(this.mSEIAPIToolStripMenuItem_Click);
            // 
            // uploadHoldingToolStripMenuItem
            // 
            this.uploadHoldingToolStripMenuItem.Name = "uploadHoldingToolStripMenuItem";
            this.uploadHoldingToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.uploadHoldingToolStripMenuItem.Text = "Upload Holding";
            this.uploadHoldingToolStripMenuItem.Click += new System.EventHandler(this.uploadHoldingToolStripMenuItem_Click);
            // 
            // uploadBankAccBalanceToolStripMenuItem
            // 
            this.uploadBankAccBalanceToolStripMenuItem.Name = "uploadBankAccBalanceToolStripMenuItem";
            this.uploadBankAccBalanceToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.uploadBankAccBalanceToolStripMenuItem.Text = "Upload Bank Acc Balance";
            this.uploadBankAccBalanceToolStripMenuItem.Click += new System.EventHandler(this.uploadBankAccBalanceToolStripMenuItem_Click);
            // 
            // uploadCashEquToolStripMenuItem
            // 
            this.uploadCashEquToolStripMenuItem.Name = "uploadCashEquToolStripMenuItem";
            this.uploadCashEquToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.uploadCashEquToolStripMenuItem.Text = "Upload Cash Equ";
            this.uploadCashEquToolStripMenuItem.Click += new System.EventHandler(this.uploadCashEquToolStripMenuItem_Click);
            // 
            // cC01ToolStripMenuItem
            // 
            this.cC01ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importCC01ForStoxkartToolStripMenuItem,
            this.importMG13ForStoxkartToolStripMenuItem,
            this.importCC01ForSMCToolStripMenuItem,
            this.exportNMassMCXForSMCToolStripMenuItem,
            this.exportNMassMCXForStoxkartToolStripMenuItem,
            this.exportClientAllocationUADToolStripMenuItem});
            this.cC01ToolStripMenuItem.Name = "cC01ToolStripMenuItem";
            this.cC01ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.cC01ToolStripMenuItem.Text = "NMASS";
            // 
            // importCC01ForStoxkartToolStripMenuItem
            // 
            this.importCC01ForStoxkartToolStripMenuItem.Name = "importCC01ForStoxkartToolStripMenuItem";
            this.importCC01ForStoxkartToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.importCC01ForStoxkartToolStripMenuItem.Text = "Import CC01 for Stoxkart";
            this.importCC01ForStoxkartToolStripMenuItem.Click += new System.EventHandler(this.importCC01ForStoxkartToolStripMenuItem_Click);
            // 
            // importMG13ForStoxkartToolStripMenuItem
            // 
            this.importMG13ForStoxkartToolStripMenuItem.Name = "importMG13ForStoxkartToolStripMenuItem";
            this.importMG13ForStoxkartToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.importMG13ForStoxkartToolStripMenuItem.Text = "Import MG13 for Stoxkart";
            this.importMG13ForStoxkartToolStripMenuItem.Click += new System.EventHandler(this.importMG13ForStoxkartToolStripMenuItem_Click);
            // 
            // importCC01ForSMCToolStripMenuItem
            // 
            this.importCC01ForSMCToolStripMenuItem.Name = "importCC01ForSMCToolStripMenuItem";
            this.importCC01ForSMCToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.importCC01ForSMCToolStripMenuItem.Text = "Import CC01 for SMC";
            this.importCC01ForSMCToolStripMenuItem.Click += new System.EventHandler(this.importCC01ForSMCToolStripMenuItem_Click);
            // 
            // exportNMassMCXForSMCToolStripMenuItem
            // 
            this.exportNMassMCXForSMCToolStripMenuItem.Name = "exportNMassMCXForSMCToolStripMenuItem";
            this.exportNMassMCXForSMCToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.exportNMassMCXForSMCToolStripMenuItem.Text = "Export NMass  for SMC";
            this.exportNMassMCXForSMCToolStripMenuItem.Click += new System.EventHandler(this.exportNMassMCXForSMCToolStripMenuItem_Click);
            // 
            // exportNMassMCXForStoxkartToolStripMenuItem
            // 
            this.exportNMassMCXForStoxkartToolStripMenuItem.Name = "exportNMassMCXForStoxkartToolStripMenuItem";
            this.exportNMassMCXForStoxkartToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.exportNMassMCXForStoxkartToolStripMenuItem.Text = "Import NMass  for Stoxkart";
            this.exportNMassMCXForStoxkartToolStripMenuItem.Click += new System.EventHandler(this.exportNMassMCXForStoxkartToolStripMenuItem_Click);
            // 
            // exportClientAllocationUADToolStripMenuItem
            // 
            this.exportClientAllocationUADToolStripMenuItem.Name = "exportClientAllocationUADToolStripMenuItem";
            this.exportClientAllocationUADToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.exportClientAllocationUADToolStripMenuItem.Text = "Export Client Allocation EOD";
            this.exportClientAllocationUADToolStripMenuItem.Click += new System.EventHandler(this.exportClientAllocationUADToolStripMenuItem_Click);
            // 
            // nSEAllocationToolStripMenuItem
            // 
            this.nSEAllocationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nSEToolStripMenuItem});
            this.nSEAllocationToolStripMenuItem.Name = "nSEAllocationToolStripMenuItem";
            this.nSEAllocationToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.nSEAllocationToolStripMenuItem.Text = "NSE Allocation";
            this.nSEAllocationToolStripMenuItem.Click += new System.EventHandler(this.nSEAllocationToolStripMenuItem_Click);
            // 
            // nSEToolStripMenuItem
            // 
            this.nSEToolStripMenuItem.Name = "nSEToolStripMenuItem";
            this.nSEToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.nSEToolStripMenuItem.Text = "NSE ";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nSEFileUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bSEFileUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mSEIAPIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadHoldingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadBankAccBalanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadCashEquToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem holdingFileUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bankBalanceFileUploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cC01ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importCC01ForStoxkartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMG13ForStoxkartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importCC01ForSMCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportNMassMCXForSMCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportNMassMCXForStoxkartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nSEAllocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nSEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportClientAllocationUADToolStripMenuItem;
    }
}