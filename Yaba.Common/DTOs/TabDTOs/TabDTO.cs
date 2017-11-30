using System;
using System.Collections.Generic;
using System.Text;
using Yaba.Common;

namespace Yaba.Common.DTOs.TabDTOs
{
    public class TabDTO
    {
        public decimal Balance { get; set; }
        public IEnumerable<TabItemSimpleDTO> TabItems { get; set; }
        public State State { get; set; }

    }
}
