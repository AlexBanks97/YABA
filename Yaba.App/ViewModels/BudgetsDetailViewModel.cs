using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Yaba.App.Models;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.App.ViewModels
{
	public class BudgetsDetailViewModel : ViewModelBase
	{
		private readonly IBudgetRepository _repository;

		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<CategoryGoalDto> Categories { get; set; }

		public BudgetsDetailViewModel(IBudgetRepository repository)
		{
			_repository = repository;
			Categories = new ObservableCollection<CategoryGoalDto>();
		}

		public async Task Initialize(Guid budgetId)
		{
			var budget = await _repository.Find(budgetId);
			if (budget == null) return;

			Name = budget.Name;
			Categories.Clear();
			Categories.AddRange(budget.Categories);
		}
	}
}
