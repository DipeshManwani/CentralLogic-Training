using Library_magmt.DTO;
using Library_magmt.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System;

namespace Library_magmt.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BorrowReturnController : ControllerBase
    {

        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "library-mgmt";
        public string ContainerName = "library";

        public readonly Container container;
        

        public BorrowReturnController()
        {
            container = GetContainer();
        }

        [HttpGet]
        public async Task<IActionResult> IssueBook(int studentprnnumber, string bookid)
        {
            try
            {
                var student = container.GetItemLinqQueryable<Student>(true).Where(s => s.PrnNumber == studentprnnumber).AsEnumerable().FirstOrDefault();

                var book = container.GetItemLinqQueryable<Book>(true).Where(b => b.BookId == bookid).AsEnumerable().FirstOrDefault();

                if (student == null || book == null)
                {
                    return NotFound("Student or Book not found.");
                }
                
                else
                {
                  
                    BorrowReturn brbook = new BorrowReturn();
                    brbook.BookUid = bookid;
                    brbook.bookIssue = true;
                    brbook.IssueDate = DateTime.Now;
                    brbook.returnBook = false;
                    brbook.ReturnDate = DateTime.Now.AddDays(7);

                   
                    brbook.Id= Guid.NewGuid().ToString();
                    brbook.UId = brbook.Id;
                    brbook.DocumentType = "BorrowReturn";
                    brbook.CreatedBy = "Dipesh's UId ";
                    brbook.CreatedByName = "Dipesh Manwani";
                    brbook.CreatedOn = DateTime.Now;
                    brbook.UpdatedBy = "";
                    brbook.UpdatedByName = "";
                    brbook.UpdatedOn = DateTime.Now;
                    brbook.Version = 1;
                    brbook.Active = true;
                    brbook.Archieved = false;

                   
                    //Add data to database
                    await container.CreateItemAsync(brbook);


                    return Ok(" Book issued successfully ");
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ReturnBook(int studentprnnumber, string bookid)
        {
            try
            {
                var book = container.GetItemLinqQueryable<Book>(true).Where(b => b.BookId == bookid).AsEnumerable().FirstOrDefault();

                var Istrue = container.GetItemLinqQueryable<BorrowReturn>(true).Where(b =>b.bookIssue == true).AsEnumerable().FirstOrDefault();
                var student = container.GetItemLinqQueryable<Student>(true).Where(s => s.PrnNumber == studentprnnumber).AsEnumerable().FirstOrDefault();
                if (Istrue == null || student == null || book == null)
                {
                    return NotFound("book is not  issued to this student");
                }
                else
                {
                    BorrowReturn brbook = new BorrowReturn();
                    brbook.BookUid = bookid;
                    brbook.bookIssue = false;
                    brbook.IssueDate = DateTime.Now;
                    brbook.returnBook = true;
                    brbook.ReturnDate = DateTime.Now.AddDays(7);

                    //step 2 Assign madetory fields
                    brbook.Id = Guid.NewGuid().ToString();
                    brbook.UId = brbook.Id;
                    brbook.DocumentType = "BorrowReturn";
                    brbook.CreatedBy = "Dipesh's UId ";
                    brbook.CreatedByName = "Dipesh Manwani";
                    brbook.CreatedOn = DateTime.Now;
                    brbook.UpdatedBy = "";
                    brbook.UpdatedByName = "";
                    brbook.UpdatedOn = DateTime.Now;
                    brbook.Version = 1;
                    brbook.Active = true;
                    brbook.Archieved = false;

                    
                    //stp 3 Add data to database
                    await container.CreateItemAsync(brbook);


                    return Ok($"Book returned successfully ");
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
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

 