using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Goal;

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
        public async Task<IActionResult> Get()
        {
			
			var goals = await repository.Find();
			return Ok(goals);
        }

        // GET: api/Goal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
			var goal = await repository.Find(id);
			if (goal == null) return NotFound();
			return Ok(goal);
		}
        
        // POST: api/Goal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GoalCreateDto goal)
        {
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var id = await repository.CreateGoal(goal);
			return Ok(id);
		}
        
        // PUT: api/Goal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] GoalDto goal)
        {
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var updated = await repository.UpdateGoal(goal);

			if (!updated) return NotFound();
			return NoContent();
		}
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
			var deleted = await repository.DeleteGoal(id);

			if (!deleted) return NotFound();
			return NoContent();
		}
    }
}
