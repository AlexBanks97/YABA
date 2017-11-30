using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaba.Common;
using Yaba.Common.DTOs.TabDTOs;

namespace Yaba.Web.Controllers
{
    public class TabsController : Controller
    {
        private readonly ITabRepository _repository;

        public TabsController(ITabRepository repository)
        {
            _repository = repository;
        }

        // GET api/tabs
        [HttpGet]
        public async Task<IEnumerable<TabDTO>> Get()
        {
            return await _repository.FindAllTabs();
        }

        // GET api/budgets/{guid}
        [HttpGet]
        [Route("tabId:Guid")]
        public async Task<IActionResult> Get(Guid id)
        {
            var tab = await _repository.FindTab(id);
            if (tab == null) return NotFound(); // Returns 404
            return Ok(tab); // Returns 200
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TabCreateDTO tab)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // returns 404
            var guid = await _repository.CreateTab(tab);
            return CreatedAtAction(nameof(Get), new {guid}, null);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TabUpdateDTO tab)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var success = await _repository.UpdateTab(tab);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }

        public async Task<IActionResult> Delete(TabDTO tab)
        {
            throw new NotImplementedException();
        }
    }
}
