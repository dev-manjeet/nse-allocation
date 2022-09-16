using Newtonsoft.Json;
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
    public partial class FrmMSEIBankAccBalance : Form
    {
        string Uncompressoutputstr = "";
        string Token = "";
        public FrmMSEIBankAccBalance()
        {
            InitializeComponent();
        }

        private void FrmMSEIBankAccBalance_Load(object sender, EventArgs e)
        {
            pnlWait.Visible = false;
            Token = GetToken();
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

                string StrResponse = RespApi.Replace("\\", "").Replace("\"", ""); ;
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

        private void btnBankBalUpld_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Bank Account Balance File Dialog";
                //fdlg.InitialDirectory = @"c:\";
                fdlg.InitialDirectory = @"";
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
                ClsMSEIBankBalanceData BankBalanceData = new ClsMSEIBankBalanceData();
                List<ClsMSEIBankBalance> listBankBalanceData = new List<ClsMSEIBankBalance>();
                ClsMSEIBankBalance cls = new ClsMSEIBankBalance();
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
                        ClsMSEIBankBalance cls1 = new ClsMSEIBankBalance();
                        //{
                        cls1.BankAccountNo = Arr[0].ToString().Replace("'", "");
                        cls1.IFSC = Arr[1].ToString();
                        cls1.BankAccountType = Arr[2].ToString();
                        cls1.WeekDay1 = Convert.ToDouble(Arr[3].ToString());
                        cls1.WeekDay2 = Convert.ToDouble(Arr[4].ToString());
                        cls1.WeekDay3 = Convert.ToDouble(Arr[5].ToString());
                        cls1.WeekDay4 = Convert.ToDouble(Arr[6].ToString());
                        cls1.WeekDay5 = Convert.ToDouble(Arr[7].ToString());
                        cls1.WeekDay6 = Convert.ToDouble(Arr[8].ToString());

                       /// };
                        listBankBalanceData.Add(cls1);
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
                if (System.IO.File.Exists(ResponseFileSave + "\\" + pan + "_BA_Response_" + fILEdATE.Replace("-", "") + ".txt"))
                {
                    pnlWait.Visible = false;
                    lblTitle.Visible = true;
                    MessageBox.Show("This File Already Exist");
                    return;
                    //System.IO.File.Delete(ResponseFileSave + "\\" + pan + "_HS_Response" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".Zip");
                }
                string ResponseFileSave1 = ResponseFileSave + "\\" + pan + "MSEI_BA_Response_" + fILEdATE.Replace("-", "") + ".txt";

                if (!System.IO.Directory.Exists(pathTXT))
                {
                    System.IO.Directory.CreateDirectory(pathTXT);
                }
                if (System.IO.File.Exists(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".json"))
                {
                    System.IO.File.Delete(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".json");
                }
                if (System.IO.File.Exists(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".gz"))
                {
                    System.IO.File.Delete(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".gz");
                }



                using (StreamWriter sw = new StreamWriter(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".json", true))
                {
                    sw.Write(jsonFilter);
                }
                bool Result = CreateZip(pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".json", pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + "");
                //ClsUtlity clsUtility = new ClsUtlity();
                clsUtilityMSEI clsUtility = new clsUtilityMSEI();
                var sFile = pathTXT + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".gz";
                string url = ConfigurationManager.AppSettings["UrlMSEI"].ToString();
                //string RespApi = clsUtility. GetUrlCASHBANK(url + "/tradingEodBalanceUpload/version/1.0", "Bearer " + GetToken(), sFile, sFile, true, ref Uncompressoutputstr);
                //string RespApi = clsUtility.GetUrlHolding(url + "/tradingEodBalanceUpload", GetToken(), sFile, true, ResponseFileSave1);
                string RespApi = clsUtilityMSEI.GetUrlCASHBANK(url + "tradingEodBalanceUpload", GetToken(), sFile, true, ref Uncompressoutputstr);
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
                    else
                    {
                        if (RespApi.ToString() != "")
                        {
                            RespApi.Replace(@"\", "");
                            using (StreamWriter sw = new StreamWriter(ResponseFileSave1))
                            {
                                sw.WriteLine(RespApi);
                            }
                        }
                        MessageBox.Show(RespApi.ToString());
                    }
                }
                else
                {
                    MessageBox.Show(RespApi.ToString());
                }

            }
            pnlWait.Visible = false;
            lblTitle.Visible = true;
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
    }

}
