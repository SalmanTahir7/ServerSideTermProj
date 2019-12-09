﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



using System.Data;              // import needed for DataSet and other data classes

using System.Data.SqlClient;    // import needed for ADO.NET classes

using Utilities;                // import needed for DBConnect class





namespace WebAPIMerchant.Controllers
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
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        private bool isMerchant(int merchid, int apikey)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_FindMerchant";
            SqlParameter merchant = new SqlParameter("@merchantid", merchid);
            merchant.Direction = ParameterDirection.Input;
            merchant.SqlDbType = SqlDbType.Int;
            merchant.Size = 4;
            objCommand.Parameters.Add(merchant);
            SqlParameter api = new SqlParameter("@webapi", apikey);
            api.Direction = ParameterDirection.Input;
            api.SqlDbType = SqlDbType.Int;
            api.Size = 4;
            objCommand.Parameters.Add(api);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            objCommand.Parameters.Clear();
            

            //DataSet ds = objDB.GetDataSet("SELECT * FROM Merchant WHERE MerchantID = " + merchid + " AND WebAPIKey = " + apikey);
            if (ds.Tables[0].Rows.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost("CreateVirtualWallet/{MerchantAccountID}/{APIKey}")]
        public Boolean CreateVirtualWallet([FromBody] Classes.Patron AccountHolderInformation, int MerchantAccountID, int APIKey)
        {

            if(isMerchant(MerchantAccountID, APIKey) == true)
            {
                try
                {
                    //Creates a Virtal Wallet ID for the Merchant to use
                    //VirtualID of 6 Values between 0-9
                    //DataSet ds = objDB.GetDataSet("SELECT * FROM TP_USERS WHERE LoginID = " + AccountHolderInformation);
                    /*objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_GetUser";
                    SqlParameter login = new SqlParameter("@loginid", AccountHolderInformation);
                    login.Direction = ParameterDirection.Input;
                    login.SqlDbType = SqlDbType.Int;
                    login.Size = 4;
                    objCommand.Parameters.Add(login);
                    DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
                    objCommand.Parameters.Clear(); 


                    DataRow record;
                    record = ds.Tables[0].Rows[0];*/
                    Merchant merchant = new Merchant(AccountHolderInformation.Name, AccountHolderInformation.Address, AccountHolderInformation.AccountType, AccountHolderInformation.AccountNum, 0.0, MerchantAccountID, APIKey);
                    

                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_AddWallet";


                    Random random = new Random();
                    string r = "";
                    int i;
                    int virtualid;
                    for (i = 1; i < 2; i++)
                    {
                        r += random.Next(0, 9).ToString();
                    }
                    r += MerchantAccountID.ToString();
                    virtualid = Convert.ToInt32(r);

                    SqlParameter vid = new SqlParameter("@virtualid", virtualid);
                    vid.Direction = ParameterDirection.Input;
                    vid.SqlDbType = SqlDbType.Int;
                    vid.Size = 4;
                    objCommand.Parameters.Add(vid);
                    SqlParameter inputParameter = new SqlParameter("@amount", merchant.InitBalance);

                    inputParameter.Direction = ParameterDirection.Input;

                    inputParameter.SqlDbType = SqlDbType.Float;

                    inputParameter.Size = 4;
                    objCommand.Parameters.Add(inputParameter);
                    SqlParameter type = new SqlParameter("@atype", merchant.MerchantType);
                    type.Direction = ParameterDirection.Input;
                    type.SqlDbType = SqlDbType.VarChar;
                    type.Size = 50;
                    objCommand.Parameters.Add(type);
                    SqlParameter address = new SqlParameter("@address", merchant.Address);
                    address.Direction = ParameterDirection.Input;
                    address.SqlDbType = SqlDbType.VarChar;
                    address.Size = 50;
                    objCommand.Parameters.Add(address);
                    SqlParameter name = new SqlParameter("@name", merchant.Name);
                    name.Direction = ParameterDirection.Input;
                    name.SqlDbType = SqlDbType.VarChar;
                    name.Size = 50;
                    objCommand.Parameters.Add(name);
                    SqlParameter accountnum = new SqlParameter("@anum", merchant.AccountNum);
                    accountnum.Direction = ParameterDirection.Input;
                    accountnum.SqlDbType = SqlDbType.Int;
                    accountnum.Size = 4;
                    objCommand.Parameters.Add(accountnum);
                    objDB.DoUpdateUsingCmdObj(objCommand);
                    objCommand.Parameters.Clear();
                    return true;
                }
                catch(Exception ex)
                {
                    string message = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }

            //return virtualid;
        }

       
        [HttpGet("GetTransactions/{VirtualWalletID}/{MerchantAccountID}/{APIKey}")]

        public List<Transaction> GetTransactions(int VirtualWalletID, int MerchantAccountID, int APIKey)
        {
            //Retrieves the Transactions from the Database that match virtual wallet id and merchant account id 
             List<Transaction> transactions = new List<Transaction>();
             

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_GetTransactionRecord";

            SqlParameter input = new SqlParameter("@virtualid", (int)VirtualWalletID);
            input.Direction = ParameterDirection.Input;
            input.SqlDbType = SqlDbType.Int;
            input.Size = 5;
            objCommand.Parameters.Add(input);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            objCommand.Parameters.Clear();
            DataTable dataTable = new DataTable();
            
            if(ds.Tables[0].Rows.Count != 0)
            {
                for(int i=0;i< ds.Tables[0].Rows.Count; i++)
                {
                    Transaction transaction = new Transaction((string)ds.Tables[0].Rows[i][1], (string)ds.Tables[0].Rows[i][3], (double)ds.Tables[0].Rows[i][2]);
                    transactions.Add(transaction);
                }
                
            }

            //Retrieve Transaction Data From DB 


            return transactions;
        }

       
        [HttpPost("ProcessPayment/{VirtualWalletID}/{VirtualWalletID2}/{amount}/{type}/{MerchantAccountID}/{APIKey}")]

        public bool ProcessPayment(int VirtualWalletID, int VirtualWalletID2, double amount, string type, int MerchantAccountID, int APIKey)
        {
            try
            {
                if (isMerchant(MerchantAccountID, APIKey))
                {
                    //Process a Payment BY Type, deduct from one merchant account and deposit it into another 
                    if (type.Equals("payment"))
                    {
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "TP_WalletTransactionR";
                        SqlParameter inputParameter = new SqlParameter("@amount", (float)amount);
                        inputParameter.Direction = ParameterDirection.Input;
                        inputParameter.SqlDbType = SqlDbType.Float;
                        inputParameter.Size = 4;
                        objCommand.Parameters.Add(inputParameter);
                        SqlParameter vid5 = new SqlParameter("@virtualid", VirtualWalletID);
                        vid5.Direction = ParameterDirection.Input;
                        vid5.SqlDbType = SqlDbType.Int;
                        vid5.Size = 4;
                        objCommand.Parameters.Add(vid5);
                        objDB.DoUpdateUsingCmdObj(objCommand);
                        objCommand.Parameters.Clear();

                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "TP_WalletTransactionP";
                        SqlParameter inputParameter2 = new SqlParameter("@amount", (float)amount);
                        inputParameter2.Direction = ParameterDirection.Input;
                        inputParameter2.SqlDbType = SqlDbType.Float;
                        inputParameter2.Size = 4;
                        objCommand.Parameters.Add(inputParameter2);
                        SqlParameter vid2 = new SqlParameter("@virtualid", VirtualWalletID2);
                        vid2.Direction = ParameterDirection.Input;
                        vid2.SqlDbType = SqlDbType.Int;
                        vid2.Size = 4;
                        objCommand.Parameters.Add(vid2);
                        objDB.DoUpdateUsingCmdObj(objCommand);
                        objCommand.Parameters.Clear();

                        //Transaction Payment for VID1
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "TP_AddTransaction";
                        SqlParameter input = new SqlParameter("@type", "Received Payment");
                        input.Direction = ParameterDirection.Input;

                        input.SqlDbType = SqlDbType.VarChar;

                        input.Size = 50;
                        objCommand.Parameters.Add(input);
                        SqlParameter tamount = new SqlParameter("@amount", (double)amount);

                        tamount.Direction = ParameterDirection.Input;

                        tamount.SqlDbType = SqlDbType.Float;

                        tamount.Size = 4;
                        objCommand.Parameters.Add(tamount);
                        SqlParameter date = new SqlParameter("@datetime", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                        date.Direction = ParameterDirection.Input;
                        date.SqlDbType = SqlDbType.VarChar;
                        date.Size = 50;
                        objCommand.Parameters.Add(date);
                        SqlParameter vid = new SqlParameter("@virtualid", (int)VirtualWalletID);
                        vid.Direction = ParameterDirection.Input;
                        vid.SqlDbType = SqlDbType.Int;
                        vid.Size = 4;
                        objCommand.Parameters.Add(vid);
                        objDB.DoUpdateUsingCmdObj(objCommand);
                        objCommand.Parameters.Clear();

                        //Received Payment for the other 
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "TP_AddTransaction";
                        SqlParameter input2 = new SqlParameter("@type", "Paid Payment");
                        input2.Direction = ParameterDirection.Input;

                        input2.SqlDbType = SqlDbType.VarChar;

                        input2.Size = 50;
                        objCommand.Parameters.Add(input2);
                        objCommand.Parameters.Add(tamount);
                        objCommand.Parameters.Add(date);
                        SqlParameter virtual2 = new SqlParameter("@virtualid", (int)VirtualWalletID2);
                        virtual2.Direction = ParameterDirection.Input;
                        virtual2.SqlDbType = SqlDbType.Int;
                        virtual2.Size = 4;
                        objCommand.Parameters.Add(virtual2);
                        objDB.DoUpdateUsingCmdObj(objCommand);
                        objCommand.Parameters.Clear();

                    }
                    else if (type.Equals("refund")) //Refund from Virtual Wallet 2 and add to Wallet 1 
                    {
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "TP_WalletTransactionR";
                        SqlParameter inputParameter = new SqlParameter("@amount", (float)amount);
                        inputParameter.Direction = ParameterDirection.Input;
                        inputParameter.SqlDbType = SqlDbType.Float;
                        inputParameter.Size = 4;
                        objCommand.Parameters.Add(inputParameter);
                        SqlParameter vid3 = new SqlParameter("@virtualid", VirtualWalletID2);
                        vid3.Direction = ParameterDirection.Input;
                        vid3.SqlDbType = SqlDbType.Int;
                        vid3.Size = 4;
                        objCommand.Parameters.Add(vid3);
                        objDB.DoUpdateUsingCmdObj(objCommand);
                        objCommand.Parameters.Clear();

                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "TP_WalletTransactionP";
                        SqlParameter inputParameter2 = new SqlParameter("@amount", (float)amount);
                        inputParameter2.Direction = ParameterDirection.Input;
                        inputParameter2.SqlDbType = SqlDbType.Float;
                        inputParameter2.Size = 4;
                        objCommand.Parameters.Add(inputParameter2);
                        SqlParameter vid2 = new SqlParameter("@virtualid", VirtualWalletID);
                        vid2.Direction = ParameterDirection.Input;
                        vid2.SqlDbType = SqlDbType.Int;
                        vid2.Size = 4;
                        objCommand.Parameters.Add(vid2);
                        objDB.DoUpdateUsingCmdObj(objCommand);
                        objCommand.Parameters.Clear();

                        //Transaction Payment for VID1
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "TP_AddTransaction";
                        SqlParameter input = new SqlParameter("@type", "Received Payment");
                        input.Direction = ParameterDirection.Input;

                        input.SqlDbType = SqlDbType.VarChar;

                        input.Size = 50;
                        objCommand.Parameters.Add(input);
                        SqlParameter tamount = new SqlParameter("@amount", (double)amount);

                        tamount.Direction = ParameterDirection.Input;

                        tamount.SqlDbType = SqlDbType.Float;

                        tamount.Size = 4;
                        objCommand.Parameters.Add(tamount);
                        SqlParameter date = new SqlParameter("@datetime", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                        date.Direction = ParameterDirection.Input;
                        date.SqlDbType = SqlDbType.VarChar;
                        date.Size = 50;
                        objCommand.Parameters.Add(date);
                        SqlParameter vid = new SqlParameter("@virtualid", (int)VirtualWalletID2);
                        vid.Direction = ParameterDirection.Input;
                        vid.SqlDbType = SqlDbType.Int;
                        vid.Size = 4;
                        objCommand.Parameters.Add(vid);
                        objDB.DoUpdateUsingCmdObj(objCommand);
                        objCommand.Parameters.Clear();

                        //Received Payment for the other 
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandText = "TP_AddTransaction";
                        SqlParameter input2 = new SqlParameter("@type", "Paid Payment");
                        input2.Direction = ParameterDirection.Input;

                        input2.SqlDbType = SqlDbType.VarChar;

                        input2.Size = 50;
                        objCommand.Parameters.Add(input2);
                        objCommand.Parameters.Add(tamount);
                        objCommand.Parameters.Add(date);
                        SqlParameter virtual2 = new SqlParameter("@virtualid", (int)VirtualWalletID);
                        virtual2.Direction = ParameterDirection.Input;
                        virtual2.SqlDbType = SqlDbType.Int;
                        virtual2.Size = 4;
                        objCommand.Parameters.Add(virtual2);
                        objDB.DoUpdateUsingCmdObj(objCommand);
                        objCommand.Parameters.Clear();
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }catch(Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            //Record Transaction Record, updating date and time 
            
        }

        
        [HttpPut("UpdatePaymentAccount/{VirtualWalletID}/{MerchantAccountID}/{APIKey}")]

        public bool UpdatePaymentAccount(int VirtualWalletID, [FromBody] Classes.Patron AccountHolderInformation, int MerchantAccountID, int APIKey)
        {
            try
            {
                if (isMerchant(MerchantAccountID, APIKey))
                {
                    //Ask about This method 
                    //Update Customer or restaurant Information
                    /*DataSet ds = objDB.GetDataSet("SELECT * FROM USERS WHERE LoginID = " + AccountHolderInformation);
                    DataRow record;
                    record = ds.Tables[0].Rows[0];
                    Merchant merchant = new Merchant();
                    merchant.Name = (string)record["Name"];
                    merchant.MerchantType = (string)record["Type"];
                    merchant.Address = (string)record["Address"];
                    merchant.AccountNum = (int)record["AccountNum"];
                    merchant.InitBalance = (float)record["InitBalance"];*/
                    /*objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "GetUser";
                    SqlParameter login = new SqlParameter("@loginid", AccountHolderInformation);
                    login.Direction = ParameterDirection.Input;
                    login.SqlDbType = SqlDbType.Int;
                    login.Size = 4;
                    objCommand.Parameters.Add(login);
                    DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
                    objCommand.Parameters.Clear();


                    DataRow record;
                    record = ds.Tables[0].Rows[0];*/
                    Merchant merchant = new Merchant(AccountHolderInformation.Name, AccountHolderInformation.Address, AccountHolderInformation.AccountType, AccountHolderInformation.AccountNum , AccountHolderInformation.Amount , MerchantAccountID, APIKey);


                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_UpdateWallet";
                    SqlParameter id = new SqlParameter("@virtualid", VirtualWalletID);
                    id.Direction = ParameterDirection.Input;
                    id.SqlDbType = SqlDbType.Int;
                    id.Size = 4;
                    objCommand.Parameters.Add(id);
                    SqlParameter inputParameter = new SqlParameter("@amount", merchant.InitBalance);
                    inputParameter.Direction = ParameterDirection.Input;
                    inputParameter.SqlDbType = SqlDbType.Float;
                    inputParameter.Size = 4;
                    objCommand.Parameters.Add(inputParameter);
                    SqlParameter type = new SqlParameter("@atype", merchant.MerchantType);
                    type.Direction = ParameterDirection.Input;
                    type.SqlDbType = SqlDbType.VarChar;
                    type.Size = 50;
                    objCommand.Parameters.Add(type);
                    SqlParameter address = new SqlParameter("@address", merchant.Address);
                    address.Direction = ParameterDirection.Input;
                    address.SqlDbType = SqlDbType.VarChar;
                    address.Size = 50;
                    objCommand.Parameters.Add(address);
                    SqlParameter name = new SqlParameter("@name", merchant.Name);
                    name.Direction = ParameterDirection.Input;
                    name.SqlDbType = SqlDbType.VarChar;
                    name.Size = 50;
                    objCommand.Parameters.Add(name);
                    SqlParameter accountnum = new SqlParameter("@anum", merchant.AccountNum);
                    accountnum.Direction = ParameterDirection.Input;
                    accountnum.SqlDbType = SqlDbType.Int;
                    accountnum.Size = 4;
                    objCommand.Parameters.Add(accountnum);
                    objDB.DoUpdateUsingCmdObj(objCommand);

                    objCommand.Parameters.Clear();
                    return true;
                }
                else
                {
                    return false;
                }

            }catch(Exception ex)
            {
                string error = ex.Message;
                return false;
            }


        }

        
        [HttpPut("FundAccount/{VirtualWalletID}/{amount}/{MerchantAccountID}/{APIKey}")]

        public bool FundAccount(int VirtualWalletID, double amount, int MerchantAccountID, int APIKey)
        {
            try
            {
                if(isMerchant(MerchantAccountID, APIKey))
                {
                    //Add money to the customer account 

                    //Record Transaction for Logging Purposes 
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_AddFunds";
                    SqlParameter inputParameter = new SqlParameter("@amount", (double)amount);

                    inputParameter.Direction = ParameterDirection.Input;

                    inputParameter.SqlDbType = SqlDbType.Float;

                    //inputParameter.Size = 4;
                    objCommand.Parameters.Add(inputParameter);
                    SqlParameter id = new SqlParameter("@virtualid", (int)VirtualWalletID);
                    id.Direction = ParameterDirection.Input;
                    id.SqlDbType = SqlDbType.Int;
                    id.Size = 4;
                    objCommand.Parameters.Add(id);

                    objDB.DoUpdateUsingCmdObj(objCommand);
                    objCommand.Parameters.Clear();

                    
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.CommandText = "TP_AddTransaction";
                    SqlParameter input = new SqlParameter("@type", "Funding");
                    input.Direction = ParameterDirection.Input;

                    input.SqlDbType = SqlDbType.VarChar;

                    input.Size = 50;
                    objCommand.Parameters.Add(input);
                    SqlParameter tamount = new SqlParameter("@amount", (double)amount);

                    tamount.Direction = ParameterDirection.Input;

                    tamount.SqlDbType = SqlDbType.Float;

                    tamount.Size = 4;
                    objCommand.Parameters.Add(tamount);
                    SqlParameter date = new SqlParameter("@datetime", DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                    date.Direction = ParameterDirection.Input;
                    date.SqlDbType = SqlDbType.VarChar;
                    date.Size = 50;
                    objCommand.Parameters.Add(date);
                    SqlParameter vid = new SqlParameter("@virtualid", (int)VirtualWalletID);
                    vid.Direction = ParameterDirection.Input;
                    vid.SqlDbType = SqlDbType.Int;
                    vid.Size = 4;
                    objCommand.Parameters.Add(vid);
                    objDB.DoUpdateUsingCmdObj(objCommand);
                    objCommand.Parameters.Clear();
                    return true;
                }
                else
                {
                    return false;
                }

            }catch(Exception ex)
            {
                string error = ex.Message;
                return false ;
            }







        }

        
        // POST: api/PaymentGateway/Post
        [HttpPost("PostString/{value}")]
        public string Poststring(string value)
        {
            return value;
        }
        // POST: api/PaymentGateway/Post
        [HttpPost("PostString2")]
        public string Poststring2(string value)
        {
            return value;
        }

        // PUT: api/PaymentGateway/5
        [HttpPut("Putstuff/{id}")]
        public int Put(int id)
        {
            return 1;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("Add/{x}/{y}")]  // GET api/Calculator/Add/2/5

        public double Add(double x, double y)

        {

            return x + y;

        }
        [HttpGet("{id}")] 
        public string  Get(int id)
        {
            return "value" + id;
        }
    }
}