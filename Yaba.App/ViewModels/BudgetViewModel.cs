using System;
using System.Collections.ObjectModel;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.App.ViewModels
{
	public class BudgetViewModel : ViewModelBase
	{
		private Guid _id;
		public Guid Id
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged();
			}
		}


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

		private ObservableCollection<CategoryViewModel> _categories = new ObservableCollection<CategoryViewModel>();
		public ObservableCollection<CategoryViewModel> Categories
		{
			get => _categories;
			set
			{
				_categories = value;
				OnPropertyChanged();
			}
		}


		private ObservableCollection<RecurringViewModel> _recurrings = new ObservableCollection<RecurringViewModel>();
		public ObservableCollection<RecurringViewModel> Recurrings
		{
			get => _recurrings;
			set
			{
				_recurrings = value;
				OnPropertyChanged();
			}
		}

		public BudgetViewModel(BudgetSimpleDto simple)
		{
			Id = simple.Id;
			Name = simple.Name;
		}
	}
}
