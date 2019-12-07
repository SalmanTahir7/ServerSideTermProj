using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using System.Web.Script.Serialization;  // needed for JSON serializers

using System.IO;                        // needed for Stream and Stream Reader

using System.Net;                       // needed for the Web Request

using Utilities;
using System.Data;              // import needed for DataSet and other data classes

using System.Data.SqlClient;
namespace PaymentProcessor
{
    public partial class Registration : System.Web.UI.Page
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        string url = "http://cis-iis2.temple.edu/Fall2019/3342_tug17598/TermProjectWS/api/service/PaymentGateway";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            int merchantID = GenerateKey(7);
            int webKey = GenerateKey(9);
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddMerchant";
            SqlParameter merchant = new SqlParameter("@merchantid", merchantID);
            merchant.Direction = ParameterDirection.Input;
            merchant.SqlDbType = SqlDbType.Int;
            merchant.Size = 4;
            objCommand.Parameters.Add(merchant);
            SqlParameter api = new SqlParameter("@webapi", webKey);
            api.Direction = ParameterDirection.Input;
            api.SqlDbType = SqlDbType.Int;
            api.Size = 4;
            objCommand.Parameters.Add(api);
            SqlParameter name = new SqlParameter("@name", txtName.Text);
            name.Direction = ParameterDirection.Input;
            name.SqlDbType = SqlDbType.VarChar;
            name.Size = 50;
            objCommand.Parameters.Add(name);
            SqlParameter uname = new SqlParameter("@username", txtUsername.Text);
            uname.Direction = ParameterDirection.Input;
            uname.SqlDbType = SqlDbType.VarChar;
            uname.Size = 50;
            objCommand.Parameters.Add(uname);
            SqlParameter pass = new SqlParameter("@password", txtPassword.Text);
            pass.Direction = ParameterDirection.Input;
            pass.SqlDbType = SqlDbType.VarChar;
            pass.Size = 50;
            objCommand.Parameters.Add(pass);

            objDB.DoUpdateUsingCmdObj(objCommand);


            lblProcess.Text = "Merchant Added!";
            lblProcess.Visible = true;
            objCommand.Parameters.Clear();

            WebRequest request = WebRequest.Create(url + "CreateVirtualWallet/" + merchantID.ToString() + "/" + webKey.ToString());
            request.Method = "POST";
            //request.ContentLength = MerchantJson.Length;
            request.ContentType = "application/json";

        }

        public int GenerateKey(int num)
        { int key;
            Random random = new Random();
            string r = "";
            int i;
            
            for (i = 0; i < num; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            key = Convert.ToInt32(r);
            return key; 
        }

        protected void btnRSubmit_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            string r = "";
            int i;
            int MerchantID;
            for (i = 1; i < 7; i++)
            {
                r += random.Next(0, 5).ToString();
            }
            MerchantID = Convert.ToInt32(r);
            int WebAPIKey = GenerateKey(5);
            Merchant merchant = new Merchant(lblName.Text, txtRAddress.Text, txtRBAcctType.Text, Convert.ToInt32(txtRBAcctNum.Text), Convert.ToDouble(txtRBAcctBalance.Text), "Restaurant");
            JavaScriptSerializer js = new JavaScriptSerializer();
            String MerchantJson = js.Serialize(merchant);
            try
            {
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.
                
                WebRequest request = WebRequest.Create(url + "CreateVirtualWallet/");
                request.Method = "POST";                
                request.ContentLength = MerchantJson.Length;
                request.ContentType = "application/json";



                // Write the JSON data to the Web Request

                StreamWriter writer = new StreamWriter(request.GetRequestStream());

                writer.Write(MerchantJson);

                writer.Flush();

                writer.Close();

                // Read the data from the Web Response, which requires working with streams.

                WebResponse response = request.GetResponse();

                Stream theDataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(theDataStream);

                String data = reader.ReadToEnd();

                reader.Close();

                response.Close();

                if (data == "true")
                {
                    lblProcess.Visible = true;
                }
                else
                {
                    lblProcess.Visible = true;
                    lblProcess.Text = "Something Went Wrong";
                }
            }
            catch(Exception ex)
            {
                lblProcess.Text = ex.Message;
                lblProcess.Visible = true;
            }

        }

        protected void btnCSubmit_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            string r = "";
            int i;
            int MerchantID;
            for (i = 1; i < 7; i++)
            {
                r += random.Next(0, 5).ToString();
            }
            MerchantID = Convert.ToInt32(r);
            int WebAPIKey = GenerateKey(5);
            Merchant merchant = new Merchant(lblName.Text, txtRAddress.Text, txtRBAcctType.Text, Convert.ToInt32(txtRBAcctNum.Text), Convert.ToDouble(txtRBAcctBalance.Text), "Customer");
            JavaScriptSerializer js = new JavaScriptSerializer();
            String MerchantJson = js.Serialize(merchant);
            try
            {
                // Setup an HTTP POST Web Request and get the HTTP Web Response from the server.

                WebRequest request = WebRequest.Create(url + "CreateVirtualWallet/");
                request.Method = "POST";
                request.ContentLength = MerchantJson.Length;
                request.ContentType = "application/json";



                // Write the JSON data to the Web Request

                StreamWriter writer = new StreamWriter(request.GetRequestStream());

                writer.Write(MerchantJson);

                writer.Flush();

                writer.Close();

                // Read the data from the Web Response, which requires working with streams.

                WebResponse response = request.GetResponse();

                Stream theDataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(theDataStream);

                String data = reader.ReadToEnd();

                reader.Close();

                response.Close();

                if (data == "true")
                {
                    lblProcess.Visible = true;
                }
                else
                {
                    lblProcess.Visible = true;
                    lblProcess.Text = "Something Went Wrong";
                }
            }
            catch (Exception ex)
            {
                lblProcess.Text = ex.Message;
                lblProcess.Visible = true;
            }
        }
    }
}