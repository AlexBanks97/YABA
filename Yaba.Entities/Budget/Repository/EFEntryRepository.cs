using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.Entities.Budget.Repository
{
	public class EFEntryRepository : IEntryRepository
	{
		IYabaDBContext _context;
		public EFEntryRepository(IYabaDBContext context)
		{
			_context = context;
		}
		public async Task<Guid> CreateBudgetEntry(EntryCreateDto entry)
		{
			var budgetCategory = _context.BudgetCategories.SingleOrDefault(e => e.Id == entry.CategoryId);
			var budgetEntry = new EntryEntity
			{
				Amount = entry.Amount,
				Date = entry.Date,
				Description = entry.Description,
				CategoryEntity = budgetCategory
			};
			var result = _context.BudgetEntries.Add(budgetEntry);
			await _context.SaveChangesAsync();
			return budgetEntry.Id;
		}

		public async Task<bool> DeleteBudgetEntry(Guid Id)
		{
			var entry = _context.BudgetEntries.SingleOrDefault(e => e.Id == Id);
			if(entry == null)
			{
				return false;
			}
			_context.BudgetEntries.Remove(entry);
			var NumberOfRowsRemoved = await _context.SaveChangesAsync();
			return true;
		}

		public async Task<ICollection<EntrySimpleDto>> Find()
		{
			return _context.BudgetEntries.Select(b => new EntrySimpleDto
			{
				Id = b.Id,
				Amount = b.Amount,
				Description = b.Description,
				Date = b.Date,
			}).ToList();

		}

		public async Task<EntryDetailsDto> Find(Guid BudgetEntryId)
		{
			return _context.BudgetEntries.Select(b => new EntryDetailsDto
			{
				Id = b.Id,
				Amount = b.Amount,
				Description = b.Description,
				Date = b.Date,
				BudgetCategory = new CategorySimpleDto
				{
					Id = b.CategoryEntity.Id,
					Name = b.CategoryEntity.Name,
				}


			}).Where(b => b.Id == BudgetEntryId).FirstOrDefault();

		}

		public async Task<ICollection<EntryDto>> FindFromBudgetCategory(Guid BudgetCategoryId)
		{
			var cat = _context.BudgetCategories.FirstOrDefault(c => c.Id == BudgetCategoryId);
			if(cat == null)
			{
				return null;
			}
			var BudgetCategoryDTO = new CategoryDto
			{
				Id = cat.Id,
				Name = cat.Name
			};
			var entries = _context.BudgetEntries.Select(b => b).Where(b => b.CategoryEntity.Id == cat.Id);

			var result = new List<EntryDto>();

			foreach(EntryEntity entry in entries)
			{
				result.Add(new EntryDto
				{
					Id = entry.Id,
					Amount = entry.Amount,
					Date = entry.Date,
					Category = BudgetCategoryDTO,
					Description = entry.Description
				});
			}
			return result;
		}

		public async Task<bool> UpdateBudgetEntry(EntryDto entry)
		{
			var entity = _context.BudgetEntries
				.SingleOrDefault(c => c.Id == entry.Id);
			if (entity == null)
			{
				return false;
			}

			if (entry.Category != null) {
				var BudgetCategory = new CategoryEntity {
					Id = entry.Category.Id,
					Name = entry.Category.Name
				};
				entity.CategoryEntity = BudgetCategory;
			}

			entity.Description = entry.Description;
			entity.Amount = entry.Amount;
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
		// ~EFBudgetRepository() {
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
