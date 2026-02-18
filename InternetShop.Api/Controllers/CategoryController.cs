using InternetShop.Contract.Requests;
using InternetShop.Contract.Responses;
using InternetShop.Service;
using InternetShop.Service.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync([FromServices] IRequestHandler<IList<CategoryResponse>> getCategoriesQuery)
        {
            return Ok(await getCategoriesQuery.Handle());
        }

        [HttpGet("{categoryId}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategoryByIdAsync(long categoryId, [FromServices] IRequestHandler<long, CategoryResponse> getCategoryByIdQuery)
        {
            return Ok(await getCategoryByIdQuery.Handle(categoryId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(
            [FromServices] IRequestHandler<CreateCategoryCommand, CategoryResponse> createCategoryCommand,
            [FromBody] CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Name field is missing");

            var category = await createCategoryCommand.Handle(new CreateCategoryCommand
            {
                Name = request.Name,
                Description = request.Description
            });

            return CreatedAtRoute(
                "GetCategory",      // 1. GET-метод для нової сутності
                new { categoryId = category.CategoryId }, // 2. Згенерований ID
                category                                // 3. Створений об'єкт
            );
        }

        [HttpPatch("{categoryId}")]
        public async Task<IActionResult> UpdateCategoryAsync(long categoryId,
            [FromServices] IRequestHandler<UpdateCategoryCommand, CategoryResponse> updateCategoryCommand,
            [FromBody] UpdateCategoryRequest request)
        {
            var category = await updateCategoryCommand.Handle(new UpdateCategoryCommand
            {
                CategoryId = categoryId,
                Name = request.Name,
                Description = request.Description
            });

            if (category == null)
                return NotFound($"Category with ID {categoryId} not found.");

            return Ok(category);
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategoryByIdAsync(int categoryId, [FromServices] IRequestHandler<DeleteCategoryCommand, bool> deleteCategoryCommand)
        {
            var result = await deleteCategoryCommand.Handle(new DeleteCategoryCommand { CategoryId = categoryId });

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
