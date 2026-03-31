using Cassandra;
using Cassandra.Fluent.Migrator.Core;
using ecom_cassandra.DependencyInjection;
using ecom_cassandra.Infrastructure.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ecom_cassandra.MigrationJob;

internal class Program
{
    internal static async Task Main(string[] args)
    {
        // Appsettings Configuration
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("jobsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables()
            .Build();

        // DI Configuration
        var services = new ServiceCollection();
        services.SetCassandraConfig(config);

        var provider = services.BuildServiceProvider();
        var session = provider.GetRequiredService<ISession>();

        // Create control table if not exists
        const string createSchemaMigrationsTable = @"
            CREATE TABLE IF NOT EXISTS schema_migrations (
                version text PRIMARY KEY,
                name text,
                description text,
                applied_at timestamp
            );";
        await session.ExecuteAsync(new SimpleStatement(createSchemaMigrationsTable));

        // Automatically discover all migrations with reflection
        var migrations = typeof(CreateUserAndAddrress20260202).Assembly
            .GetTypes()
            .Where(t => typeof(IMigrator).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(t => (IMigrator)Activator.CreateInstance(t, session)!)
            .OrderBy(m => m.Version)
            .DistinctBy(m => m.Name)
            .ToList();

        // Apply Migrations
        foreach (var migration in migrations)
        {
            // VCheck if it has already been applied
            var checkStmt = new SimpleStatement(
                "SELECT version FROM schema_migrations WHERE version = ?",
                migration.Version.ToString()
            );
            var result = await session.ExecuteAsync(checkStmt);

            if (result.Any())
            {
                Console.WriteLine($"⚠️  Migration {migration.Name} already applied, skiping...");
                continue;
            }

            Console.WriteLine($"🚀  Applying migration: {migration.Name}");
            await migration.ApplyMigrationAsync();
            Console.WriteLine($"✅  Migration {migration.Name} applied successfully!");

            // Register migration on controlled table
            const string insertMigrationCql = @"
                INSERT INTO schema_migrations (version, name, description, applied_at)
                VALUES (?, ?, ?, toTimestamp(now()))";

            var stmt = new SimpleStatement(
                insertMigrationCql,
                migration.Version.ToString(),
                migration.Name,
                migration.Description
            );
            await session.ExecuteAsync(stmt);
        }
        Console.WriteLine("🎉  All migrations have been applied.");
    }
}