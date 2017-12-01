using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO;

namespace Yaba.Web.Controllers
{

	[Route("api/budgets")]
	public class BudgetsController : Controller
	{
		private readonly IBudgetRepository _repository;

		public BudgetsController(IBudgetRepository repository)
		{
			_repository = repository;
		}

		// GET api/budgets
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var budgets = await _repository.All();
			return Ok(budgets);
		}

		// GET api/budgets/{budgetId}
		[HttpGet]
		[Route("{budgetId:Guid}")]
		public async Task<IActionResult> Get(Guid budgetId)
		{
			var budget = await _repository.Find(budgetId);
			if (budget == null)
			{
				return NotFound();
			}
			return Ok(budget);
		}

		// POST api/budgets
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] BudgetCreateUpdateDto budget)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var guid = await _repository.Create(budget);

			return CreatedAtAction(nameof(Get), new { budgetId = guid }, null);
		}

		// PUT api/values/5
		[HttpPut("{budgetId:Guid}")]
		public async Task<IActionResult> Put(Guid budgetId, [FromBody] BudgetCreateUpdateDto budget)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var _ = await _repository.Update(budget);
			return NoContent();
		}

		// DELETE api/values/5
		[HttpDelete("{budgetId:Guid}")]
		public async Task<IActionResult> Delete(Guid budgetId)
		{
			throw new NotImplementedException();
		}

	}
}
