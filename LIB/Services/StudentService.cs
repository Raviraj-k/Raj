using Library_Management.DTO;
using Library_Management.Entities;
using Library_Management.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management.Services
{
    public class StudentService : IStudentService
    {
        public readonly Container container;
        public StudentService()
        {
            container = GetContainer();
        }
        public async Task<Student> Signup(Student student)
        {
            student.Id = Guid.NewGuid().ToString();
            student.UId = student.Id;
            student.DocumentType = "Student";
            student.CreatedBy = student.UId;
            student.CreatedOn = DateTime.Now;
            student.UpdatedBy = "";
            student.Version = 1;
            student.Active = true;
            student.Archieved = false;

            Student response = await container.CreateItemAsync(student);
            return response;
        }
        public async Task<List<Student>> GetAllStudents()
        {
            List<Student> list = container.GetItemLinqQueryable<Student>(true).AsEnumerable().ToList();
            return list;
        }
        public async Task<Student> Login(string email,string pass)
        {
            Student response = container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "Student" && q.Email == email && q.StudentPassword == pass).AsEnumerable().FirstOrDefault();
            return response;
        }
        private static Container? GetContainer()
        {
            try
            {
                // fetching environment variables
                string Uri = Environment.GetEnvironmentVariable("URI");
                string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
                string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
                string ContainerName = "LibrarianContainer";

                // creating container
                CosmosClient cosmosclient = new CosmosClient(Uri, PrimaryKey);
                Database database = cosmosclient.GetDatabase(DatabaseName);
                Container studentContainer = database.GetContainer(ContainerName);
                Console.WriteLine("Database connected successfully!!!");
                return studentContainer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured during generating container :" + ex);
                return null;
            }
        }
        public async Task<Student> UpdateStudent(Student student)
        {
            var existingstudent = container.GetItemLinqQueryable<Student>(true).Where(q => q.UId == student.UId && q.DocumentType == "Student" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            existingstudent.Archieved = true;
            existingstudent.Active = false;

            await container.ReplaceItemAsync(existingstudent, existingstudent.Id);

            existingstudent.Id = Guid.NewGuid().ToString();
            existingstudent.UId = student.UId;
            existingstudent.UpdatedBy = "";
            existingstudent.UpdatedOn = DateTime.Now;
            existingstudent.Version = existingstudent.Version + 1;
            existingstudent.Active = true;
            existingstudent.Archieved = false;

            existingstudent.Email = student.Email.ToLower();
            existingstudent.StudentName = student.StudentName;
            existingstudent.StudentPassword = student.StudentPassword;
            existingstudent.PrnNumber = student.PrnNumber;

            existingstudent = await container.CreateItemAsync(existingstudent);

            return existingstudent;
        }
        public async Task<Student> DeleteStudent(string id)
        {
            Student student = container.GetItemLinqQueryable<Student>(true).Where(q => q.UId == id && q.DocumentType == "Student" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            student.Active = false;
            var response = await container.ReplaceItemAsync(student, student.Id);
            return response;
        }

    }
}
