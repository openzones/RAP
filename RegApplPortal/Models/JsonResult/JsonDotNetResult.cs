﻿using Newtonsoft.Json;
using RegApplPortal.WebApps.Models.Core;
using System;
using System.Web.Mvc;

namespace RegApplPortal.WebApps.Models.JsonResult
{
	public class JsonDotNetResult : System.Web.Mvc.JsonResult
	{
		private static readonly JsonSerializerSettings Settings = UIConstants.DefaultJsonSerializerSettings;

		public override void ExecuteResult(ControllerContext context)
		{
			if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
				string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("GET request not allowed.");
			}

			var response = context.HttpContext.Response;

			response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

			if (this.ContentEncoding != null)
			{
				response.ContentEncoding = this.ContentEncoding;
			}

			if (this.Data == null)
			{
				return;
			}

			response.Write(JsonConvert.SerializeObject(this.Data, Settings));
		}
	}
}