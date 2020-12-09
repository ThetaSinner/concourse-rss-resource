using System.Xml.Serialization;

namespace concourse_rss_resource.RssModel20
{
    [XmlRoot(ElementName = "item")]
    public class RssItem20
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
    }
}
