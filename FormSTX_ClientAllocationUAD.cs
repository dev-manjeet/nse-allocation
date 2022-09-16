using NSEAllocation.Models;
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

namespace NSEAllocation
{
    public partial class FormSTX_ClientAllocationUAD : Form
    {
        public FormSTX_ClientAllocationUAD()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                string QUERY = "smc_ClientAllocation";
                DataTable dtExport = new DataTable();


                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Stxoffice"].ConnectionString))
                {
                    con.Open();
                    SqlCommand sqlcommand = new SqlCommand(QUERY, con);
                    sqlcommand.CommandType = CommandType.StoredProcedure;
                    sqlcommand.CommandText = QUERY;
                    sqlcommand.Connection = con;
                    sqlcommand.CommandTimeout = 3600;
                    SqlDataAdapter da = new SqlDataAdapter(sqlcommand);

                    da.Fill(dtExport);

                }


                string FilePath = AppDomain.CurrentDomain.BaseDirectory + "STX_Export_ClientAllocation";
                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }
                if (System.IO.File.Exists(FilePath + "\\stx_ClientAllocation_EOD_" + DateTime.Now.ToString("ddMMyyyy") + ".csv"))
                {
                    System.IO.File.Delete(FilePath + "\\stx_ClientAllocation_EOD_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                }
                Helper.ToCSV(dtExport, FilePath + "\\stx_ClientAllocation_EOD_" + DateTime.Now.ToString("ddMMyyyy") + ".csv");
                MessageBox.Show("File Export Successfuly");
            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message.ToString());
            }
        }
    }
}
