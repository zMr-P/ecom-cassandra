using System.Text;
using Cassandra.Mapping;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;

namespace ecom_cassandra.Infrastructure.Repositories;

public class OrderRepository(IMapper sessionMapper) : IOrderRepository
{
    private readonly IMapper _sessionMapper = sessionMapper;

    // Use with Operation Batch to batch multiple operations
    public async Task<string> CreateQueryAsync(Order order, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var cqlQuery = new StringBuilder();
        cqlQuery.Append("INSERT INTO orders (id, user_id, status, total_amount, created_at) ");
        cqlQuery.Append("VALUES (");
        cqlQuery.Append($"{order.Id}, ");
        cqlQuery.Append($"{order.UserId}, ");
        cqlQuery.Append($"'{order.Status}', ");
        cqlQuery.Append($"{order.TotalAmount}, ");
        cqlQuery.Append($"'{order.CreatedAt:yyyy-MM-ddTHH:mm:ssZ}'");
        cqlQuery.Append(");");

        return await Task.FromResult(cqlQuery.ToString());
    }

    public async Task<List<Order>> GetAllAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var orders = await _sessionMapper.FetchAsync<Order>("SELECT * FROM orders");
        return orders.ToList();
    }
}