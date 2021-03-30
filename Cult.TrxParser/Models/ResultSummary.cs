using System.Xml.Serialization;

namespace Cult.TrxParser.Models
{
    public class ResultSummary
    {
        [XmlAttribute("outcome")]
        public string Outcome { get; set; }

        [XmlElement("Counters")]
        public Counters Counters { get; set; }
    }
}
