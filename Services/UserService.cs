using System.Security.Cryptography;
using System.Text;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class UserService : IUserService, IUserEncryptionService
    {
        private readonly List<User> _users;

        public UserService()
        {
            _users = new List<User>
            {
                new User { Id = 1, FirstName = "Victoria", LastName = "Relonvald", Email = "Relonvald@example.com", DateOfBirth = new System.DateTime(2004, 9, 2), Password = "password1" },
                new User { Id = 2, FirstName = "Bohdan", LastName = "Lair", Email = "Bohdan@example.com", DateOfBirth = new System.DateTime(2003, 5, 10), Password = "password2" }
            };
        }

        public User Register(User user)
        {
            if (_users.Any(u => u.Email == user.Email))
                return null;

            user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
            user.Password = EncryptPassword(user.Password);
            _users.Add(user);
            return user;
        }

        public User Login(string email, string password)
        {
            string encryptedPassword = EncryptPassword(password);
            return _users.FirstOrDefault(u => u.Email == email && u.Password == encryptedPassword);
        }

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
