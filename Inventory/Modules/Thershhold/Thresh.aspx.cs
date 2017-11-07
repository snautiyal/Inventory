using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Inventory.Domain;
using Inventory.Repository;
using Domain = Inventory.Domain;
using System.Web.Configuration;

namespace Inventory.Modules.Thershhold
{
    public partial class Thresh : System.Web.UI.Page
    {
        InventoryDb db = new InventoryDb();
        ProductRepository prodrepo;
        ItemRepository itemRepo;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {
                    prodrepo = new ProductRepository(db);
                    if (!IsPostBack)
                    {


                        BindData();
                    }
                }
                else
                {
                    lblMsg.Text = "you don't have permission for this page";
                }
            }
            else
            {


                Response.Redirect("~/Login/Login.aspx");
            }

        }
        protected void BindData()
        {

            try
            {
                prodrepo = new ProductRepository(db);
                var data = prodrepo.Getthreshhold(tenantId);
                if (data.Count == 0)
                {
                    lblmsgs.Visible = true;
                    lblmsgs.Text = "There's no ThreshHold";
                }
                else
                {
                    lblmsgs.Visible = false;
                    gvItemDetails.DataSource = GetData3();
                    gvItemDetails.DataBind();
                }



            }
            catch (Exception ex)
            {


            }
        }
        private List<ThreshHold_Result> GetData3()
        {

            prodrepo = new ProductRepository(db);
            return prodrepo.Getthreshhold(tenantId);


        }

      
        protected void gvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemDetails.PageIndex = e.NewPageIndex;
            BindData();
        }


    }
}