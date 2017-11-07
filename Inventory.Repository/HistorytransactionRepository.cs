using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Domain;
using System.Data.Entity;

namespace Inventory.Repository
{
    public interface IHistoryTrasactionRepository : Base.IRepository<Historytransaction>
    {

    }
    public class HistoryTrasactionRepository : Base.Repository<Historytransaction>, IHistoryTrasactionRepository
    {
        public HistoryTrasactionRepository(DbContext context) : base(context) { }
    }
}
