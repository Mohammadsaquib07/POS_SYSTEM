namespace Products_Crud.Model
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employees emp);
    }
    public interface IEmployeeUpdateRepository
    {
        void UpdateEmployee(Employees emp);
    }
    public interface IEmployeeDeleteRepository
    {
        void DeleteEmployee(int empId);
    }

}
