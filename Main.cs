
//using NSEAllocation;
using NSEAllocation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSEAllocation
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void nSEFileUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmNse alloc = new FrmNse();
            //alloc.MdiParent = this;
            //alloc.Show();
        }

        private void bSEFileUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BSEFileUpoad objBSE = new BSEFileUpoad();
            objBSE.MdiParent = this;
            objBSE.Show();
        }

        private void mSEIAPIToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void uploadHoldingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMSEI objBSE = new FrmMSEI();
            objBSE.MdiParent = this;
            objBSE.Show();
        }

        private void uploadBankAccBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMSEIBankAccBalance objBSE = new FrmMSEIBankAccBalance();
            objBSE.MdiParent = this;
            objBSE.Show();
        }

        private void uploadCashEquToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMSEICashEqv objBSE = new frmMSEICashEqv();
            objBSE.MdiParent = this;
            objBSE.Show();
        }

        private void holdingFileUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNSEHolding frmNSEHOLDING=new FormNSEHolding();
            frmNSEHOLDING.MdiParent=this;
            frmNSEHOLDING.Show();
        }

        private void importCC01ForStoxkartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSTXImportCC01 frmc001 = new frmSTXImportCC01();
            frmc001.MdiParent = this;
            frmc001.Show();
        }

        private void importMG13ForStoxkartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_MG_13 form = new Form_MG_13();
            form.MdiParent = this;
            form.Show();
        }

        private void importCC01ForSMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSMC_CC01 form = new FrmSMC_CC01();
            form.MdiParent = this;
            form.Show();
        }

        private void exportNMassMCXForStoxkartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormStxNMASS_Export form = new FormStxNMASS_Export();
            form.MdiParent = this;
            form.Show();
        }

        private void exportNMassMCXForSMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSMC_Nmass_MCX form = new FrmSMC_Nmass_MCX();
            form.MdiParent = this;
            form.Show();
        }

        private void nSEAllocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAllocation form = new FrmAllocation();
            form.MdiParent = this;
            form.Show();
        }

        private void exportClientAllocationUADToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormSTX_ClientAllocationUAD form = new FormSTX_ClientAllocationUAD();
            form.MdiParent = this;
            form.Show();
        }

        private void bankBalanceFileUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNSE_Bank_Balance form= new FormNSE_Bank_Balance();
            form.MdiParent = this;
            form.Show();
        }

        private void cashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_NSE_Cash_Equivalent FrmNseCash= new Frm_NSE_Cash_Equivalent();
            FrmNseCash.MdiParent = this;
            FrmNseCash.Show();
        }
    }
}
