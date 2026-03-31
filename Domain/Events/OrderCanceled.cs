namespace ecom_cassandra.Domain.Events;

public record OrderCanceled(Guid OrderId, Guid UserId, string Reason, DateTime CancelledAt);