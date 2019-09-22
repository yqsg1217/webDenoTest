using MvcCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCodeFirst.Repositories
{
    public interface ISysUserRepository : IDisposable
    {
        IEnumerable<SysUser> GetUsers();
        SysUser GetUserByID(int userID);

        void InsertUser(SysUser user);
        void DeleteUser(int userID);
        void UpdateUser(SysUser user);

        void Save();
    }
}
