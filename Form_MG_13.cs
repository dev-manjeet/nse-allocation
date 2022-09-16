using System;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OracleConnection = Oracle.ManagedDataAccess.Client.OracleConnection;
using OracleCommand = System.Data.OracleClient.OracleCommand;

namespace NSEAllocation
{
    public partial class Form_MG_13 : Form
    {
        public Form_MG_13()
        {
            InitializeComponent();
        }

        private void Form_MG_13_Load(object sender, EventArgs e)
        {
            cmbSegment.Items.Add("CM");
            cmbSegment.Items.Add("FO");
            cmbSegment.Items.Add("CD");
            cmbSegment.Items.Add("CO");
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
                fdlg.Title = "File Dialog";
               
                fdlg.InitialDirectory = "";
                fdlg.Filter = "All files (*.lis)|*.*|All files (*.LIS)|*.lis";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtfile.Text = fdlg.FileName;
                }
                if (!fdlg.FileName.ToUpper().Contains(".LIS"))
                {
                    MessageBox.Show("Please Upload Only LIS Format");
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
                    string  v = txtfile.Text.Substring(txtfile.Text.Length-31);
                    string[] val= v.Split('_');
                    if (v[0].ToString()!="C")
                    {
                        MessageBox.Show("Please Select File As Segment Wise");
                        return;
                    }
                    
                }
                if (cmbSegment.Text == "FO")
                {
                    string v = txtfile.Text.Substring(txtfile.Text.Length - 31);
                    string[] val = v.Split('_');
                    if (v[0].ToString() != "F")
                    {
                        MessageBox.Show("Please Select File As Segment Wise");
                        return;
                    }

                }
                if (cmbSegment.Text == "CD")
                {
                    string v = txtfile.Text.Substring(txtfile.Text.Length - 31);
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
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into STX_C_MG_13 (Curr_date,ClientCode,VAR_MARGIN_EL_MARGIN," +
                            "Minimum_Margin,Additional_margin,MTM_Loss,Client_Type_Flag,Create_DateTime)" +
                            "values(@Curr_date,@ClientCode,@VAR_MARGIN_EL_MARGIN," +
                            "@Minimum_Margin,@Additional_margin,@MTM_Loss,@Client_Type_Flag,@Create_DateTime)";
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand sqlcommand = new SqlCommand();
                            sqlcommand.CommandType = CommandType.Text;
                            sqlcommand.CommandText = QUERY;
                            sqlcommand.Connection = con;
                            sqlcommand.Parameters.Add("@Curr_date", SqlDbType.NVarChar).Value = Arr[0].ToString();
                            sqlcommand.Parameters.Add("@ClientCode", SqlDbType.VarChar).Value = Arr[1].ToString();
                            sqlcommand.Parameters.Add("@VAR_MARGIN_EL_MARGIN", SqlDbType.VarChar).Value = Arr[2].ToString();
                            sqlcommand.Parameters.Add("@Minimum_Margin", SqlDbType.VarChar).Value = Arr[3].ToString();
                            sqlcommand.Parameters.Add("@Additional_margin", SqlDbType.VarChar).Value = Arr[4].ToString();
                            sqlcommand.Parameters.Add("@MTM_Loss", SqlDbType.VarChar).Value = Arr[5].ToString();
                            sqlcommand.Parameters.Add("@Client_Type_Flag", SqlDbType.VarChar).Value = Arr[8].ToString();
                            sqlcommand.Parameters.Add("@Create_DateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                            sqlcommand.ExecuteNonQuery();

                        }
                    }
                }
                if (cmbSegment.Text == "FO")
                {
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into STX_CC01 (code,Cash_Component,Value_of_GSEC_GMF_CMP_Repledged," +
                            "Value_of_non_cash_Repledged,Total,Margin,Create_DateTime)" +
                            "values(@code,@Cash_Component,@Value_of_GSEC_GMF_CMP_Repledged," +
                            "@Value_of_non_cash_Repledged,@Total,@Margin,@Create_DateTime)";
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand sqlcommand = new SqlCommand();
                            sqlcommand.CommandType = CommandType.Text;
                            sqlcommand.CommandText = QUERY;
                            sqlcommand.Connection = con;
                            sqlcommand.Parameters.Add("@code", SqlDbType.VarChar).Value = Arr[0].ToString();
                            sqlcommand.Parameters.Add("@Cash_Component", SqlDbType.VarChar).Value = Arr[1].ToString();
                            sqlcommand.Parameters.Add("@Value_of_GSEC_GMF_CMP_Repledged", SqlDbType.VarChar).Value = Arr[2].ToString();
                            sqlcommand.Parameters.Add("@Value_of_non_cash_Repledged", SqlDbType.VarChar).Value = Arr[3].ToString();
                            sqlcommand.Parameters.Add("@Total", SqlDbType.VarChar).Value = Arr[4].ToString();
                            sqlcommand.Parameters.Add("@Margin", SqlDbType.VarChar).Value = Arr[5].ToString();
                            sqlcommand.Parameters.Add("@Create_DateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                            //sqlcommand.ExecuteNonQuery();

                        }
                    }
                }
                if (cmbSegment.Text == "CD")
                {
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into STX_CC01 (code,Cash_Component,Value_of_GSEC_GMF_CMP_Repledged," +
                            "Value_of_non_cash_Repledged,Total,Margin,Create_DateTime)" +
                            "values(@code,@Cash_Component,@Value_of_GSEC_GMF_CMP_Repledged," +
                            "@Value_of_non_cash_Repledged,@Total,@Margin,@Create_DateTime)";
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand sqlcommand = new SqlCommand();
                            sqlcommand.CommandType = CommandType.Text;
                            sqlcommand.CommandText = QUERY;
                            sqlcommand.Connection = con;
                            sqlcommand.Parameters.Add("@code", SqlDbType.VarChar).Value = Arr[0].ToString();
                            sqlcommand.Parameters.Add("@Cash_Component", SqlDbType.VarChar).Value = Arr[1].ToString();
                            sqlcommand.Parameters.Add("@Value_of_GSEC_GMF_CMP_Repledged", SqlDbType.VarChar).Value = Arr[2].ToString();
                            sqlcommand.Parameters.Add("@Value_of_non_cash_Repledged", SqlDbType.VarChar).Value = Arr[3].ToString();
                            sqlcommand.Parameters.Add("@Total", SqlDbType.VarChar).Value = Arr[4].ToString();
                            sqlcommand.Parameters.Add("@Margin", SqlDbType.VarChar).Value = Arr[5].ToString();
                            sqlcommand.Parameters.Add("@Create_DateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                            //sqlcommand.ExecuteNonQuery();

                        }
                    }
                }
                if (cmbSegment.Text == "CO")
                {
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "select party,mcspan,MCXTENDER,MCXADDLMRG,MCXSPLMRG,elmmrg from rmsposi where mcspan <> 0";

                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand sqlcommand = new SqlCommand();
                            sqlcommand.CommandType = CommandType.Text;
                            sqlcommand.CommandText = QUERY;
                            sqlcommand.Connection = con;
                            sqlcommand.Parameters.Add("@code", SqlDbType.VarChar).Value = Arr[0].ToString();
                            sqlcommand.Parameters.Add("@Cash_Component", SqlDbType.VarChar).Value = Arr[1].ToString();
                            sqlcommand.Parameters.Add("@Value_of_GSEC_GMF_CMP_Repledged", SqlDbType.VarChar).Value = Arr[2].ToString();
                            sqlcommand.Parameters.Add("@Value_of_non_cash_Repledged", SqlDbType.VarChar).Value = Arr[3].ToString();
                            sqlcommand.Parameters.Add("@Total", SqlDbType.VarChar).Value = Arr[4].ToString();
                            sqlcommand.Parameters.Add("@Margin", SqlDbType.VarChar).Value = Arr[5].ToString();
                            sqlcommand.Parameters.Add("@Create_DateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                            //sqlcommand.ExecuteNonQuery();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            try
            {

            
            if (cmbSegment.Text == "CO")
            {
               //string QUERY = "select party,mcspan, MCXTENDER,MCXADDLMRG,MCXSPLMRG from rmsposi where mcspan<>0";
               // using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["StxRMSequity"].ConnectionString))
               // {
               //     conn.Open();
               //         OracleCommand cmd = new OracleCommand(QUERY, conn);

               //         cmd.CommandType = CommandType.Text;
               //     OracleDataAdapter adp = new OracleDataAdapter(cmd);
               //     DataTable DT = new DataTable();
               //     adp.Fill(DT);

               // }
            }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
