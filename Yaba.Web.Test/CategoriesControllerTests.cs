using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Yaba.Common;
using Yaba.Common.Budget;
using Yaba.Common.Budget.DTO.Category;
using Yaba.Web.Controllers;

namespace Yaba.Web.Test
{
	public class CategoriesControllerTests
	{

		// [HttpGet]
		// public async Task<ICollection<CategorySimpleDto>> Get(Guid budgetId)
		[Fact]
		public async void GetAll_given_no_params_returns_all_categories()
		{
			var mock = new Mock<ICategoryRepository>();
			var categories = new List<CategorySimpleDto>
			{
				new CategorySimpleDto(),
				new CategorySimpleDto(),
				new CategorySimpleDto(),
			};
			mock.Setup(r => r.Find())
				.ReturnsAsync(categories);

			using (var controller = new CategoriesController(mock.Object))
			{
				var response = await controller.GetAll() as OkObjectResult;
				Assert.Equal(categories, response.Value);
			}
		}

		[Fact]
		public async void GetAll_given_budgetId_returns_Ok_with_budget_categories()
		{
			var mock = new Mock<ICategoryRepository>();
			var guid = Guid.NewGuid();
			var categories = new List<CategorySimpleDto>
			{
				new CategorySimpleDto(),
			};
			mock.Setup(r => r.FindFromBudget(guid))
				.ReturnsAsync(categories);

			using (var ctrl = new CategoriesController(mock.Object))
			{
				var response = await ctrl.GetAll(guid) as OkObjectResult;
				Assert.Equal(categories, response.Value);
			}
		}

		// [HttpGet("{categoryId}")]
		// public async Task<IActionResult> Get(Guid budgetId, Guid categoryId)
		[Fact]
		public async void Get_given_existing_category_id_returns_Ok_with_content()
		{
			var mock = new Mock<ICategoryRepository>();
			var guid = Guid.NewGuid();
			var dto = new CategoryDetailsDto {Name = "Category"};
			mock.Setup(r => r.Find(guid))
				.ReturnsAsync(dto);

			using (var ctrl = new CategoriesController(mock.Object))
			{
				var response = await ctrl.Get(guid) as OkObjectResult;
				Assert.Equal(dto, response.Value);
			}
		}

		[Fact]
		public async void Get_given_nonexisting_category_id_returns_NotFound()
		{
			var mock = new Mock<ICategoryRepository>();
			mock.Setup(r => r.Find(It.IsAny<Guid>()))
				.ReturnsAsync(default(CategoryDetailsDto));

			using (var ctrl = new CategoriesController(mock.Object))
			{
				var response = await ctrl.Get(Guid.NewGuid());
				Assert.IsType<NotFoundResult>(response);
			}
		}

		// [HttpPost]
		// public async Task<IActionResult> Post(Guid budgetId, [FromBody] CategoryCreateDto category)
		[Fact]
		public async void Post_given_correct_category_body_returns_CreatedAt()
		{
			var mock = new Mock<ICategoryRepository>();
			mock.Setup(r => r.Create(It.IsAny<CategoryCreateDto>()))
				.ReturnsAsync(Guid.NewGuid());

			using (var ctrl = new CategoriesController(mock.Object))
			{
				var response = await ctrl.Post(new CategoryCreateDto {Name = ""});
				Assert.IsType<CreatedAtActionResult>(response);
			}
		}

		[Fact(Skip = "Fix this!")]
		public async void Post_given_malformed_category_body_returns_BadRequest()
		{
			var mock = new Mock<ICategoryRepository>();
			using (var ctrl = new CategoriesController(mock.Object))
			{
				var response = await ctrl.Post(new CategoryCreateDto());
				Assert.IsType<BadRequestResult>(response);
			}
		}

		// [HttpPut("{categoryId}")]
		// public async Task<IActionResult> Put(Guid budgetId, Guid categoryId, [FromBody] CategorySimpleDto category)
		[Fact]
		public async void Put_given_existing_cat_id_and_body_returns_NoContent()
		{
			var mock = new Mock<ICategoryRepository>();
			mock.Setup(r => r.Update(It.IsAny<CategorySimpleDto>()))
				.ReturnsAsync(true);

			using (var ctrl = new CategoriesController(mock.Object))
			{
				var response = await ctrl.Put(Guid.NewGuid(), new CategorySimpleDto { Name = "New Name" });
				Assert.IsType<NoContentResult>(response);
			}
		}
		// WE NEED MALFORMED (BADREQUEST TEST)

		// [HttpDelete("{categoryId}")]
		// public async Task<IActionResult> Delete(Guid budgetId, Guid categoryId)
		[Fact]
		public async void Delete_given_existing_id_returns_NoContent()
		{
			var mock = new Mock<ICategoryRepository>();
			var guid = Guid.NewGuid();
			mock.Setup(r => r.Delete(guid))
				.ReturnsAsync(true);

			using (var ctrl = new CategoriesController(mock.Object))
			{
				var response = await ctrl.Delete(guid);
				Assert.IsType<NoContentResult>(response);
			}
		}

		[Fact]
		public async void Delete_given_nonexisting_id_returns_NotFound()
		{
			var mock = new Mock<ICategoryRepository>();
			var guid = Guid.NewGuid();
			mock.Setup(r => r.Delete(guid))
				.ReturnsAsync(false);

			using (var ctrl = new CategoriesController(mock.Object))
			{
				var response = await ctrl.Delete(guid);
				Assert.IsType<NotFoundResult>(response);
			}
		}

	}
}
