using System;
using System.Collections.ObjectModel;
using System.Linq;
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

		private CategoryDetailsDto _category;

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
		public decimal TotalThisMonth => _category.Entries
			.Aggregate(0.0m, (total, entry) => total + entry.Amount);

		public ObservableCollection<EntrySimpleDto> Entries { get; private set; }

		public CategoryViewModel(ICategoryRepository repository)
		{
			_repository = repository;
			Entries = new ObservableCollection<EntrySimpleDto>();
		}

		public async Task Initialize(Guid categoryId)
		{
			_category = await _repository.Find(categoryId);

			Name = _category.Name;
			Entries.Clear();
			Entries.AddRange(_category.Entries);
		}
	}
}
