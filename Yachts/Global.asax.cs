using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DotNetEnv;

namespace Yachts
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // BaseDirectory -- base directory of the "Yachts" project
            // We need to pass this option, because ASP.NET MVC projects are executed in the context of IIS
            // TraversePath() searches for .env in parent directories (e.g. ../, ../.., etc.)
            // DotNetEnv.Env is a NuGet library
            Env.TraversePath().Load(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}