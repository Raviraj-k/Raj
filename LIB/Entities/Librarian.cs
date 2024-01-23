using Newtonsoft.Json;

namespace Library_Management.Entities
{
    public class Librarian
    {

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "dType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }


        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean Active { get; set; }

        [JsonProperty(PropertyName = "archieved", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean Archieved { get; set; }


        // Class  Feilds / Properties

        [JsonProperty(PropertyName = "Name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "mobileNo", NullValueHandling = NullValueHandling.Ignore)]
        public double MobileNo { get; set; }

        [JsonProperty(PropertyName = "emailId")]
        public string EmailId { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
