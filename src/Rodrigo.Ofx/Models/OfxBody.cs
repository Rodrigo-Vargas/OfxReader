using Rodrigo.Ofx.Attributes;

namespace Rodrigo.Ofx.Models
{
    public class OfxBody
    {
        [OfxProperty("SIGNONMSGSRSV1")]
        public SignOnMessage SignOnMessage { get; set; }
        public BankMessages BankMessages { get; set; }

        public OfxBody()
        {
            SignOnMessage = new SignOnMessage();
            BankMessages = new BankMessages();
        }
    }
}
