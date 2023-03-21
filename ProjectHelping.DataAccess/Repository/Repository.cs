
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectHelping.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelping.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext DbContext;
        private readonly DbSet<T> DbSet;
        public List<string> ErrorMessageList = new List<string>();

        public Repository(DbContext dbContext, List<string> errorMessageList)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
            ErrorMessageList = errorMessageList;
        }
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public int Count()
        {
            IQueryable<T> iQueryable = DbSet.AsQueryable();
            return iQueryable.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet.Where(predicate);
            return iQueryable.Count();
        }

        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet.Where(predicate);
            return iQueryable.ToList().FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> iQueryable = DbSet.AsQueryable();
            return iQueryable;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = DbSet;
            iQueryable = iQueryable.Where(predicate);
            return iQueryable;
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
