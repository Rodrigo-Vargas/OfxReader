using Rodrigo.Ofx.Attributes;

namespace Rodrigo.Ofx.Models
{
    public class Status
    {
        [OfxProperty("CODE")]
        public string Code { get; set; }

        [OfxProperty("SEVERITY")]
        public string Severity { get; set; }
    }
}
