using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Modules.Shared.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<User> Users { get; set; }
    }
}