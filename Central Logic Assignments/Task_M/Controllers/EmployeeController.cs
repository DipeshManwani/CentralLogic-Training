using Task_M.DTO;
using Task_M.Entity;
using Task_M.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Container = Microsoft.Azure.Cosmos.Container;
using Task_M.Models;
using System.Threading.Tasks;
using Microsoft.VisualBasic;



namespace Task_M.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Task_Manager";
        public string ContainerName = "container2";

        public readonly Container container1;

        public EmployeeController()
        {
            container1 = GetContainer();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDTO employeeDTO)
        {
            try
            {
                employeecs employeeEntity = new employeecs();

                employeeEntity.TaskName = employeeDTO.TaskName;
                employeeEntity.TaskDescription = employeeDTO.TaskDescription;



                employeeEntity.Id = Guid.NewGuid().ToString();
                employeeEntity.UId = employeeEntity.Id;
                employeeEntity.DocumentType = "Employee";


                employeeEntity.CreatedOn = DateTime.Now;
                employeeEntity.CreatedByName = "DIPESH MANWANI";
                employeeEntity.CreatedBy = "Dipesh's UId";

                employeeEntity.UpdateOn = DateTime.Now;
                employeeEntity.UpdateByName = "DIPESH MANWANI";
                employeeEntity.UpdateBy = "Dipesh's UId";

                employeeEntity.Version = 1;
                employeeEntity.Active = true;
                employeeEntity.Archieved = false;

                employeecs resposne = await container1.CreateItemAsync(employeeEntity);

                // Reverse MApping 
                employeeDTO.TaskName = resposne.TaskName;
                employeeDTO.TaskDescription = resposne.TaskDescription;



                return Ok(employeeDTO);
            }
            catch (Exception ex)
            {

                return BadRequest("Data Adding Failed" + ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(string uId, string name, string taskDesc)
        {

            employeecs existingTask = container1.GetItemLinqQueryable<employeecs>(true).Where(q => q.DocumentType == "Employee" && q.UId == uId).AsEnumerable().FirstOrDefault();
            if (existingTask != null)
            {
                existingTask.TaskName = name;
                existingTask.TaskDescription = taskDesc;
                existingTask.Version++;

                try
                {
                    var response = await container1.UpsertItemAsync(existingTask, new PartitionKey(uId));
                    if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return Ok("Task Updated Successfully");
                    }
                    else
                    {
                        return BadRequest("Failed to Update Task");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest();
        }
            [HttpGet]
            public IActionResult GetemployeeByUId(string uId)
            {
                try
                {
                    employeecs employeecs = container1.GetItemLinqQueryable<employeecs>(true).Where(q => q.DocumentType == "employee" && q.UId == uId).AsEnumerable().FirstOrDefault();

                    // Reverse MApping 
                    var employeeModel = new employeecs();
                    employeeModel.TaskName = employeecs.TaskName;
                    employeeModel.Id = employeecs.Id;
                    employeeModel.TaskDescription = employeecs.TaskDescription;
                    return Ok(employeeModel);

                }
                catch (Exception ex)
                {

                    return BadRequest("Data Get Failed");
                }
            }
            [HttpGet]
            public IActionResult GetAllEmployee()
            {
                try
                {

                    var listresponse = container1.GetItemLinqQueryable<employeecs>(true).AsEnumerable().ToList();
                    return Ok(listresponse);

                }
                catch (Exception ex)
                {

                    return BadRequest("Data Get Failed");
                }
            }

            [HttpDelete]
            public async Task DeleteTaskAsync(string uId)
            {
                await container1.DeleteItemAsync<employeecs>(uId, new PartitionKey(uId));
            }
            private Container GetContainer()
            {
                CosmosClient cosmoscClient = new CosmosClient(URI, PrimaryKey);
                Database database = cosmoscClient.GetDatabase(DatabaseName);
                Container container = database.GetContainer(ContainerName);
                return container;
            }

        }
    }

