using Newtonsoft.Json;

namespace Library_Management.Entities
{
    public class Book
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "bookId", NullValueHandling = NullValueHandling.Ignore)]
        public string BookId { get; set; }

        [JsonProperty(PropertyName = "createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "bookName", NullValueHandling = NullValueHandling.Ignore)]
        public string BookName { get; set; }

        [JsonProperty(PropertyName = "authorName", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthorName { get; set; }

        [JsonProperty(PropertyName = "bookType", NullValueHandling = NullValueHandling.Ignore)]
        public string BookType { get; set; }

        [JsonProperty(PropertyName = "dType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "archieved", NullValueHandling = NullValueHandling.Ignore)]
        public bool Archieved { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }
    }
}
