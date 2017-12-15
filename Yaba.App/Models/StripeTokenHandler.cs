using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;

namespace Yaba.App.Models
{
	public class StripeServices 
	{
		public string CardToToken(CreditCard creditCard)
		{
			var stripeTokenCreateOptions = new StripeTokenCreateOptions
			{
				Card = new StripeCreditCardOptions
				{
					Number = creditCard.Numbers,
					ExpirationMonth = Int32.Parse(creditCard.Month),
					ExpirationYear = Int32.Parse(creditCard.Year),
					Cvc = creditCard.Cvc,
					Name = creditCard.HolderName
				}
			};

			var tokenService = new StripeTokenService();
			var stripeToken = tokenService.Create(stripeTokenCreateOptions);

			return stripeToken.Id;
		}
	}
}
