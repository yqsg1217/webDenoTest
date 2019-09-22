using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCodeFirst.Utility.MenuHelper.Base
{
    /// <summary>
    /// 说明： 其中OrdinalNum表示记录的序号（构建时，每个TagContainer都有个OrdinalNum作为标记，每产生一个li或ul都加1） 
    /// Tb是MVC原生的类，包含用于创建 HTML 元素的类和属性。 
    /// </summary>
    public class TagContainer
    {
        public int OrdinalNum;
        public string Name;
        public TagBuilder Tb;
        public TagContainer ParentContainer;
        public List<TagContainer> ChildrenContainers = new List<TagContainer>();
        public TagContainer(ref int Num, TagContainer parent)
        {
            OrdinalNum = Num++;
            ParentContainer = parent;
            if (parent != null)
            {
                parent.ChildrenContainers.Add(this);
            }
        }
    }
}