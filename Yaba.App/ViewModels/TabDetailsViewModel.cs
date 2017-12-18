using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yaba.App.Models;
using Yaba.App.Services;
using Yaba.Common;
using Yaba.Common.Payment;
using Yaba.Common.Tab.DTO;
using Yaba.Common.Tab.DTO.Item;

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

		public Guid CurrentTabId { private get; set; }

		private readonly PaymentRepository paymentRepository;
		private readonly IItemRepository _itemRepository;
		private readonly IUserRepository _userRepository;
		private readonly IUserHelper _userHelper;

		public ObservableCollection<TabItemSimpleDTO> TabItemList { get; set; }

		public TabItemViewModel TabItemVM { get; set; }

		public ICommand PayWithStripe { get; }

		public ICommand AddTabItemCommand { get; }

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

		public TabDetailsViewModel(PaymentRepository repo, IItemRepository itemRepository, IUserRepository userRepository, IUserHelper userHelper)
		{
			StripePaymentViewModel = new StripePaymentViewModel();
			_itemRepository = itemRepository;
			_userRepository = userRepository;
			_userHelper = userHelper;
			paymentRepository = repo;

			TabItemList = new ObservableCollection<TabItemSimpleDTO>();
			TabItemVM = new TabItemViewModel();

			AddTabItemCommand = new RelayCommand(async e =>
			{
				if (!(e is TabItemViewModel tivm)) throw new Exception();

				var kek_dto = new TabItemCreateDTO
				{
					Amount = (decimal)tivm.Amount,
					Description = tivm.Description,
					CreatedBy = (await _userHelper.GetCurrentUser()).Id,
					TabId = CurrentTabId,
				};

				var dto = await _itemRepository.Create(kek_dto);

				TabItemList.Add(dto);

			});

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

		public async void Initialize(Guid tabId, string notCurrentUserName)
		{
			var tabItems = await _itemRepository.FindFromTab(tabId);
			Name = notCurrentUserName;

			TabItemList.Clear();
			TabItemList.AddRange(tabItems);
		}
	}
}
