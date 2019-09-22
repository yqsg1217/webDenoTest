using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcCodeFirst.Models;
using MvcCodeFirst.DAL;

namespace MvcCodeFirst.Repositories
{
    public class UserRepository : GenericRepository<SysUser>, IUserRepository
    {
        public UserRepository() : base(new AccountContext())
        {

        }
    }
}