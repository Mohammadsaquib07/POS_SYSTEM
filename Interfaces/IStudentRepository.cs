using Products_Crud.Model;

namespace Products_Crud.Interfaces
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student);
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
