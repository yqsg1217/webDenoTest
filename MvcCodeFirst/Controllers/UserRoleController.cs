using MvcCodeFirst.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCodeFirst.ViewModel;
using System.Data;
using System.Data.Entity;
using System.Net;
using MvcCodeFirst.Models;

namespace MvcCodeFirst.Controllers
{
    public class UserRoleController : Controller
    {
        private static AccountContext db = new AccountContext();
        // GET: UserRole
        public ActionResult Index(int? id)
        {
            var viewModel = new UserRoleIndexData();
            viewModel.SysUsers = db.SysUsers.Include(d => d.SysDepartment).Include(u => u.UserRoles.Select(ur => ur.SysRole)).OrderBy(u => u.UserName);

            if (id != null)
            {
                ViewBag.UserID = id.Value;
                viewModel.SysUserRoles = db.SysUsers.Where(t => t.ID == id.Value).Single().UserRoles;
                viewModel.SysRoles = db.SysUserRoles.Where(t => t.SysUserID == id.Value).Select(t => t.SysRole);
            }
            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser user = db.SysUsers.Include(u => u.SysDepartment).Include(t => t.UserRoles).Where(u => u.ID == id).Single();

            //用户下的部门信息
            ViewBag.SysDepartmentId = new SelectList(db.SysDepartments, "Id", "DepartmentName", user.SysDepartmentId);
            //将用户下的角色信息显示
            PopulateAssignedRoleData(user);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string[] selectedRoles)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userToUpdate = db.SysUsers.Include(t => t.UserRoles).Where(u => u.ID == id).Single();
            if (TryUpdateModel(userToUpdate, "", new string[] { "LoginName", "PassWord", "Email", "CreateDate", "SysDepartmentId" }))
            {
                try
                {
                    UpdateUserRoles(selectedRoles, userToUpdate);
                    db.Entry(userToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    string error = ex.Message;

                }
                finally { }
            }
            //如果失败重新绑定视图
            ViewBag.SysDepartmentId = new SelectList(db.SysDepartments, "Id", "DepartmentName", userToUpdate.SysDepartmentId);
            //将用户下的角色信息显示
            PopulateAssignedRoleData(userToUpdate);
            return View(userToUpdate);
        }

        private void UpdateUserRoles(string[] selectedRoles, SysUser userToUpdate)
        {
            using (AccountContext db2 = new AccountContext())
            {
                if (selectedRoles == null)
                {
                    var sysUserRoles = db2.SysUserRoles.Where(u => u.SysUserID == userToUpdate.ID).ToList();
                    foreach (var item in sysUserRoles)
                    {
                        db2.SysUserRoles.Remove(item);
                    }
                    db2.SaveChanges();
                    return;
                }
                //编辑后的角色
                var selectedRolesHS = new HashSet<string>(selectedRoles);
                //原来的角色
                var userRoles = userToUpdate.UserRoles.Select(t => t.SysRoleID);
                foreach (var item in db2.SysRoles)
                {
                    //如果被选中，原来的没有角色需要添加
                    if (selectedRolesHS.Contains(item.ID.ToString()))
                    {
                        if (!userRoles.Contains(item.ID))
                        {
                            userToUpdate.UserRoles.Add(new SysUserRole { SysUserID = userToUpdate.ID, SysRoleID = item.ID });
                        }
                    }
                    else
                    {
                        if (userRoles.Contains(item.ID))//如果没被选中，原来的角色去掉
                        {
                            SysUserRole sysUserRole = db2.SysUserRoles.FirstOrDefault(ur => ur.SysRoleID == item.ID && userToUpdate.ID == ur.SysUserID);
                            db2.SysUserRoles.Remove(sysUserRole);
                            db2.SaveChanges();
                        }
                    }
                }
            }
        }

        private void PopulateAssignedRoleData(SysUser user)
        {
            var allRoles = db.SysRoles;
            //获取用户关联的角色id
            var userRoles = new HashSet<int>(user.UserRoles.Select(t => t.SysRoleID));
            var viewModel = new List<AssignedRoleData>();
            foreach (var item in allRoles)
            {
                viewModel.Add(new AssignedRoleData
                {
                    RoleId = item.ID,
                    RoleName = item.RoleName,
                    Assigned = userRoles.Contains(item.ID)
                });
            }
            ViewBag.Roles = viewModel;
        }
    }
}