using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common.Budget;

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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
