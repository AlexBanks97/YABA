using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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

		private string _newBudgetName = "";
		public string NewBudgetName
		{
			get => _newBudgetName;
			set
			{
				_newBudgetName = value;
				OnPropertyChanged();
			}
		}

		public ICommand AddBudgetCommand { get; }
		public ICommand KeyDownCommand { get; }

		public BudgetsViewModel(IBudgetRepository repository, IAuthenticationHelper authenticationHelper)
		{
			_repository = repository;
			_authenticationHelper = authenticationHelper;
			Budgets = new ObservableCollection<BudgetSimpleDto>();

			AddBudgetCommand = new RelayCommand(_ => AddBudget());

			KeyDownCommand = new RelayCommand(async e =>
			{
				if (!(e is KeyRoutedEventArgs ke)) return;
				if (ke.Key != VirtualKey.Enter) return;
				if (!(ke.OriginalSource is TextBox textBox)) return;

				var name = textBox.Text;
				if (string.IsNullOrWhiteSpace(name)) return;
				var budget = new BudgetCreateUpdateDto
				{
					Name = name,
					OwnerId = (await _authenticationHelper.GetAccountAsync())?.AccessToken.Subject,
				};
				await _repository.Create(budget);
				await Initialize();
				textBox.Text = "";
			});
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

		private void AddBudget()
		{

		}
	}
}
