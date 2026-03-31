namespace ecom_cassandra.Application.UseCases.Users.GetAll;

public class GetAllUserAddressResponse
{
    public Guid Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public int Number { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}