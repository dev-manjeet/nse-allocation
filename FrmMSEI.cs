using ChoETL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSEAllocation.Models;
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
    public partial class FrmMSEI : Form
    {
        DataTable dt = new DataTable();
        string Token = "";
        OpenFileDialog fdlg1 = new OpenFileDialog();
        string Uncompressoutputstr = "";
        public FrmMSEI()
        {
            InitializeComponent();
            pnlWait.Visible = false;
        }

        private void FrmMSEI_Load(object sender, EventArgs e)
        {
            Token = GetToken();
            pnlWait.Visible = false;
        }
        public string GetToken()
        {
            string Result = "";
            string path = AppDomain.CurrentDomain.BaseDirectory + "MSEILoginFile\\MSEILoginLog.txt";
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
                                        Result = Arr[0].ToString().Replace("Token=", "");
                                    }
                                    else
                                    {
                                        Result = Login();
                                    }
                                }
                                else
                                {
                                    Result = Login();
                                }

                            }
                            else
                            {
                                Result = Login();
                            }
                        }
                        else
                        {
                            Result = Login();
                        }

                    }
                    else
                    {
                        Result = Login();
                    }
                }
                else
                {

                    string pathdel = AppDomain.CurrentDomain.BaseDirectory + "MSEILoginLog";
                    if (System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.Delete(path);
                    }
                    Result = Login();
                }
            }
            else
            {
                Result = Login();
            }
            return Result;
        }
        public string Login()
        {
            string _result = "";

            //ClsAESEncryptDecrypt clsEncrpt = new ClsAESEncryptDecrypt();
            clsUtilityMSEI clsUtility = new clsUtilityMSEI();
            string path = AppDomain.CurrentDomain.BaseDirectory + "MSEILoginFile";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            //string LoginDatetime = "";
            string url = ConfigurationManager.AppSettings["UrlMSEI"].ToString();
            string mmcode = ConfigurationManager.AppSettings["MEMCODEMSEI"].ToString();
            string loginID = ConfigurationManager.AppSettings["LoginIdMSEI"].ToString();
            string Password = ConfigurationManager.AppSettings["PasswordMSEI"].ToString();
            string SecretKey = ConfigurationManager.AppSettings["SecretKeyMSEI"].ToString();

            string EncryptPassword = clsUtility.Encrypt_AES256(Password);

            try
            {

                string txtJSON = "{ ";
                txtJSON += Environment.NewLine;
                txtJSON += " \"memberCode\" : \"" + mmcode + "\",";
                txtJSON += Environment.NewLine;
                txtJSON += " \"loginId\" : \"" + loginID + "\",";
                txtJSON += Environment.NewLine;
                txtJSON += " \"password\" : \"" + clsUtility.Encrypt_AES256(Password) + "\"";
                txtJSON += Environment.NewLine;
                txtJSON += " }";

                string JSONStr = txtJSON.Replace("\"", "'");
                string RespApi = clsUtility.GetUrl(url + "login", "", JSONStr, true, ref Uncompressoutputstr);

                string StrResponse = RespApi.Replace("\\", "").Replace("\"", ""); 
                ClsLoginResp ClsResp = new ClsLoginResp();
                if (StrResponse.Contains("memberCode"))
                {
                    if (StrResponse.Contains("success"))
                    {
                        string[] val = StrResponse.Split(',');
                        if (val.Length > 0)
                        {
                            string Nval = val[2].Replace("'", "").Replace("token:", "");
                            if (Nval.ToString().Trim() != "")
                            {
                                if (System.IO.File.Exists(path + "\\MSEILoginLog.txt"))
                                {
                                    System.IO.File.Delete(path + "\\MSEILoginLog.txt");
                                }
                                _result = Nval;
                                System.IO.File.AppendAllText(path + "/MSEILoginLog.txt", "Token=" + Nval.ToString() + ",DateTime=" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Login Failed");
                    }

                }

                else
                {
                    _result = "";
                    //string errmsg = MasterError(ClsResp.code[0].ToString());
                    //MessageBox.Show("Login Fail." + Environment.NewLine + errmsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                return _result;





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
                Token = GetToken();

            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string pan = "";
                //string _date = DateTime.Now.ToString("ddMMyyyy");
                string pathTXT = AppDomain.CurrentDomain.BaseDirectory + "MSEIHoldingFile";
                if (txtfileupload.Text.ToString() != "")
                {
                    pnlWait.Visible = true;
                    lblTitle.Visible = true;
                    string path = txtfileupload.Text;
                    string CSVFilePath = Path.GetFullPath(path);
                    //string ReadCSV = System.IO.File.ReadAllText(CSVFilePath);
                    string[] allLines = System.IO.File.ReadAllLines(CSVFilePath);
                    int ToTALrECORD = allLines.Count() - 1;
                    List<MSEIHoldingDataFilter> DATEfILTER = new List<MSEIHoldingDataFilter>();
                    List<MSEIHoldingDataFilter> DATEfILTER_sort = new List<MSEIHoldingDataFilter>();
                    MSEIHoldingDataFilter clssort = new MSEIHoldingDataFilter();
                    for (int i = 0; i < allLines.Count(); i++)
                    {
                        clssort = new MSEIHoldingDataFilter();
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
                            clssort.action = "A";
                            DATEfILTER.Add(clssort);
                        }
                    }


                    //string LASTwEEKDARW = DATEfILTER.Max(x => x.Date.ToString());
                    var Result1 = DATEfILTER.GroupBy(x => x.Date).Distinct().ToList();
                    //String Result2 = Result1[Result1.Count() - 1].Key.ToString();
                    //string LASTwEEKDARW = DATEfILTER.Max(x => x.Date);
                    //if (LASTwEEKDARW != Result2)
                    //{
                    //    LASTwEEKDARW = "";
                    //    LASTwEEKDARW = Result2;
                    //}
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
                        string dat = sort[sort.Count() - j].date.ToString().Substring(0, 10);
                        //string dat = Result1[i].Key.ToString();
                        ClsReqMSEIHoldingFileSubmission _clsReqHold = new ClsReqMSEIHoldingFileSubmission()
                        {
                            memberPan = pan,
                            lastWeekDay = LASTwEEKDARW,
                            weekDay = dat,
                            holdingData = lstHolddata(dat)
                        };


                        string ResponseFileSave = AppDomain.CurrentDomain.BaseDirectory + "MSEIHoldingFileResponse";
                        pan = _clsReqHold.memberPan;
                        if (!System.IO.Directory.Exists(ResponseFileSave))
                        {
                            System.IO.Directory.CreateDirectory(ResponseFileSave);
                        }
                        if (System.IO.File.Exists(ResponseFileSave + "\\" + pan + "_MSEI_HS_Response_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt"))
                        {
                            pnlWait.Visible = false;
                            lblTitle.Visible = true;
                            MessageBox.Show("This File Already Exist");
                            return;
                            //System.IO.File.Delete(ResponseFileSave + "\\" + pan + "_HS_Response" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".Zip");
                        }
                        string ResponseFileSave1 = ResponseFileSave + "\\" + pan + "_MSEI_HS_Response_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt";

                        if (!System.IO.Directory.Exists(pathTXT))
                        {
                            System.IO.Directory.CreateDirectory(pathTXT);
                        }
                        if (System.IO.File.Exists(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".json"))
                        {
                            System.IO.File.Delete(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".json");
                        }
                        //if (System.IO.File.Exists(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt.gz"))
                        //{
                        //    System.IO.File.Delete(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".txt.gz");
                        //}
                        string JSON = JsonConvert.SerializeObject(_clsReqHold);


                        using (StreamWriter sw = new StreamWriter(pathTXT + "\\" + _clsReqHold.memberPan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".json", true))
                        {
                            sw.Write(JSON);
                        }
                        bool Result = CreateZip(pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".json", pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + "");

                        clsUtilityMSEI clsUtility = new clsUtilityMSEI();
                        var sFile = pathTXT + "\\" + pan + "_HS_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".gz";
                        string url = ConfigurationManager.AppSettings["UrlMSEI"].ToString();
                        string RespApi = clsUtilityMSEI.GetUrlCASHBANK(url + "/tradingHoldingUpload", GetToken(), sFile, true, ref Uncompressoutputstr);
                        //string RespApi = clsUtility.GetUrlCASHBANK(url + "/tradingHoldingUpload/1.0",GetToken(), sFile, true, ref Uncompressoutputstr);
                        //var RespApi = clsUtility.GetUrl(url + "tradingHoldingUpload", GetToken(), sFile, true, ref Uncompressoutputstr);
                        if (RespApi != "")
                        {
                            string StrResponse = RespApi.Replace("\\", "");
                            // clsMSEIHoldingResp resp = new clsMSEIHoldingResp();
                            using (StreamWriter sw = new StreamWriter(ResponseFileSave1))
                            {
                                sw.Write(StrResponse);
                            }

                            //for (int k = 0; i < resp.holdingData.Count(); k++)
                            //{
                            //    if (k == 0)
                            //    {
                            //        HeaderTag = "memberPan,";
                            //    }
                            //}


                        }


                    }
                    pnlWait.Visible = false;
                    lblTitle.Visible = true;



                    // MessageBox.Show("Total Record Read from Upload file " + dt.Rows.Count);
                }
                else
                {
                    MessageBox.Show("No Record found");
                }

            }
            catch (Exception ex)
            {
                pnlWait.Visible = false;
                lblTitle.Visible = true;
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public List<MSEIHoldingData> lstHolddata(string _weekdate)
        {
            List<MSEIHoldingData> lstResp = new List<MSEIHoldingData>();
            List<MSEIHoldingDataFilter> lstRespfilter = new List<MSEIHoldingDataFilter>();
            List<MSEIHoldingDataFilter> lstRespfilter1 = new List<MSEIHoldingDataFilter>();
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
                        MSEIHoldingDataFilter _hold = new MSEIHoldingDataFilter()
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
                            action = "A",


                        };
                        lstRespfilter.Add(_hold);
                    }

                }
                var result = lstRespfilter.Where(h => h.Date == _weekdate).ToList();
                MSEIHoldingData cls = new MSEIHoldingData();
                for (int i = 0; i < result.Count; i++)
                {
                    cls = new MSEIHoldingData()
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
                        action = result[i].action.ToString(),
                    };
                    lstResp.Add(cls);
                }



            }
            return lstResp;
        }
    }
}
