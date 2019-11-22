using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;

using System.Data.SqlClient;


//using Utilities;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/service/PaymentGateway")]
    public class PaymentGatewayController : Controller
    {
        /*
        // GET: api/PaymentGateway
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        /*
        // GET: api/PaymentGateway/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        */

        [HttpPost()]
        [HttpPost("CreateVirtualWallet")]
        public int CreateVirtualWallet(object AccountHolderInformation, object MerchantAccountID, object APIKey){

            Random random = new Random();
            string r = "";
            int i;
            int virtualid;
            for (i = 1; i < 7; i++)
            {
                r += random.Next(0, 5).ToString();
            }
            r += MerchantAccountID.ToString();
            virtualid =   Convert.ToInt32(r);
            
            return virtualid;
        }

        [HttpGet()]
        [HttpGet("GetTransactions")]
        
        public List<Transaction> GetTransactions(object VirtualWalletID, object MerchantAccountID, object APIKey)
        {
            List<Transaction> transactions = new List<Transaction>();
            //Retrieve Transaction Data From DB 

            return transactions;
        }

        [HttpPost()]
        [HttpPost("ProcessPayment")]

        public void ProcessPayment(object VirtualWalletID, object VirtualWalletID2, object amount, object type, object MerchantAccountID, object APIKey)
        {
            //Process a Payment BY Type, deduct from one merchant account and deposit it into another 
            //Record Transaction Record, updating date and time 
        }

        [HttpPut]
        [HttpPut("UpdatePaymentAccount")]

        public void UpdatePaymentAccount(object VirtualWalletID, object AccountHolderInformation, object MerchantAccountID, object APIKey)
        {
            //Ask about This method 
            //Update Customer or restaurant Information
        }

        [HttpPut]
        [HttpPut("FundAccount")]

        public void FundAccount(object VirtualWalletID, object amount, object MerchantAccountID, object APIKey)
        {
            //Add money to the customer account 
            //Record Transaction for Logging Purposes 
        }

        /*
        // POST: api/PaymentGateway
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/PaymentGateway/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
