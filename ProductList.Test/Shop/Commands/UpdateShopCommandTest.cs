using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Logic.Shop.Commands.UpdateShop;
using ProductList.Test.Common;

namespace ProductList.Test.Shop.Commands;

public class UpdateShopCommandTest : TestCommandBase
{
    [Fact]
    public async Task UpdateShopCommand_SuccessOnRoleAdmin()
    {
        // Arrange
        var userService = UserInfoServiceMock.GetAdminMock();
        var handler = new UpdateShopCommandHandler(Context, userService);
        var validator = new UpdateShopCommandValidator();

        var name = "new name";

        var command = new UpdateShopCommand()
        {
            Id = ProductListContextFactory.ShopToUpdateId,
            Name = name
        };

        // Act
        var validationResult = await validator.ValidateAsync(command, CancellationToken.None);
        await handler.Handle(command, CancellationToken.None);
        var result =
            await Context.Shops.SingleOrDefaultAsync(x => x.Id == command.Id && x.Name == name && x.Address == null);

        // Assert
        validationResult.IsValid.ShouldBeTrue();
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task UpdateShopCommand_SuccessOnRoleModerator()
    {
        // Arrange
        var userService = UserInfoServiceMock.GetModeratorMock();
        var handler = new UpdateShopCommandHandler(Context, userService);
        var validator = new UpdateShopCommandValidator();

        var name = "new name";

        var command = new UpdateShopCommand()
        {
            Id = ProductListContextFactory.ShopToUpdateId,
            Name = name
        };

        // Act
        var validationResult = await validator.ValidateAsync(command, CancellationToken.None);
        await handler.Handle(command, CancellationToken.None);
        var result = await Context.Shops.SingleOrDefaultAsync(x => x.Id == command.Id && x.Name == name && x.Address == null);

        // Assert
        validationResult.IsValid.ShouldBeTrue();
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task UpdateShopCommand_FailOnWrongId()
    {
        // Arrange
        var userService = UserInfoServiceMock.GetAdminMock();
        var handler = new UpdateShopCommandHandler(Context, userService);
        var validator = new UpdateShopCommandValidator();

        var command = new UpdateShopCommand()
        {
            Id = Guid.NewGuid()
        };

        // Act
        var validationResult = await validator.ValidateAsync(command, CancellationToken.None);
        // Assert
        validationResult.IsValid.ShouldBeTrue();
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateShopCommand_FailOnUserRole()
    {
        // Arrange
        var userService = UserInfoServiceMock.GetUserMock();
        var handler = new UpdateShopCommandHandler(Context, userService);
        var validator = new UpdateShopCommandValidator();

        var command = new UpdateShopCommand()
        {
            Id = Guid.NewGuid()
        };

        // Act
        var validationResult = await validator.ValidateAsync(command, CancellationToken.None);
        // Assert
        validationResult.IsValid.ShouldBeTrue();
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateShopCommand_ValidationError()
    {
        // Arrange
        var validator = new UpdateShopCommandValidator();

        var command1 = new UpdateShopCommand()
        {
            Id = Guid.Empty
        };
        
        var command2 = new UpdateShopCommand()
        {
            Id = Guid.NewGuid(),
            Name = "1",
        };

        // Act
        var validationResult1 = await validator.ValidateAsync(command1, CancellationToken.None);
        var validationResult2 = await validator.ValidateAsync(command2, CancellationToken.None);

        // Assert
        validationResult1.IsValid.ShouldBeFalse();
        validationResult2.IsValid.ShouldBeFalse();
    }
}