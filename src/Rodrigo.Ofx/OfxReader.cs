using System.IO;
using System.Xml;
using System.Xml.Linq;
using Rodrigo.Ofx.Attributes;
using Rodrigo.Ofx.Models;

namespace Rodrigo.Ofx
{
    public class OfxReader
    {
        public OfxModel Load(string fileContent)
        {
            var model = new OfxModel();

            var lines = fileContent.Split("\r\n");

            fileContent = string.Join("", lines);

            string ofxBodyContent = FileParser.GetOfxBody(fileContent);


            using (var stringReader = new StringReader(ofxBodyContent))
            {
                XmlReader reader = XmlReader.Create(stringReader);

                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "CODE")
                            model.Ofx.SignOnMessage.Status.Code = GetXmlValue(reader);

                        if (reader.Name == "SEVERITY")
                            model.Ofx.SignOnMessage.Severity = GetXmlValue(reader);

                        if (reader.Name == "DTSERVER")
                            model.Ofx.SignOnMessage.DateServer = GetXmlValue(reader);

                        if (reader.Name == "LANGUAGE")
                            model.Ofx.SignOnMessage.Language = GetXmlValue(reader);

                        if (reader.Name == "ORG")
                            model.Ofx.SignOnMessage.Fi.Organization = GetXmlValue(reader);

                        if (reader.Name == "FID")
                            model.Ofx.SignOnMessage.Fi.Fid = GetXmlValue(reader);
                    }
                }
            }

            foreach (var line in lines)
            {
                string value = "";
                string propName = "";

                foreach (var property in model.GetType().GetProperties())
                {
                    object[] attrs = property.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        OfxProperty authAttr = attr as OfxProperty;
                        if (authAttr != null)
                        {
                            propName = property.Name;
                            value = authAttr.FieldName;
                        }
                    }

                    var keyValue = line.Split(":");

                    if (keyValue.Length > 1 && value == keyValue[0])
                    {
                        model[propName] = keyValue[1];
                    }
                }                
            }

            return model;
        }

        private string GetXmlValue (XmlReader reader)
        {
            XElement element = XNode.ReadFrom(reader) as XElement;
            if (element == null)
                return "";

            return element.Value;
        }
    }
}
