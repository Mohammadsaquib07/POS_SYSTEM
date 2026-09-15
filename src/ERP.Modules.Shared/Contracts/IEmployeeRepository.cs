using Products_Crud.Model;

namespace Products_Crud.Common.Contracts
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee emp);
    }

    public interface IEmployeeUpdateRepository
    {
        void UpdateEmployee(Employee emp);
    }

    public interface IEmployeeDeleteRepository
    {
           void DeleteEmployee(int empId);
    }
}
