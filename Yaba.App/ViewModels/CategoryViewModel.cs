using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Yaba.App.Models;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.App.ViewModels
{
	public class CategoryViewModel : ViewModelBase
	{
		private readonly ICategoryRepository _repository;
		private readonly IEntryRepository _entryRepository;

		private CategoryDetailsDto _category;

		public EntryCreateViewModel EntryCreateVM { get; } = new EntryCreateViewModel();

		public ICommand AddEntryCommand { get; }
		public ICommand RemoveEntryCommand { get; }

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

		public ObservableCollection<EntryViewModel> Entries { get; private set; }

		public CategoryViewModel(ICategoryRepository repository, IEntryRepository entryRepository)
		{
			_repository = repository;
			_entryRepository = entryRepository;
			Entries = new ObservableCollection<EntryViewModel>();

			RemoveEntryCommand = new RelayCommand(async o =>
			{
				if (!(o is Guid id)) return;
				var deleted = await _entryRepository.DeleteBudgetEntry(id);
				if (!deleted) return;
				var entry = Entries.FirstOrDefault(e => e.Dto.Id == id);
				Entries.Remove(entry);
			});

			AddEntryCommand = new RelayCommand(async o =>
			{
				if (!(o is EntryCreateViewModel evm)) return;
				if (evm.Amount == 0.0) return;
				var dto = new EntryCreateDto
				{
					Amount = (decimal) evm.Amount,
					Date = DateTime.Now,
					Description = evm.Description,
					CategoryId = _category.Id,
				};
				await _entryRepository.CreateBudgetEntry(dto);
				await Initialize(_category.Id);

				evm.Amount = 0;
				evm.Description = "";
			});
		}

		public async Task Initialize(Guid categoryId)
		{
			_category = await _repository.Find(categoryId);

			Name = _category.Name;

			var sortedEntries = _category.Entries
				.OrderByDescending(e => e.Date)
				.Select(e => new EntryViewModel
				{
					Dto = e,
					RemoveCommand = RemoveEntryCommand,
				});
			Entries.Clear();
			Entries.AddRange(sortedEntries);
		}
	}
}
