using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Photogasm
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterRoutes(RouteTable.Routes);
        }
        static void RegisterRoutes(RouteCollection routes)
        {
            //routes.MapPageRoute("MyPage", "MyPage", "~/MyPage.aspx");
            routes.MapPageRoute("User", "{userUrl}", "~/UserPage.aspx");
        }
    }
}