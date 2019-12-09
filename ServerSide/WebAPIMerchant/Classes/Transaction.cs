using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIMerchant
{
    public class Transaction
    {
        public String TransactionType { get; set; }
        public String TransactionDate { get; set; }

        public Double TransactionAmount { get; set; }

        public Transaction(string type, string date, double amount)
        {
            this.TransactionAmount = amount;
            this.TransactionDate = date;
            this.TransactionType = type;
        }
    }
}