using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.App.ViewModels;

namespace Yaba.App.Models
{
	public class StripePaymentViewModel : ViewModelBase
	{

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

		private string _numbers;
		public string Numbers
		{
			get => _numbers;
			set
			{
				_numbers = value;
				OnPropertyChanged();
			}
		}

		private string _holderName;
		public string HolderName
		{
			get => _holderName;
			set
			{
				_holderName = value;
				OnPropertyChanged();
			}
		}

		private string _month { get; set; }
		public string Month
		{
			get => _month;
			set
			{
				_month = value;
				OnPropertyChanged();
			}
		}

		private string _year { get; set; }
		public string Year
		{
			get => _year;
			set
			{
				_year = value;
				OnPropertyChanged();
			}
		}

		private string _cvc { get; set; }
		public string CVC
		{
			get => _cvc;
			set
			{
				_cvc = value;
				OnPropertyChanged();
			}
		}

		public bool VerifyCreditCardInfo()
		{
			if (_numbers == ""
			    || _month == ""
			    || _year == ""
			    || _cvc == ""
			    || _holderName == "")
				return false;
			try
			{
				int month = 0;
				int year = 0;
				int cvc = 0;

				if (!Int32.TryParse(_month, out month)
				    || !Int32.TryParse(_year, out year)
				    || !Int32.TryParse(_year, out cvc))
					return false;

				if (month < 1 || month > 12)
					return false;
				else if (year < DateTime.Now.Year)
					return false;
				else if (_cvc.Length != 3)
					return false;
			}
			catch (Exception) { return false; }

			return true;
		}
	}
}
