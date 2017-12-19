using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Yaba.App.Models;
using Yaba.App.Views;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;

namespace Yaba.App.ViewModels
{
	public class BudgetsPageViewModel : ViewModelBase
	{
		private readonly IBudgetRepository _repository;
		private readonly IAuthenticationHelper _authenticationHelper;

		public ObservableCollection<BudgetViewModel> Budgets { get; set; }

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

		public Frame Frame { get; set; }

		public ICommand KeyDownCommand { get; }

		public ICommand DeleteBudgetCommand { get; }

		public ICommand BudgetSelectionCommand { get; }

		public BudgetsPageViewModel(IBudgetRepository repository, IAuthenticationHelper authenticationHelper)
		{
			_repository = repository;
			_authenticationHelper = authenticationHelper;
			Budgets = new ObservableCollection<BudgetViewModel>();

			DeleteBudgetCommand = new RelayCommand(async o =>
			{
				if (!(o is BudgetViewModel budget)) return;

				var deleteDialog = new ContentDialog
				{
					Title = $"Delete {budget.Name}",
					Content = $"Are you sure you want to delete the budget \"{budget.Name}\"? This action is irreversible",
					PrimaryButtonText = "Yes, I am sure",
					CloseButtonText = "Cancel",
				};

				var result = await deleteDialog.ShowAsync();

				if (result != ContentDialogResult.Primary) return;
				var deleted = await _repository.Delete(budget.Id);
				if (deleted)
				{
					Budgets.Remove(budget);
				}
			});

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
				var budgetDto = await _repository.Create(budget);
				if (budgetDto != null)
				{
					Frame.Content = null;
					Budgets.Add(new BudgetViewModel(budgetDto));
				}

				textBox.Text = "";
			});

			BudgetSelectionCommand = new RelayCommand(e =>
			{
				Frame?.Navigate(typeof(BudgetDetailsPage), e);
			});
		}

		public async Task Initialize()
		{
			var user = await _authenticationHelper.GetAccountAsync();
			var userId = user?.AccessToken.Subject;
			if (userId == null) return;

			var budgets = (await _repository.AllByUser(userId))
				.Select(b => new BudgetViewModel(b));

			Budgets.Clear();
			Budgets.AddRange(budgets);
		}
	}
}
