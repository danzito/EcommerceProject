using System.Linq.Expressions;
using Ecomm.DataAccess.Data;
using Ecomm.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecomm.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
            db.Products.Include(u => u.Category).Include(u => u.CategoryId);
        }
        IEnumerable<T> IRepository<T>.GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        //T IRepository<T>.Get(Expression<Func<T, bool>> filter)
        //{
        //    IQueryable<T> query = dbSet;
        //    query = query.Where(filter);
        //    return query.FirstOrDefault();
        //}

        void IRepository<T>.Add(T entity)
        {
            dbSet.Add(entity);
        }

        void IRepository<T>.Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        void IRepository<T>.RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (!String.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }
    }
}
