using ERP.Modules.Shared.Domain;

namespace ERP.Modules.Shared.Contracts
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
