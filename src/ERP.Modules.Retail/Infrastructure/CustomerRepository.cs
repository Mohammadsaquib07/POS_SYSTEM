using Microsoft.EntityFrameworkCore;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Infrastructure
{
    public class CustomerRepository : ICustomerCreate, ICustomerRead
    {
        private readonly IRetailDbContext _context;

            public CustomerRepository(IRetailDbContext context)
            {
                _context = context;
            }

        public async System.Threading.Tasks.Task<int> AddCustomerAsync(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            // Check if customer already exists
            if (CustomerExists(customer.Email))
                throw new InvalidOperationException($"Customer with email '{customer.Email}' already exists.");

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer.CustomerId;
        }

        public Customer? GetCustomer(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Customer ID must be greater than 0", nameof(id));

            return _context.Customers.Find(id);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.OrderBy(c => c.Name).ToList();
        }
        private bool CustomerExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return _context.Customers
                .Any(c => c.Email.ToLower() == email.ToLower());
        }
        
    }
}
