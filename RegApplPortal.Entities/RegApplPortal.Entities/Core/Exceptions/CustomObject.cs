using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegApplPortal.Entities.Core.Exceptions
{
	public class CustomObject : Dictionary<string, dynamic>
	{
		public CustomObject() : base()
		{
		}

		public CustomObject(IDictionary<string, dynamic> properties) : base(properties)
		{
		}

		public ExpandoObject ToExpando()
		{
			var expando = new ExpandoObject();
			var expandoDic = (IDictionary<string, object>)expando;

			foreach (var item in this)
			{
				var value = (item.Value is CustomObject) ? item.Value.ToExpando() : item.Value;
				expandoDic.Add(item.Key, value);
			}

			return expando;
		}
	}
}
