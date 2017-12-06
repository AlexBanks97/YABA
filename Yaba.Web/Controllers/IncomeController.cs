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
    public class IncomeController : Controller
    {
		private readonly IRecurringRepository _repository;

		public IncomeController(IRecurringRepository repository)
		{
			_repository = repository;
		}
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
			var budgetIncome = await _repository.FindAllBudgetRecurrings();
			return Ok(budgetIncome);
        }

        // GET api/values/5
        [HttpGet]
		[Route("{incomeId:Guid}")]
		public async Task<IActionResult> Get(Guid incomeId)
        {
			var budgetIncome = await _repository.FindBudgetRecurring(incomeId);
			if(budgetIncome == null) { return NotFound(); }

			return Ok(budgetIncome);
        }

		// POST api/values
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] RecurringCreateDto income)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var guid = await _repository.CreateBudgetRecurring(income);
			return CreatedAtAction(nameof(Get), new { incomeId = guid }, null);
        }

        // PUT api/values/5
        [HttpPut("{incomeId:Guid}")]
        public async Task<IActionResult> Put(Guid incomeId, [FromBody]RecurringUpdateDto income)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var update = await _repository.UpdateBudgetRecurring(income);
			return NoContent(); 
        }

        // DELETE api/values/5
        [HttpDelete("{incomeId:Guid}")]
        public async Task<IActionResult> Delete(Guid incomeId)
        {
			var deleted = await _repository.DeleteBudgetRecurring(incomeId);
			if(!deleted)
			{
				return NotFound();
			}
			return NoContent();
        }
    }
}
