using RegApplPortal.BusinessLogic;
using RegApplPortal.CacheHelper;
using RegApplPortal.Entities.Core.Exceptions;
using RegApplPortal.Interfaces;
using RegApplPortal.WebApps.Models;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Text;
using RegApplPortal.WebApps.Models.JsonResult;

namespace RegApplPortal.WebApps.Controllers
{
	public abstract class RegApplPortalController : Controller
	{
		private static readonly string _cacheLookupsPrefix = "___appchache";
		protected IReferenceBusinessLogic referenceBusinessLogic;
        protected IUserBusinessLogic userBusinessLogic;
		protected IStatementBusinessLogic statementBusinessLogic;
        private Entities.User currentUser;
		protected HttpContextBase httpContext;

		public Entities.User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    currentUser = userBusinessLogic.User_GetByLogin(User.Identity.Name);
                    currentUser = Entities.Core.Role.FillWeightRoles(currentUser);
                }
                return currentUser;
            }
        }

        public RegApplPortalController()
        {
            referenceBusinessLogic = new ReferenceBusinessLogic();
            userBusinessLogic = new UserBusinessLogic();
			statementBusinessLogic = new StatementBusinessLogic(); 
        }

		protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return new JsonDotNetResult
			{
				Data = data,
				ContentType = contentType,
				ContentEncoding = contentEncoding,
				JsonRequestBehavior = behavior
			};
		}

		//protected override void OnException(ExceptionContext filterContext)
		//{
		//    Exception ex = filterContext.Exception;
		//    filterContext.ExceptionHandled = true;
		//    if (!ex.GetType().IsSubclassOf(typeof(ApplicationException)))
		//    {
		//        AsyncLogger.Error(ex);
		//    }

		//    HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");
		//    filterContext.Result = new ViewResult()
		//    {
		//        ViewName = "Error",
		//        ViewData = new ViewDataDictionary(model)
		//    };
		//}

		//protected virtual void FillReferences(
		//    string referenceName,
		//    string selectedValue = null,
		//    bool withDefaultEmpty = false)
		//{
		//    List<SelectListItem> items = ReferencesProvider.GetReferences(referenceName, selectedValue, withDefaultEmpty);
		//    ViewData[referenceName] = items;
		//}

		private static string CreateGlobalCacheKey()
		{
			return _cacheLookupsPrefix;
		}
		// все справочники
		public async Task<CustomObject> GetDirectoryAsync()
		{
			var cacheObj = IISAppMemoryItem.CreateStaticCacheDataItem<CustomObject>(CreateGlobalCacheKey());
			Task cacheGetter = null;
			if (cacheObj.MemoryObject == null)
			{
				cacheGetter = Task.Run(() =>
				{
					lock (cacheObj.Key)
					{
						cacheObj.Reset();
						System.Web.HttpContext.Current = this.HttpContext.ApplicationInstance.Context;
						if (cacheObj.MemoryObject == null)
						{
							var directory = new AppCachedDirectory();
							var result = referenceBusinessLogic.GetAllReference();
							cacheObj.MemoryObject = directory.LoadDirectory(result);
							cacheObj.SaveChanges();
						}
					}
				}
				);
			}
			if (cacheGetter != null)
			{
				await cacheGetter;
			}

			return cacheObj.MemoryObject;
		}
	}
}