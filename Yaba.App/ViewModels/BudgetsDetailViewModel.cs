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

		public ObservableCollection<CategoryGoalDto> Categories { get; set; }
		public ICommand AddCategory { get;  }

		public ICommand CategoryChangedCommand { get; set; }

		public ICommand DeleteCategoryCommand { get; set; }

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
		}

		public async Task Initialize(BudgetViewModel budget)
		{
			Budget = budget;

			var detailedBudget = await _budgetRepository.Find(budget.Id);
			var categories = detailedBudget.Categories
				.Select(c => new CategoryViewModel(c));
			budget.Categories.Clear();
			budget.Categories.AddRange(categories);
		}
	}
}
