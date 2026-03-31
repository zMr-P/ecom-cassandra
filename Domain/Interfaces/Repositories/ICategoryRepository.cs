using ecom_cassandra.Domain.Entities;

namespace ecom_cassandra.Domain.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task CreateAsync ( Category category, CancellationToken cancellationToken);
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
}