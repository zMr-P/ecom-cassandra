using ecom_cassandra.Domain.Events;
using ecom_cassandra.Domain.Interfaces.Events;
using MassTransit;

namespace ecom_cassandra.Infrastructure.EventsPublisher;

public class OrderEventPublisher(IPublishEndpoint publishEndpoint) : IOrderEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    public async Task Publish(OrderCreated orderCreated, CancellationToken ct)
    {
        await _publishEndpoint.Publish(orderCreated, ct);
    }

    public async Task Publish(OrderCanceled orderCanceled, CancellationToken ct)
    {
        await _publishEndpoint.Publish(orderCanceled, ct);
    }
}