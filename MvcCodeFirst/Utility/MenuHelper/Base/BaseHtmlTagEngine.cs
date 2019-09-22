using MvcCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCodeFirst.Utility.MenuHelper.Base
{
    public abstract class BaseHtmlTagEngine<T>
    {
        protected int _CntNumber = 0;
        TagContainer _TopTagContainer;
        string _OutString;
        protected HtmlHelper _htmlHelper;
        public BaseHtmlTagEngine(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public TagContainer TopTagContainer
        {
            get { return _TopTagContainer; }
        }

        //public string Build()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            // 获取第一个叶节点 
        //            TagContainer tc = GetNoChildNode(_TopTagContainer);
        //            bool PrcComplete = false;
        //            Levelup(tc, ref PrcComplete);
        //            if (PrcComplete)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return _OutString;
        //}


        ////递归执行移除分支扫描树
        //private void Levelup(TagContainer tc, ref bool ProcessingComplete)
        //{
        //    while (tc != null)
        //    {
        //        if (tc.ParentContainer != null)
        //        {
        //            if (tc.ParentContainer.Tb != null)
        //            {
        //                tc.ParentContainer.Tb.InnerHtml += tc.Tb.ToString();
        //                _OutString = tc.ParentContainer.Tb.ToString();
        //            }
        //            else
        //            {
        //                ProcessingComplete = true;
        //                break; //dummy or invalid container 
        //            }
        //            if (tc.ParentContainer.ChildrenContainers.Count > 1)
        //            {
        //                tc.ParentContainer.ChildrenContainers.Remove(tc);
        //                break;
        //            }
        //            tc = tc.ParentContainer; // moving up the tree 
        //        }
        //        else
        //        {
        //            ProcessingComplete = true;
        //            break;
        //        }
        //    }
        //}
    }
}