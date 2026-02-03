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

        // Descobre todas as migrations automaticamente via reflection
        var migrations = typeof(CreateUserAndAddrress20260202).Assembly
            .GetTypes()
            .Where(t => typeof(IMigrator).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(t => (IMigrator)Activator.CreateInstance(t, session)!)
            .OrderBy(m => m.Version)
            .ToList();

        // Aplica migrations
        foreach (var migration in migrations)
        {
            Console.WriteLine($"Aplicando migration: {migration.Name}");
            await migration.ApplyMigrationAsync();
            Console.WriteLine($"Migration {migration.Name} aplicada com sucesso!");
        }

        Console.WriteLine("✅ Todas as migrations foram aplicadas.");
    }
}