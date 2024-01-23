using Newtonsoft.Json;

namespace Library_Management.DTO
{
    public class StudentModel
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }
        [JsonProperty(PropertyName = "prnNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int PrnNumber { get; set; }
        [JsonProperty(PropertyName = "studentName", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentName { get; set; }
        [JsonProperty(PropertyName = "branchName", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentEmail { get; set; }
        [JsonProperty(PropertyName = "studentPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentPassword { get; set; }
    }
}
