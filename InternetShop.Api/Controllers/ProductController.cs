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
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromServices] IRequestHandler<IList<ProductResponse>> getProductsQuery)
        {
            return Ok(await getProductsQuery.Handle());
        }

        [HttpGet("{productId}", Name = "GetProduct")]
        public async Task<IActionResult> GetProductByIdAsync(long productId, [FromServices] IRequestHandler<long, ProductResponse> getProductByIdQuery)
        {
            return Ok(await getProductByIdQuery.Handle(productId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(
            [FromServices] IRequestHandler<CreateProductCommand, ProductResponse> createProductCommand,
            [FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Name, Price or ManufacturerId field is missing");

            var product = await createProductCommand.Handle(new CreateProductCommand
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ManufacturerId = request.ManufacturerId,
                CategoriesOfProduct = request.CategoriesOfProduct
            });

            return CreatedAtRoute(
                "GetProduct",      // 1. GET-метод для нової сутності
                new { productId = product.ProductId }, // 2. Згенерований ID
                product                                // 3. Створений об'єкт
            );
        }

        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateProductAsync(long productId,
            [FromServices] IRequestHandler<UpdateProductCommand, ProductResponse> updateProductCommand,
            [FromBody] UpdateProductRequest request)
        {
            var product = await updateProductCommand.Handle(new UpdateProductCommand
            {
                ProductId = productId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ManufacturerId = request.ManufacturerId,
                CategoriesOfProduct = request.CategoriesOfProduct
            });

            if (product == null)
                return NotFound($"Product with ID {productId} not found.");

            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductByIdAsync(int productId,
            [FromServices] IRequestHandler<DeleteProductCommand, bool> deleteProductCommand)
        {
            var result = await deleteProductCommand.Handle(new DeleteProductCommand { ProductId = productId });

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
