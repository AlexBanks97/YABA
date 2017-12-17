using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Yaba.App.Models;
using Yaba.Common;
using Yaba.Common.Tab.DTO;

namespace Yaba.App.ViewModels
{
	public class TabsPageViewModel : ViewModelBase
	{
		private readonly ITabRepository _tabRepo;
		private readonly IUserRepository _userRepo;
		private readonly IAuthenticationHelper _auth;

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

		public ICommand TextChangedCommand { get; set; }
		public ICommand SuggestionChosenCommand { get; set; }
		public ICommand CreateTabCommand { get; set; }
		public ICommand RemoveTabCommand { get; set; }

		private async void RemoveTab(object o)
		{
			if (!(o is TabViewModel tab)) throw new Exception();
			if (await _tabRepo.Delete(tab.Id))
			{
				Tabs.Remove(tab);
			}
		}

		public TabsPageViewModel(ITabRepository tabRepo, IUserRepository userRepo, IAuthenticationHelper auth)
		{
			_tabRepo = tabRepo;
			_userRepo = userRepo;
			_auth = auth;
			Tabs = new ObservableCollection<TabViewModel>();
			Users = new ObservableCollection<UserViewModel>();
			UserSearchResult = new ObservableCollection<UserViewModel>();
			RemoveTabCommand = new RelayCommand(RemoveTab);

			TextChangedCommand = new RelayCommand(e =>
			{
				if (!(e is string text)) throw new Exception();
				UserSearchResult.Clear();
				var matches = Users
					.Where(u => u.Name.Contains(text, StringComparison.OrdinalIgnoreCase));
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
				var me = Users.FirstOrDefault(o => o.Name != otherUser.Name);

				var dto = new TabCreateDto
				{
					UserOne = me?.Id ?? Guid.Empty,
					UserTwo = otherUser.Id,
					State = State.Active,
				};

				await _tabRepo.CreateTab(dto);
				await Initialize();
			});
		}

		public async Task Initialize()
		{
			var tabs = (await _tabRepo.FindAllTabs()) // <- change to find all with user
				.Select(t => new TabViewModel
				{
					Id = t.Id,
					UserOne =  new UserViewModel(t.UserOne),
					UserTwo = new UserViewModel(t.UserTwo),
					UserNotCurrentUser = !t.UserOne.Name.Equals("Dr. Phil")
						? new UserViewModel(t.UserOne)
						: new UserViewModel(t.UserTwo)
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
