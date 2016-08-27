using RegApplPortal.Entities;
using RegApplPortal.Entities.Core;
using RegApplPortal.Entities.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegApplPortal.WebApps.Models.ViewModels
{
	public sealed class StatementList : BaseViewModel 
	{
		public StatementList(CustomObject cachedDirectory)
			: base(cachedDirectory)
		{
		}

		public StatementList(ExceptionDetails exceptionDetails)
			: base(exceptionDetails)
		{
		}

		public Statement Statement { get; set; }
		public StatementStatus Status { get; set; }
		public List<User> Users { get; set; }
		public User CurrentUser { get; set; }
		public File File { get; set; }
		public Execution Execution { get; set; }
		public List<Role> Roles { get; set; }
    }
}