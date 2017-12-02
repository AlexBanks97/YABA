using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common.DTO.TabDTOs;
using Yaba.Common.Tab;
using Yaba.Common.Tab.TabItemDTOs;

namespace Yaba.Web.Controllers
{
    public class TabItemCategoriesController : Controller
    {
		private readonly ITabItemCategoryRepository _repository;

		public TabItemCategoriesController(ITabItemCategoryRepository repository)
		{
			_repository = repository;
		}

		public async Task<IActionResult> Get(Guid id)
		{

		}

		public async Task<IActionResult> Get(TabItemSimpleDTO tab)
		{

		}

		public async Task<IActionResult> Post(TabItemCategoryCreateDTO category)
		{

		}

		public async Task<IActionResult> Put(TabItemCategoryDTO category)
		{

		}

		public async Task<IActionResult> Delete(Guid id)
		{

		}

	}
}
