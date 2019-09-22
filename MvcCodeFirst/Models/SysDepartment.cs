using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFirst.Models
{
    public class SysDepartment
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }
        public virtual ICollection<SysUser> SysUsers { get; set; }
    }
}