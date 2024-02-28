using ShopDb = ProductList.Test.Common.ShopInitializer;

using ProductList.Application.Interfaces;
using ProductList.Application.Logic.Shop.Queries.SearchShop;
using ProductList.Test.Common;

namespace ProductList.Test.Shop.Queries;

[Collection("InitDbCollection")]
public class SearchShopQueryTest(InitDbFixture fixture)
{
    private readonly IProductListDbContext _context = fixture.Context;

    [Fact]
    public async Task SearchShopQuery_Success()
    {
        // Arrange
        var query1 = new SearchShopQuery
        {
            Search = "Value"
        };
        
        var query2 = new SearchShopQuery
        {
            Search = "Value",
            Take = 1
        };
        
        var query3 = new SearchShopQuery
        {
            Search = "Value",
            Take = 1,
            Skip = 1
        };
        
        var query4 = new SearchShopQuery
        {
            Search = "Unique"
        };
        
        var query5 = new SearchShopQuery
        {
            Search = "52/2"
        };

        var validator = new SearchShopQueryValidator();
        var handler = new SearchShopHandler(_context);

        // Act
        var validation1 = await validator.ValidateAsync(query1);
        var result1 = await handler.Handle(query1, CancellationToken.None);
        
        var validation2 = await validator.ValidateAsync(query2);
        var result2 = await handler.Handle(query2, CancellationToken.None);
        
        var validation3 = await validator.ValidateAsync(query3);
        var result3 = await handler.Handle(query3, CancellationToken.None);
        
        var validation4 = await validator.ValidateAsync(query4);
        var result4 = await handler.Handle(query4, CancellationToken.None);
        
        var validation5 = await validator.ValidateAsync(query5);
        var result5 = await handler.Handle(query5, CancellationToken.None);

        // Assert
        validation1.IsValid.ShouldBeTrue();
        result1.ShouldNotBeNull();
        result1.TotalCount.ShouldBe(2);
        result1.Records.Any(x => x.Id == ShopDb.ShopToGetId1).ShouldBeTrue();
        result1.Records.Any(x => x.Id == ShopDb.ShopToGetId2).ShouldBeTrue();
        
        validation2.IsValid.ShouldBeTrue();
        result2.ShouldNotBeNull();
        result2.TotalCount.ShouldBe(2);
        result2.Records.Count.ShouldBe(1);
        result2.Records.Any(x => x.Id == ShopDb.ShopToGetId1).ShouldBeFalse();
        result2.Records.Any(x => x.Id == ShopDb.ShopToGetId2).ShouldBeTrue();
        
        validation3.IsValid.ShouldBeTrue();
        result3.ShouldNotBeNull();
        result2.TotalCount.ShouldBe(2);
        result2.Records.Count.ShouldBe(1);
        result3.Records.Any(x => x.Id == ShopDb.ShopToGetId1).ShouldBeTrue();
        result3.Records.Any(x => x.Id == ShopDb.ShopToGetId2).ShouldBeFalse();
        
        validation4.IsValid.ShouldBeTrue();
        result4.ShouldNotBeNull();
        result4.TotalCount.ShouldBe(1);
        result4.Records.Any(x => x.Id == ShopDb.ShopToGetId1).ShouldBeTrue();
        result4.Records.Any(x => x.Id == ShopDb.ShopToGetId2).ShouldBeFalse();
        
        validation5.IsValid.ShouldBeTrue();
        result5.ShouldNotBeNull();
        result5.TotalCount.ShouldBe(1);
        result5.Records.Any(x => x.Id == ShopDb.ShopToGetId1).ShouldBeFalse();
        result5.Records.Any(x => x.Id == ShopDb.ShopToGetId2).ShouldBeTrue();
    }

    [Theory]
    [InlineData("Some str", 0, null)]
    [InlineData("Some str", -5, null)]
    [InlineData("Some str", 1, -2)]
    [InlineData("Some str", 51, 5)]
    public async Task SearchShopQuery_ValidationError(string search, int? take, int? skip)
    {
        // Arrange
        var query = new SearchShopQuery
        {
            Search = search,
            Take = take,
            Skip = skip
        };

        var validator = new SearchShopQueryValidator();

        // Act
        var validation = await validator.ValidateAsync(query);

        // Assert
        validation.IsValid.ShouldBeFalse();
    }
}