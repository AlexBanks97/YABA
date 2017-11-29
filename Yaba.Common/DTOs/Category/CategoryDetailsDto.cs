using System;
using Yaba.Common.DTOs.BudgetDTOs;

namespace Yaba.Common.DTOs.Category
{
    public class CategoryDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public BudgetEntryDTO Entries { get; set; }
    }
}