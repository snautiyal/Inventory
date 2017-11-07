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
using Microsoft.Reporting.WebForms;
using System.Web.Configuration;

namespace Inventory
{
    public partial class Transaction : System.Web.UI.Page
    {
        InventoryDb db = new InventoryDb();
        ITransactionRepository transactionRepo;
        IProductRepository Prorepo;
        int id = 0;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {
                    Print.Visible = false;
                    transactionRepo = new TransactionRepository(db);

                    if (!IsPostBack)
                    {
                        tbxfrom.Text = DateTime.Today.ToString("yyy.MM.dd");
                        tbxto.Text = DateTime.Today.ToString("yyy.MM.dd");
                        Prorepo = new ProductRepository(db);
                        ddlAddProduct.DataSource = Prorepo.GetAll().Where(p=>p.TenantId==tenantId).ToList();
                        ddlAddProduct.DataTextField = "ProductName";
                        ddlAddProduct.DataValueField = "ProductId";
                        ddlAddProduct.DataBind();
                        ddlAddProduct.Items.Insert(0, new ListItem("-- Select --", string.Empty));
                        
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
                
                transactionRepo = new TransactionRepository(db);
                DateTime sdt = Convert.ToDateTime(tbxfrom.Text);
                DateTime ldt = Convert.ToDateTime(tbxto.Text);
                //var data = transactionRepo.GetAll(orderBy: p => p.OrderByDescending(t => t.CreationTime)).ToList();
                var data = transactionRepo.Find(t => (sdt.Equals(DateTime.MinValue) || t.CreationTime >= sdt) && (ldt.Equals(DateTime.MinValue) || t.CreationTime <= ldt)).Where(t=>t.TenantId==tenantId).ToList();

                if (data.Count == 0)
                {

                    gvTransactionDetails.DataSource = data;
                    gvTransactionDetails.DataBind();
                    lblrecord.Visible = true;
                    lblrecord.Text = "No record found";
                    ReportViewer1.Visible = false;
                    Button1.Visible = false;
                   
                    // gvTransactionDetails.ShowFooter = false;
                }
                else
                {

                    gvTransactionDetails.DataSource = data;
                    gvTransactionDetails.DataBind();
                    ReportViewer1.Visible = false;
                    Button1.Visible = true;
                    lblrecord.Visible = false;
                    // gvTransactionDetails.ShowFooter = true;
                }
                // DropDownList ddlAddProduct = (DropDownList)gvTransactionDetails.FooterRow.FindControl("ddlAddProduct");



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
        private List<TransactionReport_Result> Gettransactionreport(int id, DateTime startDate, DateTime endDate,int tenantId)
        {
            TransactionRepository terpo = new TransactionRepository(new InventoryDb());
            return terpo.GetTransactionreport(id, startDate, endDate, tenantId);



        }

        protected void gvTransactionDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblTransactionId = (Label)gvTransactionDetails.Rows[e.RowIndex].FindControl("lblTransactionId");
                transactionRepo.Delete(Convert.ToInt32(lblTransactionId.Text));
                transactionRepo.Save();

                BindData();
            }
            catch (Exception ex)
            {
                lblmsgs.Visible = true;
                lblmsgs.Text = "This Transaction be use in system";
                Response.Write(ex.Message);

            }
            //conn.Open();
            //string cmdstr = "delete from EmployeeDetails where empid=@empid";
            //SqlCommand cmd = new SqlCommand(cmdstr, conn);
            //cmd.Parameters.AddWithValue("@empid", lblEmpID.Text);
            //cmd.ExecuteNonQuery();
            //conn.Close();


        }
        protected void gvTransactionDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("ADD"))
                {
                    DropDownList ddlAddProduct = (DropDownList)gvTransactionDetails.FooterRow.FindControl("ddlAddProduct");
                    TextBox txtAddComments = (TextBox)gvTransactionDetails.FooterRow.FindControl("txtAddComments");
                    TextBox txtAddCount = (TextBox)gvTransactionDetails.FooterRow.FindControl("txtAddCount");
                    TextBox txtAddTotalAmount = (TextBox)gvTransactionDetails.FooterRow.FindControl("txtAddTotalAmount");




                    Domain.Transaction transaction = new Domain.Transaction()
                     {

                         Comments = txtAddComments.Text,
                         Count = Convert.ToInt32(txtAddCount.Text),
                         ProductId = Convert.ToInt32(ddlAddProduct.SelectedValue),
                         TotalAmount = Convert.ToDecimal(txtAddTotalAmount.Text),
                         CreationTime = DateTime.Now,
                         LastUpdationTime = DateTime.Now,
                         CreatedBy = "admin",
                         LastUpdatedBy = "admin",
                         TenantId=tenantId,
                     };

                    transactionRepo.Add(transaction);
                    transactionRepo.Save();
                    BindData();
                }
            }

            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }




        }
        protected void gvTransactionDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblTransactionId = (Label)gvTransactionDetails.Rows[e.RowIndex].FindControl("lblTransactionId");
            DropDownList ddlEditProduct = (DropDownList)gvTransactionDetails.Rows[e.RowIndex].FindControl("ddlEditProduct");
            TextBox txtEditComments = (TextBox)gvTransactionDetails.Rows[e.RowIndex].FindControl("txtEditComments");
            TextBox txtEditCount = (TextBox)gvTransactionDetails.Rows[e.RowIndex].FindControl("txtEditCount");
            TextBox txtEditTotalAmount = (TextBox)gvTransactionDetails.Rows[e.RowIndex].FindControl("txtEditTotalAmount");

            Domain.Transaction existing = transactionRepo.GetById(Convert.ToInt32(lblTransactionId.Text));
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


            transactionRepo.Edit(existing);
            transactionRepo.Save();
            gvTransactionDetails.EditIndex = -1;
            BindData();
        }



        protected void gvTransactionDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTransactionDetails.EditIndex = -1;
            BindData();
        }
        protected void gvTransactionDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTransactionDetails.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void gvTransactionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            DropDownList ddlProduct = (DropDownList)e.Row.FindControl("ddlEditProduct");
            if (ddlProduct != null)
            {
                Prorepo = new ProductRepository(db);
                ddlProduct.DataSource = Prorepo.GetAll().Where(p=>p.TenantId==tenantId).ToList();
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataBind();

                ddlProduct.SelectedValue = gvTransactionDetails.DataKeys[e.Row.RowIndex].Values[1].ToString();
                Label lblTrans = (Label)e.Row.FindControl("lblProductId");
                if (lblTrans != null)
                    lblTrans.Text = "3";
            }

        }
        protected void gvTransactionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTransactionDetails.PageIndex = e.NewPageIndex;
            gvTransactionDetails.DataBind();
            btnsearch_Click(sender, e);
            // BindData();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (ddlAddProduct.SelectedItem.Text != "-- Select --")
                {
                    ReportViewer1.Visible = false;
                    string ProdId = ddlAddProduct.SelectedValue;


                    string starting = tbxfrom.Text;
                    string ending = tbxto.Text;

                    DateTime sdt = Convert.ToDateTime(starting);
                    DateTime ldt = Convert.ToDateTime(ending);
                    int cout = 0;

                    var date = transactionRepo.Find();
                    var data = transactionRepo.Find(t => t.ProductId.ToString() == ProdId && (sdt.Equals(DateTime.MinValue) || t.CreationTime >= sdt) && (ldt.Equals(DateTime.MinValue) || t.CreationTime <= ldt)).Where(t=>t.TenantId==tenantId);
                    //var data = productRepo.Find(p => p.CategoryId.Value.ToString() == cateId);
                    //&& (!p.BrandId.HasValue || p.BrandId.Value.ToString() == brandId));
                    // var data = productRepo.GetAll(catrepo.GetById).ToList();
                    gvTransactionDetails.DataSource = data.ToList();
                    gvTransactionDetails.DataBind();
                    cout = data.Count();
                    if (cout == 0)
                    {
                        lblrecord.Visible = true;
                        lblrecord.Text = "No record found";
                        Button1.Visible = false;
                       
                    }

                    else
                    {
                       
                        Button1.Visible = true;
                        lblrecord.Visible = false;
                        //BindData();
                    }
                }
                else
                {

                    BindData();
                    
                }
                
            }

            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            if (ddlAddProduct.SelectedItem.Text != "-- Select --")
            {
                id = int.Parse(ddlAddProduct.SelectedValue);

            }

            DateTime startDate = Convert.ToDateTime(tbxfrom.Text);
            DateTime endDate = Convert.ToDateTime(tbxto.Text);
            var dsperiodic = Gettransactionreport(id, startDate, endDate,tenantId);
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Modules/Transaction/TransactionR.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dsperiodic);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            Print.Visible = true;
        

            //ReportViewer1.LocalReport.DataSources.Clear();
        }

        protected void tbxfrom_TextChanged(object sender, EventArgs e)
        {
            Button1.Visible = false;
        }

        protected void tbxto_TextChanged(object sender, EventArgs e)
        {
            Button1.Visible = false;
        }
        //ddlAddProduct.Items.Insert(0, new ListItem("-- Select --", string.Empty));
    }
}