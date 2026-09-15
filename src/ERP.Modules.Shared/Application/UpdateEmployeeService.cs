using Products_Crud.Model;
using Products_Crud.Common.Contracts;

namespace Products_Crud.BL
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
