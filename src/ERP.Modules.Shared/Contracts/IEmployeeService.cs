using ERP.Modules.Shared.Domain;

namespace ERP.Modules.Shared.Application
{
    public interface IEmployeeUpdateService
    {
        void UpdateEmployeeData(int id, Employee emp);
    }
}

