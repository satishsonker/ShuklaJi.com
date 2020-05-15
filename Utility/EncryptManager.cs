using System;
using System.Security.Cryptography;
using System.Text;

namespace ShuklaJi.Utilities
{
    public static class EncryptManager
    {
        /// <summary>
        /// Encrypt Plain text Using Triple DES Algo
        /// </summary>
        /// <param name="PlainText">Readable string</param>
        /// <param name="Key">Salt Key to Encrypt, if you do not provide salt key then it will read from the AppSettings.json>AppVariable>SaltKey</param>
        /// <returns>Cipher Text</returns>
        public static string Encrypt(string PlainText, string Key = "")
        {
                byte[] inputArray = Encoding.UTF8.GetBytes(PlainText);

                Key = string.IsNullOrEmpty(Key.Trim()) ?
                    AppSetting.GetAppSettingsData(UtilityConstant.AppConfigConstant.AppVariable, UtilityConstant.AppConfigConstant.SaltKey, string.Empty) :
                    Key;

                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.UTF8.GetBytes(Key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Decrypt cipher text Using Triple DES Algo
        /// </summary>
        /// <param name="CipherText">Encrypted Text</param>
        /// <param name="Key">Salt Key to Decrypt, if you do not provide salt key then it will read from the AppSettings.json>AppVariable>SaltKey</param>
        /// <returns></returns>
        public static string Decrypt(string CipherText, string Key = "")
        {
            try
            {
                byte[] inputArray = Convert.FromBase64String(CipherText);

                Key = string.IsNullOrEmpty(Key.Trim()) ?
                    AppSetting.GetAppSettingsData("AppVariable", "SaltKey", string.Empty) :
                    Key;

                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider
                {
                    Key = Encoding.UTF8.GetBytes(Key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Hash String from plain string
        /// </summary>
        /// <param name="PlainString">plain string</param>
        /// <returns>Hashed String</returns>
        public static string GetHashString(string PlainString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(PlainString))
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Get Byte array of the string
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}
