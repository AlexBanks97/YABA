using System.Windows.Input;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.App.ViewModels
{
	public class EntryViewModel : ViewModelBase
	{
		private EntrySimpleDto _dto;
		public EntrySimpleDto Dto
		{
			get => _dto;
			set
			{
				_dto = value;
				OnPropertyChanged();
			}
		}

		public ICommand RemoveCommand { get; set; }
	}
}
