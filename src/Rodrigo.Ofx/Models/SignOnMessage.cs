using Rodrigo.Ofx.Attributes;

namespace Rodrigo.Ofx.Models
{
    public class SignOnMessage
    {
        [OfxProperty("STATUS")]
        public Status Status { get; set; }

        [OfxProperty("DTSERVER")]
        public string DateServer { get; set; }

        [OfxProperty("LANGUAGE")]
        public string Language { get; set; }

        [OfxProperty("FI")]
        public Fi Fi { get; set; }

        public SignOnMessage()
        {
            Status = new Status();
            Fi = new Fi();
        }
    }
}
