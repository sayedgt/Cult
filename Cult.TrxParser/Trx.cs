using System.IO;
using System.Linq;
using System.Text;
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

        public static string ToMarkdown(TestRun testRun)
        {
            var sb = new StringBuilder();
            var groups = testRun.Results.UnitTestResults
                .GroupBy(x => x.TestId)
                .ToList();
            foreach (var group in groups)
            {
                var testName = @group.FirstOrDefault()?.TestName;
                var name = testName?.Substring(0, testName.IndexOf('('));
                sb.AppendLine($"## {name}");
                var i = 0;
                foreach (var g in @group.OrderBy(x => x.StartTime))
                {
                    if (testName == null) continue;
                    var text = g.TestName.Substring(testName.IndexOf(')') + 1).Trim();
                    sb.AppendLine($"{++i}. {text}");
                }
                sb.AppendLine();
            }

            return sb.ToString();
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
