using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Logic.Shop.Commands.RemoveShop;
using ProductList.Test.Common;

namespace ProductList.Test.Shop.Commands;

public class RemoveShopCommandTest : TestCommandBase
{
    [Fact]
    public async Task RemoveShopCommand_Success()
    {
        // Arrange
        var userService = UserInfoServiceMock.GetAdminMock();
        var handler = new RemoveShopCommandHandler(Context, userService);
        var validator = new RemoveShopCommandValidator();

        var command = new RemoveShopCommand()
        {
            Id = ProductListContextFactory.ShopToDeleteId
        };
        
        // Act
        var validationResult = await validator.ValidateAsync(command, CancellationToken.None);
        await handler.Handle(command, CancellationToken.None);
        var result = await Context.Shops.SingleOrDefaultAsync(x => x.Id == command.Id);

        // Assert
        validationResult.IsValid.ShouldBeTrue();
        result.ShouldBeNull();
    }
    
    [Fact]
    public async Task RemoveShopCommand_FailOnWrongId()
    {
        // Arrange
        var userService = UserInfoServiceMock.GetAdminMock();
        var handler = new RemoveShopCommandHandler(Context, userService);
        var validator = new RemoveShopCommandValidator();

        var command = new RemoveShopCommand()
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
    public async Task RemoveShopCommand_FailOnUserRole()
    {
        // Arrange
        var userService = UserInfoServiceMock.GetUserMock();
        var handler = new RemoveShopCommandHandler(Context, userService);
        var validator = new RemoveShopCommandValidator();

        var command = new RemoveShopCommand()
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
    public async Task RemoveShopCommand_FailOnModeratorRole()
    {
        // Arrange
        var userService = UserInfoServiceMock.GetModeratorMock();
        var handler = new RemoveShopCommandHandler(Context, userService);
        var validator = new RemoveShopCommandValidator();

        var command = new RemoveShopCommand()
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
    public async Task RemoveShopCommand_ValidationError()
    {
        // Arrange
        var validator = new RemoveShopCommandValidator();

        var command = new RemoveShopCommand()
        {
            Id = Guid.Empty
        };
        
        // Act
        var validationResult = await validator.ValidateAsync(command, CancellationToken.None);
        
        // Assert
        validationResult.IsValid.ShouldBeFalse();
    }
}