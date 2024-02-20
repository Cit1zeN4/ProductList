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

        var name = "Some name";
        var address = "Some address";

        var command = new CreateShopCommand()
        {
            Name = name,
            Address = address
        };

        // Act
        var shopId = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(
            await Context.Shops.SingleOrDefaultAsync(x => x.Id == shopId && x.Name == name && x.Address == address));
    }
}