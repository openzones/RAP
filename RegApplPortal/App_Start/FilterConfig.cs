using RegApplPortal.WebApps.Security;
using System.Web.Mvc;

namespace RegApplPortal.WebApps
{
    public class FilterConfig
    {
        internal static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeUserAttribute());
        }
    }
}
