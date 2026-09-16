using Microsoft.Data.SqlClient;
using ERP.Modules.Shared.Domain;
using ERP.Modules.Shared.Contracts;
using Microsoft.Extensions.Configuration;
namespace ERP.Modules.Shared.Infrastructure
{
    public class EmployeeDelete: IEmployeeDeleteRepository
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmployeeDelete(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Sql_Connection_String");
        }
        public void DeleteEmployee(int empId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM EMPDETAILS WHERE Eid = @Eid";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Eid", empId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
