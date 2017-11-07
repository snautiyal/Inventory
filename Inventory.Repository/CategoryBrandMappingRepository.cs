using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Inventory.Repository
{
    public interface ICategoryBrandMappingRepository : Base.IRepository<CategoryBrandMapping>
    {

    }
    public class CategoryBrandMappingRepository : Base.Repository<CategoryBrandMapping>,ICategoryBrandMappingRepository
    {
        public CategoryBrandMappingRepository(DbContext context) : base(context) { }
    }
}
