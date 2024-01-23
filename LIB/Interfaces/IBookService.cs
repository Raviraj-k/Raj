using Library_Management.Entities;

namespace Library_Management.Interfaces
{
    public interface IBookService
    {
        public Task<Book> AddBook(Book book);
        public Task<Book> DeleteBook(string id);
        public Task<List<Book>> GetAllBooks();
        public Task<Book> GetBook(string bookId);
        public Task<Book> UpdateBook(Book book);
    }
}
