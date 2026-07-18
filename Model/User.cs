using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Products_Crud.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, MaxLength(256)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; } = true;   // new column
    }
}
