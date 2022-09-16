using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    internal class ClsCIMAllocation
    {
        public string GetAuthorization()
        {
            string Key = ConfigurationManager.AppSettings["Key"].ToString();//"1d2d8c19228c43819496be4bd2d78a2f";
            string secret = ConfigurationManager.AppSettings["Secret"].ToString();//"4c75a4c0b9394cefa024a8d1516ba38c";
            string str = Key + ":" + secret;
            string Authorization = ToBase64Encode(str);
            return Authorization;
        }

        public string GetNonse()
        {
            string Dateformate = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
            string Nonse = Dateformate + ":" + GenerateNewRandom();
            string Nonsebase64 = ToBase64Encode(Nonse);
            return Nonsebase64;
        }
        public string ToBase64Encode(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }
        public string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
        public string GenerateNewRandom7()
        {
            Random generator = new Random();
            String r = generator.Next(1111111, 9999999).ToString();
            //if (r.Distinct().Count() == 1)
            //{
            //    r = GenerateNewRandom();
            //}
            return r;
        }


    }
    public class TokenResp
    {

        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
        public string status { get; set; }
    }

}
