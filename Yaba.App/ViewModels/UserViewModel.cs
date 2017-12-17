using System;
using Yaba.Common.User.DTO;

namespace Yaba.App.ViewModels
{
	public class UserViewModel : ViewModelBase
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

		public UserViewModel(UserDto user)
		{
			Id = user.Id;
			Name = user.Name;
		}
	}
}
