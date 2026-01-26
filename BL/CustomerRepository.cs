using Microsoft.EntityFrameworkCore;
using Products_Crud.DAL;
using Products_Crud.Model;

namespace Products_Crud.BL
{
    /// <summary>
    /// EF Core based repository for Customer operations
    /// Provides clean LINQ-based data access
    /// </summary>
    public class CustomerRepository : ICustomerCreate, ICustomerRead
    {
        private readonly UserDbContext _context;

        public CustomerRepository(UserDbContext context)
        {
            _context = context;
        }

        // --------- CREATE ----------
        /// <summary>
        /// Add a new customer to the database
        /// </summary>
        public int AddCustomer(Customers customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            // Check if customer already exists
            if (CustomerExists(customer.Email))
                throw new InvalidOperationException($"Customer with email '{customer.Email}' already exists.");

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer.CustomerId;
        }

        // --------- READ ----------
        /// <summary>
        /// Get a single customer by ID
        /// </summary>
        public Customers? GetCustomer(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Customer ID must be greater than 0", nameof(id));

            return _context.Customers.Find(id);
        }

        /// <summary>
        /// Get all customers from database
        /// </summary>
        public IEnumerable<Customers> GetAllCustomers()
        {
            return _context.Customers.OrderBy(c => c.Name).ToList();
        }

        // --------- HELPER ----------
        /// <summary>
        /// Check if customer with given email already exists
        /// </summary>
        private bool CustomerExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return _context.Customers
                .Any(c => c.Email.ToLower() == email.ToLower());
        }
    }
}
