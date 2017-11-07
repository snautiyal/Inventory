using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repository.Base
{
    public interface IRepository<T> where T : class
    {
        int Count();
        IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int page = 0, int size = 0);
        T GetById(object id);
        IEnumerable<T> Find(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          string includeProperties = "", int page = 0, int size = 0);
        T Add(T entity);
        //IEnumerable<T> GetForList(string text, string value, Expression<Func<T, bool>> filter = null);
        //IEnumerable<SelectListItem> GetForListQuery(string tableName, string textField, string valueField);
        void Delete(object id);
        void Edit(T entity);
        void Save();
       
    }
}
