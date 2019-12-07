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
    public partial class ResturantHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAcctInfo_Click(object sender, EventArgs e)
        {
            divUpdateAcctR.Visible = true;
        }
    }
}