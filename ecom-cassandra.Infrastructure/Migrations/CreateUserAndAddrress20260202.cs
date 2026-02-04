using Cassandra;
using Cassandra.Fluent.Migrator.Core;

namespace ecom_cassandra.Infrastructure.Migrations;

public class CreateUserAndAddrress20260202(ISession session) : IMigrator
{
    public string Name => "CreateUserAndAddress20260202";
    public Version Version => new Version(1, 0, 0);
    public string Description => "Create User and Address tables";
    private readonly ISession _session = session;

    public async Task ApplyMigrationAsync()
    {
        // Create the UDT Address
        const string createAddressTypeCql = @"
            CREATE TYPE IF NOT EXISTS address (
                id uuid,
                street text,
                number int,
                city text,
                state text,
                zipcode text,
                country text
            );";
        await _session.ExecuteAsync(new SimpleStatement(createAddressTypeCql));

        // Create table Users with UDT address
        const string createUserTableCql = @"
            CREATE TABLE IF NOT EXISTS users (
                id uuid PRIMARY KEY,
                name text,
                email text,
                password_hash text,
                created_at timestamp,
                updated_at timestamp,
                addresses list<frozen<address>>
            );";
        await _session.ExecuteAsync(new SimpleStatement(createUserTableCql));
    }
}