using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaba.App.Models
{
	public class CreditCard
	{
		public string Numbers { get; set; }
		public string HolderName { get; set; }
		public string Month { get; set; }
		public string Year { get; set; }
		public string Cvc { get; set; }

		public bool VerifyCreditCardInfo()
		{
			if (Numbers == ""
			    || Month == ""
			    || Year == ""
			    || Cvc == ""
			    || HolderName == "")
				return false;
			try
			{
				int month = 0;
				int year = 0;
				int cvc = 0;

				if (!Int32.TryParse(Month, out month)
				    || !Int32.TryParse(Year, out year)
				    || !Int32.TryParse(Year, out cvc))
					return false;

				if (month < 1 || month > 12)
					return false;
				else if (year < 1990 || year > new DateTime().Year)
					return false;
				else if (Cvc.Length != 3)
					return false;
			}
			catch (Exception) { return false; }

			return true;
		}
	}
}
