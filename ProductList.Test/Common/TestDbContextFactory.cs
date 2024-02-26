using Microsoft.EntityFrameworkCore;
using ProductList.Application.Interfaces;
using ProductList.Data;

namespace ProductList.Test.Common;

public static class TestDbContextFactory
{
    public static ProductListDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ProductListDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new ProductListDbContext(options);
        context.Database.EnsureCreated();
        
        TestDbInitializer.ShopInitializer.InitShop(context);

        context.SaveChanges();
        return context;
    }
}