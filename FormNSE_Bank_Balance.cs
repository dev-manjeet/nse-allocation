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
    public partial class FormNSE_Bank_Balance : Form
    {
        string Token = "";
        OpenFileDialog fdlg1 = new OpenFileDialog();
        string Uncompressoutputstr = "";
        public FormNSE_Bank_Balance()
        {
            InitializeComponent();
         
            pnlWait.Visible = false;
        }
        private void FormNSE_Bank_Balance_Load(object sender, EventArgs e)
        {
            Token = GetToken();
            txtToken.Text = "Bearer " + Token;
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
               // Token = GetToken();

            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message.ToString());
            }
        }

        private void btnBankAccBalSubmit_Click(object sender, EventArgs e)
        {
            string pathTXT = AppDomain.CurrentDomain.BaseDirectory + "Nse_Bank_Account_Balance_File";
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

                
                string ResponseFileSave = AppDomain.CurrentDomain.BaseDirectory + "NSE_Bank_Balance_Response";
                if (!System.IO.Directory.Exists(ResponseFileSave))
                {
                    System.IO.Directory.CreateDirectory(ResponseFileSave);
                }
                if (System.IO.File.Exists(ResponseFileSave + "\\" + pan + "_BA_Response_" + fILEdATE.Replace("-", "") + ".gz"))
                {
                    DialogResult dialog = MessageBox.Show("Are you Sure do you want delete Response Exit File  ?", "", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.No)
                    {
                        pnlWait.Visible = false;
                        lblTitle.Visible = true;
                        return;
                    }
                    else
                    {
                        System.IO.File.Delete(ResponseFileSave + "\\" + pan + "_BA_Response_" + fILEdATE.Replace("-", "") + ".gz");
                    }
                 
                }
                string ResponseFileSave1 = ResponseFileSave + "\\" + pan + "_BA_Response_" + fILEdATE.Replace("-", "") + ".gz";

                if (!System.IO.Directory.Exists(pathTXT + "\\" + fILEdATE))
                {
                    System.IO.Directory.CreateDirectory(pathTXT + "\\" + fILEdATE);
                }
                if (System.IO.File.Exists(pathTXT + "\\"+ fILEdATE+"\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt"))
                {
                    System.IO.File.Delete(pathTXT + "\\" + fILEdATE + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt");
                }
                if (System.IO.File.Exists(pathTXT + "\\" + fILEdATE + "\\" + pan + "_BA_" + week6.Replace("-", "") + "txt.gz"))
                {
                    System.IO.File.Delete(pathTXT + "\\" + fILEdATE + "\\" + pan + "_BA_" + week6.Replace("-", "") + "txt.gz");
                }
                using (StreamWriter sw = new StreamWriter(pathTXT + "\\" + fILEdATE + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt", true))
                {
                    sw.Write(jsonFilter);
                }
                bool Result = CreateZip(pathTXT + "\\" + fILEdATE + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt", pathTXT + "\\" + fILEdATE + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt");

               
                var sFile = pathTXT + "\\" + fILEdATE + "\\" + pan + "_BA_" + week6.Replace("-", "") + ".txt.gz";
                string url = ConfigurationManager.AppSettings["UrlNse"].ToString();
                txtToken.Text = "Bearer " + GetToken();
                ClsUtlity clsUtility = new ClsUtlity();
                //clsUtilityMSEI _clsUtilitymsei = new clsUtilityMSEI();
                string RespApi = clsUtility.GetUrlHoldingTest(url + "/tradingEodBalanceUpload/1.0", "Bearer " + GetToken(), sFile, true, ResponseFileSave1);
                //string RespApi = clsUtility.GetUrlHolding(url + "/tradingEodBalanceUpload/1.0", "Bearer " + GetToken(), sFile, true, ResponseFileSave1);
                //string RespApi = clsUtility.GetUrlNSEBankbalance(url + "/tradingEodBalanceUpload/1.0", "Bearer " + GetToken(), sFile, true, ResponseFileSave1);
                //string RespApi = _clsUtilitymsei.GetUrl(url + "/tradingEodBalanceUpload/1.0", "Bearer " + GetToken(), sFile, true,ref ResponseFileSave1);
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
                        if(RespApi.Contains(@"\"))
                        {
                            RespApi.Replace(@"\", "");
                            MessageBox.Show(RespApi.ToString());
                        }
                        else
                        {
                            MessageBox.Show(RespApi.ToString());
                        }
                        
                    }
                }
               

            }
            pnlWait.Visible = false;
            lblTitle.Visible = true;
        }

        
    }
    
}
