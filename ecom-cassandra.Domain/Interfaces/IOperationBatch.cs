namespace ecom_cassandra.Domain.Interfaces;

public interface IOperationBatch
{
    Task AppendAsync(params string[] queries);
    Task CommitAsync(CancellationToken cancellationToken);
}