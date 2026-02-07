using ecom_cassandra.Domain.Interfaces;
using ecom_cassandra.Domain.Interfaces.Repositories;
using ecom_cassandra.Infrastructure;
using ecom_cassandra.Infrastructure.Repositories;
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

        services.AddScoped<IOperationBatch, OperationBatch>();
        return services;
    }
}