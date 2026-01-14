using Microsoft.EntityFrameworkCore;
using Products_Crud.Interfaces;
using Products_Crud.Model;

namespace Products_Crud.DAL
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }
        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
        }
        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
