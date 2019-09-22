using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCodeFirst.Models
{
    public class SysUser
    {
        public SysUser()
        {
            CreateDate = DateTime.Now;
        }
        public int ID { get; set; }

        [DisplayName("用户名")]
        [StringLength(10, ErrorMessage = "最大不能大于10位字符")]
        [Column("LoginName")]
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        public virtual ICollection<SysUserRole> UserRoles { get; set; }

        public int? SysDepartmentId { get; set; }
        public virtual SysDepartment SysDepartment { get; set; }
    }
}