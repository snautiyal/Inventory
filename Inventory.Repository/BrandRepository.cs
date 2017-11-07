using Inventory.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Inventory.Repository
{
    public interface IBrandRepository : Base.IRepository<Brand>
    { 
    
    
    }
    public class BrandRepository : Base.Repository<Brand>, IBrandRepository
    {
        public BrandRepository(DbContext context) : base(context) { }
       
    }
}
