using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain;
using System.Data.Entity;

namespace Inventory.Repository
{
    public interface ITransactionRepository : Base.IRepository<Transaction>
    {
        List<TransactionReport_Result> GetTransactionreport(int productId, DateTime fromdate, DateTime todate,int tenantId);


    }
    public class TransactionRepository : Base.Repository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(DbContext context) : base(context) { }


        public List<TransactionReport_Result>GetTransactionreport(int productId, DateTime fromdate, DateTime todate,int tenantId)
        {

            var db = new InventoryDb();
            return db.TransactionReport(productId,fromdate, todate,tenantId).ToList();




        }

    }
    

      
}
