using Inventory.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Inventory.Repository
{
    public interface IStockingDetailsRepository : Base.IRepository<StockingDetail>
    {

        List<PeriodicReport_Result> GetProductStock(int productId, DateTime startDate, DateTime endDate, int tenantId);
        List<StockingReport_Result> GetProductStocking(int productId, DateTime startDate, DateTime endDate, int tenantId);
        List<StockingItemsReport_Result> GetProductStockingItem(DateTime startDate, DateTime endDate, int tenantId);
        
    }
    public class StockingDetailsRepository : Base.Repository<StockingDetail>,IStockingDetailsRepository
    {
        public StockingDetailsRepository(DbContext context) : base(context) { }


        public List<PeriodicReport_Result> GetProductStock(int productId, DateTime startDate, DateTime endDate, int tenantId)
        {
            var db = new InventoryDb();
            var stock = db.PeriodicReport(productId,startDate,endDate, tenantId).ToList();
            return stock;
           // return db.StockReport(productId, startDate, endDate).ToList();

        }
        public List<StockingReport_Result> GetProductStocking(int productId, DateTime startDate, DateTime endDate,int tenantId)
        {
            var db = new InventoryDb();
            var stocking = db.StockingReport(productId, startDate, endDate,tenantId).ToList();
            return stocking;
            // return db.StockReport(productId, startDate, endDate).ToList();

        }

        public List<StockingItemsReport_Result> GetProductStockingItem(DateTime startDate, DateTime endDate, int tenantId)
        {
            var db = new InventoryDb();
            var stockingitems = db.StockingItemsReport(startDate, endDate, tenantId).ToList();
            return stockingitems;
            // return db.StockReport(productId, startDate, endDate).ToList();

        }

    }
}
