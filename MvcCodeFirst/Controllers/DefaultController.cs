using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCodeFirst.Models;
using MvcCodeFirst.Repositories;
using Ninject;

namespace MvcCodeFirst.Controllers
{
    public class DefaultController : Controller
    {
        private IWeapon weapon;
        // GET: Default
        public ActionResult Index()
        {
            ISysUserRepository _uerRepository = new SysUserRepository(new DAL.AccountContext());
            _uerRepository.InsertUser(new Models.SysUser { CreateDate = DateTime.Now, Email = "a", PassWord = "df", UserName = "咳咳咳", SysDepartment = new Models.SysDepartment { DepartmentName = "后勤部", DepartmentDesc = "后勤保障" } });
            _uerRepository.Save();
            var list = _uerRepository.GetUsers();
            return View(list);
        }

        public ActionResult SharedDateDemo()
        {
            ViewBag.DateTime = DateTime.Now;
            return View();
        }

        [ChildActionOnly]
        public ActionResult PartialViewDate()
        {
            ViewBag.DateTime = DateTime
                .Now.AddMinutes(10);
            return PartialView("_PartialPageDateTime");
        }

        public ActionResult Battle()
        {
            var warrior1 = new Samurai(new Sword());


            // 1.创建一个Ninject的内核实例
            IKernel ninjectKernel = new StandardKernel();
            //2. 配置Ninject内核,指明接口需绑定的类
            ninjectKernel.Bind<IWeapon>().To<Sword>();
            //3. 根据上一步的配置创建一个对象
            var weapon = ninjectKernel.Get<IWeapon>();



            warrior1 = new Samurai(weapon);



            ViewBag.Res = warrior1.Attack("the evildoers");



            return View();
        }
    }
}