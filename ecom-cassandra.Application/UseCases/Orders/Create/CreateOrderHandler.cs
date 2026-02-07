using ecom_cassandra.CrossCutting.Constants;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces;
using ecom_cassandra.Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Orders.Create;

public class CreateOrderHandler(
    IUserRepository userRepository,
    IOrderRepository orderRepository,
    IOrderItemRepository orderItemRepository,
    IOperationBatch operationBatch)
    : IRequestHandler<CreateOrderRequest, Result>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;
    private readonly IOperationBatch _operationBatch = operationBatch;

    public async Task<Result> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var dataUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (dataUser is null)
                return new Result(false)
                    .AddErrorMessage("The user does not exist.");

            var order = request.Adapt<Order>();
            var orderItems = request.Items.Adapt<List<OrderItem>>();
            orderItems.ForEach(item => item.OrderId = order.Id);

            var orderQuery = await _orderRepository.CreateQueryAsync(order, cancellationToken);
            var orderItemsQuery = await _orderItemRepository.CreateBatchQueryAsync(orderItems, cancellationToken);

            await _operationBatch.AppendAsync(orderQuery, orderItemsQuery);

            await _operationBatch.CommitAsync(cancellationToken);

            return new Result(true)
                .AddMessage(SuccessMessage.Created);
        }
        catch (Exception ex)
        {
            return new Result(false)
                .AddErrorMessage(ex.Message);
        }
    }
}