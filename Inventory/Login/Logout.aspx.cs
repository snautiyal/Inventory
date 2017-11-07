﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.Login
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"]!=null)
            {

                Session.Clear();
                Response.Redirect("Login/LoginPage.aspx");
            
            }
        }
    }
}