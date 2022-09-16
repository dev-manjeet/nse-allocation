using Newtonsoft.Json;
using NSEAllocation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NSEAllocation.Models;
using NSEAllocation;
using Oracle.ManagedDataAccess.Client;
using System.Threading;

namespace NSEAllocation
{
    public partial class FrmNse : Form
    {
        DataTable dt = new DataTable();
        string Token = "";
        OpenFileDialog fdlg1 = new OpenFileDialog();
        string Uncompressoutputstr = "";
        public FrmNse()
        {
            InitializeComponent();
            Token = GetToken();
            pnlWait.Visible = false;
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


        //public List<ClsCashEquiluent> lstCashEqldata(string _weekdate)
        //{
        //    List<ClsCashEquiluent> lstResp = new List<ClsCashEquiluent>();
        //    List<ClsCashEquiluent> lstRespfilter = new List<ClsCashEquiluent>();
        //    List<ClsCashEquiluent> lstRespfilter1 = new List<ClsCashEquiluent>();
        //    string path = txtfileupload.Text;
        //    string CSVFilePath = Path.GetFullPath(path);

        //    string[] allLines = System.IO.File.ReadAllLines(CSVFilePath);
        //    if (allLines.Length > 0)
        //    {
        //        for (int i = 0; i < allLines.Length; i++)
        //        {
        //            if (i != 0)
        //            {

        //                string[] Arr = allLines[i].Split(',');
        //                //var lst = Arr.ToList().Where(x => x.Length > 0).ToList();
        //                HoldingDataFilter _hold = new HoldingDataFilter()
        //                {
        //                    recId = Convert.ToString(i),
        //                    Date = Arr[1].ToString(),
        //                    dmat = Arr[2].ToString(),
        //                    accountType = Arr[3].ToString(),
        //                    ucc = Arr[4].ToString(),
        //                    clientName = Arr[5].ToString(),
        //                    pan = Arr[6].ToString(),
        //                    isin = Arr[7].ToString(),
        //                    securityType = Arr[8].ToString(),
        //                    nameOfCommodity = Arr[9].ToString(),
        //                    unitType = Arr[10].ToString(),
        //                    totalPldgQty = Arr[11].ToString(),
        //                    freeBalQty = Arr[12].ToString(),
        //                    totalQty = Arr[13].ToString(),


        //                };
        //                lstRespfilter.Add(_hold);
        //            }

        //        }
        //        var result = lstRespfilter.Where(h => h.Date == _weekdate).ToList();
        //        HoldingData cls = new HoldingData();
        //        for (int i = 0; i < result.Count; i++)
        //        {
        //            cls = new HoldingData()
        //            {
        //                recId = result[i].recId.ToString(),//Convert.ToString(i + 1),
        //                dmat = result[i].dmat.ToString(),
        //                accountType = result[i].accountType.ToString(),
        //                ucc = result[i].ucc.ToString(),
        //                clientName = result[i].clientName.ToString(),
        //                pan = result[i].pan.ToString(),
        //                isin = result[i].isin.ToString(),
        //                securityType = result[i].securityType.ToString(),
        //                nameOfCommodity = result[i].nameOfCommodity.ToString(),
        //                unitType = result[i].unitType.ToString(),
        //                totalPldgQty = result[i].totalPldgQty.ToString(),
        //                freeBalQty = result[i].freeBalQty.ToString(),
        //                totalQty = result[i].totalQty.ToString(),
        //            };
        //            lstResp.Add(cls);
        //        }



        //    }
        //    return lstResp;
        //}
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

        public string LogOut()
        {
            string StrResponse = "";
            try
            {


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
            return StrResponse;
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

        private void btnBankBalUpld_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Bank Account Balance File Dialog";
                //fdlg.InitialDirectory = @"c:\";
                fdlg.InitialDirectory = @"G:\Prashant\doc\NSE API Circular";
                fdlg.Filter = "All files (*.csv)|*.*|All files (*.CSV)|*.csv";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtBankBal.Text = fdlg.FileName;
                }
                if (!fdlg.FileName.ToUpper().Contains(".CSV"))
                {
                    MessageBox.Show("Please Upload Only CSV Format");
                    txtBankBal.Text = "";
                    return;
                }
                Token = GetToken();

            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void btnBankAccBalSubmit_Click(object sender, EventArgs e)
        {
            string pathTXT = AppDomain.CurrentDomain.BaseDirectory + "BankAccountBalanceFile";
            string pan = "";
            string fILEdATE = "";
            if (txtBankBal.Text.ToString() != "")
            {
                pnlWait.Visible = true;
                lblTitle.Visible = true;
                string path = txtBankBal.Text;
                string[] _Name = path.Substring(path.Length - 26).Split('_');
                pan = _Name[0];
                fILEdATE = _Name[2].Replace(".csv", "").Replace(".CSV", "");
                string CSVFilePath = Path.GetFullPath(path);

                string[] allLines = System.IO.File.ReadAllLines(CSVFilePath);
                int ToTALrECORD = allLines.Count() - 1;
                //string HeaderTag="";
                //string[] HeaderTag;
                ClsBankBalanceData BankBalanceData = new ClsBankBalanceData();
                List<ClsBankBalance> listBankBalanceData = new List<ClsBankBalance>();
                ClsBankBalance cls = new ClsBankBalance();
                string week1 = "";
                string week2 = "";
                string week3 = "";
                string week4 = "";
                string week5 = "";
                string week6 = "";
                for (int i = 0; i < allLines.Length; i++)
                {
                    string[] Arr = allLines[i].Split(',');
                    if (i == 0)
                    {
                        if (Arr[0].ToString() != "Bank Account No.")
                        {
                            MessageBox.Show("Please Select Bank Account Balance File");
                            return;
                        }
                        //HeaderTag = allLines[i].Split(','); ;
                        week1 = Arr[3].ToString();
                        week2 = Arr[4].ToString();
                        week3 = Arr[5].ToString();
                        week4 = Arr[6].ToString();
                        week5 = Arr[7].ToString();
                        week6 = Arr[8].ToString();
                    }
                    else
                    {
                        cls = new ClsBankBalance()
                        {
                            BankAccountNo = Arr[0].Replace("'", ""),
                            IFSC = Arr[1].ToString(),
                            BankAccountType = Arr[2].ToString(),
                            WeekDay1 = Arr[3].ToString(),
                            WeekDay2 = Arr[4].ToString(),
                            WeekDay3 = Arr[5].ToString(),
                            WeekDay4 = Arr[6].ToString(),
                            WeekDay5 = Arr[7].ToString(),
                            WeekDay6 = Arr[8].ToString(),

                        };
                        listBankBalanceData.Add(cls);
                    }
                }
                BankBalanceData.BankBalanceData = listBankBalanceData;
                string JSON = JsonConvert.SerializeObject(BankBalanceData);
                string jsonFilter = JSON.Replace("WeekDay1", week1).Replace("WeekDay2", week2).Replace("WeekDay3", week3)
                                    .Replace("WeekDay4", week4).Replace("WeekDay5", week5).Replace("WeekDay6", week6)
                                    .Replace("BankAccountNo", "Bank Account No").Replace("BankAccountType", "Bank Account Type");

                //string json = jsonFilter;
                string ResponseFileSave = AppDomain.CurrentDomain.BaseDirectory + "BankBalanceFileResponse";
                if (!System.IO.Directory.Exists(ResponseFileSave))
                {
                    System.IO.Directory.CreateDirectory(ResponseFileSave);
                }
                if (System.IO.File.Exists(ResponseFileSave + "\\" + pan + "_BA_Response_" + fILEdATE.Replace("-", "") + ".gz"))
                {
                    pnlWait.Visible = false;
                    lblTitle.Visible = true;
                    MessageBox.Show("This File Already Exist");
                    return;
                    //System.IO.File.Delete(ResponseFileSave + "\\" + pan + "_HS_Response" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".Zip");
                }
                string ResponseFileSave1 = ResponseFileSave + "\\" + pan + "_BA_Response_" + fILEdATE.Replace("-", "") + ".gz";

                if (!System.IO.Directory.Exists(pathTXT))
                {
                    System.IO.Directory.CreateDirectory(pathTXT);
                }
                if (System.IO.File.Exists(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt"))
                {
                    System.IO.File.Delete(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt");
                }
                if (System.IO.File.Exists(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + "txt.gz"))
                {
                    System.IO.File.Delete(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + "txt.gz");
                }



                using (StreamWriter sw = new StreamWriter(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt", true))
                {
                    sw.Write(jsonFilter);
                }
                bool Result = CreateZip(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt", pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt");

                ClsUtlity clsUtility = new ClsUtlity();
                var sFile = pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt.gz";
                string url = ConfigurationManager.AppSettings["UrlNse"].ToString();
                clsUtilityMSEI CLS = new clsUtilityMSEI();
                //string RespApi = clsUtility. GetUrlCASHBANK(url + "/tradingEodBalanceUpload/version/1.0", "Bearer " + GetToken(), sFile, sFile, true, ref Uncompressoutputstr);
                //string RespApi = CLS.GetUrl(url + "/tradingEodBalanceUpload/1.0", "Bearer " + GetToken(), sFile, true, ref Uncompressoutputstr);
                string RespApi = clsUtility.GetUrlNSEBankbalance(url + "/tradingEodBalanceUpload/1.0", "Bearer " + GetToken(), sFile, true, ResponseFileSave1);
                if (RespApi != "True")
                {
                    RespApi = clsUtility.GetUrlHolding(url + "/tradingEodBalanceUpload/1.0", "Bearer " + GetToken(), sFile, true, ResponseFileSave1);
                }
                //string StrResponse = RespApi.Replace("\\", "")
                pnlWait.Visible = false;
                lblTitle.Visible = true;
                if (RespApi != "")
                {
                    if (RespApi.Contains("Internal Server Error - Read") == true)
                    {
                        pnlWait.Visible = false;
                        lblTitle.Visible = true;
                        MessageBox.Show(RespApi);
                    }
                }

            }
            pnlWait.Visible = false;
            lblTitle.Visible = true;
        }

        private void btnuploadHold_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Trading Holding File Dialog";
                //fdlg.InitialDirectory = @"c:\";
                fdlg.InitialDirectory = @"G:\Prashant\doc\NSE API Circular";
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
                Token = GetToken();

            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message.ToString());
            }

        }

        private void btnsubmit_Click_1(object sender, EventArgs e)
        {
            try
            {
                string pan = "";
                //string _date = DateTime.Now.ToString("ddMMyyyy");
                string pathTXT = AppDomain.CurrentDomain.BaseDirectory + "HoldingFile";
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
                    string LASTwEEKDARW = LASTwEEKDARW1.Substring(0, 10);
                    //if (LASTwEEKDARW != Result2)
                    //{
                    //    LASTwEEKDARW = "";
                    //    LASTwEEKDARW = Result2;
                    //}
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


                        string ResponseFileSave = AppDomain.CurrentDomain.BaseDirectory + "HoldingFileResponse";
                        pan = _clsReqHold.memberPan;
                        if (!System.IO.Directory.Exists(ResponseFileSave))
                        {
                            System.IO.Directory.CreateDirectory(ResponseFileSave);
                        }
                        if (System.IO.File.Exists(ResponseFileSave + "\\" + pan + "_HS_Response_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".gz"))
                        {
                            pnlWait.Visible = false;
                            lblTitle.Visible = true;
                            MessageBox.Show("This File Already Exist");
                            return;
                            //System.IO.File.Delete(ResponseFileSave + "\\" + pan + "_HS_Response" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".Zip");
                        }
                        string ResponseFileSave1 = ResponseFileSave + "\\" + pan + "_HS_Response_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".gz";

                        if (!System.IO.Directory.Exists(pathTXT))
                        {
                            System.IO.Directory.CreateDirectory(pathTXT);
                        }
                        if (System.IO.File.Exists(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt"))
                        {
                            System.IO.File.Delete(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt");
                        }
                        if (System.IO.File.Exists(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt.gz"))
                        {
                            System.IO.File.Delete(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt.gz");
                        }
                        string JSON = JsonConvert.SerializeObject(_clsReqHold);


                        using (StreamWriter sw = new StreamWriter(pathTXT + "\\" + _clsReqHold.memberPan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt", true))
                        {
                            sw.Write(JSON);
                        }
                        bool Result = CreateZip(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt", pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt");

                        ClsUtlity clsUtility = new ClsUtlity();
                        var sFile = pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt.gz";
                        string url = ConfigurationManager.AppSettings["UrlNse"].ToString();
                        string TOK = GetToken();
                        if (TOK == "")
                        {
                            MessageBox.Show("Token not Created");
                            return;
                        }
                        string TOKEN = "Bearer " + TOK;

                        var RespApi = clsUtility.GetUrlHolding(url + "/tradingHoldingUpload/1.0", TOKEN, sFile, true, ResponseFileSave1);
                        if (RespApi != "True")
                        {
                            RespApi = clsUtility.GetUrlHolding(url + "/tradingHoldingUpload/1.0", TOKEN, sFile, true, ResponseFileSave1);
                        }
                        if (RespApi == "True")
                        {
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
                        }
                    }

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
            pnlWait.Visible = false;
            lblTitle.Visible = true;
        }

        private void btnuploadCashEqulaint_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Trading Cash File Dialog";
                fdlg.InitialDirectory = @"c:\";
                //fdlg.InitialDirectory = @"G:\Prashant\doc\NSE API Circular";
                fdlg.Filter = "All files (*.csv)|*.*|All files (*.CSV)|*.csv";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtCashEqulaint.Text = fdlg.FileName;
                }
                if (!fdlg.FileName.ToUpper().Contains(".CSV"))
                {
                    MessageBox.Show("Please Upload Only CSV Format");
                    txtfileupload.Text = "";
                    return;
                }
                Token = GetToken();

            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message.ToString());
            }

        }

        private void btnCashEqulaintSubmit_Click(object sender, EventArgs e)
        {
            Thread threadInput = new Thread(CashEqulivantProcess);
            threadInput.Start();
        }
        public void CashEqulivantProcess()
        {
            try
            {
                string pan = "";
                //string _date = DateTime.Now.ToString("ddMMyyyy");
                string pathTXT = AppDomain.CurrentDomain.BaseDirectory + "CashEquivalent";
                if (txtCashEqulaint.Text.ToString() != "")
                {
                    if (InvokeRequired)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {


                            pnlWait.Visible = true;
                            lblTitle.Visible = true;
                            string path = txtCashEqulaint.Text;
                            string CSVFilePath = Path.GetFullPath(path);
                            //string ReadCSV = System.IO.File.ReadAllText(CSVFilePath);
                            string[] allLines = System.IO.File.ReadAllLines(CSVFilePath);
                            int ToTALrECORD = allLines.Count() - 1;
                            List<ClsCashEquiluent> DATEfILTER = new List<ClsCashEquiluent>();
                            List<ClsCashEquiluent> DATEfILTER_sort = new List<ClsCashEquiluent>();
                            ClsCashEquiluent clssort = new ClsCashEquiluent();
                            string Header = "";
                            string ContentTag = "";
                            for (int i = 0; i < allLines.Count(); i++)
                            {
                                clssort = new ClsCashEquiluent();
                                string[] Arr = allLines[i].Split(',');
                                if (i == 0)
                                {
                                    if (Arr[0].ToString() != "TRADING MEMBER PAN")
                                    {
                                        MessageBox.Show("Please Upload Only Correct file Name Cash Equivalent");
                                        txtfileupload.Text = "";
                                        return;
                                    }
                                    Header = allLines[i];
                                }
                                if (i != 0)
                                {
                                    pan = Arr[0].ToString();

                                    clssort.TRADING_MEMBER_PAN = Arr[0].ToString();
                                    clssort.DATE = Arr[1].ToString();
                                    clssort.UNIQUE_Client_Code = Arr[2].ToString();
                                    clssort.Client_PAN = Arr[3].ToString();
                                    clssort.Client_Name = Arr[4].ToString();
                                    clssort.MTF_NON_MTF_INDICATOR = Arr[5].ToString();
                                    clssort.FINANCIAL_LEDGER_BALANCE_A = Arr[6].ToString();
                                    clssort.FINANCIAL_LEDGER_BALANCE_CLEAR_B = Arr[7].ToString();
                                    clssort.Peak_Financial_Ledger_Balance_clear_C = Arr[8].ToString();
                                    clssort.FINANCIAL_LEDGER_BALANCE_MCX = Arr[9].ToString();
                                    clssort.FINANCIAL_LEDGER_BALANCE_NCDEX = Arr[10].ToString();
                                    clssort.FINANCIAL_LEDGER_BALANCE_Icex = Arr[11].ToString();
                                    clssort.BANK_GUARANTEE_BG = Arr[12].ToString();
                                    clssort.FIXED_DEPOSIT_RECEIPT_Fdr = Arr[13].ToString();

                                    clssort.GOVERNMENT_OF_INDIA_SecuRITIES_GSEC = Arr[14].ToString();
                                    clssort.GILT_FUNDS = Arr[15].ToString();
                                    clssort.Credit_entry_in_ledger_in_lieu_of_EPI = Arr[16].ToString();
                                    clssort.Pool_Account = Arr[17].ToString();
                                    clssort.Uncleared_Cheques = Arr[18].ToString();
                                    clssort.Value_of_Commodities = Arr[19].ToString();
                                    clssort.Cash_Collateral_for_MTF_Positions = Arr[20].ToString();
                                    clssort.Unclaimed_Unsettled_Client_Funds = Arr[21].ToString();
                                    clssort.Client_Bank_Account_No = Arr[22].ToString();
                                    clssort.LAST_SETTLEMENT_DATE = Arr[23].ToString();
                                    clssort.ES_INFORMATION_TYPE = Arr[24].ToString();
                                    clssort.Value = Arr[25].ToString();

                                    DATEfILTER.Add(clssort);
                                }
                            }


                            //string LASTwEEKDARW = DATEfILTER.Max(x => x.DATE.ToString());
                            var Result1 = DATEfILTER.GroupBy(x => x.DATE).Distinct().ToList();
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
                            string LASTwEEKDARW = LASTwEEKDARW1.Substring(0, 10);

                            for (int i = 0; i < Result1.Count(); i++)
                            {
                                int j = i + 1;


                                progressBar1.Maximum = 6;
                                progressBar1.Value = j;
                                if (!System.IO.Directory.Exists(pathTXT))
                                {
                                    System.IO.Directory.CreateDirectory(pathTXT);
                                }
                                if (System.IO.File.Exists(pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv"))
                                {
                                    System.IO.File.Delete(pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv");
                                }
                                if (System.IO.File.Exists(pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + "csv.gz"))
                                {
                                    System.IO.File.Delete(pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + "csv.gz");
                                }
                                ContentTag = "";
                                string dat = sort[sort.Count() - j].date.ToString().Substring(0, 10);
                                //string dat = Result1[i].Key.ToString();
                                DATEfILTER_sort = DATEfILTER.Where(h => h.DATE == dat).ToList();
                                for (int k = 0; k < DATEfILTER_sort.Count(); k++)
                                {
                                    ContentTag = string.Empty;
                                    ContentTag = DATEfILTER_sort[k].TRADING_MEMBER_PAN + "," + DATEfILTER_sort[k].DATE + ",";
                                    ContentTag += DATEfILTER_sort[k].UNIQUE_Client_Code + "," + DATEfILTER_sort[k].Client_PAN + ",";
                                    ContentTag += DATEfILTER_sort[k].Client_Name + "," + DATEfILTER_sort[k].MTF_NON_MTF_INDICATOR + ",";
                                    ContentTag += DATEfILTER_sort[k].FINANCIAL_LEDGER_BALANCE_A + "," + DATEfILTER_sort[k].FINANCIAL_LEDGER_BALANCE_CLEAR_B + ",";
                                    ContentTag += DATEfILTER_sort[k].Peak_Financial_Ledger_Balance_clear_C + "," + DATEfILTER_sort[k].FINANCIAL_LEDGER_BALANCE_MCX + ",";
                                    ContentTag += DATEfILTER_sort[k].FINANCIAL_LEDGER_BALANCE_NCDEX + "," + DATEfILTER_sort[k].FINANCIAL_LEDGER_BALANCE_Icex + ",";
                                    ContentTag += DATEfILTER_sort[k].BANK_GUARANTEE_BG + "," + DATEfILTER_sort[k].FIXED_DEPOSIT_RECEIPT_Fdr + ",";
                                    ContentTag += DATEfILTER_sort[k].GOVERNMENT_OF_INDIA_SecuRITIES_GSEC + "," + DATEfILTER_sort[k].GILT_FUNDS + ",";
                                    ContentTag += DATEfILTER_sort[k].Credit_entry_in_ledger_in_lieu_of_EPI + "," + DATEfILTER_sort[k].Pool_Account + ",";
                                    ContentTag += DATEfILTER_sort[k].Uncleared_Cheques + "," + DATEfILTER_sort[k].Value_of_Commodities + ",";
                                    ContentTag += DATEfILTER_sort[k].Cash_Collateral_for_MTF_Positions + "," + DATEfILTER_sort[k].Unclaimed_Unsettled_Client_Funds + ",";
                                    ContentTag += DATEfILTER_sort[k].Client_Bank_Account_No.Replace("'", "") + "," + DATEfILTER_sort[k].LAST_SETTLEMENT_DATE + ",";
                                    ContentTag += DATEfILTER_sort[k].ES_INFORMATION_TYPE + "," + DATEfILTER_sort[k].Value + Environment.NewLine; ;

                                    if (k == 0)
                                    {
                                        using (StreamWriter sw = new StreamWriter(pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv", true))
                                        {
                                            sw.Write(Header.ToUpper() + Environment.NewLine);
                                        }
                                        using (StreamWriter sw = new StreamWriter(pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv", true))
                                        {
                                            sw.Write(ContentTag);
                                        }
                                    }
                                    else

                                    {
                                        using (StreamWriter sw = new StreamWriter(pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv", true))
                                        {
                                            sw.Write(ContentTag);
                                        }
                                    }

                                }
                                string ResponseFileSave = AppDomain.CurrentDomain.BaseDirectory + "CashEquivalentFileResponse";

                                if (!System.IO.Directory.Exists(ResponseFileSave))
                                {
                                    System.IO.Directory.CreateDirectory(ResponseFileSave);
                                }
                                if (System.IO.File.Exists(ResponseFileSave + "\\" + pan + "_CE_Response_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".gz"))
                                {
                                    //pnlWait.Visible = false;
                                    // lblTitle.Visible = true;
                                    MessageBox.Show("This File Already Exist");
                                    return;
                                    //System.IO.File.Delete(ResponseFileSave + "\\" + pan + "_HS_Response" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".Zip");
                                }
                                string ResponseFileSave1 = ResponseFileSave + "\\" + pan + "_CE_Response_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".gz";

                                //string JSON = JsonConvert.SerializeObject(DATEfILTER_sort);





                                bool Result = CreateZip(pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv", pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv");

                                ClsUtlity clsUtility = new ClsUtlity();
                                var sFile = pathTXT + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv.gz";
                                string url = ConfigurationManager.AppSettings["UrlNse"].ToString();

                                string RespApi = clsUtility.GetUrlHolding(url + "/tradingCashAndCashEquivalentUpload/1.0", "Bearer " + GetToken(), sFile, true, ResponseFileSave1);
                                if (RespApi != "True")
                                {
                                   // RespApi = clsUtility.GetUrlHolding(url + "/tradingCashAndCashEquivalentUpload/1.0", "Bearer " + GetToken(), sFile, true, ResponseFileSave1);
                                }
                                //string StrResponse = RespApi.Replace("\\", "");
                                if (RespApi != "")
                                {
                                    pnlWait.Visible = false;
                                    lblTitle.Visible = true;
                                    //MessageBox.Show("File UplOADED TO NSE");
                                }
                                else
                                {

                                }
                            }




                        }));
                    }
                }
                else
                {
                    MessageBox.Show("No Record found");
                    pnlWait.Visible = false;
                    lblTitle.Visible = true;
                }

            }
            catch (Exception ex)
            {
                pnlWait.Visible = false;
                lblTitle.Visible = true;
                MessageBox.Show(ex.Message.ToString());
            }
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
        public bool NSEResponseHoldingFile(string sourceFile)
        {
            bool ResultResp = false;
            try
            {
                if (sourceFile.ToString() != "")
                {
                    string ResponseFileSaveUnzip = AppDomain.CurrentDomain.BaseDirectory + "HoldingFileResponseUnzip";
                    if (!Directory.Exists(ResponseFileSaveUnzip))
                    {
                        Directory.CreateDirectory(ResponseFileSaveUnzip);
                    }
                    string path = sourceFile;
                    string zipped_path = path;
                    string[] val = path.Substring(path.Length - 38).Split('_');
                    string SeqNo = val[4].Replace(".gz", "");
                    string unzipped_path = ResponseFileSaveUnzip.Replace(@"\", "/");
                    string zipFileName = @"" + unzipped_path + "/" + path.Substring(path.Length - 38).Replace(".gz", "").Replace("\\", "");
                    bool Result = CreateUnZip(zipped_path, zipFileName);
                    if (Result == true)
                    {
                        string[] allLines = System.IO.File.ReadAllLines(zipFileName + "/" + path.Substring(path.Length - 38).Replace(".gz", "").Replace("\\", ""));
                        if (allLines.Length > 0)
                        {
                            if ((allLines.Contains("success") == true) && (allLines.Contains("601") == true))
                            {
                                ClsNSEHoldingSuccessResponse obj = new ClsNSEHoldingSuccessResponse();

                                obj = JsonConvert.DeserializeObject<ClsNSEHoldingSuccessResponse>(allLines[0].ToString());
                                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                                {
                                    conn.Open();
                                    for (int l = 0; l < obj.holdingData.Count(); l++)
                                    {

                                        string query = "insert into NSE_HOLDING_UPLOAD_RESP (FILESEQUNCENO,MEMBERPAN,WEEKDAY," +
                                            "LASTWEEKDAY,RECID,hdrCode,HDRSTATUS,ackId,CODE,STATUS,RESP_CREATEDATETIME)";
                                        query += " values(:FILESEQUNCENO,:MEMBERPAN,:WEEKDAY," +
                                            ":LASTWEEKDAY,:RECID,:hdrCode,:HDRSTATUS,:ackId,:CODE,:STATUS,:RESP_CREATEDATETIME)";



                                        OracleCommand cmd = new OracleCommand(query, conn);
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Add(":FILESEQUNCENO", OracleDbType.Char).Value = SeqNo;
                                        cmd.Parameters.Add(":MEMBERPAN", OracleDbType.Char).Value = obj.memberPan.ToString();
                                        cmd.Parameters.Add(":WEEKDAY", OracleDbType.Char).Value = obj.weekDay.ToString();
                                        cmd.Parameters.Add(":LASTWEEKDAY", OracleDbType.Char).Value = obj.lastWeekDay.ToString();
                                        cmd.Parameters.Add(":RECID", OracleDbType.Char).Value = obj.holdingData[l].recId.ToString();
                                        cmd.Parameters.Add(":hdrCode", OracleDbType.Char).Value = obj.hdrCode[0].ToString();
                                        cmd.Parameters.Add(":HDRSTATUS", OracleDbType.Varchar2).Value = obj.hdrStatus[0].ToString();
                                        cmd.Parameters.Add(":ackId", OracleDbType.Char).Value = obj.ackId.ToString();
                                        cmd.Parameters.Add(":CODE", OracleDbType.Varchar2).Value = obj.hdrCode[0].ToString();
                                        cmd.Parameters.Add(":STATUS", OracleDbType.Varchar2).Value = obj.hdrStatus[0].ToString();
                                        cmd.Parameters.Add(":RESP_CREATEDATETIME", OracleDbType.Date).Value = Convert.ToDateTime(DateTime.Now);

                                        cmd.ExecuteNonQuery();

                                    }
                                }
                            }
                            else
                            {
                                ClsNSEHoldingFailedResponse obj = new ClsNSEHoldingFailedResponse();

                                obj = JsonConvert.DeserializeObject<ClsNSEHoldingFailedResponse>(allLines[0].ToString());
                                using (OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["SMCoffice"].ConnectionString))
                                {
                                    conn.Open();
                                    for (int l = 0; l < obj.holdingData.Count(); l++)
                                    {

                                        string query = "insert into NSE_HOLDING_UPLOAD_RESP (FILESEQUNCENO,MEMBERPAN,WEEKDAY," +
                                            "LASTWEEKDAY,RECID,hdrCode,HDRSTATUS,ackId,CODE,STATUS,RESP_CREATEDATETIME)";
                                        query += " values(:FILESEQUNCENO,:MEMBERPAN,:WEEKDAY," +
                                            ":LASTWEEKDAY,:RECID,:hdrCode,:HDRSTATUS,:ackId,:CODE,:STATUS,:RESP_CREATEDATETIME)";



                                        OracleCommand cmd = new OracleCommand(query, conn);
                                        cmd.CommandType = CommandType.Text;
                                        cmd.Parameters.Add(":FILESEQUNCENO", OracleDbType.Char).Value = SeqNo;
                                        cmd.Parameters.Add(":MEMBERPAN", OracleDbType.Char).Value = obj.memberPan.ToString();
                                        cmd.Parameters.Add(":WEEKDAY", OracleDbType.Char).Value = obj.weekDay.ToString();
                                        cmd.Parameters.Add(":LASTWEEKDAY", OracleDbType.Char).Value = obj.lastWeekDay.ToString();
                                        cmd.Parameters.Add(":RECID", OracleDbType.Char).Value = obj.holdingData[l].recId.ToString();
                                        cmd.Parameters.Add(":hdrCode", OracleDbType.Char).Value = obj.hdrCode[0].ToString();
                                        cmd.Parameters.Add(":HDRSTATUS", OracleDbType.Varchar2).Value = obj.hdrStatus[0].ToString();
                                        cmd.Parameters.Add(":ackId", OracleDbType.Char).Value = obj.ackId.ToString();
                                        cmd.Parameters.Add(":CODE", OracleDbType.Varchar2).Value = obj.hdrCode[0].ToString();
                                        cmd.Parameters.Add(":STATUS", OracleDbType.Varchar2).Value = obj.hdrStatus[0].ToString();
                                        cmd.Parameters.Add(":RESP_CREATEDATETIME", OracleDbType.Date).Value = Convert.ToDateTime(DateTime.Now);

                                        cmd.ExecuteNonQuery();

                                    }
                                }
                            }

                        }
                    }




                }
            }

            catch (Exception ex)
            {

                ResultResp = false;
            }
            return ResultResp;
        }
        private void btnHoldingShowResult_Click(object sender, EventArgs e)
        {
            FrmConvertHoldingCsv frm = new FrmConvertHoldingCsv();
            frm.ShowDialog();

        }

        private void btnBankBalanceShowResult_Click(object sender, EventArgs e)
        {

        }

        private void btnCashEqulantShowResult_Click(object sender, EventArgs e)
        {

        }

        private void FrmNse_Load(object sender, EventArgs e)
        {

        }
    }
}