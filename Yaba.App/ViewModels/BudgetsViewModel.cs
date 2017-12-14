using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Yaba.App.Models;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;

namespace Yaba.App.ViewModels
{
	public class BudgetsViewModel : ViewModelBase
	{
		private readonly IBudgetRepository _repository;
		private readonly IAuthenticationHelper _authenticationHelper;

		public ObservableCollection<BudgetSimpleDto> Budgets { get; set; }

		public BudgetsViewModel(IBudgetRepository repository, IAuthenticationHelper authenticationHelper)
		{
			_repository = repository;
			_authenticationHelper = authenticationHelper;
			Budgets = new ObservableCollection<BudgetSimpleDto>();
		}

		public async Task Initialize()
		{
			var user = await _authenticationHelper.GetAccountAsync();
			var userId = user?.AccessToken.Subject;
			if (userId == null) return;

			var budgets = await _repository.AllByUser(userId);
			Budgets.Clear();
			Budgets.AddRange(budgets);
		}
	}
}
