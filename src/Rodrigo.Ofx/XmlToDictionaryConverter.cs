using System.IO;
using System.Xml;

namespace Rodrigo.Ofx
{
    public class XmlToDictionaryConverter
    {
        public static OfxDictionary<string, object> Convert(string content)
        {
            OfxDictionary<string, object> root = new OfxDictionary<string, object>();

            using (var stringReader = new StringReader(content))
            {
                XmlReader reader = XmlReader.Create(stringReader);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                        root.Add(reader.Name, BuildChildren(reader));
                }
            }

            return root;
        }

        private static object BuildChildren(XmlReader reader)
        {
            var children = new OfxDictionary<string, object>();

            while(reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement)
                    return children;

                if (reader.NodeType == XmlNodeType.Text)
                {
                    string value = reader.Value;
                    reader.Read();
                    return value;
                }

                if (reader.NodeType == XmlNodeType.Element)
                    children.Add(reader.Name, BuildChildren(reader));
            }

            return children;
        }
    }
}
