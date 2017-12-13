using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.UWPApp.ViewModels
{
    public class BudgetDetailViewModel : BaseViewModel
    {
		private readonly IBudgetRepository repository;

		public Guid Id { get; set; }
		public string Name { get; set; }
		public decimal RecurringTotal { get; set; }
		public decimal CategoriesTotal { get; set; }
		public ICollection<CategoryGoalDto> Categories { get; set; }
		public ICollection<RecurringSimpleDto> Recurrings { get; set; }

		public BudgetDetailViewModel(IBudgetRepository repository)
		{
			this.repository = repository;
		}

		public async Task Initialize(BudgetSimpleDto dto)
		{
			Id = dto.Id;
			Name = dto.Name;

			//TODO get details and calculate totals

			var details = await repository.Find(Id);

			Categories = details.Categories;
			Recurrings = details.Recurrings;

			calculateRecurringsTotal();
			calculateCategoriesTotal();
		}

	    private void calculateCategoriesTotal()
	    {
		    CategoriesTotal = Categories.Sum(c => c.Balance);
	    }

	    private void calculateRecurringsTotal()
	    {
		    RecurringTotal = Recurrings.Sum(r => r.Amount);
	    }
	}
}
