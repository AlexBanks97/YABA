using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.App.Models
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly HttpClient _client;

		public CategoryRepository(DelegatingHandler handler)
		{
			_client = new HttpClient(handler)
			{
				BaseAddress = new Uri("http://localhost:50150/api/budgets/"),
			};
		}

		public void Dispose()
		{
			
		}

		public async Task<ICollection<CategorySimpleDto>> Find()
		{
			throw new NotImplementedException();
		}

		public async Task<ICollection<CategorySimpleDto>> FindFromBudget(Guid budgetId)
		{
			throw new NotImplementedException();
		}

		public async Task<CategoryDetailsDto> Find(Guid id)
		{
			var response = await _client.GetAsync($"categories/{id.ToString()}");
			if (!response.IsSuccessStatusCode) throw new Exception();
			return await response.Content.To<CategoryDetailsDto>();
		}

		public async Task<Guid?> Create(CategoryCreateDto category)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Update(CategorySimpleDto category)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
