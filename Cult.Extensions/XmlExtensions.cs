using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
namespace Cult.Extensions
{
    public static class XmlExtensions
    {
        public static string FormatXmlText(this string xmlText)
        {
            var document = new XmlDocument();
            document.Load(new StringReader(xmlText));

            var builder = new StringBuilder();
            using (var writer = new XmlTextWriter(new StringWriter(builder)))
            {
                writer.Formatting = Formatting.Indented;
                document.Save(writer);
            }
            return xmlText;
        }
        private static XElement SortXmlElement(this XElement xe, bool sortAttributes = true, Action<XElement> postSort = null, params string[] customAttributes)
        {
            var nodesToBePreserved = xe.Nodes().Where(p => p.GetType() != typeof(XElement));
            if (sortAttributes)
            {
                xe.ReplaceAttributes(xe.Attributes().OrderBy(x => x.Name.LocalName));
            }
            if (customAttributes == null || customAttributes.Length == 0)
            {
                xe.ReplaceNodes(xe.Elements().OrderBy(x => x.Name.LocalName).Union(nodesToBePreserved.OrderBy(p => p.ToString())).OrderBy(n => n.NodeType.ToString()));
            }
            else
            {
                foreach (var att in customAttributes.Reverse())
                {
                    xe.ReplaceNodes(xe.Elements().OrderBy(x => x.Name.LocalName).ThenBy(x => (string)x.Attribute(att)).Union(nodesToBePreserved.OrderBy(p => p.ToString())).OrderBy(n => n.NodeType.ToString()));
                }
            }
            foreach (var xc in xe.Elements())
            {
                postSort?.Invoke(xc);
                SortXmlElement(xc, sortAttributes, postSort, customAttributes);
            }
            return xe;
        }
        public static string SortXmlText(this string xmlText, bool sortAttributes = true, Action<XElement> postSort = null, params string[] customAttributes)
        {
            var xmlTree = XElement.Parse(xmlText);
            var sortedXmlTree = SortXmlElement(xmlTree, sortAttributes, postSort, customAttributes);
            return sortedXmlTree.ToString();
        }
        public static Stream ToMemoryStream(this XmlDocument xmlDocument)
        {
            var xmlStream = new MemoryStream();
            xmlDocument.Save(xmlStream);
            xmlStream.Flush(); //Adjust this if you want read your data
            xmlStream.Position = 0;
            return xmlStream;
        }
        public static Stream ToMemoryStream(this XElement xElement)
        {
            return xElement.ToXmlDocument().ToMemoryStream();
        }
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }
        public static XmlDocument ToXmlDocument(this XElement xElement)
        {
            var sb = new StringBuilder();
            var xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = false };
            using (var xw = XmlWriter.Create(sb, xws))
            {
                xElement.WriteTo(xw);
            }
            var doc = new XmlDocument();
            doc.LoadXml(sb.ToString());
            return doc;
        }
    }
}
