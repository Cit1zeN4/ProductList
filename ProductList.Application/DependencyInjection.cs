using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductList.Application.Common.Behaviors;
using ProductList.Application.Common.Mappings;
using ProductList.Application.Interfaces;

namespace ProductList.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new AssemblyMappingProfile(typeof(IProductListDbContext).Assembly));
        });
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}