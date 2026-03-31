using ecom_cassandra.Domain.Entities;

namespace ecom_cassandra.Domain.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<string> CreateQueryAsync(Order order, CancellationToken ct);
    Task<List<Order>> GetAllAsync(CancellationToken ct);
}