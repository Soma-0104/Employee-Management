using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagement.Repository.DataUtilities
{
    public class DataEncryption
    {
        private static readonly string key = "12345678901234567890123456789012";

        public static string EncryptData(string plainData)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainData);
            }

            return Convert.ToBase64String(ms.ToArray());
        }
        public static string DecryptData(string encryptedData)
        {
            var fullCipher = Convert.FromBase64String(encryptedData);
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);

            byte[] iv = fullCipher.Take(16).ToArray();
            byte[] cipher = fullCipher.Skip(16).ToArray();
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
