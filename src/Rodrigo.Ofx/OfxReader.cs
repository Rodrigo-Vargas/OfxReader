using System;
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

            OfxDictionary<string, object> root = XmlToDictionaryConverter.Convert(ofxBodyContent);

            var messages = root["OFX"]["SIGNONMSGSRSV1"]["SONRS"];

            model.Ofx.SignOnMessage.Status.Code = messages["STATUS"]["CODE"];
            model.Ofx.SignOnMessage.Status.Severity = messages["STATUS"]["SEVERITY"];
            model.Ofx.SignOnMessage.DateServer = messages["DTSERVER"];
            model.Ofx.SignOnMessage.Language = messages["LANGUAGE"];
            model.Ofx.SignOnMessage.Fi.Organization = messages["FI"]["ORG"];
            model.Ofx.SignOnMessage.Fi.Fid = messages["FI"]["FID"];

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
