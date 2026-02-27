using InternetShop.Contract.Enums;
using InternetShop.Data;
using InternetShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Commands.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly InternetShopDbContext _context;

        public DeleteOrderCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken canToken = default)
        {
            // Спроба отримати замовлення
            Order? order = await GetCustomersOrderAsync(request, canToken);
            if (order == null)
                return false;

            // Видалення пов'язаних OrderLines
            _context.OrderLines.RemoveRange(order.OrderLines);

            // Видалення вказаного замовлення
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync(canToken);

            return true;
        }

        private async Task<Order?> GetCustomersOrderAsync(DeleteOrderCommand request, CancellationToken canToken = default)
        {
            Order? order = await _context.Orders
               .Include(x => x.OrderLines)
               .SingleOrDefaultAsync(x => x.Id == request.OrderId, canToken);

            // Перевірка, що замовлення належить вказаному клієнту
            if (order == null || order.CustomerId != request.CustomerId)
                return null;

            if (order.Status is (OrderStatus.Completed or OrderStatus.Confirmed))
                return null;

            return order;
        }
    }
}
