using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Yaba.App.Models;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.App.ViewModels
{
	public class BudgetsDetailViewModel : ViewModelBase
	{
		private readonly IBudgetRepository _budgetRepository;
		private readonly ICategoryRepository _categoryRepository;

		private Guid _budgetId;

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

		public ICommand KeyDownCommand { get; }

		public BudgetsDetailViewModel(IBudgetRepository budgetRepository, ICategoryRepository categoryRepository)
		{
			_budgetRepository = budgetRepository;
			_categoryRepository = categoryRepository;

			Categories = new ObservableCollection<CategoryGoalDto>();

			KeyDownCommand = new RelayCommand(async e =>
			{
				if (!(e is KeyRoutedEventArgs ke)) return;
				if (ke.Key != VirtualKey.Enter) return;
				if (!(ke.OriginalSource is TextBox textBox)) return;

				var name = textBox.Text;
				if (string.IsNullOrWhiteSpace(name)) return;
				var category = new CategoryCreateDto
				{
					Name = name,
					BudgetId = _budgetId
				};
				await _categoryRepository.Create(category);
				await Initialize(_budgetId);
				textBox.Text = "";
			});
		}

		public async Task Initialize(Guid budgetId)
		{
			var budget = await _budgetRepository.Find(budgetId);
			if (budget == null) return;

			Name = budget.Name;
			_budgetId = budgetId;

			Categories.Clear();
			Categories.AddRange(budget.Categories);
		}
	}
}
