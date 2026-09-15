using System.ComponentModel.DataAnnotations;
using Products_Crud.Model;

namespace Erp.ModelCompanies
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }          // was CompanyName
        public DateTime CreatedAt { get; set; }   // was CreatedDate
        public ICollection<User> Users { get; set; }
    }
}