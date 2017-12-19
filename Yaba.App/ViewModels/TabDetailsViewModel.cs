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
		public IView View { get; set; }
        public Guid CurrentTabId { private get; set; }
        private readonly PaymentRepository paymentRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserHelper _userHelper;
        public ObservableCollection<TabItemSimpleDTO> TabItemList { get; set; }
        public TabItemViewModel TabItemVM { get; set; }
        public ICommand PayWithStripe { get; }
        public ICommand PayWithPayPal { get; }
        private readonly IAuthenticationHelper _helper;
        public ICommand AddTabItemCommand { get; }

		public PayPalPaymentViewModel PayPalPaymentViewModel { get; set; }
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

		private decimal _computedBalance;
		public decimal ComputedBalance
		{
			get => _computedBalance;
			set
			{
				_computedBalance = value;
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
		public ICommand PayWithPayPal { get; }

		private readonly IAuthenticationHelper _helper;

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

        public TabDetailsViewModel(PaymentRepository repo, IAuthenticationHelper helper, IItemRepository itemRepository, IUserRepository userRepository, IUserHelper userHelper)
		{
			StripePaymentViewModel = new StripePaymentViewModel();
			if(PayPalPaymentViewModel == null) PayPalPaymentViewModel = new PayPalPaymentViewModel();
			_itemRepository = itemRepository;
			_userRepository = userRepository;
			_userHelper = userHelper;
			paymentRepository = repo;
			_helper = helper;

			TabItemList = new ObservableCollection<TabItemSimpleDTO>();
			TabItemVM = new TabItemViewModel();

			AddTabItemCommand = new RelayCommand(async e =>
			{
				if (!(e is TabItemViewModel tivm)) throw new Exception();

				var createDto = new TabItemCreateDTO
				{
					Amount = (decimal)tivm.Amount,
					Description = tivm.Description,
					CreatedBy = (await _userHelper.GetCurrentUser()).Id,
					TabId = CurrentTabId,
				};

				saveTabItem(createDto);

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

                var xx = (await _helper.GetAccountAsync())?.AccessToken;
                await repo.Pay(payment, xx.RawData);
				await CreateAndSaveTabItemFromAmount(StripePaymentViewModel.Amount);

				StripePaymentViewModel = new StripePaymentViewModel();
				StripeIsOpen = false;
				Success = true;

			});

			PayWithPayPal = new RelayCommand(async _ =>
			{

				if (!(_ is PayPalPaymentViewModel cc)) return;

				// Get amount and email

				PaymentDto dto = new PaymentDto()
				{
					Amount = PayPalPaymentViewModel.Amount.ToString(),
					PaymentProvider = "PayPal",
					RecipientEmail = PayPalPaymentViewModel.Email
				};

				var xx = (await _helper.GetAccountAsync())?.AccessToken;
				// Ask API to create payment
				// Receive linkOrMessage, and open accept link if link
                var linkOrMessage = await paymentRepository.Pay(dto, xx.RawData);

                if (linkOrMessage.Equals("true"))
			{
				// Successful Stripe payment
				// Show success screen

			}
                else if (linkOrMessage.Contains("http"))
				{
                    Uri targetUri = new Uri(linkOrMessage);

					// Open webview and load uri
					View.OpenUriInWebView(targetUri);

					await CreateAndSaveTabItemFromAmount(PayPalPaymentViewModel.Amount);
				}
				else
				{
					// Failure
					View.ActivateFailurePopup();
					// Show failrue screen, and ask user to try again
				}

			});
		}

		private async Task CreateAndSaveTabItemFromAmount(float amount)
		{
			

			var createDto = new TabItemCreateDTO
			{
				Amount = Decimal.Parse(amount.ToString()),
				Description = "Payment",
				CreatedBy = (await _userHelper.GetCurrentUser()).Id,
				TabId = CurrentTabId,
			};
			saveTabItem(createDto);
		}

		private async void saveTabItem(TabItemCreateDTO createDto)
		{
			var dto = await _itemRepository.Create(createDto);
			TabItemList.Add(dto);
		}

		public async void Initialize(Guid tabId, string notCurrentUserName)
		{
			var tabItems = await _itemRepository.FindFromTab(tabId);
			Name = notCurrentUserName;
			TabItemList.Clear();
			TabItemList.AddRange(tabItems);

			var currentUser = await _userHelper.GetCurrentUser();

			ComputedBalance = TabItemList
				.Aggregate(0.0m, ((amount, tabItem) =>
				{
					if (tabItem.CreatedBy.Id == currentUser.Id)
					{
						return amount + tabItem.Amount;
					}
					return amount - tabItem.Amount;
				}));

		}
	}
}
