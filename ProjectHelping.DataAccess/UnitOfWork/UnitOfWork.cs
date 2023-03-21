using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProjectHelping.Data.Context;
using ProjectHelping.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ProjectHelping.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        private bool disposed = false;
        public List<string> ErrorMessageList = new List<string>();

        private DbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    var contextOptions = new DbContextOptionsBuilder<ProjectDbContext>()
                    .UseSqlServer(Environment.GetEnvironmentVariable("myconn"))
                    .Options;
                    _dbContext = new ProjectDbContext(contextOptions);
                }
                return _dbContext;
            }
            set { _dbContext = value; }
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(DbContext, ErrorMessageList);
        }

        public int SaveChanges()
        {
            try
            {
                using (TransactionScope tScope = new TransactionScope())
                {
                    int result = DbContext.SaveChanges();
                    tScope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageList.Add(ex.Message);
                return -1;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                    DbContext = null;
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
