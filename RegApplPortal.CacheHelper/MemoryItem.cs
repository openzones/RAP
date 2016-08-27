using System;
using System.Threading.Tasks;

namespace RegApplPortal.CacheHelper
{
	// todo -сделать абстрактный класс пожирнее, перенести из IISAppMemoryItems
	public abstract class MemoryItem<T> : IRemoteItem<T> //where T : class
	{
		protected string _key;
		public MemoryItem(string objKey)
		{
			if (typeof(T).IsValueType)
				if (default(T) != null)
					throw new InvalidOperationException("Only nullable structs supported for arguments");
			this._key = objKey;
		}
		public string Key
		{
			get
			{
				return _key;
			}
		}

		public abstract T GetItem();

		public abstract Task<T> GetItemAsync();
	}
}
