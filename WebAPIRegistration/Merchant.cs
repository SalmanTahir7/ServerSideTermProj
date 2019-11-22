using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIRegistration
{
    public class Merchant
    {
        private string name;
        private string merchantid;
        private int webapikey;
        public Merchant()
        {
            merchantid = MerchantID();
            webapikey = KeyGenerator();
        }
        public Merchant(string newname)
        {
            name = newname;
            merchantid = MerchantID();
            webapikey = KeyGenerator();
        }
        public String Name { get; set; }
        public String MerchantID()
        {
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i < 5; i++)
            {
                r += random.Next(0, 4).ToString();
            }
            return r;
        }
        public int KeyGenerator()
        {
            Random random = new Random();
            string r = "";
            int i;
            int webkey;
            for (i = 1; i < 6; i++)
            {
                r += random.Next(0, 5).ToString();
            }
            webkey = Convert.ToInt32(r);
            return webkey;
        }
    }
}