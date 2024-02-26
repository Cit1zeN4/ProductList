using FluentValidation;
using ProductList.Application.Interfaces;
using ProductList.Application.Logic.Shop.Commands.CreateShop;
using ProductList.Test.Common;

namespace ProductList.Test.Shop.Commands;

[Collection("InitDbCollection")]
public class CreateShopCommandTest(InitDbFixture fixture)
{
    private readonly IProductListDbContext _context = fixture.Context;

    [Fact]
    public async Task CreateShopCommand_Success()
    {
        // Arrange
        var name = "Some name";
        var address = "Some address";

        var command = new CreateShopCommand()
        {
            Name = name,
            Address = address
        };
        
        var handler = new CreateShopCommandHandler(_context);
        var validator = new CreateShopCommandValidator();


        // Act
        var validation = await validator.ValidateAsync(command);
        var shopId = await handler.Handle(command, CancellationToken.None);

        // Assert
        validation.IsValid.ShouldBeTrue();
        shopId.ShouldNotBe(Guid.Empty);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    public async Task CreateShopCommand_ValidationError(string name, string address)
    {
        // Arrange
        var command = new CreateShopCommand()
        {
            Name = name,
            Address = address
        };
        
        var validator = new CreateShopCommandValidator();

        // Act
        var validation = await validator.ValidateAsync(command);
        
        // Assert
        validation.IsValid.ShouldBeFalse();
    }
}