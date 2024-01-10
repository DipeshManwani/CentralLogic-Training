using Newtonsoft.Json;

namespace Library_magmt.DTO
{
    public class BorrowReturn
    {
        [JsonProperty(PropertyName = "bookUid", NullValueHandling = NullValueHandling.Ignore)]
        public string? BookUid { get; set; }

        //[JsonProperty(PropertyName = "bookName", NullValueHandling = NullValueHandling.Ignore)]
        //public string? BookName { get; set; }

        //[JsonProperty(PropertyName = "studentName", NullValueHandling = NullValueHandling.Ignore)]
        //public string? SstudentName { get; set; }

        [JsonProperty(PropertyName = "bookIssue", NullValueHandling = NullValueHandling.Ignore)]
        public bool bookIssue { get; set; }


        [JsonProperty(PropertyName = "issueDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime IssueDate { get; set; }

        [JsonProperty(PropertyName = "returnBook", NullValueHandling = NullValueHandling.Ignore)]
        public bool returnBook { get; set; }

        [JsonProperty(PropertyName = "returnDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ReturnDate { get; set; }

        //mandatory fields
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "dType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }


        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdByName", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedByName { get; set; }

        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedByName", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedByName { get; set; }

        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "archieved", NullValueHandling = NullValueHandling.Ignore)]
        public bool Archieved { get; set; }


    }
}
