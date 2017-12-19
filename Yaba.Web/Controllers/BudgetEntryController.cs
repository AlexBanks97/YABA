using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Entry;

namespace Yaba.Web.Controllers
{
    
    [Route("api/budgets/entries")]
    public class BudgetEntryController : Controller
    {
		IEntryRepository _repository;

		public BudgetEntryController(IEntryRepository repository)
		{
			_repository = repository;
		}

		// GET: api/BudgetEntry
		[HttpGet]
        public async Task<IActionResult> Get()
        {
			var entries = await _repository.Find();
			return Ok(entries);
        }
		
        // GET: api/BudgetEntry/5
        [HttpGet("{entryId}")]
        public async Task<IActionResult> Get(Guid entryId)
        {
			var entry = await _repository.Find(entryId);

			if (entry == null) return NotFound();

			return Ok(entry);

        }
        
        // POST: api/BudgetEntry
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EntryCreateDto entry)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var dto = await _repository.CreateBudgetEntry(entry);
			return Ok(dto);
		}
        
        // PUT: api/BudgetEntry/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EntryDto entry)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			await _repository.UpdateBudgetEntry(entry);
			return NoContent();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
			var deleted = await _repository.DeleteBudgetEntry(id);

			if (!deleted) return NotFound();

			return NoContent();
        }
    }
}
