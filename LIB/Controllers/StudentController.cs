using Library_Management.DTO;
using Library_Management.Entities;
using Library_Management.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel.DataAnnotations;
using Container = Microsoft.Azure.Cosmos.Container;


namespace Library_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public Container _container;
        // Dependency Injection
        public IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            try
            {
                _container = GetContainer();  
                _studentService = studentService;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error occured while initialization of student controller : "+ex);
            }
        }
        private static Container? GetContainer()
        {
            try
            {
                // fetching environment variables
                string Uri = Environment.GetEnvironmentVariable("URI");
                string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
                string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
                string ContainerName = "StudentContainer";

                // creating container
                CosmosClient cosmosclient = new CosmosClient(Uri, PrimaryKey);
                Database database = cosmosclient.GetDatabase(DatabaseName);
                Container studentContainer = database.GetContainer(ContainerName);
                Console.WriteLine("Database connected successfully!!!");
                return studentContainer;
            } catch(Exception ex)
            {
                Console.WriteLine("Error occured during generating container :"+ex);
                return null;
            }
        }
        // Add student
        [HttpPost]
        [Route("/Signup")]
        public async Task<IActionResult> Signup(StudentModel studentModel)
        {
            try
            {
                // Step 1 -> Convert studentModel to Student entity
                Student student = new Student();
                student.StudentName = studentModel.StudentName;
                student.PrnNumber = studentModel.PrnNumber;
                student.StudentPassword = studentModel.StudentPassword;
                student.Email = studentModel.StudentEmail;

                // Step 2 -> Call service function
                var response = await _studentService.Signup(student);

                // Step 3 -> Return model to user interface
                StudentModel model = new StudentModel();
                model.UId = response.UId;
                model.StudentName = response.StudentName;
                model.PrnNumber = response.PrnNumber;

                // Step 4 -> Return the model
                return Ok(model);
            } 
            catch(Exception ex)
            {
                Console.WriteLine("Failed to add student" + ex);
                return BadRequest("Add student failed");
            }
        }
        // Get All students
        [HttpGet]
        [Route("/GetAll")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var response = await _studentService.GetAllStudents();
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest("Error occured while getting all students :" + ex);
            }
        }
        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(string email,string password)
        {
            if (email == null || password == null)
            {
                return BadRequest("please provide valid credentials!!");
            }
            try
            {
                var emailId = email.ToLower();
                var response = await _studentService.Login(email,password);
                if(response != null)
                {
                    return Ok(response.UId);
                }
                else
                {
                    return BadRequest("login failed");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("login failed" + ex);
            }
        }
        [HttpPut]
        [Route("/UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(StudentModel studentModel)
        {
            try
            {
                if(studentModel.UId == null)
                {
                    return BadRequest("please enter valid uid");
                }
                Student student = new Student();
                student.StudentName = studentModel.StudentName;
                student.PrnNumber = studentModel.PrnNumber;
                student.Email = studentModel.StudentEmail;
                student.StudentPassword = studentModel.StudentPassword;
                student.UId = studentModel.UId;

                var updatedStudent = await _studentService.UpdateStudent(student);

                studentModel.StudentEmail = updatedStudent.Email;
                studentModel.StudentName = updatedStudent.StudentName;
                studentModel.PrnNumber = updatedStudent.PrnNumber;
                studentModel.StudentPassword = updatedStudent.StudentPassword;
                return Ok(studentModel);
            }
            catch (Exception ex)
            {
                return BadRequest("Update student failed !! : "+ex);
            }
        }
        [HttpDelete]
        [Route("/DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            try
            {
                if(id == null)
                {
                    return BadRequest("ivalid id");
                }
                var student = _studentService.DeleteStudent(id);
                return Ok("student deleted successfully!!");
            }
            catch(Exception ex)
            {
                return BadRequest("Delete failed!!");
            }
        }
        
    }
}
