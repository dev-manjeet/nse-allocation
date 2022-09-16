using Newtonsoft.Json;
using NSEAllocation.Models;
using Oracle.ManagedDataAccess.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSEAllocation
{
    public partial class FrmStatusEnquiry : Form
    {
        public string Token { get; set; }
        public string Nonce { get; set; }
        public bool isSuccess = false;
        DataTable dtfILTERdATEwISE = new DataTable();
        DataTable dtEnqstatus = new DataTable();
        public FrmStatusEnquiry()
        {
            InitializeComponent();
            panelwaitshow(false);
        }
        public string GetToken()
        {
            string token = "";
            try
            {
                string Url = ConfigurationManager.AppSettings["Url"].ToString();
                ClsCIMAllocation cls = new ClsCIMAllocation();
                Nonce = cls.GetNonse();
                string Auth = cls.GetAuthorization();
                TokenResp Resp = new TokenResp();

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + "/coll-token");
                // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("nonce", Nonce);
                request.Headers.Add("cache-control", "no-cache");
                //request.Headers.Add("content-type", "application/x-www-form-urlencoded");
                request.Headers.Add("authorization", "Basic " + Auth);

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string appJson = "grant_type=client_credentials";

                    streamWriter.Write(appJson);
                }

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    StreamReader streamReader = new StreamReader(webStream);
                    string json = streamReader.ReadToEnd();
                    Resp = JsonConvert.DeserializeObject<TokenResp>(json);
                }

                token = Resp.access_token;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return token = "";
            }
            return token;
        }
        private void FrmStatusEnquiry_Load(object sender, EventArgs e)
        {
            if (Token == null)
            {
                MessageBox.Show("Token is not Generate. Please Try Again");
                return;
            }
            comboBox1.Items.Add("CM");
            comboBox1.Items.Add("FO");
            comboBox1.Items.Add("CD");
            comboBox1.Items.Add("CO");
            comboBox1.Items.Add("DT");
            comboBox1.Items.Add("SLB");
            comboBox1.Items.Add("TPR");
            comboBox1.Items.Add("OFS");
            comboBox1.SelectedIndex = 1;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchParty.Text == "")
                {
                    MessageBox.Show("Please Enter Party Code");
                    return;
                }
                Thread threadInput = new Thread(SearchData);
                threadInput.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchData()
        {
            SetLoading(true);
            DataTable dt = new DataTable();

            string xx = "select MSGID,REQUESTTYPE,CPCODE,CLICODE,AMT, ";
            xx += " RESPSTATUS,RESPFEILDMSG,RESPVALIDATIONMSG from AllocationReqData where trim(cliCode)=:cliCode";

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
            {
                conn.Open();


                OracleCommand cmd = new OracleCommand(xx, conn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(":cliCode", OracleDbType.Char).Value = txtSearchParty.Text.ToUpper().Trim().ToString();

                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                adp.Fill(dt);
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        //if (dt.rows.count > 0)
                        //{
                        dtgrdSearch.DataSource = dt;
                        //excelexport(dt); //working for export csv file.
                        //}
                    }));
                }

            }
            SetLoading(false);
        }

        private void ExcelExport(DataTable dt)
        {
            try
            {
                //var Date = dat.Text;
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = "AllocationEnquire";
                //savefile.Filter = "CSV file (*.csv)|*.csv";
                savefile.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
                if (dt.Rows.Count > 0)
                {
                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter wr = new StreamWriter(savefile.FileName);
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            wr.Write(dt.Columns[i]);
                            if (i < dt.Columns.Count - 1)
                            {
                                wr.Write(",");
                            }
                        }

                        wr.WriteLine();
                        //write rows to csv file
                        foreach (DataRow dr in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (!Convert.IsDBNull(dr[i]))
                                {
                                    string value = dr[i].ToString();
                                    if (value.Contains(','))
                                    {
                                        value = String.Format("\"{0}\"", value);
                                        wr.Write(value);
                                    }
                                    else
                                    {
                                        wr.Write(dr[i].ToString());
                                    }
                                }
                                if (i < dt.Columns.Count - 1)
                                {
                                    wr.Write(",");
                                }
                            }
                            wr.Write(wr.NewLine);
                        }
                        wr.Close();
                        MessageBox.Show(this, "Data saved in Excel format at location " + savefile.FileName, "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Zero record to export , perform a operation first", "Can't export file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //el.LogError(ex);
            }
        }

        private void btnStausEnqApi_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    if (Token == null)
            //    {
            //        MessageBox.Show("Token is not Generate. Please Try Again");
            //        return;
            //    }
            //    if (txtStatusEnq.Text.Trim() == "")
            //    {
            //        MessageBox.Show("Please Insert MessageID");
            //        return;

            //    }
            //    if (txtStatusEnq.Text.Length < 6)
            //    {
            //        MessageBox.Show("MessageID must be at 20 characters long.");
            //        return /*false*/;
            //    }
            //    Thread threadInput = new Thread(GetEnquireStatus);
            //    threadInput.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    EnqSetLoading(false);
            //}

        }
        private void GetEnquireStatus()
        {
            try
            {
               

                if (dtEnqstatus.Rows.Count > 0)
                {
                    //panelwaitshow.Visible = true;
                    //EnqSetLoading(true);
                    DataTable dt1= dtEnqstatus.DefaultView.ToTable(true, "msgid");
                    int COUNT = 0;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (Token.ToString() == "")
                        {
                            Token = GetToken();
                        }
                        DataTable dtMessageMater = GetMessageMaster();
                        ClsEnquiryStatusResp Resp = new ClsEnquiryStatusResp();

                        DataEnq data = new DataEnq()
                        {
                            msgId = dt1.Rows[i][0].ToString().Trim(),
                            dataFormat = "json"

                        };
                        ClsEnquiryStatusReq _Req = new ClsEnquiryStatusReq()
                        {
                            version = "1.0",
                            data = data
                        };


                        string JSON = JsonConvert.SerializeObject(_Req);
                        string Url = ConfigurationManager.AppSettings["Url"].ToString();
                        #region 
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + "/coll-allocation/allocation-statusInquiry");
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        //request.Method = "POST";
                        //request.ContentType = "application/x-www-form-urlencoded";
                        //request.Headers.Add("nonce", Nonce);
                        //request.Headers.Add("cache-control", "no-cache");
                        ////request.Headers.Add("content-type", "application/x-www-form-urlencoded");
                        //request.Headers.Add("Authorization", "Bearer " + Token);

                        //request.ContentLength = JSON.Length;
                        //using (Stream webStream = request.GetRequestStream())
                        //using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
                        //{
                        //    requestWriter.Write(JSON);
                        //}

                        //WebResponse webResponse = request.GetResponse();
                        //using (Stream webStream = webResponse.GetResponseStream())
                        //{
                        //    StreamReader streamReader = new StreamReader(webStream);
                        //    string json = streamReader.ReadToEnd();
                        //    Resp = JsonConvert.DeserializeObject<ClsEnquiryStatusResp>(json);
                        //}
                        #endregion
                        var client = new RestClient(Url + "/coll-allocation/allocation-statusInquiry");
                        //client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Authorization", "Bearer " + Token);
                        request.AddHeader("nonce", Nonce);

                        request.AddParameter("application/json", JSON, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        if (response.StatusDescription == "Internal Server Error")
                        {

                            MessageBox.Show("Internal Server Error.");
                            //panelwaitshow.Visible = false;
                            return;

                        }
                        else if (response.StatusDescription.Contains("An error occurred while processing your request.") == true)
                        {
                            MessageBox.Show("An error occurred while processing your request.");
                            //panelwaitshow.Visible = false;
                            return;
                        }
                        else if (response.StatusDescription == "Service Unavailable")
                        {

                            MessageBox.Show("Service Unavailable.");
                            //panelwaitshow.Visible = false;
                            return;
                        }
                        else if (response.StatusDescription == "Gateway Time-out")
                        {

                            MessageBox.Show("Gateway Time-out");
                            //panelwaitshow.Visible = false;
                            return;
                        }
                        else if (response.StatusDescription == "Unauthorized")
                        {

                            MessageBox.Show("Unauthorized");
                            //panelwaitshow.Visible = false;
                            return;
                        }
                        else if (response.Content.Contains("Invalid date"))
                        {

                            MessageBox.Show("Invalid date.");
                            //panelwaitshow.Visible = false;
                            return;
                        }
                        else
                        {
                            Resp = JsonConvert.DeserializeObject<ClsEnquiryStatusResp>(response.Content);


                        }
                        if (Resp.status == null)
                        {

                            MessageBox.Show(" Not Response.");
                            //panelwaitshow.Visible = false;
                            return;
                        }
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {

                            //bool IsExit = IsExitMsgid(txtStatusEnq.Text.Trim());
                            bool IsExit = IsExitMsgid(dt1.Rows[i][0].ToString().Trim());
                            if (IsExit == true)
                            {
                                conn.Open();
                                for (int l = 0; l < Resp.data.inquiryResponse.Count; l++)
                                {

                                    string FeildMsg = Resp.messages.code.Substring(0, 4);
                                    string validMsg = Resp.messages.code.Substring(4, 4);
                                    DataRow[] dr = dtMessageMater.Select("TYPE='F' and ERRORCODE=" + FeildMsg + "");
                                    DataRow[] drV = dtMessageMater.Select("TYPE='V' and ERRORCODE=" + validMsg + "");
                                    string MsgValid = "";
                                    string MsgFeild = "";
                                    if (dr.Length != 0)
                                    {
                                        MsgFeild = Convert.ToString(dr[0]["ERRDESCRIPTION"].ToString()).ToString();
                                    }
                                    if (drV.Length != 0)
                                    {
                                        MsgValid = Convert.ToString(drV[0]["ERRDESCRIPTION"].ToString()).ToString();
                                    }
                                   
                                    string query = "insert into ALLOCENQSTATUSDATA (STATUS,MESSAGESCODE,MSGID,CURDATE," +
                                        " SEG,CMCODE,TMCODE,CPCODE,CLICODE,ACCTYPE,AMT,ACTION,ERRCD,ERRCDMSG,RESPFEILDMSG,RESPVALIDATIONMSG)" +
                                        "values(:STATUS,:MESSAGESCODE,:MSGID,:CURDATE,:SEG,:CMCODE,:TMCODE,:CPCODE,:CLICODE,:ACCTYPE,:AMT" +
                                        ",:ACTION,:ERRCD,:ERRCDMSG,:RESPFEILDMSG,:RESPVALIDATIONMSG)";
                                    OracleCommand cmd = new OracleCommand(query, conn);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.Add(":STATUS", OracleDbType.Varchar2).Value = Resp.status.ToString();
                                    cmd.Parameters.Add(":MESSAGESCODE", OracleDbType.Varchar2).Value = Resp.messages.code.ToString();
                                    cmd.Parameters.Add(":MSGID", OracleDbType.Varchar2).Value = _Req.data.msgId.ToString();
                                    cmd.Parameters.Add(":CURDATE", OracleDbType.Varchar2).Value = Resp.data.inquiryResponse[l].curDate.ToString();
                                    cmd.Parameters.Add(":SEG", OracleDbType.Char).Value = Resp.data.inquiryResponse[l].segment.ToString();
                                    cmd.Parameters.Add(":CMCODE", OracleDbType.Char).Value = Resp.data.inquiryResponse[l].cmCode.ToString();
                                    cmd.Parameters.Add(":TMCODE", OracleDbType.Char).Value = Resp.data.inquiryResponse[l].tmCode.ToString();
                                    cmd.Parameters.Add(":CPCODE", OracleDbType.Char).Value = Resp.data.inquiryResponse[l].cpCode.ToString();
                                    cmd.Parameters.Add(":CLICODE", OracleDbType.Char).Value = Resp.data.inquiryResponse[l].cliCode.ToString();
                                    cmd.Parameters.Add(":ACCTYPE", OracleDbType.Char).Value = Resp.data.inquiryResponse[l].accType.ToString();
                                    cmd.Parameters.Add(":AMT", OracleDbType.Double).Value = Convert.ToDouble(Resp.data.inquiryResponse[l].amt);
                                    cmd.Parameters.Add(":ACTION", OracleDbType.Char).Value = Resp.data.inquiryResponse[l].action.ToString();
                                    cmd.Parameters.Add(":ERRCD", OracleDbType.Varchar2).Value = Resp.data.inquiryResponse[l].errCd.ToString();
                                    cmd.Parameters.Add(":ERRCDMSG", OracleDbType.Varchar2).Value = "";
                                    cmd.Parameters.Add(":RESPFEILDMSG", OracleDbType.Varchar2).Value = MsgFeild;
                                    cmd.Parameters.Add(":RESPVALIDATIONMSG", OracleDbType.Varchar2).Value = MsgValid;

                                    int J = Convert.ToInt32(cmd.ExecuteNonQuery());
                                    COUNT = COUNT + J;

                                }
                            }


                        }
                    }
                    MessageBox.Show("TOTAL Record update = " + COUNT);
                    //panelwaitshow.Visible = false;
                }
                else
                {
                    MessageBox.Show("No Records found .");
                    //panelwaitshow.Visible = false;
                }


                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        GridBindEnqStatus();
                    }));
                }

            }
            catch (Exception EX)
            {
                //panelwaitshow.Visible = false;
                MessageBox.Show(EX.Message.ToString());

            }
           // EnqSetLoading(false);
        }

        public void GridBindEnqStatus()
        {
            string xx = "select T1.msgid,T1.curdate,T1.seg,T1.cmcode,T1.clicode,T1.acctype,T1.AMT,T1.ACTION,T1.source," +
                        " T1.Respmessagescode,T1.respvalidationmsg,T2.ERRCD,T2.ERRCDMSG" +
                        " from ALLOCATIONREQDATA T1" +
                        " LEFT JOIN ALLOCENQSTATUSDATA T2 ON T1.msgid=T2.msgid AND T1.seg=T2.seg and T1.clicode=T2.clicode" +
                        "  and T1.acctype=T2.acctype and T1.AMT=T2.AMT AND T1.ACTION=T2.ACTION  where to_date(T1.createdatetime,'DD-MM-YY')" +
                        " between to_date(:fromDate,'DD-MM-YY') AND to_date(:ToDate,'DD-MM-YY')" +
                        "and TRIM(T1.Seg)=:Seg";


            dtfILTERdATEwISE = new DataTable();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(xx, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(":fromDate", OracleDbType.Char).Value = frmdate.Text;
                cmd.Parameters.Add(":ToDate", OracleDbType.Char).Value = todate.Text;
                cmd.Parameters.Add(":Seg", OracleDbType.Varchar2).Value = comboBox1.Text;
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                adp.Fill(dtfILTERdATEwISE);
                if (dtfILTERdATEwISE.Rows.Count > 0)
                {
                    grdSerachFilter.DataSource = dtfILTERdATEwISE;

                }
                else
                {
                    grdSerachFilter.DataSource = dtfILTERdATEwISE;
                }
            }
        }

        public DataTable GetMessageMaster()
        {
            string xx = "select * from ALLOCERRDESC ";
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(xx, conn);
                cmd.CommandType = CommandType.Text;
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                adp.Fill(dt);
                return dt;
            }
        }
       public void panelwaitshow(bool n)
        {
            pnlWait.Visible = n;
            lblTitle.Visible = n;
            progressBar1.Visible = n;
        }

        public bool IsExitMsgid(string Msgid)
        {
            bool Result = false;
            string xx = "select MSGID from AllocationReqData where trim(MSGID)=:MSGID ";
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(xx, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(":MSGID", OracleDbType.Varchar2).Value = Msgid.Trim().ToUpper().ToString();
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Result = true;
                }
                return Result;
            }
        }

        private void SetLoading(bool displayLoader)
        {
            if (displayLoader)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    pictureBox1.Visible = true;
                    // this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    pictureBox1.Visible = false;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
        }
        private void EnqSetLoading(bool displayLoader)
        {
            if (displayLoader)
            {

                this.Invoke((MethodInvoker)delegate
                {
                    pictureBox3.Visible = true;
                    // this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
            else
            {

                this.Invoke((MethodInvoker)delegate
                {
                    pictureBox3.Visible = false;
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                });
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            //EnqSetLoading(true);
            panelwaitshow(true);

            string xx = "select T1.msgid,T1.curdate,T1.seg,T1.cmcode,T1.clicode,T1.acctype,T1.AMT,T1.ACTION,T1.source," +
                         " T1.Respmessagescode,T1.respvalidationmsg,T2.ERRCD,T3.ERRDESCRIPTION" +
                         " from ALLOCATIONREQDATA T1" +
                         " LEFT JOIN ALLOCENQSTATUSDATA T2 ON T1.msgid=T2.msgid AND T1.seg=T2.seg and T1.clicode=T2.clicode" +
                         "  and T1.acctype=T2.acctype and T1.AMT=T2.AMT AND T1.ACTION=T2.ACTION" +
                         " LEFT JOIN ALLOCERRDESC T3 ON TRIM(T3.ERRORCODE)=SUBSTR(TRIM(T2.ERRCD),5,4) AND T3.TYPE='V'" +
                         "  where to_date(T1.createdatetime,'DD-MM-YY')" +
                         " between to_date(:fromDate,'DD-MM-YY') AND to_date(:ToDate,'DD-MM-YY')" +
                         "and TRIM(T1.Seg)=:Seg";


            dtfILTERdATEwISE = new DataTable();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(xx, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(":fromDate", OracleDbType.Char).Value = frmdate.Text;
                cmd.Parameters.Add(":ToDate", OracleDbType.Char).Value = todate.Text;
                cmd.Parameters.Add(":Seg", OracleDbType.Varchar2).Value = comboBox1.Text;
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                adp.Fill(dtfILTERdATEwISE);
                if (dtfILTERdATEwISE.Rows.Count > 0)
                {
                    //dtfILTERdATEwISE.Columns.Remove("client");
                    //dtfILTERdATEwISE.AcceptChanges();
                    grdSerachFilter.DataSource = dtfILTERdATEwISE;

                }
                else
                {
                    grdSerachFilter.DataSource = dtfILTERdATEwISE;
                }
            }
            panelwaitshow(false);
            //EnqSetLoading(false);
        }

        private void btnStausEnqApi_Click_1(object sender, EventArgs e)
        {

            try
            {
                panelwaitshow(true);
                string xx = "select T1.msgid,T1.curdate,T1.seg,T1.cmcode,T1.clicode,T1.acctype,T1.AMT,T1.ACTION,T1.source," +
                         " T1.Respmessagescode,T1.respvalidationmsg,T2.ERRCD,T3.ERRDESCRIPTION" +
                         " from ALLOCATIONREQDATA T1" +
                         " LEFT JOIN ALLOCENQSTATUSDATA T2 ON T1.msgid=T2.msgid AND T1.seg=T2.seg and T1.clicode=T2.clicode" +
                         "  and T1.acctype=T2.acctype and T1.AMT=T2.AMT AND T1.ACTION=T2.ACTION" +
                         " LEFT JOIN ALLOCERRDESC T3 ON TRIM(T3.ERRORCODE)=SUBSTR(TRIM(T2.ERRCD),5,4) AND T3.TYPE='V'" +
                         "  where to_date(T1.createdatetime,'DD-MM-YY')" +
                         " between to_date(:fromDate,'DD-MM-YY') AND to_date(:ToDate,'DD-MM-YY')" +
                         "  and TRIM(T1.Seg)=:Seg AND trim(T2.ERRCD)IS NULL";


                dtEnqstatus = new DataTable();
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand(xx, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(":fromDate", OracleDbType.Char).Value = frmdate.Text;
                    cmd.Parameters.Add(":ToDate", OracleDbType.Char).Value = todate.Text;
                    cmd.Parameters.Add(":Seg", OracleDbType.Varchar2).Value = comboBox1.Text;
                    OracleDataAdapter adp = new OracleDataAdapter(cmd);
                    adp.Fill(dtEnqstatus);
                }


                Thread threadInput = new Thread(GetEnquireStatus);
                threadInput.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //EnqSetLoading(false);
                panelwaitshow(false);
            }
            panelwaitshow(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //EnqSetLoading(true);
            try
            {
                panelwaitshow(true);
                string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Export_Allocation_csv";
                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }
                if (System.IO.File.Exists(FilePath + "\\" + Convert.ToDateTime(frmdate.Text).ToString("ddMMyyyy") + "_" + comboBox1.Text + ".csv"))
                {
                    System.IO.File.Delete(FilePath + "\\" + Convert.ToDateTime(frmdate.Text).ToString("ddMMyyyy") + "_" + comboBox1.Text + ".csv");
                }
                string xx = "select T1.curdate,T1.seg,T1.CMcode,T1.TMcode,T1.filler1,T1.clicode,T1.acctype," +
                            "T1.AMT,T1.filler1," +
                            "T1.filler2,T1.filler3,T1.filler4,T1.filler5,T1.filler6,T1.ACTION,T2.ERRCD," +
                         " T1.Respmessagescode,T1.respvalidationmsg,T2.ERRCD,T3.ERRDESCRIPTION" +
                         " from ALLOCATIONREQDATA T1" +
                         " LEFT JOIN ALLOCENQSTATUSDATA T2 ON T1.msgid=T2.msgid AND T1.seg=T2.seg and T1.clicode=T2.clicode" +
                         "  and T1.acctype=T2.acctype and T1.AMT=T2.AMT AND T1.ACTION=T2.ACTION  " +
                         " LEFT JOIN ALLOCERRDESC T3 ON TRIM(T3.ERRORCODE)=SUBSTR(TRIM(T2.ERRCD),5,4) AND T3.TYPE='V'" +
                         "where to_date(T1.createdatetime,'DD-MM-YY')" +
                         " between to_date(:fromDate,'DD-MM-YY') AND to_date(:ToDate,'DD-MM-YY')" +
                         "and TRIM(T1.Seg)=:Seg";


                DataTable dtexport = new DataTable();
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand(xx, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(":fromDate", OracleDbType.Char).Value = frmdate.Text;
                    cmd.Parameters.Add(":ToDate", OracleDbType.Char).Value = todate.Text;
                    cmd.Parameters.Add(":Seg", OracleDbType.Varchar2).Value = comboBox1.Text;
                    OracleDataAdapter adp = new OracleDataAdapter(cmd);
                    adp.Fill(dtexport);

                }
                //dtexport.Columns.Remove("client");
                //dtexport.AcceptChanges();
                Helper.ToCSV(dtexport, FilePath + "\\" + Convert.ToDateTime(frmdate.Text).ToString("ddMMyyyy") + "_" + comboBox1.Text + ".csv");
                MessageBox.Show("File Export Successfuly");

            }
            catch (Exception ex)
            {
                panelwaitshow(false);
                MessageBox.Show(ex.Message.ToString());
            }
            panelwaitshow(false);
        }
    }
}
