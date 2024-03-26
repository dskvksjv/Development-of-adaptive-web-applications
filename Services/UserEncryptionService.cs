using System.Security.Cryptography;
using System.Text;
using WebApplication2.Interfaces;

namespace WebApplication2.Services
{
    public class UserEncryptionService : IUserEncryptionService
    {
        public string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
