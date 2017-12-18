using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Yaba.App.Services;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Goal;

namespace Yaba.App.Models
{
	public class GoalRepository : IGoalRepository
	{
		private readonly HttpClient _client;

		public GoalRepository(DelegatingHandler handler, AppConstants constants)
		{
			_client = new HttpClient(handler)
			{
				BaseAddress = constants.BaseApiAddress,
			};
		}

		public void Dispose()
		{
		}

		public async Task<ICollection<GoalDto>> Find()
		{
			throw new NotImplementedException();
		}

		public async Task<GoalDto> FindFromCategory(Guid CategoryId)
		{
			throw new NotImplementedException();
		}

		public async Task<GoalDto> Find(Guid GoalId)
		{
			throw new NotImplementedException();
		}

		public async Task<Guid> CreateGoal(GoalCreateDto goal)
		{
			var response = await _client.PostAsync("goal", goal.ToHttpContent());
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

		public async Task<bool> UpdateGoal(GoalDto goal)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteGoal(Guid Id)
		{
			throw new NotImplementedException();
		}
	}
}
