using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain;
using System.Data.Entity;

namespace Inventory.Repository
{
    public interface ICategoryRepository: Base.IRepository<Category>
    {

    }
    public class CategoryRepository:Base.Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context) { }
    }
}
