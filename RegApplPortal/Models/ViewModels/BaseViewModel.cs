using RegApplPortal.Entities.Core.Exceptions;

namespace RegApplPortal.WebApps.Models.ViewModels
{
	public abstract class BaseViewModel
	{
		public BaseViewModel(ExceptionDetails exceptionDetails)
		{
			var modelState = new CustomObject();

			modelState.Add("HasErrors", true);
			modelState.Add("ErrorMessage", exceptionDetails.Message);
			modelState.Add("BaseExceptionMessage", exceptionDetails.BaseExceptionMessage);

			ModelState = modelState.ToExpando();
		}

		public BaseViewModel(CustomObject directories)
		{
			Directories = directories?.ToExpando();
		}

		public dynamic Directories { get; private set; }
		public dynamic ModelState { get; private set; }
	}
}