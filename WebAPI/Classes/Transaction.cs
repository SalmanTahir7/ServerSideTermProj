using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Transaction
    {
        public String TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }

        public Double TransactionAmount { get; set; }
    }
}
