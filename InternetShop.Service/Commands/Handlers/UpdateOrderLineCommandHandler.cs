using InternetShop.Contract.Responses;
using InternetShop.Contract.Enums;
using InternetShop.Data;
using InternetShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Commands.Handlers
{
    public class UpdateOrderLineCommandHandler : IRequestHandler<UpdateOrderLineCommand, OrderResponse?>
    {
        private readonly InternetShopDbContext _context;

        public UpdateOrderLineCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse?> Handle(UpdateOrderLineCommand request, CancellationToken canToken = default)
        {
            // Спроба отримати замовлення
            Order? order = await GetCustomersOrderAsync(request, canToken);
            if (order == null)
                return null;

            // Пошук OrderLine прямо у Order для безпеки та оминання ще одного запиту до БД
            OrderLine? orderLine = order.OrderLines.SingleOrDefault(ol => ol.Id == request.OrderLineId);
            if (orderLine == null)
                return null;

            // Збереження попередньої ціни OrderLine
            decimal oldOrderLineCost = orderLine.TotalPrice;

            // Збільшення кількості у OrderLine та оновлення ціни
            orderLine.Quantity = request.Quantity;
            orderLine.TotalPrice = orderLine.Product.Price * request.Quantity;

            //Оновлення підсумкової ціни у Order
            order.TotalPrice = (order.TotalPrice - oldOrderLineCost) + orderLine.TotalPrice;

            await _context.SaveChangesAsync(canToken); // Збереження змін

            return new OrderResponse
            {
                OrderId = order.Id,
                TotalPrice = order.TotalPrice,
                OrderLines = order.OrderLines
                    .Select(ol => new OrderLineResponse
                    {
                        OrderLineId = ol.Id,
                        Quantity = ol.Quantity,
                        TotalPrice = ol.TotalPrice,
                        ProductId = ol.ProductId,
                        ProductName = ol.Product.Name,
                        CreatedAt = ol.CreatedAt,
                        UpdatedAt = ol.UpdatedAt
                    }
                    ).ToList(),
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt
            };
        }

        // Отримання замовлення з перевіркою його допустимого стану та що воно належить вказаному клієнту
        private async Task<Order?> GetCustomersOrderAsync(UpdateOrderLineCommand request, CancellationToken canToken = default)
        {
            Order? order = await _context.Orders
               .Include(x => x.Customer)
               .Include(x => x.OrderLines)
                   .ThenInclude(ol => ol.Product)
               .SingleOrDefaultAsync(x => x.Id == request.OrderId, canToken);

            if (order == null || order.CustomerId != request.CustomerId)
                return null;

            if (order.Status is not (OrderStatus.Draft or OrderStatus.Created))
                return null;

            return order;
        }
    }
}