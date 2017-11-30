using System;

namespace Yaba.Common.DTOs.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public Guid BudgetId { get; set; }
    }
}