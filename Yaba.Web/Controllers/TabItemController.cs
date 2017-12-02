using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common;
using Yaba.Common.DTO.TabDTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yaba.Web.Controllers
{
    [Route("api/tabitems")]
    public class TabItemController : Controller
    {
		private readonly ITabItemRepository _repository;

		public TabItemController(ITabItemRepository repository)
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
		[Route("{tabId:Guid}")]
		public async Task<IActionResult> Get(TabDTO tab)
		{
			var tabItem = await _repository.FindFrom(tab);
			if(tabItem == null)
			{
				return NotFound();
			}
			return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
