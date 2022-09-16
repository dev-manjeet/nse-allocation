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
    public partial class FrmAllocation : Form
    {
        public string Token { get; set; }
        public string Nonce { get; set; }
        public FrmAllocation()
        {
            InitializeComponent();
            pnlWait.Visible = false;
            this.Load += Main_Load;
            common.CommonFun();
            ControlEnableDisable(true);

        }
        private void Main_Load(object sender, EventArgs e)
        {
            cmbSegment.Items.Add("CM");
            cmbSegment.Items.Add("FO");
            cmbSegment.Items.Add("CD");
            cmbSegment.Items.Add("CO");
            cmbSegment.Items.Add("DT");
            cmbSegment.Items.Add("SLB");
            cmbSegment.Items.Add("TPR");
            cmbSegment.Items.Add("OFS");
            cmbSegment.SelectedIndex = 0;

            cmbActionType.Items.Add("");
            cmbActionType.Items.Add("U");
            cmbActionType.Items.Add("D");

            cmbfilActionType.Items.Add("ALL");
            cmbfilActionType.Items.Add("U");
            cmbfilActionType.Items.Add("D");
            label4.Text = "";
            label7.Text = "";
            btnNseUpload.Visible = false;
            datecc02picker.Text = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
            panel2.Visible = false;
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
        public void ControlEnableDisable(bool i)
        {
            frmdatepicker.Enabled = i;
            cmbSegment.Enabled = i;
            btnAllocation.Enabled = i;
            btnStatusEnquiry.Enabled = i;
            cmbActionType.Enabled = i;
            txtparty.Enabled = i;
            txtAmnt.Enabled = i;
            // btnNseUpload.Visible = false;
        }
        private void FrmAllocation_Load(object sender, EventArgs e)
        {
            if (Token == null)
            {
                //Token = GetToken();
            }

            try
            {
                pnlWait.Visible = false;


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void btnAllocation_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible=true;
            BringToFront();

        }
        public void Submit_AllocationAPI()
        {
            DialogResult dialog = MessageBox.Show("Are you Sure do you want Post Data to Allocation ?", "", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No)
            {
                return;
            }
            String Source = "";
            if (Token == null)
            {
                Token = GetToken();
            }
            StringBuilder strbulder = new StringBuilder();
            string fileDate = Convert.ToString(DateTime.Now.ToString("ddMMyyyyHHmmss"));
            string file = AppDomain.CurrentDomain.BaseDirectory + "AllocationFile";
            if (!System.IO.Directory.Exists(file))
            {
                System.IO.Directory.CreateDirectory(file);
            }
            pnlWait.Visible = true;
            lblTitle.Visible = true;

            ControlEnableDisable(false);

            int MsgSuccess = 0;
            int MsgError = 0;
            ClsAllocationResp Resp = new ClsAllocationResp();
            ClsCIMAllocation cls = new ClsCIMAllocation();
            string memid = ConfigurationManager.AppSettings["MEMCODE"].ToString();//"07714";
            DataTable dt = new DataTable();
            string xx = "";


            try
            {
                string Valid = "";
                if (txtAmnt.Text == "")
                {
                    Valid += "1";

                }

                if (cmbActionType.Text == "")
                {
                    Valid += "2";
                    //MessageBox.Show("Please fill Amount");
                    //txtAmnt.Focus();
                    //return;
                }

                if ((Valid != "12") && (Valid != ""))
                {
                    pnlWait.Visible = false;
                    ControlEnableDisable(true);
                    MessageBox.Show("Please fill all feild ");
                    if (!Valid.Contains("1") == true)
                    {
                        txtAmnt.Focus();
                    }
                    //if (!Valid.Contains("2") == true)
                    //{
                    //    txtparty.Focus();
                    //}
                    if (!Valid.Contains("2") == true)
                    {
                        cmbActionType.Focus();
                    }
                    return;
                }
                if (cmbSegment.Text == "CM") //CM,FO,CD
                {
                    if (Valid == "12")
                    {
                        xx = "SELECT MAX(TmYn) TmYn,Party ,Max(TYP) TYP,MAX(UCC) ucc,SUM(Funds)-SUM(FundsP) DifferentFunds,SUM(Funds) " +
                           " Funds,SUM(FundsP) FundsP,max(Account_type) Account_type,Max(TmCode) TmCode,  " +
                           " case when SUM(Funds)-SUM(FundsP)< 0 then 'D' else 'U' end action FROM(" +
                           " SELECT TmYn,party, CASE WHEN GROUP1='IF98' THEN 'P' ELSE 'C' END TYP,A.Account_type, UccN UCC," +
                           " ' ' TmCode, FundsN Funds,0  FundsP " +
                           " from Allocation01 A " +
                           " LEFT OUTER JOIN SuMast01 On SuMAst01.account=A.Party" +
                           " WHERE A.TrDate=TO_DATE(:fromdate,'DD-MM-YYYY') AND SUBSTR(PARAM,6,1) NOT IN ('5')AND GROUP1<>'IF98'" +
                           " AND SUBSTR(PARAM,6,1) NOT IN ('5') AND GROUP1<>'IF98'" +
                           " AND TRIM(UccN) IS NOT NULL" +
                           " UNION ALL" +
                           " SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ ' ' TmYn,B.party,'C' TYP,' ' Account_type, B.ABNM UCC, ' ' TmCode," +
                           " 0 Funds,B.ALLOCATION  FundsP from CC0201 B" +
                           " LEFT OUTER JOIN (SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ MAX(TRDATE) TRDATE,PARTY,EXCD FROM CC0201 WHERE TRIM(ABNM)  " +
                           " IS NOT NULL AND TRIM(TMCODE)=TRIM('07714') AND EXCD='N' GROUP BY PARTY,EXCD)A" +
                           " ON B.PARTY=A.PARTY AND B.EXCD=A.EXCD AND B.TRDATE=A.TRDATE WHERE TRIM(B.ABNM) IS NOT NULL AND  " +
                           " TRIM(B.TMCODE)=TRIM('07714') AND TRIM(B.TMCODE)<>TRIM(B.ABNM) AND B.TRDATE=TO_DATE(:CC02date,'DD-MM-YYYY')" +
                           " AND B.ABNM NOT LIKE 'PRO_%' AND B.EXCD='N' )Final Group By Party having (SUM(Funds)<>SUM(FundsP)) Order By 5";
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();


                            OracleCommand cmd = new OracleCommand(xx, conn);
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(":fromdate", OracleDbType.Char).Value = Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MM-yyyy");
                            cmd.Parameters.Add(":CC02date", OracleDbType.Char).Value = Convert.ToDateTime(datecc02picker.Text).ToString("dd-MM-yyyy");
                            OracleDataAdapter adp = new OracleDataAdapter(cmd);
                            adp.Fill(dt);
                            Source = "Focus";
                        }
                    }
                    else
                    {

                        // xx = "select uccn party,fundsn fund,ACCOUNT_TYPE from allocation01 where trdate=TO_DATE(:fromdate,'DD-MM-YYYY') " +
                        //     "and trim(uccn)='" + txtparty.Text + "' ";
                        dt = new DataTable();
                        dt.Columns.Add("party", typeof(string));
                        dt.Columns.Add("fund", typeof(string));
                        dt.Columns.Add("ACCOUNT_TYPE", typeof(string));
                        dt.Rows.Add(txtparty.Text, txtAmnt.Text, txtparty.Text.Trim() != "" ? "C" : "P");
                        Source = "Manual";
                    }



                }
                else if (cmbSegment.Text == "FO") //CM,FO,CD
                {
                    if (Valid == "12")
                    {
                        xx = "SELECT MAX(TmYn) TmYn,Party ,Max(TYP) TYP,MAX(UCC) ucc,SUM(Funds)-SUM(FundsP) DifferentFunds,SUM(Funds)Funds,SUM(FundsP) FundsP, " +
                           " max(Account_type) Account_type,Max(TmCode) TmCode, case when SUM(Funds)-SUM(FundsP)< 0 then 'D' else 'U' end action" +
                           " FROM( SELECT TmYn,party, CASE WHEN GROUP1='IF98' THEN 'P' ELSE 'C'" +
                           "  END TYP,A.Account_type, UccZ UCC, ' ' TmCode, FundsZ Funds,0  FundsP " +
                           " from Allocation01 A" +
                           " LEFT OUTER JOIN SuMast01 On SuMAst01.account=A.Party WHERE A.TrDate=TO_DATE(:fromdate,'DD-MM-YYYY')" +
                           " AND SUBSTR(PARAM,6,1) NOT IN ('5') AND GROUP1<>'IF98'" +
                           " AND TRIM(UccZ) IS NOT NULL" +
                           " UNION ALL" +
                           " SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ ' ' TmYn,B.party, 'C' TYP,' ' Account_type, B.ABNM UCC, ' ' TmCode," +
                           " 0 Funds,B.ALLOCATION  FundsP from CC0201 B" +
                           " LEFT OUTER JOIN (SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ MAX(TRDATE) TRDATE,PARTY,EXCD FROM CC0201 WHERE TRIM(ABNM) " +
                           " IS NOT NULL AND TRIM(TMCODE)=TRIM('07714') AND EXCD='Z' GROUP BY PARTY,EXCD)A" +
                           " ON B.PARTY=A.PARTY AND B.EXCD=A.EXCD AND B.TRDATE=A.TRDATE WHERE TRIM(B.ABNM) IS NOT NULL AND TRIM(B.TMCODE)=TRIM('07714') AND TRIM(B.TMCODE)<>TRIM(B.ABNM) AND " +
                           " B.TRDATE=TO_DATE(:CC02date,'DD-MM-YYYY') AND B.ABNM NOT LIKE 'PRO_%' AND B.EXCD='Z' )Final Group By Party" +
                           " having (SUM(Funds)<>SUM(FundsP))Order By 5";
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();


                            OracleCommand cmd = new OracleCommand(xx, conn);
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(":fromdate", OracleDbType.Char).Value = Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MM-yyyy");
                            cmd.Parameters.Add(":CC02date", OracleDbType.Char).Value = Convert.ToDateTime(datecc02picker.Text).ToString("dd-MM-yyyy");
                            OracleDataAdapter adp = new OracleDataAdapter(cmd);
                            adp.Fill(dt);


                            Source = "Focus";
                        }
                    }
                    else
                    {

                        // xx = "select uccn party,fundsn fund,ACCOUNT_TYPE from allocation01 where trdate=TO_DATE(:fromdate,'DD-MM-YYYY') " +
                        //     "and trim(uccn)='" + txtparty.Text + "' ";
                        dt = new DataTable();
                        dt.Columns.Add("party", typeof(string));
                        dt.Columns.Add("fund", typeof(string));
                        dt.Columns.Add("ACCOUNT_TYPE", typeof(string));
                        dt.Rows.Add(txtparty.Text, txtAmnt.Text, txtparty.Text.Trim() != "" ? "C" : "P");
                        Source = "Manual";
                    }



                }
                else if (cmbSegment.Text == "CD") //CM,FO,CD
                {
                    if (Valid == "12")
                    {
                        xx = "SELECT MAX(TmYn) TmYn,Party ,Max(TYP) TYP,MAX(UCC) ucc,SUM(Funds)-SUM(FundsP)DifferentFunds,SUM(Funds)Funds,SUM(FundsP) FundsP," +
                            " max(Account_type) Account_type,Max(TmCode) TmCode,  case when SUM(Funds)-SUM(FundsP)< 0 then 'D' else 'U' end action" +
                            " FROM( SELECT TmYn,party, CASE WHEN GROUP1='IF98' THEN 'P' ELSE 'C'" +
                            " END TYP,A.Account_type, UccC UCC, ' ' TmCode, FundsC Funds,0  FundsP from Allocation01 A" +
                            " LEFT OUTER JOIN SuMast01 On SuMAst01.account=A.Party WHERE " +
                            " A.TrDate=TO_DATE(:fromdate,'DD-MM-YYYY') AND SUBSTR(PARAM,6,1) NOT IN ('5')AND GROUP1<>'IF98' AND TRIM(UccC) IS NOT NULL" +
                            " UNION ALL" +
                            " SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ ' ' TmYn,B.party,'C' TYP,' ' Account_type, B.ABNM UCC, ' ' TmCode," +
                            " 0 Funds,B.ALLOCATION  FundsP from CC0201 B LEFT OUTER JOIN (SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ " +
                            " MAX(TRDATE) TRDATE,PARTY,EXCD FROM CC0201 WHERE TRIM(ABNM)" +
                            " IS NOT NULL AND TRIM(TMCODE)=TRIM('07714') AND EXCD='C' GROUP BY PARTY,EXCD)A" +
                            " ON B.PARTY=A.PARTY AND B.EXCD=A.EXCD AND B.TRDATE=A.TRDATE WHERE TRIM(B.ABNM) IS NOT NULL AND " +
                            " TRIM(B.TMCODE)=TRIM('07714') AND TRIM(B.TMCODE)<>TRIM(B.ABNM) AND B.TRDATE=TO_DATE(:CC02date,'DD-MM-YYYY')" +
                            " AND B.ABNM NOT LIKE 'PRO_%' AND B.EXCD='C' )Final Group By Party having (SUM(Funds)<>SUM(FundsP))" +
                            " Order By 5";
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();


                            OracleCommand cmd = new OracleCommand(xx, conn);
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(":fromdate", OracleDbType.Char).Value = Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MM-yyyy");
                            cmd.Parameters.Add(":CC02date", OracleDbType.Char).Value = Convert.ToDateTime(datecc02picker.Text).ToString("dd-MM-yyyy");
                            OracleDataAdapter adp = new OracleDataAdapter(cmd);
                            adp.Fill(dt);
                            Source = "Focus";
                        }
                    }
                    else
                    {

                        // xx = "select uccn party,fundsn fund,ACCOUNT_TYPE from allocation01 where trdate=TO_DATE(:fromdate,'DD-MM-YYYY') " +
                        //     "and trim(uccn)='" + txtparty.Text + "' ";
                        dt = new DataTable();
                        dt.Columns.Add("party", typeof(string));
                        dt.Columns.Add("fund", typeof(string));
                        dt.Columns.Add("ACCOUNT_TYPE", typeof(string));
                        dt.Rows.Add(txtparty.Text, txtAmnt.Text, txtparty.Text.Trim() != "" ? "C" : "c");
                        Source = "Manual";
                    }
                }
                else if (cmbSegment.Text == "CO") //CM,FO,CD
                {
                    if (Valid == "12")
                    {
                        xx = "SELECT MAX(TmYn) TmYn,Party ,Max(TYP) TYP,MAX(UCC) ucc,SUM(Funds)-SUM(FundsP) DifferentFunds,SUM(Funds)Funds,SUM(FundsP) FundsP, " +
                            " max(Account_type) Account_type,Max(TmCode) TmCode,  case when SUM(Funds)-SUM(FundsP)< 0 then 'D' else 'U' end action" +
                            " FROM(SELECT TmYn,party,CASE WHEN GROUP1='IF98' THEN 'P' ELSE 'C' END " +
                            " TYP,A.Account_type,UccL UCC,' ' TmCode,FundsL Funds,0  FundsP from Allocation01 A " +
                            " LEFT OUTER JOIN SuMast01 On SuMAst01.account=A.Party WHERE A.TrDate=TO_DATE(:fromdate,'DD-MM-YYYY')" +
                            " AND SUBSTR(PARAM,6,1) NOT IN ('5') AND GROUP1<>'IF98' AND TRIM(UccL) IS NOT NULL " +
                            " UNION ALL" +
                            " SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ ' ' TmYn,B.party,'C' TYP,' ' Account_type," +
                            " B.ABNM UCC,' ' TmCode,0 Funds,B.ALLOCATION  FundsP from CC0201 B" +
                            " LEFT OUTER JOIN (SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ MAX(TRDATE) TRDATE,PARTY,EXCD FROM CC0201" +
                            " WHERE TRIM(ABNM) IS NOT NULL AND TRIM(TMCODE)=TRIM('07714') AND EXCD='L' GROUP BY PARTY,EXCD)A" +
                            " ON B.PARTY=A.PARTY AND B.EXCD=A.EXCD AND B.TRDATE=A.TRDATE WHERE TRIM(B.ABNM) IS NOT NULL " +
                            " AND TRIM(B.TMCODE)=TRIM('07714') AND TRIM(B.TMCODE)<>TRIM(B.ABNM) AND B.TRDATE=TO_DATE(:CC02date,'DD-MM-YYYY')" +
                            " AND B.ABNM NOT LIKE 'PRO_%' AND B.EXCD='L' )Final Group By Party having (SUM(Funds)<>SUM(FundsP)) " +
                            " Order By 5";

                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();


                            OracleCommand cmd = new OracleCommand(xx, conn);
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(":fromdate", OracleDbType.Char).Value = Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MM-yyyy");
                            cmd.Parameters.Add(":CC02date", OracleDbType.Char).Value = Convert.ToDateTime(datecc02picker.Text).ToString("dd-MM-yyyy");
                            OracleDataAdapter adp = new OracleDataAdapter(cmd);
                            adp.Fill(dt);
                            Source = "Focus";
                        }
                    }
                    else
                    {

                        // xx = "select uccn party,fundsn fund,ACCOUNT_TYPE from allocation01 where trdate=TO_DATE(:fromdate,'DD-MM-YYYY') " +
                        //     "and trim(uccn)='" + txtparty.Text + "' ";
                        dt = new DataTable();
                        dt.Columns.Add("party", typeof(string));
                        dt.Columns.Add("fund", typeof(string));
                        dt.Columns.Add("ACCOUNT_TYPE", typeof(string));
                        dt.Rows.Add(txtparty.Text, txtAmnt.Text, txtparty.Text.Trim() != "" ? "C" : "P");
                        Source = "Manual";
                    }



                }


                
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    DataTable dtfILTER1 = new DataTable();
                    if (cmbfilActionType.Text == "D")
                    {
                        dtfILTER1 = dt.Select("action='D'").CopyToDataTable();
                    }
                    else if (cmbfilActionType.Text == "U")
                    {
                        dtfILTER1 = dt.Select("action='U'").CopyToDataTable();
                    }
                    else
                    {
                        dtfILTER1 = dt.Copy();
                    }
                    DataTable dtMessageMater = GetMessageMaster();
                    //DataRow[] DrFeild = dtMessageMater.Select("TYPE='F'");
                    //DataRow[] DrValid = dtMessageMater.Select("TYPE='V'");
                    //DataTable dtFeildMsg = DrFeild.CopyToDataTable();
                    //DataTable dtValidMsg = DrValid.CopyToDataTable();
                    string result = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(dtfILTER1.Rows.Count) / Convert.ToDecimal(1000)));
                    string[] val = result.Split('.');
                    int _RealNum = Convert.ToInt32(val[0]);
                    int _decimal = Convert.ToInt32(val[1]);
                    int j = 0;
                    //Thread bgThread = new Thread();
                    for (int i = 0; i <= _RealNum; i++)
                    {
                        if (i == 0)
                        {
                            j = i + 1;
                        }
                        progressBar1.Maximum = _RealNum;
                        progressBar1.Value = i;

                        int pageSize = 1000;
                        DataTable dtfILTER = dtfILTER1.Rows.Cast<System.Data.DataRow>().Skip(i * pageSize).Take(pageSize).CopyToDataTable();

                        List<AllocationRequest> list = new List<AllocationRequest>();
                        for (int k = 0; k < dtfILTER.Rows.Count; k++)
                        {
                            if (Valid != "12")
                            {
                                AllocationRequest _allocation = new AllocationRequest()
                                {
                                    curDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy"), //Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MMM-yyyy"),
                                    segment = cmbSegment.Text,
                                    cmCode = "07714", //memid,
                                    tmCode = "07714",
                                    cpCode = "",
                                    cliCode = dtfILTER.Rows[k]["party"].ToString().Trim(), //dtfILTER.Rows[k]["ACCOUNT_TYPE"].ToString() == "P" ? "" : dtfILTER.Rows[k]["party"].ToString(),
                                    accType = dtfILTER.Rows[k]["ACCOUNT_TYPE"].ToString().Trim(),
                                    amt = Convert.ToDouble(txtAmnt.Text),
                                    filler1 = "",
                                    filler2 = "",
                                    filler3 = "",
                                    filler4 = "",
                                    filler5 = "",
                                    filler6 = "",
                                    action = cmbActionType.Text
                                };
                                list.Add(_allocation);
                            }
                            else
                            {
                                AllocationRequest _allocation = new AllocationRequest()
                                {
                                    curDate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy"), //Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MMM-yyyy"),
                                    segment = cmbSegment.Text,
                                    cmCode = memid,
                                    tmCode = "07714",
                                    cpCode = "",
                                    cliCode = dtfILTER.Rows[k]["ucc"].ToString().Trim(), //dtfILTER.Rows[k]["ACCOUNT_TYPE"].ToString() == "P" ? "" : dtfILTER.Rows[k]["party"].ToString(),
                                    accType = "C",//dtfILTER.Rows[k]["ACCOUNT_TYPE"].ToString().Trim(),
                                    amt = Convert.ToDouble(dtfILTER.Rows[k]["Funds"].ToString().Trim()),
                                    filler1 = "",
                                    filler2 = "",
                                    filler3 = "",
                                    filler4 = "",
                                    filler5 = "",
                                    filler6 = "",
                                    action = dtfILTER.Rows[k]["action"].ToString().Trim()
                                };
                                list.Add(_allocation);
                            }

                        }
                        Data data = new Data()
                        {

                            msgId = memid + "" + DateTime.Now.ToString("yyyyMMdd") + "" + cls.GenerateNewRandom7(),
                            requestType = "I",
                            allocationRequest = list
                        };

                        ClsAllocationReq _Req = new ClsAllocationReq()
                        {
                            version = "1.0",
                            data = data

                        };
                        if (Token == null)
                        {
                            Token = GetToken();
                        }
                        string JSON = JsonConvert.SerializeObject(_Req);
                        string Url = ConfigurationManager.AppSettings["Url"].ToString();

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + "/coll-allocation/allocation");
                        request.Method = "POST";
                        request.ContentType = "application/json";
                        request.Headers.Add("nonce", Nonce);
                        request.Headers.Add("Authorization", "Bearer " + Token);
                        request.ContentLength = JSON.Length;
                        using (Stream webStream = request.GetRequestStream())
                        using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
                        {
                            requestWriter.Write(JSON);
                        }
                        WebResponse webResponse = (HttpWebResponse)request.GetResponse();

                        using (Stream webStream = webResponse.GetResponseStream())
                        {
                            if (webStream != null)
                            {
                                using (StreamReader responseReader = new StreamReader(webStream))
                                {
                                    var responseString = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                                    Resp = JsonConvert.DeserializeObject<ClsAllocationResp>(responseString);
                                    //RespMessage= SaveRazorPaymentHistoryRequest();
                                }
                            }
                        }

                        if (Resp.status == null)
                        {
                            pnlWait.Visible = false;
                            ControlEnableDisable(true);
                            MessageBox.Show("Extension Not Response.");
                            return;
                        }
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();
                            for (int l = 0; l < _Req.data.allocationRequest.Count; l++)
                            {
                                // strbulder = new StringBuilder();
                                DataRow[] dr = null;
                                DataRow[] drV = null;
                                if (Resp.messages.code.Length == 8)
                                {
                                    string FeildMsg = Resp.messages.code.Substring(0, 4);
                                    string validMsg = Resp.messages.code.Substring(4, 4);
                                    dr = dtMessageMater.Select("TYPE='F' and ERRORCODE=" + FeildMsg + "");
                                    drV = dtMessageMater.Select("TYPE='V' and ERRORCODE=" + validMsg + "");
                                }

                                string MsgValid = "";
                                string MsgFeild = "";
                                if (dr.Length != 0)
                                {
                                    MsgFeild = Convert.ToString(dr[0]["ERRDESCRIPTION"].ToString());
                                }
                                if (drV.Length != 0)
                                {
                                    MsgValid = Convert.ToString(drV[0]["ERRDESCRIPTION"].ToString());
                                }
                                string query = "insert into AllocationReqData (msgId,requestType,curDate,seg,cmCode,tmCode,cpCode,cliCode,accType, ";
                                query += " amt,Filler1,Filler2,Filler3,Filler4,Filler5,Filler6,action,RespStatus,RespStatusCode,RespMessagesCode, ";
                                query += " RespFeildMsg,RespValidationMsg,CreateDateTime,Source)";
                                query += " values(:msgId,:requestType,:curDate,:seg,:cmCode,:tmCode,:cpCode,:cliCode,:accType,:amt,:Filler1,:Filler2,:Filler3,:Filler4,:Filler5,:Filler6, ";
                                query += " :action,:RespStatus,:RespStatusCode,:RespMessagesCode,:RespFeildMsg,:RespValidationMsg,:CreateDateTime,:Source)";

                                OracleCommand cmd = new OracleCommand(query, conn);
                                cmd.CommandType = CommandType.Text;
                                cmd.Parameters.Add(":msgId", OracleDbType.Varchar2).Value = _Req.data.msgId.ToString();
                                cmd.Parameters.Add(":requestType", OracleDbType.Char).Value = _Req.data.requestType.ToString();
                                cmd.Parameters.Add(":curDate", OracleDbType.Varchar2).Value = _Req.data.allocationRequest[l].curDate.ToString();
                                cmd.Parameters.Add(":seg", OracleDbType.Char).Value = _Req.data.allocationRequest[l].segment.ToString();
                                cmd.Parameters.Add(":cmCode", OracleDbType.Char).Value = _Req.data.allocationRequest[l].cmCode.ToString();
                                cmd.Parameters.Add(":tmCode", OracleDbType.Char).Value = _Req.data.allocationRequest[l].tmCode.ToString();
                                cmd.Parameters.Add(":cpCode", OracleDbType.Char).Value = _Req.data.allocationRequest[l].cpCode.ToString();
                                cmd.Parameters.Add(":cliCode", OracleDbType.Char).Value = _Req.data.allocationRequest[l].cliCode.ToString();
                                cmd.Parameters.Add(":accType", OracleDbType.Char).Value = _Req.data.allocationRequest[l].accType.ToString();
                                cmd.Parameters.Add(":amt", OracleDbType.Double).Value = Convert.ToDouble(_Req.data.allocationRequest[l].amt);
                                cmd.Parameters.Add(":Filler1", OracleDbType.Varchar2).Value = _Req.data.allocationRequest[l].filler1.ToString();
                                cmd.Parameters.Add(":Filler2", OracleDbType.Varchar2).Value = _Req.data.allocationRequest[l].filler2.ToString();
                                cmd.Parameters.Add(":Filler3", OracleDbType.Varchar2).Value = _Req.data.allocationRequest[l].filler3.ToString();
                                cmd.Parameters.Add(":Filler4", OracleDbType.Varchar2).Value = _Req.data.allocationRequest[l].filler4.ToString();
                                cmd.Parameters.Add(":Filler5", OracleDbType.Varchar2).Value = _Req.data.allocationRequest[l].filler5.ToString();
                                cmd.Parameters.Add(":Filler6", OracleDbType.Varchar2).Value = _Req.data.allocationRequest[l].filler6.ToString();
                                cmd.Parameters.Add(":action", OracleDbType.Char).Value = _Req.data.allocationRequest[l].action.ToString();
                                cmd.Parameters.Add(":RespStatus", OracleDbType.Varchar2).Value = Resp.status;
                                cmd.Parameters.Add(":RespStatusCode", OracleDbType.Char).Value = "";
                                cmd.Parameters.Add(":RespMessagesCode", OracleDbType.Varchar2).Value = Resp.messages.code;
                                cmd.Parameters.Add(":RespFeildMsg", OracleDbType.Varchar2).Value = MsgFeild;
                                cmd.Parameters.Add(":RespValidationMsg", OracleDbType.Varchar2).Value = MsgValid;

                                //cmd.Parameters.Add(":JsonRequestData", OracleDbType.Varchar2).Value = JSON;
                                //cmd.Parameters.Add(":Token", OracleDbType.Varchar2).Value = "";//Token;
                                //cmd.Parameters.Add(":Nonce", OracleDbType.Varchar2).Value = "";// Nonce;
                                cmd.Parameters.Add(":CreateDateTime", OracleDbType.Date).Value = Convert.ToDateTime(DateTime.Now);
                                cmd.Parameters.Add(":Source", OracleDbType.Varchar2).Value = Source;
                                cmd.ExecuteNonQuery();
                                if (Resp.status == "SUCCESS")
                                {
                                    MsgSuccess = MsgSuccess + 1;
                                }
                                else
                                {
                                    MsgError = MsgError + 1;
                                }
                                string str = "";
                                if (l == 0)
                                {
                                    str = "msgId,requestType,curDate,seg,cmCode,tmCode,cpCode,cliCode,accType,amt,action,RespStatus," +
                                       "RespMessagesCode,RespFeildMsg,RespValidationMsg,CreateDateTime,Source";

                                    strbulder.Append(str);
                                    strbulder.AppendLine();
                                }
                                if (l > 0)
                                {
                                    strbulder.AppendLine();
                                }
                                str = _Req.data.msgId.ToString() + "," + _Req.data.requestType.ToString() + "," + _Req.data.allocationRequest[l].curDate.ToString() + "," +
                              _Req.data.allocationRequest[l].segment.ToString() + "," + _Req.data.allocationRequest[l].cmCode.ToString() + "," +
                              _Req.data.allocationRequest[l].tmCode.ToString() + "," + _Req.data.allocationRequest[l].cpCode.ToString() + "," +
                              _Req.data.allocationRequest[l].cliCode.ToString() + "," + _Req.data.allocationRequest[l].accType.ToString() + "," +
                              Convert.ToString(_Req.data.allocationRequest[l].amt.ToString()) + "," + _Req.data.allocationRequest[l].action.ToString() + "," +
                              Resp.status + "," + Resp.messages.code + "," + MsgFeild + "," + MsgValid + "," + DateTime.Now.ToString() + "," + Source;
                                // strbulder.Append(str);
                                string filedir = file + "\\Holding_" + fileDate + ".csv";

                                using (StreamWriter sw = new StreamWriter(filedir, false))
                                {
                                    sw.Write(strbulder.Append(str));
                                }


                            }
                        }

                    }

                }
                else
                {
                    pnlWait.Visible = false;
                    ControlEnableDisable(true);
                    MessageBox.Show("No Record Found");

                }

            }
            catch (Exception ex)
            {
                pnlWait.Visible = false;
                ControlEnableDisable(true);
                ex.Message.ToString();

            }
            pnlWait.Visible = false;
            DialogResult res = MessageBox.Show("Total Record = " + dt.Rows.Count + " Success Record = " + MsgSuccess + " Failed Record = " + MsgError, "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            ControlEnableDisable(true);

        }
        private void btnStatusEnquiry_Click(object sender, EventArgs e)
        {
            if (Token == null)
            {
                Token = GetToken();
            }

            FrmStatusEnquiry frm = new FrmStatusEnquiry();
            frm.Token = Token;
            frm.Nonce = Nonce;
            frm.ShowDialog();
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

        private void btnNseUpload_Click(object sender, EventArgs e)
        {
            FrmNse frmnse = new FrmNse();
            frmnse.BringToFront();
            frmnse.Show();
        }

        private void txtAmnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void cmbActionType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string xx = "";


            try
            {
                string Valid = "";
                if (txtAmnt.Text == "")
                {
                    Valid += "1";
                    //MessageBox.Show("Please fill Amount");
                    //txtAmnt.Focus();
                    //return;
                }

                if (cmbActionType.Text == "")
                {
                    Valid += "2";
                    //MessageBox.Show("Please fill Amount");
                    //txtAmnt.Focus();
                    //return;
                }

                if ((Valid != "12") && (Valid != ""))
                {
                    pnlWait.Visible = false;
                    ControlEnableDisable(true);
                    MessageBox.Show("Please fill all feild ");
                    if (!Valid.Contains("1") == true)
                    {
                        txtAmnt.Focus();
                    }

                    if (!Valid.Contains("2") == true)
                    {
                        cmbActionType.Focus();
                    }
                    return;
                }
                if (cmbSegment.Text == "CM") //CM,FO,CD
                {
                    if (Valid == "12")
                    {
                        //xx = "select t1.uccn party,t1.fundsn fund,t1.ACCOUNT_TYPE from allocation01 t1 ,shmast01 mst where mst.abnmn=t1.uccn and mst.typop<>'5' and t1.trdate=TO_DATE(:fromdate,'DD-MM-YYYY')" +
                        //    " AND t1.account_type='C' and trim(t1.uccn) is not null and fundsn<>0 ";

                        xx = "SELECT MAX(TmYn) TmYn,Party ,Max(TYP) TYP,MAX(UCC) ucc,SUM(Funds)-SUM(FundsP) DifferentFunds,SUM(Funds) " +
                            " Funds,SUM(FundsP) FundsP,max(Account_type) Account_type,Max(TmCode) TmCode,  " +
                            " case when SUM(Funds)-SUM(FundsP)< 0 then 'D' else 'U' end action FROM(" +
                            " SELECT TmYn,party, CASE WHEN GROUP1='IF98' THEN 'P' ELSE 'C' END TYP,A.Account_type, UccN UCC," +
                            " ' ' TmCode, FundsN Funds,0  FundsP " +
                            " from Allocation01 A " +
                            " LEFT OUTER JOIN SuMast01 On SuMAst01.account=A.Party" +
                            " WHERE A.TrDate=TO_DATE(:fromdate,'DD-MM-YYYY') AND SUBSTR(PARAM,6,1) NOT IN ('5')AND GROUP1<>'IF98'" +
                            " AND SUBSTR(PARAM,6,1) NOT IN ('5') AND GROUP1<>'IF98'" +
                            " AND TRIM(UccN) IS NOT NULL" +
                            " UNION ALL" +
                            " SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ ' ' TmYn,B.party,'C' TYP,' ' Account_type, B.ABNM UCC, ' ' TmCode," +
                            " 0 Funds,B.ALLOCATION  FundsP from CC0201 B" +
                            " LEFT OUTER JOIN (SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ MAX(TRDATE) TRDATE,PARTY,EXCD FROM CC0201 WHERE TRIM(ABNM)  " +
                            " IS NOT NULL AND TRIM(TMCODE)=TRIM('07714') AND EXCD='N' GROUP BY PARTY,EXCD)A" +
                            " ON B.PARTY=A.PARTY AND B.EXCD=A.EXCD AND B.TRDATE=A.TRDATE WHERE TRIM(B.ABNM) IS NOT NULL AND  " +
                            " TRIM(B.TMCODE)=TRIM('07714') AND TRIM(B.TMCODE)<>TRIM(B.ABNM) AND B.TRDATE=TO_DATE(:CC02date,'DD-MM-YYYY')" +
                            " AND B.ABNM NOT LIKE 'PRO_%' AND B.EXCD='N' )Final Group By Party having (SUM(Funds)<>SUM(FundsP)) Order By 5";
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();


                            OracleCommand cmd = new OracleCommand(xx, conn);
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(":fromdate", OracleDbType.Char).Value = Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MM-yyyy");
                            cmd.Parameters.Add(":CC02date", OracleDbType.Char).Value = Convert.ToDateTime(datecc02picker.Text).ToString("dd-MM-yyyy");
                            OracleDataAdapter adp = new OracleDataAdapter(cmd);
                            adp.Fill(dt);

                        }
                    }
                    else
                    {

                        // xx = "select uccn party,fundsn fund,ACCOUNT_TYPE from allocation01 where trdate=TO_DATE(:fromdate,'DD-MM-YYYY') " +
                        //     "and trim(uccn)='" + txtparty.Text + "' ";
                        dt = new DataTable();
                        dt.Columns.Add("party", typeof(string));
                        dt.Columns.Add("fund", typeof(string));
                        dt.Columns.Add("ACCOUNT_TYPE", typeof(string));
                        dt.Rows.Add(txtparty.Text, txtAmnt.Text, txtparty.Text.Trim() != "" ? "C" : "P");

                    }



                }
                else if (cmbSegment.Text == "FO") //CM,FO,CD
                {
                    if (Valid == "12")
                    {
                        //xx = "select t1.uccz party,t1.fundsz fund,t1.ACCOUNT_TYPE FROM allocation01 t1,shmast01 mst where mst.abnmz=t1.uccz and mst.typop<>'5' " +
                        //  "  and t1.trdate=TO_DATE(:fromdate,'DD-MM-YYYY') AND t1.account_type='C' and trim(t1.uccz) is not null and t1.fundsz<>0 ";

                        xx = "SELECT MAX(TmYn) TmYn,Party ,Max(TYP) TYP,MAX(UCC) ucc,SUM(Funds)-SUM(FundsP) DifferentFunds,SUM(Funds)Funds,SUM(FundsP) FundsP, " +
                            " max(Account_type) Account_type,Max(TmCode) TmCode, case when SUM(Funds)-SUM(FundsP)< 0 then 'D' else 'U' end action" +
                            " FROM( SELECT TmYn,party, CASE WHEN GROUP1='IF98' THEN 'P' ELSE 'C'" +
                            "  END TYP,A.Account_type, UccZ UCC, ' ' TmCode, FundsZ Funds,0  FundsP " +
                            " from Allocation01 A" +
                            " LEFT OUTER JOIN SuMast01 On SuMAst01.account=A.Party WHERE A.TrDate=TO_DATE(:fromdate,'DD-MM-YYYY')" +
                            " AND SUBSTR(PARAM,6,1) NOT IN ('5') AND GROUP1<>'IF98'" +
                            " AND TRIM(UccZ) IS NOT NULL" +
                            " UNION ALL" +
                            " SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ ' ' TmYn,B.party, 'C' TYP,' ' Account_type, B.ABNM UCC, ' ' TmCode," +
                            " 0 Funds,B.ALLOCATION  FundsP from CC0201 B" +
                            " LEFT OUTER JOIN (SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ MAX(TRDATE) TRDATE,PARTY,EXCD FROM CC0201 WHERE TRIM(ABNM) " +
                            " IS NOT NULL AND TRIM(TMCODE)=TRIM('07714') AND EXCD='Z' GROUP BY PARTY,EXCD)A" +
                            " ON B.PARTY=A.PARTY AND B.EXCD=A.EXCD AND B.TRDATE=A.TRDATE WHERE TRIM(B.ABNM) IS NOT NULL AND TRIM(B.TMCODE)=TRIM('07714') AND TRIM(B.TMCODE)<>TRIM(B.ABNM) AND " +
                            " B.TRDATE=TO_DATE(:CC02date,'DD-MM-YYYY') AND B.ABNM NOT LIKE 'PRO_%' AND B.EXCD='Z' )Final Group By Party" +
                            " having (SUM(Funds)<>SUM(FundsP))Order By 5";
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();


                            OracleCommand cmd = new OracleCommand(xx, conn);
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(":fromdate", OracleDbType.Char).Value = Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MM-yyyy");
                            cmd.Parameters.Add(":CC02date", OracleDbType.Char).Value = Convert.ToDateTime(datecc02picker.Text).ToString("dd-MM-yyyy");
                            OracleDataAdapter adp = new OracleDataAdapter(cmd);
                            adp.Fill(dt);

                        }
                    }
                    else
                    {

                        // xx = "select uccn party,fundsn fund,ACCOUNT_TYPE from allocation01 where trdate=TO_DATE(:fromdate,'DD-MM-YYYY') " +
                        //     "and trim(uccn)='" + txtparty.Text + "' ";
                        dt = new DataTable();
                        dt.Columns.Add("party", typeof(string));
                        dt.Columns.Add("fund", typeof(string));
                        dt.Columns.Add("ACCOUNT_TYPE", typeof(string));
                        dt.Rows.Add(txtparty.Text, txtAmnt.Text, txtparty.Text.Trim() != "" ? "C" : "P");

                    }



                }
                else if (cmbSegment.Text == "CD") //CM,FO,CD
                {
                    if (Valid == "12")
                    {
                        // xx = "select t1.uccc party,t1.fundsc fund,t1.ACCOUNT_TYPE from allocation01 t1,shmast01 mst where mst.abnmc=t1.uccc and mst.typop<>'5' and t1.trdate=TO_DATE(:fromdate,'DD-MM-YYYY')" +
                        //     " AND t1.account_type='C' and trim(t1.uccc) is not null and t1.fundsc<>0 ";

                        xx = "SELECT MAX(TmYn) TmYn,Party ,Max(TYP) TYP,MAX(UCC) ucc,SUM(Funds)-SUM(FundsP)DifferentFunds,SUM(Funds)Funds,SUM(FundsP) FundsP," +
                            " max(Account_type) Account_type,Max(TmCode) TmCode,  case when SUM(Funds)-SUM(FundsP)< 0 then 'D' else 'U' end action " +
                            " FROM( SELECT TmYn,party, CASE WHEN GROUP1='IF98' THEN 'P' ELSE 'C'" +
                            " END TYP,A.Account_type, UccC UCC, ' ' TmCode, FundsC Funds,0  FundsP from Allocation01 A" +
                            " LEFT OUTER JOIN SuMast01 On SuMAst01.account=A.Party WHERE " +
                            " A.TrDate=TO_DATE(:fromdate,'DD-MM-YYYY') AND SUBSTR(PARAM,6,1) NOT IN ('5')AND GROUP1<>'IF98' AND TRIM(UccC) IS NOT NULL" +
                            " UNION ALL" +
                            " SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ ' ' TmYn,B.party,'C' TYP,' ' Account_type, B.ABNM UCC, ' ' TmCode," +
                            " 0 Funds,B.ALLOCATION  FundsP from CC0201 B LEFT OUTER JOIN (SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ " +
                            " MAX(TRDATE) TRDATE,PARTY,EXCD FROM CC0201 WHERE TRIM(ABNM)" +
                            " IS NOT NULL AND TRIM(TMCODE)=TRIM('07714') AND EXCD='C' GROUP BY PARTY,EXCD)A" +
                            " ON B.PARTY=A.PARTY AND B.EXCD=A.EXCD AND B.TRDATE=A.TRDATE WHERE TRIM(B.ABNM) IS NOT NULL AND " +
                            " TRIM(B.TMCODE)=TRIM('07714') AND TRIM(B.TMCODE)<>TRIM(B.ABNM) AND B.TRDATE=TO_DATE(:CC02date,'DD-MM-YYYY')" +
                            " AND B.ABNM NOT LIKE 'PRO_%' AND B.EXCD='C' )Final Group By Party having (SUM(Funds)<>SUM(FundsP))" +
                            " Order By 5";
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();


                            OracleCommand cmd = new OracleCommand(xx, conn);
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(":fromdate", OracleDbType.Char).Value = Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MM-yyyy");
                            cmd.Parameters.Add(":CC02date", OracleDbType.Char).Value = Convert.ToDateTime(datecc02picker.Text).ToString("dd-MM-yyyy");
                            OracleDataAdapter adp = new OracleDataAdapter(cmd);
                            adp.Fill(dt);

                        }
                    }
                    else
                    {

                        // xx = "select uccn party,fundsn fund,ACCOUNT_TYPE from allocation01 where trdate=TO_DATE(:fromdate,'DD-MM-YYYY') " +
                        //     "and trim(uccn)='" + txtparty.Text + "' ";
                        dt = new DataTable();
                        dt.Columns.Add("party", typeof(string));
                        dt.Columns.Add("fund", typeof(decimal));
                        dt.Columns.Add("ACCOUNT_TYPE", typeof(string));
                        dt.Rows.Add(txtparty.Text, Convert.ToDecimal(txtAmnt.Text), txtparty.Text.Trim() != "" ? "C" : "C");

                    }



                }
                else if (cmbSegment.Text == "CO") //CM,FO,CD
                {
                    if (Valid == "12")
                    {
                        xx = "SELECT MAX(TmYn) TmYn,Party ,Max(TYP) TYP,MAX(UCC) ucc,SUM(Funds)-SUM(FundsP) DifferentFunds,SUM(Funds)Funds,SUM(FundsP) FundsP, " +
                            " max(Account_type) Account_type,Max(TmCode) TmCode,  case when SUM(Funds)-SUM(FundsP)< 0 then 'D' else 'U' end action" +
                            " FROM(SELECT TmYn,party,CASE WHEN GROUP1='IF98' THEN 'P' ELSE 'C' END TYP," +
                            " A.Account_type,UccL UCC,' ' TmCode,FundsL Funds,0  FundsP from Allocation01 A " +
                            " LEFT OUTER JOIN SuMast01 On SuMAst01.account=A.Party WHERE A.TrDate=TO_DATE(:fromdate,'DD-MM-YYYY')" +
                            " AND SUBSTR(PARAM,6,1) NOT IN ('5') AND GROUP1<>'IF98' AND TRIM(UccL) IS NOT NULL" +
                            " UNION ALL" +
                            " SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ ' ' TmYn,B.party,'C' TYP,' ' Account_type," +
                            " B.ABNM UCC,' ' TmCode,0 Funds,B.ALLOCATION  FundsP from CC0201 B" +
                            " LEFT OUTER JOIN (SELECT /*+ INDEX(CC0201 CC02DATESQL01)*/ MAX(TRDATE) TRDATE,PARTY,EXCD FROM CC0201" +
                            " WHERE TRIM(ABNM) IS NOT NULL AND TRIM(TMCODE)=TRIM('07714') AND EXCD='L' GROUP BY PARTY,EXCD)A" +
                            " ON B.PARTY=A.PARTY AND B.EXCD=A.EXCD AND B.TRDATE=A.TRDATE WHERE TRIM(B.ABNM) IS NOT NULL " +
                            " AND TRIM(B.TMCODE)=TRIM('07714') AND TRIM(B.TMCODE)<>TRIM(B.ABNM) AND B.TRDATE=TO_DATE(:CC02date,'DD-MM-YYYY')" +
                            " AND B.ABNM NOT LIKE 'PRO_%' AND B.EXCD='L' )Final Group By Party having (SUM(Funds)<>SUM(FundsP)) " +
                            " Order By 5";

                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        {
                            conn.Open();


                            OracleCommand cmd = new OracleCommand(xx, conn);
                            cmd.CommandType = CommandType.Text;

                            cmd.Parameters.Add(":fromdate", OracleDbType.Char).Value = Convert.ToDateTime(frmdatepicker.Text).ToString("dd-MM-yyyy");
                            cmd.Parameters.Add(":CC02date", OracleDbType.Char).Value = Convert.ToDateTime(datecc02picker.Text).ToString("dd-MM-yyyy");
                            OracleDataAdapter adp = new OracleDataAdapter(cmd);
                            adp.Fill(dt);

                        }
                    }
                    else
                    {

                        // xx = "select uccn party,fundsn fund,ACCOUNT_TYPE from allocation01 where trdate=TO_DATE(:fromdate,'DD-MM-YYYY') " +
                        //     "and trim(uccn)='" + txtparty.Text + "' ";
                        dt = new DataTable();
                        dt.Columns.Add("party", typeof(string));
                        dt.Columns.Add("fund", typeof(string));
                        dt.Columns.Add("ACCOUNT_TYPE", typeof(string));
                        dt.Rows.Add(txtparty.Text, txtAmnt.Text, txtparty.Text.Trim() != "" ? "C" : "P");

                    }



                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Valid == "12")
                    {
                        panel3.Visible = false;
                        panel2.Visible = true;
                        mAllocGrd.Visible = false;
                        AAllocGrd.Visible = true;
                        dt.Columns.Remove("TmYn");
                        dt.Columns.Remove("TYP");
                        dt.Columns.Remove("Party");
                        dt.Columns.Remove("TmCode");
                        AAllocGrd.DataSource = dt;
                        
                        var U_Total = (from t in dt.AsEnumerable()
                                       where t["Action"].ToString().Trim() == "U"
                                       select Convert.ToDecimal(t["funds"]))
                                                      .Sum();
                        var D_Total = (from t in dt.AsEnumerable()
                                       where t["Action"].ToString().Trim() == "D"
                                       select Convert.ToDecimal(t["funds"]))
                                                      .Sum();
                        var U_diff_Total = (from t in dt.AsEnumerable()
                                       where t["Action"].ToString().Trim() == "U"
                                       select Convert.ToDecimal(t["DifferentFunds"]))
                                                      .Sum();
                        var D_diff_Total = (from t in dt.AsEnumerable()
                                       where t["Action"].ToString().Trim() == "D"
                                       select Convert.ToDecimal(t["DifferentFunds"]))
                                                      .Sum();
                        label4.Text = "Total Amount Action U = " + U_Total + Environment.NewLine +
                                     "Total Amount Action D = " + D_Total;
                       // BringToFront();
                       label7.Text= "Total Different Amount Action U = " + U_diff_Total + Environment.NewLine +
                                     "Total Different Amount Action D = " + D_diff_Total;
                    }
                    else
                    {
                        panel3.Visible = false;
                        panel2.Visible = true;
                        AAllocGrd.Visible = false;
                        mAllocGrd.Visible = true;
                        mAllocGrd.DataSource = dt;
                        label4.Text = "";
                        //BringToFront();
                    }


                }
                else
                {
                    AAllocGrd.DataSource = dt;
                    mAllocGrd.DataSource = dt;
                    label4.Text = "Total Amount Action U = 0 " + Environment.NewLine +
                                    "Total Amount Action D = 0";
                    MessageBox.Show("No Record found");


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnSubmitAllocation_Click(object sender, EventArgs e)
        {
            Submit_AllocationAPI();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
            BringToFront();
        }

        //private void btnAllocationSubmit_Click(object sender, EventArgs e)
        //{
        //    Submit_AllocationAPI();
        //}

        //private void BtnCancel_Click(object sender, EventArgs e)
        //{
        //    panel2.Visible = false;
        //}

        //private void btnAllocationSubmit_Click_1(object sender, EventArgs e)
        //{
        //    Submit_AllocationAPI();
        //}

        //private void BtnCancel_Click_1(object sender, EventArgs e)
        //{
        //    panel2.Visible = false;
        //}
    }
}
