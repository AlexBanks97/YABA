using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.DTOs.BudgetDTOs;

namespace Yaba.Common
{
    public interface IBudgetEntryRepository
    {
        Task<ICollection<BudgetEntryDTO>> Find();

        Task<ICollection<BudgetEntryDTO>> FindFromBudgetCategory(Guid BudgetCategoryId);

        Task<BudgetEntryDTO> Find(Guid BudgetEntryId);

        Task<Guid> CreateBudgetEntry(BudgetEntryDTO entry);

        Task<bool> UpdateBudgetEntry(BudgetEntryDTO entry);

        Task<bool> DeleteBudgetEntry(Guid Id);
    }
}
