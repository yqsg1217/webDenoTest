using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MvcCodeFirst.Models;

namespace MvcCodeFirst.DAL
{
    public class AccountInitializer : DropCreateDatabaseAlways<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            var list = new List<SysUser>();
            list.Add(new SysUser { UserName = "小张", PassWord = "000", CreateDate=DateTime.Now, Email="edd@163.com" });
            list.Add(new SysUser { UserName = "小兰", PassWord = "111", CreateDate = DateTime.Now, Email = "edd@163.com" });
            list.ForEach(s => context.SysUsers.Add(s));

            context.SysRoles.Add(new SysRole { RoleName = "董事长", RoleDesc = "公司最高领导人" });
            context.SysRoles.Add(new SysRole { RoleName = "总经理", RoleDesc = "负责公司整个运营管理" });

            context.SysUserRoles.Add(new SysUserRole { SysRole = new SysRole { RoleName = "市场部经理", RoleDesc = "负责市场运营以及营销方案策划" }, SysUser = new SysUser { UserName = "小李", PassWord = "8888" } });
            base.Seed(context);
        }
    }
}