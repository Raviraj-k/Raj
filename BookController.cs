using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
    }



    /*
     feature :

   1-  user : [ I can be able to login and singup ] 
          steps : 1 students must able to singup ( add student )
          steps : 2 Stundets must be able to log in ( request : username and pass .... ) return (uid):

    need : student (dtype)   - CRUD
           librarian      - CRUD  
       
    Deadline : Tomarrow (3 Jan ) 


    2 - Book  
Features : 

    1- Add Book , Delete , Update , Issue book , return book , Request (xyz) 
    2 -  search by book-name , author , subject (get)


    librarian : 
    show books in library 
    show borrowed books 
    show total books 
     
    Need : Book CRUD
     
     */
}
