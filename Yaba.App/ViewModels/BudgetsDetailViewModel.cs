using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Yaba.App.Models;
using Yaba.App.Views;
using Yaba.Common;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Goal;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.App.ViewModels
{
	public class BudgetsDetailViewModel : ViewModelBase
	{
		private readonly IBudgetRepository _budgetRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IRecurringRepository _recurringRepository;
		private readonly IGoalRepository _goalRepository;


		private BudgetViewModel _budget;
		public BudgetViewModel Budget
		{
			get => _budget;
			set
			{
				_budget = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<Recurrence> Recurrences { get; }

		public Frame Frame { get; set; }

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
		public RecurringViewModel NewRecurringVM { get; } = new RecurringViewModel();

		public ObservableCollection<CategoryGoalDto> Categories { get; set; }
		public ICommand AddCategory { get;  }
		public ICommand CategoryChangedCommand { get; set; }
		public ICommand DeleteCategoryCommand { get; set; }
		public ICommand AddRecurringCommand { get; set; }

		public BudgetsDetailViewModel(IBudgetRepository budgetRepository, ICategoryRepository categoryRepository, IRecurringRepository recurringRepository, IGoalRepository goalRepository)
		{
			_budgetRepository = budgetRepository;
			_categoryRepository = categoryRepository;
			_recurringRepository = recurringRepository;
			_goalRepository = goalRepository;

			Categories = new ObservableCollection<CategoryGoalDto>();
			Recurrences = new ObservableCollection<Recurrence>();

			foreach (var rec in Enum.GetNames(typeof(Recurrence)))
			{
				var t = (Recurrence) Enum.Parse(typeof(Recurrence), rec);
				Recurrences.Add(t);
			}

			DeleteCategoryCommand = new RelayCommand(async o =>
			{
				if (!(o is CategoryViewModel category)) return;

				var deleteDialog = new ContentDialog
				{
					Title = $"Delete {category.Name}",
					Content = $"Are you sure you want to delete the category \"{category.Name}\"? This action is irreversible",
					PrimaryButtonText = "Yes, I am sure",
					CloseButtonText = "Cancel",
				};

				var result = await deleteDialog.ShowAsync();

				if (result == ContentDialogResult.Primary)
				{
					var deleted = await _categoryRepository.Delete(category.Id);
					if (deleted)
					{
						Budget.Categories.Remove(category);
					}
				}
			});

			CategoryChangedCommand = new RelayCommand(e =>
			{
				Frame?.Navigate(typeof(CategoryDetailPage), e);
			});

			AddCategory = new RelayCommand(async e =>
			{
				if (!(e is CategoryCreateViewModel cvm)) return;

				var category = new CategoryCreateDto
				{
					BudgetId = Budget.Id,
					Name = cvm.Name,
				};
				var categoryGuid = await _categoryRepository.Create(category);

				if (!categoryGuid.HasValue) throw new Exception();

				if (cvm.Recurrence != Recurrence.None && cvm.GoalAmount != 0.0)
				{
					var goal = new GoalCreateDto
					{
						Amount = (decimal) cvm.GoalAmount,
						CategoryId = categoryGuid.Value,
						Recurrence = cvm.Recurrence,
					};
					var goalGuid = await _goalRepository.CreateGoal(goal);
				}
				await Initialize(Budget);
			});

			AddRecurringCommand = new RelayCommand(async o =>
			{
				if (!(o is RecurringViewModel recurring)) return;
				if (string.IsNullOrWhiteSpace(recurring.Name)
					|| recurring.Amount == 0f
					|| recurring.Recurrence == Recurrence.None)
					return;

				var dto = new RecurringCreateDto
				{
					Amount = (decimal) recurring.Amount,
					Name = recurring.Name,
					Recurrence = recurring.Recurrence,
					BudgetId = Budget.Id,
				};

				var rec = await _recurringRepository.CreateBudgetRecurring(dto);
				if (rec == null) return;
				Budget.Recurrings.Add(new RecurringViewModel(rec));
			});
		}

		public async Task Initialize(BudgetViewModel budget)
		{
			Budget = budget;

			var detailedBudget = await _budgetRepository.Find(budget.Id);
			var categories = detailedBudget.Categories
				.Select(c => new CategoryViewModel(c));
			var recurrings = detailedBudget.Recurrings
				.Select(r => new RecurringViewModel(r));

			budget.Categories.Clear();
			budget.Categories.AddRange(categories);

			budget.Recurrings.Clear();
			budget.Recurrings.AddRange(recurrings);
		}
	}
}
