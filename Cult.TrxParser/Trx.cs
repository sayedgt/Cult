using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using Cult.TrxParser.Models;
// ReSharper disable All

namespace Cult.TrxParser
{
    /// <summary>
    /// Deserializes trx files
    /// </summary>
    public static class Trx
    {
        /// <summary>
        /// Deserializes Trx 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>T</returns>
        public static TestRun Deserialize(string filePath)
        {
            RemoveXmlnsAndRewriteFile(filePath);
            TestRun entity;
            var xs = new XmlSerializer(typeof(TestRun));
            using (Stream sr = File.OpenRead(filePath))
            {
                entity = (TestRun)xs.Deserialize(sr);
            }
            return entity;
        }

        /// <summary>
        /// Deletes all xmlns namespaces in xml file and and overwrites file
        /// </summary>
        /// <param name="filePath">path to file with name included</param>
        private static void RemoveXmlnsAndRewriteFile(string filePath)
        {
            Regex rgx = new Regex("xmlns=\".*?\" ?");
            var fileContent = rgx.Replace(File.ReadAllText(filePath), string.Empty);
            XDocument xDoc = XDocument.Parse(fileContent);
            xDoc.Save(filePath);
        }
    }
}
