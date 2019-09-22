using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCodeFirst.Utility.MenuHelper
{
    public static class MenuHelper
    {
        public static string SayHi(this HtmlHelper helper)
        {
            return "Hello Word";
        }

        public static MvcHtmlString GetGetMenuHtml(this HtmlHelper helper, string menuName)
        {
            return MvcHtmlString.Create("");//解决直接显示html标记
        }
    }
}