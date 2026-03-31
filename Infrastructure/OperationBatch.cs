using System.Text;
using Cassandra;
using ecom_cassandra.Domain.Interfaces;

namespace ecom_cassandra.Infrastructure;

public class OperationBatch(ISession session) : IOperationBatch
{
    private readonly List<string> _queries = [];
    private readonly ISession _session = session;

    public Task AppendAsync(params string[] queries)
    {
        _queries.AddRange(queries);
        return Task.CompletedTask;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var batchCql = Build();
        await _session.ExecuteAsync(new SimpleStatement(batchCql));
        _queries.Clear();
    }

    private string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine("BEGIN BATCH");
        foreach (var q in _queries)
        {
            sb.AppendLine(q);
        }

        sb.AppendLine("APPLY BATCH;");
        return sb.ToString();
    }
}