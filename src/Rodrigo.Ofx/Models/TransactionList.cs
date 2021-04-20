using System.Collections.Generic;

namespace Rodrigo.Ofx.Models
{
    public class TransactionList
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<Stmttrn> Transactions { get; set; }

        public TransactionList()
        {
            Transactions = new List<Stmttrn>();
        }
    }
}
