using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Recurring;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yaba.Web.Controllers
{
    [Route("api/[controller]")]
    public class RecurringController : Controller
    {
		private readonly IRecurringRepository _repository;

		public RecurringController(IRecurringRepository repository)
		{
			_repository = repository;
		}
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
			var recurring = await _repository.FindAllBudgetRecurrings();
			return Ok(recurring);
        }

        // GET api/values/5
        [HttpGet]
		[Route("{recurringId:Guid}")]
		public async Task<IActionResult> Get(Guid recurringId)
        {
			var recurring = await _repository.FindBudgetRecurring(recurringId);
			if(recurring == null) { return NotFound(); }

			return Ok(recurring);
        }

		// POST api/values
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] RecurringCreateDto recurring)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var guid = await _repository.CreateBudgetRecurring(recurring);
			return CreatedAtAction(nameof(Get), new { incomeId = guid }, null);
        }

        // PUT api/values/5
        [HttpPut("{recurringId:Guid}")]
        public async Task<IActionResult> Put(Guid recurringId, [FromBody]RecurringUpdateDto recurring)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var update = await _repository.UpdateBudgetRecurring(recurring);
			return NoContent(); 
        }

        // DELETE api/values/5
        [HttpDelete("{recurringId:Guid}")]
        public async Task<IActionResult> Delete(Guid recurringId)
        {
			var deleted = await _repository.DeleteBudgetRecurring(recurringId);
			if(!deleted)
			{
				return NotFound();
			}
			return NoContent();
        }
    }
}
