using System.ComponentModel.DataAnnotations;
using System;

namespace WebApplication2.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(15)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime LastLoginDate { get; set; }

        public int FailedLoginAttempts { get; set; }
    }
}
