using System;
using System.Collections.Generic;
using System.Text;

namespace Yaba.Common.DTOs.TabDTOs
{
    public class TabUpdateDTO
    {
        public Guid Id { get; set; }
        public decimal? Balance { get; set; }
        public State? State { get; set; }
    }
}
