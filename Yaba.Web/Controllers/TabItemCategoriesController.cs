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
				return BadRequest(ModelState);
			}
			var tabCategory = await _repository.Find(tabCategoryId);
			if (tabCategory == null) return NotFound();
			return Ok(tabCategory);

		}

		[HttpGet]
		public async Task<IActionResult> Get(TabItemSimpleDTO tab)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var tabCategory = await _repository.FindFromTabItemId(tab.Id);
			if (tabCategory == null) return NotFound();
			return Ok(tabCategory);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] TabItemCategoryCreateDTO category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var guid = await _repository.Create(category);
			if (guid == Guid.Empty) return BadRequest();
			return CreatedAtAction(nameof(Get), new { tabCategoryId = guid }, null);
		}

		[HttpPut("{categoryId:Guid}")]
		public async Task<IActionResult> Put([FromBody]TabItemCategoryDTO category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var updated = await _repository.Update(category);
			if (!updated) return BadRequest();
			return NoContent();
		}

		[HttpDelete("{categoryId:Guid}")]
		public async Task<IActionResult> Delete(Guid categoryId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var deleted = await _repository.Delete(categoryId);
			if (!deleted) return BadRequest();
			return NoContent();
		}
	}
}
