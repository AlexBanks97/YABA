using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.Tab.DTO;

namespace Yaba.Web.Controllers
{
	[Route("api/tabs")]
	public class TabsController : Controller
	{
		private readonly ITabRepository _repository;

		public TabsController(ITabRepository repository)
		{
			_repository = repository;
		}

		// GET api/tabs
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var tabs = await _repository.FindAllTabs();
			if (tabs == null) return NotFound();
			return Ok(tabs);
		}

		// GET api/budgets/{guid}
		[HttpGet]
		[Route("{tabId:Guid}")]
		public async Task<IActionResult> Get(Guid tabId)
		{
			var tab = await _repository.FindTab(tabId);
			if (tab == null) return NotFound(); // Returns 404
			return Ok(tab); // Returns 200
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] TabCreateDto tab)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState); // returns 404
			var guid = await _repository.CreateTab(tab);
			return CreatedAtAction(nameof(Get), new { tabId = guid}, null);
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] TabUpdateDto tab)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var success = await _repository.UpdateTab(tab);

			if (success) return NoContent();
			return NotFound();
		}

		[HttpDelete]
		[Route("{tabId:Guid}")]
		public async Task<IActionResult> Delete(Guid tabId)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var deleted = await _repository.Delete(tabId);
			if (deleted) return NoContent();
			return NotFound();
		}
	}
}
