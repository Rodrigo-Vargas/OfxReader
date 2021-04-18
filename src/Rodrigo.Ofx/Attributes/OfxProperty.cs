using System;

namespace Rodrigo.Ofx.Attributes
{
    public class OfxProperty : Attribute
    {
        public string FieldName { get; private set; }

        public OfxProperty(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
