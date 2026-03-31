using Cassandra;
using Cassandra.Fluent.Migrator.Core;

namespace ecom_cassandra.Infrastructure.Migrations;

public class InsertAdminUser20260223(ISession session) : IMigrator
{
    public string Name => "InsertAdminUser20260223";
    public Version Version => new Version(1, 0, 4);
    public string Description => "Insert an admin user into the users table for initial setup.";
    private readonly ISession _session = session;

    public async Task ApplyMigrationAsync()
    {
        const string insertAdminUserCql = @"
            INSERT INTO users 
            (id, name, email, role, password_hash)
            VALUES(
             uuid(), 
             'master-admin',
             'admin@admin.com',
             'Admin',
             '$2a$11$u6QJ.wo7QTz9agWN7uHNFO11wNB2UOg/7RSl755IAv7WNWhPIr5Py');
        ";

        await _session.ExecuteAsync(new SimpleStatement(insertAdminUserCql));
    }
}