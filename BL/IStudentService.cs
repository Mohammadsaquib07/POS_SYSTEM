using Products_Crud.Model;

namespace Products_Crud.BL
{
    public interface IStudentService
    {
        Task AddStudent(Student s);
        Task<List<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task UpdateStudent(Student s);
        Task DeleteStudent(int id);
    }
}
