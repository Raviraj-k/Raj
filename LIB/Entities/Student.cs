using Newtonsoft.Json;

namespace Library_Management.Entities
{
    public class Student
    {
        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean Active { get; set; }
        [JsonProperty(PropertyName = "archieved", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean Archieved { get; set; }
        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }
        [JsonProperty(PropertyName = "prnNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int PrnNumber { get; set; }
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string? UId { get; set; }
        [JsonProperty(PropertyName = "studentName", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentName { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "studentPassword", NullValueHandling = NullValueHandling.Ignore)]
        public string StudentPassword { get; set; }
        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }
        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }
        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }
        [JsonProperty(PropertyName = "dType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }
        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }
    }
}
