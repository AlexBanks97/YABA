using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yaba.Common;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Tab.DTO;

namespace Yaba.UWPApp.Models.Repositories
{
    public class TabRepository : ITabRepository
    {
	    public void Dispose()
	    {
		    throw new NotImplementedException();
	    }

	    public async Task<TabDto> FindTab(Guid id)
	    {
		    throw new NotImplementedException();
	    }

	    public async Task<Guid> CreateTab(TabCreateDto tab)
	    {
		    throw new NotImplementedException();
	    }

	    public async Task<bool> UpdateTab(TabUpdateDto tab)
	    {
		    throw new NotImplementedException();
	    }

	    public async Task<ICollection<TabDto>> FindAllTabs()
	    {
			var client = new HttpClient();
		    client.DefaultRequestHeaders.Accept.Clear();
		    var stringTask = await client.GetAsync(ApiAddress.Address + "tabs");
		    stringTask.EnsureSuccessStatusCode();
		    var response = await stringTask.Content.ReadAsStringAsync();
		    var tabs = JsonConvert.DeserializeObject<ICollection<TabDto>>(response);
		    return tabs;
		}

	    public async Task<bool> Delete(Guid id)
	    {
		    throw new NotImplementedException();
	    }
    }
}
