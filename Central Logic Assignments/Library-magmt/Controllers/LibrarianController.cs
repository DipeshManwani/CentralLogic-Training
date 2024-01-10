
using Library_magmt.DTO;
using Library_magmt.Entity;
using Library_Management.DTO;
using Library_Management.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using librarian = Library_magmt.Entity.Librarian;
using Librarian = Library_Management.Entity.Librarian;


namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "library-mgmt";
        public string ContainerName = "library";

        public readonly Microsoft.Azure.Cosmos.Container _container;
        public LibrarianController()
        {
            _container = GetContainer();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(LibrarianModel librarianModel)
        {
            try
            {
                Librarian librarian = new Librarian();

                librarian.Name = librarianModel.Name;
                librarian.MobileNo = librarianModel.MobileNo;
                librarian.EmailId = librarianModel.EmailId;
                librarian.Address = librarianModel.Address;
                librarian.Password = librarianModel.Password;


                librarian.Id = Guid.NewGuid().ToString();
                librarian.UId = librarian.Id;
                librarian.DocumentType = "librarian";

                librarian.CreatedOn = DateTime.Now;
                librarian.CreatedByName = "Dipesh";
                librarian.CreatedBy = "Dipesh's UId";

                librarian.UpdatedOn = DateTime.Now;
                librarian.UpdatedByName = "Dipesh";
                librarian.UpdatedBy = "Dipesh's UId";

                librarian.Version = 1;
                librarian.Active = true;
                librarian.Archieved = false;

                Librarian response = await _container.CreateItemAsync(librarian);

                librarianModel.Name = response.Name;

                return Ok("login Sucessfully!!!");

            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpPost]
        public IActionResult Login(string emailId, string password)
        {
            try
            {

                Librarian librarian = _container.GetItemLinqQueryable<Librarian>(true).Where(q => q.DocumentType == "librarian" && q.EmailId == emailId && q.Password == password).AsEnumerable().FirstOrDefault();
                if (librarian != null)
                {
                    return Ok("Login Successfully !!! ");
                }
                else
                {
                    return BadRequest("Invalid Credentials !!!");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Data Adding Failed" + ex);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateLibrarian(LibrarianModel librarianModel)
        {
            try
            { 
            

            var existinglibrarian = _container.GetItemLinqQueryable<Librarian>(true).Where(q => q.UId == librarianModel.UId && q.DocumentType == "librarian" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();

            existinglibrarian.Archieved = true;
            await _container.ReplaceItemAsync(existinglibrarian, existinglibrarian.Id);


            existinglibrarian.Id = Guid.NewGuid().ToString();
            existinglibrarian.UpdatedBy = "Dipesh UId";
            existinglibrarian.UpdatedByName = "Dipesh Manwani";
            existinglibrarian.UpdatedOn = DateTime.Now;
            existinglibrarian.Version = existinglibrarian.Version + 1;
            existinglibrarian.Active = true;
            existinglibrarian.Archieved = false;

            existinglibrarian.Name= librarianModel.Name;
            existinglibrarian.MobileNo = librarianModel.MobileNo;
            existinglibrarian.EmailId = librarianModel.EmailId.ToLower();
            existinglibrarian.Password = librarianModel.Password;
           



            existinglibrarian = await _container.CreateItemAsync(existinglibrarian);

            LibrarianModel model = new LibrarianModel();
            model.UId = existinglibrarian.UId;
            model.Name = existinglibrarian.Name;
            model.MobileNo = existinglibrarian.MobileNo;
            model.EmailId = existinglibrarian.EmailId;
            model.Password = existinglibrarian.Password;
          




            return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteLibrarian(string LibrarianUId)
        {
            try { 
            var librarian = _container.GetItemLinqQueryable<Librarian>(true).Where(q => q.UId == LibrarianUId && q.DocumentType == "librarian" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();

            librarian.Active = false;
            await _container.ReplaceItemAsync(librarian, librarian.Id);


            return Ok(true);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllBooks()
        {
            try { 
            //step 1 get all student
            var Booklineup = _container.GetItemLinqQueryable<Book>(true).Where(q => q.DocumentType == "book" && q.Archieved == false && q.Active == true).AsEnumerable().ToList();

            //step 2 mapping all data
            List<BookModel> bookModelist = new List<BookModel>();
            foreach (var book in Booklineup)
            {
                BookModel model = new BookModel();
              
                    model.UId = book.UId;
                    model.BookId = book.BookId;
                    model.BookName = book.BookName;
                    model.AuthorName = book.AuthorName;
                    model.BookType = book.BookType;

                    bookModelist.Add(model);
            }
            return Ok(bookModelist);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ShowissueBooks()
        {
            try { 
            //step 1 get all student
            var Booklist = _container.GetItemLinqQueryable<BorrowReturn>(true).Where(q => q.DocumentType == "BorrowReturn"  && q.Archieved == false && q.Active == true && q.bookIssue == true).AsEnumerable().ToList();

            
            //step 2 mapping all data
            List<BorrowReturnModel> bookModelist = new List<BorrowReturnModel>();
            foreach (var book in Booklist)
            {
                BorrowReturnModel model = new BorrowReturnModel();
                model.BookUid = book.BookUid;
                model.bookIssue = book.bookIssue;
                model.IssueDate = book.IssueDate;
                model.returnBook = book.returnBook;
                model.ReturnDate = book.ReturnDate;

               

                bookModelist.Add(model);
            }
            return Ok(bookModelist);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }
        private Microsoft.Azure.Cosmos.Container GetContainer()
        {
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosclient.GetDatabase(DatabaseName);
            Microsoft.Azure.Cosmos.Container container = database.GetContainer(ContainerName);
            return container;
        }
    }
}