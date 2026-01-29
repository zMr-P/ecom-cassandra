using ecom_cassandra.Application.UseCases.Users.Create;
using ecom_cassandra.Domain.Entities;
using Mapster;

namespace ecom_cassandra.Application.Mapping;

public class UserMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserRequest, User>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.PasswordHash, src => src.Password);
    }
}