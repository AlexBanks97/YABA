using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;

namespace Yaba.UWPApp.ViewModels
{
	public class BudgetOverviewViewModel : BaseViewModel
	{
		private readonly IBudgetRepository _repository;
		public ObservableCollection<BudgetSimpleDto> Budgets { get; set; }

		public BudgetOverviewViewModel(IBudgetRepository repository)
		{
			_repository = repository;
			Budgets = new ObservableCollection<BudgetSimpleDto>();
		}

		public async Task Initialize()
		{
			var budgets = await _repository.All();
			foreach (var budget in budgets)
			{
				Budgets.Add(budget);
			}
		}



	}
}
