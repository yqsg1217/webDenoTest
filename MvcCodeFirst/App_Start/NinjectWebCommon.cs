using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFirst.App_Start
{
    public static class NinjectWebCommon
    {
        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new MvcCodeFirst.NinjectDependencyResolver(kernel));
        }
    }
}