using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTOs.BudgetDTOs;
using Yaba.Common.DTOs.BudgetEntry;

namespace Yaba.Common
{
    public interface IBudgetEntryRepository : IDisposable
    {
        Task<ICollection<BudgetEntryDTO>> Find();

        Task<ICollection<BudgetEntryDTO>> FindFromBudgetCategory(Guid BudgetCategoryId);

        Task<BudgetEntryDetailsDto> Find(Guid BudgetEntryId);

        Task<Guid> CreateBudgetEntry(BudgetEntryCreateDto entry);

        Task<bool> UpdateBudgetEntry(BudgetEntryDTO entry);

        Task<bool> DeleteBudgetEntry(Guid Id);
    }
}
