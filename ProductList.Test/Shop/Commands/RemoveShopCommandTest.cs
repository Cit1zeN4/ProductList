using ShopDb = ProductList.Test.Common.ShopInitializer;

using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Interfaces;
using ProductList.Application.Logic.Shop.Commands.RemoveShop;
using ProductList.Test.Common;


namespace ProductList.Test.Shop.Commands;

[Collection("InitDbCollection")]
public class RemoveShopCommandTest(InitDbFixture fixture)
{
    private readonly IProductListDbContext _context = fixture.Context;

    [Fact]
    public async Task RemoveShopCommand_Success()
    {
        // Arrange
        var command = new RemoveShopCommand()
        {
            Id = ShopDb.ShopToDeleteId
        };

        var userInfoService = UserInfoServiceMock.GetAdminMock().Object;
        var handler = new RemoveShopCommandHandler(_context, userInfoService);
        var validator = new RemoveShopCommandValidator();

        // Act
        var validation = await validator.ValidateAsync(command);
        await handler.Handle(command, CancellationToken.None);

        // Assert
        validation.IsValid.ShouldBeTrue();
        (await _context.Shops.SingleOrDefaultAsync(x => x.Id == ShopDb.ShopToDeleteId)).ShouldBeNull();
    }

    [Fact]
    public async Task RemoveShopCommand_FailOnWrongId()
    {
        // Arrange
        var command = new RemoveShopCommand()
        {
            Id = Guid.NewGuid()
        };

        var userInfoService = UserInfoServiceMock.GetAdminMock().Object;
        var handler = new RemoveShopCommandHandler(_context, userInfoService);
        var validator = new RemoveShopCommandValidator();
        
        // Act
        var validation = await validator.ValidateAsync(command);
        
        // Assert
        validation.IsValid.ShouldBeTrue();
        Should.Throw<NotFoundException>( async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task RemoveShopCommand_FailOnUserRole()
    {
        // Arrange
        var command = new RemoveShopCommand()
        {
            Id = ShopDb.ShopToDeleteId
        };

        var userInfoService = UserInfoServiceMock.GetUserMock().Object;
        var handler = new RemoveShopCommandHandler(_context, userInfoService);
        var validator = new RemoveShopCommandValidator();
        
        // Act
        var validation = await validator.ValidateAsync(command);
        
        // Assert
        validation.IsValid.ShouldBeTrue();
        Should.Throw<NotFoundException>( async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task RemoveShopCommand_FailOnModeratorRole()
    {
        // Arrange
        var command = new RemoveShopCommand()
        {
            Id = ShopDb.ShopToDeleteId
        };

        var userInfoService = UserInfoServiceMock.GetModeratorMock().Object;
        var handler = new RemoveShopCommandHandler(_context, userInfoService);
        var validator = new RemoveShopCommandValidator();
        
        // Act
        var validation = await validator.ValidateAsync(command);
        
        // Assert
        validation.IsValid.ShouldBeTrue();
        Should.Throw<NotFoundException>( async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task RemoveShopCommand_ValidationError()
    {
        // Arrange
        var command = new RemoveShopCommand()
        {
            Id = Guid.Empty
        };
        
        var validator = new RemoveShopCommandValidator();
        
        // Act
        var validation = await validator.ValidateAsync(command);
        
        // Assert
        validation.IsValid.ShouldBeFalse();
    }
}