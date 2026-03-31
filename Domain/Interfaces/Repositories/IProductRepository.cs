using ecom_cassandra.Domain.Entities;

namespace ecom_cassandra.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    Task CreateAsync(Product product, CancellationToken ct);
    Task<List<Product>> GetAllProducts(CancellationToken ct);
}