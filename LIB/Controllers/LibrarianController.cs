using Library_Management.DTO;
using Library_Management.Entities;
using Library_Management.Interfaces;
using Library_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {
        public Container _container;
        // Dependency Injection
        public ILibrarianService _librarianService;
        public LibrarianController(ILibrarianService librarianService)
        {
            try
            {
                _container = GetContainer();
                _librarianService = librarianService;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while initialization of librarian controller : " + ex);
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
        // Add librarian
        [HttpPost]
        [Route("/LibrarianSignup")]
        public async Task<IActionResult> Signup(LibrarianModel librarianModel)
        {
            try
            {
                // Step 1 -> Convert studentModel to Student entity
                Librarian librarian = new Librarian();
                librarian.Name = librarianModel.Name;
                librarian.MobileNo = librarianModel.MobileNo;
                librarian.EmailId = librarianModel.EmailId;
                librarian.Password = librarianModel.Password;

                // Step 2 -> Call service function
                var response = await _librarianService.Signup(librarian);

                // Step 3 -> Return model to user interface
                LibrarianModel model = new LibrarianModel();
                model.UId = response.UId;
                model.Name = response.Name;
                model.MobileNo = response.MobileNo;

                // Step 4 -> Return the model
                return Ok(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add librarian" + ex);
                return BadRequest("Add librarian failed"+ex);
            }
        }
        // Get All librarians
        [HttpGet]
        [Route("/GetAllLibrarians")]
        public async Task<IActionResult> GetAllLibrarians()
        {
            try
            {
                var response = await _librarianService.GetAllLibrarians();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured while getting all students :" + ex);
            }
        }
        [HttpPost]
        [Route("/LibrarianLogin")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (email == null || password == null)
            {
                return BadRequest("please provide valid credentials!!");
            }
            try
            {
                var emailId = email.ToLower();
                var response = await _librarianService.Login(email, password);
                if (response != null)
                {
                    return Ok(response.UId);
                }
                else
                {
                    return BadRequest("please enter valid credentials");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("login failed" + ex);
            }
        }
        [HttpPut]
        [Route("/UpdateLibrarian")]
        public async Task<IActionResult> UpdateStudent(LibrarianModel librarianModel)
        {
            try
            {
                if (librarianModel.UId == null)
                {
                    return BadRequest("please enter valid uid");
                }
                Librarian librarian = new Librarian();
                librarian.Name = librarianModel.Name;
                librarian.MobileNo = librarianModel.MobileNo;
                librarian.EmailId = librarianModel.EmailId;
                librarian.Password = librarianModel.Password;
                librarian.UId = librarianModel.UId;

                var updatedLibrarian = await _librarianService.UpdateLibrarian(librarian);

                librarianModel.EmailId = updatedLibrarian.EmailId;
                librarianModel.Name = updatedLibrarian.Name;
                librarianModel.MobileNo = updatedLibrarian.MobileNo;
                librarianModel.Password = updatedLibrarian.Password;
                return Ok(librarianModel);
            }
            catch (Exception ex)
            {
                return BadRequest("Update student failed !! : " + ex);
            }
        }
        [HttpDelete]
        [Route("/DeleteLibrarian")]
        public async Task<IActionResult> DeleteLibrarian(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("ivalid id");
                }
                var librarian = _librarianService.DeleteLibrarian(id);
                return Ok("librarian deleted successfully!!");
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed!!");
            }
        }

    }
}
