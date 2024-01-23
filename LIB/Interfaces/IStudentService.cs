using Library_Management.Entities;

namespace Library_Management.Interfaces
{
    public interface IStudentService
    {
        public Task<Student> Signup(Student student);
        public Task<List<Student>> GetAllStudents();
        public Task<Student> Login(string email, string pass);
        public Task<Student> UpdateStudent(Student student);
        public Task<Student> DeleteStudent(string id);
    }
}
