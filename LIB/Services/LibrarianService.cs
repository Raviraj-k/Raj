using Library_Management.Entities;
using Library_Management.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Library_Management.Services
{
    public class LibrarianService : ILibrarianService
    {
        public readonly Container container;
        public LibrarianService()
        {
            container = GetContainer();
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
        public async Task<Librarian> Signup(Librarian librarian)
        {
            librarian.Id = Guid.NewGuid().ToString();
            librarian.UId = librarian.Id;
            librarian.DocumentType = "Librarian";
            librarian.CreatedBy = librarian.UId;
            librarian.CreatedOn = DateTime.Now;
            librarian.UpdatedBy = "";
            librarian.Version = 1;
            librarian.Active = true;
            librarian.Archieved = false;

            Librarian response = await container.CreateItemAsync(librarian);
            return response;
        }
        public async Task<List<Librarian>> GetAllLibrarians()
        {
            List<Librarian> list = container.GetItemLinqQueryable<Librarian>(true).AsEnumerable().ToList();
            return list;
        }
        public async Task<Librarian> Login(string email, string pass)
        {
            Librarian response = container.GetItemLinqQueryable<Librarian>(true).Where(q => q.DocumentType == "Librarian" && q.EmailId == email && q.Password == pass && q.Active == true && q.Archieved == false).AsEnumerable().FirstOrDefault();
            return response;
        }
        public async Task<Librarian> UpdateLibrarian(Librarian librarian)
        {
            var existingLibrarian = container.GetItemLinqQueryable<Librarian>(true).Where(q => q.UId == librarian.UId && q.DocumentType == "Librarian" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            existingLibrarian.Archieved = true;
            existingLibrarian.Active = false;
            await container.ReplaceItemAsync(existingLibrarian, existingLibrarian.Id);

            existingLibrarian.Id = Guid.NewGuid().ToString();
            existingLibrarian.UId = librarian.UId;
            existingLibrarian.UpdatedBy = "";
            existingLibrarian.UpdatedOn = DateTime.Now;
            existingLibrarian.Version = existingLibrarian.Version + 1;
            existingLibrarian.Active = true;
            existingLibrarian.Archieved = false;

            existingLibrarian.EmailId = librarian.EmailId.ToLower();
            existingLibrarian.Name = librarian.Name;
            existingLibrarian.Password = librarian.Password;
            existingLibrarian.MobileNo = librarian.MobileNo;

            existingLibrarian = await container.CreateItemAsync(existingLibrarian);

            return existingLibrarian;
        }
        public async Task<Librarian> DeleteLibrarian(string id)
        {
            Librarian librarian = container.GetItemLinqQueryable<Librarian>(true).Where(q => q.UId == id && q.DocumentType == "Librarian" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
            librarian.Active = false;
            var response = await container.ReplaceItemAsync(librarian, librarian.Id);
            return response;
        }
    }
}
