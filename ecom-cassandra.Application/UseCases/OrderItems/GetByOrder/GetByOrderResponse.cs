namespace ecom_cassandra.Application.UseCases.OrderItems.GetByOrder;

public class GetByOrderResponse
{
    public List<OrderItemsResponse> OrderItems { get; set; }
}