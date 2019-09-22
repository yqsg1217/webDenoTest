using MvcCodeFirst.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MvcCodeFirst.App_Start
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string roles = "";
            if (!string.IsNullOrWhiteSpace(roles))
            {
                this.AuthRoles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            base.OnAuthorization(filterContext);
        }
        private string[] AuthRoles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentException("HttpContext");
            }
            if (AuthRoles == null || AuthRoles.Length == 0)
            {
                return true;
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            #region 确定当前用户角色是否是指定的角色
            string query = "select RoleName from SysRole where id in (select id from SysUserRole where SysUserID=(select id from SysUser where LoginName=@userName))";
            string userName = httpContext.User.Identity.Name;


            DAL.AccountContext db = new AccountContext();
            SqlParameter[] paras = new SqlParameter[]
          {
                new SqlParameter("@userName",userName)
          };
            var useRoles = db.Database.SqlQuery<string>(query, paras).ToList();
            for (int i = 0; i < AuthRoles.Length; i++)
            {
                if (useRoles.Contains(AuthRoles[i]))
                {
                    return true;
                }
            }

            #endregion
            return false;
            // return base.AuthorizeCore(httpContext);
        }

        public static string GetActionRoles(string action, string controller)
        {
            XElement rootElement = XElement.Load(HttpContext.Current.Server.MapPath("~/Config/") + "ActionRoles.xml");
            XElement controllerElement = FindElementByAttribute(rootElement, "Controller", controller);
            if (controllerElement != null)
            {
                XElement actionElement = FindElementByAttribute(controllerElement, "Action", action);
                if (actionElement != null)
                {
                    return actionElement.Value;
                }
            }
            return "";
        }

        public static XElement FindElementByAttribute(XElement xElement, string tagName, string attribute)
        {
            return xElement.Elements(tagName).FirstOrDefault(x => x.Attribute("name").Value.Equals(attribute, StringComparison.OrdinalIgnoreCase));
        }
    }
}