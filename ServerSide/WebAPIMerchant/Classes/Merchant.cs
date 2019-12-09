using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMerchant
{
    public class Merchant
    {
        public String Name { get; set; }
        public String Address { get; set; }
        public String BAccountType { get; set; }
        public String MerchantType { get; set; }
        public Int64 AccountNum { get; set; }
        public Double InitBalance { get; set; }
        public int Merchantid { get; set; }
        public int WebAPIKey { get; set; }
        public Merchant()
        {
            this.Merchantid = Convert.ToInt32(MerchantIDgen());
            this.WebAPIKey = KeyGenerator();
        }
        public Merchant(string newname, string address, string type, int num, double balance, int merchant, int web)
        {
            this.Name = newname;
            this.Address = address;
            this.MerchantType = type;
            //this.baccounttype = type;
            this.AccountNum = num;
            this.InitBalance = balance;
            this.Merchantid = merchant;
            this.WebAPIKey = web;
        }
        
        public String MerchantIDgen()
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