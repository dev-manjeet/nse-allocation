using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace NSEAllocation.Models
{
    public class clsUtilityMSEI
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
        public static string Key_AES256 = "8UHjPgXZzXCGkhxV2QCnooyJexUzvJrO";

        private static readonly Encoding encoding = Encoding.UTF8;

        static byte[] HmacSHA256(String data, String key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(encoding.GetBytes(key)))
            {
                return hmac.ComputeHash(encoding.GetBytes(data));
            }
        }

        public clsUtilityMSEI()
        {
            URLPath = System.Configuration.ConfigurationManager.AppSettings.GetValues("UrlMSEI").GetValue(0).ToString();
            Key_AES256 = System.Configuration.ConfigurationManager.AppSettings.GetValues("SecretKeyMSEI").GetValue(0).ToString();
            versionNo_CashData = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings.GetValues("StrVersionNo_CashData").GetValue(0).ToString());
            versionNo_BankData = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings.GetValues("StrVersionNo_BankData").GetValue(0).ToString());
            versionNo_HoldingData = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings.GetValues("StrVersionNo_HoldingData").GetValue(0).ToString());
        }
        public  string Encrypt_AES256(string plainText)
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

        public  string CompressString(string text)
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

        public  string GetUrl(string Url, string AuthInfo, string PostData, bool GZip, ref string UncompressedResponseData)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest Http = (HttpWebRequest)WebRequest.Create(Url);

            if (GZip)
            {
                PostData = CompressString(PostData);
                Http.Headers.Add("Accept-Encoding", "gzip");
                Http.Headers.Add("compression", "true");
            }
            Http.AutomaticDecompression = DecompressionMethods.GZip;
            Http.Headers.Add("Content-Encoding", "gzip");
            if (AuthInfo.Length > 0)
            {
                Http.Headers["Authorization"] = AuthInfo;
            }
            if (!string.IsNullOrEmpty(PostData))
            {
                Http.Method = "POST";
                Http.ContentType = "application/json";
                Http.KeepAlive = true;
                Http.Timeout = 1000 * 600;
                byte[] lbPostBuffer;
                if (GZip)
                    lbPostBuffer = Encoding.Default.GetBytes("\"{" + PostData + "}\"");
                else
                    lbPostBuffer = Encoding.Default.GetBytes(PostData);

                Http.ContentLength = lbPostBuffer.Length;
                Stream PostStream = Http.GetRequestStream();
                PostStream.Write(lbPostBuffer, 0, lbPostBuffer.Length);
                PostStream.Close();
            }

            HttpWebResponse WebResponse = (HttpWebResponse)Http.GetResponse();

            Stream responseStream = responseStream = WebResponse.GetResponseStream();
            Stream uncompressedresponseStream = responseStream;
            if (WebResponse.ContentEncoding.ToLower().Contains("gzip"))
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            else if (WebResponse.ContentEncoding.ToLower().Contains("deflate"))
                responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

            StreamReader Reader = new StreamReader(responseStream, Encoding.Default);
            string Html = Reader.ReadToEnd();

            StreamReader uncompressReader = new StreamReader(uncompressedresponseStream);

            UncompressedResponseData = uncompressReader.ReadToEnd();

            WebResponse.Close();
            responseStream.Close();

            return Html;
        }


        public static string GetUrlCASHBANK(string Url, string AuthInfo, string PostData, bool GZip, ref string UncompressedResponseData)
        {
            //ServicePointManager.MaxServicePointIdleTime = 1000;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
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
            string Html = Reader.ReadToEnd();

            StreamReader uncompressReader = new StreamReader(uncompressedresponseStream);

            UncompressedResponseData = uncompressReader.ReadToEnd();

            WebResponse.Close();
            responseStream.Close();

            return Html;
        }

    
}
}
