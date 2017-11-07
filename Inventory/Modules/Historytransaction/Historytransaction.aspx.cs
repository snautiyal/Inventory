using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain = Inventory.Domain;
using System.Data.Entity;
using Inventory.Repository;
using Inventory.Domain;
using System.Web.Configuration;

namespace Inventory
{
    public partial class Historytransaction : System.Web.UI.Page
    {
        InventoryDb db = new InventoryDb();
        IHistoryTrasactionRepository historytransactionRepo;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);
        //IItemRepository repo = new ItemRepository(new InventoryDb());
        //IItemRepository repo;
        IProductRepository Prorepo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {
                    historytransactionRepo = new HistoryTrasactionRepository(db);
                    if (!IsPostBack)
                    {
                        BindData();

                        //gvHistoryTransactionDetails.DataSource = historytransactionRepo.GetAll().ToList();
                        //gvHistoryTransactionDetails.DataBind();
                        //ddlbrandid.DataSource = historytransactionRepo.GetAll().ToList();
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
                historytransactionRepo = new HistoryTrasactionRepository(db);
                var data = historytransactionRepo.GetAll().Where(h=>h.TenantId==tenantId).ToList();
                if (data.Count == 0)
                {
                    data.Add(new Domain.Historytransaction() { HistoryTransactionId = 0, ProductId = 0, Count = 0, TotalAmount = 0 });
                    gvHistoryTransactionDetails.DataSource = data;
                    gvHistoryTransactionDetails.DataBind();
                    gvHistoryTransactionDetails.Rows[0].Visible = false;
                    gvHistoryTransactionDetails.ShowFooter = true;
                }
                else
                {
                    gvHistoryTransactionDetails.DataSource = data;
                    gvHistoryTransactionDetails.DataBind();
                }
                DropDownList ddlAddProduct = (DropDownList)gvHistoryTransactionDetails.FooterRow.FindControl("ddlAddProduct");

                Prorepo = new ProductRepository(db);
                ddlAddProduct.DataSource = Prorepo.GetAll().Where(p=>p.TenantId==tenantId).ToList();
                ddlAddProduct.DataTextField = "ProductName";
                ddlAddProduct.DataValueField = "ProductId";
                ddlAddProduct.DataBind();
                ////conn.Open();
                //string cmdstr = "Select * from EmployeeDetails";
                //SqlCommand cmd = new SqlCommand(cmdstr, conn);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //adp.Fill(ds);
                //cmd.ExecuteNonQuery();
                //FromTable = ds.Tables[0];
                //if (FromTable.Rows.Count > 0)
                //{
                //    gvHistoryTransactionDetails.DataSource = FromTable;
                //    gvHistoryTransactionDetails.DataBind();
                //}
                //else
                //{
                //    FromTable.Rows.Add(FromTable.NewRow());
                //    gvHistoryTransactionDetails.DataSource = FromTable;
                //    gvHistoryTransactionDetails.DataBind();
                //    int TotalColumns = gvHistoryTransactionDetails.Rows[0].Cells.Count;
                //    gvHistoryTransactionDetails.Rows[0].Cells.Clear();
                //    gvHistoryTransactionDetails.Rows[0].Cells.Add(new TableCell());
                //    gvHistoryTransactionDetails.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                //    gvHistoryTransactionDetails.Rows[0].Cells[0].Text = "No records Found";
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

        protected void gvHistoryTransactionDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblHistoryTransactionId = (Label)gvHistoryTransactionDetails.Rows[e.RowIndex].FindControl("lblHistoryTransactionId");
                historytransactionRepo.Delete(Convert.ToInt32(lblHistoryTransactionId.Text));
                historytransactionRepo.Save();

                BindData();
            }
            catch (Exception ex)
            {
                lblmsgs.Visible = true;
                lblmsgs.Text = "This Brand be use in system";
                Response.Write(ex.Message);

            }
            //conn.Open();
            //string cmdstr = "delete from EmployeeDetails where empid=@empid";
            //SqlCommand cmd = new SqlCommand(cmdstr, conn);
            //cmd.Parameters.AddWithValue("@empid", lblEmpID.Text);
            //cmd.ExecuteNonQuery();
            //conn.Close();


        }
        protected void gvHistoryTransactionDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("ADD"))
                {
                    DropDownList ddlAddProduct = (DropDownList)gvHistoryTransactionDetails.FooterRow.FindControl("ddlAddProduct");
                    TextBox txtAddComments = (TextBox)gvHistoryTransactionDetails.FooterRow.FindControl("txtAddComments");
                    TextBox txtAddCount = (TextBox)gvHistoryTransactionDetails.FooterRow.FindControl("txtAddCount");
                    TextBox txtAddTotalAmount = (TextBox)gvHistoryTransactionDetails.FooterRow.FindControl("txtAddTotalAmount");




                    Domain.Historytransaction historytransaction = new Domain.Historytransaction()
                    {
                        ProductId = Convert.ToInt32(ddlAddProduct.SelectedValue),
                        Comments = txtAddComments.Text,
                        Count = Convert.ToInt32(txtAddCount.Text),
                        TotalAmount = Convert.ToInt32(txtAddTotalAmount.Text),
                        CreationTime = DateTime.Now,
                        LastUpdationTime = DateTime.Now,
                        CreatedBy = "admin",
                        LastUpdatedBy = "admin",
                        TenantId=tenantId,
                    };

                    historytransactionRepo.Add(historytransaction);
                    historytransactionRepo.Save();
                    BindData();
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }

        }
        protected void gvHistoryTransactionDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblHistoryTransactionId = (Label)gvHistoryTransactionDetails.Rows[e.RowIndex].FindControl("lblHistoryTransactionId");
            DropDownList ddlEditProduct = (DropDownList)gvHistoryTransactionDetails.Rows[e.RowIndex].FindControl("ddlEditProduct");
            TextBox txtEditComments = (TextBox)gvHistoryTransactionDetails.Rows[e.RowIndex].FindControl("txtEditComments");
            TextBox txtEditCount = (TextBox)gvHistoryTransactionDetails.Rows[e.RowIndex].FindControl("txtEditCount");
            TextBox txtEditTotalAmount = (TextBox)gvHistoryTransactionDetails.Rows[e.RowIndex].FindControl("txtEditTotalAmount");

            Domain.Historytransaction existing = historytransactionRepo.GetById(Convert.ToInt32(lblHistoryTransactionId.Text));
            if (existing != null)
            {
                existing.ProductId = Convert.ToInt32(ddlEditProduct.SelectedValue);
                existing.Comments = txtEditComments.Text;
                existing.Count = Convert.ToInt32(txtEditCount.Text);
                existing.TotalAmount = Convert.ToDecimal(txtEditTotalAmount.Text);
                existing.LastUpdatedBy = "admin";
                existing.LastUpdationTime = DateTime.Now;
                existing.TenantId = tenantId;
            }


            historytransactionRepo.Edit(existing);
            historytransactionRepo.Save();
            gvHistoryTransactionDetails.EditIndex = -1;
            BindData();

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
            //gvHistoryTransactionDetails.EditIndex = -1;
            //BindData();

        }
        protected void gvHistoryTransactionDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvHistoryTransactionDetails.EditIndex = -1;
            BindData();
        }
        protected void gvHistoryTransactionDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvHistoryTransactionDetails.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void gvHistoryTransactionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlProduct = (DropDownList)e.Row.FindControl("ddlEditProduct");
            if (ddlProduct != null)
            {
                Prorepo = new ProductRepository(db);
                ddlProduct.DataSource = Prorepo.GetAll().ToList();
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataBind();

                ddlProduct.SelectedValue = gvHistoryTransactionDetails.DataKeys[e.Row.RowIndex].Values[1].ToString();
                Label lblTrans = (Label)e.Row.FindControl("lblProductId");
                if (lblTrans != null)
                    lblTrans.Text = "3";
            }
        }
    }
}
