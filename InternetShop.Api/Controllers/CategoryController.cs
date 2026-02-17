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
        public async Task<IActionResult> UpdateCategoryAsync(
            [FromServices] IRequestHandler<UpdateCategoryCommand, CategoryResponse> updateCategoryCommand,
            [FromBody] UpdateCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Undefined which category to update");

            var category = await updateCategoryCommand.Handle(new UpdateCategoryCommand
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description
            });

            return Ok(category);
        }
    }
}
