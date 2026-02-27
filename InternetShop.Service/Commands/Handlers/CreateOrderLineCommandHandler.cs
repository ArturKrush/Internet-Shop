using InternetShop.Contract.Responses;
using InternetShop.Contract.Enums;
using InternetShop.Data;
using InternetShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Service.Commands.Handlers
{
    public class CreateOrderLineCommandHandler : IRequestHandler<CreateOrderLineCommand, OrderResponse>
    {
        private readonly InternetShopDbContext _context;

        public CreateOrderLineCommandHandler(InternetShopDbContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(CreateOrderLineCommand request, CancellationToken cancellationToken = default)
        {
            // З даних команди заповнюється новий OrderLine, рахується вартість по кількості продуктів
            OrderLine? orderLine = await CreateOrderLineAsync(request, cancellationToken);

            // Спроба отримати єдине замовлення-чернетку
            Order? draftOrder = await GetCustomersDraftAsync(request.CustomerId, cancellationToken);

            // Якщо замовлення-чернетки поки не було, його потрібно створити
            if (draftOrder == null)
            {
                draftOrder = new Order
                {
                    CustomerId = request.CustomerId,
                    Status = OrderStatus.Draft,
                    TotalPrice = orderLine.TotalPrice,
                    OrderLines = new List<OrderLine>() // Щоб не було NullReferenceException при Add()
                };

                // Замовлення-чернетка готується до створення
                await _context.Orders.AddAsync(draftOrder, cancellationToken);
            }
            else
            {
                // Якщо чернетка вже існувала, її чек збільшується на вартість нового OrderLine
                draftOrder.TotalPrice += orderLine.TotalPrice;
            }

            // OrderLine прив'язується до замовлення-чернетки
            // - orderLine.OrderId = draftOrder.Id;

            // До замовлення-чернетки додається OrderLine
            draftOrder.OrderLines.Add(orderLine);

            // Збереження змін
            // - await _context.OrderLines.AddAsync(orderLine, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken); /* Тут генерується orderLine.Id,
            orderLine додається до draftOrder */

            return new OrderResponse
            {
                OrderId = draftOrder.Id,
                TotalPrice = draftOrder.TotalPrice,
                OrderLines = draftOrder.OrderLines
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
                CreatedAt = draftOrder.CreatedAt,
                UpdatedAt = draftOrder.UpdatedAt
            };
        }

        private async Task<OrderLine> CreateOrderLineAsync(CreateOrderLineCommand request, CancellationToken cT)
        {
            Product? product = await _context.Products.SingleOrDefaultAsync(p => p.Id == request.ProductId, cT);
            if (product == null)
            {
                throw new ArgumentException($"Product with ID {request.ProductId} not found.");
            }

            return new OrderLine
            {
                Quantity = request.Quantity,
                ProductId = request.ProductId,
                Product = product, // Для нового OrderLine вказується Product
                TotalPrice = request.Quantity * product.Price
            };
        }

        // Отримання єдиного замовлення-чернетки
        private async Task<Order?> GetCustomersDraftAsync(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderLines)
                    .ThenInclude(ol => ol.Product)
                .SingleOrDefaultAsync(x => x.CustomerId == customerId && x.Status == OrderStatus.Draft, cancellationToken);
        }
    }
}

