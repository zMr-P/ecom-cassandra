using ecom_cassandra.Domain.Entities;

namespace ecom_cassandra.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken ct);
    Task UpdateAsync(User user, CancellationToken ct);
    Task DeleteAsync(User user, CancellationToken ct);
    Task<User> GetByIdAsync(int id, CancellationToken ct);
}