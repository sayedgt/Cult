using System.Collections.Generic;
using System.Xml.Serialization;

namespace Cult.TrxParser.Models
{
    public class TestDefinitions
    {
        [XmlElement("UnitTest")]
        public List<UnitTest> UnitTests { get; set; }
    }
}
