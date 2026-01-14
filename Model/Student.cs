namespace Products_Crud.Model
{
    public class Student
    {
        public int Id { get; set; }          
        public string Name { get; set; }    
        public DateTime JoinedAt { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new();
    }
}
