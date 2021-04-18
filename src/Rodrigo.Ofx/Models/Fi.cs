using Rodrigo.Ofx.Attributes;

namespace Rodrigo.Ofx.Models
{
    public class Fi
    {
        [OfxProperty("ORG")]
        public string Organization { get; set; }

        [OfxProperty("FID")]
        public string Fid { get; set; }
    }
}
