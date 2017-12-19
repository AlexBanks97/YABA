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
	public class CategoryPageViewModel : ViewModelBase
	{
		private readonly ICategoryRepository _repository;
		private readonly IEntryRepository _entryRepository;


		private CategoryViewModel _categoryVm;
		public CategoryViewModel Category
		{
			get => _categoryVm;
			set
			{
				_categoryVm = value;
				OnPropertyChanged();
			}
		}

		public EntryViewModel NewEntryVM { get; } = new EntryViewModel();

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

		public CategoryPageViewModel(ICategoryRepository repository, IEntryRepository entryRepository)
		{
			_repository = repository;
			_entryRepository = entryRepository;

			RemoveEntryCommand = new RelayCommand(async o =>
			{
				if (!(o is Guid id)) return;
				var deleted = await _entryRepository.DeleteBudgetEntry(id);
				if (!deleted) return;
				var entry = Category.Entries.FirstOrDefault(e => e.Id == id);
				Category.Entries.Remove(entry);
				
			});

			AddEntryCommand = new RelayCommand(async o =>
			{
				if (!(o is EntryViewModel evm)) return;
				if (evm.Amount == 0.0) return;
				var dto = new EntryCreateDto
				{
					Amount = (decimal) evm.Amount,
					Date = evm.Date,
					Description = evm.Description,
					CategoryId = Category.Id,
				};
				var returnedDto = await _entryRepository.CreateBudgetEntry(dto);

				if (returnedDto != null)
				{
					Category.Entries.Add(new EntryViewModel(returnedDto));
				}

				evm.Amount = 0;
				evm.Description = "";
			});
		}

		public async Task Initialize(CategoryViewModel category)
		{
			Category = category;
		}
	}
}
