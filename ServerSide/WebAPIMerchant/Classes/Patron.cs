using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIMerchant.Classes
{
    public class Patron
    {
        public String Name { get; set; }
        public String Address { get; set; }
        public int AccountNum { get; set; }
        public String AccountType { get; set; }
        public Double Amount { get; set; }

        public Patron(string name, string address, int accountnum, string type, double amount)
        {
            this.Name = name;
            this.Address = address;
            this.AccountNum = accountnum;
            this.AccountType = type;
            this.Amount = amount;
        }
    }
}
