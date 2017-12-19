using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Yaba.App.Services;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.App.Models
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly HttpClient _client;

		public CategoryRepository(DelegatingHandler handler, AppConstants constants)
		{
			_client = new HttpClient(handler)
			{
				BaseAddress = constants.BaseApiAddress,
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
			var response = await _client.GetAsync($"budgets/categories?budgetId={budgetId.ToString()}");
			if (!response.IsSuccessStatusCode) throw new Exception();
			return await response.Content.To<ICollection<CategorySimpleDto>>();
		}

		public async Task<CategoryDetailsDto> Find(Guid id)
		{
			var response = await _client.GetAsync($"budgets/categories/{id.ToString()}");
			if (!response.IsSuccessStatusCode) throw new Exception();
			return await response.Content.To<CategoryDetailsDto>();
		}

		public async Task<Guid?> Create(CategoryCreateDto category)
		{
			var response = await _client.PostAsync("budgets/categories", category.ToHttpContent());
			if (response.IsSuccessStatusCode)
			{
				var stringGuid = response.Headers.GetValues("Location")
					.First()
					.Split("/")
					.Last();
				return Guid.Parse(stringGuid);
			}
			throw new Exception();
		}

		public async Task<bool> Update(CategorySimpleDto category)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(Guid id)
		{
			var response = await _client.DeleteAsync($"budgets/categories/{id.ToString()}");
			return response.IsSuccessStatusCode;
		}
	}
}
