using InternetShop.Contract.Responses;
using InternetShop.Service;
using InternetShop.Service.Commands;
using InternetShop.Service.Queries;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/Customer/{customerId}/Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("{orderId}", Name = "GetOrder")]
        public async Task<IActionResult> GetOrderByIdAsync(long customerId,
            long orderId,
            [FromServices] IRequestHandler<GetOrderByIdQuery, OrderResponse> getOrderByIdQuery)
        {
            var order = await getOrderByIdQuery.Handle(new GetOrderByIdQuery
            {
                CustomerId = customerId,
                OrderId = orderId
            });

            if (order == null)
            {
                return NotFound(); // Якщо null, то повертається статус 404
            }

            return Ok(order);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderByIdAsync(long customerId, long orderId,
            [FromServices] IRequestHandler<DeleteOrderCommand, bool> deleteOrderCommand)
        {
            var result = await deleteOrderCommand.Handle(new DeleteOrderCommand
            {
                CustomerId = customerId,
                OrderId = orderId
            });

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
