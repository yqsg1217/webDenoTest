using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCodeFirst.Repositories;
using MvcCodeFirst.Models;
using System.Transactions;

namespace MvcCodeFirst.Controllers
{
    public class UserTestController : Controller
    {
        IGenericRepository<SysUser> _sysuserRepository = new GenericRepository<SysUser>(new DAL.AccountContext());
        IUserRepository _userRepository = new UserRepository();
        private UnityOfWork unityOfWork = new UnityOfWork();
        // GET: User
        public ActionResult Index()
        {
            //分布式事务
            //using (var db = new DAL.AccountContext())
            //{
            //    using (var ts = new TransactionScope(TransactionScopeOption.Required))
            //    {
            //        //db数据库上下文操作
            //        ts.Complete();
            //    }
            //}
            //，EF在DbContext对象上提供了Database.BeginTransaction()方法，当使用上下文类在事务中执行原生SQL命令时，这个方法特别有用
            //using (var db = new DAL.AccountContext())
            //{
            //    using (var trans = db.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            db.SysUsers.SqlQuery("select * from sysuers");
            //            trans.Commit();
            //        }
            //        catch
            //        {
            //            trans.Rollback();
            //        }
            //        finally
            //        {

            //        }
            //    }
            //}

            //var s = unityOfWork.DbTransaction;
            //unityOfWork.SysUserRepository.Insert(new SysUser { CreateDate = DateTime.Now, Email = "qilong1217@163.com", PassWord = "uuuuuu", UserName = "看看看", SysDepartment = new SysDepartment { DepartmentName = "消防部", DepartmentDesc = "工资结算财务审计" } });
            //unityOfWork.SysUserRepository.Insert(new Models.SysUser { CreateDate = DateTime.Now, Email = "yqsg1217@163.com", PassWord = "123456", UserName = "呃呃呃" });

            //unityOfWork.SysUserRepository.Insert(new SysUser { CreateDate = DateTime.Now, Email = "qilong1217@163.com", PassWord = "uuuuuu", UserName = "急急急", SysDepartment = new SysDepartment { DepartmentName = "研发部", DepartmentDesc = "工资结算财务审计" } });
            //unityOfWork.SysUserRepository.Insert(new Models.SysUser { CreateDate = DateTime.Now, Email = "yqsg1217@163.com", PassWord = "123456", UserName = "鹅鹅鹅" });
            //unityOfWork.Commit();

            // unityOfWork.SysUserRepository.Insert(new SysUser { CreateDate = DateTime.Now, Email = "qilong1217@163.com", PassWord = "uuuuuu", UserName = "没看到", SysDepartment = new SysDepartment { DepartmentName = "财务部", DepartmentDesc = "工资结算财务审计" } });
            //unityOfWork.SysUserRepository.Insert(new Models.SysUser { CreateDate = DateTime.Now, Email = "yqsg1217@163.com", PassWord = "123456", UserName = "酷酷酷" });
            //unityOfWork.Save();

            try
            {
                var one = unityOfWork.SysUserRepository.GetAsNoTrcking().FirstOrDefault(t => t.ID == 1);
                string s = one.SysDepartment.DepartmentName;

                var find = unityOfWork.SysUserRepository.GetByID(1);
                if (find.SysDepartment.DepartmentName != null)
                {
                    string departName = find.SysDepartment.DepartmentName;
                }

                var list = unityOfWork.SysUserRepository.Get();
                unityOfWork.Dispose();

                return View(list);
            }
            catch
            {
                return null;
            }
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
