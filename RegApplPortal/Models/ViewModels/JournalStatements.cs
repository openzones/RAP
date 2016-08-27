using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegApplPortal.WebApps.Models.ViewModels
{
	public sealed class JournalStatements : BaseViewModel
	{
		public JournalStatements(CustomObject cachedDirectory)
			: base(cachedDirectory)
		{
		}

		public JournalStatements(ExceptionDetails exceptionDetails)
			: base(exceptionDetails)
		{
		}

		public List<StatementInfo> Journal { get; set; }
		public List<User> Users { get; set; }
	}
}