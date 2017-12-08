using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Goal;
using Microsoft.EntityFrameworkCore;

namespace Yaba.Entities.Budget.Repository
{

	public class EFGoalRepository : IGoalRepository
	{
		IYabaDBContext _context;
		public EFGoalRepository(IYabaDBContext context)
		{
			_context = context;
		}

		public async Task<ICollection<GoalDto>> Find()
		{
			var goals = _context.BudgetGoals.Include(b => b.CategoryEntity);

			var dtos = new List<GoalDto>();

			foreach(GoalEntity goal in goals)
			{
				dtos.Add(new GoalDto
				{
					Id = goal.Id,
					Amount = goal.Amount,
					Category = new CategorySimpleDto
					{
						Id = goal.CategoryEntity.Id,
						Name = goal.CategoryEntity.Name
					},
					Recurrence = goal.Recurrence
				});
			}
			return dtos;
		}

		public async Task<GoalDto> Find(Guid GoalId)
		{
			var goal = _context.BudgetGoals.FirstOrDefault(g => g.Id == GoalId);
			if (goal == null) return null;
			var category = new CategorySimpleDto
			{
				Id = goal.CategoryEntity.Id,
				Name = goal.CategoryEntity.Name,
			};
			var dto = new GoalDto
			{
				Id = goal.Id,
				Amount = goal.Amount,
				Category = category,
				Recurrence = goal.Recurrence
			};
			return dto;
		}

		public async Task<GoalDto> FindFromCategory(Guid CategoryId)
		{
			var goal = _context.BudgetGoals.FirstOrDefault(g => g.CategoryEntity.Id == CategoryId);
			if (goal == null) return null;
			var category = new CategorySimpleDto
			{
				Id = goal.CategoryEntity.Id,
				Name = goal.CategoryEntity.Name,
			};
			var dto = new GoalDto
			{
				Id = goal.Id,
				Amount = goal.Amount,
				Category = category,
				Recurrence = goal.Recurrence
			};
			return dto;
		}

		public async Task<Guid> CreateGoal(GoalCreateDto goal)
		{
			var cat = _context.BudgetCategories.SingleOrDefault(c => c.Id == goal.CategoryEntity.Id);
			var goalEntity = new GoalEntity
			{
				Amount = goal.Amount,
				CategoryEntity = cat,
				Recurrence = goal.Recurrence
			};
			_context.BudgetGoals.Add(goalEntity);
			await _context.SaveChangesAsync();
			return goalEntity.Id;
		}

		public async Task<bool> UpdateGoal(GoalDto goalUpdate)
		{
			var goal = _context.BudgetGoals.SingleOrDefault(g => g.Id == goalUpdate.Id);
			if (goal == null) return false;

			goal.Amount = (goal.Amount == goalUpdate.Amount) ? goal.Amount : goalUpdate.Amount;
			goal.Recurrence = (goal.Recurrence == goalUpdate.Recurrence) ? goal.Recurrence : goalUpdate.Recurrence;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteGoal(Guid Id)
		{
			var goal = _context.BudgetGoals.SingleOrDefault(g => g.Id == Id);
			if (goal == null) return false;
			_context.BudgetGoals.Remove(goal);
			await _context.SaveChangesAsync();
			return true;
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_context.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~EFGoalRepository() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
