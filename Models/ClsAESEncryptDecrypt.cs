using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NSEAllocation.Models
{
    public class ClsAESEncryptDecrypt
    {
        private static string getString(byte[] b)
        {
            return Encoding.UTF8.GetString(b);
        }
        public string GetEncryptData(string data1, string SecretKey,bool Encryp)
        {
            string result = "";
            byte[] data = Encoding.UTF8.GetBytes(data1);
            byte[] a = Convert.FromBase64String(SecretKey);
           if(Encryp==true)
            {
                byte[] enc = Encrypt(data, a);
                 result = Convert.ToBase64String(enc);
            }
            else
            {
                byte[] dec = Decrypt(data, a);
                result=Convert.ToBase64String(dec);
            }
           
           
           
            return result;
        }


        public  string AES256Encrypt(string password, string key)
        {
            string EncryptionKey = key;
            var clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Padding = PaddingMode.Zeros;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    password = Convert.ToBase64String(ms.ToArray());
                }
            }

            return password;
        }

        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            using (RijndaelManaged csp = new RijndaelManaged())
            {
                csp.KeySize = 256;
                csp.BlockSize = 128; 
                csp.Key = key;
                csp.Padding = PaddingMode.PKCS7;
                csp.Mode = CipherMode.ECB;
                ICryptoTransform encrypter = csp.CreateEncryptor();
                return encrypter.TransformFinalBlock(data, 0, data.Length);
            }
        }
        private static byte[] Decrypt(byte[] data, byte[] key)
        {
            using (RijndaelManaged csp = new RijndaelManaged())
            {
                csp.KeySize = 256;
                csp.BlockSize = 128;
                csp.Key = key;
                csp.Padding = PaddingMode.PKCS7;
                csp.Mode = CipherMode.ECB;
                ICryptoTransform decrypter = csp.CreateDecryptor();
                return decrypter.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
}
