using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Income;

namespace Yaba.Entities.Budget
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
				.Include(b => b.Categories)
				.Include(b => b.Incomes)
				.FirstOrDefault(b => b.Id == id);
			if (budget == null) return null;
			return new BudgetDetailsDto
			{
				Id = budget.Id,
				Name = budget.Name,

				Categories = budget.Categories
					.Select(c => new CategorySimpleDto { Id = c.Id, Name = c.Name} )
					.ToList(),

				Incomes = budget.Incomes
					.Select(i => new IncomeSimpleDto { Id = i.Id, Name = i.Name })
					.ToList(),
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
