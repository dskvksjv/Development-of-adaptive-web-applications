using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography; 
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users;
        private readonly string _jwtSecret;
        private readonly int _jwtExpirationInMinutes;

        public UserService(string jwtSecret, int jwtExpirationInMinutes)
        {
            _users = new List<User>
            {
                new User { Id = 1, FirstName = "Victoria", LastName = "Relonvald", Email = "Relonvald@example.com", DateOfBirth = new DateTime(2004, 9, 2), Password = "password1" },
                new User { Id = 2, FirstName = "Bohdan", LastName = "Lair", Email = "Bohdan@example.com", DateOfBirth = new DateTime(2003, 5, 10), Password = "password2" }
            };
            _jwtSecret = jwtSecret;
            _jwtExpirationInMinutes = jwtExpirationInMinutes;
        }

        public User Register(User user)
        {
            if (_users.Exists(u => u.Email == user.Email))
                return null;

            user.Id = _users.Count + 1;
            user.Password = EncryptPassword(user.Password);
            _users.Add(user);
            return user;
        }

        public string Login(string email, string password)
        {
            string encryptedPassword = EncryptPassword(password);

            var user = _users.Find(u => u.Email == email && u.Password == encryptedPassword);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string EncryptPassword(string password)
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
