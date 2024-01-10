using Newtonsoft.Json;

namespace Library_magmt.DTO
    {
        public class BorrowReturnModel
        {
            //[JsonProperty(PropertyName = "bookName", NullValueHandling = NullValueHandling.Ignore)]
            //public string? BookName { get; set; }

           // [JsonProperty(PropertyName = "studentName", NullValueHandling = NullValueHandling.Ignore)]
            //public string? SstudentName { get; set; }

            [JsonProperty(PropertyName = "bookIssue", NullValueHandling = NullValueHandling.Ignore)]
            public bool bookIssue { get; set; }


            [JsonProperty(PropertyName = "issueDate", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime IssueDate { get; set; }

            [JsonProperty(PropertyName = "returnBook", NullValueHandling = NullValueHandling.Ignore)]
            public bool returnBook { get; set; }

            [JsonProperty(PropertyName = "returnDate", NullValueHandling = NullValueHandling.Ignore)]
            public DateTime ReturnDate { get; set; }


            [JsonProperty(PropertyName = "bookUid", NullValueHandling = NullValueHandling.Ignore)]
            public string? BookUid { get; set; }


    }
}


