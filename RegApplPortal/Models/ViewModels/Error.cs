using RegApplPortal.Entities.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegApplPortal.WebApps.Models.ViewModels
{
	public class Error : BaseViewModel
	{
		public Error(ExceptionDetails exceptionDetails)
			: base(exceptionDetails)
		{
		}
	}
}