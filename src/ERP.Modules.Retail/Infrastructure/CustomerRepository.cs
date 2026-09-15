using Microsoft.EntityFrameworkCore;
using Products_Crud.Model;
using Products_Crud.Common.Contracts;
using ERP.Modules.Retail.Contracts;

namespace Products_Crud.BL
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
