using System.Threading.Tasks;

namespace RegApplPortal.CacheHelper
{
	public interface IRemoteItem<T>
	{
		T GetItem();

		Task<T> GetItemAsync();

		string Key
		{
			get;
		}
	}
}
