using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.Category;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;
using Yaba.Entities.BudgetEntities;

namespace Yaba.Entities
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
                       where category.Budget.Id == budgetId
                       select new CategorySimpleDto {
                            Id = category.Id,
                            Name = category.Name,
                        };
            return cats.ToList();
        }

        public async Task<CategoryDetailsDto> Find(Guid id)
        {
            var category = await _context.BudgetCategories
                .SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            return new CategoryDetailsDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<Guid?> Create(CategoryCreateDto category)
        {
            var entity = new BudgetCategory
            {
                Name = category.Name,
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