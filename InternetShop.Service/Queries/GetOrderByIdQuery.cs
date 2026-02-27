using InternetShop.Contract.Responses;
using InternetShop.Data;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Queries
{
    public class GetOrderByIdQuery
    {
        public long OrderId { get; set; }

        public long CustomerId { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse?>
    {
        private readonly InternetShopDbContext _context;

        public GetOrderByIdQueryHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse?> Handle(GetOrderByIdQuery query, CancellationToken canToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.CustomerId == query.CustomerId && x.Id == query.OrderId)
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
                .SingleOrDefaultAsync(canToken);
        }
    }
}