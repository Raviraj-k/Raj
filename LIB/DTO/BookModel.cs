using Newtonsoft.Json;

namespace Library_Management.DTO
{
    public class BookModel
    {
        [JsonProperty(PropertyName = "BookId", NullValueHandling = NullValueHandling.Ignore)]
        public string BookId { get; set; }

        [JsonProperty(PropertyName = "bookName", NullValueHandling = NullValueHandling.Ignore)]
        public string BookName { get; set; }

        [JsonProperty(PropertyName = "authorName", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthorName { get; set; }

        [JsonProperty(PropertyName = "bookType", NullValueHandling = NullValueHandling.Ignore)]
        public string BookType { get; set; }
    }
}
