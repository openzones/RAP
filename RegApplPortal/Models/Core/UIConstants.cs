using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace RegApplPortal.WebApps.Models.Core
{
	public static class UIConstants
	{
		static UIConstants()
		{
			DefaultJsonSerializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				Converters = new List<JsonConverter> { new StringEnumConverter() }
			};
		}

		public static JsonSerializerSettings DefaultJsonSerializerSettings { get; private set; }
	}
}