using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace concourse_rss_resource.ConcourseModel
{
    public class FetchResourceOutputModel
    {
        [JsonPropertyName("version")]
        public RssVersion Version { get; set; }
        
        [JsonPropertyName("metadata")]
        public List<OutputMetadata> Metadata { get; set; }
    }

    public class OutputMetadata
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}