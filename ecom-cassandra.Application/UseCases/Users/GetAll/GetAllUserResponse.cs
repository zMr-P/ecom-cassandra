using ecom_cassandra.Domain.Enums;

namespace ecom_cassandra.Application.UseCases.Users.GetAll;

public class GetAllUserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<GetAllUserAddressResponse>? Addresses { get; set; }
}