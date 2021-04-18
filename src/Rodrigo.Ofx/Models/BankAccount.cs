namespace Rodrigo.Ofx.Models
{
    public class BankAccount
    {
        public string Id { get; set; }
        public string BranchID { get; set; }
        public string AccountID { get; set; }
        public TransactionList BANKTRANLIST { get; set; }
        public string AccountType { get; set; }

        public BankAccount()
        {
            BANKTRANLIST = new TransactionList();
        }
    }
}
