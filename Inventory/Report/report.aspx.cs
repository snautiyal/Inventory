using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using Inventory.Domain;
using Inventory.Repository;
using Domain = Inventory.Domain;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Configuration;

namespace Inventory.Report
{
    public partial class report : System.Web.UI.Page
    {

        InventoryDb db = new InventoryDb();
        IProductRepository productRepo;
        ICategoryRepository catrepo;
        IStockingDetailsRepository stockingrepo;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin"))
                {
                    if (!IsPostBack)
                    {
                        ddlinventory.Items.Add("--Select--");
                        ////ddlinventory.Items.Add("Brand");
                        ////ddlinventory.Items.Add("Category");
                        ////ddlinventory.Items.Add("Item");
                        ////ddlinventory.Items.Add("Product");
                        ddlinventory.Items.Add("Stock");
                        ddlinventory.Items.Add("Stocking");
                        ddlinventory.Items.Add("Consumption");

                        //tbxldate2.Text = "abc";

                    }

                    Print.Visible = false;
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
        public Product GetData(string _frmdate, string _todate)
        {
            string conString = ConfigurationManager.ConnectionStrings["inventoryConnectionString"].ConnectionString;
            DateTime frmdt = Convert.ToDateTime(_frmdate);
            DateTime todt = Convert.ToDateTime(_todate);
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(conString))
            {


                using (SqlDataAdapter sda1 = new SqlDataAdapter())
                {


                    if (ddlinventory.SelectedItem.Value.Equals("Product"))
                    {

                        Print.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Report2.rdlc");
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ProductReport";
                        sda1.SelectCommand = cmd;
                        cmd.Parameters.Add("@fromdatetime", SqlDbType.NVarChar).Value = frmdt;
                        cmd.Parameters.Add("@todatetime", SqlDbType.DateTime).Value = todt;
                        
                    }
                    using (Product dsProduct = new Product())
                    {
                        sda1.Fill(dsProduct, "DataTable2");
                        return dsProduct;
                    }


                }
            }
        }
        private List<StockReport_Result> GetData3(int pid,int tenantId)
        {

            ItemRepository repo = new ItemRepository(new InventoryDb());
            return repo.GetProductStock(pid,tenantId);


        }
        private List<SiCategoryReport_Result> Getsicategory(int id,int tenantId)
        {
            ItemRepository repo = new ItemRepository(new InventoryDb());
            return repo.Getsicategorystock(id,tenantId);

        }
        private List<CategorySReport_Result> GetDatacategory(int cid, int pid,int tenantId)
        {

            ItemRepository repo = new ItemRepository(new InventoryDb());
            return repo.Getscategorystock(cid, pid,tenantId);


        }
        private List<ConsumptionReport_Result> getcounsumption(int productId, DateTime startDate, DateTime endDate,int tenantId)
        {

            ItemRepository repo = new ItemRepository(new InventoryDb());
            return repo.GetcounsumptionReport(productId, startDate, endDate,tenantId);


        }
        private List<StockingReport_Result> GetData5(DateTime startDate, DateTime endDate, int id,int tenantId)
        {
            stockingrepo = new StockingDetailsRepository(db);
            return stockingrepo.GetProductStocking(id, startDate, endDate, tenantId);

        }
        private List<PeriodicReport_Result> GetData4(DateTime startDate, DateTime endDate, int id,int tenantId)
        {
            stockingrepo = new StockingDetailsRepository(db);
            return stockingrepo.GetProductStock(id, startDate, endDate, tenantId); 

        }
        private List<StockingItemsReport_Result> GetData6(DateTime startDate, DateTime endDate, int tenantId)
        {
            stockingrepo = new StockingDetailsRepository(db);
            return stockingrepo.GetProductStockingItem(startDate, endDate, tenantId);

        }
        private Item GetData1(string _frmdate, string _todate)
        {
            DateTime frmdt = Convert.ToDateTime(_frmdate);
            DateTime todt = Convert.ToDateTime(_todate);
            string conString = ConfigurationManager.ConnectionStrings["inventoryConnectionString"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(conString))
            {



                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    if (ddlinventory.SelectedItem.Text.Equals("Item"))
                    {
                        Print.Visible = true;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Report1.rdlc");
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "FetchReport";
                        sda.SelectCommand = cmd;
                        cmd.Parameters.Add("@fromdatetime", SqlDbType.DateTime).Value = frmdt;
                        cmd.Parameters.Add("@todatetime", SqlDbType.DateTime).Value = todt;
                    }
                    using (Item dsitem = new Item())
                    {
                        sda.Fill(dsitem, "DataTable1");
                        return dsitem;
                    }


                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int cid = 0;
            int pid = 0;

            if (!String.IsNullOrEmpty(ddlcategory.SelectedValue)) { cid = Convert.ToInt32(ddlcategory.SelectedValue); }
            if (!String.IsNullOrEmpty(ddlproduct.SelectedValue)) { pid = Convert.ToInt32(ddlproduct.SelectedValue); }


            if (ddlinventory.SelectedItem.Text.Equals("Stock"))
            {
                ddlcategory.Visible = true;
                if (!ddlcategory.SelectedItem.Text.Equals("-- Select --") && (!ddlproduct.SelectedItem.Text.Equals("-- Select --")))
                {

                    Print.Visible = true;
                    ReportViewer1.Visible = true;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    var dscatstock = GetDatacategory(cid, pid, tenantId);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/StockReport.rdlc");
                    ReportDataSource stockSource1 = new ReportDataSource("DataSet2", dscatstock);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(stockSource1);

                }
                else if (!ddlproduct.SelectedItem.Text.Equals("-- Select --") && ddlcategory.SelectedItem.Text.Equals("-- Select --"))
                {
                    ReportViewer1.Visible = true;
                    Print.Visible = true;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    var dstock = GetData3(pid, tenantId);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/StockReport.rdlc");
                    ReportDataSource stockSource = new ReportDataSource("DataSet2", dstock);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(stockSource);

                }
                if (!ddlcategory.SelectedItem.Text.Equals("-- Select --") && (ddlproduct.SelectedItem.Text.Equals("-- Select --")))
                {
                    ReportViewer1.Visible = true;
                    Print.Visible = true;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    var dstock = Getsicategory(cid, tenantId);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/StockReport.rdlc");
                    ReportDataSource stockSource = new ReportDataSource("DataSet2", dstock);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(stockSource);
                }
                if (ddlcategory.SelectedItem.Text.Equals("-- Select --") && (ddlproduct.SelectedItem.Text.Equals("-- Select --")))
                {
                    ReportViewer1.Visible = true;
                    Print.Visible = true;
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    var dstock = GetData3(pid, tenantId);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/StockReport.rdlc");
                    ReportDataSource stockSource = new ReportDataSource("DataSet2", dstock);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(stockSource);



                }
                else
                {

                }


            }

            if (ddlinventory.SelectedItem.Text.Equals("Item"))
            {
                string frmdate1 = tbxfrom.Text;
                string todate1 = tbxto.Text;

                Item dsitem = GetData1(frmdate1, todate1);
                ReportDataSource datasource = new ReportDataSource("Item", dsitem.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            if (ddlinventory.SelectedItem.Text.Equals("Stocking"))
            {

                ReportViewer1.Visible = true;
                ddlcategory.Visible = false;
                DateTime frmdatest = Convert.ToDateTime(tbxfrom.Text);
                DateTime todatest = Convert.ToDateTime(tbxto.Text);
                //string productName = ddlinventory.SelectedItem.Text;
                if (ddlproduct.SelectedItem.Text.Equals("-- Select --"))
                {
                    Print.Visible = true;
                    var dsstocking = GetData6(frmdatest, todatest,tenantId);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/StockingItems.rdlc");
                    ReportDataSource datasource = new ReportDataSource("DataSet1", dsstocking);


                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);

                }
                else
                {
                    Print.Visible = true;
                    int id = int.Parse(ddlproduct.SelectedValue);
                    var dsperiodic = GetData5(frmdatest, todatest, id, tenantId);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Stocking.rdlc");
                    ReportDataSource datasource = new ReportDataSource("DataSet1", dsperiodic);


                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                }
            }
            if (ddlinventory.SelectedItem.Text.Equals("Consumption"))
            {

                ReportViewer1.Visible = true;
                ddlcategory.Visible = false;
                DateTime frmdatest = Convert.ToDateTime(tbxfrom.Text);
                DateTime todatest = Convert.ToDateTime(tbxto.Text);
                //string productName = ddlinventory.SelectedItem.Text;
                if (ddlproduct.SelectedItem.Text.Equals("-- Select --"))
                {
                    Print.Visible = true;
                    var dsconsumption = getcounsumption(0, frmdatest, todatest, tenantId);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Consumption.rdlc");
                    ReportDataSource datasource = new ReportDataSource("DataSet1", dsconsumption);


                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);

                }
                else
                {
                    Print.Visible = true;
                    int id = int.Parse(ddlproduct.SelectedValue);
                    var dsconsumption = getcounsumption(id, frmdatest, todatest, tenantId);
                    if (dsconsumption.Count > 0)
                    {
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Consumption.rdlc");
                        ReportDataSource datasource = new ReportDataSource("DataSet1", dsconsumption);


                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(datasource);
                    }
                    else
                    {
                        ReportViewer1.Visible = false;
                        //lblrecord.Text = "No record found";

                    }
                }
            }
            //if (ddlinventory.SelectedItem.Text.Equals("Brand"))
            //{

            //    Brand dsbrand = GetDatabrand(frmdate, todate);
            //    //ReportDataSource datasource = new ReportDataSource("Brand", dsbrand.Tables[0]);
            //    ReportViewer1.LocalReport.DataSources.Clear();
            //    ReportViewer1.LocalReport.DataSources.Add(datasource);
            //}

            if (ddlinventory.SelectedItem.Text.Equals("Product"))
            {
                string frmdate1 = tbxfrom.Text;
                string todate1 = tbxto.Text;
                Product dsproduct = GetData(frmdate1, todate1);
                ReportDataSource datasource = new ReportDataSource("Product", dsproduct.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }



        }

        protected void ddlinventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportViewer1.Visible = false;

            if (ddlinventory.SelectedItem.Text.Equals("--Select--"))
            {
                fromdate.Visible = false;
                todate.Visible = false;
                lblfrom.Visible = false;
                lblto.Visible = false;
                lblproduct.Visible = false;
                ddlproduct.Visible = false;

            }
            if (ddlinventory.SelectedItem.Text.Equals("Stock"))
            {
                fromdate.Visible = false;
                todate.Visible = false;
                lblfrom.Visible = false;
                lblto.Visible = false;
                lblproduct.Visible = true;
                lblCategory.Visible = true;

                catrepo = new CategoryRepository(db);
                var cdata = catrepo.GetAll().Where(c=>c.TenantId==tenantId);
                ddlcategory.DataSource = cdata.ToList();
                ddlcategory.DataTextField = "CategoryName";
                ddlcategory.DataValueField = "CategoryId";
                ddlcategory.DataBind();
                ddlcategory.Items.Insert(0, new ListItem("-- Select --", string.Empty));
                ddlcategory.Visible = true;

                productRepo = new ProductRepository(db);
                var data = productRepo.GetAll().Where(p=>p.ProductId== tenantId);
                ddlproduct.DataSource = data.ToList();
                ddlproduct.DataTextField = "ProductName";
                ddlproduct.DataValueField = "ProductId";
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, new ListItem("-- Select --", string.Empty));
                ddlproduct.Visible = true;

            }
            if (ddlinventory.SelectedItem.Text.Equals("Stocking"))
            {
                fromdate.Visible = true;
                todate.Visible = true;
                lblfrom.Visible = true;
                lblto.Visible = true;
                lblproduct.Visible = true;
                lblCategory.Visible = false;
                ddlcategory.Visible = false;
                tbxfrom.Text = DateTime.Now.ToString("yyyy.MM.dd");
                tbxto.Text = DateTime.Now.ToString("yyyy.MM.dd");

                productRepo = new ProductRepository(db);
                var data = productRepo.GetAll().Where(p=>p.TenantId==tenantId);
                ddlproduct.DataSource = data.ToList();
                ddlproduct.DataTextField = "ProductName";
                ddlproduct.DataValueField = "ProductId";
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, new ListItem("-- Select --", string.Empty));
                ddlproduct.Visible = true;

            }
            if (ddlinventory.SelectedItem.Text.Equals("Consumption"))
            {
                fromdate.Visible = true;
                todate.Visible = true;
                lblfrom.Visible = true;
                lblto.Visible = true;
                lblproduct.Visible = true;
                lblCategory.Visible = false;
                ddlcategory.Visible = false;
                tbxfrom.Text = DateTime.Now.ToString("yyyy.MM.dd");
                tbxto.Text = DateTime.Now.ToString("yyyy.MM.dd");

                productRepo = new ProductRepository(db);
                var data = productRepo.GetAll().Where(p => p.TenantId == tenantId);
                ddlproduct.DataSource = data.ToList();
                ddlproduct.DataTextField = "ProductName";
                ddlproduct.DataValueField = "ProductId";
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, new ListItem("-- Select --", string.Empty));
                ddlproduct.Visible = true;

            }

        }

        protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportViewer1.Visible = false;
            if (!ddlproduct.SelectedItem.Text.Equals("-- Select --"))
            {



            }
            else
            {


            }
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportViewer1.Visible = false;
            productRepo = new ProductRepository(db);
            if (!ddlcategory.SelectedItem.Text.Equals("-- Select --"))
            {


                int value = int.Parse(ddlcategory.SelectedValue);
                string cateId = value.ToString();
                var data = productRepo.Find(p => p.CategoryId.Value.ToString() == cateId && p.TenantId==tenantId);
                ddlproduct.DataSource = data.ToList();
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, new ListItem("-- Select --", string.Empty));
                // IEnumerable<Product> products = productRepo.Find(i => i.CategoryId == value);

            }
            else
            {

                productRepo = new ProductRepository(db);
                var data = productRepo.GetAll().Where(p => p.TenantId == tenantId);
                ddlproduct.DataSource = data.ToList();
                ddlproduct.DataTextField = "ProductName";
                ddlproduct.DataValueField = "ProductId";
                ddlproduct.DataBind();
                ddlproduct.Items.Insert(0, new ListItem("-- Select --", string.Empty));
                ddlproduct.Visible = true;
            }

        }





    }
}
////SqlCommand cmd = new SqlCommand();
//               //cmd.CommandType = CommandType.StoredProcedure;
//               //cmd.CommandText = "sp_AddGuest";
//               ReportViewer1.ProcessingMode = ProcessingMode.Local;
//               ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Report/Report.rdlc");
//               Item dsitem = GetData(tbxldate2.Text);
//               //SqlCommand c);
//               //Item dsitem = GetData(dsitem.);
//               ReportDataSource datasource = new ReportDataSource("Item", dsitem.Tables[0]);
//               ReportViewer1.LocalReport.DataSources.Clear();
//               ReportViewer1.LocalReport.DataSources.Add(datasource);
//           }
//       }

//       private Item GetData(string query)
//       {
//           string conString = "Data Source=192.168.10.12;Initial Catalog=inventory; User ID=sa; password=sa@123";
//           SqlCommand cmd = new SqlCommand(query);
//           using (SqlConnection con = new SqlConnection(conString))
//           {
//               using (SqlDataAdapter sda = new SqlDataAdapter())
//               {
//                   cmd.Connection = con;
//                   cmd.CommandType = CommandType.StoredProcedure;
//                   cmd.CommandText = "sp_AddGuest";
//                   sda.SelectCommand = cmd;
//                   using (Item dsitem = new Item())
//                   {
//                       sda.Fill(dsitem, "DataTable1");
//                       return dsitem;
//                   }
//               }
//           }
//       }