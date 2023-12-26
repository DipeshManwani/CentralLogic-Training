using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Task_M.DTO
{
    public class EmployeeDTO
    {
        [JsonProperty(PropertyName = "taskName", NullValueHandling = NullValueHandling.Ignore)]
        public string TaskName { get; set; }

        [JsonProperty(PropertyName = "taskDescription", NullValueHandling = NullValueHandling.Ignore)]
        public string TaskDescription { get; set; }

    }
}
