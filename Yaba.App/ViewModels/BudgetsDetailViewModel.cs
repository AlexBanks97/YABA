using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Yaba.App.Models;
using Yaba.Common;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Goal;

namespace Yaba.App.ViewModels
{
	public class BudgetsDetailViewModel : ViewModelBase
	{
		private readonly IBudgetRepository _budgetRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IGoalRepository _goalRepository;

		private Guid _budgetId;

		public ObservableCollection<Recurrence> Recurrences { get; }
		

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

		public CategoryCreateViewModel NewCategoryVM { get; } = new CategoryCreateViewModel();

		public ObservableCollection<CategoryGoalDto> Categories { get; set; }

		public ICommand KeyDownCommand { get; }
		public ICommand AddCategory { get;  }

		public BudgetsDetailViewModel(IBudgetRepository budgetRepository, ICategoryRepository categoryRepository, IGoalRepository goalRepository)
		{
			_budgetRepository = budgetRepository;
			_categoryRepository = categoryRepository;
			_goalRepository = goalRepository;

			Categories = new ObservableCollection<CategoryGoalDto>();
			Recurrences = new ObservableCollection<Recurrence>();

			foreach (var rec in Enum.GetNames(typeof(Recurrence)))
			{
				var t = (Recurrence) Enum.Parse(typeof(Recurrence), rec);
				Recurrences.Add(t);
			}

			AddCategory = new RelayCommand(async e =>
			{
				if (!(e is CategoryCreateViewModel cvm)) return;

				Debug.WriteLine(cvm);

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
