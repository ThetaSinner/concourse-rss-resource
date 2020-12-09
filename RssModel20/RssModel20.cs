using System.Xml.Serialization;

namespace concourse_rss_resource.RssModel20
{
    [XmlRoot(ElementName = "rss")]
    public class RssModel20
    {
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
        
        [XmlElement(ElementName = "channel")]
        public RssChannel20 Channel { get; set; }
    }
}
