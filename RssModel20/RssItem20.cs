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
        
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        
        [XmlElement(ElementName = "author")]
        public string Author { get; set; }
        
        [XmlElement(ElementName = "category")]
        public Category Category { get; set; }
        
        [XmlElement(ElementName = "comments")]
        public string Comments { get; set; }
        
        [XmlElement(ElementName = "enclosure")]
        public Enclosure Enclosure { get; set; }
        
        [XmlElement(ElementName = "guid")]
        public Guid Guid { get; set; }
        
        [XmlElement(ElementName = "pubDate")]
        public string PubDate { get; set; }
        
        [XmlElement(ElementName = "source")]
        public Source Source { get; set; }
    }

    public class Source
    {
        [XmlText]
        public string Value { get; set; }
        
        [XmlAttribute("url")]
        public string Url { get; set; }
    }

    public class Enclosure
    {
        [XmlAttribute("url")]
        public string Url { get; set; }
        
        [XmlAttribute("length")]
        public string Length { get; set; }
        
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
    
    public class Category
    {
        [XmlText]
        public string Value { get; set; }
        
        [XmlAttribute("domain")]
        public string Domain { get; set; }
    }
    
    public class Guid
    {
        [XmlText]
        public string Value { get; set; }
        
        [XmlAttribute("isPermaLink")]
        public string IsPermaLink { get; set; }
    }
}
