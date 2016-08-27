using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using RegApplPortal.Entities;
using RegApplPortal.WebApps.Models.ViewModels;

namespace RegApplPortal.WebApps.Models.ConfigurationModels
{
	public static class AutoMapperConfig
	{
		public static void RegisterMappings()
		{
			Mapper.Initialize(cfg =>
			{

            });

#if DEBUG
			Mapper.AssertConfigurationIsValid();
#endif
		}
	}
}