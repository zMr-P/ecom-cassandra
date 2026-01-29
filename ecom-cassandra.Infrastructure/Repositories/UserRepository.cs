using Cassandra.Mapping;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;

namespace ecom_cassandra.Infrastructure.Repositories;

public class UserRepository(IMapper mapper) : IUserRepository
{
    private readonly IMapper _mapper = mapper;

    public async Task CreateAsync(User user, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        await _mapper.InsertAsync(user);
    }

    public async Task UpdateAsync(User user, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        await _mapper.UpdateAsync(user);
    }

    public async Task DeleteAsync(User user, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        await _mapper.DeleteAsync(user);
    }

    public async Task<User> GetByIdAsync(int id, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        return await _mapper.FirstOrDefaultAsync<User>("WHERE id = ?", id);
    }
}