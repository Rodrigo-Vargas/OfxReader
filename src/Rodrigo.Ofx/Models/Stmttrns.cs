namespace Rodrigo.Ofx.Models
{
    public class Stmttrns
    {
        public string Trnuid { get; set; }
        public Status Status { get; set; }
        public string CURDEF { get; set; }
        public BankAccount BANKACCTFROM { get; set; }

        public Stmttrns()
        {
            Status = new Status();
            BANKACCTFROM = new BankAccount();
        }
    }
}
