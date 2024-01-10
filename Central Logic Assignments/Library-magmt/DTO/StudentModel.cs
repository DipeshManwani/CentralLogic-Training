using Newtonsoft.Json;
using System.Globalization;

namespace Library_Management.DTO
{
    public class StudentModel
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string  UId { get; set; }

        [JsonProperty(PropertyName = "prnNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int PrnNumber { get; set; }
      
        [JsonProperty(PropertyName = "studentName", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentName { get; set; }

        [JsonProperty(PropertyName = "branchName", NullValueHandling = NullValueHandling.Ignore)]
        public string BranchName { get; set; }

        [JsonProperty(PropertyName = "contactNumber", NullValueHandling = NullValueHandling.Ignore)]
        public double ContactNumber { get; set; }

        [JsonProperty(PropertyName = "studentAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentAddress { get; set; }

        [JsonProperty(PropertyName = "graduationYear", NullValueHandling = NullValueHandling.Ignore)]
        public string GraduationYear { get; set; }

        [JsonProperty(PropertyName = "studentEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentEmail { get; set; }

        [JsonProperty(PropertyName = "studentPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentPassword { get; set; }

    }
}
