using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory.Login
{
    public partial class Login : System.Web.UI.Page
    {
        string userName = WebConfigurationManager.AppSettings["EditorUser"];
        string password = WebConfigurationManager.AppSettings["EditorPassword"];

        string userName1 = WebConfigurationManager.AppSettings["AdminUser"];
        string password1 = WebConfigurationManager.AppSettings["AdminPassword"];
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["Username"] = userName;
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (userName != null)
            {
                if (tbxuser.Text == userName && tbxpwd.Text == password)
                {

                    Session["id"] = tbxuser.Text;
                    Response.Redirect("~/Modules/Search/Search.aspx");

                }

                else
                {
                    lblstatus.Visible = true;

                }
            }
            if (userName1 != null)
            {
                if (tbxuser.Text == userName1 && tbxpwd.Text == password1)
                {
                    //tbxuser.Text = Session["id"].ToString(); 

                    Session["id"] = tbxuser.Text;
                    Response.Redirect("~/Modules/Search/Search.aspx");

                }
                else
                {
                    lblstatus.Visible = true;

                }
            }

        }


    }

}

