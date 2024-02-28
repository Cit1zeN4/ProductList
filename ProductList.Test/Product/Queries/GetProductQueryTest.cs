using ProductDb = ProductList.Test.Common.ProductInitializer;

using AutoMapper;
using ProductList.Application.Interfaces;
using ProductList.Application.Logic.Product.Query;
using ProductList.Test.Common;

namespace ProductList.Test.Product.Queries;

[Collection("InitDbCollection")]
public class GetProductQueryTest(InitDbFixture fixture)
{
    private readonly IProductListDbContext _context = fixture.Context;
    private readonly IMapper _mapper = fixture.Mapper;

    [Fact]
    public async Task GetProductQuery_Success()
    {
        // Arrange
        var query1 = new GetProductQuery
        {
            Id = ProductDb.GetProductId
        };
        
        var query2 = new GetProductQuery
        {
            Barcode = ProductDb.GetProductBarcode
        };

        var validator = new GetProductQueryValidator();
        var handler = new GetProductQueryHandler(_context, _mapper);

        // Act
        var validation1 = await validator.ValidateAsync(query1);
        var product1 = await handler.Handle(query1, CancellationToken.None);
        
        var validation2 = await validator.ValidateAsync(query2);
        var product2 = await handler.Handle(query2, CancellationToken.None);
        
        // Assert
        validation1.IsValid.ShouldBeTrue();
        product1.Id.ShouldBe(ProductDb.GetProductId);
        
        validation2.IsValid.ShouldBeTrue();
        product2.Barcode.ShouldBe(ProductDb.GetProductBarcode);
    }
    
    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { null, null};
        yield return new object[] { Guid.NewGuid(), "9012345678"};
        yield return new object[] { Guid.Empty, null};
        yield return new object[] { null, ""};
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public async Task GetProductQuery_ValidationError(Guid? id, string? barcode)
    {
        // Arrange
        var query = new GetProductQuery
        {
            Id = id,
            Barcode = barcode
        };

        var validator = new GetProductQueryValidator();

        // Act
        var validation = await validator.ValidateAsync(query);
        
        // Assert
        validation.IsValid.ShouldBeFalse();
    }
}