using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.Tab.DTO.ItemCategory;

namespace Yaba.Common.Tab
{
    public interface IItemCategoryRepository : IDisposable
    {
		Task<TabItemCategoryDTO> Find(Guid id); // Find from TabItemCategory Id.
		Task<TabItemCategoryDTO> FindFromTabItemId(Guid tabItemID);
		Task<Guid> Create(TabItemCategoryCreateDTO category);
		Task<bool> Update(TabItemCategoryDTO category);
		Task<bool> Delete(Guid categoryId);
	}
}
