using System;
using System.Windows.Input;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.App.ViewModels
{
	public class EntryViewModel : ViewModelBase
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


		private string _description;
		public string Description
		{
			get => _description;
			set
			{
				_description = value;
				OnPropertyChanged();
			}
		}


		private DateTime _date = DateTime.Now;
		public DateTime Date
		{
			get => _date;
			set
			{
				_date = value;
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

		public EntryViewModel()
		{
		}

		public EntryViewModel(EntrySimpleDto entry)
		{
			Id = entry.Id;
			Amount = (float) entry.Amount;
			Description = entry.Description;
			Date = entry.Date;
		}
	}
}
