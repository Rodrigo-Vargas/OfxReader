using System;
using System.Reflection;
using Rodrigo.Ofx.Attributes;

namespace Rodrigo.Ofx.Models
{
    public class OfxModel
    {
        public object this[string propertyName]
        {
            get
            {
                Type currentType = typeof(OfxModel);
                PropertyInfo propInfo = currentType.GetProperty(propertyName);
                return propInfo.GetValue(this, null);
            }
            set
            {
                Type currrentType = typeof(OfxModel);
                PropertyInfo propInfo = currrentType.GetProperty(propertyName);
                propInfo.SetValue(this, value, null);
            }
        }


        [OfxProperty("OFXHEADER")]
        public string Header { get; set; }

        [OfxProperty("DATA")]
        public string Data { get; set; }

        [OfxProperty("VERSION")]
        public string Version { get; set; }

        [OfxProperty("SECURITY")]
        public string Security { get; set; }

        [OfxProperty("ENCODING")]
        public string Encoding { get; set; }

        [OfxProperty("CHARSET")]
        public string Charset { get; set; }

        [OfxProperty("COMPRESSION")]
        public string Compression { get; set; }

        [OfxProperty("OLDFILEUID")]
        public string OldFileUID { get; set; }

        [OfxProperty("NEWFILEUID")]
        public string NewFileUID { get; set; }

        [OfxProperty("OFX")]
        public OfxBody Ofx { get; set; }

        public OfxModel()
        {
            Ofx = new OfxBody();
        }
    }
}
