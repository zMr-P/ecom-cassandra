using ecom_cassandra.Domain.Events;

namespace ecom_cassandra.Domain.Interfaces.Events;

public interface IOrderEventPublisher
{
    Task Publish(OrderCreated orderCreated, CancellationToken ct);
    Task Publish(OrderCanceled orderCanceled, CancellationToken ct);
}