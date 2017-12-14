using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Yaba.App.Models;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.App.ViewModels
{
	public class CategoryViewModel : ViewModelBase
	{
		private readonly ICategoryRepository _repository;

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

		public ObservableCollection<EntrySimpleDto> Entries { get; private set; }

		public CategoryViewModel(ICategoryRepository repository)
		{
			_repository = repository;
			Entries = new ObservableCollection<EntrySimpleDto>();
		}

		public async Task Initialize(Guid categoryId)
		{
			var category = await _repository.Find(categoryId);

			Name = category.Name;
			Entries.Clear();
			Entries.AddRange(category.Entries);
		}
	}
}
