using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Goal;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Recurring;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.Entities.Budget.Repository
{
	public class EFBudgetRepository : IBudgetRepository
	{
		private readonly IYabaDBContext _context;

		public EFBudgetRepository(IYabaDBContext context)
		{
			_context = context;
		}

		public async Task<BudgetDetailsDto> Find(Guid id)
		{
            var budget = _context.Budgets
                .Include(b => b.Categories).ThenInclude(c => c.GoalEntity)
				.Include(b => b.Recurrings)
				.FirstOrDefault(b => b.Id == id);
			if (budget == null) return null;

            return new BudgetDetailsDto
            {
                Id = budget.Id,
                Name = budget.Name,

                Categories = budget.Categories
                    .Select(c => new CategoryGoalDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Balance = _context.BudgetEntries.Include(b => b.CategoryEntity.Id)
                                  .Where(b => b.CategoryEntity.Id == c.Id)
                                          .Select(ca => ca.Amount).Sum(),

                        Goal = c.GoalEntity == null ? null : new GoalSimpleDto{
                            Id = c.GoalEntity.Id,
                            Amount = c.GoalEntity.Amount,
                            Recurrence = c.GoalEntity.Recurrence
                        } 
                                 
                    }).ToList(),

				Recurrings = budget.Recurrings
					.Select(i => new RecurringSimpleDto { 
                    Id = i.Id, 
                    Name = i.Name,
                    Amount = i.Amount,
                    Recurrence = i.Recurrence
                }).ToList(),
			};
		}

		public async Task<Guid> Create(BudgetCreateUpdateDto budget)
		{
			var budgetEntity = new BudgetEntity
			{
				Name = budget.Name,
			};

			_context.Budgets.Add(budgetEntity);
			await _context.SaveChangesAsync();
			return budgetEntity.Id;
		}

		public async Task<ICollection<BudgetSimpleDto>> All()
		{
			return _context.Budgets.Select(b => new BudgetSimpleDto
			{
				Id = b.Id,
				Name = b.Name,
			}).ToList();
		}

		public async Task<bool> Update(BudgetCreateUpdateDto budget)
		{
			if (budget.Id == null)
			{
				return false;
			}
			var entity = await _context.Budgets.FindAsync(budget.Id);
			if (entity == null)
			{
				return false;
			}

			entity.Name = budget.Name;

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> Delete(Guid budgetId)
		{
			var entity = await _context.Budgets
				.SingleOrDefaultAsync(b => b.Id == budgetId);
			if (entity == null)
			{
				return false;
			}
			_context.Budgets.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public void Dispose()
		{
			_context?.Dispose();
		}
	}
}
