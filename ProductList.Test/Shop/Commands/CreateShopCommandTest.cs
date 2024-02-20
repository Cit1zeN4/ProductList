using Microsoft.EntityFrameworkCore;
using ProductList.Application.Logic.Shop.Commands.CreateShop;
using ProductList.Test.Common;

namespace ProductList.Test.Shop.Commands;

public class CreateShopCommandTest : TestCommandBase
{
    [Fact]
    public async Task CreateShopCommand_Success()
    {
        // Arrange
        var handler = new CreateShopCommandHandler(Context);
        var validator = new CreateShopCommandValidator();

        var name = "Some name";
        var address = "Some address";

        var command = new CreateShopCommand()
        {
            Name = name,
            Address = address
        };

        // Act
        var validationResult = await validator.ValidateAsync(command, CancellationToken.None);
        var shopId = await handler.Handle(command, CancellationToken.None);
        var result =
            await Context.Shops.SingleOrDefaultAsync(x => x.Id == shopId && x.Name == name && x.Address == address);

        // Assert
        validationResult.IsValid.ShouldBeTrue();
        shopId.ShouldNotBe(Guid.Empty);
        result.ShouldNotBeNull();
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    public async Task CreateShopCommand_ValidationError(string name, string address)
    {
        // Arrange
        var validator = new CreateShopCommandValidator();

        var command = new CreateShopCommand()
        {
            Name = name,
            Address = address
        };

        // Act
        var validationResult = await validator.ValidateAsync(command, CancellationToken.None);

        // Assert
        validationResult.IsValid.ShouldBeFalse();
    }
}