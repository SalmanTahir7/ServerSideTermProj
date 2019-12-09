using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentProcessor
{
    public class Merchant
    {
        private string name;
        private string address;
        private string baccounttype;
        private string mtype;
        private int accountnum;
        private double initbalance;
        private string merchantid;
        private int webapikey;
        public Merchant()
        {
            this.merchantid = MerchantID();
            this.webapikey = KeyGenerator();
        }
        public Merchant(string newname, string address, string type, int num, double balance, string merchant)
        {
            this.name = newname;
            this.address = address;
            this.mtype = merchant;
            this.baccounttype = type;
            this.accountnum = num;
            this.initbalance = balance;
            this.merchantid = MerchantID();
            this.webapikey = KeyGenerator();
        }
        public String Name { get; set; }
        public String Address { get; set; }
        public String BAccountType { get; set; }
        public String MerchantType { get; set; }
        public Int64 AccountNum { get; set; }
        public Double InitBalance { get; set; }
        public String MerchantID()
        {
            //ID is 8 digits from 0-4
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i < 9; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;
        }
        public int KeyGenerator()
        {
            //Key is 10 digits from 0-5
            Random random = new Random();
            string r = "";
            int i;
            int webkey;
            for (i = 1; i < 11; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            webkey = Convert.ToInt32(r);
            return webkey;
        }
    }
}