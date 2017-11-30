using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.TabDTOs
{
    public class TabItemDTO
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TabDTO Tab { get; set; }
        public TabCategoryDTO Category { get; set; }
    }

}
