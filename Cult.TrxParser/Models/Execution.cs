using System.Xml.Serialization;

namespace Cult.TrxParser.Models
{
    public class Execution
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }
}
