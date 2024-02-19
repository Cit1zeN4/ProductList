using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductList.Application.Interfaces;

namespace ProductList.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ProductListDbContext>(options => { options.UseNpgsql(connectionString); });
        services.AddScoped<IProductListDbContext>(provider => provider.GetService<ProductListDbContext>());
        return services;
    }
}