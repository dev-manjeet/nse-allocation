using Newtonsoft.Json;
using NSEAllocation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSEAllocation
{
    public partial class FormNSEHolding : Form
    {
        string Token = "";
        OpenFileDialog fdlg1 = new OpenFileDialog();
        string Uncompressoutputstr = "";
        public FormNSEHolding()
        {
            InitializeComponent();
            
        }
        public string GetToken()
        {
            //string Result = "";
            string path = AppDomain.CurrentDomain.BaseDirectory + "LoginFile\\LoginLog.txt";
            if (System.IO.File.Exists(path))
            {

                string[] allLines = System.IO.File.ReadAllLines(path);
                if (allLines.Count() > 0)
                {
                    string[] Arr = allLines[0].Split(',');
                    if (Arr.Length > 0)
                    {

                        DateTime dt1 = DateTime.ParseExact(Arr[1].ToString().Replace("DateTime=", "").Replace(".", ":"), "yyyyMMdd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                        DateTime dt3 = DateTime.Now;

                        if (dt1.ToString().Substring(0, 10) == dt3.ToString().Substring(0, 10))
                        {

                            TimeSpan T1 = dt1.Subtract(dt3);
                            string strT2 = T1.ToString();
                            if (strT2.Contains('-') == true)
                            {
                                if (strT2.Substring(1, 2) == "00")
                                {
                                    if (Convert.ToInt32(strT2.Substring(4, 2)) < 28)
                                    {
                                        Token = Arr[0].ToString().Replace("Token=", "");
                                    }
                                    else
                                    {
                                        Token = Login();
                                    }
                                }
                                else
                                {
                                    Token = Login();
                                }
                            }
                            else
                            {
                                Token = Login();
                            }

                        }
                        else
                        {
                            Token = Login();
                        }
                    }
                    else
                    {
                        string pathdel = AppDomain.CurrentDomain.BaseDirectory + "LoginFile";
                        if (System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.Delete(path);
                        }
                        Token = Login();
                    }
                }
                else
                {
                    Token = Login();
                }

            }
            else
            {
                Token = Login();
            }
            return Token;
        }
        public string Login()
        {
            string _result = "";

            ClsAESEncryptDecrypt clsEncrpt = new ClsAESEncryptDecrypt();
            ClsUtlity clsUtility = new ClsUtlity();
            string path = AppDomain.CurrentDomain.BaseDirectory + "LoginFile";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            //string LoginDatetime = "";
            string url = ConfigurationManager.AppSettings["UrlNse"].ToString();
            string mmcode = ConfigurationManager.AppSettings["MEMCODE"].ToString();
            string loginID = ConfigurationManager.AppSettings["LoginId"].ToString();
            string Password = ConfigurationManager.AppSettings["Password"].ToString();
            string SecretKey = ConfigurationManager.AppSettings["SecretKey"].ToString();

            string EncryptPassword = clsEncrpt.GetEncryptData(Password, SecretKey, true);

            try
            {
                ClsNseLoginReq _clsReq = new ClsNseLoginReq()
                {
                    memberCode = mmcode,
                    loginId = loginID,
                    password = EncryptPassword
                };
                string JSON = JsonConvert.SerializeObject(_clsReq);
                if (System.IO.File.Exists(path + "\\Login" + mmcode + ".txt"))
                {
                    System.IO.File.Delete(path + "\\Login" + mmcode + ".txt");
                }
                using (StreamWriter sw = new StreamWriter(path + "\\Login" + mmcode + ".txt", false))
                {
                    sw.Write(JSON);
                }
                bool Result = CreateZip(path + "\\Login" + mmcode + ".txt", path + "\\Login" + mmcode + "");

                var sFile = path + "\\Login07714.gz";
                string RespApi = clsUtility.LoginGetUrl(url + "/login", "", sFile, true, ref Uncompressoutputstr);

                string StrResponse = RespApi.Replace("\\", "");
                ClsLoginResp ClsResp = new ClsLoginResp();
                if (StrResponse.Contains("memberCode"))
                {
                    ClsResp = JsonConvert.DeserializeObject<ClsLoginResp>(StrResponse); ;
                }
                if (ClsResp.token.ToString().Trim() != "")
                {
                    if (System.IO.File.Exists(path + "\\LoginLog.txt"))
                    {
                        System.IO.File.Delete(path + "\\LoginLog.txt");
                    }
                    System.IO.File.AppendAllText(path + "/LoginLog.txt", "Token=" + ClsResp.token.ToString() + ",DateTime=" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                }
                else
                {
                    string errmsg = MasterError(ClsResp.code[0].ToString());
                    MessageBox.Show("Login Fail." + Environment.NewLine + errmsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                return _result = ClsResp.token.ToString();





            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            return _result;
        }
        public bool CreateZip(string SourcePath, string DesinationPath)
        {

            try
            {

                ProcessStartInfo p = new ProcessStartInfo();
                p.FileName = @"C:\Program Files\7-Zip\7zG.exe";
                p.Arguments = "a -tgzip \"" + DesinationPath + ".gz\" \"" + SourcePath + "\" -mx=9";
                p.WindowStyle = ProcessWindowStyle.Hidden;
                Process x = Process.Start(p);
                x.WaitForExit();
                //Compress(DesinationPath);
                return true;
            }
            catch (Exception)
            {

                return true;
            }
        }
        public string MasterError(string ErrCode)
        {
            string StrErr = "";



            if (ErrCode.Trim() == "602")
            {
                StrErr = "GENERAL ERROR.";
                return StrErr;
            }
            if (ErrCode.Trim() == "928")
            {
                StrErr = "ACCOUNT_INACTIVE.";
                return StrErr;
            }
            if (ErrCode.Trim() == "928")
            {
                StrErr = "ACCOUNT_INACTIVE.";
                return StrErr;
            }
            if (ErrCode.Trim() == "928")
            {
                StrErr = "ACCOUNT_INACTIVE.";
                return StrErr;
            }
            if (ErrCode.Trim() == "928")
            {
                StrErr = "ACCOUNT_INACTIVE.";
                return StrErr;
            }
            return StrErr;
        }
        public List<HoldingData> lstHolddata(string _weekdate)
        {
            List<HoldingData> lstResp = new List<HoldingData>();
            List<HoldingDataFilter> lstRespfilter = new List<HoldingDataFilter>();
            List<HoldingDataFilter> lstRespfilter1 = new List<HoldingDataFilter>();
            string path = txtfileupload.Text;
            string CSVFilePath = Path.GetFullPath(path);

            string[] allLines = System.IO.File.ReadAllLines(CSVFilePath);
            if (allLines.Length > 0)
            {
                for (int i = 0; i < allLines.Length; i++)
                {
                    if (i != 0)
                    {

                        string[] Arr = allLines[i].Split(',');
                        //var lst = Arr.ToList().Where(x => x.Length > 0).ToList();
                        HoldingDataFilter _hold = new HoldingDataFilter()
                        {
                            recId = Convert.ToString(i),
                            Date = Arr[1].ToString(),
                            dmat = Arr[2].ToString(),
                            accountType = Arr[3].ToString(),
                            ucc = Arr[4].ToString(),
                            clientName = Arr[5].ToString(),
                            pan = Arr[6].ToString(),
                            isin = Arr[7].ToString(),
                            securityType = Arr[8].ToString(),
                            nameOfCommodity = Arr[9].ToString(),
                            unitType = Arr[10].ToString(),
                            totalPldgQty = Arr[11].ToString(),
                            freeBalQty = Arr[12].ToString(),
                            totalQty = Arr[13].ToString(),


                        };
                        lstRespfilter.Add(_hold);
                    }

                }
                var result = lstRespfilter.Where(h => h.Date == _weekdate).ToList();
                HoldingData cls = new HoldingData();
                for (int i = 0; i < result.Count; i++)
                {
                    cls = new HoldingData()
                    {
                        recId = result[i].recId.ToString(),//Convert.ToString(i + 1),
                        dmat = result[i].dmat.ToString(),
                        accountType = result[i].accountType.ToString(),
                        ucc = result[i].ucc.ToString(),
                        clientName = result[i].clientName.ToString(),
                        pan = result[i].pan.ToString(),
                        isin = result[i].isin.ToString(),
                        securityType = result[i].securityType.ToString(),
                        nameOfCommodity = result[i].nameOfCommodity.ToString(),
                        unitType = result[i].unitType.ToString(),
                        totalPldgQty = result[i].totalPldgQty.ToString(),
                        freeBalQty = result[i].freeBalQty.ToString(),
                        totalQty = result[i].totalQty.ToString(),
                    };
                    lstResp.Add(cls);
                }



            }
            return lstResp;
        }
        private void FormNSEHolding_Load(object sender, EventArgs e)
        {
            Token = GetToken();
            txtToken.Text = "Bearer " + Token;
            pnlWait.Visible = false;

        }

        private void btnuploadHold_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Trading Holding File Dialog";
                //fdlg.InitialDirectory = @"c:\";
                fdlg.InitialDirectory = @"";
                fdlg.Filter = "All files (*.csv)|*.*|All files (*.CSV)|*.csv";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtfileupload.Text = fdlg.FileName;
                }
                if (!fdlg.FileName.ToUpper().Contains(".CSV"))
                {
                    MessageBox.Show("Please Upload Only CSV Format");
                    txtfileupload.Text = "";
                    return;
                }


            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void btnConvertDateWise_Click(object sender, EventArgs e)
        {
            string LASTwEEKDARW = "";
            string pathTXT = AppDomain.CurrentDomain.BaseDirectory + "NSE_HoldingFile" + "\\" + LASTwEEKDARW;

            try
            {
                string pan = "";
                //string _date = DateTime.Now.ToString("ddMMyyyy");

                if (txtfileupload.Text.ToString() != "")
                {
                    pnlWait.Visible = true;
                    lblTitle.Visible = true;
                    string path = txtfileupload.Text;
                    string CSVFilePath = Path.GetFullPath(path);
                    //string ReadCSV = System.IO.File.ReadAllText(CSVFilePath);
                    string[] allLines = System.IO.File.ReadAllLines(CSVFilePath);
                    int ToTALrECORD = allLines.Count() - 1;
                    List<HoldingDataFilter> DATEfILTER = new List<HoldingDataFilter>();
                    List<HoldingDataFilter> DATEfILTER_sort = new List<HoldingDataFilter>();
                    HoldingDataFilter clssort = new HoldingDataFilter();
                    for (int i = 0; i < allLines.Count(); i++)
                    {
                        clssort = new HoldingDataFilter();
                        string[] Arr = allLines[i].Split(',');
                        if (i == 0)
                        {
                            if (Arr[0].ToString() != "Member PAN")
                            {
                                MessageBox.Show("Please Upload Only Correct file Holding ");
                                txtfileupload.Text = "";
                                return;
                            }
                        }
                        if (i != 0)
                        {
                            pan = Arr[0].ToString();
                            clssort.recId = i.ToString();
                            clssort.Date = Arr[1].ToString();
                            clssort.dmat = Arr[2].ToString();
                            clssort.accountType = Arr[3].ToString();
                            clssort.ucc = Arr[4].ToString();
                            clssort.clientName = Arr[5].ToString();
                            clssort.pan = Arr[6].ToString();
                            clssort.isin = Arr[7].ToString();
                            clssort.securityType = Arr[8].ToString();
                            clssort.nameOfCommodity = Arr[9].ToString();
                            clssort.unitType = Arr[10].ToString();
                            clssort.totalPldgQty = Arr[11].ToString();
                            clssort.freeBalQty = Arr[12].ToString();
                            clssort.totalQty = Arr[13].ToString();
                            DATEfILTER.Add(clssort);
                        }
                    }
                    //string LASTwEEKDARW = DATEfILTER.Max(x => x.Date.ToString());
                    var Result1 = DATEfILTER.GroupBy(x => x.Date).Distinct().ToList();
                    List<datefilter> _datefilter = new List<datefilter>();
                    List<datefilter> _datefilter_sort = new List<datefilter>();
                    for (int i = 0; i < Result1.Count(); i++)
                    {
                        datefilter df = new datefilter()
                        {
                            date = Convert.ToDateTime(Result1[i].Key)
                        };

                        _datefilter.Add(df);
                    }

                    var sort = _datefilter.OrderByDescending(x => x.date).ToList();
                    string LASTwEEKDARW1 = sort[0].date.ToString();
                    LASTwEEKDARW = LASTwEEKDARW1.Substring(0, 10);
                    int count = 0;
                    for (int i = 0; i < Result1.Count(); i++)
                    {
                        int j = i + 1;

                        progressBar1.Maximum = 6;
                        progressBar1.Value = j;
                        //string dat = Result1[i].Key.ToString();
                        string dat = sort[sort.Count() - j].date.ToString().Substring(0, 10);
                        ClsReqNseHoldingFileSubmission _clsReqHold = new ClsReqNseHoldingFileSubmission()
                        {
                            memberPan = pan,
                            lastWeekDay = LASTwEEKDARW,
                            weekDay = dat,
                            holdingData = lstHolddata(dat)
                        };


                        
                        pan = _clsReqHold.memberPan;
                       
                        if (!System.IO.Directory.Exists(pathTXT + LASTwEEKDARW))
                        {
                            System.IO.Directory.CreateDirectory(pathTXT + LASTwEEKDARW);
                        }
                        if (System.IO.File.Exists(pathTXT + LASTwEEKDARW + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt"))
                        {
                            System.IO.File.Delete(pathTXT + LASTwEEKDARW + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt");
                        }
                        if (System.IO.File.Exists(pathTXT + LASTwEEKDARW + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt.gz"))
                        {
                            System.IO.File.Delete(pathTXT + LASTwEEKDARW + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt.gz");
                        }
                        string JSON = JsonConvert.SerializeObject(_clsReqHold);


                        using (StreamWriter sw = new StreamWriter(pathTXT + LASTwEEKDARW + "\\" + _clsReqHold.memberPan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt", true))
                        {
                            sw.Write(JSON);
                        }
                        bool Result = CreateZip(pathTXT + LASTwEEKDARW + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt", pathTXT + LASTwEEKDARW + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt");
                        if (Result == true)
                        {
                            count = count + 1;
                        }
                        //ClsUtlity clsUtility = new ClsUtlity();
                        //var sFile = pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt.gz";
                        //string url = ConfigurationManager.AppSettings["UrlNse"].ToString();
                        //string TOK = GetToken();
                        //if (TOK == "")
                        //{
                        //    MessageBox.Show("Token not Created");
                        //    return;
                        //}
                        //string TOKEN = "Bearer " + TOK;

                        //var RespApi = clsUtility.GetUrlHolding(url + "/tradingHoldingUpload/1.0", TOKEN, sFile, true, ResponseFileSave1);
                        //if (RespApi != "True")
                        //{
                        //    RespApi = clsUtility.GetUrlHolding(url + "/tradingHoldingUpload/1.0", TOKEN, sFile, true, ResponseFileSave1);
                        //}
                        // if (RespApi == "True")
                        // {
                        //using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                        //{
                        //    conn.Open();
                        //    for (int l = 0; l < _clsReqHold.holdingData.Count(); l++)
                        //    {
                        //        string query = "insert into NSE_HOLDING_UPLOAD_Req (FileSequnceNo,MEMBERPAN,WEEKDAY,";
                        //        query += " LASTWEEKDAY,RECID,Dmat,AccountType,UCC,clientName,PAN,isin,securityType,nameOfCommodity,";
                        //        query += " unitType,totalPldgQty,freeBalQty,totalQty,Req_CREATEDATETIME)";
                        //        query += " values(:FileSequnceNo,:MEMBERPAN,:WEEKDAY,";
                        //        query += " :LASTWEEKDAY,:RECID,:Dmat,:AccountType,:UCC,:clientName,:PAN,:isin,:securityType,:nameOfCommodity,";
                        //        query += " :unitType,:totalPldgQty,:freeBalQty,:totalQty,:Req_CREATEDATETIME)";

                        //        OracleCommand cmd = new OracleCommand(query, conn);
                        //        cmd.CommandType = CommandType.Text;
                        //        cmd.Parameters.Add(":FileSequnceNo", OracleDbType.Char).Value = "0" + j.ToString();
                        //        cmd.Parameters.Add(":MEMBERPAN", OracleDbType.Char).Value = _clsReqHold.memberPan.ToString();
                        //        cmd.Parameters.Add(":WEEKDAY", OracleDbType.Char).Value = _clsReqHold.weekDay.ToString();
                        //        cmd.Parameters.Add(":LASTWEEKDAY", OracleDbType.Char).Value = _clsReqHold.lastWeekDay.ToString();
                        //        cmd.Parameters.Add(":RECID", OracleDbType.Char).Value = _clsReqHold.holdingData[l].recId.ToString();
                        //        cmd.Parameters.Add(":Dmat", OracleDbType.Char).Value = _clsReqHold.holdingData[l].dmat.ToString();
                        //        cmd.Parameters.Add(":AccountType", OracleDbType.Char).Value = _clsReqHold.holdingData[l].accountType.ToString();
                        //        cmd.Parameters.Add(":UCC", OracleDbType.Char).Value = _clsReqHold.holdingData[l].ucc.ToString();
                        //        cmd.Parameters.Add(":clientName", OracleDbType.Varchar2).Value = _clsReqHold.holdingData[l].clientName.ToString();
                        //        cmd.Parameters.Add(":PAN", OracleDbType.Char).Value = _clsReqHold.holdingData[l].pan.ToString();
                        //        cmd.Parameters.Add(":isin", OracleDbType.Char).Value = _clsReqHold.holdingData[l].isin.ToString();
                        //        cmd.Parameters.Add(":securityType", OracleDbType.Char).Value = _clsReqHold.holdingData[l].securityType.ToString();
                        //        cmd.Parameters.Add(":nameOfCommodity", OracleDbType.Varchar2).Value = _clsReqHold.holdingData[l].nameOfCommodity.ToString();
                        //        cmd.Parameters.Add(":unitType", OracleDbType.Char).Value = _clsReqHold.holdingData[l].unitType.ToString();
                        //        cmd.Parameters.Add(":totalPldgQty", OracleDbType.Char).Value = _clsReqHold.holdingData[l].totalPldgQty.ToString();
                        //        cmd.Parameters.Add(":freeBalQty", OracleDbType.Char).Value = _clsReqHold.holdingData[l].freeBalQty.ToString();
                        //        cmd.Parameters.Add(":totalQty", OracleDbType.Char).Value = _clsReqHold.holdingData[l].totalQty.ToString();
                        //        cmd.Parameters.Add(":Req_CREATEDATETIME", OracleDbType.Date).Value = Convert.ToDateTime(DateTime.Now);
                        //        cmd.ExecuteNonQuery();

                        //    }
                        //}
                        //bool Resp = NSEResponseHoldingFile(ResponseFileSave1);
                        //}
                    }
                    MessageBox.Show("Total File =" + Result1.Count() + "" + Environment.NewLine + "Success File Created = " + count);
                }
                pnlWait.Visible = false;
                lblTitle.Visible = true;
            }
            catch (Exception ex)
            {
                pnlWait.Visible = false;
                lblTitle.Visible = true;
                MessageBox.Show(ex.Message.ToString());
            }
            var ext = new List<string> { ".gz" };
            var myFiles = Directory
                .EnumerateFiles(pathTXT + LASTwEEKDARW, "*.*", SearchOption.AllDirectories)
                .Where(s => ext.Contains(Path.GetExtension(s).ToLowerInvariant())).ToList();
            //DirectoryInfo dinfo = new DirectoryInfo(pathTXT + LASTwEEKDARW);
            //FileInfo[] finfo = dinfo.GetFiles(".gz",SearchOption);
            DataTable dt = new DataTable();
            dt.Columns.Add("Select", typeof(bool));
            dt.Columns.Add("SequenceNo", typeof(string));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("Path", typeof(string));
            for (int i = 0; i < myFiles.Count(); i++)
            {
                dt.Rows.Add(true, i + 1, myFiles[i].ToString().Substring(myFiles[i].ToString().Length - 32), myFiles[i].ToString());
            }

            grdViewFile.DataSource = dt;
            grdViewFile.Columns["Path"].Visible = false;
            grdViewFile.Columns["FileName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdViewFile.Columns["SequenceNo"].ReadOnly = true;
            grdViewFile.Columns["FileName"].ReadOnly = true;
            grdViewFile.AllowUserToAddRows = false;
            pnlWait.Visible = false;
            lblTitle.Visible = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (grdViewFile.Rows.Count > 0)
            {
             
                string url = ConfigurationManager.AppSettings["UrlNse"].ToString();
                
                for (int i = 0; i < grdViewFile.Rows.Count; i++)
                {
                    //pnlWait.Visible = true;
                    //progressBar1.Maximum = 6;
                    //progressBar1.Value = i+1;
                    
                    bool isChecked = Convert.ToBoolean(grdViewFile.Rows[i].Cells["Select"].Value);
                    if(isChecked==true)
                    {
                       
                        var sFile = grdViewFile.Rows[i].Cells["Path"].Value.ToString();
                        string val = Convert.ToString(grdViewFile.Rows[i].Cells["FileName"].Value.ToString());
                        string[] Filter = val.Split('_');
                        string pan = Filter[0].ToString();
                        string Seq = Filter[3].ToString().Replace(".txt.gz", "");
                        string LASTwEEKDARW = Filter[2].ToString();

                        DateTime datetime = DateTime.ParseExact(Filter[2].ToString(), "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
                        string date = datetime.ToString("dd-MM-yyyy");


                        ClsUtlity clsUtility = new ClsUtlity();
                        string ResponseFileSave = AppDomain.CurrentDomain.BaseDirectory + "NSE_HoldingFileResponse";
                        if (!System.IO.Directory.Exists(ResponseFileSave + "\\" + LASTwEEKDARW))
                        {
                            System.IO.Directory.CreateDirectory(ResponseFileSave + "\\" + LASTwEEKDARW);
                        }
                        if (System.IO.File.Exists(ResponseFileSave + "\\" + LASTwEEKDARW + "\\" + pan + "_HS_Response_" + LASTwEEKDARW.Replace("-", "") + "_" + Seq + ".gz"))
                        {
                            pnlWait.Visible = false;
                            lblTitle.Visible = true;
                            MessageBox.Show("This File Already Exist");
                            return;
                            //System.IO.File.Delete(ResponseFileSave + "\\" + pan + "_HS_Response" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".Zip");
                        }
                        string ResponseFileSave1 = ResponseFileSave + "\\" + LASTwEEKDARW + "\\" + pan + "_HS_Response_" + LASTwEEKDARW.Replace("-", "") + "_" + Seq + ".gz";
                        string TOK = GetToken();
                        if (TOK == "")
                        {
                            MessageBox.Show("Token not Created");
                            return;
                        }
                        string TOKEN = "Bearer " + TOK;
                        txtToken.Text = TOKEN;
                        //var RespApi = clsUtility.GetUrlHoldingTest2(url + "/tradingHoldingUpload/1.0", TOKEN, sFile, true, ResponseFileSave1);
                        var RespApi = clsUtility.GetUrlHoldingTest(url + "/tradingHoldingUpload/1.0", TOKEN, sFile, true, ResponseFileSave1);
                        if(RespApi.ToString()!="")
                        {
                            RespApi.Replace(@"\", "");
                            using (StreamWriter sw = new StreamWriter(ResponseFileSave1))
                            {
                                sw.WriteLine(RespApi);
                            }
                        }
                    }
                    
                }
                pnlWait.Visible = false;
                lblTitle.Visible = true;
            }
            else 
            {
                MessageBox.Show("No Record ");
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string ResponseFileSave = AppDomain.CurrentDomain.BaseDirectory + "NSE_HoldingFileResponse//";
            var ext = new List<string> { cmbFileSeq.Text+".gz" };
            var myFiles = Directory
                .EnumerateFiles(ResponseFileSave + dateLastweek, "*.*", SearchOption.AllDirectories)
                .Where(s => ext.Contains(Path.GetExtension(s).ToLowerInvariant())).ToList();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {

        }
    }
}
