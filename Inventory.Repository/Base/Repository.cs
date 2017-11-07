using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace Inventory.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext dbContext;
        protected readonly IDbSet<T> dbSet;

        public Repository(DbContext context)
        {
            dbContext = context;
            dbSet = context.Set<T>();
        }

        public virtual int Count()
        {
            IQueryable<T> query = dbSet;
            var prop = query.ElementType.GetProperty("IsDeleted");
            if (prop != null)
                query = Simplified<T>(query, prop, null);
            return query.Count();
        }
        public virtual IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int page = 0, int size = 0)
        {

            IQueryable<T> query = dbSet;
            if (orderBy != null)
            {
                query = orderBy(query);
                if (size > 0)
                {
                    query = query.Skip(size * page).Take(size);
                }
            }
            return query.AsEnumerable<T>();
        }

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> Find(
          Expression<Func<T, bool>> filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          string includeProperties = "", int page = 0, int size = 0)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            try
            {
                var prop = query.ElementType.GetProperty("IsDeleted");
                if (prop != null)
                    query = Simplified<T>(query, prop, null);
            }
            catch (Exception e)
            { }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    query = query.Include(includeProperty);
                }
                catch (Exception ex)
                { }
            }


            if (orderBy != null)
            {
                query = orderBy(query);

                //apply pagination
                if (size > 0)
                {
                    query = query.Skip(size * page).Take(size);

                }
                else
                {
                    query = query.Take(10);
                }
            }

            return query.ToList();

        }

        public virtual T GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual T Add(T entity)
        {
            return dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (dbContext.Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Edit(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dbContext.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Save()
        {
            dbContext.SaveChanges();
        }


        //public IEnumerable<SelectListItem> GetForList(string text, string value, Expression<Func<T, bool>> filter = null)
        //{
        //    List<SelectListItem> response = new List<SelectListItem>();
        //    try
        //    {
        //        IQueryable<T> query = dbSet;
        //        if (filter != null)
        //            query = query.Where(filter);
        //        try
        //        {
        //            var prop = query.ElementType.GetProperty("IsActive");
        //            if (prop != null)
        //                query = Simplified<T>(query, prop, true);
        //        }
        //        catch (Exception e)
        //        { }
        //        try
        //        {
        //            var prop = query.ElementType.GetProperty("IsDeleted");
        //            if (prop != null)
        //                query = Simplified<T>(query, prop, null);
        //        }
        //        catch (Exception e)
        //        { }

        //        IEnumerable<T> responseObject = query.ToList();
        //        //if (query.Count() > 0)
        //        {
        //            foreach (var item in responseObject)
        //            {
        //                //var objText=item.GetType().GetProperty(text).GetValue(item, null);
        //                response.Add(new SelectListItem()
        //                {

        //                    //Text = objText==null?string.Empty:objText.ToString(),
        //                    //Value = item.GetType().GetProperty(value).GetValue(item, null).ToString(),
        //                    Text = HelperMethods.GetProperty(item, text),
        //                    Value = HelperMethods.GetProperty(item, value)
        //                });
        //            }
        //        }
        //    }
        //    catch (System.Data.Entity.Core.MetadataException ex)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return response as IEnumerable<SelectListItem>;
        //}

        //public IEnumerable<SelectListItem> GetForListQuery(string tableName, string textField, string valueField)
        //{

        //    List<SelectListItem> data = new List<SelectListItem>();
        //    string[] arr = new string[] { };
        //    Type type = Type.GetType("Workers.Core.API.Domain." + tableName + ",Workers.Core.API.Domain");
        //    string query = "select * from Master." + tableName;
        //    var quer = dbContext.Database.SqlQuery(type, query, arr);
        //    foreach (var item in quer)
        //    {
        //        data.Add(new SelectListItem()
        //        {
        //            Text = item.GetType().GetProperty(textField).GetValue(item).ToString(),
        //            Value = item.GetType().GetProperty(valueField).GetValue(item).ToString(),
        //        }
        //        );
        //    }
        //    return data;
        //}


        private IQueryable<T> Simplified<T>(IQueryable<T> query, string propertyName, bool propertyValue)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
            return Simplified<T>(query, propertyInfo, propertyValue);
        }

        private IQueryable<T> Simplified<T>(IQueryable<T> query, PropertyInfo propertyInfo, Nullable<bool> propertyValue)
        {
            ConstantExpression c;
            ParameterExpression e = Expression.Parameter(typeof(T), "e");
            MemberExpression m = Expression.MakeMemberAccess(e, propertyInfo);
            if (!propertyValue.HasValue)
                c = Expression.Constant(null);
            else
                c = Expression.Constant(propertyValue.Value, propertyValue.GetType());
            BinaryExpression b = Expression.Equal(m, c);

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(b, e);
            return query.Where(lambda);
        }
    }
}
