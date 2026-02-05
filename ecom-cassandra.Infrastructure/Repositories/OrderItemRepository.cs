using System.Text;
using Cassandra.Mapping;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;

namespace ecom_cassandra.Infrastructure.Repositories;

public class OrderItemRepository(IMapper sessionMapper) : IOrderItemRepository
{
    private readonly IMapper _sessionMapper = sessionMapper;
    
    public Task<string> CreateBatchQueryAsync(List<OrderItem> orderItems, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var cqlQuery = new StringBuilder();

        foreach (var item in orderItems)
        {
            cqlQuery.AppendLine(
                $"INSERT INTO order_items (order_id, product_id, quantity, unit_price) " +
                $"VALUES ({item.OrderId}, {item.ProductId}, {item.Quantity}, {item.UnitPrice});"
            );
        }

        return Task.FromResult(cqlQuery.ToString());
    }
}