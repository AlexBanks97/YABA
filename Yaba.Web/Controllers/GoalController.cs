using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common.Budget;

namespace Yaba.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Goal")]
    public class GoalController : Controller
    {
		IGoalRepository repository;

		public GoalController(IGoalRepository repository)
		{
			this.repository = repository;
		}


		// GET: api/Goal
		[HttpGet]
        public Task<IActionResult> Get()
        {
			throw new NotImplementedException();
        }

        // GET: api/Goal/5
        [HttpGet("{id}")]
        public Task<IActionResult> Get(int id)
        {
			throw new NotImplementedException();
		}
        
        // POST: api/Goal
        [HttpPost]
        public Task<IActionResult> Post([FromBody]string value)
        {
			throw new NotImplementedException();
		}
        
        // PUT: api/Goal/5
        [HttpPut("{id}")]
        public Task<IActionResult> Put(int id, [FromBody]string value)
        {
			throw new NotImplementedException();
		}
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Task<IActionResult> Delete(int id)
        {
			throw new NotImplementedException();
		}
    }
}
