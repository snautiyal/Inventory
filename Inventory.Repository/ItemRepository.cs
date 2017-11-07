using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain;
using System.Data.Entity;

namespace Inventory.Repository
{
    public interface IItemRepository : Base.IRepository<Item>
    {

        List<StockReport_Result> GetProductStock(int productId, int tenantId);
        List<CategorySReport_Result> Getscategorystock(int categoryId, int productId, int tenantId);
        List<SiCategoryReport_Result> Getsicategorystock(int categoryId, int tenantId);
        List<ConsumptionReport_Result> GetcounsumptionReport(int productId, DateTime startDate, DateTime endDate, int tenantId);
      
    }
    public class ItemRepository : Base.Repository<Item>, IItemRepository
    {

        public ItemRepository(DbContext context) : base(context) { }


        public List<FetchReport_Result> GetItemsByDate(DateTime startDate, DateTime endDate, int tenantId)
        {
            List<Item> response = new List<Item>();
            var db = new InventoryDb();
            return db.FetchReport(startDate, endDate, tenantId).ToList();
            
        }
        public List<ConsumptionReport_Result> GetcounsumptionReport(int productId, DateTime startDate, DateTime endDate, int tenantId)
        {
            List<Item> response = new List<Item>();
            var db = new InventoryDb();
            return db.ConsumptionReport(productId,startDate, endDate, tenantId).ToList();
            
        }

        public List<StockReport_Result> GetProductStock(int productId,int tenantId)
        {
            var db = new InventoryDb();
            return db.StockReport(productId,tenantId).ToList();

        }
         public List<CategorySReport_Result>Getscategorystock(int categoryId,int productId,int tenantid)
        {
            var db = new InventoryDb();
            return db.CategorySReport(categoryId,productId,tenantid).ToList();


        }

         public List<SiCategoryReport_Result> Getsicategorystock(int categoryId,int tenantid)
         {

             var db = new InventoryDb();
             return db.SiCategoryReport(categoryId, tenantid).ToList();
         
         
         }
        
         //public List<> Getsicategorystock(int categoryId)
         //{

         //    var db = new InventoryDb();
         //    return db.SiCategoryReport(categoryId).ToList();


         //}

         
    }
}
