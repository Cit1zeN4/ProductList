using ProductList.Application.Interfaces;

namespace ProductList.Test.Common;

public static class ShopInitializer
{
    public static Guid ShopToDeleteId = Guid.NewGuid();
    public static Guid ShopToUpdateId = Guid.NewGuid();
    public static Guid ShopToGetId1 = Guid.NewGuid();
    public static Guid ShopToGetId2 = Guid.NewGuid();

    public static void InitShop(IProductListDbContext context)
    {
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
            },
            new Domain.Shop()
            {
                Id = ShopToGetId1,
                Name = "Unique value",
                Address = "Night City 52/1",
                CreateAt = DateTimeOffset.UtcNow.AddMinutes(-10)
            },
            new Domain.Shop()
            {
                Id = ShopToGetId2,
                Name = "Value",
                Address = "Night City 52/2",
                CreateAt = DateTimeOffset.UtcNow
            }
        );
    }
}

public static class ProductInitializer
{
    public static Guid GetProductId = Guid.NewGuid();
    public static readonly string GetProductBarcode = "90123456789";

    public static void InitProduct(IProductListDbContext context)
    {
        context.Products.AddRange(
            new Domain.Product
            {
                Id = GetProductId,
                Barcode = GetProductBarcode,
                Name = "TestProduct1"
            }
        );
    }
}