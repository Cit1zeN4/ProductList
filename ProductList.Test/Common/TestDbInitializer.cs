using ProductList.Application.Interfaces;

namespace ProductList.Test.Common;

public static class TestDbInitializer
{
    public static class ShopInitializer
    {
        public static Guid ShopToDeleteId = Guid.NewGuid();
        public static Guid ShopToUpdateId = Guid.NewGuid();
    
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
                }
            );
        }
    }
}