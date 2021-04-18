using System.Collections.Generic;

namespace Rodrigo.Ofx.Models
{
    public class TransactionList
    {
        public string DTSTART { get; set; }
        public string DTEND { get; set; }
        public List<Stmttrn> Transactions { get; set; }

        public TransactionList()
        {
            Transactions = new List<Stmttrn>();
        }
    }
}
