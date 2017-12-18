using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.Tab.DTO;

namespace Yaba.Common
{
	public interface ITabRepository : IDisposable
	{
		Task<TabDto> FindTab(Guid id);
		Task<Guid> CreateTab(TabCreateDto tab);

		Task<bool> UpdateTab(TabUpdateDto tab);

		Task<ICollection<TabDto>> FindAllTabs();
		Task<ICollection<TabDto>> FindWithUser(Guid userId);
		Task<bool> Delete(Guid id);
	}
}
