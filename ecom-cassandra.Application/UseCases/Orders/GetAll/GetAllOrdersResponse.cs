using ecom_cassandra.Domain.Enums;

namespace ecom_cassandra.Application.UseCases.Orders.GetAll;

public class GetAllOrdersResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
}