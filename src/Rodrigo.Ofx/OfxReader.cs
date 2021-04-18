using System;
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
    }
}
