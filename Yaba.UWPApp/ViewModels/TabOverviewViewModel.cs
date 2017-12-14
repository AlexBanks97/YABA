using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;
using Yaba.Common.Tab.DTO;

namespace Yaba.UWPApp.ViewModels
{
    public class TabOverviewViewModel
    {
	    private readonly ITabRepository _repository;
	    public ObservableCollection<TabDto> Tabs { get; set; }

	    public TabOverviewViewModel(ITabRepository repository)
	    {
		    _repository = repository;
		    Tabs = new ObservableCollection<TabDto>();
	    }

	    public async Task Initialize()
	    {
		    var tabs = await _repository.FindAllTabs();
		    foreach (var tab in tabs)
		    {
			    Tabs.Add(tab);
		    }
	    }
	}
}
