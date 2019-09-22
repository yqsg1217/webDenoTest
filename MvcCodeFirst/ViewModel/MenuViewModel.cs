using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcCodeFirst.Models;
using MvcCodeFirst.Repositories;

namespace MvcCodeFirst.ViewModel
{
    public class MenuViewModel<T>
    {
        public IList<T> MenuItems = new List<T>();

        //构建这个菜单的树形结构
        public static MenuViewModel<SysMenu> CreateMenuModel(string menuname)
        {
            UnityOfWork unityOfWork = new Repositories.UnityOfWork();
            // 1. 根据menuName获取开始的根菜单
            SysMenu itemRoot = unityOfWork.SysMenuRepository.Get(filter: m => m.Name == menuname).FirstOrDefault();
            if (itemRoot != null)
            {
                MenuViewModel<SysMenu> model = new ViewModel.MenuViewModel<Models.SysMenu>();
                // 2. 依次添加枝叶菜单 
                // 2.1 获取itemRoot的所有子菜单
                IEnumerable<SysMenu> menus = unityOfWork.SysMenuRepository.Get(filter: m => m.ParentID == itemRoot.ID);
                //2.2对每个子菜单进行递归查询
                foreach (var item in menus)
                {
                    itemRoot.MenuChildren.Add(item);
                    AddChildNode(item);
                }
            }
            return null;
        }

        //递归执行：找到menu子成员并添加
        public static void AddChildNode(SysMenu menu)
        {
            UnityOfWork unityOfWork = new UnityOfWork();
            var menus = unityOfWork.SysMenuRepository.Get(filter: m => m.ParentID == menu.ID);
            foreach (var item in menus)
            {
                menu.MenuChildren.Add(item);
                AddChildNode(item);
            }
        }
    }
}