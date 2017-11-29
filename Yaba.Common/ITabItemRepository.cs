using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTOs.TabDTOs;

namespace Yaba.Common
{
    public interface ITabItemRepository : IDisposable
    {
        Task<Guid> Create(TabItemSimpleDTO tabItemDTO);
        Task<TabItemSimpleDTO> Find(Guid id);
        Task<ICollection<TabItemSimpleDTO>> FindFrom(TabDTO tab);
        Task<bool> Update();

    }
}
