using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yaba.Common;
using Yaba.Common.DTOs.Category;

namespace Yaba.Web.Controllers
{
    [Route("api/budgets/{budgetId}/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ICollection<CategorySimpleDto>> Get(Guid budgetId)
        {
            return await _repository.FindFromBudget(budgetId);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> Get(Guid budgetId, Guid categoryId)
        {
            var category = await _repository.Find(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid budgetId, [FromBody] CategoryCreateDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var guid = await _repository.Create(category);
            return CreatedAtAction(nameof(Get), new {guid}, null);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Put(Guid budgetId, Guid categoryId, [FromBody] CategorySimpleDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updated = await _repository.Update(category);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> Put(Guid budgetId, Guid categoryId)
        {
            var deleted = await _repository.Delete(categoryId);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}