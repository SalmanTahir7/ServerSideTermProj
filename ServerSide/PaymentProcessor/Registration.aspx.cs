using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using System.Web.Script.Serialization;  // needed for JSON serializers

using System.IO;                        // needed for Stream and Stream Reader

using System.Net;                       // needed for the Web Request


namespace PaymentProcessor
{
    public partial class Registration : System.Web.UI.Page
    {

        string url = "http://cis-iis2.temple.edu/Fall2019/3342_tug17598/TermProjectWS/api/service/PaymentGateway";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Text == "Customer")
            {
                Customer.Style["visibility"] = "visible";
            }
            else
            {
                Restaurant.Style["visibility"] = "visible";
            }
        }

        public int GenerateKey()
        { int key;
            Random random = new Random();
            string r = "";
            int i;
            
            for (i = 1; i < 7; i++)
            {
                r += random.Next(0, 5).ToString();
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
            int WebAPIKey = GenerateKey();
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
            int WebAPIKey = GenerateKey();
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