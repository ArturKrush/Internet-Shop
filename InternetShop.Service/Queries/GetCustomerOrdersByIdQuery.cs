using InternetShop.Contract.Responses;
using InternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Queries
{
    public class GetCustomerOrdersByIdQueryHandler : IRequestHandler<long, IList<OrderResponse>?>
    {
        private readonly InternetShopDbContext _context;

        public GetCustomerOrdersByIdQueryHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<IList<OrderResponse>?> Handle(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.CustomerId == customerId)
                .Select(x => new OrderResponse
                {
                    OrderId = x.Id,
                    TotalPrice = x.TotalPrice,
                    OrderLines = x.OrderLines
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
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
