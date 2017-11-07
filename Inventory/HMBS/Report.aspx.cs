using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Microsoft.Reporting.WebForms;

namespace Inventory.HMBS
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlinventory.Items.Add("--Select--");
                ddlinventory.Items.Add("Ratio");

                //tbxldate2.Text = "abc";

            }

        }
        public Status GetData()
        {
            string conString = "Data Source=192.168.10.12;Initial Catalog=HMBS; User ID=sa; password=sa@123";

            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(conString))
            {


                using (SqlDataAdapter sda1 = new SqlDataAdapter())
                {

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_statuscategories";
                    sda1.SelectCommand = cmd;
                    cmd.Parameters.Add("@categoryId", SqlDbType.NVarChar).Value = "10";


                    using (Status st = new Status())
                    {
                        sda1.Fill(st, "DataTable1");
                        return st;
                    }


                }
            }
        }

        //public Ratio  GetDataratio()
        //{
        //    string conString = "Data Source=192.168.10.12;Initial Catalog=HMBS; User ID=sa; password=sa@123";

        //    SqlCommand cmd = new SqlCommand();
        //    using (SqlConnection con = new SqlConnection(conString))
        //    {


        //        using (SqlDataAdapter sda1 = new SqlDataAdapter())
        //        {

        //            cmd.Connection = con;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "ratio";
        //            sda1.SelectCommand = cmd;
        //            cmd.Parameters.Add("@fromdatetime", SqlDbType.DateTime).Value = frmdt;
        //            cmd.Parameters.Add("@todatetime", SqlDbType.DateTime).Value = todt;


        //            using (Ratio rt = new Ratio())
        //            {
        //                sda1.Fill(rt, "DataTable1");
        //                return rt;
        //            }


        //        }
        //    }
        //}
        protected void fetchreport_Click(object sender, EventArgs e)
        {
            if (ddlinventory.SelectedItem.Text.Equals("--Select--"))
            {

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                var dstatus = GetData();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("/HMBS/Report1.rdlc");
                ReportDataSource stockSource = new ReportDataSource("DataSet1", dstatus.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(stockSource);
            }
            if (ddlinventory.SelectedItem.Text.Equals("Ratio"))
            {
                try
                {

                    DateTime frmdate = Convert.ToDateTime(tbxfrom.Text);
                    DateTime todate = Convert.ToDateTime(tbxto.Text);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    var dratio = GetDataratio(frmdate, todate);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("/HMBS/Ratio.rdlc");
                    ReportDataSource ratioSource = new ReportDataSource("DataSet1", dratio.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(ratioSource);
                }
                catch (Exception ex)

                {

                    HttpContext.Current.Response.Write(ex.ToString());
                
                }

            }
        }

        public Ratio GetDataratio(DateTime frmdate, DateTime todate)
        {
            string conString = "Data Source=192.168.10.12;Initial Catalog=HMBS; User ID=sa; password=sa@123";

            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(conString))
            {


                using (SqlDataAdapter sda1 = new SqlDataAdapter())
                {

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ratio";
                    sda1.SelectCommand = cmd;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = frmdate;
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = todate;


                    using (Ratio rt = new Ratio())
                    {
                        sda1.Fill(rt, "DataTable1");
                        return rt;
                    }


                }
            }
        }
    }
}