using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.Net.Http;
using RestSharp;
using RestSharp.Extensions;
using System.Windows;
using System.Xml.Linq;

namespace NSEAllocation.Models
{
    public class ClsUtlity
    {
        public static string URLPath = "";
        public static string AuthMemberCode = "";
        public static string AuthLoginId = "";
        public static string AuthToken = "";

        //Version No of API
        // public static decimal versionNo_NILData = 1.0m;
        public static decimal versionNo_HoldingData = 1.0m;
        public static decimal versionNo_CashData = 1.0m;
        public static decimal versionNo_BankData = 1.0m;

        // The encryption key. Must be 32 characters.
        public static string Key_AES256 = System.Configuration.ConfigurationManager.AppSettings.GetValues("StrKey_AES256").GetValue(0).ToString();

        private static readonly Encoding encoding = Encoding.UTF8;

        static byte[] HmacSHA256(String data, String key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(key)))
            {
                return hmac.ComputeHash(encoding.GetBytes(data));
            }
        }

        public ClsUtlity()
        {
            URLPath = System.Configuration.ConfigurationManager.AppSettings.GetValues("StrURL").GetValue(0).ToString();
            Key_AES256 = System.Configuration.ConfigurationManager.AppSettings.GetValues("StrKey_AES256").GetValue(0).ToString();
            versionNo_CashData = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings.GetValues("StrVersionNo_CashData").GetValue(0).ToString());
            versionNo_BankData = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings.GetValues("StrVersionNo_BankData").GetValue(0).ToString());
            versionNo_HoldingData = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings.GetValues("StrVersionNo_HoldingData").GetValue(0).ToString());
        }
        public string Encrypt_AES256(string plainText)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                aes.Key = encoding.GetBytes(Key_AES256);
                aes.GenerateIV();

                ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] buffer = encoding.GetBytes(plainText);

                string encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));

                String mac = "";

                mac = BitConverter.ToString(HmacSHA256(Convert.ToBase64String(aes.IV) + encryptedText, Key_AES256)).Replace("-", "").ToLower();

                var keyValues = new Dictionary<string, object>
                {
                    { "iv", Convert.ToBase64String(aes.IV) },
                    { "value", encryptedText },
                    { "mac", mac },
                };

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                return Convert.ToBase64String(encoding.GetBytes(serializer.Serialize(keyValues)));
            }
            catch (Exception e)
            {
                throw new Exception("Error encrypting: " + e.Message);
            }
        }
        static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
        public string CompressString(string text)
        {

            byte[] buffer = Encoding.UTF8.GetBytes(text);

            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);

        }
        


        public ClsLoginResp BSELogin(string url, ClsNseLoginReq jsonReq)
        {
            ClsLoginResp ClsResp = new ClsLoginResp();
            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsondata = js.Serialize(jsonReq);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = jsondata.Length;
            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(jsondata);
            }
            WebResponse webResponse = (HttpWebResponse)request.GetResponse();

            using (Stream webStream = webResponse.GetResponseStream())
            {
                if (webStream != null)
                {
                    using (StreamReader responseReader = new StreamReader(webStream))
                    {
                        var responseString = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                        ClsResp = js.Deserialize<ClsLoginResp>(responseString);
                    }
                }
            }
            return ClsResp;
        }
        public string GetBSEUrlHolding(string Url, string AuthInfo, string lASTWeekDay, string PostData)
        {
            // string pATH = UncompressedResponseData.Replace(@"\", "/");
            string BSEMEMCODE = ConfigurationManager.AppSettings["BSEMEMCODE"].ToString();
            string BSELoginId = ConfigurationManager.AppSettings["BSELoginId"].ToString();
            string Html = "";
            try
            {
                var client = new RestClient(Url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("TOKEN", AuthInfo);
                request.AddHeader("LASTWEEKDATE", lASTWeekDay);
                request.AddHeader("MEMBERCODE", BSEMEMCODE);
                request.AddHeader("LOGINID", BSELoginId);
                //request.AddFile("File", "AAACS0581R_HS_07052022_01.zip");
                //request.AddHeader("Authorization", AuthInfo);
                request.AddFile("File", PostData);
                IRestResponse response = client.Execute(request);

                Html = response.Content;
                //byte[] buffer = new byte[response.Content.Length];
                //client.DownloadData(request).SaveAs(pATH);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
            return Html;
        }


        public string LoginGetUrl(string Url, string AuthInfo, string PostData, bool GZip, ref string UncompressedResponseData)
        {
            byte[] fileToSend = File.ReadAllBytes(PostData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "*/*";
            //request.Connection = "keep-alive";
            // request.Headers.Add("Content-Type","application/json");

            request.Headers.Add("Accept-Encoding", "gzip,deflate,br");
            request.Headers.Add("Content-Encoding", "gzip");
            //request.Headers.Add("Accept", "*/*");
            //request.Headers.Add("Connection","keep-alive");
            request.ContentLength = fileToSend.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileToSend, 0, fileToSend.Length);
                requestStream.Close();
            }




            HttpWebResponse WebResponse = (HttpWebResponse)request.GetResponse();

            Stream responseStream = WebResponse.GetResponseStream();
            Stream uncompressedresponseStream = responseStream;
            //if (WebResponse.ContentEncoding.ToLower().Contains("gzip"))
            responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            //else if (WebResponse.ContentEncoding.ToLower().Contains("deflate"))
            //responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

            StreamReader Reader = new StreamReader(responseStream, Encoding.Default);
            string Html = Reader.ReadToEnd();

            //StreamReader uncompressReader = new StreamReader(uncompressedresponseStream);

            //UncompressedResponseData = uncompressReader.ReadToEnd();


            WebResponse.Close();
            responseStream.Close();

            return Html;
        }
        public string GetUrl(string Url, string AuthInfo, string PostData, bool GZip, ref string UncompressedResponseData)
        {
            string Html = "";
            //string boundary = "----------------------------" + DateTime.Now.Ticks.ToString();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                var boundary = "------------------------" + DateTime.Now.Ticks;
                var newLine = Environment.NewLine;
                var propFormat = "--" + boundary + newLine +
                    "Content-Disposition: form-data; name=\"{0}\"" + newLine + newLine +
                    "{1}" + newLine;
                //var propFormat = "--" + boundary + newLine +
                //                    "Content-Disposition: form-data; name=\"{0}\"" + newLine + newLine +
                //                    "{1}" + newLine;
                var fileHeaderFormat = "--" + boundary + newLine +
                                        "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" + newLine;

                var req = (HttpWebRequest)HttpWebRequest.Create(Url);
                req.Method = WebRequestMethods.Http.Post;
                req.Timeout = 1000 * 600;
                req.ContentType = "multipart/form-data; boundary=" + boundary;
                //req.Headers.Add("",AuthInfo)
                req.Headers["Authorization"] = AuthInfo;
                req.Headers.Add("Accept-Encoding", "gzip,deflate,br");
                req.Headers.Add("Content-Encoding", "gzip");
                using (var reqStream = req.GetRequestStream())
                {
                    var reqWriter = new StreamWriter(reqStream);
                    var tmp = string.Format(fileHeaderFormat, "File", PostData);
                    //var tmp = string.Format(propFormat, "File", PostData.Substring(PostData.Length - 32));
                    reqWriter.Write(tmp);
                    //tmp = string.Format(propFormat, "str2", "hello world 2");
                    //reqWriter.Write(tmp);
                    reqWriter.Write("--" + boundary + "--");
                    reqWriter.Flush();
                }
                var res = req.GetResponse();
                using (var resStream = res.GetResponseStream())
                {

                    var reader = new StreamReader(resStream);
                    Html = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

                string message = ex.Message;
                Html = message;
            }
            return Html;
        }
        //public string GetUrlHolding(string Url, string AuthInfo, string PostData, bool GZip, string UncompressedResponseData)
        //{
        //    string pATH = UncompressedResponseData.Replace(@"\", "/");
        //    string Html = "";
        //    try
        //    {
        //        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls|SecurityProtocolType.Ssl3;
        //        var client = new RestClient("https://api.nseindia.com/inspectionapi/tradingHoldingUpload/1.0");
        //        client.Proxy = new WebProxy();
        //        client.Timeout = 1000 * 600;
        //        var request = new RestRequest(Method.POST);
        //        ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.DefaultConnectionLimit = 9999;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
        //        request.AddHeader("Authorization", AuthInfo);
        //        request.AddHeader("Accept-Encoding", "gzip, deflate, br");
        //        request.AddHeader("Content-Type", "multipart / form - data; boundary = --------------------------562249733216909054111775");
        //        request.AddFile("File", PostData);

        //        //request.AddHeader("Connection", "keep-alive");
        //        //IRestResponse response = client.Execute(request);
        //        //Console.WriteLine(response.Content);
        //        IRestResponse response = client.Execute(request);

        //        if (response.Content != "")
        //        {
        //            try
        //            {
        //                byte[] buffer = new byte[response.Content.Length];
        //                client.DownloadData(request).SaveAs(pATH);
        //                Html = "True";
        //            }
        //            catch (Exception)
        //            {

        //                byte[] buffer = new byte[response.Content.Length];
        //                client.DownloadData(request).SaveAs(pATH);
        //            }

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        Html = "";

        //    }
        //    return Html;
        //}
        public string GetUrlHolding(string Url, string AuthInfo, string PostData, bool GZip, string UncompressedResponseData)
        {
            //string pATH = UncompressedResponseData.Replace(@"\", "/");
            string Html = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                byte[] fileToSend = File.ReadAllBytes(PostData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Headers.Add("Authorization", AuthInfo);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "*/*";
                request.Timeout = 1000 * 600;
                request.Headers.Add("Accept-Encoding", "gzip,deflate,br");
                request.Headers.Add("Content-Encoding", "gzip");
                request.ContentLength = fileToSend.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileToSend, 0, fileToSend.Length);
                    requestStream.Close();
                    request.KeepAlive = false;
                }
                HttpWebResponse WebResponse = (HttpWebResponse)request.GetResponse();
                Stream responseStream = WebResponse.GetResponseStream();
                Stream uncompressedresponseStream = responseStream;
                //if (WebResponse.ContentEncoding.ToLower().Contains("gzip"))
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                //else if (WebResponse.ContentEncoding.ToLower().Contains("deflate"))
                //    responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

                StreamReader Reader = new StreamReader(responseStream, Encoding.Default);
                Html = Reader.ReadToEnd();

                WebResponse.Close();
                responseStream.Close();
                return Html;
                


            }
            catch (Exception ex)
            {
                Html = "";

            }

            return Html;
        }
        public string GetUrlHoldingTest(string Url, string AuthInfo, string PostData, bool GZip, string UncompressedResponseData)
        {
            string pATH = UncompressedResponseData.Replace(@"\", "/");
            string Html = "";
            try
            {

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var client = new RestClient(Url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", AuthInfo);
                request.AddFile("File", PostData);
                IRestResponse response = client.Execute(request);
                Html = response.Content;
                byte[] buffer = new byte[response.Content.Length];
                client.DownloadData(request).SaveAs(pATH);

                
                return Html;



            }
            catch (Exception ex)
            {
                Html = "";

            }

            return Html;
        }
        public string GetUrlNSEBankbalance(string Url, string AuthInfo, string PostData, bool GZip, string UncompressedResponseData)
        {
            string pATH = UncompressedResponseData.Replace(@"\", "/");
            string Html = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                byte[] fileToSend = File.ReadAllBytes(PostData);
                //byte[] fileToSend = File.ReadAllBytes(@"G:\package\28062022\NSEAllocation\bin\Debug\Nse_Bank_Account_Balance_File\16072022\AAACS0581R_BA_16072022.txt.gz");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/json";
                //request.Accept = "*/*";
                request.Timeout = 1000 * 600;
                request.Headers.Add("Accept-Encoding", "gzip,deflate,br");
                request.Headers.Add("Content-Encoding", "gzip");
                request.Headers.Add("Authorization", AuthInfo);
                request.ContentLength = fileToSend.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileToSend, 0, fileToSend.Length);
                    requestStream.Close();
                }
                using (var s = request.GetResponse().GetResponseStream())
                {
                    using (var w = File.OpenWrite(pATH))
                    {
                        s.CopyTo(w);
                    }
                }
                //HttpWebResponse WebResponse = (HttpWebResponse)request.GetResponse();

                //Stream responseStream = WebResponse.GetResponseStream();
                //Stream uncompressedresponseStream = responseStream;

                //responseStream = new GZipStream(responseStream, CompressionMode.Decompress);


                //StreamReader Reader = new StreamReader(responseStream, Encoding.Default);
                //Html = Reader.ReadToEnd();

                //WebResponse.Close();
                //responseStream.Close();


                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //var client = new RestClient(Url);
                //client.Timeout = -1;
                //var request = new RestRequest(Method.POST);
                //request.AddHeader("Authorization", AuthInfo);
                //request.AddHeader("ContentType", "application/json");

                //request.AddHeader("Accept-Encoding", "gzip,deflate,br");
                //request.AddHeader("Content-Encoding", "gzip");
                //request.AddFile("File", PostData);
                //IRestResponse response = client.Execute(request);

                //client.DownloadData(request).SaveAs(pATH);
                //using (var s = response)
                //{
                //    using (var w = File.OpenWrite(@"G:\MyFile.zip"))
                //    {
                //        s.CopyTo(w);
                //    }
                //}



            }
            catch (Exception ex)
            {
                Html = "";

            }

            return Html;
        }
        public string GetUrlHoldingTest2(string Url, string AuthInfo, string PostData, bool GZip, string UncompressedResponseData)
        {
            string pATH = UncompressedResponseData.Replace(@"\", "/");
            string Html = "";
            try
            {

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);

                string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

                httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                httpWebRequest.Method = "POST";
                httpWebRequest.KeepAlive = true;
                httpWebRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
                if (AuthInfo.Length > 0)
                {
                    httpWebRequest.Headers["Authorization"] = AuthInfo;
                }
                Stream memStream = new System.IO.MemoryStream();
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition:  form-data; name=\"{0}\";\r\n\r\n{1}";
                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                if (!string.IsNullOrEmpty(PostData.ToString()))
                {

                    httpWebRequest.Method = "POST";
                    httpWebRequest.KeepAlive = true;
                    httpWebRequest.Timeout = 1000 * 600;

                    string header = string.Format(headerTemplate, "file", PostData);
                    //string header = string.Format(headerTemplate, "uplTheFile", files[i]);
                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                    memStream.Write(headerbytes, 0, headerbytes.Length);
                    FileStream fileStream = new FileStream(PostData, FileMode.Open,
                    FileAccess.Read);
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                    }
                    memStream.Write(boundarybytes, 0, boundarybytes.Length);
                    fileStream.Close();

                    httpWebRequest.ContentLength = memStream.Length;
                    Stream requestStream = httpWebRequest.GetRequestStream();
                    memStream.Position = 0;
                    byte[] tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();
                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    requestStream.Close();
                }

                HttpWebResponse WebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                Stream responseStream = WebResponse.GetResponseStream();
                Stream uncompressedresponseStream = responseStream;
                if (WebResponse.ContentEncoding.ToLower().Contains("gzip"))
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                else if (WebResponse.ContentEncoding.ToLower().Contains("deflate"))
                    responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

                StreamReader Reader = new StreamReader(responseStream, Encoding.Default);
                 Html = Reader.ReadToEnd();

                StreamReader uncompressReader = new StreamReader(uncompressedresponseStream);

                UncompressedResponseData = uncompressReader.ReadToEnd();

                WebResponse.Close();
                responseStream.Close();

                return Html;



            }
            catch (Exception ex)
            {
                Html = "";

            }

            return Html;
        }

    }
}
