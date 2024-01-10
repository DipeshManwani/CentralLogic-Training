using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Container = Microsoft.Azure.Cosmos.Container;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Library_Management.Entity;
using Library_Management.DTO;

namespace Library_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "library-mgmt";
        public string ContainerName = "library";

        public readonly Container _container;

        public StudentController()

        {
            _container = GetContainer();

        }


        [HttpPost]
        public async Task<IActionResult> SignUp(StudentModel studentModel)
        {
            try
            {
                Student student = new Student();

                student.StudentName = studentModel.StudentName;
                student.PrnNumber = studentModel.PrnNumber;
                student.ContactNumber = studentModel.ContactNumber;
                student.StudentEmail = studentModel.StudentEmail.ToLower();
                student.BranchName = studentModel.BranchName;
                student.GraduationYear = studentModel.GraduationYear;
                student.StudentAddress = studentModel.StudentAddress;
                student.StudentPassword = studentModel.StudentPassword;


                student.Id = Guid.NewGuid().ToString();
                student.UId = student.Id;
                student.DocumentType = "student";

                student.CreatedOn = DateTime.Now;
                student.CreatedByName = "DIPESH MANWANI";
                student.CreatedBy = "Dipesh's UId";

                student.UpdatedOn = DateTime.Now;
                student.UpdatedByName = "DIPESH MANWANI";
                student.UpdatedBy = "Dipesh's UId";

                student.Version = 1;
                student.Active = true;
                student.Archieved = false;

                Student response = await _container.CreateItemAsync(student);


                studentModel = ToStudentModel(response);

                return Ok("SignUp Successfull!!! ");

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
                emailId = emailId.ToLower();
                Student student = _container.GetItemLinqQueryable<Student>(true).Where(q => q.DocumentType == "student" && q.StudentEmail == emailId && q.StudentPassword == password).AsEnumerable().FirstOrDefault();
                if (student != null)
                {
                    return Ok(student.UId);
                }
                else
                {
                    return BadRequest("Invalid Credentials !!!");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Login Get Failed");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateStudent(StudentModel studentModel)
        {
            try
            {

                var existingstudent = _container.GetItemLinqQueryable<Student>(true).Where(q => q.UId == studentModel.UId && q.DocumentType == "student" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();
                existingstudent.Archieved = true;

                await _container.ReplaceItemAsync(existingstudent, existingstudent.Id);


                existingstudent.Id = Guid.NewGuid().ToString();
                existingstudent.UpdatedBy = "Dipesh manwani UId";
                existingstudent.UpdatedByName = "Dipesh manwani";
                existingstudent.UpdatedOn = DateTime.Now;
                existingstudent.Version = existingstudent.Version + 1;
                existingstudent.Active = true;
                existingstudent.Archieved = false;

                existingstudent.StudentAddress= studentModel.StudentAddress;
                existingstudent.StudentEmail = studentModel.StudentEmail.ToLower();
                existingstudent.StudentName= studentModel.StudentName;
                existingstudent.StudentPassword = studentModel.StudentPassword;
                existingstudent.BranchName= studentModel.BranchName;
                existingstudent.GraduationYear = studentModel.GraduationYear;
                existingstudent.PrnNumber = studentModel.PrnNumber;
                existingstudent.ContactNumber = studentModel.ContactNumber;
                

                existingstudent = await _container.CreateItemAsync(existingstudent);

                Student student = new Student();

                student.StudentName = studentModel.StudentName;
                student.PrnNumber = studentModel.PrnNumber;
                student.ContactNumber = studentModel.ContactNumber;
                student.StudentEmail = studentModel.StudentEmail.ToLower();
                student.BranchName = studentModel.BranchName;
                student.GraduationYear = studentModel.GraduationYear;
                student.StudentAddress = studentModel.StudentAddress;
                student.StudentPassword = studentModel.StudentPassword;

                return Ok(student);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(string StudentUId)
        {
            try
            {
                var student = _container.GetItemLinqQueryable<Student>(true).Where(q => q.UId == StudentUId && q.DocumentType == "student" && q.Archieved == false && q.Active == true).AsEnumerable().FirstOrDefault();

                student.Active = false;
                await _container.ReplaceItemAsync(student, student.Id);


                return Ok($"student record Delete successfully");
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

        private StudentModel ToStudentModel(Student student)
        {
            var studentModel = new StudentModel();
            studentModel.StudentName = student.StudentName;
            studentModel.PrnNumber = student.PrnNumber;
            studentModel.ContactNumber = student.ContactNumber;
            studentModel.StudentEmail = student.StudentEmail;
            studentModel.BranchName = student.BranchName;
            studentModel.GraduationYear = student.GraduationYear;
            studentModel.StudentAddress = student.StudentAddress;
            studentModel.StudentPassword = student.StudentPassword;
            studentModel.UId = student.UId;
            return studentModel;
        }
    }
}
