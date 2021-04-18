using Rodrigo.Ofx.Attributes;

namespace Rodrigo.Ofx.Models
{
    public class Status
    {
        [OfxProperty("CODE")]
        public string Code { get; set; }
    }
}
