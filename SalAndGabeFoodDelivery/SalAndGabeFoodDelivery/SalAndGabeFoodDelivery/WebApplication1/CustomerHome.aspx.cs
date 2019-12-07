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
namespace WebApplication1
{
    public partial class CustomerHome : System.Web.UI.Page
    {
        DBConnect dBConnect = new DBConnect();
        SqlCommand bigCommand = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getUserName();

                User user = new User();
                
                
                bigCommand.CommandType = CommandType.StoredProcedure;
                bigCommand.CommandText = "TP_AllResturants";
                SqlParameter param = new SqlParameter("@typeUser", user.UserType);
                param.Size = 50;

                param.Direction = ParameterDirection.Input;
                param.SqlDbType = SqlDbType.VarChar;
                bigCommand.Parameters.Add(param);
                DataSet dataSet = dBConnect.GetDataSetUsingCmdObj(bigCommand);

                gvResturants.DataSource = dataSet;
                gvResturants.DataBind();
            }
        }

        protected void btnViewMenu_Click(object sender, EventArgs e)
        {
            menuDiv.Visible = true;
            divRestur.Visible = false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            menuDiv.Visible = false;
            divRestur.Visible = true;
        }

        private void getUserName()
        {
            string nameCust = "";
            string emailName = (string)Session["userNameC"];
            SqlCommand comm = new SqlCommand("SELECT Name FROM [TP_User] WHERE Name = '" + emailName + "'", dBConnect.GetConnection());
            dBConnect.GetConnection().Open();
            SqlDataReader dataRead = comm.ExecuteReader();

            while (dataRead.Read())
            {
                nameCust = dataRead["Name"].ToString();
                lblCustName.Text = "Welcome back, " + nameCust + "!";
            }
            dataRead.Close();
            dBConnect.CloseConnection();
        }
    }
}