using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;

namespace Yaba.App.ViewModels
{
	public class CategoryCreateViewModel : ViewModelBase
	{
		private string _name = "";
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		private float _goalAmount;
		public float GoalAmount
		{
			get => _goalAmount;
			set
			{
				_goalAmount = value;
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
	}
}
