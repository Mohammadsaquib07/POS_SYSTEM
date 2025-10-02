using Products_Crud.Model;

namespace Products_Crud.BL
{
    public class UpdatEmployeeServices :IEmployeeUpdateService
    {
        private readonly IEmployeeUpdateRepository _employeeUpdateRepository;
        public UpdatEmployeeServices(IEmployeeUpdateRepository UpdatEmployeeServicesObj)
        {
            _employeeUpdateRepository = UpdatEmployeeServicesObj;
        }

        public void UpdateEmployeeData(int id, Employees emp)
        {
            _employeeUpdateRepository.UpdateEmployee(emp);
        }
    }
}
