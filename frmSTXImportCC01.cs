using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSEAllocation
{
    public partial class frmSTXImportCC01 : Form
    {
        public frmSTXImportCC01()
        {
            InitializeComponent();
        }
        private void frmSTXImportCC01_Load(object sender, EventArgs e)
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
        private void btnCashEqulaintSubmit_Click(object sender, EventArgs e)
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
                    Truncate();
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into STX_C_CC01 (code,Cash_Component,Value_of_GSEC_GMF_CMP_Repledged," +
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
                            sqlcommand.ExecuteNonQuery();

                        }
                    }
                    MessageBox.Show("Total Records " + allLines.Count() + " Import Successfully");
                }
                if (cmbSegment.Text == "FO")
                {
                    Truncate();
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into STX_F_CC01 (code,Cash_Component,Value_of_GSEC_GMF_CMP_Repledged," +
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
                            sqlcommand.ExecuteNonQuery();

                        }
                    }
                    MessageBox.Show("Total Records " + allLines.Count() + " Import Successfully");
                }
                if (cmbSegment.Text == "CD")
                {
                    Truncate();
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into STX_X_CC01 (code,Cash_Component,Value_of_GSEC_GMF_CMP_Repledged," +
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
                            sqlcommand.ExecuteNonQuery();

                        }
                    }
                    MessageBox.Show("Total Records " + allLines.Count() + " Import Successfully");
                }
                if (cmbSegment.Text == "CO")
                {
                    Truncate();
                    for (int i = 0; i < allLines.Count(); i++)
                    {

                        string[] Arr = allLines[i].Split(',');
                        string QUERY = "INSERT into STX_CO_CC01 (PartyCode,Margin,Create_DateTime)" +
                            "values(@PartyCode,@Margin,@Create_DateTime)";
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand sqlcommand = new SqlCommand();
                            sqlcommand.CommandType = CommandType.Text;
                            sqlcommand.CommandText = QUERY;
                            sqlcommand.Connection = con;
                            sqlcommand.Parameters.Add("@PartyCode", SqlDbType.VarChar).Value = Arr[2].ToString();
                            sqlcommand.Parameters.Add("@Margin", SqlDbType.VarChar).Value = Arr[6].ToString();
                            sqlcommand.Parameters.Add("@Create_DateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                            sqlcommand.ExecuteNonQuery();

                        }
                    }
                    //for (int i = 0; i < allLines.Count(); i++)
                    //{

                    //    string[] Arr = allLines[i].Split(',');
                    //    string QUERY = "INSERT into STX_CO_CC01 (code,Cash_Component,Repledged," +
                    //        "non_cash_Repledged,Total,Margin,Create_DateTime)" +
                    //        "values(@code,@Cash_Component,@Repledged," +
                    //        "@non_cash_Repledged,@Total,@Margin,@Create_DateTime)";
                    //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                    //    {
                    //        con.Open();
                    //        SqlCommand sqlcommand = new SqlCommand();
                    //        sqlcommand.CommandType = CommandType.Text;
                    //        sqlcommand.CommandText = QUERY;
                    //        sqlcommand.Connection = con;
                    //        sqlcommand.Parameters.Add("@code", SqlDbType.VarChar).Value = Arr[2].ToString();
                    //        sqlcommand.Parameters.Add("@Cash_Component", SqlDbType.VarChar).Value = Arr[1].ToString();
                    //        sqlcommand.Parameters.Add("@Repledged", SqlDbType.VarChar).Value = Arr[2].ToString();
                    //        sqlcommand.Parameters.Add("@non_cash_Repledged", SqlDbType.VarChar).Value = Arr[3].ToString();
                    //        sqlcommand.Parameters.Add("@Total", SqlDbType.VarChar).Value = Arr[4].ToString();
                    //        sqlcommand.Parameters.Add("@Margin", SqlDbType.VarChar).Value = Arr[5].ToString();
                    //        sqlcommand.Parameters.Add("@Create_DateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                    //        sqlcommand.ExecuteNonQuery();

                    //    }
                    //}
                    MessageBox.Show("Total Records " + allLines.Count() + " Import Successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void Truncate()
        {
            if (cmbSegment.Text == "CM")
            {
                string QUERY = "truncate table STX_C_CC01 ";
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                {
                    con.Open();
                    SqlCommand sqlcommand = new SqlCommand();
                    sqlcommand.CommandType = CommandType.Text;
                    sqlcommand.CommandText = QUERY;
                    sqlcommand.Connection = con;

                    sqlcommand.ExecuteNonQuery();

                }
            }
            if (cmbSegment.Text == "FO")
            {
                string QUERY = "truncate table STX_F_CC01 ";
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                {
                    con.Open();
                    SqlCommand sqlcommand = new SqlCommand();
                    sqlcommand.CommandType = CommandType.Text;
                    sqlcommand.CommandText = QUERY;
                    sqlcommand.Connection = con;

                    sqlcommand.ExecuteNonQuery();

                }
            }
            if (cmbSegment.Text == "CD")
            {
                string QUERY = "truncate table STX_X_CC01 ";
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                {
                    con.Open();
                    SqlCommand sqlcommand = new SqlCommand();
                    sqlcommand.CommandType = CommandType.Text;
                    sqlcommand.CommandText = QUERY;
                    sqlcommand.Connection = con;

                    sqlcommand.ExecuteNonQuery();

                }
            }
            if (cmbSegment.Text == "CO")
            {
                string QUERY = "truncate table STX_CO_CC01 ";
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                {
                    con.Open();
                    SqlCommand sqlcommand = new SqlCommand();
                    sqlcommand.CommandType = CommandType.Text;
                    sqlcommand.CommandText = QUERY;
                    sqlcommand.Connection = con;

                    sqlcommand.ExecuteNonQuery();

                }
            }
        }

    }
}
