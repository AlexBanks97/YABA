using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;

namespace Yaba.UWPApp.Models
{
	public class RestBudgetRepository : IBudgetRepository
	{
		private readonly HttpClient _client;

		public RestBudgetRepository(DelegatingHandler handler)
		{
			_client = new HttpClient(handler)
			{
				BaseAddress = new Uri("http://localhost:50150/"),
			};
		}

		public void Dispose()
		{
			_client.Dispose();
		}

		public Task<BudgetDetailsDto> Find(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<BudgetSimpleDto>> All()
		{
			var response = await _client.GetAsync("api/budgets");

			if (response.IsSuccessStatusCode)
			{
				var budgets = await response.Content.To<ICollection<BudgetSimpleDto>>();
				return budgets;
			}
			throw new Exception();
		}

		public Task<ICollection<BudgetSimpleDto>> AllByUser(string userId)
		{
			throw new NotImplementedException();
		}

		public Task<Guid> Create(BudgetCreateUpdateDto budget)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Update(BudgetCreateUpdateDto budget)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Delete(Guid budgetId)
		{
			throw new NotImplementedException();
		}
	}
}
