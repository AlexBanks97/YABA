using System;
using Yaba.Common;
using Yaba.Common.Budget.DTO.Goal;

namespace Yaba.App.ViewModels
{
	public class GoalViewModel : ViewModelBase
	{
		private Guid _Id;
		public Guid Id
		{
			get => _Id;
			set
			{
				_Id = value;
				OnPropertyChanged();
			}
		}


		private decimal _Amount;
		public decimal Amount
		{
			get => _Amount;
			set
			{
				_Amount = value;
				OnPropertyChanged();
			}
		}

		private Recurrence _recurrence;
		public Recurrence Recurrence
		{
			get => _recurrence;
			set
			{
				_recurrence = value;
				OnPropertyChanged();
			}
		}

		public GoalViewModel(GoalSimpleDto goal)
		{
			Id = goal.Id;
			Recurrence = goal.Recurrence;
			Amount = goal.Amount;
		}
	}
}
