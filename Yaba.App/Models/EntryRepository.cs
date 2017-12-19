using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Yaba.App.Services;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.App.Models
{
	public class EntryRepository : RestBaseRepository, IEntryRepository
	{
		public EntryRepository(DelegatingHandler handler, AppConstants constants) : base(handler, constants)
		{
		}

		public void Dispose()
		{
		}

		public async Task<ICollection<EntrySimpleDto>> Find()
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<EntryDto>> FindFromBudgetCategory(Guid BudgetCategoryId)
		{
			throw new NotImplementedException();
		}

		public async Task<EntryDetailsDto> Find(Guid BudgetEntryId)
		{
			throw new NotImplementedException();
		}

		public async Task<EntrySimpleDto> CreateBudgetEntry(EntryCreateDto entry)
		{
			var response = await Client.PostAsync("budgets/entries", entry.ToHttpContent());
			if (!response.IsSuccessStatusCode) throw new Exception();
			return await response.Content.To<EntrySimpleDto>();
		}

		public async Task<bool> UpdateBudgetEntry(EntryDto entry)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteBudgetEntry(Guid id)
		{
			var response = await Client.DeleteAsync($"budgets/entries/{id.ToString()}");
			return response.IsSuccessStatusCode;
		}
	}
}
