using Cassandra;
using Cassandra.Fluent.Migrator.Core;

namespace ecom_cassandra.Infrastructure.Migrations;

public class AddEmailIndexOnUser20260215(ISession session) : IMigrator
{
    public string Name => "AddEmailIndexOnUser20260215";
    public Version Version => new Version(1, 0, 3);
    public string Description => "Adds a new index to optimize email lookups on the User table.";
    private readonly ISession _session = session;

    public async Task ApplyMigrationAsync()
    {
        const string createEmailIndex = @"
           CREATE INDEX users_email_idx ON users (email);
        ";

        await _session.ExecuteAsync(new SimpleStatement(createEmailIndex));
    }
}