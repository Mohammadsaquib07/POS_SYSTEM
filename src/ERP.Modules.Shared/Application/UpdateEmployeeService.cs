using ERP.Modules.Shared.Domain;
using ERP.Modules.Shared.Contracts;

namespace ERP.Modules.Shared.Application
{
    public class UpdateEmployeeService : IEmployeeUpdateService
    {
        private readonly IEmployeeUpdateRepository _employeeUpdateRepository;
        public UpdateEmployeeService(IEmployeeUpdateRepository employeeUpdateRepository)
        {
            _employeeUpdateRepository = employeeUpdateRepository;
        }

        public void UpdateEmployeeData(int id, Employee emp)
        {
            _employeeUpdateRepository.UpdateEmployee(emp);
        }
    }
}
