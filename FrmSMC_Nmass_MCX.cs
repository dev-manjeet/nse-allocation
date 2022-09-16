using NSEAllocation.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSEAllocation
{
    public partial class FrmSMC_Nmass_MCX : Form
    {
        public FrmSMC_Nmass_MCX()
        {
            InitializeComponent();
        }

        private void FrmSMC_Nmass_MCX_Load(object sender, EventArgs e)
        {
            cmbSegment.Items.Add("CM");
            cmbSegment.Items.Add("FO");
            cmbSegment.Items.Add("CD");
            cmbSegment.Items.Add("CO");
            //cmbSegment.Items.Add("DT");
            //cmbSegment.Items.Add("SLB");
            //cmbSegment.Items.Add("TPR");
            //cmbSegment.Items.Add("OFS");
            cmbSegment.SelectedIndex = -1;
            label2.Visible = false;
            cmbType.Visible = false;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (cmbSegment.Text == "")
            {
                MessageBox.Show("Please Select Segment");
                return;
            }
                if (cmbSegment.Text == "CM")
            {
                string Type = "";
                if(cmbType.Text=="")
                {
                    MessageBox.Show("Please Select File Type");
                    return;
                }
                if(cmbType.Text== "Summary")
                {
                    Type = "S";
                }
                else
                {
                    Type = "D";
                }
                DataTable DT = new DataTable();
                string QUERY = "smcshield.CashMinMargin";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.CommandText = QUERY;
                    cmd.Parameters.Add("reptype", OracleDbType.Char).Value  =Type;//S/D
                    cmd.Parameters.Add("rep", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter adp = new OracleDataAdapter(cmd);
                    DT = new DataTable();
                    adp.Fill(DT);

                }
                if (DT.Rows.Count > 0)
                {
                    //    DT.Columns[0].ColumnName = "CM CODE";
                    //    DT.Columns[1].ColumnName = "MEMBER NAME";
                    //    DT.Columns[2].ColumnName = "TM / CP CODE";
                    //    DT.Columns[3].ColumnName = "TM / CP NAME";
                    //    DT.Columns[4].ColumnName = "CLIENT CODE";
                    //    DT.Columns[5].ColumnName = "CASH(A)";
                    //    DT.Columns[6].ColumnName = "NON CASH(B)";
                    //    DT.Columns[7].ColumnName = "MTM LOSS(C)";
                    //    DT.Columns[8].ColumnName = "TRADE MARGINS(D)";
                    //    DT.Columns[9].ColumnName = "ICMTM(E)";
                    //    DT.Columns[10].ColumnName = "TOTAL MARGINS(F= C + D + E)";
                    //    DT.Columns[11].ColumnName = "Client Margins > 90 %";
                    //    DT.Columns[12].ColumnName = "90 % of Client Excess Non - cash";
                    //    DT.Columns[13].ColumnName = "Eligible Non-cash(Subject to the extent of TM Excess Cash / CM Excess Cash)";

                    //    DT.AcceptChanges();
                    string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Export_NMass";
                    if (!System.IO.Directory.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                    }
                    if (System.IO.File.Exists(FilePath + "\\SMC_NMASS_ClientMargin_CM_"+cmbType.Text+"_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"))
                    {
                        System.IO.File.Delete(FilePath + "\\SMC_NMASS_ClientMargin_CM_" + cmbType.Text + "_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    }
                    Helper.ToCSV(DT, FilePath + "\\SMC_NMASS_ClientMargin_CM_" + cmbType.Text + "_"  + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    MessageBox.Show("File Export Successfuly");
                }
                else
                {
                    MessageBox.Show("No Record Found");
                }

            }
            if (cmbSegment.Text == "FO")
            {

                DataTable DT = new DataTable();
                string QUERY = "select 'M50504','SMC','07714','SMC',MST.TRDIDF,0,0,0,0,0,FOSPAN+FOEXP Margin,0,0,0,0 from rmsposi POSI,CLNTMST MST" +
                               " where POSI.PARTY=MST.PARTY AND FOSPAN+FOEXP<>0";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCRMSequity"].ConnectionString))
                {
                    conn.Open();


                    OracleCommand cmd = new OracleCommand(QUERY, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataAdapter adp = new OracleDataAdapter(cmd);

                    adp.Fill(DT);

                }
                if (DT.Rows.Count > 0)
                {
                    DT.Columns[0].ColumnName = "CM Code";
                    DT.Columns[1].ColumnName = "Member Name";
                    DT.Columns[2].ColumnName = "TM/CP Code";
                    DT.Columns[3].ColumnName = "TM/CP Name";
                    DT.Columns[4].ColumnName = "Client Code";
                    DT.Columns[5].ColumnName = "Cash";
                    DT.Columns[6].ColumnName = "Non cash";
                    DT.Columns[7].ColumnName = "Initial Margins";
                    DT.Columns[8].ColumnName = "Crystallised Obligation Margins";
                    DT.Columns[9].ColumnName = "Delivery Margin";
                    DT.Columns[10].ColumnName = "Extreme Loss Margin";
                    DT.Columns[11].ColumnName = "Total Margins";
                    DT.Columns[12].ColumnName = "MTM P/L";
                    DT.Columns[13].ColumnName = "Client Margins > 90%";
                    DT.Columns[14].ColumnName = "90% of Client Excess Non-cash";
                    DT.Columns[15].ColumnName = "Eligible Non-cash (Subject to the extent of TM Excess Cash/CM Excess Cash)";
                    DT.AcceptChanges();
                    string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Export_NMass";
                    if (!System.IO.Directory.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                    }
                    if (System.IO.File.Exists(FilePath + "\\SMC_NMASS_ClientMargin_FO_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"))
                    {
                        System.IO.File.Delete(FilePath + "\\SMC_NMASS_ClientMargin_FO_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    }
                    Helper.ToCSV(DT, FilePath + "\\SMC_NMASS_ClientMargin_FO_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    MessageBox.Show("File Export Successfuly");
                }
                else
                {
                    MessageBox.Show("No Record Found");
                }
            }
            if (cmbSegment.Text == "CD")
            {

                DataTable DT = new DataTable();
                string QUERY = "select 'M50504','SMC','07714','SMC',MST.TRDIDC,0,0,0,0,0,0,NCURMRG+NCUREXMRG Margin,0,0,0,0 from rmsposi POSI,CLNTMST MST" +
                    " where POSI.PARTY=MST.PARTY AND NCURMRG+NCUREXMRG<>0";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCRMSequity"].ConnectionString))
                {
                    conn.Open();


                    OracleCommand cmd = new OracleCommand(QUERY, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataAdapter adp = new OracleDataAdapter(cmd);

                    adp.Fill(DT);

                }
                if (DT.Rows.Count > 0)
                {
                    DT.Columns[0].ColumnName = "CM Code";
                    DT.Columns[1].ColumnName = "Member Name";
                    DT.Columns[2].ColumnName = "TM/CP Code";
                    DT.Columns[3].ColumnName = "TM/CP Name";
                    DT.Columns[4].ColumnName = "Client Code";
                    DT.Columns[5].ColumnName = "Cash";
                    DT.Columns[6].ColumnName = "Non Cash";
                    DT.Columns[7].ColumnName = "Initial Margins";
                    DT.Columns[8].ColumnName = "Crystallised Obligation Margins";
                    DT.Columns[9].ColumnName = "Delivery Margin";
                    DT.Columns[10].ColumnName = "Extreme Loss Margin";
                    DT.Columns[11].ColumnName = "Total Margin";
                    DT.Columns[12].ColumnName = "MTM P/L";
                    DT.Columns[13].ColumnName = "Client Margins > 90%";
                    DT.Columns[14].ColumnName = "90% of Client Excess Non-cash";
                    DT.Columns[15].ColumnName = "Eligible Non-cash (Subject to the extent of TM Excess Cash/CM Excess Cash)";
                    DT.AcceptChanges();
                    string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Export_NMass";
                    if (!System.IO.Directory.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                    }
                    if (System.IO.File.Exists(FilePath + "\\SMC_NMASS_ClientMargin_CD_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"))
                    {
                        System.IO.File.Delete(FilePath + "\\SMC_NMASS_ClientMargin_CD_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    }
                    Helper.ToCSV(DT, FilePath + "\\SMC_NMASS_ClientMargin_CD_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    MessageBox.Show("File Export Successfuly");
                }
                else
                {
                    MessageBox.Show("No Record Found");
                }
            }
            if (cmbSegment.Text == "CO")
            {

                DataTable DT = new DataTable();
                string QUERY = "select TRUNC(SYSDATE),'8200','SMC','','10515','SMC',MST.TRDIDM,0,0,0,0,0,0,0,0,0,0,0,0,0,0,mcspan+MCXTENDER+MCXADDLMRG+MCXSPLMRG+ELMMRG Margin from rmsposi POSI, CLNTMST MST" +
                    " where POSI.PARTY=MST.PARTY AND mcspan<>0";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCRMSequity"].ConnectionString))
                {
                    conn.Open();


                    OracleCommand cmd = new OracleCommand(QUERY, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataAdapter adp = new OracleDataAdapter(cmd);

                    adp.Fill(DT);

                }
                if (DT.Rows.Count > 0)
                {
                    DT.Columns[0].ColumnName = "Date";
                    DT.Columns[1].ColumnName = "CM Code";
                    DT.Columns[2].ColumnName = "CM Name";
                    DT.Columns[3].ColumnName = "CP Code";
                    DT.Columns[4].ColumnName = "TM Code";
                    DT.Columns[5].ColumnName = "TM Name";
                    DT.Columns[6].ColumnName = "End Client";
                    DT.Columns[7].ColumnName = "Allocation at BOD";
                    DT.Columns[8].ColumnName = "Allocations during the Day";
                    DT.Columns[9].ColumnName = "Deallocation during the Day";
                    DT.Columns[10].ColumnName = "Allocation at EOD";
                    DT.Columns[11].ColumnName = "OWN Collateral at BOD";
                    DT.Columns[12].ColumnName = "OWN Collateral added during Day";
                    DT.Columns[13].ColumnName = "OWN Collateral reduced during Day";
                    DT.Columns[14].ColumnName = "OWN Collateral at EOD";
                    DT.Columns[15].ColumnName = "OWN Others Collateral at BOD";
                    DT.Columns[16].ColumnName = "OWN Others Collateral added during Day";
                    DT.Columns[17].ColumnName = "OWN Others Collateral reduced during Day";
                    DT.Columns[18].ColumnName = "OWN Others Collateral at EOD";
                    DT.Columns[19].ColumnName = "Total Collateral at EOD";
                    DT.Columns[20].ColumnName = "Eligible Collateral at EOD";
                    DT.Columns[21].ColumnName = "Margin Utilisation";


                    DT.AcceptChanges();
                    string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Export_NMass";
                    if (!System.IO.Directory.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                    }
                    if (System.IO.File.Exists(FilePath + "\\SMC_NMASS_ClientMargin_CO_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"))
                    {
                        System.IO.File.Delete(FilePath + "\\SMC_NMASS_ClientMargin_CO_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    }
                    Helper.ToCSV(DT, FilePath + "\\SMC_NMASS_ClientMargin_CO_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    MessageBox.Show("File Export Successfuly");
                }
                else
                {
                    MessageBox.Show("No Record Found");
                }
            }
        }

        private void cmbSegment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSegment.Text == "CM")
            {
                label2.Visible=true;
                cmbType.Visible=true;
                cmbType.Items.Add("Summary");
                cmbType.Items.Add("Details");

                cmbType.SelectedIndex = 0;
            }
            else
            {
                label2.Visible = false;
                cmbType.Visible = false;
            }
        }
    }
}
