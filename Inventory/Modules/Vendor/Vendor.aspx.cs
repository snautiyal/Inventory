using Inventory.Domain;
using Inventory.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace Inventory.Modules.Vendor
{
    public partial class Vendor : System.Web.UI.Page
    {
        InventoryDb db = new InventoryDb();
        IvendorRepository venderrepo;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {
                    venderrepo = new vendorRepository(db);
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

                venderrepo = new vendorRepository(db);
                var data = venderrepo.GetAll().Where(v=>v.TenantId==tenantId).OrderByDescending(o => o.VendorID).ToList();
                if (data.Count == 0)
                {
                    data.Add(new Domain.Vendor() { VendorID = 0, VendorName = "", Address = "", ContactNo = "", BillNo = "" });
                    gvvendor.DataSource = data;
                    gvvendor.DataBind();
                    gvvendor.Rows[0].Visible = false;
                    gvvendor.ShowFooter = true;
                }
                else
                {
                    gvvendor.DataSource = data;
                    gvvendor.DataBind();
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

        protected void gvvendor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblvendorID = (Label)gvvendor.Rows[e.RowIndex].FindControl("lblvendorID");
                venderrepo.Delete(Convert.ToInt32(lblvendorID.Text));
                venderrepo.Save();

                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vendor Sucessfully DELETED');", true);
            }
            catch (Exception ex)
            {
                lblmsgs.Visible = true;
                lblmsgs.Text = "This Category be use in system";

                Response.Write(ex.Message);

            }



        }
        protected void gvvendor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("ADD"))
                {

                    TextBox txtAddvendorName = (TextBox)gvvendor.FooterRow.FindControl("txtAddVendorName");
                    TextBox txtAddAddress = (TextBox)gvvendor.FooterRow.FindControl("txtAddAddress");
                    TextBox txtAddContactNo = (TextBox)gvvendor.FooterRow.FindControl("txtAddContactNo");
                    TextBox txtAddBillNo = (TextBox)gvvendor.FooterRow.FindControl("txtAddBillNo");


                    if (txtAddvendorName.Text != ("") && txtAddAddress.Text != ("") && txtAddContactNo.Text != ("") && txtAddBillNo.Text != (""))
                    {
                        Domain.Vendor vendor = new Domain.Vendor()
                        {
                            VendorName = txtAddvendorName.Text,
                            Address = txtAddAddress.Text,
                            ContactNo = txtAddContactNo.Text,
                            BillNo = txtAddBillNo.Text,
                            CreationTime = DateTime.Now,
                            LastUpdationTime = DateTime.Now,
                            CreatedBy = "admin",
                            LastUpdatedBy = "admin",
                            TenantId=tenantId,
                        };


                        venderrepo.Add(vendor);
                        venderrepo.Save();
                        BindData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vendor Sucessfully Inserted');", true);
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
        protected void gvvendor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblvendorId = (Label)gvvendor.Rows[e.RowIndex].FindControl("lblvendorId");
            TextBox txtEditVendorName = (TextBox)gvvendor.Rows[e.RowIndex].FindControl("txtEditVendorName");
            TextBox txtEditAddress = (TextBox)gvvendor.Rows[e.RowIndex].FindControl("txtEditAddress");
            TextBox txtEditContactNo = (TextBox)gvvendor.Rows[e.RowIndex].FindControl("txtEditContactNo");
            TextBox txtEditBillNo = (TextBox)gvvendor.Rows[e.RowIndex].FindControl("txtEditBillNo");


            Domain.Vendor existing = venderrepo.GetById(Convert.ToInt32(lblvendorId.Text));
            if (txtEditVendorName.Text != ("") && txtEditAddress.Text != ("") && txtEditContactNo.Text != ("") && txtEditBillNo.Text != (""))
            {
                if (existing != null)
                {
                    existing.VendorName = txtEditVendorName.Text;
                    existing.Address = txtEditAddress.Text;
                    existing.ContactNo = txtEditContactNo.Text;
                    existing.BillNo = txtEditBillNo.Text;
                    existing.LastUpdatedBy = "admin";
                    existing.LastUpdationTime = DateTime.Now;
                    existing.TenantId = tenantId;
                }


                venderrepo.Edit(existing);
                venderrepo.Save();
                gvvendor.EditIndex = -1;
                BindData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vendor Sucessfully Updated');", true);
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
            //gvvendor.EditIndex = -1;
            //BindData();

        }
        protected void gvvendor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvvendor.EditIndex = -1;
            BindData();
        }
        protected void gvvendor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvvendor.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void gvvendor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvvendor.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}
