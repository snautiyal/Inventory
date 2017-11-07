using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain;
using System.Data.Entity;


namespace Inventory.Repository
{
    public interface IFeatureRepository : Base.IRepository<Feature>
    { 
    
    
    
    }
    public class FeatureRepository : Base.Repository<Feature>,IFeatureRepository
    { 
    
    public FeatureRepository(DbContext context) : base(context) { }
    
    }
   
}
