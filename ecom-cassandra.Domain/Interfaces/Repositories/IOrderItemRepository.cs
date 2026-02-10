using ecom_cassandra.Domain.Entities;

namespace ecom_cassandra.Domain.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<string> CreateBatchQueryAsync(List<OrderItem> orderItems, CancellationToken ct);
    Task<List<OrderItem>> GetByOrderIdAsync(Guid orderId, CancellationToken ct);
}