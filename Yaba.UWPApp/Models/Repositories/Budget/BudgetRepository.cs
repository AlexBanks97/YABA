using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;

namespace Yaba.UWPApp.Models.Repositories
{
	public class BudgetRepository : IBudgetRepository
	{


		public Task<BudgetDetailsDto> Find(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<BudgetSimpleDto>> All()
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			var stringTask = await client.GetAsync(ApiAddress.Address + "budgets");
			stringTask.EnsureSuccessStatusCode();
			var response = await stringTask.Content.ReadAsStringAsync();
			var budgets = JsonConvert.DeserializeObject<ICollection<BudgetSimpleDto>>(response);
			return budgets;
		}

		public async Task<Guid> Create(BudgetCreateUpdateDto budget)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Update(BudgetCreateUpdateDto budget)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(Guid budgetId)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
