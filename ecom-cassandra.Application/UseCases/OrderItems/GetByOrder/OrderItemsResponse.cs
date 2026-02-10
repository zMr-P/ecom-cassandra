namespace ecom_cassandra.Application.UseCases.OrderItems.GetByOrder;

public class OrderItemsResponse
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}