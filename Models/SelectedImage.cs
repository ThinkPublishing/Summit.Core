using Newtonsoft.Json;

namespace Summit.Core.Models
{
    public class SelectedImage
    {
        [JsonProperty(PropertyName = "file")]
        public string FilePath { get; set; }
        [JsonProperty(PropertyName = "descr")]
        public string Description { get; set; }
    }
}