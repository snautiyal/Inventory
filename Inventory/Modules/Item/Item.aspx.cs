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

namespace Inventory
{
    public partial class Item : System.Web.UI.Page
    {
        InventoryDb db = new InventoryDb();
        IItemRepository itemRepo;
        IvendorRepository vendorRepo;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);
        //IItemRepository repo = new ItemRepository(new InventoryDb());
        //IItemRepository repo;
        IStockingDetailsRepository stockingrepo;
        IProductRepository Prorepo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {

                    stockingrepo = new StockingDetailsRepository(db);
                    itemRepo = new ItemRepository(db);
                    if (!IsPostBack)
                    {

                        BindData();
                        //gvItemDetails.DataSource = itemRepo.GetAll().ToList();
                        //gvItemDetails.DataBind();
                        //ddlbrandid.DataSource = itemRepo.GetAll().ToList();
                        //ddlbrandid.DataSource=Product;;
                        //ddlbrandid.DataTextField = "BrandName";
                        //ddlbrandid.DataValueField = "BrandId";
                        // ddlbrandid.DataBind();
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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void BindData()
        {

            try
            {
                //itemRepo = new ItemRepository(db);
                //var data = itemRepo.GetAll().ToList();
                var data = stockingrepo.GetAll().Where(i=>i.TenantId==tenantId).OrderByDescending(o => o.StockingId).ToList();
                if (data.Count == 0)
                {
                    data.Add(new Domain.StockingDetail() { ProductId=0, Quantity = 0, BillNo = "", VendorId = 0, CreationTime = DateTime.Now });
                    gvItemDetails.DataSource = data;
                    gvItemDetails.DataBind();
                    gvItemDetails.Rows[0].Visible = false;
                    gvItemDetails.ShowFooter = true;
                }
                else
                {
                    gvItemDetails.DataSource = data;
                    gvItemDetails.DataBind();
                }

                DropDownList ddlAddProduct = (DropDownList)gvItemDetails.FooterRow.FindControl("ddlAddProduct");

                Prorepo = new ProductRepository(db);
                ddlAddProduct.DataSource = Prorepo.GetAll().Where(p=>p.TenantId==tenantId).ToList();
                ddlAddProduct.DataTextField = "ProductName";
                ddlAddProduct.DataValueField = "ProductId";
                ddlAddProduct.DataBind();

                DropDownList ddlAddvendor = (DropDownList)gvItemDetails.FooterRow.FindControl("ddlAddvendor");

                vendorRepo = new vendorRepository(db);
                ddlAddvendor.DataSource = vendorRepo.GetAll().Where(v=>v.TenantId==tenantId).ToList();
                ddlAddvendor.DataTextField = "VendorName";
                ddlAddvendor.DataValueField = "VendorId";
                ddlAddvendor.DataBind();

                //DropDownList ddlAddBillno = (DropDownList)gvItemDetails.FooterRow.FindControl("ddlAddBillno");

                //vendorRepo = new vendorRepository(db);
                //ddlAddBillno.DataSource = vendorRepo.GetAll().ToList();
                //ddlAddBillno.DataTextField = "Billno";
                //ddlAddBillno.DataValueField = "VendorId";
                //ddlAddBillno.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {

                //conn.Close();
            }
        }

        protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblItemID = (Label)gvItemDetails.Rows[e.RowIndex].FindControl("lblItemID");
                itemRepo.Delete(Convert.ToInt32(lblItemID.Text));
                itemRepo.Save();

                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Stock Sucessfully DELETED');", true);
            }

            catch (Exception ex)
            {
                lblmsgs.Visible = true;
                lblmsgs.Text = "This Item be use in system";

                Response.Write(ex.Message);

            }



        }


        protected void gvItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("ADD"))
                {
                    DropDownList ddlAddProduct = (DropDownList)gvItemDetails.FooterRow.FindControl("ddlAddProduct");
                    int productid = !String.IsNullOrEmpty(ddlAddProduct.Text) ? Convert.ToInt32(ddlAddProduct.Text) : 0;
                    // DropDownList ddlAddvendor = (DropDownList)gvItemDetails.FooterRow.FindControl("ddlAddvendor");
                    TextBox ddlAddBillno = (TextBox)gvItemDetails.FooterRow.FindControl("ddlAddBillno");
                    TextBox txtAddCount = (TextBox)gvItemDetails.FooterRow.FindControl("txtAddCount");
                    TextBox txtAddthresh = (TextBox)gvItemDetails.FooterRow.FindControl("txtAddThresh");
                    TextBox tbxfrom = (TextBox)gvItemDetails.FooterRow.FindControl("tbxfrom");
                    DateTime puchasedate = Convert.ToDateTime(tbxfrom.Text);
                    DateTime date = DateTime.Today;

                    if (puchasedate > date)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('PurchaseDate Not be Greter than CurrentDate');", true);

                    }
                    else
                    {

                        Domain.Item existing = itemRepo.Find(p => p.ProductId == productid).FirstOrDefault();
                        if (ddlAddProduct.SelectedItem.Text != ("") && txtAddCount.Text != (""))
                        {
                            if (existing != null)
                            {


                                existing.Count += Convert.ToInt32(txtAddCount.Text);
                                //existing.BillNo = ddlAddBillno.Text;
                                //existing.VendorID = Convert.ToInt32(ddlAddvendor.SelectedValue);
                                //existing.ThreshHold += Convert.ToInt32(txtAddthresh.Text);
                                existing.LastUpdationTime = DateTime.Now;
                                existing.LastUpdatedBy = "admin";
                                existing.TenantId = tenantId;
                                itemRepo.Edit(existing);
                                itemRepo.Save();
                                gvItemDetails.EditIndex = -1;
                                updatingitem();
                                BindData();
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Stock Sucessfully Updated');", true);
                                BindData();
                            }
                            else
                            {
                                Domain.Item item = new Domain.Item()
                                {

                                    ProductId = Convert.ToInt32(ddlAddProduct.SelectedValue),
                                    //VendorID = Convert.ToInt32(ddlAddvendor.SelectedValue),
                                    // BillNo =ddlAddBillno.Text,
                                    Count = Convert.ToInt32(txtAddCount.Text),
                                    //ThreshHold = Convert.ToInt32(txtAddthresh.Text),
                                    CreationTime = puchasedate,
                                    LastUpdationTime = puchasedate,
                                    CreatedBy = "admin",
                                    LastUpdatedBy = "admin",
                                    TenantId = tenantId,
                                };

                                itemRepo.Add(item);
                                itemRepo.Save();
                                updatingitem();
                                BindData();
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Stock Sucessfully Inserted');", true);
                                BindData();

                            }
                        }

                        else
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Null Filled Not allowed');", true);

                        }
                        //ddlAddProduct = Find(p => p.ProductId == productid).FirstOrDefault();
                    }
                }

            }

            catch (Exception ex)
            {

                Response.Write(ex.Message);
               

            }

        }
        private void updatingitem()
        {

            DropDownList ddlAddProduct = (DropDownList)gvItemDetails.FooterRow.FindControl("ddlAddProduct");
            int productid = !String.IsNullOrEmpty(ddlAddProduct.Text) ? Convert.ToInt32(ddlAddProduct.Text) : 0;
            TextBox txtAddCount = (TextBox)gvItemDetails.FooterRow.FindControl("txtAddCount");
            DropDownList ddlAddvendor = (DropDownList)gvItemDetails.FooterRow.FindControl("ddlAddvendor");
            TextBox ddlAddBillno = (TextBox)gvItemDetails.FooterRow.FindControl("ddlAddBillno");

            TextBox tbxfrom = (TextBox)gvItemDetails.FooterRow.FindControl("tbxfrom");


            Domain.StockingDetail stocking = new Domain.StockingDetail();
            {
                stocking.ProductId = Convert.ToInt32(ddlAddProduct.SelectedValue);
                stocking.BillNo = ddlAddBillno.Text;
                stocking.VendorId = Convert.ToInt32(ddlAddvendor.SelectedValue);
                stocking.Quantity = Convert.ToInt32(txtAddCount.Text);
                stocking.CreatedBy = "admin";
                stocking.CreationTime = Convert.ToDateTime(tbxfrom.Text);
                stocking.TenantId = tenantId;


            };
            stockingrepo.Add(stocking);
            stockingrepo.Save();
            BindData();


        }








        protected void gvItemDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblItemID = (Label)gvItemDetails.Rows[e.RowIndex].FindControl("lblItemID");
            DropDownList ddlEditProduct = (DropDownList)gvItemDetails.Rows[e.RowIndex].FindControl("ddlEditProduct");
            DropDownList ddlEditvendor = (DropDownList)gvItemDetails.Rows[e.RowIndex].FindControl("ddlEditvendor");
            TextBox ddlEditBillno = (TextBox)gvItemDetails.Rows[e.RowIndex].FindControl("ddlEditBillno");
            TextBox txtEditCount = (TextBox)gvItemDetails.Rows[e.RowIndex].FindControl("txtEditCount");
            TextBox txtEditthresh = (TextBox)gvItemDetails.Rows[e.RowIndex].FindControl("txtEditThresh");
            if (ddlEditProduct.SelectedItem.Text != ("") && txtEditCount.Text != (""))
            {
                Domain.Item existing = itemRepo.GetById(Convert.ToInt32(lblItemID.Text));
                if (existing != null)
                {
                    existing.ProductId = Convert.ToInt32(ddlEditProduct.SelectedValue);
                    existing.Count = Convert.ToInt32(txtEditCount.Text);
                    existing.TenantId = tenantId;
                    //existing.VendorID = Convert.ToInt32(ddlEditvendor.SelectedValue);
                    //existing.BillNo = ddlEditBillno.Text;
                    //existing.ThreshHold = Convert.ToInt32(txtEditthresh.Text);
                    existing.LastUpdatedBy = "admin";
                    existing.LastUpdationTime = DateTime.Now;
                }


                itemRepo.Edit(existing);
                itemRepo.Save();
                gvItemDetails.EditIndex = -1;
                //updatingitem();
                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Stock Sucessfully Updated');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Null Filled Not allowed');", true);
            }
        }
        protected void gvItemDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvItemDetails.EditIndex = -1;
            BindData();
        }
        protected void gvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvItemDetails.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void gvItemDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlProduct = (DropDownList)e.Row.FindControl("ddlEditProduct");

            if (ddlProduct != null)
            {
                Prorepo = new ProductRepository(db);
                ddlProduct.DataSource = Prorepo.GetAll().Where(v => v.TenantId == tenantId).ToList();
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataBind();

                ddlProduct.SelectedValue = gvItemDetails.DataKeys[e.Row.RowIndex].Values[1].ToString();
                Label lblPro = (Label)e.Row.FindControl("lblProductId");
                if (lblPro != null)
                    lblPro.Text = "3";
            }
            DropDownList ddlvendor = (DropDownList)e.Row.FindControl("ddlEditvendor");
            if (ddlvendor != null)
            {
                stockingrepo = new StockingDetailsRepository(db);
                vendorRepo = new vendorRepository(db);
                ddlvendor.DataSource = vendorRepo.GetAll().Where(v => v.TenantId == tenantId).ToList();
                ddlvendor.DataTextField = "VendorName";
                ddlvendor.DataValueField = "VendorID";
                ddlvendor.DataBind();

                ddlvendor.SelectedValue = gvItemDetails.DataKeys[e.Row.RowIndex].Values[2].ToString();
                Label lblvend = (Label)e.Row.FindControl("lblvendorid");
                if (lblvend != null)
                    lblvend.Text = "3";
            }

        }
        protected void gvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}