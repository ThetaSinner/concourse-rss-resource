using System.Collections.Generic;
using System.Xml.Serialization;

namespace concourse_rss_resource.RssModel20
{
    public class RssChannel20
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
        
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        
        [XmlElement(ElementName = "language")]
        public string Language { get; set; }
        
        [XmlElement(ElementName = "item")]
        public List<RssItem20> Items { get; set; }
    }
}
