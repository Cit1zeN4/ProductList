using Microsoft.EntityFrameworkCore;
using ProductList.Data;

namespace ProductList.Test.Common;

public class ProductListContextFactory
{
    public static Guid ShopToDeleteId = Guid.NewGuid();
    public static Guid ShopToUpdateId = Guid.NewGuid();

    public static ProductListDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ProductListDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ProductListDbContext(options);
        context.Database.EnsureCreated();

        context.Shops.AddRange(
            new Domain.Shop()
            {
                Id = ShopToDeleteId,
                Name = "Test shop1",
                Address = "Test shop1 address"
            },
            new Domain.Shop()
            {
                Id = ShopToUpdateId,
                Name = "Test shop2",
                Address = "Test shop2 address"
            }
        );

        return context;
    }

    public static void Destroy(ProductListDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}