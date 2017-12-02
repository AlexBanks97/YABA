using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTO.TabDTOs;
using Yaba.Common.Tab.TabItemDTOs;
using Yaba.Common.Tab.TabItemDTOs.Category;

namespace Yaba.Common.Tab
{
    public interface ITabItemCategoryRepository : IDisposable
    {
		Task<TabItemCategorySimpleDTO> Find(Guid id); // Find from TabItemCategory Id.
		Task<TabItemCategorySimpleDTO> FindFromTabItemId(Guid tabItemID);
		Task<Guid> Create(TabItemCategoryCreateDTO category);
		Task<bool> Update(TabItemCategorySimpleDTO category);
		Task<bool> Delete(TabItemCategorySimpleDTO category);
	}
}
