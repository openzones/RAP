using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegApplPortal.Entities.Core.Exceptions
{
	public class ExceptionDetails
	{
		private Exception _exception;
		private string _message;

		public ExceptionDetails(Exception ex)
		{
			_exception = ex;
		}

		public ExceptionDetails(Exception ex, string message)
		{
			_exception = ex;
			_message = message;
		}

		public bool HasErrors
		{
			get { return true; }
		}

		public string Type
		{
			get { return _exception.GetType().FullName; }
		}

		public string Message
		{
			get { return (!string.IsNullOrWhiteSpace(_message)) ? _message : "Непредвиденная ошибка."; }
		}

		public string BaseExceptionMessage
		{
			get { return _exception.GetBaseException().Message; }
		}
	}
}
