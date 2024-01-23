using Library_Management.Entities;

namespace Library_Management.Interfaces
{
    public interface ILibrarianService
    {
        public Task<Librarian> Signup(Librarian librarian);
        public Task<List<Librarian>> GetAllLibrarians();
        public Task<Librarian> Login(string email, string pass);
        public Task<Librarian> UpdateLibrarian(Librarian librarian);
        public Task<Librarian> DeleteLibrarian(string id);
    }
}
