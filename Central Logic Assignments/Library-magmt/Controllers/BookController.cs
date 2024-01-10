using Library_magmt.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "library-mgmt";
        public string ContainerName = "library";

        public readonly Container _container;
        public BookController()
        {
            _container = GetContainer();
        }

        //Add Books 
        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel bookModel)
        {
            try
            {
                Book books = new Book();

                books.BookId = bookModel.BookId;
                books.BookName = bookModel.BookName;
                books.AuthorName = bookModel.AuthorName;
                books.BookType = bookModel.BookType;

                books.Id = Guid.NewGuid().ToString();
                books.UId = books.BookId;
                books.DocumentType = "book";

                books.CreatedOn = DateTime.Now;
                books.CreatedBy = "Dipesh Manwani";
                books.CreatedByName =" Dipesh Manwani";

                books.UpdatedBy = "Dipesh Manwani";
                books.UpdatedByName = "DIPESH MANWANI";
                books.UpdatedOn = DateTime.Now;


                books.Version= 1;
                books.Active = true;
                books.Archieved = false;

                Book response = await _container.CreateItemAsync(books);

                Book model = new Book();

                model.CreatedOn = response.CreatedOn;
                model.CreatedBy = response.CreatedBy;
                model.CreatedByName = response.CreatedByName;

                model.UpdatedBy = response.UpdatedBy;
                model.UpdatedByName = response.UpdatedByName;
                model.UpdatedOn = response.UpdatedOn;


                model.UId = response.UId;
                model.BookId = response.BookId;
                model.BookName = response.BookName;
                model.AuthorName = response.AuthorName;
                model.BookType = response.BookType;

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }


        //Get Single Book
        [HttpGet]
        public IActionResult GetBook(string bookName, string bookId)
        {
            try
            {
                Book books = _container.GetItemLinqQueryable<Book>(true).Where(b => b.DocumentType == "book" && b.BookName == bookName || b.BookId == bookId).AsEnumerable().FirstOrDefault();

                var model = new Book();
                model.UId = books.UId;
                model.BookId = books.BookId;
                model.BookName = books.BookName;
                model.AuthorName = books.AuthorName;
                model.BookType = books.BookType;

                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }
        //Get Available Books
        [HttpGet]
        public IActionResult AvailableBooks()
        {
            try
            {
                var booksList = _container.GetItemLinqQueryable<Book>(true)
                    .Where(b => b.DocumentType == "book" && b.Active == true && b.Archieved == false)
                    .AsEnumerable().ToList();
                List<Book> bookDetails = new List<Book>();
                foreach (var book in booksList)
                {
                    Book model = new Book();
                    model.UId = book.UId;
                    model.BookId = book.BookId;
                    model.BookName = book.BookName;
                    model.AuthorName = book.AuthorName;
                    model.BookType = book.BookType;

                    bookDetails.Add(model);
                }
                return Ok(bookDetails);
            }
            catch
            {
                return BadRequest();
            }
        }

        //Get Not Available Books
        [HttpGet]
        public IActionResult NotAvailableBooks()
        {
            try
            {
                var bookList = _container.GetItemLinqQueryable<Book>(true)
                    .Where(b => b.DocumentType == "book" && b.Archieved == true && b.Active == true)
                    .AsEnumerable().ToList();
                List<Book> bookDetails = new List<Book>();
                foreach (var book in bookList)
                {
                    Book model = new Book();
                    model.UId = book.UId;
                    model.BookId = book.BookId;
                    model.BookName = book.BookName;
                    model.AuthorName = book.AuthorName;
                    model.BookType = book.BookType;

                    bookDetails.Add(model);
                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        //Update Book Details
        [HttpPut]
        public async Task<IActionResult> UpdateBookDetails(BookModel book)
        {
            try
            {
                var existingbook = _container.GetItemLinqQueryable<Book>(true).Where(b => b.UId == book.UId && b.DocumentType == "book" && b.Active == true && b.Archieved == false).AsEnumerable().FirstOrDefault();
               if (existingbook != null)
                {
                    existingbook.BookName = book.BookName;
                    existingbook.AuthorName = book.AuthorName;
                    existingbook.BookType = book.BookType;
                    existingbook.Version++;
                    var response = await _container.UpsertItemAsync(existingbook);
                    return Ok(response);

                }
               else
                {
                    return Ok("BOOK NOT FOUND");
                }
                
            }
            catch
            {
                return BadRequest();
            }
        }

        //Delete Book
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(string bookId)
        {
            try
            {
                var book = _container.GetItemLinqQueryable<Book>(true).Where(b => b.BookId == bookId && b.DocumentType == "book" && b.Archieved == false && b.Active == true).AsEnumerable().FirstOrDefault();
                book.Active = false;
                await _container.ReplaceItemAsync(book, book.Id);
                return Ok("Book Deleted SucessFully!!!");
            }
            catch
            {
                return BadRequest();
            }
        }

        private Container GetContainer()
        {
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;
        }
    }
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

