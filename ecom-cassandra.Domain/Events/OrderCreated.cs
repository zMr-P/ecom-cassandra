namespace ecom_cassandra.Domain.Events;

public record OrderCreated(Guid OrderId, Guid UserId, decimal TotalAmount, DateTime CreatedAt);