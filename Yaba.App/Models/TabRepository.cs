using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Yaba.App.Services;
using Yaba.Common;
using Yaba.Common.Tab.DTO;

namespace Yaba.App.Models
{
	public class TabRepository : ITabRepository
	{
		private readonly HttpClient _client;

		public TabRepository(DelegatingHandler handler, AppConstants constants)
		{
			_client = new HttpClient(handler)
			{
				BaseAddress = constants.BaseApiAddress,
			};
		}

		public void Dispose()
		{
		}

		public async Task<TabDto> FindTab(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<Guid> CreateTab(TabCreateDto tab)
		{
			var response = await _client.PostAsync("tabs", tab.ToHttpContent());
			if (!response.IsSuccessStatusCode) throw new Exception();
			return Guid.Empty;
		}

		public async Task<bool> UpdateTab(TabUpdateDto tab)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<TabDto>> FindAllTabs()
		{
			var response = await _client.GetAsync("tabs");
			if (!response.IsSuccessStatusCode) throw new Exception();
			return await response.Content.To<ICollection<TabDto>>();
		}

		public async Task<bool> Delete(Guid id)
		{
			var response = await _client.DeleteAsync($"tabs/{id.ToString()}");
			return response.IsSuccessStatusCode;
		}
	}
}
