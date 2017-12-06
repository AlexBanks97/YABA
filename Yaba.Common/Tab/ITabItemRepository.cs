using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTO.TabDTOs;

namespace Yaba.Common
{
	public interface ITabItemRepository : IDisposable
	{
		Task<Guid> Create(TabItemCreateDTO tabItemDTO);
		Task<TabItemSimpleDTO> Find(Guid id);
		Task<IEnumerable<TabItemSimpleDTO>> FindFromTab(Guid tabId);
		Task<bool> Update(TabItemSimpleDTO tabItemDTO);
		Task<bool> Delete(Guid id);

	}
}
