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
    public class SysUsersController : Controller
    {
        private AccountContext db = new AccountContext();

        // GET: SysUsers
        public ActionResult Index()
        {
            var sysUsers = db.SysUsers.Include(s => s.SysDepartment);
            return View(sysUsers.ToList());
        }

        // GET: SysUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // GET: SysUsers/Create
        public ActionResult Create()
        {
            ViewBag.SysDepartmentId = new SelectList(db.SysDepartments, "Id", "DepartmentName");
            return View();
        }

        // POST: SysUsers/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,PassWord,Email,CreateDate,SysDepartmentId")] SysUser sysUser)
        {
            if (ModelState.IsValid)
            {
                db.SysUsers.Add(sysUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SysDepartmentId = new SelectList(db.SysDepartments, "Id", "DepartmentName", sysUser.SysDepartmentId);
            return View(sysUser);
        }

        // GET: SysUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.SysDepartmentId = new SelectList(db.SysDepartments, "Id", "DepartmentName", sysUser.SysDepartmentId);
            return View(sysUser);
        }

        // POST: SysUsers/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,PassWord,Email,CreateDate,SysDepartmentId")] SysUser sysUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SysDepartmentId = new SelectList(db.SysDepartments, "Id", "DepartmentName", sysUser.SysDepartmentId);
            return View(sysUser);
        }

        // GET: SysUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser sysUser = db.SysUsers.Find(id);
            if (sysUser == null)
            {
                return HttpNotFound();
            }
            return View(sysUser);
        }

        // POST: SysUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            db.SysUsers.Remove(sysUser);
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
