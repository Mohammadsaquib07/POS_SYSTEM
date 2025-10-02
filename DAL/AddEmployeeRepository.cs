using Microsoft.Data.SqlClient;
using Products_Crud.Model;
namespace Products_Crud.DAL
{
    public class AddEmployeeRepository:IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public AddEmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Sql_Connection_String");
        }

        public void AddEmployee(Employees emp)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO EMPDETAILS (ENAME, EMAIL,OCCUPATION,SALARY) VALUES (@ENAME, @EMAIL,@OCCUPATION,@SALARY)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ENAME", emp.Name);
                cmd.Parameters.AddWithValue("@EMAIL", emp.Email);
                cmd.Parameters.AddWithValue("@OCCUPATION", emp.Occupation);
                cmd.Parameters.AddWithValue("@SALARY", emp.Salary);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
