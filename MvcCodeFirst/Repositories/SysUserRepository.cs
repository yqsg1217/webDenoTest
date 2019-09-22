using MvcCodeFirst.DAL;
using MvcCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcCodeFirst.Repositories
{
    public class SysUserRepository : ISysUserRepository
    {
        private AccountContext context;
        public SysUserRepository(AccountContext context)
        {
            this.context = context;
        }

        //查询所有用户
        public IEnumerable<SysUser> GetUsers()
        {
            return context.SysUsers.ToList();
        }
        public SysUser GetUserByID(int userID)
        {
            return context.SysUsers.Find(userID);
        }
        //添加用户
        public void InsertUser(SysUser user)
        {
            context.SysUsers.Add(user);
        }
        //删除用户
        public void DeleteUser(int id)
        {
            var delSysUser = context.SysUsers.FirstOrDefault(u => u.ID == id);
            context.SysUsers.Remove(delSysUser);
        }
        public void UpdateUser(SysUser user)
        {
            context.Entry(user).State = EntityState.Modified;
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