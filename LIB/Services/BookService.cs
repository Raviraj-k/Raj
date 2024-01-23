using Library_Management.Entities;
using Library_Management.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Library_Management.Services
{
    public class BookService : IBookService
    {
        public readonly Container container;
        public BookService()
        {
            container = GetContainer();
        }
        public async Task<Book> AddBook(Book book)
        {
            book.Id = Guid.NewGuid().ToString();
            book.BookId = book.Id;
            book.DocumentType = "Book";
            book.CreatedBy = book.Id;
            book.CreatedOn = DateTime.Now;
            book.UpdatedBy = "";
            book.Version = 1;
            book.Active = true;
            book.Archieved = false;

            Book response = await container.CreateItemAsync(book);
            return response;
        }
        public async Task<List<Book>> GetAllBooks()
        {
            List<Book> list = container.GetItemLinqQueryable<Book>(true).AsEnumerable().ToList();
            return list;
        }
        public async Task<Book> GetBook(string id)
        {
            Book response = container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "Book" && q.Id == id).AsEnumerable().FirstOrDefault();
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
        public async Task<Book> UpdateBook(Book book)
        {
            var existingBook = container.GetItemLinqQueryable<Book>(true).Where(q => q.BookId == book.BookId && q.DocumentType == "Book" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            existingBook.Archieved = true;
            existingBook.Active = false;

            await container.ReplaceItemAsync(existingBook, existingBook.Id);

            existingBook.Id = Guid.NewGuid().ToString();
            existingBook.BookId = book.BookId;
            existingBook.UpdatedBy = "";
            existingBook.UpdatedOn = DateTime.Now;
            existingBook.Version = existingBook.Version + 1;
            existingBook.Active = true;
            existingBook.Archieved = false;

            existingBook.AuthorName = book.AuthorName;
            existingBook.BookName = book.BookName;

            existingBook = await container.CreateItemAsync(existingBook);

            return existingBook;
        }
        public async Task<Book> DeleteBook(string id)
        {
            Book book = container.GetItemLinqQueryable<Book>(true).Where(q => q.BookId == id && q.DocumentType == "Book" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            book.Active = false;
            var response = await container.ReplaceItemAsync(book, book.BookId);
            return response;
        }
    }
}
