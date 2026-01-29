using Cassandra;
using ecom_cassandra.Infrastructure.Config;
using Microsoft.Extensions.Options;

namespace ecom_cassandra.Infrastructure.Session;

public class CassandraSession
{
    private readonly ISession _session;
    
    public CassandraSession(IOptions<CassandraConfig> config)
    {
        var cfg = config.Value;
        
        var cluster = Cluster.Builder()
            .AddContactPoint(cfg.Host)
            .WithPort(cfg.Port)
            .WithCredentials(cfg.UserName, cfg.Password)
            .Build();

        _session = cluster.Connect(cfg.KeySpace);
    }
    
    public ISession GetSession() => _session;
}