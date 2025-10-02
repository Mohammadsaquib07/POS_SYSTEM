using Microsoft.Data.SqlClient;
using Products_Crud.Model;

namespace Products_Crud.DAL
{
    public class SqlInvoiceRepository : ICustomerCreate, ICustomerRead, IInvoiceCreate, IInvoiceRead
    {
        private readonly IConfiguration _conn;
        private readonly string _connectionString;
        public SqlInvoiceRepository(IConfiguration iConfiguration)
        {
            _conn = iConfiguration;
            _connectionString = iConfiguration.GetConnectionString("Sql_Connection_String");
        }

        //-----------Customer---------------------
        public int AddCustomer(Customers customers)
        {
            if (CustomerExists(customers.Email))
                throw new Exception("Customer already exists.");
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(@"INSERT INTO Customers (Name, Email, Phone, BillingAddress)
      OUTPUT INSERTED.CustomerId
      VALUES (@Name, @Email, @Phone, @Address)", con);
            cmd.Parameters.AddWithValue("@Name", customers.Name);
            cmd.Parameters.AddWithValue("@Email", (object?)customers.Email ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Phone", (object?)customers.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Address", (object?)customers.BillingAddress ?? DBNull.Value);
            con.Open();
            return (int)cmd.ExecuteScalar();
        }

        public Customers? GetCustomer(int Id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT CustomerId,Name,Email,Phone,BillingAddress,CreatedAt FROM Customers WHERE CustomerId=@Id", con);
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;
            return new Customers
            {
                CustomerId = r.GetInt32(0),
                Name = r.GetString(1),
                Email = r.IsDBNull(2) ? null : r.GetString(2),
                Phone = r.IsDBNull(3) ? null : r.GetString(3),
                BillingAddress = r.IsDBNull(4) ? null : r.GetString(4),
                CreatedAt = r.GetDateTime(5)
            };
        }

        public IEnumerable<Customers> GetAllCustomers()
        {
            var list = new List<Customers>();
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT CustomerId,Name,Email,Phone,BillingAddress,CreatedAt FROM Customers", con);
            con.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Customers
                {
                    CustomerId = r.GetInt32(0),
                    Name = r.GetString(1),
                    Email = r.IsDBNull(2) ? null : r.GetString(2),
                    Phone = r.IsDBNull(3) ? null : r.GetString(3),
                    BillingAddress = r.IsDBNull(4) ? null : r.GetString(4),
                    CreatedAt = r.GetDateTime(5)
                });
            }
            return list;
        }
        //---------------------InvoiceAddCustomer
        public int AddInvoice(Invoices invoices)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(@"
    INSERT INTO Invoices
        (InvoiceNumber, CustomerId, InvoiceDate, Subtotal, TaxAmount, TotalAmount, Notes, CreatedBy)
    OUTPUT INSERTED.InvoiceId
    VALUES (@No, @CustId, @Date, @Sub, @Tax, @Total, @Notes, @By)", con);
            cmd.Parameters.AddWithValue("@No", invoices.InvoiceNumber);
            cmd.Parameters.AddWithValue("@CustId", invoices.CustomerId);
            cmd.Parameters.AddWithValue("@Date", invoices.InvoiceDate);
            cmd.Parameters.AddWithValue("@Sub", invoices.Subtotal);
            cmd.Parameters.AddWithValue("@Tax", invoices.TaxAmount);
            cmd.Parameters.AddWithValue("@Total", invoices.TotalAmount);
            cmd.Parameters.AddWithValue("@Notes", (object?)invoices.Notes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@By", (object?)invoices.CreatedBy ?? DBNull.Value);
            con.Open();
            return (int)cmd.ExecuteScalar();

        }
        public void AddInvoiceItem(InvoiceItem item)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(@"
                INSERT INTO Invoice_Items
                (InvoiceId,ProductName,Quantity,Price,Total)
                VALUES (@InvId,@Name,@Qty,@Price,@Total)", con);

            cmd.Parameters.AddWithValue("@InvId", item.InvoiceId);
            cmd.Parameters.AddWithValue("@Name", item.ProductName);
            cmd.Parameters.AddWithValue("@Qty", item.Quantity);
            cmd.Parameters.AddWithValue("@Price", item.Price);
            cmd.Parameters.AddWithValue("@Total", item.Total);

            con.Open();
            cmd.ExecuteNonQuery();
        }
        public Invoices? GetInvoice(int id)
        {
            // Simple header fetch
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "SELECT InvoiceId,InvoiceNumber,CustomerId,InvoiceDate,Subtotal,TaxAmount,TotalAmount,Notes,CreatedBy,CreatedAt FROM Invoices WHERE InvoiceId=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return new Invoices
            {
                InvoiceId = r.GetInt32(0),
                InvoiceNumber = r.GetString(1),
                CustomerId = r.GetInt32(2),
                InvoiceDate = r.GetDateTime(3),
                Subtotal = r.GetDecimal(4),
                TaxAmount = r.GetDecimal(5),
                TotalAmount = r.GetDecimal(6),
                Notes = r.IsDBNull(7) ? null : r.GetString(7),
                CreatedBy = r.IsDBNull(8) ? null : r.GetString(8),
                CreatedAt = r.GetDateTime(9)
            };
        }
        public IEnumerable<Invoices> GetInvoicesByCustomer(int customerId)
        {
            var list = new List<Invoices>();
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "SELECT InvoiceId,InvoiceNumber,CustomerId,InvoiceDate,Subtotal,TaxAmount,TotalAmount FROM Invoices WHERE CustomerId=@Cid", con);
            cmd.Parameters.AddWithValue("@Cid", customerId);

            con.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Invoices
                {
                    InvoiceId = r.GetInt32(0),
                    InvoiceNumber = r.GetString(1),
                    CustomerId = r.GetInt32(2),
                    InvoiceDate = r.GetDateTime(3),
                    Subtotal = r.GetDecimal(4),
                    TaxAmount = r.GetDecimal(5),
                    TotalAmount = r.GetDecimal(6)
                });
            }
            return list;
        }

        //Check user is exist or not..
        public bool CustomerExists(string Email)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "SELECT COUNT(1) FROM Customers WHERE Email = @Email", con);
            cmd.Parameters.AddWithValue("@Email", Email);
            con.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}
