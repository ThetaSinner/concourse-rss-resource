using System.Text.Json.Serialization;

namespace concourse_rss_resource.ConcourseModel
{
    public class ResourceInputModel
    {
        [JsonPropertyName("source")]
        public RssSource Source { get; set; }
        
        [JsonPropertyName("version")]
        public RssVersion Version { get; set; }
    }

    public class RssSource
    {
        [JsonPropertyName("uri")]
        public string Uri { get; set; }
    }
    
    public class RssVersion
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}