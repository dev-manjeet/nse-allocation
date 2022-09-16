using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NSEAllocation.Models;
using Oracle.ManagedDataAccess.Client;

namespace NSEAllocation
{
    public partial class FormStxNMASS_Export : Form
    {
        public FormStxNMASS_Export()
        {
            InitializeComponent();
        }

        private void FormStxNMASS_Export_Load(object sender, EventArgs e)
        {
            //cmbSegment.Items.Add("CM");
            cmbSegment.Items.Add("FO");
            cmbSegment.Items.Add("CD");
            cmbSegment.Items.Add("CO");
            //cmbSegment.Items.Add("DT");
            //cmbSegment.Items.Add("SLB");
            //cmbSegment.Items.Add("TPR");
            //cmbSegment.Items.Add("OFS");
            cmbSegment.SelectedIndex = 0;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string QUERY = "";
                if (cmbSegment.Text == "CM")
                {

                    QUERY = "";
                    using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["StxRMSequity"].ConnectionString))
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand(QUERY, conn);
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(":fromDate", OracleDbType.Char).Value = frmdate.Text;
                        //cmd.Parameters.Add(":ToDate", OracleDbType.Char).Value = todate.Text;
                        //cmd.Parameters.Add(":Seg", OracleDbType.Varchar2).Value = comboBox1.Text;
                        OracleDataAdapter adp = new OracleDataAdapter(cmd);
                        DataTable DT = new DataTable();
                        adp.Fill(DT);

                    }
                }
                if (cmbSegment.Text == "FO")
                {

                    QUERY = "";
                    using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["StxRMSequity"].ConnectionString))
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand(QUERY, conn);
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(":fromDate", OracleDbType.Char).Value = frmdate.Text;
                        //cmd.Parameters.Add(":ToDate", OracleDbType.Char).Value = todate.Text;
                        //cmd.Parameters.Add(":Seg", OracleDbType.Varchar2).Value = comboBox1.Text;
                        OracleDataAdapter adp = new OracleDataAdapter(cmd);
                        DataTable DT = new DataTable();
                        adp.Fill(DT);

                    }
                }
                if (cmbSegment.Text == "CD")
                {

                    QUERY = "";
                    using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["StxRMSequity"].ConnectionString))
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand(QUERY, conn);
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(":fromDate", OracleDbType.Char).Value = frmdate.Text;
                        //cmd.Parameters.Add(":ToDate", OracleDbType.Char).Value = todate.Text;
                        //cmd.Parameters.Add(":Seg", OracleDbType.Varchar2).Value = comboBox1.Text;
                        OracleDataAdapter adp = new OracleDataAdapter(cmd);
                        DataTable DT = new DataTable();
                        adp.Fill(DT);

                    }
                }
                if (cmbSegment.Text == "CO")
                {

                    QUERY = "";
                    using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["StxRMSequity"].ConnectionString))
                    {
                        conn.Open();


                        OracleCommand cmd = new OracleCommand(QUERY, conn);
                        cmd.CommandType = CommandType.Text;

                        OracleDataAdapter adp = new OracleDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                    }
                }

            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (cmbSegment.Text == "CM")
            {

            }
            if (cmbSegment.Text == "FO")
            {
                DataTable dt = new DataTable();
                string QUERY = "select MST.TRDIDF PARTY,FOSPAN+FOEXP-(LEAST(PREMRCVDFO,0)) Margin from rmsposi POSI,CLNTMST MST" +
                               " where POSI.PARTY=MST.PARTY AND FOSPAN+FOEXP-(LEAST(PREMRCVDFO,0))<>0";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["StxRMSequity"].ConnectionString))
                {
                    conn.Open();


                    OracleCommand cmd = new OracleCommand(QUERY, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataAdapter adp = new OracleDataAdapter(cmd);

                    adp.Fill(dt);

                }

                if (dt.Rows.Count > 0)
                {
                    TRUNCATE();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        QUERY = "INSERT into STX_FO_MG_13 (party,Margin,Create_DateTime)" +

                               "values(@party,@Margin,@Create_DateTime)";

                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand sqlcommand = new SqlCommand();
                            sqlcommand.CommandType = CommandType.Text;
                            sqlcommand.CommandText = QUERY;
                            sqlcommand.Connection = con;
                            sqlcommand.Parameters.Add("@party", SqlDbType.VarChar).Value = dt.Rows[i]["party"].ToString();
                            sqlcommand.Parameters.Add("@Margin", SqlDbType.VarChar).Value = dt.Rows[i]["Margin"].ToString();


                            sqlcommand.Parameters.Add("@Create_DateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                            sqlcommand.ExecuteNonQuery();

                        }
                    }
                    //string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Export_MG13";
                    //if (!System.IO.Directory.Exists(FilePath))
                    //{
                    //    System.IO.Directory.CreateDirectory(FilePath);
                    //}
                    //if (System.IO.File.Exists(FilePath + "\\CO_MG13_"+DateTime.Now.ToString("ddMMyyyy")+".csv"))
                    //{
                    //    System.IO.File.Delete(FilePath + "\\CO_MG13_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    //}
                    //Helper.ToCSV(dt, FilePath + "\\CO_MG13_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    // MessageBox.Show("File Export Successfuly");
                    MessageBox.Show("Total Record Insert " + dt.Rows.Count + " Successfuly");
                }
            }
            if (cmbSegment.Text == "CD")
            {
                DataTable dt = new DataTable();
                string QUERY = "select MST.TRDIDC PARTY,NCURMRG+NCUREXMRG Margin from rmsposi POSI,CLNTMST MST" +
                   " where POSI.PARTY=MST.PARTY AND NCURMRG+NCUREXMRG<>0";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["StxRMSequity"].ConnectionString))
                {
                    conn.Open();


                    OracleCommand cmd = new OracleCommand(QUERY, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataAdapter adp = new OracleDataAdapter(cmd);

                    adp.Fill(dt);

                }

                if (dt.Rows.Count > 0)
                {
                    TRUNCATE();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        QUERY = "INSERT into STX_CD_MG_13 (party,Margin,Create_DateTime)" +

                               "values(@party,@Margin,@Create_DateTime)";

                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand sqlcommand = new SqlCommand();
                            sqlcommand.CommandType = CommandType.Text;
                            sqlcommand.CommandText = QUERY;
                            sqlcommand.Connection = con;
                            sqlcommand.Parameters.Add("@party", SqlDbType.VarChar).Value = dt.Rows[i]["party"].ToString();
                            sqlcommand.Parameters.Add("@Margin", SqlDbType.VarChar).Value = dt.Rows[i]["Margin"].ToString();


                            sqlcommand.Parameters.Add("@Create_DateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                            sqlcommand.ExecuteNonQuery();

                        }
                    }
                    //string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Export_MG13";
                    //if (!System.IO.Directory.Exists(FilePath))
                    //{
                    //    System.IO.Directory.CreateDirectory(FilePath);
                    //}
                    //if (System.IO.File.Exists(FilePath + "\\CO_MG13_"+DateTime.Now.ToString("ddMMyyyy")+".csv"))
                    //{
                    //    System.IO.File.Delete(FilePath + "\\CO_MG13_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    //}
                    //Helper.ToCSV(dt, FilePath + "\\CO_MG13_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    // MessageBox.Show("File Export Successfuly");
                    MessageBox.Show("Total Record Insert " + dt.Rows.Count + " Successfuly");
                }
            }
            if (cmbSegment.Text == "CO")
            {
                DataTable dt = new DataTable();
                string QUERY = "select MST.TRDIDM PARTY,mcspan+MCXTENDER+MCXADDLMRG+MCXSPLMRG+ELMMRG Margin from rmsposi POSI, CLNTMST MST" +
                     " where POSI.PARTY=MST.PARTY AND mcspan<>0";
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["StxRMSequity"].ConnectionString))
                {
                    conn.Open();


                    OracleCommand cmd = new OracleCommand(QUERY, conn);
                    cmd.CommandType = CommandType.Text;

                    OracleDataAdapter adp = new OracleDataAdapter(cmd);

                    adp.Fill(dt);

                }

                if (dt.Rows.Count > 0)
                {
                    TRUNCATE();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        QUERY = "INSERT into STX_CO_MG_13 (party,Margin,Create_DateTime)" +

                               "values(@party,@Margin,@Create_DateTime)";

                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand sqlcommand = new SqlCommand();
                            sqlcommand.CommandType = CommandType.Text;
                            sqlcommand.CommandText = QUERY;
                            sqlcommand.Connection = con;
                            sqlcommand.Parameters.Add("@party", SqlDbType.VarChar).Value = dt.Rows[i]["party"].ToString();
                            sqlcommand.Parameters.Add("@Margin", SqlDbType.VarChar).Value = dt.Rows[i]["Margin"].ToString();


                            sqlcommand.Parameters.Add("@Create_DateTime", SqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now);
                            sqlcommand.ExecuteNonQuery();

                        }
                    }
                    //string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Export_MG13";
                    //if (!System.IO.Directory.Exists(FilePath))
                    //{
                    //    System.IO.Directory.CreateDirectory(FilePath);
                    //}
                    //if (System.IO.File.Exists(FilePath + "\\CO_MG13_"+DateTime.Now.ToString("ddMMyyyy")+".csv"))
                    //{
                    //    System.IO.File.Delete(FilePath + "\\CO_MG13_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    //}
                    //Helper.ToCSV(dt, FilePath + "\\CO_MG13_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                    // MessageBox.Show("File Export Successfuly");
                    MessageBox.Show("Total Record Insert " + dt.Rows.Count + " Successfuly");
                }
            }
        }

        private void TRUNCATE()
        {
            if (cmbSegment.Text == "CD")
            {

                string QUERY = "TRUNCATE TABLE STX_CD_MG_13";
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

                string QUERY = "TRUNCATE TABLE STX_FO_MG_13";
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

                string QUERY = "TRUNCATE TABLE STX_CO_MG_13";
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
