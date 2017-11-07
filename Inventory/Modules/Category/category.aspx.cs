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
using System.Web.Configuration;

namespace Inventory
{
    public partial class category : System.Web.UI.Page
    {
        InventoryDb db = new InventoryDb();
        ICategoryRepository categoryRepo;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);
        //IBrandRepository repo = new BrandRepository(new InventoryDb());
        //IBrandRepository repo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {

                    categoryRepo = new CategoryRepository(db);
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
               
                categoryRepo = new CategoryRepository(db);
                var data = categoryRepo.GetAll().Where(c=>c.TenantId==tenantId).OrderByDescending(o => o.CategoryId).ToList();
                if (data.Count == 0)
                {
                    data.Add(new Domain.Category() { CategoryId = 0, CategoryName = "", CategoryDescription = "" });
                    gvCategoryDetails.DataSource = data;
                    gvCategoryDetails.DataBind();
                    gvCategoryDetails.Rows[0].Visible = false;
                    gvCategoryDetails.ShowFooter = true;
                }
                else
                {
                    gvCategoryDetails.DataSource = data;
                    gvCategoryDetails.DataBind();
                }
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

        protected void gvCategoryDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblCategoryID = (Label)gvCategoryDetails.Rows[e.RowIndex].FindControl("lblCategoryID");
                categoryRepo.Delete(Convert.ToInt32(lblCategoryID.Text));
                categoryRepo.Save();

                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Category Sucessfully DELETED');", true);
            }
            catch (Exception ex)
            {
                lblmsgs.Visible = true;
                lblmsgs.Text = "This Category be use in system";

                Response.Write(ex.Message);

            }



        }
        protected void gvCategoryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("ADD"))
                {

                    TextBox txtAddCategoryName = (TextBox)gvCategoryDetails.FooterRow.FindControl("txtAddCategoryName");
                    TextBox txtAddDescription = (TextBox)gvCategoryDetails.FooterRow.FindControl("txtAddDescription");

                    if (txtAddCategoryName.Text != ("") && txtAddDescription.Text != (""))
                    {
                        Domain.Category category = new Domain.Category()
                        {

                            CategoryDescription = txtAddDescription.Text,
                            CategoryName = txtAddCategoryName.Text,
                            CreationTime = DateTime.Now,
                            LastUpdationTime = DateTime.Now,
                            CreatedBy = "admin",
                            LastUpdatedBy = "admin",
                            TenantId= tenantId,


                        };


                        categoryRepo.Add(category);
                        categoryRepo.Save();
                        BindData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Category Sucessfully Inserted');", true);
                        BindData();
                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Null Filled Not allowed');", true);

                    }

                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }

        }
        protected void gvCategoryDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblCategoryId = (Label)gvCategoryDetails.Rows[e.RowIndex].FindControl("lblCategoryId");
            TextBox txtEditCategoryName = (TextBox)gvCategoryDetails.Rows[e.RowIndex].FindControl("txtEditCategoryName");
            TextBox txtEditDescription = (TextBox)gvCategoryDetails.Rows[e.RowIndex].FindControl("txtEditDescription");

            Domain.Category existing = categoryRepo.GetById(Convert.ToInt32(lblCategoryId.Text));
            if (txtEditCategoryName.Text != ("") && txtEditDescription.Text != (""))
            {
                if (existing != null)
                {
                    existing.CategoryName = txtEditCategoryName.Text;
                    existing.CategoryDescription = txtEditDescription.Text;
                    existing.TenantId = tenantId;
                    existing.LastUpdatedBy = "admin";
                    existing.LastUpdationTime = DateTime.Now;
                }


                categoryRepo.Edit(existing);
                categoryRepo.Save();
                gvCategoryDetails.EditIndex = -1;
                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Category Sucessfully Updated');", true);
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
            //gvCategoryDetails.EditIndex = -1;
            //BindData();

        }
        protected void gvCategoryDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategoryDetails.EditIndex = -1;
            BindData();
        }
        protected void gvCategoryDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategoryDetails.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void gvCategoryDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategoryDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}
