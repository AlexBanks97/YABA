using System;
using System.Threading.Tasks;
using Yaba.Common.Budget;

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

		public BudgetsDetailViewModel(IBudgetRepository repository)
		{
			_repository = repository;
		}

		public async Task Initialize(Guid budgetId)
		{
			var budget = await _repository.Find(budgetId);
			if (budget == null) return;

			Name = budget.Name;
		}
	}
}
