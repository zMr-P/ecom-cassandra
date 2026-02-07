using Cassandra.Mapping;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;

namespace ecom_cassandra.Infrastructure.Repositories;

public class CategoryRepository(IMapper sessionMapper) : ICategoryRepository
{
    private readonly IMapper _sessionMapper = sessionMapper;

    public async Task CreateAsync(Category category, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _sessionMapper.InsertAsync(category);
    }

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var categories = await _sessionMapper.FetchAsync<Category>("SELECT * FROM categories");
        return categories.ToList();
    }
}