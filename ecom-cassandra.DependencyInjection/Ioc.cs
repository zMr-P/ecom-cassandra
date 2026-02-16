using ecom_cassandra.Domain.Interfaces;
using ecom_cassandra.Domain.Interfaces.Repositories;
using ecom_cassandra.Domain.Interfaces.Security;
using ecom_cassandra.Infrastructure;
using ecom_cassandra.Infrastructure.Repositories;
using ecom_cassandra.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace ecom_cassandra.DependencyInjection;

public static class Ioc
{
    public static IServiceCollection SetInversionOfControl(this IServiceCollection services)
    {
        #region Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        #endregion

        #region  OperationBatch
        services.AddScoped<IOperationBatch, OperationBatch>();
        #endregion

        #region Security
        services.AddSingleton<IHashSecurity, HashSecurity>();
        services.AddSingleton<IJwtSecurity, JwtSecurity>();
        #endregion
        
        return services;
    }
}