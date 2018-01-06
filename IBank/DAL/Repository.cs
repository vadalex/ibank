using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        private IBankContext context;
        private DbSet<T> dbSet;

        public DataRepository(IBankContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> query = dbSet;
            if (where != null)
                query = query.Where(where);
            return query;
        }

        public T GetSingle(int id)
        {
            return dbSet.Find(id);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Remove(T entity)
        {            
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
