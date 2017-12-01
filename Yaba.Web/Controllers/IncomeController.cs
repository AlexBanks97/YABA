using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Income;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yaba.Web.Controllers
{
    [Route("api/[controller]")]
    public class IncomeController : Controller
    {
		private readonly IIncomeRepository _repository;

		public IncomeController(IIncomeRepository repository)
		{
			_repository = repository;
		}
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
			var budgetIncome = await _repository.FindAllBudgetIncomes();
			return Ok(budgetIncome);
        }

        // GET api/values/5
        [HttpGet]
		[Route("{incomeId:Guid}")]
		public async Task<IActionResult> Get(Guid incomeId)
        {
			var budgetIncome = await _repository.FindBudgetIncome(incomeId);
			if(budgetIncome == null) { return NotFound(); }

			return Ok(budgetIncome);
        }

		// POST api/values
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] IncomeCreateDto income)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var guid = await _repository.CreateBudgetIncome(income);
			return CreatedAtAction(nameof(Get), new { incomeId = guid }, null);
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
