using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTOs.TabDTOs;

namespace Yaba.Common
{
    public interface ITabRepository : IDisposable
    {
        Task<TabDTO> FindTab(int id);
    }
}
