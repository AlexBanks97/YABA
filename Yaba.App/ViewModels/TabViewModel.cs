using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Yaba.App.Models;
using Yaba.Common;
using Yaba.Common.Payment;
using Yaba.Common.Tab.DTO.Item;

namespace Yaba.App.ViewModels
{
	public class TabViewModel : ViewModelBase
	{

		private Guid _id;
		public Guid Id
		{
			get => _id;
			set
			{
				_id = value;
				OnPropertyChanged();
			}
		}

		private UserViewModel _userOne;
		public UserViewModel UserOne
		{
			get
			{
				return _userOne;
			}
			set
			{
				_userOne = value;
				OnPropertyChanged();
			}
		}

		private UserViewModel _userTwo;
		public UserViewModel UserTwo
		{
			get => _userTwo;
			set
			{
				_userTwo = value;
				OnPropertyChanged();
			}
		}

		private UserViewModel _userNotCurrentUser;
		public UserViewModel UserNotCurrentUser
		{
			get => _userNotCurrentUser;
			set
			{
				_userNotCurrentUser = value;
				OnPropertyChanged();
			}
		}
	}
}
