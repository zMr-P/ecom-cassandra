using ecom_cassandra.Application.UseCases.Users.Create;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Enums;
using Mapster;

namespace ecom_cassandra.Application.Mapping;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserRequest, User>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Role, src => nameof(UserRoles.Customer))
            .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
            .Map(dest => dest.Addresses, src => src.Addresses);

        config.NewConfig<CreateUserAddressRequest, Address>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.ZipCode, src => src.ZipCode);
    }
}