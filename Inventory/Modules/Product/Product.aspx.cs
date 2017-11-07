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
    public partial class Product : System.Web.UI.Page
    {
        InventoryDb db = new InventoryDb();
        IProductRepository productRepo;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);
        //IBrandRepository repo = new BrandRepository(new InventoryDb());
        IBrandRepository brandrepo;
        ICategoryRepository catrepo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {

                    productRepo = new ProductRepository(db);




                    if (!IsPostBack)
                    {


                        BindData();
                        //ddlbrandid.DataSource = productRepo.GetAll().ToList();
                        ////ddlbrandid.DataSource=Product;;
                        //ddlbrandid.DataTextField = "BrandName";
                        //ddlbrandid.DataValueField = "BrandId";
                        //ddlbrandid.DataBind();

                        //if (gvProductDetails.ro)
                        //{
                        //    gvProductDetails.DataSource = featureRepo.GetAll().ToList();
                        //    gvProductDetails.DataBind();
                        //}
                        //else
                        //{
                        //    //Empty DataTable to execute the “else-condition”  
                        //    DataTable dt = new Datatable();
                        //    gvProductDetails.DataSource = dt;
                        //    gvProductDetails.DataBind();
                        //}
                        //DataTable FromTable = new DataTable();

                        ////if (gvProductDetails.Rows.Count > 0)
                        //{
                        //    gvProductDetails.DataSource = featureRepo.GetAll().ToList(); 
                        //    gvProductDetails.DataBind();
                        //}
                        //else
                        //{
                        //    FromTable.Rows.Add(FromTable.NewRow());
                        //    gvProductDetails.DataSource = FromTable;
                        //    gvProductDetails.DataBind();
                        //    int TotalColumns = gvProductDetails.Rows[0].Cells.Count;
                        //    gvProductDetails.Rows[0].Cells.Clear();
                        //    gvProductDetails.Rows[0].Cells.Add(new TableCell());
                        //    gvProductDetails.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                        //    gvProductDetails.Rows[0].Cells[0].Text = "No records Found";
                        //}
                        //ddlbrandid.DataSource = featureRepo.GetAll().ToList();
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
                //productRepo = new ProductRepository(db);
                var data = productRepo.GetAll().Where(p=>p.TenantId==tenantId).OrderByDescending(o=>o.ProductId).ToList();
                if (data.Count == 0)
                {
                    //data.Add(new Domain.Product() { ProductId = 0, BrandId = 0, CategoryId = 0, ProductName = "", ProductDescription = "", ProductWeight = "", StockInPrice = 0, ProductPrice = 0, ThreshHold = 0 });
                    data.Add(new Domain.Product() { ProductId = 0, CategoryId = 0, ProductName = "", ProductDescription = "", ProductWeight = "", StockInPrice = 0, ProductPrice = 0, ThreshHold = 0 });
                    gvProductDetails.DataSource = data;
                    gvProductDetails.DataBind();
                    gvProductDetails.Rows[0].Visible = false;
                    gvProductDetails.ShowFooter = true;
                }
                else
                {

                    gvProductDetails.DataSource = data;
                    gvProductDetails.DataBind();

                    //ddlAddCategory.SelectedItem


                }
                ///////////Bind category list///////////////////

                DropDownList ddlAddCategory = (DropDownList)gvProductDetails.FooterRow.FindControl("ddlAddCategory");
                //DropDownList ddlAddBrand = (DropDownList)gvProductDetails.FooterRow.FindControl("ddlAddBrand");

                catrepo = new CategoryRepository(db);
                ddlAddCategory.DataSource = catrepo.GetAll().Where(p =>p.TenantId==tenantId).ToList();
                ddlAddCategory.DataTextField = "CategoryName";
                ddlAddCategory.DataValueField = "CategoryId";
                ddlAddCategory.DataBind();
                /////////Bind brand data list

                //brandrepo = new BrandRepository(db);
                //ddlAddBrand.DataSource = brandrepo.GetAll().ToList();
                //ddlAddBrand.DataTextField = "BrandName";
                //ddlAddBrand.DataValueField = "BrandId";
                //ddlAddBrand.DataBind();

                ////conn.Open();
                //string cmdstr = "Select * from EmployeeDetails";
                //SqlCommand cmd = new SqlCommand(cmdstr, conn);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //adp.Fill(ds);
                //cmd.ExecuteNonQuery();
                //FromTable = ds.Tables[0];
                //if (FromTable.Rows.Count > 0)
                //{
                //    gvProductDetails.DataSource = FromTable;
                //    gvProductDetails.DataBind();
                //}
                //else
                //{
                //    FromTable.Rows.Add(FromTable.NewRow());
                //    gvProductDetails.DataSource = FromTable;
                //    gvProductDetails.DataBind();
                //    int TotalColumns = gvProductDetails.Rows[0].Cells.Count;
                //    gvProductDetails.Rows[0].Cells.Clear();
                //    gvProductDetails.Rows[0].Cells.Add(new TableCell());
                //    gvProductDetails.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                //    gvProductDetails.Rows[0].Cells[0].Text = "No records Found";
                //}
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

        protected void gvProductDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblProductID = (Label)gvProductDetails.Rows[e.RowIndex].FindControl("lblProductID");
                productRepo.Delete(Convert.ToInt32(lblProductID.Text));
                productRepo.Save();
                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Product Sucessfully DELETED');", true);
            }
            catch (Exception ex)
            {
                lblmsgs.Visible = true;
                lblmsgs.Text = "This Product be use in system";

                Response.Write(ex.Message);

            }
            //conn.Open();
            //string cmdstr = "delete from EmployeeDetails where empid=@empid";
            //SqlCommand cmd = new SqlCommand(cmdstr, conn);
            //cmd.Parameters.AddWithValue("@empid", lblEmpID.Text);
            //cmd.ExecuteNonQuery();
            //conn.Close();


        }


        protected void gvProductDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                 
                 
                if (e.CommandName.Equals("ADD"))
                {
                    Button btnadd=(Button)gvProductDetails.FooterRow.FindControl("lbtnAdd");
                    DropDownList ddlAddCategory = (DropDownList)gvProductDetails.FooterRow.FindControl("ddlAddCategory");
                    //DropDownList ddlAddBrand = (DropDownList)gvProductDetails.FooterRow.FindControl("ddlAddBrand");
                    TextBox txtAddProductName = (TextBox)gvProductDetails.FooterRow.FindControl("txtAddProductName");
                    TextBox txtAddDescription = (TextBox)gvProductDetails.FooterRow.FindControl("txtAddDescription");
                    TextBox txtAddthresh = (TextBox)gvProductDetails.FooterRow.FindControl("txtAddThresh");

                    TextBox txtAddWeight = (TextBox)gvProductDetails.FooterRow.FindControl("txtAddWeight");
                    TextBox txtAddPrice = (TextBox)gvProductDetails.FooterRow.FindControl("txtAddPrice");
                    //TextBox txtAddbarcode = (TextBox)gvProductDetails.FooterRow.FindControl("txtAddbarcode");
                    TextBox txtstockinprice = (TextBox)gvProductDetails.FooterRow.FindControl("txtstockinAddPrice");
                    //btn.Visible = false;
                    btnadd.Enabled = false;

                    //TextBox txtAddFeatureName = (TextBox)gvProductDetails.FooterRow.FindControl("txtAddFeatureName");
                    //TextBox txtAddDescription = (TextBox)gvProductDetails.FooterRow.FindControl("txtAddDescription");
                    //if (ddlAddCategory.SelectedItem.Text != ("") && ddlAddBrand.Text != ("") && txtAddProductName.Text != ("")) if (ddlAddCategory.SelectedItem.Text != ("") && ddlAddBrand.Text != ("") && txtAddProductName.Text != (""))
                    if (ddlAddCategory.SelectedItem.Text != ("") && txtAddProductName.Text != (""))
                    {

                        Domain.Product product = new Domain.Product()
                        {
                            
                            CategoryId = Convert.ToInt32(ddlAddCategory.SelectedValue),
                            //BrandId = Convert.ToInt32(ddlAddBrand.SelectedValue),
                            ProductName = txtAddProductName.Text,
                            ProductDescription = txtAddDescription.Text,
                            ProductWeight = txtAddWeight.Text,
                            StockInPrice = !string.IsNullOrWhiteSpace(txtstockinprice.Text) ? Convert.ToDecimal(txtstockinprice.Text) : 0,
                            ProductPrice = !string.IsNullOrWhiteSpace(txtAddPrice.Text) ? Convert.ToDecimal(txtAddPrice.Text) : 0,
                            ThreshHold = !string.IsNullOrWhiteSpace(txtAddthresh.Text) ? (Convert.ToInt32(txtAddthresh.Text)) : 0,
                            CreationTime = DateTime.Now,
                            LastUpdationTime = DateTime.Now,
                            CreatedBy = "admin",
                            LastUpdatedBy = "admin",
                            TenantId=tenantId,


                        };

                        productRepo.Add(product);
                        productRepo.Save();
                        BindData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Product Sucessfully Inserted');", true);
                        BindData();
                        btnadd.Enabled = true;
                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Null Filled Not allowed');", true);
                        btnadd.Enabled = true; 



                    }
                   

                }
                //BindData();
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }


        }
        protected void gvProductDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label lblProductId = (Label)gvProductDetails.Rows[e.RowIndex].FindControl("lblProductId");
                DropDownList ddlEditCategory = (DropDownList)gvProductDetails.Rows[e.RowIndex].FindControl("ddlEditCategory");
                //DropDownList ddlEditBrand = (DropDownList)gvProductDetails.Rows[e.RowIndex].FindControl("ddlEditBrand");
                TextBox txtEditProductName = (TextBox)gvProductDetails.Rows[e.RowIndex].FindControl("txtEditProductName");
                TextBox txtEditDescription = (TextBox)gvProductDetails.Rows[e.RowIndex].FindControl("txtEditDescription");


                TextBox txtEditWeight = (TextBox)gvProductDetails.Rows[e.RowIndex].FindControl("txtEditWeight");
                TextBox txtstockinEditPrice = (TextBox)gvProductDetails.Rows[e.RowIndex].FindControl("txtstockinEditPrice");
                TextBox txtEditPrice = (TextBox)gvProductDetails.Rows[e.RowIndex].FindControl("txtEditPrice");
                TextBox txtEditProduct = (TextBox)gvProductDetails.Rows[e.RowIndex].FindControl("txtEditbarcode");
                TextBox txtEditthresh = (TextBox)gvProductDetails.Rows[e.RowIndex].FindControl("txtEditThresh");
                //if (ddlEditCategory.SelectedItem.Text != ("") && ddlEditBrand.Text != ("") && txtEditProductName.Text != (""))
                if (ddlEditCategory.SelectedItem.Text != ("") && txtEditProductName.Text != (""))
                {
                    Domain.Product existing = productRepo.GetById(Convert.ToInt32(lblProductId.Text));
                    if (existing != null)
                    {

                        existing.CategoryId = Convert.ToInt32(ddlEditCategory.SelectedValue);
                        //existing.BrandId = Convert.ToInt32(ddlEditBrand.SelectedValue);
                        existing.ProductName = txtEditProductName.Text;
                        existing.ProductDescription = txtEditDescription.Text;
                        existing.ProductWeight = txtEditWeight.Text;
                        existing.StockInPrice = Convert.ToDecimal(txtstockinEditPrice.Text);
                        existing.ProductPrice = Convert.ToDecimal(txtEditPrice.Text);
                        //existing.BarCode = Convert.ToDecimal(txtEditProduct.Text);
                        existing.ThreshHold = Convert.ToInt32(txtEditthresh.Text);
                        existing.LastUpdatedBy = "admin";
                        existing.LastUpdationTime = DateTime.Now;
                        existing.TenantId = tenantId;



                    }


                    productRepo.Edit(existing);
                    productRepo.Save();
                    gvProductDetails.EditIndex = -1;
                    BindData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Product Sucessfully Updated');", true);
                }
                else
                {


                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Null Filled Not allowed');", true);

                }
            }
            catch (Exception ex)
            {

            }


        }
        protected void gvProductDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProductDetails.EditIndex = -1;
            BindData();
        }
        protected void gvProductDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvProductDetails.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            BindData();
            gvProductDetails.ShowFooter = true;
            gvProductDetails.ShowHeader = true;
        }

        protected void gvProductDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlEditCategory");
            if (ddlCategory != null)
            {
                catrepo = new CategoryRepository(db);
                ddlCategory.DataSource = catrepo.GetAll().Where(p => p.TenantId == tenantId).ToList();
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();

                ddlCategory.SelectedValue = gvProductDetails.DataKeys[e.Row.RowIndex].Values[1].ToString();
                Label lblCat = (Label)e.Row.FindControl("lblCategoryId");
                if (lblCat != null)
                    lblCat.Text = "3";
            }


            DropDownList ddlBrand = (DropDownList)e.Row.FindControl("ddlEditBrand");
            if (ddlBrand != null)
            {
                brandrepo = new BrandRepository(db);
                ddlBrand.DataSource = brandrepo.GetAll().Where(p => p.TenantId == tenantId).ToList();
                ddlBrand.DataTextField = "BrandName";
                ddlBrand.DataValueField = "BrandId";
                ddlBrand.DataBind();

                ddlBrand.SelectedValue = gvProductDetails.DataKeys[e.Row.RowIndex].Values[2].ToString();
                Label lblBrand = (Label)e.Row.FindControl("lblBrandId");
                if (lblBrand != null)
                    lblBrand.Text = "3";
            }
        }
        protected void gvProductDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductDetails.PageIndex = e.NewPageIndex;
            BindData();
        }

    }
}
