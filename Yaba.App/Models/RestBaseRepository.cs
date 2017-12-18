using System.Net.Http;
using Yaba.App.Services;

namespace Yaba.App.Models
{
	public abstract class RestBaseRepository
	{
		protected HttpClient Client;

		protected RestBaseRepository(DelegatingHandler handler, AppConstants constants)
		{
			Client = new HttpClient(handler)
			{
				BaseAddress = constants.BaseApiAddress,
			};
		}
	}
}
