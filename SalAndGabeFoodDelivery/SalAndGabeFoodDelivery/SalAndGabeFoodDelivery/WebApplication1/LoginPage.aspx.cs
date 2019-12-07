using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Utilities;
using Classes;
using System.Data;
using System.Data.SqlClient;

namespace SalAndGabeFoodDelivery
{
    public partial class LoginPage : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        SqlCommand bigCommand = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnCreateAcct_Click(object sender, EventArgs e)
        {
            regDiv.Visible = true;
            loginDiv.Visible = false;
            regRestDiv.Visible = false;
        }

        protected void btnCreateRestAcct_Click(object sender, EventArgs e)
        {
            regDiv.Visible = false;
            loginDiv.Visible = false;
            regRestDiv.Visible = true;
        }

        protected void btnBack2_Click(object sender, EventArgs e)
        {
            regDiv.Visible = false;
            loginDiv.Visible = true;
            regRestDiv.Visible = false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            regDiv.Visible = false;
            loginDiv.Visible = true;
            regRestDiv.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if(IsValidLogin() == true)
            {
                SqlCommand sql = new SqlCommand("SELECT Email, Password FROM [TP_Users] WHERE Email = '" + txtUN.Text + "' AND Password = '" + txtPW.Text + "'", dBConnect.GetConnection());
                dBConnect.GetConnection().Open();
                SqlDataReader read = sql.ExecuteReader();

                if(read.Read() == true)
                {
                    string un = read["Email"].ToString();
                    string pw = read["Password"].ToString();

                    if(un != txtUN.Text && pw != txtPW.Text)
                    {
                        errorLBL.Visible = true;
                        read.Close();
                    }
                    else if(un == txtUN.Text && pw == txtPW.Text)
                    {
                        read.Close();
                        dBConnect.CloseConnection();
                        User user = new User();
                        SqlCommand sqlGet = new SqlCommand("SELECT * FROM [TP_Users] WHERE Email = '" + txtUN.Text + "' AND Password = '" + txtPW.Text + "'", dBConnect.GetConnection());
                        dBConnect.GetConnection().Open();
                        SqlDataReader getReader = sqlGet.ExecuteReader();

                        if (getReader.Read() == true)
                        {
                            user.LoginID = getReader["LoginID"].ToString();
                            user.Email = getReader["Email"].ToString();
                            user.Password = getReader["Password"].ToString();
                            user.UserType = getReader["UserType"].ToString();
                            user.Name = getReader["Name"].ToString();
                            user.Image = getReader["Image"].ToString();
                            user.Address = getReader["Address"].ToString();
                            user.BillingAddress = getReader["BillingAddress"].ToString();
                            user.PhoneNumber = getReader["PhoneNumber"].ToString();
                            user.MenuID = getReader["MenuID"].ToString();
                            user.ResturantType = getReader["ResturantType"].ToString();
                        }

                        if(user.UserType == "Customer")
                        {
                            getReader.Close();
                            Session["customer"] = user;
                            Session["userNameC"] = user.Name;
                            Server.Transfer("CustomerHome.aspx");
                        }
                        else if(user.UserType == "Resturant")
                        {
                            getReader.Close();
                            Session["resturant"] = user;
                            Session["userNameR"] = user.Name;
                            Server.Transfer("ResturantHome.aspx");
                        }

                    }
                }


                /*User user = new User();
                bigCommand.CommandType = CommandType.StoredProcedure;
                bigCommand.CommandText = "TP_Login";
                SqlParameter param1 = new SqlParameter("@Email", user.Email);
                SqlParameter param2 = new SqlParameter("@Password", user.Password);
                */
                
            }
        }

        private bool IsValidLogin()
        {
            if (txtUN.Text == "")
            {
                errorLBL.Visible = true;
                txtUN.Text = "";
                txtPW.Text = "";
                txtUN.Focus();
                return false;
            }

            if (txtPW.Text == "")
            {
                errorLBL.Visible = true;
                txtUN.Text = "";
                txtPW.Text = "";
                txtUN.Focus();
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}