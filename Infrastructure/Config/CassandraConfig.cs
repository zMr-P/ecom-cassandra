namespace ecom_cassandra.Infrastructure.Config;

public class CassandraConfig
{
    public string UserName { get; init; } = string.Empty;   
    public string Password { get; init; } = string.Empty;
    public string Host { get; init; } = string.Empty;
    public int Port { get; init; }
    public string KeySpace { get; set; } = string.Empty;
}