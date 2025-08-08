using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagement.Repository.DataUtilities
{
    public class PasswordHasher
    {
        public static readonly string salt = "Mysecretsalt12345678";
        public static string Hash(string password)
        {
            string saltAndPwd = password + salt;
            using (var algorithm = SHA256.Create())
            {
                byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd));
                return string.Concat(hash.Select(b => b.ToString("x2"))).ToUpperInvariant();
            }
        }

    }
}
