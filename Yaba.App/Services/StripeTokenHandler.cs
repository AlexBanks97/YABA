using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;

namespace Yaba.App.Models
{
	public static class StripeTokenHandler 
	{
		public static string CardToToken(StripePaymentViewModel stripePaymentViewModel)
		{
			var stripeTokenCreateOptions = new StripeTokenCreateOptions
			{
				Card = new StripeCreditCardOptions
				{
					Number = stripePaymentViewModel.Numbers,
					ExpirationMonth = Int32.Parse(stripePaymentViewModel.Month),
					ExpirationYear = Int32.Parse(stripePaymentViewModel.Year),
					Cvc = stripePaymentViewModel.CVC,
					Name = stripePaymentViewModel.HolderName
				}
			};

			var tokenService = new StripeTokenService();
			var stripeToken = tokenService.Create(stripeTokenCreateOptions);

			return stripeToken.Id;
		}
	}
}
