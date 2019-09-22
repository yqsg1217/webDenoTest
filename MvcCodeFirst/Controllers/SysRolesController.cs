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
    public class SysRolesController : Controller
    {
        private AccountContext db = new AccountContext();

        // GET: SysRoles
        public ActionResult Index()
        {
            return View(db.SysRoles.ToList());
        }

        // GET: SysRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysRole sysRole = db.SysRoles.Find(id);
            if (sysRole == null)
            {
                return HttpNotFound();
            }
            return View(sysRole);
        }

        // GET: SysRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SysRoles/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoleName,RoleDesc")] SysRole sysRole)
        {
            if (ModelState.IsValid)
            {
                db.SysRoles.Add(sysRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysRole);
        }

        // GET: SysRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysRole sysRole = db.SysRoles.Find(id);
            if (sysRole == null)
            {
                return HttpNotFound();
            }
            return View(sysRole);
        }

        // POST: SysRoles/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoleName,RoleDesc")] SysRole sysRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysRole);
        }

        // GET: SysRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysRole sysRole = db.SysRoles.Find(id);
            if (sysRole == null)
            {
                return HttpNotFound();
            }
            return View(sysRole);
        }

        // POST: SysRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysRole sysRole = db.SysRoles.Find(id);
            db.SysRoles.Remove(sysRole);
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
