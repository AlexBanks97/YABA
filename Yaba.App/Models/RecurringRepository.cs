using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Yaba.App.Services;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.App.Models
{
	public class RecurringRepository : RestBaseRepository, IRecurringRepository
	{
		public RecurringRepository(DelegatingHandler handler, AppConstants constants) : base(handler, constants)
		{
		}

		public void Dispose()
		{
			
		}

		public async Task<RecurringSimpleDto> FindBudgetRecurring(Guid budgetRecurringId)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<RecurringSimpleDto>> FindAllBudgetRecurringsFromSpecificBudget(BudgetDto BudgetId)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<RecurringSimpleDto>> FindAllBudgetRecurrings()
		{
			throw new NotImplementedException();
		}

		public async Task<RecurringSimpleDto> CreateBudgetRecurring(RecurringCreateDto recurring)
		{
			var response = await Client.PostAsync("recurring", recurring.ToHttpContent());
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.To<RecurringSimpleDto>();
			}
			return null;
		}

		public async Task<bool> DeleteBudgetRecurring(Guid budgetRecurring)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> UpdateBudgetRecurring(RecurringUpdateDto recurring)
		{
			throw new NotImplementedException();
		}
	}
}
