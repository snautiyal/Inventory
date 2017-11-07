using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain;
using System.Data.Entity;

namespace Inventory.Repository
{
    public interface IProductRepository : Base.IRepository<Product>
    {



    }
    public class ProductRepository : Base.Repository<Product>,IProductRepository
    {

        public ProductRepository(DbContext context) : base(context) { }
        public List<ThreshHold_Result> Getthreshhold(int tenantId)
        {
            var db = new InventoryDb();
            return db.ThreshHold(tenantId).OrderByDescending(o => o.ProductId).ToList();


        }

    }
     
}
