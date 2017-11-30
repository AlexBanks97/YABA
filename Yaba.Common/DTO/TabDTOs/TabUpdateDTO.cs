using System;

namespace Yaba.Common.DTO.TabDTOs
{
    public class TabUpdateDTO
    {
        public Guid Id { get; set; }
        public decimal? Balance { get; set; }
        public State? State { get; set; }
    }
}
