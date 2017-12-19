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
			return Ok(tabItem);
        }

        // GET api/values/5
        [HttpGet("{tabId:Guid}")]
		public async Task<IActionResult> GetAll(Guid tabId)
		{
			if (tabId == Guid.Empty) return BadRequest();
			var tabItems = await _repository.FindFromTab(tabId);
			if (tabItems == null) return NotFound();
			return Ok(tabItems);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TabItemCreateDTO tabItem)
        {
			if(!ModelState.IsValid) // This should probably be outsources to a [ValidGuid] check
			{
				return BadRequest(ModelState);
			}
			var dto = await _repository.Create(tabItem);
	        return Ok(dto);
        }

		// PUT api/values/5
		[HttpPut]
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
