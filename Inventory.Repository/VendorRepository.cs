using Inventory.Domain;
using Inventory.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository
{
    public interface IvendorRepository:Base.IRepository<Vendor>
    {


    }

    public class vendorRepository : Base.Repository<Vendor>, IvendorRepository
    {

        public vendorRepository(DbContext context) : base(context) { }
    
    }




}
