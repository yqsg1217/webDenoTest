using MvcCodeFirst.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCodeFirst.Repositories;
using PagedList;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Common;
using MvcCodeFirst.Models;
using System.Security.Cryptography;
using System.Web.Security;

namespace MvcCodeFirst.Controllers
{
    public class AccountController : Controller
    {
        ISysUserRepository users = new SysUserRepository(new AccountContext());
        private AccountContext db = new AccountContext();
        // GET: Account
        public ActionResult Index(string currentFilter, int? page)
        {
            var list = users.GetUsers();
            ///UnityOfWork unity = new Repositories.UnityOfWork();
            //var s = unity.SysUserRepository.Get();
            // db.SysUsers.Add(new Models.SysUser { Email = "yqsg1217@163.com", PassWord = "123456", UserName = "酷酷酷" });
            // db.SaveChanges();
            //var list = db.SysUsers.ToList();
            //var aa = from a in db.SysUsers join b in db.SysDepartments on a.SysDepartmentId equals b.Id select a;
            //var newlist = db.SysUsers.Include(r => r.SysDepartment);
            //users.Dispose();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string username = fc["username"];
            string password = fc["password"];
            string encrptPwd = Md5(password);
            bool RememberMe = fc["RememberMe"] == null ? false : true;
            string returnUrl = Convert.ToString(TempData["ReturnUrl"]);
            UnityOfWork unity = new UnityOfWork();
            SysUser user = unity.SysUserRepository.Get(t => t.UserName == username && t.PassWord == password).FirstOrDefault();
            unity.Dispose();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, RememberMe);
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("~/");
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Account/LogOn");
        }

        /// <summary>
        /// md5
        /// </summary>
        /// <param name="encypStr"></param>
        /// <returns></returns>
        public static string Md5(string encypStr)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
            byte[] inputBye;
            byte[] outputBye;
            inputBye = System.Text.Encoding.ASCII.GetBytes(encypStr);
            outputBye = m5.ComputeHash(inputBye);
            retStr = Convert.ToBase64String(outputBye);
            return (retStr);
        }


        public ActionResult Register()
        {
            return View();
        }

        public int Update()
        {
            //例子3：Database.ExecuteSqlCommand执行更新语句

            return db.Database.ExecuteSqlCommand("UPDATE dbo.Posts SET Rating = 5 WHERE Author = @author",
                    new SqlParameter("@author", "111"));

        }

        public ActionResult Details(int id)
        {
            string query = "select LoginName as UserName,* from dbo.sysuser where id=@id";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@id",id)
            };
            //例子1：DbSet.SqlQuery查询并返回Entities
            var find = db.SysUsers.SqlQuery(query, paras).SingleOrDefault();
            var model = db.SysUsers.Include(u => u.SysDepartment).SingleOrDefault(t => t.ID == id);

            DbParameter[] parameters = new DbParameter[]
          {
            new SqlParameter{ ParameterName="id",Value=id, DbType= System.Data.DbType.Int16},
          };

            var entity = db.SysUsers.SqlQuery(query, parameters).SingleOrDefault();

            //例子2 Database.SqlQuery 返回其他类型
            string querys = "select loginName from SysUser";
            var names = db.Database.SqlQuery<string>(querys).ToList();
            //当表中有外表字段，并显示相应外表id对应的name值必须自定义类来装载数据
            SqlParameter[] param = new SqlParameter[]
     {
                new SqlParameter("@id",id)
     };
            var sysUser = db.Database.SqlQuery<SysUser>(query, param).SingleOrDefault();//返回Model报错
            return View(model);
        }
    }
}
