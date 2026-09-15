using System.ComponentModel.DataAnnotations;
using Erp.ModelCompanies;
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

        public bool IsActive { get; set; } = true;

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
