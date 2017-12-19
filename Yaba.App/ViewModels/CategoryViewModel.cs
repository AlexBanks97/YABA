using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls.Maps;
using Yaba.App.Models;
using Yaba.Common;
using Yaba.Common.Budget.DTO.Category;

namespace Yaba.App.ViewModels
{
	public class CategoryViewModel : ViewModelBase
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


		private GoalViewModel _goal;
		public GoalViewModel Goal
		{
			get => _goal;
			set
			{
				_goal = value;
				OnPropertyChanged();
			}
		}


		private ObservableCollection<EntryViewModel> _Entries = new ObservableCollection<EntryViewModel>();
		public ObservableCollection<EntryViewModel> Entries
		{
			get => _Entries;
			set
			{
				_Entries = value;
				OnPropertyChanged();
			}
		}

		public CategoryViewModel()
		{
			Entries.CollectionChanged += (sender, args) =>
			{
				OnPropertyChanged(nameof(UsagePercentage));
				OnPropertyChanged(nameof(UsedInTimeSpan));
			};
		}

		public string PrettyUsage => Goal == null ? string.Empty : $"{UsedInTimeSpan}/{Goal.Amount}";

		public CategoryViewModel(CategoryGoalDto simple) : base()
		{
			Id = simple.Id;
			Name = simple.Name;
			Goal = simple.Goal != null
				? new GoalViewModel(simple.Goal)
				: null;
			Entries.AddRange(simple.Entries.Select(e => new EntryViewModel(e)));
		}

		public int UsagePercentage
		{
			get
			{
				if (Goal == null) return 0;
				return (int) ((UsedInTimeSpan / (float) Goal.Amount) * 100);
			}
		}

		public float MonthlyUsage
		{
			get
			{
				var earliest = DateTime.Now.Subtract(TimeSpan.FromDays(30));
				return Entries
					.Where(entry => entry.Date > earliest)
					.Select(entry => entry.Amount)
					.Sum();
			}
		}

		public float UsedInTimeSpan
		{
			get
			{
				if (Goal == null) return 0f;

				TimeSpan span;

				switch (Goal.Recurrence)
				{
					case Recurrence.None:
						return 0;
						break;
					case Recurrence.Daily:
						span = TimeSpan.FromDays(1);
						break;
					case Recurrence.Weekly:
						span = TimeSpan.FromDays(7);

						break;
					case Recurrence.Monthly:
						span = TimeSpan.FromDays(30);
						break;
					case Recurrence.Yearly:
						span = TimeSpan.FromDays(365);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				var earliest = DateTime.Now.Subtract(span);

				var totalInSpan = Entries
					.Where(entry => entry.Date > earliest)
					.Select(entry => entry.Amount)
					.Sum();

				return totalInSpan;
			}
		}
	}
}
