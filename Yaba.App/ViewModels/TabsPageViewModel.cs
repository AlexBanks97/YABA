using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Yaba.App.Models;
using Yaba.App.Services;
using Yaba.Common;
using Yaba.Common.Payment;
using Yaba.Common.Tab.DTO;

namespace Yaba.App.ViewModels
{
	public class TabsPageViewModel : ViewModelBase
	{
		private readonly ITabRepository _tabRepo;
		private readonly IUserRepository _userRepo;
		private readonly IUserHelper _userHelper;
		private readonly IAuthenticationHelper _helper;
		private readonly PaymentRepository paymentRepository;



		public ObservableCollection<TabViewModel> Tabs { get; }
		public ObservableCollection<UserViewModel> Users { get; }
		public ObservableCollection<UserViewModel> UserSearchResult { get; }

		private UserViewModel _selectedUser;
		public UserViewModel SelectedUser
		{
			get => _selectedUser ;
			set
			{
				_selectedUser = value;
				OnPropertyChanged();
			}
		}

        private string _approvalUri = "";
        public string ApprovalUri
        {
            get => _approvalUri;
            set
            {
                _approvalUri = value;
                OnPropertyChanged();
            }
        }

		public ICommand TextChangedCommand { get; set; }
		public ICommand SuggestionChosenCommand { get; set; }
		public ICommand CreateTabCommand { get; set; }
		public ICommand RemoveTabCommand { get; set; }
        public ICommand PayWithPayPal { get; }
		public ICommand PayWithStripe { get; }

		private async void RemoveTab(object o)
		{
			if (!(o is TabViewModel tab)) throw new Exception();
			if (await _tabRepo.Delete(tab.Id))
			{
				Tabs.Remove(tab);
			}
		}

		public TabsPageViewModel(ITabRepository tabRepo, IUserRepository userRepo, IUserHelper userHelper, IAuthenticationHelper helper, PaymentRepository repo)
		{
			_tabRepo = tabRepo;
			_userRepo = userRepo;
			_userHelper = userHelper;
			_helper = helper;
			paymentRepository = repo;

			Tabs = new ObservableCollection<TabViewModel>();
			Users = new ObservableCollection<UserViewModel>();
			UserSearchResult = new ObservableCollection<UserViewModel>();
			RemoveTabCommand = new RelayCommand(RemoveTab);

			TextChangedCommand = new RelayCommand(async e =>
			{
				if (!(e is string text)) throw new Exception();
				UserSearchResult.Clear();
				var currentUser = await _userHelper.GetCurrentUser();
				var matches = Users
					.Where(u => u.Name.Contains(text, StringComparison.OrdinalIgnoreCase) && u.Id != currentUser.Id);
				UserSearchResult.AddRange(matches);
			});

			SuggestionChosenCommand = new RelayCommand(async e =>
			{
				Debug.WriteLine(e);

				if (!(e is AutoSuggestBoxSuggestionChosenEventArgs scea)) throw new Exception();
				if (!(scea.SelectedItem is UserViewModel selectedUser)) throw new Exception();

				SelectedUser = selectedUser;
			});

			CreateTabCommand = new RelayCommand(async e =>
			{
				var otherUser = SelectedUser;
				var me = await _userHelper.GetCurrentUser();

				var dto = new TabCreateDto
				{
					UserOne = me.Id,
					UserTwo = otherUser.Id,
					State = State.Active,
				};

				await _tabRepo.CreateTab(dto);
				await Initialize();
			});

            PayWithPayPal = new RelayCommand(async _ =>
            {

                // Get amount and email

                PaymentDto dto = new PaymentDto()
                {
                    Amount = "100.00",
                    PaymentProvider = "PayPal",
                    Token = "tok_visa",
                    RecipientEmail = "christoffer.nissen-buyer@me.com"
                };

                var xx = (await _helper.GetAccountAsync())?.AccessToken;
                // Ask API to create payment
                // Receive linkOrMessage, and open accept link if link
                var uriOrSuccess = await paymentRepository.Pay(dto, xx.RawData);

                if(uriOrSuccess.Equals("true"))
                {
                    // Successful stripe payment



                    // Show success screen

                } 
                else if(uriOrSuccess.Equals("Failure..."))
                {
                    Uri targetUri = new Uri(uriOrSuccess);
                    ApprovalUri = targetUri.ToString();

                    // Open webview and load uri
                    DoStuff(targetUri);

                } 
                else 
                {
                    // Failure

                    // Show failrue screen, and ask user to try again
                }

            });


		}

        private static void DoStuff(Uri uri)
        {
            //WebView MessageBox.Show(uri);
        }

		public async Task Initialize()
		{
			var user = await _userHelper.GetCurrentUser();
			var tabs = (await _tabRepo.FindWithUser(user.Id)) // <- change to find all with user
				.Select(t => new TabViewModel
				{
					Id = t.Id,
					UserOne =  new UserViewModel(t.UserOne),
					UserTwo = new UserViewModel(t.UserTwo),
					UserNotCurrentUser = t.UserOne.Equals(user)
						? new UserViewModel(t.UserTwo)
						: new UserViewModel(t.UserOne),
				});
			Tabs.Clear();
			Tabs.AddRange(tabs);

			var users = (await _userRepo.FindAll())
				.Select(u => new UserViewModel(u));
			Users.Clear();
			Users.AddRange(users);
		}
	}
}
