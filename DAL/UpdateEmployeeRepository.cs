using Microsoft.Data.SqlClient;
using Products_Crud.Model;

namespace Products_Crud.DAL
{
    public class UpdateEmployeeRepository:IEmployeeUpdateRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public UpdateEmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Sql_Connection_String");
        }


        public void UpdateEmployee(Employees emp)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE EMPDETAILS 
                         SET ENAME = @ENAME, Email = @Email, Occupation = @Occupation, Salary = @Salary 
                         WHERE Eid = @Eid";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EID", emp.Eid);
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
}
