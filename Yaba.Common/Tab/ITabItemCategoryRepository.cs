using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTO.TabDTOs;
using Yaba.Common.Tab.TabItemDTOs;

namespace Yaba.Common.Tab
{
    public interface ITabItemCategoryRepository : IDisposable
    {
		Task<TabItemCategoryDTO> Find(Guid id); // Find from TabItemCategory Id.
		Task<TabItemCategoryDTO> FindFromTabItemId(Guid tabItemID);
		Task<Guid> Create(TabItemCategoryCreateDTO category);
		Task<bool> Update(TabItemCategoryDTO category);
		Task<bool> Delete(TabItemCategoryDTO category);
	}
}
