using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Entry;
using Yaba.Common.Budget.DTO.Goal;

namespace Yaba.Entities.Budget.Repository
{
	public class EFCategoryRepository : ICategoryRepository
	{
		private readonly IYabaDBContext _context;

		public EFCategoryRepository(IYabaDBContext context)
		{
			_context = context;
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public async Task<ICollection<CategorySimpleDto>> Find()
		{
			var cats = from category in _context.BudgetCategories
				select new CategorySimpleDto
				{
					Id = category.Id,
					Name = category.Name,
				};
			return cats.ToList();
		}

		public async Task<ICollection<CategorySimpleDto>> FindFromBudget(Guid budgetId)
		{
			var cats = from category in _context.BudgetCategories
						where category.BudgetEntity.Id == budgetId
						select new CategorySimpleDto {
							Id = category.Id,
							Name = category.Name,
						};
			return cats.ToList();
		}

		public async Task<CategoryDetailsDto> Find(Guid id)
		{
			var category = await _context.BudgetCategories
				.Include(c => c.Entries)
				.Include(c => c.GoalEntity)
				.SingleOrDefaultAsync(c => c.Id == id);
			if (category == null)
			{
				return null;
			}
			return new CategoryDetailsDto
			{
				Id = category.Id,
				Name = category.Name,
				Goal = category.GoalEntity?.ToDto(),
				Entries = category.Entries
					.Select(e => new EntrySimpleDto
					{
						Id = e.Id,
						Amount = e.Amount,
						Description = e.Description,
						Date = e.Date,
					})
					.ToList(),
			};
		}

		public async Task<Guid?> Create(CategoryCreateDto category)
		{
			var budget = await _context.Budgets
				.SingleOrDefaultAsync(b => b.Id == category.BudgetId);

			if (budget == null)
			{
				return null;
			}

			var entity = new CategoryEntity
			{
				Name = category.Name,
				BudgetEntity = budget,
			};

			_context.BudgetCategories.Add(entity);
			await _context.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<bool> Update(CategorySimpleDto category)
		{
			var entity = await _context.BudgetCategories
				.SingleOrDefaultAsync(c => c.Id == category.Id);
			if (entity == null)
			{
				return false;
			}
			entity.Name = category.Name ?? entity.Name;
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> Delete(Guid id)
		{
			var entity = await _context.BudgetCategories
				.SingleOrDefaultAsync(c => c.Id == id);
			if (entity == null)
			{
				return false;
			}
			_context.BudgetCategories.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
