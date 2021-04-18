namespace Rodrigo.Ofx.Models
{
    public class BankMessages
    {
        public Stmttrns Stmttrns { get; set; }

        public BankMessages()
        {
            Stmttrns = new Stmttrns();
        }
    }
}
