using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcCodeFirst.Models;

namespace MvcCodeFirst.ViewModel
{
    public class UserRoleIndexData
    {
        public IEnumerable<SysUser> SysUsers { get; set; }
        public IEnumerable<SysRole> SysRoles { get; set; }
        public IEnumerable<SysUserRole> SysUserRoles { get; set; }
    }
}