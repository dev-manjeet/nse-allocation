using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleCommand = Oracle.ManagedDataAccess.Client.OracleCommand;

namespace NSEAllocation
{
    public partial class FrmSMC_CC01 : Form
    {
        public FrmSMC_CC01()
        {
            InitializeComponent();
        }

        private void FrmSMC_CC01_Load(object sender, EventArgs e)
        {
            cmbSegment.Items.Add("CM");
            cmbSegment.Items.Add("FO");
            cmbSegment.Items.Add("CD");
            //cmbSegment.Items.Add("CO");
            //cmbSegment.Items.Add("DT");
            //cmbSegment.Items.Add("SLB");
            //cmbSegment.Items.Add("TPR");
            //cmbSegment.Items.Add("OFS");
            cmbSegment.SelectedIndex = 0;
        }

        private void btnupload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Trading Holding File Dialog";
                //fdlg.InitialDirectory = @"c:\";
                fdlg.InitialDirectory = "";
                fdlg.Filter = "All files (*.csv)|*.*|All files (*.CSV)|*.csv";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtfile.Text = fdlg.FileName;
                }
                if (!fdlg.FileName.ToUpper().Contains(".CSV"))
                {
                    MessageBox.Show("Please Upload Only CSV Format");
                    txtfile.Text = "";
                    return;
                }


            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtfile.Text == "")
                {
                    MessageBox.Show("Please Select Upload File");
                    return;
                }
                if (cmbSegment.Text == "CM")
                {
                    string v = txtfile.Text.Substring(txtfile.Text.Length - 25);
                    string[] val = v.Split('_');
                    if (v[0].ToString() != "C")
                    {
                        MessageBox.Show("Please Select File As Segment Wise");
                        return;
                    }

                }
                if (cmbSegment.Text == "FO")
                {
                    string v = txtfile.Text.Substring(txtfile.Text.Length - 25);
                    string[] val = v.Split('_');
                    if (v[0].ToString() != "F")
                    {
                        MessageBox.Show("Please Select File As Segment Wise");
                        return;
                    }

                }
                if (cmbSegment.Text == "CD")
                {
                    string v = txtfile.Text.Substring(txtfile.Text.Length - 25);
                    string[] val = v.Split('_');
                    if (v[0].ToString() != "X")
                    {
                        MessageBox.Show("Please Select File As Segment Wise");
                        return;
                    }

                }
                string path = txtfile.Text;
                string CSVFilePath = Path.GetFullPath(path);

                string[] allLines = System.IO.File.ReadAllLines(CSVFilePath);
                if (cmbSegment.Text == "CM")
                {
                    FunTruncate();
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into SMC_C_CC01 (code,Cash_Component,ValofGSEC_GMF_CMP_Repled," +
                            "Valofnon_cash_Repledg,Total,Margin,Create_DateTime)" +
                            "values(:code,:Cash_Component,:ValofGSEC_GMF_CMP_Repled," +
                            ":Valofnon_cash_Repledg,:Total,:Margin,:Create_DateTime)";
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();

                            OracleCommand cmd = new OracleCommand(QUERY, conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(":code", OracleDbType.Varchar2).Value = Arr[0].ToString();
                            cmd.Parameters.Add(":Cash_Component", OracleDbType.NVarchar2).Value = Arr[1].ToString();
                            cmd.Parameters.Add(":ValofGSEC_GMF_CMP_Repled", OracleDbType.NVarchar2).Value = Arr[2].ToString();
                            cmd.Parameters.Add(":Valofnon_cash_Repledg", OracleDbType.NVarchar2).Value = Arr[3].ToString();
                            cmd.Parameters.Add(":Total", OracleDbType.NVarchar2).Value = Arr[4].ToString();
                            cmd.Parameters.Add(":Margin", OracleDbType.NVarchar2).Value = Arr[5].ToString();
                            cmd.Parameters.Add(":Create_DateTime", OracleDbType.Date).Value = Convert.ToDateTime(DateTime.Now);
                            int V= cmd.ExecuteNonQuery();

                        }
                    }
                    MessageBox.Show("Total Records " + allLines.Count() + " Import Successfully");
                }
                if (cmbSegment.Text == "FO")
                {
                    FunTruncate();
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into SMC_F_CC01 (code,Cash_Component,ValofGSEC_GMF_CMP_Repled," +
                           "Valofnon_cash_Repledg,Total,Margin,Create_DateTime)" +
                           "values(:code,:Cash_Component,:ValofGSEC_GMF_CMP_Repled," +
                           ":Valofnon_cash_Repledg,:Total,:Margin,:Create_DateTime)";
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();

                            OracleCommand cmd = new OracleCommand(QUERY, conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(":code", OracleDbType.Varchar2).Value = Arr[0].ToString();
                            cmd.Parameters.Add(":Cash_Component", OracleDbType.NVarchar2).Value = Arr[1].ToString();
                            cmd.Parameters.Add(":ValofGSEC_GMF_CMP_Repled", OracleDbType.NVarchar2).Value = Arr[2].ToString();
                            cmd.Parameters.Add(":Valofnon_cash_Repledg", OracleDbType.NVarchar2).Value = Arr[3].ToString();
                            cmd.Parameters.Add(":Total", OracleDbType.NVarchar2).Value = Arr[4].ToString();
                            cmd.Parameters.Add(":Margin", OracleDbType.NVarchar2).Value = Arr[5].ToString();
                            cmd.Parameters.Add(":Create_DateTime", OracleDbType.Date).Value = Convert.ToDateTime(DateTime.Now);
                            int V = cmd.ExecuteNonQuery();

                        }
                    }
                    MessageBox.Show("Total Records " + allLines.Count() + " Import Successfully");
                }
                if (cmbSegment.Text == "CD")
                {
                    FunTruncate();
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into SMC_X_CC01 (code,Cash_Component,ValofGSEC_GMF_CMP_Repled," +
                           "Valofnon_cash_Repledg,Total,Margin,Create_DateTime)" +
                           "values(:code,:Cash_Component,:ValofGSEC_GMF_CMP_Repled," +
                           ":Valofnon_cash_Repledg,:Total,:Margin,:Create_DateTime)";
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();

                            OracleCommand cmd = new OracleCommand(QUERY, conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(":code", OracleDbType.Varchar2).Value = Arr[0].ToString();
                            cmd.Parameters.Add(":Cash_Component", OracleDbType.NVarchar2).Value = Arr[1].ToString();
                            cmd.Parameters.Add(":ValofGSEC_GMF_CMP_Repled", OracleDbType.NVarchar2).Value = Arr[2].ToString();
                            cmd.Parameters.Add(":Valofnon_cash_Repledg", OracleDbType.NVarchar2).Value = Arr[3].ToString();
                            cmd.Parameters.Add(":Total", OracleDbType.NVarchar2).Value = Arr[4].ToString();
                            cmd.Parameters.Add(":Margin", OracleDbType.NVarchar2).Value = Arr[5].ToString();
                            cmd.Parameters.Add(":Create_DateTime", OracleDbType.Date).Value = Convert.ToDateTime(DateTime.Now);
                            int V = cmd.ExecuteNonQuery();

                        }
                    }
                    MessageBox.Show("Total Records " + allLines.Count() + " Import Successfully");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void FunTruncate()
        {
            if (cmbSegment.Text == "CM")
            {
                string QUERY = "TRUNCATE TABLE SMC_C_CC01";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                {
                    conn.Open();

                    OracleCommand cmd = new OracleCommand(QUERY, conn);
                    cmd.CommandType = CommandType.Text;
                    
                    int V = cmd.ExecuteNonQuery();

                }
            }
            if (cmbSegment.Text == "FO")
            {
                string QUERY = "TRUNCATE TABLE SMC_F_CC01";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                {
                    conn.Open();

                    OracleCommand cmd = new OracleCommand(QUERY, conn);
                    cmd.CommandType = CommandType.Text;

                    int V = cmd.ExecuteNonQuery();

                }
            }
            if (cmbSegment.Text == "CD")
            {
                string QUERY = "TRUNCATE TABLE SMC_X_CC01";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                {
                    conn.Open();

                    OracleCommand cmd = new OracleCommand(QUERY, conn);
                    cmd.CommandType = CommandType.Text;

                    int V = cmd.ExecuteNonQuery();

                }
            }
        }

    }
}
