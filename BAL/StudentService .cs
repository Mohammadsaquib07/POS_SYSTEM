using Products_Crud.BL;
using Products_Crud.Interfaces;
using Products_Crud.Model;

namespace Products_Crud.BAL
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task AddStudent(Student s)
        {
            await _studentRepository.AddAsync(s);
            await _studentRepository.SaveAsync();
        }
        public async Task DeleteStudent(int id)
        {
            await _studentRepository.DeleteAsync(id);
            await _studentRepository.SaveAsync();
        }
        public async Task<Student> GetStudent(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }
        public async Task<List<Student>> GetStudents()
        {
            return await _studentRepository.GetAllAsync();
        }
        public async Task UpdateStudent(Student s)
        {
            await _studentRepository.UpdateAsync(s);
            await _studentRepository.SaveAsync();
        }
    }
}
