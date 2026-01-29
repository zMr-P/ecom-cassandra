namespace ecom_cassandra.Infrastructure.Config;

public class CassandraConfig
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string KeySpace { get; set; }
}