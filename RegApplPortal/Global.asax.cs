using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RegApplPortal.Log;
using RegApplPortal.WebApps.Models;
using RegApplPortal.WebApps.Security;
using System.Globalization;
using System.Threading;
using RegApplPortal.WebApps.Models.ConfigurationModels;

namespace RegApplPortal.WebApps
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public MvcApplication()
        {
            AuthenticateRequest += MvcApplication_AuthenticateRequest;
        }

        private void MvcApplication_AuthenticateRequest(object sender, EventArgs e)
        {
            AuthenticationManager.AuthenticateRequest(new HttpContextWrapper(Context));
        }

        protected void Application_Start()
        {
            FileLog.Initialize("RegApplPortal");
            AsyncLogger.Initialize("RegApplPortal");
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			AutoMapperConfig.RegisterMappings();
			//ModelMapper.Configure();
		}
    }
}
