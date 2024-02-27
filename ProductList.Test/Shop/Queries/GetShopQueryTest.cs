using ShopDb = ProductList.Test.Common.TestDbInitializer.ShopInitializer;

using ProductList.Application.Interfaces;
using ProductList.Application.Logic.Shop.Queries.GetShop;
using ProductList.Test.Common;

namespace ProductList.Test.Shop.Queries;

[Collection("InitDbCollection")]
public class GetShopQueryTest(InitDbFixture fixture)
{
    private readonly IProductListDbContext _context = fixture.Context;

    [Fact]
    public async Task GetShopQuery_Success()
    {
        // Arrange
        var query = new GetShopQuery
        {
            Id = ShopDb.ShopToGetId1
        };

        var validator = new GetShopQueryValidator();
        var handler = new GetShopQueryHandler(_context);
        
        // Act
        var validation = await validator.ValidateAsync(query);
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        validation.IsValid.ShouldBeTrue();
        result.ShouldNotBeNull();
        result.Id.ShouldBe(ShopDb.ShopToGetId1);
    }

    [Fact]
    public async Task GetShopQuery_ValidationError()
    {
        // Arrange
        var query = new GetShopQuery
        {
            Id = Guid.Empty
        };

        var validator = new GetShopQueryValidator();

        // Act
        var validation = await validator.ValidateAsync(query);
        
        // Assert
        validation.IsValid.ShouldBeFalse();
    }
}