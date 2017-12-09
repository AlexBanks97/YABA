using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.Budget.DTO.Goal;

namespace Yaba.Common.Budget
{
    public interface IGoalRepository : IDisposable
    {
		Task<ICollection<GoalDto>> Find();

		Task<GoalDto> FindFromCategory(Guid CategoryId);

		Task<GoalDto> Find(Guid GoalId);

		Task<Guid> CreateGoal(GoalCreateDto goal);

		Task<bool> UpdateGoal(GoalDto goal);

		Task<bool> DeleteGoal(Guid Id);
	}
}
