using Products_Crud.Model;

namespace Products_Crud.BL
{
    public interface IEmployeeUpdateService
    {
        void UpdateEmployeeData(int id, Employees emp);
    }
}
