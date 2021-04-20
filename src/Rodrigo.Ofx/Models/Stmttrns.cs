namespace Rodrigo.Ofx.Models
{
    public class Stmttrns
    {
        public string Trnuid { get; set; }
        public Status Status { get; set; }
        public string Currency { get; set; }
        public BankAccount AccountFrom { get; set; }

        public Stmttrns()
        {
            Status = new Status();
            AccountFrom = new BankAccount();
        }
    }
}
