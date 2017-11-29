using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yaba.Common.DTOs.Category;

namespace Yaba.Common
{
    public interface ICategoryRepository : IDisposable
    {
        Task<ICollection<CategorySimpleDto>> Find();
        Task<CategoryDetailsDto> Find(Guid id);
        Task<Guid?> Create(CategoryCreateDto category);
        Task<bool> Update(CategorySimpleDto category);
        Task<bool> Delete(Guid id);
    }
}