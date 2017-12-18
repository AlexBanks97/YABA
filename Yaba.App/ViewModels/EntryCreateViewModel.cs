namespace Yaba.App.ViewModels
{
	public class EntryCreateViewModel : ViewModelBase
	{
		private string _description;
		public string Description
		{
			get => _description;
			set
			{
				_description = value;
				OnPropertyChanged();
			}
		}

		private float _amount;
		public float Amount
		{
			get => _amount;
			set
			{
				_amount = value;
				OnPropertyChanged();
			}
		}
	}
}
