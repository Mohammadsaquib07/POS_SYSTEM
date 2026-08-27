using System.ComponentModel.DataAnnotations;
using MyApp.Models;

namespace Erp.ModelCompanies
{
    public class Company
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string CompanyName { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<User> Users { get; set; }
    }
}