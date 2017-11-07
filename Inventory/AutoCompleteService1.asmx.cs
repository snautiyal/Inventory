using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using OnBarcode;
using System.IO;
using System.Configuration;
using System.Web.Configuration;

namespace Inventory
{
    /// <summary>
    /// Summary description for AutoCompleteService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoCompleteService1 : System.Web.Services.WebService
    {
        SqlConnection con;
        SqlCommand cmd;
        int tenantId = int.Parse(WebConfigurationManager.AppSettings["TenantId"]);
        [WebMethod]
        public List<string> GetAutoCompleteData(string ProductName)
      {
            try
            {

                List<string> result = new List<string>();
                //string conString = "Data Source=192.168.10.12\\qa;Initial Catalog=inventory; User ID=sa; password=Ingen@123";
                string conString = ConfigurationManager.ConnectionStrings["inventoryConnectionString"].ConnectionString;
                con = new SqlConnection(conString);
                con.Open();
                string sqlQuery = "select ProductName,ProductId from Product where ProductName LIKE '%'+@SearchText+'%' and TenantId="+tenantId;
                //string sqlQuery = "select p.ProductName,(c.CategoryName),b.BrandName,p.BarCode,p.ProductDescription,p.StockInPrice, p.ProductId from Product as p left outer join Category as c on p.CategoryId=c.CategoryId left outer join Brand as b on p.BrandId=b.brandId where p.ProductName like('%'+@SearchText+'%')";
                //string sqlQuery = "select p.ProductName,(c.CategoryName),b.BrandName,p.ProductDescription, p.ProductId from Product as p left outer join Category as c on p.CategoryId=c.CategoryId left outer join Brand as b on p.BrandId=b.brandId where p.ProductName LIKE '%'+@SearchText+'%'";
                cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@SearchText", ProductName.Trim());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(String.Format("{0} - {1}", dr["ProductName"].ToString(), dr["ProductId"].ToString()));
                    //result.Add(String.Format("{0} ({1}) ({2})-{3}-{4}-{5}", dr["ProductName"].ToString(), dr["CategoryName"].ToString(), dr["BrandName"].ToString(), dr["ProductId"].ToString(), dr["ProductDescription"].ToString(), dr["StockInPrice"].ToString()));

                }
                return result;



            }


            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
            return null;
        }



        [WebMethod]
        public List<string> GetItemDetails(string ProductId)
        {
            try
            {

                List<string> result = new List<string>();
                string conString = ConfigurationManager.ConnectionStrings["inventoryConnectionString"].ConnectionString;
                //string conString = "Data Source=192.168.10.12\\qa;Initial Catalog=inventory; User ID=sa; password=Ingen@123";
                con = new SqlConnection(conString);
                con.Open();
                string sqlQuery = "select p.ProductId,p.ProductName,p.ProductDescription,p.ProductWeight,p.StockInPrice,p.ProductPrice,p.Stock,p.ThreshHold,i.Count from item as i left outer join Product as p on i.ProductId=p.ProductId where p.ProductId = " + ProductId.Trim() + " and p.TenantId="+ tenantId;
                //string sqlQuery = "select p.ProductName,p.ProductDescription,p.ProductWeight,p.StockInPrice,p.ProductPrice,p.Stock,i.Count from item as i left outer join Product as p on i.ProductId=p.ProductId where p.ProductId = " + ProductId.Trim() + "";
                cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    result.Add(String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}",dr["ProductName"].ToString(), dr["ProductDescription"].ToString(), dr["ProductWeight"].ToString(), dr["ProductPrice"].ToString(), dr["Count"].ToString(), dr["StockInPrice"].ToString(), dr["ThreshHold"].ToString()));


                }

                return result;
                //    }
                //}
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
            }
            return null;
        }

    }

}

