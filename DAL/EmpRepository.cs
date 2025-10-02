using Microsoft.Data.SqlClient;
using Products_Crud.Model;
namespace Products_Crud.DAL
{
    //public class EmpRepository
    //{
    //    private readonly string _connectionString;
    //    public EmpRepository(IConfiguration configuration)
    //    {
    //        _connectionString = configuration.GetConnectionString("Sql_Connection_String");
    //    }
    //    public List<Employees> GetEmployeeData()
    //    {
    //        var employees = new List<Employees>();
    //        using (SqlConnection con = new SqlConnection(_connectionString))
    //        {
    //            con.Open();
    //            SqlCommand cmd = new SqlCommand("select * from EMPDETAILS",con);
    //            SqlDataReader reader = cmd.ExecuteReader();
    //            while (reader.Read())
    //            {
    //                employees.Add(new Employees
    //                {
    //                    Eid = (int)reader["EID"],
    //                    Name = reader["ENAME"].ToString(),
    //                    Email = reader["EMAIL"].ToString(),
    //                    Occupation = reader["OCCUPATION"].ToString(),
    //                    Salary = (int)reader["SALARY"]
    //                });
    //            }
    //        }
    //         return employees;
    //    }
    //}
}
