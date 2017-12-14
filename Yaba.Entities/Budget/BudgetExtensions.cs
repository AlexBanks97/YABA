using Yaba.Common.Budget.DTO.Goal;

namespace Yaba.Entities.Budget
{
	public static class BudgetExtensions
	{
		public static GoalSimpleDto ToDto(this GoalEntity entity)
		{
			return new GoalSimpleDto
			{
				Id = entity.Id,
				Amount = entity.Amount,
				Recurrence = entity.Recurrence,
			};
		}
	}
}
