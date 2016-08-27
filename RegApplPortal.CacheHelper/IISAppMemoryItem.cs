using System;
using System.Threading.Tasks;
using System.Web.Caching;

namespace RegApplPortal.CacheHelper
{
	public static class IISAppMemoryItem
	{
		/// <summary>
		/// создает объект с меньшим временем жизни, который быстрее будет освобождать памятЬ, чем данные сессии
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objKey"></param>
		/// <param name="cacheDurationInMinutes"></param>
		/// <returns></returns>
		public static IISAppMemoryItem<T> CreateLargeUserDataItem<T>(string objKey, int cacheDurationInMinutes = 30)
		{
			return new IISAppMemoryItem<T>(objKey, true, cacheDurationInMinutes);
		}
		/// <summary>
		/// объект без slidingexpiration с большим временем жизни. подходит для кэша статических данных.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objKey"></param>
		/// <param name="cacheDurationInMinutes"></param>
		/// <returns></returns>
		public static IISAppMemoryItem<T> CreateStaticCacheDataItem<T>(string objKey, int cacheDurationInMinutes = 120)
		{
			return new IISAppMemoryItem<T>(objKey, true, cacheDurationInMinutes);
		}
	}
	/// <summary>
	/// контроль над тем, как загружается содержимое - на стороне клиента. просто сохраняет\поднимает данные из кэша
	/// по дефолту настройки под хранение сессии юзера
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class IISAppMemoryItem<T> : MemoryItem<T> // where T : class, System.Nullable
	{
		private readonly TimeSpan _cachingDuration;
		private readonly bool _isSlidingExpiration;

		public IISAppMemoryItem(string objKey, bool isSlidingExpiration = true, int cacheDurationInMinutes = 60) : base(objKey)
		{
			_cachingDuration = TimeSpan.FromMinutes(cacheDurationInMinutes);
			_isSlidingExpiration = isSlidingExpiration;
			_loadedObj = this.GetFromCache();
		}
		private T _loadedObj;
		public IISAppMemoryItem(string objKey, T memObject) : base(objKey)
		{
			_loadedObj = memObject;
			this.SaveToCahce(memObject);
		}

		public T MemoryObject
		{
			get
			{
				return _loadedObj;
			}
			set
			{
				_loadedObj = value;
			}
		}

		public void Reset()
		{
			this._loadedObj = this.GetFromCache();
		}

		public void SaveChanges()
		{
			this.SaveToCahce(_loadedObj);

		}
		protected void SaveToCahce(T obj)
		{
			System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
			if (cache != null)
			{
				if (this._isSlidingExpiration)
				{
					cache.Insert(this.Key, obj, null, Cache.NoAbsoluteExpiration, _cachingDuration);
				}
				else
				{
					//todo - прописать куда-нибудь в константы этот метод
					cache.Insert(this.Key, obj, null, DateTime.Now.Add(_cachingDuration), Cache.NoSlidingExpiration);
				}
				//cache.Insert(this.Key, obj, null, Cache.NoAbsoluteExpiration, _cachingDuration);
			}
			else
				throw new InvalidOperationException("Cannot access to server Cache object");
		}
		protected T GetFromCache()
		{
			System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
			if (cache != null)
			{
				return (T)cache.Get(this.Key);
			}
			else
				throw new InvalidOperationException("Cannot access to server Cache object");
		}

		public override T GetItem()
		{
			return this.GetFromCache();
		}

		public override Task<T> GetItemAsync()
		{
			throw new NotImplementedException();
		}
	}
}
