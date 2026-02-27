using InternetShop.Contract.Enums;
using InternetShop.Data;
using InternetShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Commands.Handlers
{
    public class DeleteOrderLineCommandHandler : IRequestHandler<DeleteOrderLineCommand, bool>
    {
        private readonly InternetShopDbContext _context;

        public DeleteOrderLineCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteOrderLineCommand request, CancellationToken canToken = default)
        {
            // Спроба отримати замовлення
            Order? order = await GetCustomersOrderAsync(request, canToken);
            if (order == null)
                return false;

            // Пошук OrderLine прямо у Order для безпеки та оминання ще одного запиту до БД
            OrderLine? orderLine = order.OrderLines.SingleOrDefault(ol => ol.Id == request.OrderLineId);
            if (orderLine == null)
                return false;

            // Збереження попередньої ціни OrderLine
            decimal oldOrderLineCost = orderLine.TotalPrice;

            // Видалення orderLine зі списку в Order, щоб Any() працювало
            order.OrderLines.Remove(orderLine);

            // Видалення orderLine
            _context.OrderLines.Remove(orderLine);

            // Якщо OrderLines у замовленні більше немає, то воно видаляється
            if (!order.OrderLines.Any())
            {
                _context.Orders.Remove(order);
            }
            else
            {
                // Зменшення загальної ціни замовлення на OrderLine.TotalPrice
                order.TotalPrice = order.TotalPrice - oldOrderLineCost;
            }

            await _context.SaveChangesAsync(canToken);

            return true;
        }

        private async Task<Order?> GetCustomersOrderAsync(DeleteOrderLineCommand request, CancellationToken canToken = default)
        {
            Order? order = await _context.Orders
               .Include(x => x.OrderLines)
               .SingleOrDefaultAsync(x => x.Id == request.OrderId, canToken);

            // Перевірка, що замовлення належить вказаному клієнту
            if (order == null || order.CustomerId != request.CustomerId)
                return null;

            if (order.Status is not (OrderStatus.Draft or OrderStatus.Created))
                return null;

            return order;
        }
    }
}
