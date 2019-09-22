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
    public class SysDepartmentsController : Controller
    {
        private AccountContext db = new AccountContext();

        // GET: SysDepartments
        public ActionResult Index()
        {
            var sss = db.SysDepartments;
            var list = db.SysDepartments.Include(t => t.SysUsers);
            //  return View(db.SysDepartments.ToList());
            return View(list);
        }

        // GET: SysDepartments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysDepartment sysDepartment = db.SysDepartments.Find(id);
            if (sysDepartment == null)
            {
                return HttpNotFound();
            }
            return View(sysDepartment);
        }

        // GET: SysDepartments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SysDepartments/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartmentName,DepartmentDesc")] SysDepartment sysDepartment)
        {
            if (ModelState.IsValid)
            {
                db.SysDepartments.Add(sysDepartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sysDepartment);
        }

        // GET: SysDepartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysDepartment sysDepartment = db.SysDepartments.Find(id);
            if (sysDepartment == null)
            {
                return HttpNotFound();
            }
            return View(sysDepartment);
        }

        // POST: SysDepartments/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartmentName,DepartmentDesc")] SysDepartment sysDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sysDepartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sysDepartment);
        }

        // GET: SysDepartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysDepartment sysDepartment = db.SysDepartments.Find(id);
            if (sysDepartment == null)
            {
                return HttpNotFound();
            }
            return View(sysDepartment);
        }

        // POST: SysDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SysDepartment sysDepartment = db.SysDepartments.Find(id);
            db.SysDepartments.Remove(sysDepartment);
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
