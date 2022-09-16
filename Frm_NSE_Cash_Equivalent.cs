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
    public partial class Frm_NSE_Cash_Equivalent : Form
    {
        string Token = "";
        OpenFileDialog fdlg1 = new OpenFileDialog();
        string Uncompressoutputstr = "";
        public Frm_NSE_Cash_Equivalent()
        {
            InitializeComponent();
        }
        private void Frm_NSE_Cash_Equivalent_Load(object sender, EventArgs e)
        {
            Token=GetToken();
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
       

        private void btnupload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Trading Cash File Dialog";
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
            string pathTXT = AppDomain.CurrentDomain.BaseDirectory + "NSE_Cash_Equivalent_File" + "\\" + LASTwEEKDARW;

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
                     LASTwEEKDARW = LASTwEEKDARW1.Substring(0, 10);
                    int count = 0;
                    for (int i = 0; i < Result1.Count(); i++)
                    {
                        int j = i + 1;


                        progressBar1.Maximum = 6;
                        progressBar1.Value = j;
                        if (!System.IO.Directory.Exists(pathTXT + LASTwEEKDARW))
                        {
                            System.IO.Directory.CreateDirectory(pathTXT + LASTwEEKDARW);
                        }
                        if (System.IO.File.Exists(pathTXT + LASTwEEKDARW + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv"))
                        {
                            System.IO.File.Delete(pathTXT + LASTwEEKDARW + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv");
                        }
                        if (System.IO.File.Exists(pathTXT + LASTwEEKDARW + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv.gz"))
                        {
                            System.IO.File.Delete(pathTXT + LASTwEEKDARW + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv.gz");
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
                                using (StreamWriter sw = new StreamWriter(pathTXT + LASTwEEKDARW + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv", true))
                                {
                                    sw.Write(Header.ToUpper() + Environment.NewLine);
                                }
                                using (StreamWriter sw = new StreamWriter(pathTXT + LASTwEEKDARW + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv", true))
                                {
                                    sw.Write(ContentTag);
                                }
                            }
                            else

                            {
                                using (StreamWriter sw = new StreamWriter(pathTXT + LASTwEEKDARW + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv", true))
                                {
                                    sw.Write(ContentTag);
                                }
                            }

                        }
                       

                        bool Result = CreateZip(pathTXT + LASTwEEKDARW + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv", pathTXT + LASTwEEKDARW + "\\" + pan + "_CE_" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".csv");

                        if (Result == true)
                        {
                            count = count + 1;
                        }

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
                    pnlWait.Visible = true;
                    progressBar1.Maximum = 6;
                    progressBar1.Value = i + 1;

                    bool isChecked = Convert.ToBoolean(grdViewFile.Rows[i].Cells["Select"].Value);
                    if (isChecked == true)
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
                        string ResponseFileSave = AppDomain.CurrentDomain.BaseDirectory + "NSE_Cash_Equivalent_FileResponse";
                        if (!System.IO.Directory.Exists(ResponseFileSave + "\\" + LASTwEEKDARW))
                        {
                            System.IO.Directory.CreateDirectory(ResponseFileSave + "\\" + LASTwEEKDARW);
                        }
                        if (System.IO.File.Exists(ResponseFileSave + "\\" + LASTwEEKDARW + "\\" + pan + "_CE_Response_" + LASTwEEKDARW.Replace("-", "") + "_" + Seq + ".gz"))
                        {
                            pnlWait.Visible = false;
                            lblTitle.Visible = true;
                            MessageBox.Show("This File Already Exist");
                            return;
                            //System.IO.File.Delete(ResponseFileSave + "\\" + pan + "_HS_Response" + LASTwEEKDARW.Replace("-", "") + "_0" + j + ".Zip");
                        }
                        string ResponseFileSave1 = ResponseFileSave + "\\" + LASTwEEKDARW + "\\" + pan + "_CE_Response_" + LASTwEEKDARW.Replace("-", "") + "_" + Seq + ".gz";
                        
                        string TOK = GetToken();
                        if (TOK == "")
                        {
                            MessageBox.Show("Token not Created");
                            return;
                        }
                        string TOKEN = "Bearer " + TOK;
                        txtToken.Text = TOKEN;
                        //string RespApi = clsUtilityMSEI.GetUrlCASHBANK(url + "/tradingHoldingUpload/1.0", TOKEN, sFile, true, ref Uncompressoutputstr);
                        // var RespApi = clsUtility.GetUrl(url + "/tradingHoldingUpload/1.0", TOKEN, sFile, true,ref Uncompressoutputstr);
                        //var RespApi = clsUtility.GetUrlHolding(url + "/tradingCashAndCashEquivalentUpload/1.0", TOKEN, sFile, true, ResponseFileSave1);
                        var RespApi = clsUtility.GetUrlHoldingTest(url + "/tradingCashAndCashEquivalentUpload/1.0", TOKEN, sFile, true, ResponseFileSave1);
                        //if (RespApi.ToString() != "")
                        //{
                        //    RespApi.Replace(@"\", "");
                        //    using (StreamWriter sw = new StreamWriter(ResponseFileSave1))
                        //    {
                        //        sw.WriteLine(RespApi);
                        //    }
                        //}
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
    }
}
