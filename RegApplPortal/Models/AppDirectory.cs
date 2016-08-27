using RegApplPortal.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegApplPortal.WebApps.Models
{
	public class AppDirectory
	{
		public AppDirectory(Dictionary<string, List<ReferenceUniversalItem>> cachedDirectory)
		{
			CommonDirectory = cachedDirectory;
        }
		public Dictionary<string, List<ReferenceUniversalItem>> CommonDirectory { get; set; }
	}
}