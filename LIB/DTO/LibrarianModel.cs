using Newtonsoft.Json;

namespace Library_Management.DTO
{
    public class LibrarianModel
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }
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
