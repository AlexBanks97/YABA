using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.Common.Budget
{
    public interface ICategoryRepository : IDisposable
    {
        Task<ICollection<CategorySimpleDto>> Find();
        Task<ICollection<CategorySimpleDto>> FindFromBudget(Guid budgetId);
        Task<CategoryDetailsDto> Find(Guid id);
        Task<Guid?> Create(CategoryCreateDto category);
        Task<bool> Update(CategorySimpleDto category);
        Task<bool> Delete(Guid id);
    }
}