﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yaba.App.Models;
using Yaba.Common.Payment;

namespace Yaba.App.ViewModels
{
	public class TabDetailsViewModel : ViewModelBase
	{
		private StripePaymentViewModel _StripePaymentViewModel;
		public StripePaymentViewModel StripePaymentViewModel
		{
			get => _StripePaymentViewModel;
			set
			{
				_StripePaymentViewModel = value;
				OnPropertyChanged();
			}
		}

		private readonly PaymentRepository paymentRepository;

		public ICommand PayWithStripe { get; }

		private bool _success;
		public bool Success
		{
			get => _success;
			set
			{
				_success = value;
				OnPropertyChanged();
			}
		}
		private bool _failure;
		public bool Failure
		{
			get => _failure;
			set
			{
				_failure = value;
				OnPropertyChanged();
			}
		}
		private bool _stripeIsOpen;
		public bool StripeIsOpen
		{
			get => _stripeIsOpen;
			set
			{
				_stripeIsOpen = value;
				OnPropertyChanged();
			}
		}

		public TabDetailsViewModel(PaymentRepository repo)
		{
			StripePaymentViewModel = new StripePaymentViewModel();
			paymentRepository = repo;

			PayWithStripe = new RelayCommand(async e =>
			{
				Debug.WriteLine("blah");
				if (!(e is StripePaymentViewModel cc))
				{
					StripeIsOpen = false;
					Failure = true;
					return;
				}
				if (!cc.VerifyCreditCardInfo())
				{
					Failure = true;
					return;
				}
				var payment = new PaymentDto
				{
					Amount = StripePaymentViewModel.Amount.ToString(),
					PaymentProvider = "Stripe",
					Token = StripeTokenHandler.CardToToken(cc),
				};
				await repo.Pay(payment, "");

				StripePaymentViewModel = new StripePaymentViewModel();
				StripeIsOpen = false;
				Success = true;
			});
		}
	}
}
