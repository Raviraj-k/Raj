using Library_Management.DTO;
using Library_Management.Entities;
using Library_Management.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public Container _container;
        // Dependency Injection
        public IBookService _bookService;
        public BookController(IBookService bookService)
        {
            try
            {
                _container = GetContainer();
                _bookService = bookService;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured while initialization of Book controller : " + ex);
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
                string ContainerName = "BookContainer";

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
        [HttpPost]
        [Route("/AddBook")]
        public async Task<IActionResult> AddBook(BookModel bookModel)
        {
            try
            {
                // Step 1 -> Convert studentModel to Student entity
                Book book = new Book();
                book.BookName = bookModel.BookName;
                book.AuthorName = bookModel.AuthorName;

                // Step 2 -> Call service function
                var response = await _bookService.AddBook(book);

                // Step 3 -> Return model to user interface
                BookModel model = new BookModel();
                model.BookId = response.BookId;
                model.BookName = response.BookName;
                model.AuthorName = response.AuthorName;

                // Step 4 -> Return the model
                return Ok(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to add book" + ex);
                return BadRequest("Add student failed");
            }
        }
        [HttpGet]
        [Route("/GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                List<Book> response = await _bookService.GetAllBooks();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured while getting all book :" + ex);
            }
        }
        [HttpPost]
        [Route("/GetBookById")]
        public async Task<IActionResult> GetBook(string bookId)
        {
            if (bookId == null)
            {
                return BadRequest("please provide valid book id!!");
            }
            try
            {
                var response = await _bookService.GetBook(bookId);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest("no book present");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("get book failed" + ex);
            }
        }
        [HttpPut]
        [Route("/UpdateBook")]
        public async Task<IActionResult> UpdateBook(BookModel bookModel)
        {
            try
            {
                if (bookModel.BookId == null)
                {
                    return BadRequest("please enter valid bookid");
                }
                Book book = new Book();
                book.BookName = bookModel.BookName;
                book.AuthorName = bookModel.AuthorName;
                book.BookId = bookModel.BookId;

                var updatedBook = await _bookService.UpdateBook(book);

                bookModel.BookId = updatedBook.BookId;
                bookModel.AuthorName = updatedBook.AuthorName;
                bookModel.BookName = updatedBook.BookName;
                return Ok(bookModel);
            }
            catch (Exception ex)
            {
                return BadRequest("Update book failed !! : " + ex);
            }
        }
        [HttpDelete]
        [Route("/DeleteBook")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("ivalid id");
                }
                var student = _bookService.DeleteBook(id);
                return Ok("book deleted successfully!!");
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed!!");
            }
        }
    }
}
