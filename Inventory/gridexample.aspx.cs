using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
//using Service;

namespace Inventory
{
    SqlConnection conn = new SqlConnection("Data Source=localhost;Database=Demo;Integrated Security=True");
    public partial class gridexample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void BindData()
        {
            DataSet ds = new DataSet();
            DataTable FromTable = new DataTable();
            try
            {
                conn.Open();
                string cmdstr = "Select * from EmployeeDetails";
                SqlCommand cmd = new SqlCommand(cmdstr, conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.ExecuteNonQuery();
                FromTable = ds.Tables[0];
                if (FromTable.Rows.Count > 0)
                {
                    gvEmployeeDetails.DataSource = FromTable;
                    gvEmployeeDetails.DataBind();
                }
                else
                {
                    FromTable.Rows.Add(FromTable.NewRow());
                    gvEmployeeDetails.DataSource = FromTable;
                    gvEmployeeDetails.DataBind();
                    int TotalColumns = gvEmployeeDetails.Rows[0].Cells.Count;
                    gvEmployeeDetails.Rows[0].Cells.Clear();
                    gvEmployeeDetails.Rows[0].Cells.Add(new TableCell());
                    gvEmployeeDetails.Rows[0].Cells[0].ColumnSpan = TotalColumns;
                    gvEmployeeDetails.Rows[0].Cells[0].Text = "No records Found";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                ds.Dispose();
                conn.Close();
            }
        }

        protected void gvEmployeeDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblEmpID = (Label)gvEmployeeDetails.Rows[e.RowIndex].FindControl("lblEmpID");

            conn.Open();
            string cmdstr = "delete from EmployeeDetails where empid=@empid";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@empid", lblEmpID.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            BindData();

        }
        protected void gvEmployeeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("ADD"))
            {
                TextBox txtAddEmpID = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddEmpID");
                TextBox txtAddName = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddName");
                TextBox txtAddDesignation = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddDesignation");
                TextBox txtAddCity = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddCity");
                TextBox txtAddCountry = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddCountry");


                conn.Open();
                string cmdstr = "insert into EmployeeDetails(empid,name,designation,city,country) values(@empid,@name,@designation,@city,@country)";
                SqlCommand cmd = new SqlCommand(cmdstr, conn);
                cmd.Parameters.AddWithValue("@empid", txtAddEmpID.Text);
                cmd.Parameters.AddWithValue("@name", txtAddName.Text);
                cmd.Parameters.AddWithValue("@designation", txtAddDesignation.Text);
                cmd.Parameters.AddWithValue("@city", txtAddCity.Text);
                cmd.Parameters.AddWithValue("@country", txtAddCountry.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                BindData();
            }
        }
        protected void gvEmployeeDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblEditEmpID = (Label)gvEmployeeDetails.Rows[e.RowIndex].FindControl("lblEditEmpID");
            TextBox txtEditName = (TextBox)gvEmployeeDetails.Rows[e.RowIndex].FindControl("txtEditName");
            TextBox txtEditDesignation = (TextBox)gvEmployeeDetails.Rows[e.RowIndex].FindControl("txtEditDesignation");
            TextBox txtEditCity = (TextBox)gvEmployeeDetails.Rows[e.RowIndex].FindControl("txtEditCity");
            TextBox txtEditCountry = (TextBox)gvEmployeeDetails.Rows[e.RowIndex].FindControl("txtEditCountry");

            conn.Open();
            string cmdstr = "update EmployeeDetails set name=@name,designation=@designation,city=@city,country=@country where empid=@empid";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@empid", lblEditEmpID.Text);
            cmd.Parameters.AddWithValue("@name", txtEditName.Text);
            cmd.Parameters.AddWithValue("@designation", txtEditDesignation.Text);
            cmd.Parameters.AddWithValue("@city", txtEditCity.Text);
            cmd.Parameters.AddWithValue("@country", txtEditCountry.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvEmployeeDetails.EditIndex = -1;
            BindData();

        }
        protected void gvEmployeeDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployeeDetails.EditIndex = -1;
            BindData();
        }
        protected void gvEmployeeDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployeeDetails.EditIndex = e.NewEditIndex;
            BindData();
        }
    }
}