using Cassandra;
using Cassandra.Fluent.Migrator.Core;

namespace ecom_cassandra.Infrastructure.Migrations;

public class AddColumnRolesOnUser20260203(ISession session) : IMigrator
{

    public string Name => "AddColumnRolesOnUser20260203";
    public Version Version => new Version(1, 0, 2);
    public string Description => "Add column roles on User table";
    private readonly ISession _session = session;
    
    public async Task ApplyMigrationAsync()
    {
        const string addRolesColumnCql = @"
            ALTER TABLE users 
            ADD role text;";
        
        await _session.ExecuteAsync(new SimpleStatement(addRolesColumnCql));
    }
}