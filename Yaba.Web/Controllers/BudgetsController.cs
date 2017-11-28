using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common;
using Yaba.Common.DTOs.BudgetDTOs;

namespace Yaba.Web.Controllers
{
    [Route("api/[controller]")]
    public class BudgetsController : Controller
    {
        private readonly IBudgetRepository _repository;

        public BudgetsController(IBudgetRepository repository)
        {
            _repository = repository;
        }
        
        // GET api/budgets
        [HttpGet]
        public async Task<IEnumerable<BudgetDTO>> Get()
        {
            return await _repository.FindAllBudgets();
        }

        // GET api/budgets/{guid}
        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var budget = await _repository.FindBudget(id);
            if (budget == null)
            {
                return NotFound();
            }
            return Ok(budget);
        }

        // POST api/budgets
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]BudgetCreateUpdateDTO budget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var guid = await _repository.CreateBudget(budget);
            return CreatedAtAction(nameof(Get), new {guid}, null);
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