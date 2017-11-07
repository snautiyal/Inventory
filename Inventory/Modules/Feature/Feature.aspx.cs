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
using System.Data;



namespace Inventory
{
    public partial class Feature : System.Web.UI.Page
    {

        InventoryDb db = new InventoryDb();
        IFeatureRepository featureRepo;
        //IBrandRepository repo = new BrandRepository(new InventoryDb());
        //IBrandRepository repo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {
                    featureRepo = new FeatureRepository(db);
                    if (!IsPostBack)
                    {
                        BindData();
                        //if (gvFeatureDetails.ro)
                        //{
                        //    gvFeatureDetails.DataSource = featureRepo.GetAll().ToList();
                        //    gvFeatureDetails.DataBind();
                        //}
                        //else
                        //{
                        //    //Empty DataTable to execute the “else-condition”  
                        //    DataTable dt = new Datatable();
                        //    gvFeatureDetails.DataSource = dt;
                        //    gvFeatureDetails.DataBind();
                        //}
                        //DataTable FromTable = new DataTable();

                        ////if (gvFeatureDetails.Rows.Count > 0)
                        //{
                        //    gvFeatureDetails.DataSource = featureRepo.GetAll().ToList(); 
                        //    gvFeatureDetails.DataBind();
                        //}
                        //else
                        //{
                        //    FromTable.Rows.Add(FromTable.NewRow());
                        //    gvFeatureDetails.DataSource = FromTable;
                        //    gvFeatureDetails.DataBind();
                        //    int TotalColumns = gvFeatureDetails.Rows[0].Cells.Count;
                        //    gvFeatureDetails.Rows[0].Cells.Clear();
                        //    gvFeatureDetails.Rows[0].Cells.Add(new TableCell());
                        //    gvFeatureDetails.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                        //    gvFeatureDetails.Rows[0].Cells[0].Text = "No records Found";
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
                featureRepo = new FeatureRepository(db);
                var data = featureRepo.GetAll().OrderByDescending(o => o.FeatureId).ToList();
                if (data.Count == 0)
                {
                    data.Add(new Domain.Feature() { FeatureId = 0, FeatureName = "", FeatureDescription = "" });
                    gvFeatureDetails.DataSource = data;
                    gvFeatureDetails.DataBind();
                    gvFeatureDetails.Rows[0].Visible = false;
                    gvFeatureDetails.ShowFooter = true;
                }
                else
                {
                    gvFeatureDetails.DataSource = data;
                    gvFeatureDetails.DataBind();
                }
                ////conn.Open();
                //string cmdstr = "Select * from EmployeeDetails";
                //SqlCommand cmd = new SqlCommand(cmdstr, conn);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //adp.Fill(ds);
                //cmd.ExecuteNonQuery();
                //FromTable = ds.Tables[0];
                //if (FromTable.Rows.Count > 0)
                //{
                //    gvFeatureDetails.DataSource = FromTable;
                //    gvFeatureDetails.DataBind();
                //}
                //else
                //{
                //    FromTable.Rows.Add(FromTable.NewRow());
                //    gvFeatureDetails.DataSource = FromTable;
                //    gvFeatureDetails.DataBind();
                //    int TotalColumns = gvFeatureDetails.Rows[0].Cells.Count;
                //    gvFeatureDetails.Rows[0].Cells.Clear();
                //    gvFeatureDetails.Rows[0].Cells.Add(new TableCell());
                //    gvFeatureDetails.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                //    gvFeatureDetails.Rows[0].Cells[0].Text = "No records Found";
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

        protected void gvFeatureDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblFeatureID = (Label)gvFeatureDetails.Rows[e.RowIndex].FindControl("lblFeatureID");
                featureRepo.Delete(Convert.ToInt32(lblFeatureID.Text));
                featureRepo.Save();

                BindData();
            }
            catch (Exception ex)
            {
                lblmsgs.Visible = true;
                lblmsgs.Text = "This Feature be use in system";
                Response.Write(ex.Message);

            }
            //conn.Open();
            //string cmdstr = "delete from EmployeeDetails where empid=@empid";
            //SqlCommand cmd = new SqlCommand(cmdstr, conn);
            //cmd.Parameters.AddWithValue("@empid", lblEmpID.Text);
            //cmd.ExecuteNonQuery();
            //conn.Close();


        }
        protected void gvFeatureDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName.Equals("ADD"))
                {

                    TextBox txtAddFeatureName = (TextBox)gvFeatureDetails.FooterRow.FindControl("txtAddFeatureName");
                    TextBox txtAddDescription = (TextBox)gvFeatureDetails.FooterRow.FindControl("txtAddDescription");


                    Domain.Feature feature = new Domain.Feature()
                    {

                        FeatureDescription = txtAddDescription.Text,
                        FeatureName = txtAddFeatureName.Text,
                        CreationTime = DateTime.Now,
                        LastUpdationTime = DateTime.Now,
                        CreatedBy = "admin",
                        LastUpdatedBy = "admin",
                    };

                    featureRepo.Add(feature);
                    featureRepo.Save();
                    BindData();
                }
            }

            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }



        }
        protected void gvFeatureDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblFeatureId = (Label)gvFeatureDetails.Rows[e.RowIndex].FindControl("lblFeatureId");
            TextBox txtEditFeatureName = (TextBox)gvFeatureDetails.Rows[e.RowIndex].FindControl("txtEditFeatureName");
            TextBox txtEditDescription = (TextBox)gvFeatureDetails.Rows[e.RowIndex].FindControl("txtEditDescription");

            Domain.Feature existing = featureRepo.GetById(Convert.ToInt32(lblFeatureId.Text));
            if (existing != null)
            {
                existing.FeatureName = txtEditFeatureName.Text;
                existing.FeatureDescription = txtEditDescription.Text;
                existing.LastUpdatedBy = "admin";
                existing.LastUpdationTime = DateTime.Now;
            }


            featureRepo.Edit(existing);
            featureRepo.Save();
            gvFeatureDetails.EditIndex = -1;
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
            //gvFeatureDetails.EditIndex = -1;
            //BindData();

        }
        protected void gvFeatureDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFeatureDetails.EditIndex = -1;
            BindData();
        }
        protected void gvFeatureDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFeatureDetails.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            BindData();
            gvFeatureDetails.ShowFooter = true;
            gvFeatureDetails.ShowHeader = true;
        }
    }
}
