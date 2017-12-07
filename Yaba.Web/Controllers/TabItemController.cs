using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common;
using Yaba.Common.Tab.DTO.Item;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yaba.Web.Controllers
{
    [Route("api/tabitems")]
    public class TabItemController : Controller
    {
		private readonly IItemRepository _repository;

		public TabItemController(IItemRepository repository)
		{
			_repository = repository;
		}
		// GET: api/values
		[HttpGet]
		[Route("{tabItemId:Guid}")]
		public async Task<IActionResult> Get(Guid tabItemId)
        {
			var tabItem = await _repository.Find(tabItemId);
			if (tabItem == null)
			{
				return NotFound();
			}
			return Ok(tabItemId);
        }

        // GET api/values/5
        [HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] Guid? tabId = null)
		{
			if (tabId.HasValue)
			{
				var tabItems = await _repository.FindFromTab(tabId.Value);
				return Ok(tabItems);
			}
			return Forbid();
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TabItemCreateDTO tabItem)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var tabDto = await _repository.Create(tabItem);
			return CreatedAtAction(nameof(Get), new { tab = tabDto}, null);
        }

		// PUT api/values/5
		[HttpPut("{tabItem:Guid}")]
		public async Task<IActionResult> Put(Guid tabItemId, [FromBody]TabItemSimpleDTO tabItem)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var itemToUpdate = await _repository.Update(tabItem);
			return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{tabItemId:Guid}")]
        public async Task<IActionResult> Delete(Guid tabItemId)
        {
			var deleted = await _repository.Delete(tabItemId);
			if(!deleted)
			{
				return NotFound();
			}
			return NoContent();
        }
    }
}
