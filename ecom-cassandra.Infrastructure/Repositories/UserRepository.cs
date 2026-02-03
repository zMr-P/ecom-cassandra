using Cassandra.Mapping;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;

namespace ecom_cassandra.Infrastructure.Repositories;

public class UserRepository(IMapper sessionMapper) : IUserRepository
{
    private readonly IMapper _sessionMapper = sessionMapper;

    public async Task CreateAsync(User user, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        await _sessionMapper.InsertAsync(user);
    }

    public async Task UpdateAsync(User user, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        await _sessionMapper.UpdateAsync(user);
    }

    public async Task DeleteAsync(User user, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        await _sessionMapper.DeleteAsync(user);
    }

    public async Task<User> GetByIdAsync(int id, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        return await _sessionMapper.FirstOrDefaultAsync<User>("WHERE id = ?", id);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var users = await _sessionMapper.FetchAsync<User>("SELECT * FROM users");
        return users.ToList();
    }
}