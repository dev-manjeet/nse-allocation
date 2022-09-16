
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;
using Newtonsoft.Json;
using NSEAllocation.Models;

using Oracle.ManagedDataAccess.Client;
using System.Configuration;
//using System.IO.Compression.FileSystem;


namespace NSEAllocation
{
    public partial class FrmConvertHoldingCsv : Form
    {
        public FrmConvertHoldingCsv()
        {
            InitializeComponent();
        }
        private void FrmConvertHoldingCsv_Load(object sender, EventArgs e)
        {
            cmbSeq.Items.Add("01");
            cmbSeq.Items.Add("02");
            cmbSeq.Items.Add("03");
            cmbSeq.Items.Add("04");
            cmbSeq.Items.Add("05");
            cmbSeq.Items.Add("06");
        }
       
        public bool CreateUnZip(string SourcePath, string DesinationPath)
        {

            try
            {
                string zPath = @"C:\Program Files\7-Zip\7zG.exe";
                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = zPath;
                pro.Arguments = "x \"" + SourcePath + "\" -o" + DesinationPath;
                Process x = Process.Start(pro);
                x.WaitForExit();
                return true;
            }
            catch (Exception)
            {

                return true;
            }
        }

        private void btnConvertCSV_Click(object sender, EventArgs e)
        {
            try
            {
                if(cmbSeq.Text=="")
                {
                    MessageBox.Show("Please Select File Sequence no");
                    return;
                }
                DataTable dt = new DataTable();

                string xx = "select t1.MEMBERPAN,t1.WEEKDAY,t1.LASTWEEKDAY,t1.Dmat,t1.AccountType,t1.UCC,t1.clientName,t1.PAN,t1.isin,t1.securityType, ";
                xx += " t1.nameOfCommodity,t1.unitType,t1.totalPldgQty,t1.freeBalQty,t1.totalQty,t2.HDRCODE,t2.ACKID,t2.HDRSTATUS,t3.Descrption  ";
                xx += " from NSE_HOLDING_UPLOAD_REQ t1 left join NSE_HOLDING_UPLOAD_RESP t2 on t1.WEEKDAY=t2.WEEKDAY " +
                    " and t1.LASTWEEKDAY=t2.LASTWEEKDAY and t1.FILESEQUNCENO=t2.FILESEQUNCENO and  TO_DATE(t1.REQ_CREATEDATETIME,'DD-MM-YY')= TO_DATE(t2.RESP_CREATEDATETIME,'DD-MM-YY')" +
                    " and t1.RECID=t2.RECID LEFT JOIN NSE_cir_Master_Code_Des t3 on trim(t2.HDRCODE)=trim(t3.Code)   where trim(t1.FILESEQUNCENO)=:FILESEQUNCENO and trim(t1.LASTWEEKDAY)=:LASTWEEKDAY";
                xx += " and trim(t1.WEEKDAY)=:WEEKDAY  and TO_DATE(t1.REQ_CREATEDATETIME,'DD-MM-YY')=TO_DATE(:REQ_CREATEDATETIME,'DD-MM-YY')";

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                {
                    conn.Open();


                    OracleCommand cmd = new OracleCommand(xx, conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(":FILESEQUNCENO", OracleDbType.Char).Value = cmbSeq.Text;
                    cmd.Parameters.Add(":LASTWEEKDAY", OracleDbType.Char).Value = lastweekdate.Text;
                    cmd.Parameters.Add(":WEEKDAY", OracleDbType.Char).Value = weekdate.Text;
                    
                    cmd.Parameters.Add(":REQ_CREATEDATETIME", OracleDbType.Date).Value = Convert.ToDateTime(dateTimePicker1.Text);
                    OracleDataAdapter adp = new OracleDataAdapter(cmd);
                    adp.Fill(dt);
                }
                if(dt.Rows.Count>0)
                {
                    string FileName= "Response_"+dt.Rows[0]["MEMBERPAN"].ToString()+"_HS_"+dt.Rows[0]["LASTWEEKDAY"].ToString().Replace("-","")+".CSV";
                    
                    string ResponseFileSaveUnzip = AppDomain.CurrentDomain.BaseDirectory + "HoldingFileResponseUnzip";
                    if (!System.IO.Directory.Exists(ResponseFileSaveUnzip))
                    {
                        System.IO.Directory.CreateDirectory(ResponseFileSaveUnzip);
                    }
                    if (System.IO.File.Exists(ResponseFileSaveUnzip + "/" + FileName))
                    {
                        System.IO.File.Delete(ResponseFileSaveUnzip + "/" + FileName);
                    }
                    string str = Helper.ToCsv(dt);
                    //byte[] bContent = Encoding.Default.GetBytes(str);
                    using (StreamWriter sw = new StreamWriter(ResponseFileSaveUnzip+"/"+ FileName))
                    {
                        sw.WriteLine(str);
                    }
                    MessageBox.Show("Successfuly Created csv");
                }
                else
                {
                    MessageBox.Show("No Record Found");
                }
                }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
