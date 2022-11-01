using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LandSterlingProject2.Helpers
{
    public class EncryptHandler
    {
        private static TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

        private static MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
        public static string Encrypt(string stringToEncrypt)
        {
            string key = "SP20194321";
            DES.Key = MD5Hash(key);
            DES.Mode = CipherMode.ECB;
            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(stringToEncrypt);
            return Convert.ToBase64String(DES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static string Decrypt(string encryptedString)
        {
            try
            {
                string key = "SP20194321";
                DES.Key = MD5Hash(key);
                DES.Mode = CipherMode.ECB;
                byte[] Buffer = Convert.FromBase64String(encryptedString);
                return ASCIIEncoding.ASCII.GetString(DES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception)
            {

            }
            return string.Empty;

        }

        public static byte[] MD5Hash(string value)
        {
            return MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));
        }

    }
}