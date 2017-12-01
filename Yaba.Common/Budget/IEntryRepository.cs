using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.Common.Budget
{
	public interface IEntryRepository : IDisposable
	{
		Task<ICollection<EntryDto>> Find();

		Task<ICollection<EntryDto>> FindFromBudgetCategory(Guid BudgetCategoryId);

		Task<EntryDetailsDto> Find(Guid BudgetEntryId);

		Task<Guid> CreateBudgetEntry(EntryCreateDto entry);

		Task<bool> UpdateBudgetEntry(EntryDto entry);

		Task<bool> DeleteBudgetEntry(Guid Id);
	}
}
