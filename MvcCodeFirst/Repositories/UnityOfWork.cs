using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcCodeFirst.DAL;
using MvcCodeFirst.Models;
using System.Data.Entity;
using System.Data;

namespace MvcCodeFirst.Repositories
{
    public class UnityOfWork : IDisposable
    {
        private AccountContext context = new AccountContext();
        private IGenericRepository<SysUser> sysUserRepository;
        public IGenericRepository<SysUser> SysUserRepository
        {
            get
            {
                if (sysUserRepository == null)
                {
                    sysUserRepository = new GenericRepository<SysUser>(context);
                }
                return sysUserRepository;
            }
        }

        private IGenericRepository<SysMenu> sysMenuRepository;
        public IGenericRepository<SysMenu> SysMenuRepository
        {
            get
            {
                if (sysMenuRepository == null)
                {
                    return new GenericRepository<SysMenu>(context);
                }
                return sysMenuRepository;
            }
        }

        private DbContextTransaction dbTransaction;
        public DbContextTransaction DbTransaction
        {
            get
            {
                if (dbTransaction == null)
                {
                    dbTransaction = context.Database.BeginTransaction();
                }
                return dbTransaction;
            }
        }

        public int Commit()
        {
            try
            {
                var returnValue = context.SaveChanges();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                return returnValue;
            }
            catch (Exception)
            {
                if (dbTransaction != null)
                {
                    this.dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                this.TranDispose();
            }
        }

        public void TranDispose()
        {
            if (dbTransaction != null)
            {
                this.dbTransaction.Dispose();
            }
            this.context.Dispose();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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