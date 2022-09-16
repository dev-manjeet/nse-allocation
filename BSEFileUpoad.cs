using Newtonsoft.Json;
using NSEAllocation.Models;
using NSEAllocation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NSEAllocation
{
    public partial class BSEFileUpoad : Form
    {
        DataTable dt = new DataTable();
        string Token = "";
        OpenFileDialog fdlg1 = new OpenFileDialog();
        //string Uncompressoutputstr = "";
        public BSEFileUpoad()
        {
            InitializeComponent();
            Token = GetToken();
            pnlWait.Visible = false;
        }

        private void btnuploadHold_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Trading Holding File Dialog";
                //fdlg.InitialDirectory = @"c:\";
                fdlg.InitialDirectory = @"G:\Prashant\doc\NSE API Circular";
                fdlg.Filter = "All files (*.zip)|*.*|All files (*.zip)|*.zip";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtfileupload.Text = fdlg.FileName;
                }
                if (!fdlg.FileName.ToUpper().Contains(".ZIP"))
                {
                    MessageBox.Show("Please Upload Only Zip Files");
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
                string pathTXT = AppDomain.CurrentDomain.BaseDirectory + "HoldingFile";
                if (txtfileupload.Text.ToString() != "")
                {
                    pnlWait.Visible = true;
                    lblTitle.Visible = true;

                    ClsUtlity clsUtility = new ClsUtlity();
                    string url = ConfigurationManager.AppSettings["BSEUrl"].ToString();
                    string Postadate = txtfileupload.Text.ToString();
                    string[] splitdate = Postadate.Split('_');
                    string datval = splitdate[splitdate.Length - 2].ToString();
                    DateTime datetime = DateTime.ParseExact(datval, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string date = datetime.ToString("dd-MM-yyyy");
                    var RespApi = clsUtility.GetBSEUrlHolding(url + "/HoldingFileUpload", GetToken(), date, Postadate);
                    if (RespApi != "")
                    {
                        MessageBox.Show(RespApi);
                    }
                    pnlWait.Visible = false;
                    lblTitle.Visible = true;

                    //MessageBox.Show("Total Record Read from Upload file " + dt.Rows.Count);
                }
                else
                {
                    MessageBox.Show("No Record found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnBankBalUpld_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Bank Account Balance File Dialog";
                //fdlg.InitialDirectory = @"c:\";
                fdlg.InitialDirectory = @"G:\Prashant\doc\NSE API Circular";
                fdlg.Filter = "All files (*.zip)|*.*|All files (*.ZIP)|*.zip";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtBankBal.Text = fdlg.FileName;
                }
                if (!fdlg.FileName.ToUpper().Contains(".ZIP"))
                {
                    MessageBox.Show("Please Upload Only Zip Files");
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

        private void btnBankAccBalSubmit_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtBankBal.Text.ToString() != "")
                {
                    //filenameformat="AAACS0581R_BA_30072022_01.zip"
                    pnlWait.Visible = true;
                    lblTitle.Visible = true;
                    string url = ConfigurationManager.AppSettings["BSEUrl"].ToString();
                    ClsUtlity clsUtility = new ClsUtlity();
                    string Postadate = txtBankBal.Text.ToString();
                    
                    string[] splitdate = Postadate.Split('_');
                    string datval = splitdate[2].Replace(".zip", "").ToString();
                    DateTime datetime = DateTime.ParseExact(datval, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string date = datetime.ToString("dd-MM-yyyy");
                    var RespApi = clsUtility.GetBSEUrlHolding(url + "/BankBalanceFileUpload", GetToken(), date, Postadate);
                    if (RespApi != "")
                    {
                        MessageBox.Show(RespApi);
                    }
                    pnlWait.Visible = false;
                    lblTitle.Visible = true;

                }
                else
                {
                    MessageBox.Show("No Record found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnuploadCashEqulaint_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Trading Cash File Dialog";
                //fdlg.InitialDirectory = @"c:\";
                fdlg.InitialDirectory = @"G:\Prashant\doc\NSE API Circular";
                fdlg.Filter = "All files (*.zip)|*.*|All files (*.ZIP)|*.zip";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtCashEqulaint.Text = fdlg.FileName;
                }
                if (!fdlg.FileName.ToUpper().Contains(".ZIP"))
                {
                    MessageBox.Show("Please Upload Only Zip File");
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

        private void btnCashEqulaintSubmit_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtCashEqulaint.Text.ToString() != "")
                {
                    pnlWait.Visible = true;
                    lblTitle.Visible = true;
                    string url = ConfigurationManager.AppSettings["BSEUrl"].ToString();
                    ClsUtlity clsUtility = new ClsUtlity();
                    string Postadate = txtCashEqulaint.Text.ToString();
                    string[] splitdate = Postadate.Split('_');
                    string datval = splitdate[splitdate.Length - 2].ToString();
                    DateTime datetime = DateTime.ParseExact(datval, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
                    string date = datetime.ToString("dd-MM-yyyy");
                    var RespApi = clsUtility.GetBSEUrlHolding(url + "/CashBalanceFileUpload", GetToken(), date, Postadate);
                    if (RespApi != "")
                    {
                        MessageBox.Show(RespApi);
                    }
                    pnlWait.Visible = false;
                    lblTitle.Visible = true;
                }
                else
                {
                    MessageBox.Show("No Record found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public string Login()
        {
            string _result = "";

            ClsAESEncryptDecrypt clsEncrpt = new ClsAESEncryptDecrypt();
            ClsUtlity clsUtility = new ClsUtlity();
            string path = AppDomain.CurrentDomain.BaseDirectory + "BSELoginFile";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            //string LoginDatetime = "";
            string url = ConfigurationManager.AppSettings["BSEUrl"].ToString();
            string mmcode = ConfigurationManager.AppSettings["BSEMEMCODE"].ToString();
            string BSELoginId = ConfigurationManager.AppSettings["BSELoginId"].ToString();
            string BSEPassword = ConfigurationManager.AppSettings["BSEPassword"].ToString();
            string SecretKey = ConfigurationManager.AppSettings["BSESecretKey"].ToString();
            ClsLoginResp ClsResp = new ClsLoginResp();
            //string EncryptPassword = clsEncrpt.GetEncryptData(BSEPassword, SecretKey, true);
            string EncryptPassword = clsEncrpt.AES256Encrypt(BSEPassword, SecretKey);
            try
            {
                ClsNseLoginReq _clsReq = new ClsNseLoginReq()
                {
                    memberCode = mmcode,
                    loginId = BSELoginId,
                    password = EncryptPassword
                };
                string JSON = JsonConvert.SerializeObject(_clsReq);
                ClsResp = clsUtility.BSELogin(url + "/login", _clsReq);
                //string StrResponse = RespApi.Replace("\\", "");

                if (ClsResp.token != "")
                {
                    if (System.IO.File.Exists(path + "\\LoginLog.txt"))
                    {
                        System.IO.File.Delete(path + "\\LoginLog.txt");
                    }
                    System.IO.File.AppendAllText(path + "/LoginLog.txt", "Token=" + ClsResp.token.ToString() + ",DateTime=" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                    _result = ClsResp.token.ToString();
                }
                else
                {
                    string errmsg = MasterError(ClsResp.code[0].ToString());
                    MessageBox.Show("Login Fail." + Environment.NewLine + errmsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _result = "Login Fail." + Environment.NewLine + errmsg;
                }

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

        //public string LogOut()
        //{
        //    string StrResponse = "";
        //    try
        //    {


        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //    return StrResponse;
        //}

        //public bool CreateZip(string SourcePath, string DesinationPath)
        //{

        //    try
        //    {

        //        ProcessStartInfo p = new ProcessStartInfo();
        //        p.FileName = @"C:\Program Files\7-Zip\7zG.exe";
        //        p.Arguments = "a -tgzip \"" + DesinationPath + ".gz\" \"" + SourcePath + "\" -mx=9";
        //        p.WindowStyle = ProcessWindowStyle.Hidden;
        //        Process x = Process.Start(p);
        //        x.WaitForExit();
        //        //Compress(DesinationPath);
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return true;
        //    }
        //}

        public string GetToken()
        {
            string Result = "";
            string path = AppDomain.CurrentDomain.BaseDirectory + "LoginFile\\BSELoginLog.txt";
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
                                if (Convert.ToInt32(strT2.Substring(4, 2)) < 28)
                                {
                                    Result = Arr[0].ToString().Replace("Token=", "");
                                }
                                else
                                {
                                    Result = Login();
                                }
                            }
                        }
                        else
                        {
                            string pathdel = AppDomain.CurrentDomain.BaseDirectory + "LoginFile";
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
            return Result;
        }

        //public List<HoldingData> lstHolddata(string _weekdate)
        //{
        //    List<HoldingData> lstResp = new List<HoldingData>();
        //    List<HoldingDataFilter> lstRespfilter = new List<HoldingDataFilter>();
        //    List<HoldingDataFilter> lstRespfilter1 = new List<HoldingDataFilter>();
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

    }
}
