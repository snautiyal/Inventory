using Inventory.Domain;
using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Domain = Inventory.Domain;
using System.Web.Configuration;

namespace Inventory
{
    public partial class Brand : System.Web.UI.Page
    {
        InventoryDb db = new InventoryDb();
        IBrandRepository brandRepo;
        ICategoryRepository catrepo;
        ICategoryBrandMappingRepository mappingRepo;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);
        //IBrandRepository repo = new BrandRepository(new InventoryDb());
        //IBrandRepository repo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {
                    brandRepo = new BrandRepository(db);
                    catrepo = new CategoryRepository(db);
                    mappingRepo = new CategoryBrandMappingRepository(db);

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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void BindData()
        {

            try
            {
                brandRepo = new BrandRepository(db);
                var data = brandRepo.GetAll().Where(b=>b.TenantId==tenantId).OrderByDescending(o => o.BrandId).ToList();
                if (data.Count == 0)
                {
                    data.Add(new Domain.Brand() { BrandId = 0, BrandName = "", BrandDescription = "" });
                    gvBrandDetails.DataSource = data;
                    gvBrandDetails.DataBind();
                    gvBrandDetails.Rows[0].Visible = false;
                    gvBrandDetails.ShowFooter = true;
                }
                else
                {
                    gvBrandDetails.DataSource = data;
                    gvBrandDetails.DataBind();

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {


            }

        }



        protected void gvBrandDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {



                Label lblBrandID = (Label)gvBrandDetails.Rows[e.RowIndex].FindControl("lblBrandID");
                int brandid = int.Parse(lblBrandID.Text);
                var CategoryBrandMappings = mappingRepo.Find(m => m.BrandId == brandid);
                if (CategoryBrandMappings != null)
                {
                    foreach (var item in CategoryBrandMappings)
                    {
                        mappingRepo.Delete(item.CategoryBrandMappingId);
                    }
                    brandRepo.Delete(brandid);
                }

                brandRepo.Save();

                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Brand Sucessfully DELETED');", true);
            }
            catch (Exception)
            {
                lblmsgs.Visible = true;
                lblmsgs.Text = "This Brand be use in system";


            }
            //conn.Open();



        }
        protected void gvBrandDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("ADD"))
            {

                TextBox txtAddBrandName = (TextBox)gvBrandDetails.FooterRow.FindControl("txtAddBrandName");
                TextBox txtAddDescription = (TextBox)gvBrandDetails.FooterRow.FindControl("txtAddDescription");
                ListBox lstCategory = (ListBox)gvBrandDetails.FooterRow.FindControl("lstCategory");

                if (txtAddBrandName.Text != ("") && txtAddDescription.Text != ("") && lstCategory.Text != (""))
                {

                    Domain.Brand brand = new Domain.Brand()
                   {
                       BrandDescription = txtAddDescription.Text,
                       BrandName = txtAddBrandName.Text,
                       CreationTime = DateTime.Now,
                       LastUpdationTime = DateTime.Now,
                       CreatedBy = "admin",
                       LastUpdatedBy = "admin",
                       TenantId=tenantId,
                   };
                    brand.CategoryBrandMappings = new List<CategoryBrandMapping>();
                    foreach (var index in lstCategory.GetSelectedIndices())
                    {
                        brand.CategoryBrandMappings.Add(new CategoryBrandMapping() { Brand = brand, CategoryId = Convert.ToInt32(lstCategory.Items[index].Value),TenantId= tenantId});
                    }


                    brandRepo.Add(brand);
                    brandRepo.Save();
                    BindData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Brand Sucessfully Inserted');", true);
                    BindData();
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Null Filled Not allowed');", true);

                }

            }
            //conn.Open();
            //string cmdstr = "insert into EmployeeDetails(empid,name,designation,city,country) values(@empid,@name,@designation,@city,@country)";
            //SqlCommand cmd = new SqlCommand(cmdstr, conn);
            //cmd.Parameters.AddWithValue("@empid", txtAddEmpID.Text);
            //cmd.Parameters.AddWithValue("@name", txtAddName.Text);
            //cmd.Parameters.AddWithValue("@designation", txtAddDesignation.Text);
            //cmd.Parameters.AddWithValue("@city", txtAddCity.Text);
            //cmd.Parameters.AddWithValue("@country", txtAddCountry.Text);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //BindData();

        }
        protected void gvBrandDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblBrandID = (Label)gvBrandDetails.Rows[e.RowIndex].FindControl("lblBrandID");
            TextBox txtEditBrandName = (TextBox)gvBrandDetails.Rows[e.RowIndex].FindControl("txtEditBrandName");
            TextBox txtEditDescription = (TextBox)gvBrandDetails.Rows[e.RowIndex].FindControl("txtEditDescription");
            ListBox lstCategory = (ListBox)gvBrandDetails.Rows[e.RowIndex].FindControl("ddlEditCategory");

            if (txtEditBrandName.Text != ("") && txtEditDescription.Text != ("") && lstCategory.Text != (""))
            {
                Domain.Brand existing = brandRepo.GetById(Convert.ToInt32(lblBrandID.Text));
                if (existing != null)
                {
                    existing.BrandName = txtEditBrandName.Text;
                    existing.BrandDescription = txtEditDescription.Text;
                    existing.LastUpdatedBy = "admin";
                    existing.TenantId = tenantId;
                    existing.LastUpdationTime = DateTime.Now;
                }
                existing.CategoryBrandMappings = new List<CategoryBrandMapping>();
                int[] existingIds = existing.CategoryBrandMappings.Select(c => c.CategoryBrandMappingId).ToArray();
                foreach (var id in existingIds)
                {
                    mappingRepo.Delete(id);
                }
                foreach (var index in lstCategory.GetSelectedIndices())
                {
                    existing.CategoryBrandMappings.Add(new CategoryBrandMapping() { Brand = existing, CategoryId = Convert.ToInt32(lstCategory.Items[index].Value),TenantId=tenantId });
                }

                brandRepo.Edit(existing);
                brandRepo.Save();
                gvBrandDetails.EditIndex = -1;
                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Brand Sucessfully Updated');", true);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Null Filled Not allowed');", true);

            }
            //conn.Open();
            //string cmdstr = "update EmployeeDetails set name=@name,designation=@designation,city=@city,country=@country where empid=@empid";
            //SqlCommand cmd = new SqlCommand(cmdstr, conn);
            //cmd.Parameters.AddWithValue("@empid", lblEditEmpID.Text);
            //cmd.Parameters.AddWithValue("@name", txtEditName.Text);
            //cmd.Parameters.AddWithValue("@designation", txtEditDesignation.Text);
            //cmd.Parameters.AddWithValue("@city", txtEditCity.Text);
            //cmd.Parameters.AddWithValue("@country", txtEditCountry.Text);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //gvBrandDetails.EditIndex = -1;
            //BindData();

        }
        protected void gvBrandDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBrandDetails.EditIndex = -1;
            BindData();
        }
        protected void gvBrandDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvBrandDetails.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void gvBrandDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
            ListBox ddlCategory = (ListBox)e.Row.FindControl("ddlEditCategory");
            ListBox ListCategories = (ListBox)e.Row.FindControl("lstCategories");
            if (ListCategories != null)
            {
                var brandId = Convert.ToInt32(gvBrandDetails.DataKeys[e.Row.RowIndex].Values[0]);
                var categories = catrepo.Find(c => c.CategoryBrandMappings.AsQueryable().Where(cbm => cbm.BrandId == brandId && cbm.TenantId==tenantId).Count() > 0);
                ListCategories.DataSource = categories;
                ListCategories.DataTextField = "CategoryName";
                ListCategories.DataValueField = "CategoryId";
                ListCategories.DataBind();

            }
            if (ddlCategory != null)
            {
                catrepo = new CategoryRepository(db);
                var brandId = Convert.ToInt32(gvBrandDetails.DataKeys[e.Row.RowIndex].Values[0]);
                var categories = catrepo.Find(c => c.CategoryBrandMappings.AsQueryable().Where(cbm => cbm.BrandId == brandId).Count() > 0);
                //ddlCategory.Items.Add((categories));
                //ddlCategory.Items.Add("Item " + categories);
                ddlCategory.DataSource = catrepo.GetAll().Where(c => c.TenantId == tenantId).OrderByDescending(c =>c.CategoryId).ToList();
                ddlCategory.SelectionMode = ListSelectionMode.Multiple;
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();

                foreach (ListItem Item in ddlCategory.Items)
                {
                    if (categories.Where(c => c.CategoryId.ToString() == Item.Value).Count() > 0)
                        Item.Selected = true;


                }
                ddlCategory.DataSource = catrepo.GetAll().Where(p => p.TenantId == tenantId).ToList();

                //ListCategories.SelectedValue = gvBrandDetails.DataKeys[e.Row.RowIndex].Values[2].ToString();
            }

            ListBox ListCategory = (ListBox)e.Row.FindControl("lstCategory");
            //var data = itemrepo.GetAll().ToList();

            if (ListCategory != null)
            {
                catrepo = new CategoryRepository(db);
                ListCategory.DataSource = catrepo.GetAll().Where(p => p.TenantId == tenantId).ToList();
                ListCategory.DataTextField = "CategoryName";
                ListCategory.DataValueField = "CategoryId";
                ListCategory.DataBind();


            }
        }



        protected void gvBrandDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBrandDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}
