
using Cult.Toolkit.ExtraObject;
using Cult.Toolkit.ExtraString;
using System;
using System.Xml;

namespace Cult.Toolkit.ExtraXmlNode
{
    public static class XmlNodeExtensions
    {
        public static XmlNode CreateChildNode(this XmlNode parentNode, string name)
        {
            var document = (parentNode is XmlDocument ? (XmlDocument)parentNode : parentNode.OwnerDocument);
            XmlNode node = document.CreateElement(name);
            parentNode.AppendChild(node);
            return node;
        }
        public static XmlNode CreateChildNode(this XmlNode parentNode, string name, string namespaceUri)
        {
            var document = (parentNode is XmlDocument ? (XmlDocument)parentNode : parentNode.OwnerDocument);
            XmlNode node = document.CreateElement(name, namespaceUri);
            parentNode.AppendChild(node);
            return node;
        }
        public static XmlCDataSection CreateCDataSection(this XmlNode parentNode)
        {
            return parentNode.CreateCDataSection(string.Empty);
        }
        public static XmlCDataSection CreateCDataSection(this XmlNode parentNode, string data)
        {
            var document = (parentNode is XmlDocument ? (XmlDocument)parentNode : parentNode.OwnerDocument);
            var node = document.CreateCDataSection(data);
            parentNode.AppendChild(node);
            return node;
        }
        public static string GetCDataSection(this XmlNode parentNode)
        {
            foreach (var node in parentNode.ChildNodes)
            {
                if (node is XmlCDataSection)
                    return ((XmlCDataSection)node).Value;
            }
            return null;
        }
        public static string GetAttribute(this XmlNode node, string attributeName)
        {
            return GetAttribute(node, attributeName, null);
        }
        public static string GetAttribute(this XmlNode node, string attributeName, string defaultValue)
        {
            var attribute = node.Attributes[attributeName];
            return (attribute != null ? attribute.InnerText : defaultValue);
        }
        public static T GetAttribute<T>(this XmlNode node, string attributeName)
        {
            return GetAttribute(node, attributeName, default(T));
        }
        public static T GetAttribute<T>(this XmlNode node, string attributeName, T defaultValue)
        {
            var value = GetAttribute(node, attributeName);
            if (value.IsNotEmpty())
            {
                if (typeof(T) == typeof(Type))
                    return (T)(object)Type.GetType(value, true);
                return value.ConvertTo(defaultValue);
            }
            return defaultValue;
        }
        public static void SetAttribute(this XmlNode node, string name, object value)
        {
            SetAttribute(node, name, value != null ? value.ToString() : null);
        }
        public static void SetAttribute(this XmlNode node, string name, string value)
        {
            if (node != null)
            {
                var attribute = node.Attributes[name, node.NamespaceURI];
                if (attribute == null)
                {
                    attribute = node.OwnerDocument.CreateAttribute(name, node.OwnerDocument.NamespaceURI);
                    node.Attributes.Append(attribute);
                }
                attribute.InnerText = value;
            }
        }
    }
}