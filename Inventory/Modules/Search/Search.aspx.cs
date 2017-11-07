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
using System.Web.Services;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Configuration;

namespace Inventory.Modules.Search
{
    public partial class Search : System.Web.UI.Page
    {
        InventoryDb db = new InventoryDb();
        IProductRepository productRepo;
        ItemRepository itemrepo;
        //IBrandRepository repo = new BrandRepository(new InventoryDb());
        IBrandRepository brandrepo;
        ICategoryRepository catrepo;
        ITransactionRepository transrepo;
        string comment;
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        string login;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);
        private Stream CreateStream(string name,
      string fileNameExtension, Encoding encoding,
      string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        protected void Page_Load(object sender, EventArgs e)
        
        {
            if (Session["id"] != null)
            {
                if (Session["id"].Equals("Admin") || Session["id"].Equals("Editor"))
                {
                    transrepo = new TransactionRepository(db);
                    productRepo = new ProductRepository(db);
                    itemrepo = new ItemRepository(db);
                    brandrepo = new BrandRepository(db);
                    catrepo = new CategoryRepository(db);

                    // GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;

                    //DropDownList ddlCategorys = (DropDownList)row.FindControl("ddlCategorys");


                    if (!IsPostBack)
                    {

                        BindData();

                    }
                }
                else
                {

                    imgbtnCheckout.Visible = false;
                    btnchckserver.Visible = false;
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
            //string name = Request.Form["Name"];
            ////int id=Context.GetType(id).Name;

            //string cateId = Request.Form["Name"];
            //string brandId = ddlBrand.SelectedValue;

            //var data = itemrepo.Find(i => i.Product.CategoryId.Value.ToString() == cateId && (string.IsNullOrEmpty(brandId) || i.Product.BrandId.Value.ToString() == brandId));
            //gvProductDetails.DataSource = data.ToList();
            //gvProductDetails.DataBind();
            //ddlCategory.Items.Insert(0, new ListItem("-- Select --", string.Empty));
            //var data = productRepo.Find(p => p.CategoryId.Value.ToString() == cateId);
            //&& (!p.BrandId.HasValue || p.BrandId.Value.ToString() == brandId));
            // var data = productRepo.GetAll(catrepo.GetById).ToList();

        }



        protected void BindData()
        {

            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[12] { 
                            new DataColumn("chk row", typeof(string)) ,
                            new DataColumn("ProductId", typeof(string)) ,
                             new DataColumn("Bar Code",typeof(string)) ,
                            new DataColumn("Product Name",typeof(string)) ,
                            new DataColumn("Product description", typeof(string)),
                            new DataColumn("Product Weight", typeof(string)),
                            new DataColumn("Product Price", typeof(string)),
                            new DataColumn("StocK Value", typeof(string)),
                            new DataColumn("Quantity", typeof(string)),
                            new DataColumn("Comment", typeof(string)),
                             new DataColumn("ThreshHold", typeof(string)),
                            new DataColumn("Stock in", typeof(string)) });


                //t.Rows.Add("Mudassar Khan", "India");
                dt.Rows.Add("", "", "", "", "", "", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                ViewState["CurrentTable"] = dt;
                gvProductDetails.DataSource = dt;
                gvProductDetails.DataBind();
                //catrepo = new CategoryRepository(db);
                // GridViewRow gvRow = gvProductDetails.Rows[index];



                //productRepo = new ProductRepository(db);




                /////////////Bind category list///////////////////



                //catrepo = new CategoryRepository(db);
                //ddlCategory.DataSource = catrepo.GetAll().ToList();
                //ddlCategory.DataTextField = "CategoryName";
                //ddlCategory.DataValueField = "CategoryId";
                //ddlCategory.DataBind();
                //ddlCategory.Items.Insert(0, new ListItem("-- Select --", string.Empty));
                /////////Bind brand data list

                //brandrepo = new BrandRepository(db);
                //ddlBrand.DataSource = brandrepo.GetAll().ToList();
                //ddlBrand.DataTextField = "BrandName";
                //ddlBrand.DataValueField = "BrandId";
                //ddlBrand.DataBind();
                //ddlBrand.Items.Insert(0, new ListItem("-- Select --", string.Empty));




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

        private void AddNewRowToGrid()
        {

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                //dtCurrentTable.Rows.Add(drCurrentRow);
                //dtCurrentTable.NewRow();
                dtCurrentTable.Rows.Add(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                //dtCurrentTable.NewRow("1", "!", "2", "4", string.Empty, string.Empty, string.Empty, string.Empty);


                ViewState["CurrentTable"] = dtCurrentTable;
                //BindData();


                gvProductDetails.DataSource = dtCurrentTable;
                gvProductDetails.DataBind();



            }


            //Set Previous Data on Postbacks

            //SetPreviousData();

        }
        //private void SetPreviousData()
        //{

        //    int rowIndex = 0;

        //    if (ViewState["CurrentTable"] != null)
        //    {

        //        DataTable dt = (DataTable)ViewState["CurrentTable"];

        //        if (dt.Rows.Count > 0)
        //        {

        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {



        //                TextBox box1 = (TextBox)gvProductDetails.Rows[rowIndex].Cells[1].FindControl("txtSearch");

        //                TextBox box2 = (TextBox)gvProductDetails.Rows[rowIndex].Cells[2].FindControl("txtdescription");

        //                TextBox box3 = (TextBox)gvProductDetails.Rows[rowIndex].Cells[3].FindControl("txtweight");

        //                TextBox box4 = (TextBox)gvProductDetails.Rows[rowIndex].Cells[4].FindControl("txtprice");

        //                TextBox box5 = (TextBox)gvProductDetails.Rows[rowIndex].Cells[5].FindControl("txtstock");

        //                TextBox box6 = (TextBox)gvProductDetails.Rows[rowIndex].Cells[6].FindControl("Quantity");

        //                TextBox box7 = (TextBox)gvProductDetails.Rows[rowIndex].Cells[7].FindControl("Comments");


        //                //box1.Text = "";

        //                box2.Text = dt.Rows[i]["Product description"].ToString();

        //                box3.Text = dt.Rows[i]["Product Weight"].ToString();

        //                box4.Text = dt.Rows[i]["Product Price"].ToString();

        //                box5.Text = dt.Rows[i]["StocK Value"].ToString();
        //                //box6.Text = "";

        //                // box7.Text = "";



        //                rowIndex++;

        //            }

        //        }

        //    }

        //}
        protected void gvProductDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label lblProductID = (Label)gvProductDetails.Rows[e.RowIndex].FindControl("lblProductID");
                productRepo.Delete(Convert.ToInt32(lblProductID.Text));
                productRepo.Save();

                BindData();
            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }



        }



        protected void gvProductDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }

        public void update(string _lblProductId, string _Count)
        {
            int remain;
            int productid = !String.IsNullOrEmpty(_lblProductId) ? Convert.ToInt32(_lblProductId) : 0;
            //Domain.Item existing = new Domain.Item();
            Domain.Item existing = itemrepo.Find(p => p.ProductId == productid).FirstOrDefault();
            int value = existing.Count;
            remain = (value) - (Convert.ToInt32(_Count));
            existing.Count = remain;
            existing.LastUpdatedBy = "admin";
            existing.LastUpdationTime = DateTime.Now;
            itemrepo.Edit(existing);

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
            DropDownList ddlCategorys = (DropDownList)e.Row.FindControl("ddlCategorys");
            DropDownList ddlBrands = (DropDownList)e.Row.FindControl("ddlBrands");
            //var data = itemrepo.GetAll().ToList();

            if (ddlCategorys != null)
            {
                catrepo = new CategoryRepository(db);
                ddlCategorys.DataSource = catrepo.GetAll().ToList();
                ddlCategorys.DataTextField = "CategoryName";
                ddlCategorys.DataValueField = "CategoryId";
                ddlCategorys.DataBind();
                ddlCategorys.Items.Insert(0, new ListItem("-- Select --", string.Empty));


            }





        }

        protected void Button2_Click(object sender, EventArgs e)
        {



        }


        protected void gvProductDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string cateId = ddlCategory.SelectedValue;
            string brandId = ddlBrand.SelectedValue;

            var data = itemrepo.Find(i => i.Product.CategoryId.Value.ToString() == cateId && (string.IsNullOrEmpty(brandId) || i.Product.BrandId.Value.ToString() == brandId));
            //var data = productRepo.Find(p => p.CategoryId.Value.ToString() == cateId);
            //&& (!p.BrandId.HasValue || p.BrandId.Value.ToString() == brandId));
            // var data = productRepo.GetAll(catrepo.GetById).ToList();
            gvProductDetails.DataSource = data.ToList();
            gvProductDetails.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Select --", string.Empty));
            gvProductDetails.PageIndex = e.NewPageIndex;
            gvProductDetails.DataBind();
        }



        protected void ddlCategorys_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;

            DropDownList ddlBrands = (DropDownList)row.FindControl("ddlBrands");


            //filldata();
            try
            {
                string cateId = ((DropDownList)sender).SelectedValue;

                var brands = brandrepo.Find(b => b.CategoryBrandMappings.AsQueryable().Where(cbp => cbp.CategoryId.Value.ToString() == cateId).Count() > 0);
                //var brands = brandrepo.Find(b => b.CategoryBrandMappings.ToString() == cateId);
                ddlBrands.DataSource = brands;
                ddlBrands.DataTextField = "BrandName";
                ddlBrands.DataValueField = "BrandId";
                ddlBrands.DataBind();
                ddlBrands.Items.Insert(0, new ListItem("-- Select --", string.Empty));
                //filldata();
            }
            catch (Exception ex)
            {




            }
            // var data = itemrepo.Find(i => i.Product.CategoryId.Value.ToString() == cateId && (string.IsNullOrEmpty(brandId) || i.Product.BrandId.Value.ToString() == brandId));
            //var data = productRepo.Find(p => p.CategoryId.Value.ToString() == cateId);
            //&& (!p.BrandId.HasValue || p.BrandId.Value.ToString() == brandId));
            // var data = productRepo.GetAll(catrepo.GetById).ToList();
            //gvProductDetails.DataSource = data.ToList();
            //gvProductDetails.DataBind();
            //ddlCategory.Items.Insert(0, new ListItem("-- Select --", string.Empty));
            //gvProductDetails.DataBind();
        }

        protected void ddlBrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;

            DropDownList ddlCategorys = (DropDownList)row.FindControl("ddlCategorys");


            string cateId = ddlCategorys.SelectedValue;
            string brandId = ((DropDownList)sender).SelectedValue;

            var data = itemrepo.Find(i => i.Product.CategoryId.Value.ToString() == cateId && (string.IsNullOrEmpty(brandId) || i.Product.BrandId.Value.ToString() == brandId)).FirstOrDefault();

            Label lblName = (Label)row.FindControl("lblName");
            lblName.Text = data.Product.ProductName;


        }

      

        protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //string name = Request.Form["Name"];


                catrepo = new CategoryRepository(db);
                if (ViewState["CurrentTable"] != null)
                {

                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    GridViewRow row = (GridViewRow)((ImageButton)sender).Parent.Parent;
                    HiddenField productId = (HiddenField)row.FindControl("HiddenField1");
                    Inventory.Domain.Product prod = productRepo.GetById(Convert.ToInt32(productId.Value));

                    DataRow currentRow = dt.Rows[dt.Rows.Count - 1];


                    HiddenField name = (HiddenField)row.FindControl("hftxtname");
                    HiddenField prodDesc = (HiddenField)row.FindControl("hftxtdescription");
                    HiddenField weight = (HiddenField)row.FindControl("hftxtweight");
                    HiddenField price = (HiddenField)row.FindControl("hftxtprice");
                    HiddenField stockinPrice = (HiddenField)row.FindControl("hftxtstockin");
                    HiddenField stock = (HiddenField)row.FindControl("hftxtstock");
                    HiddenField thresh = (HiddenField)row.FindControl("hfthreshhold");
                    TextBox count = (TextBox)row.FindControl("txtAddCount");
                    TextBox comment = (TextBox)row.FindControl("txtcomments");
                    //CheckBox chkrow = (CheckBox)row.FindControl("chkRow");
                    //Button btndelete = (Button)row.FindControl("imgbtnDelete");


                    currentRow["Bar Code"] = prod.BarCode;
                    currentRow["ProductId"] = productId.Value;
                    currentRow["Product Name"] = name.Value;
                    currentRow["Product Description"] = prodDesc.Value;
                    currentRow["Product Weight"] = weight.Value;
                    currentRow["Product Price"] = price.Value;
                    currentRow["Stock value"] = stock.Value;
                    currentRow["Quantity"] = count.Text;
                    currentRow["Comment"] = comment.Text;
                    currentRow["Stock in"] = stockinPrice.Value;
                    currentRow["ThreshHold"] = thresh.Value;
                    // chkrow.Checked = true;

                    // count.Attributes.Add("class", "warning");
                    //count.Attributes["class"] = "warning";
                    // count.CssClass = "warning";
                    //comment.CssClass += " warning";
                    //comment.CssClass = comment.CssClass + " warning";

                    //btndelete.Visible = true;

                    if (currentRow["Quantity"] != (""))
                    {
                      var duplicates = dt.AsEnumerable().GroupBy(r => r[1])//Using Column Index
                     .Where(gr => gr.Count() > 1).Select(g => g.Key);
                        foreach (var d in duplicates)
                        {
                           
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Same Product not allowed');", true);
                            currentRow.Delete();
                   
                        }
                           

                        AddNewRowToGrid();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill the quantity');", true);
                        // currentRow["Bar Code"] = prod.BarCode;
                        gvProductDetails.DataSource = dt;
                        gvProductDetails.DataBind();



                    }
                    //btdelete.Visible = dt.Rows.Count > 1 ? true : false ;


                }
            }

            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "SearchText", "SearchText();", true);
        }

        protected void imgbtnCheckout_Click(object sender, EventArgs e)
        {
            printButton.Visible = true;
            checkoutPrint();

        }
        public void checkoutPrint()
        {
            try
            {
                string _lblProductId;
                string _count;
                decimal totalamt;
                int hf;


                DataTable _dt = new DataTable();
                _dt.Columns.Add("ProductId");
                _dt.Columns.Add("ProductName");
                _dt.Columns.Add("Weight");
                _dt.Columns.Add("Price");
                _dt.Columns.Add("Quantity");
                _dt.Columns.Add("Total");

                int count = 0;
                bool isError = false;

                if (count < gvProductDetails.Rows.Count)
                {

                    foreach (GridViewRow gvrow in gvProductDetails.Rows)
                    {
                        count++;
                        TextBox txtcomment = (TextBox)gvrow.FindControl("txtcomments");
                        TextBox txtCounts = (TextBox)gvrow.FindControl("txtAddCount");
                        //TextBox txtPrice=(TextBox)gvrow.FindControl("txtprice");
                        HiddenField hfproduct1= (HiddenField)gvrow.FindControl("HiddenField1");
                        HiddenField hfproduct = (HiddenField)gvrow.FindControl("hfProductId");
                        HiddenField hftxtname = (HiddenField)gvrow.FindControl("hftxtname");
                        HiddenField hftxtweight = (HiddenField)gvrow.FindControl("hftxtweight");
                        HiddenField stockin = (HiddenField)gvrow.FindControl("hftxtstockin");
                        HiddenField txtPrice = (HiddenField)gvrow.FindControl("hftxtprice");
                        TextBox txtAddCount = (TextBox)gvrow.FindControl("txtAddCount");
                        
                        _lblProductId = Convert.ToString(hfproduct1.Value);
                        _count = Convert.ToString(txtCounts.Text);
        
                        if (txtCounts.Text != ("") || txtCounts.Text != "0" && hftxtname.Value != ("") )
                        {
                            Inventory.Domain.Transaction trans = new Inventory.Domain.Transaction()
                            {
                                Comments = txtcomment.Text,
                                Count = Convert.ToInt32(txtCounts.Text),
                                StockInPrice = !String.IsNullOrEmpty(stockin.Value) ? Convert.ToDecimal(stockin.Value) : 0,
                                CreatedBy = "admin",
                                CreationTime = DateTime.Now.Date,
                                LastUpdatedBy = "admin",

                                LastUpdationTime = DateTime.Now.Date,
                                Price = Convert.ToInt32(txtPrice.Value),
                                ProductId = Convert.ToInt32(hfproduct1.Value),
                                TotalAmount = Convert.ToInt32(txtCounts.Text) * Convert.ToInt32(txtPrice.Value),
                                TenantId = tenantId,


                            };
                            totalamt = Convert.ToInt32(txtCounts.Text) * Convert.ToInt32(txtPrice.Value);
                            _dt.Rows.Add(hfproduct1.Value, hftxtname.Value, hftxtweight.Value, txtPrice.Value, txtAddCount.Text, totalamt);
                            //=Sum(ReportItems!TotalAmount.Value);

                            transrepo.Add(trans);
                            //reportSource.Add(trans);
                            update(_lblProductId, _count);




                        }
                        else
                        {
                            isError = true;
                            break;


                        }

                    }

                    if (!isError)
                    {
                        transrepo.Save();
                        itemrepo.Save();

                        Generatereport(_dt);
                        BindData();
                        Response.Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Null Filled Not allowed');", true);
                        printButton.Visible = false;
                    }
                }


            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }


        }
        private DataTable Getdata(List<Inventory.Domain.Transaction> data)
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("Product Id");
            _dt.Columns.Add("Product Name");
            _dt.Columns.Add("Weight");
            _dt.Columns.Add("Price");
            _dt.Columns.Add("Quantity");
            _dt.Columns.Add("Total");


            foreach (var item in data)
            {
                _dt.Rows.Add(item.ProductId, item.Product.ProductName, item.Product.ProductWeight, item.Product.ProductPrice, item.Count, item.TotalAmount);
            }
            return _dt;
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
               "<DeviceInfo>" +
          "  <OutputFormat>EMF</OutputFormat>" +
          "  <FlashMemorySize>2mBytes</FlashMemorySize>" +
          "  <FlashFonts>320kBytes</FlashFonts>" +
           " <Resolution>180*180DPI</Resolution>" +
            "  <PrintWidth>0mm</PrintWidth>" +
            "  <FixedLeftMargin>0mm</FixedLeftMargin>" +
            "  <FixedRightMargin>72mm</FixedRightMargin>" +
            "  <DarkScale>72mm</DarkScale>" +
          "  <PageWidth>8.5in</PageWidth>" +
          "  <PageHeight>11in</PageHeight>" +
          "  <MarginTop>0.25in</MarginTop>" +
          "  <MarginLeft>0.25in</MarginLeft>" +
          "  <MarginRight>0.25in</MarginRight>" +
          "  <MarginBottom>0.25in</MarginBottom>" +
          "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void CreatePrintablePage()
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.

        }
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);

        }

        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        private void Generatereport(DataTable dt)
        {

            ReportViewer1.SizeToReportContent = true;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/BillReport/bills.rdlc");
            ReportDataSource _rsource = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Add(_rsource);
            ReportViewer1.LocalReport.Refresh();


        }

        protected void btnchckserver_Click(object sender, EventArgs e)
        {

            checkoutPrint();
            ReportViewer1.LocalReport.Refresh();
            Export(ReportViewer1.LocalReport);
            Print();


        }
        private void SetPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                HiddenField productId = (HiddenField)gvProductDetails.FindControl("hfProductId");



                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HiddenField name = (HiddenField)gvProductDetails.Rows[i].Cells[1].FindControl("hftxtname");
                        HiddenField prodDesc = (HiddenField)gvProductDetails.Rows[i].Cells[2].FindControl("hftxtdescription");
                        HiddenField weight = (HiddenField)gvProductDetails.Rows[i].Cells[3].FindControl("hftxtweight");
                        HiddenField price = (HiddenField)gvProductDetails.Rows[i].Cells[4].FindControl("hftxtprice");
                        HiddenField stockinPrice = (HiddenField)gvProductDetails.Rows[i].Cells[5].FindControl("hftxtstockin");
                        HiddenField stock = (HiddenField)gvProductDetails.Rows[i].Cells[6].FindControl("hftxtstock");
                        TextBox count = (TextBox)gvProductDetails.Rows[i].Cells[7].FindControl("txtAddCount");
                        TextBox comment = (TextBox)gvProductDetails.Rows[i].Cells[8].FindControl("txtcomments");
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //Fill the DropDownList with Data   


                        if (i < dt.Rows.Count - 1)
                        {





                            //Assign the value from DataTable to the TextBox   
                            //name.Value = dt.Rows[i]["Bar Code"] = prod.BarCode;
                            name.Value = dt.Rows[i]["Product Name"].ToString();
                            prodDesc.Value = dt.Rows[i]["Product Description"].ToString();
                            weight.Value = dt.Rows[i]["Product Weight"].ToString();
                            stockinPrice.Value = dt.Rows[i]["Product Price"].ToString();
                            stock.Value = dt.Rows[i]["Stock value"].ToString();
                            count.Text = dt.Rows[i]["Quantity"].ToString();
                            comment.Text = dt.Rows[i]["Comment"].ToString();
                            stockinPrice.Value = dt.Rows[i]["Stock in"].ToString();

                            //Set the Previous Selected Items on Each DropDownList  on Postbacks   


                        }

                        rowIndex++;
                    }
                }
            }
        }
        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {

            //Button lb = (Button)sender;
            ImageButton lb = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 1)
                {

                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //dt.Rows.RemoveAt(gvProductDetails.Rows.Count - 1);
                        //dt.AcceptChanges();
                        ResetRowID(dt);
                    }
                }

                //Store the current data in ViewState for future reference  
                ViewState["CurrentTable"] = dt;
                SetPreviousData();
                //Re bind the GridView for the updated data  
                gvProductDetails.DataSource = dt;
                gvProductDetails.DataBind();
            }

            //Set Previous Data on Postbacks  

        }

        private void ResetRowID(DataTable dt)
        {
            int rowNumber = 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row[0] = rowNumber;
                    rowNumber++;
                }
            }
        }
    }

}


//try
//{
//    //ImageButton ib = (ImageButton)sender;
//   // GridViewRow gvRow = (GridViewRow)ib.NamingContainer;
//    GridViewRow row = (GridViewRow)((ImageButton)sender).Parent.Parent;
//    int rowID = row.RowIndex;

//    if (ViewState["CurrentTable"] != null)
//    {
//        DataTable dt = (DataTable)ViewState["CurrentTable"];

//        if (rowID > -1)
//        {
//            dt.Rows.RemoveAt(rowID);
//             // if needed
//        }

//        //if (dt.Rows.Count > 1)
//        //{
//        //    int rowIndex = row.RowIndex;
//        //        //Remove the Selected Row data
//        //        dt.Rows.Remove(dt.Rows[rowIndex]);

//        //}
//        //Store the current data in ViewState for future reference
//        ViewState["CurrentTable"] = dt;
//        //Re bind the GridView for the updated data
//        gvProductDetails.DataSource = dt;
//        gvProductDetails.DataBind();
//    }



//    }
//    catch (Exception ex)
//    {

//        Response.Write(ex.Message);

//    }

//}








