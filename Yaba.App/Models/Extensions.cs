using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Yaba.App.Models
{
	public static class Extensions
	{
		public static HttpContent ToHttpContent<T>(this T obj)
		{
			var json = JsonConvert.SerializeObject(obj);
			var content = new StringContent(json);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return content;
		}

		public static async Task<T> To<T>(this HttpContent content)
		{
			var json = await content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(json);
		}

		public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				oc.Add(item);
			}
		} 
	}
}
