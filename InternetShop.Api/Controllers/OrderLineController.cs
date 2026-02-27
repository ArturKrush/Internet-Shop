using InternetShop.Contract.Requests;
using InternetShop.Contract.Responses;
using InternetShop.Data.Entities;
using InternetShop.Service;
using InternetShop.Service.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/Customer/{customerId}/Orders")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrderLineAsync(long customerId,
            [FromServices] IRequestHandler<CreateOrderLineCommand, OrderResponse> createOrderLineCommand,
            [FromBody] CreateOrderLineRequest request)
        {
            var order = await createOrderLineCommand.Handle(new CreateOrderLineCommand
            {
                Quantity = request.Quantity,
                ProductId = request.ProductId,
                CustomerId = customerId
            });

            return CreatedAtRoute(
                "GetOrder", // 1. GET-метод для нової сутності
                new { orderId = order.OrderId, customerId }, // 2. Згенерований ID
                order // 3. Створений об'єкт
            );
        }

        [HttpPatch("{orderId}/OrderLine/{orderLineId}")]
        public async Task<IActionResult> UpdateOrderLineAsync(long customerId,
            long orderId,
            long orderLineId,
            [FromServices] IRequestHandler<UpdateOrderLineCommand, OrderResponse> updateOrderLineCommand,
            [FromBody] UpdateOrderLineRequest request)
        {
            var order = await updateOrderLineCommand.Handle(new UpdateOrderLineCommand
            {
                CustomerId = customerId,
                OrderId = orderId,
                OrderLineId = orderLineId,
                Quantity = request.Quantity
            });

            if (order == null)
                return NotFound($"OrderLine with {orderLineId} ID or" +
                    $"Order with {orderId} ID not found.");

            return Ok(order);
        }

        [HttpDelete("{orderId}/OrderLine/{orderLineId}")]
        public async Task<IActionResult> DeleteOrderLineByIdAsync(long customerId, long orderId, long orderLineId,
            [FromServices] IRequestHandler<DeleteOrderLineCommand, bool> deleteOrderLineCommand)
        {
            var result = await deleteOrderLineCommand.Handle(new DeleteOrderLineCommand 
            {
                OrderLineId = orderLineId,
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
