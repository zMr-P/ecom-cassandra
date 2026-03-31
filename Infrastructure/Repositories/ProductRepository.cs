using Cassandra.Mapping;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;

namespace ecom_cassandra.Infrastructure.Repositories;

public class ProductRepository(IMapper sessionMapper) : IProductRepository
{
    private readonly IMapper _sessionMapper = sessionMapper;

    public async Task CreateAsync(Product product, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        await _sessionMapper.InsertAsync(product);
    }

    public async Task<List<Product>> GetAllProducts(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var products = await _sessionMapper.FetchAsync<Product>("SELECT * FROM products");
        return products.ToList();
    }
}