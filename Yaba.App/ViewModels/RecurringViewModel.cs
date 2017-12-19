using System;
using Yaba.Common;
using Yaba.Common.Budget.DTO.Recurring;

namespace Yaba.App.ViewModels
{
	public class RecurringViewModel : ViewModelBase
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


		private float _amount;
		public float Amount
		{
			get => _amount;
			set
			{
				_amount = value;
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

		public RecurringViewModel()
		{
		}

		public RecurringViewModel(RecurringSimpleDto dto)
		{
			Id = dto.Id;
			Name = dto.Name;
			Amount = (float) dto.Amount;
			Recurrence = dto.Recurrence;
		}
	}
}
