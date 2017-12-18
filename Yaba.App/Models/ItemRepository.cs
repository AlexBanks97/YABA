using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Yaba.App.Services;
using Yaba.Common;
using Yaba.Common.Tab.DTO.Item;

namespace Yaba.App.Models
{
	public class ItemRepository : IItemRepository
	{
		private readonly HttpClient _client;
		public ItemRepository(DelegatingHandler handler, AppConstants constants)
		{
			_client = new HttpClient(handler)
			{
				BaseAddress = constants.BaseApiAddress,
			};
		}
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public async Task<Guid> Create(TabItemCreateDTO tabItemDTO)
		{
			var response = await _client.PostAsync("tabitems", tabItemDTO.ToHttpContent());
			if (!response.IsSuccessStatusCode) throw new Exception();
			return await response.Content.To<Guid>();
		}

		public async Task<TabItemSimpleDTO> Find(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<TabItemSimpleDTO>> FindFromTab(Guid tabId)
		{
			var response = await _client.GetAsync($"tabitems/{tabId}");
			if (!response.IsSuccessStatusCode) throw new Exception();
			return await response.Content.To<IEnumerable<TabItemSimpleDTO>>();
		}

		public async Task<bool> Update(TabItemSimpleDTO tabItemDTO)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
