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

		[HttpGet]
		[Route("{tabCategoryId:Guid}")]
		public async Task<IActionResult> Get(Guid tabCategoryId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var tabCategory = await _repository.Find(tabCategoryId);
			if (tabCategory == null) return NotFound();
			return Ok(tabCategory);

		}

		public async Task<IActionResult> Get(TabItemSimpleDTO tab)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var tabCategory = await _repository.FindFromTabItemId(tab.Id);
			if (tabCategory == null) return NotFound();
			return Ok(tabCategory);
		}

		public async Task<IActionResult> Post(TabItemCategoryCreateDTO category)
		{
			throw new NotImplementedException();
		}

		public async Task<IActionResult> Put(TabItemCategoryDTO category)
		{
			throw new NotImplementedException();
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			throw new NotImplementedException();
		}

	}
}
