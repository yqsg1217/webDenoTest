using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcCodeFirst.DAL;
using MvcCodeFirst.Models;

namespace MvcCodeFirst.Controllers
{
    public class SysUserRolesController : Controller
    {
        private AccountContext db = new AccountContext();

        // GET: SysUserRoles
        public ActionResult Index()
        {
            var list = from ur in db.SysUserRoles
                       join u in db.SysUsers on ur.SysUserID equals u.ID
                       join r in db.SysRoles on ur.SysRoleID equals r.ID
                       select new
                       {
                           username = u.UserName,
                           password = u.PassWord,
                           emails = u.Email,
                           rolename = r.RoleName,
                           roledesc = r.RoleDesc,
                           uid = u.ID,
                           uidd = ur.SysUserID,
                           rid = r.ID,
                           ridd = ur.SysRoleID
                       };


            var sysUserRoles = db.SysUserRoles.Include(s => s.SysRole).Include(s => s.SysUser);
            var d = db.SysUsers.Include(r => r.SysDepartment);
            return View(sysUserRoles.ToList());
        }

        // GET: SysUserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUserRole sysUserRole = db.SysUserRoles.Find(id);
            if (sysUserRole == null)
            {
                return HttpNotFound();
            }
            return View(sysUserRole);
        }

        // GET: SysUserRoles/Create
        public ActionResult Create()
        {
            ViewBag.SysRoleID = new SelectList(db.SysRoles, "ID", "RoleName");
            ViewBag.SysUserID = new SelectList(db.SysUsers, "ID", "UserName");
            return View();
        }

        // POST: SysUserRoles/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SysUserID,SysRoleID")] SysUserRole sysUserRole)
        {
            if (ModelState.IsValid)
            {
                db.SysUserRoles.Add(sysUserRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SysRoleID = new SelectList(db.SysRoles, "ID", "RoleName", sysUserRole.SysRoleID);
            ViewBag.SysUserID = new SelectList(db.SysUsers, "ID", "UserName", sysUserRole.SysUserID);
            return View(sysUserRole);
        }

        // GET: SysUserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUserRole sysUserRole = db.SysUserRoles.Find(id);
            if (sysUserRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.SysRoleID = new SelectList(db.SysRoles, "ID", "RoleName", sysUserRole.SysRoleID);
            ViewBag.SysUserID = new SelectList(db.SysUsers, "ID", "UserName", sysUserRole.SysUserID);
            return View(sysUserRole);
        }

        // POST: SysUserRoles/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SysUserID,SysRoleID")] SysUserRole sysUserRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysUserRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SysRoleID = new SelectList(db.SysRoles, "ID", "RoleName", sysUserRole.SysRoleID);
            ViewBag.SysUserID = new SelectList(db.SysUsers, "ID", "UserName", sysUserRole.SysUserID);
            return View(sysUserRole);
        }

        // GET: SysUserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUserRole sysUserRole = db.SysUserRoles.Find(id);
            if (sysUserRole == null)
            {
                return HttpNotFound();
            }
            return View(sysUserRole);
        }

        // POST: SysUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUserRole sysUserRole = db.SysUserRoles.Find(id);
            db.SysUserRoles.Remove(sysUserRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
