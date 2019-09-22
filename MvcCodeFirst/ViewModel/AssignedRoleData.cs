using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFirst.ViewModel
{
    /// <summary>
    /// 用来表示角色是否分配给用户
    /// </summary>
    public class AssignedRoleData
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Assigned { get; set; }
    }
}