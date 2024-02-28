using ShopDb = ProductList.Test.Common.ShopInitializer;

using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Interfaces;

using ProductList.Application.Logic.Shop.Commands.UpdateShop;
using ProductList.Test.Common;

namespace ProductList.Test.Shop.Commands;

[Collection("InitDbCollection")]
public class UpdateShopCommandTest(InitDbFixture fixture)
{
    private readonly IProductListDbContext _context = fixture.Context;

    [Fact]
    public async Task UpdateShopCommand_SuccessOnRoleAdmin()
    {
        // Arrange
        var name = "new name";

        var command = new UpdateShopCommand()
        {
            Id = ShopDb.ShopToUpdateId,
            Name = name
        };
        
        var userInfoService = UserInfoServiceMock.GetAdminMock().Object;
        var handler = new UpdateShopCommandHandler(_context, userInfoService);
        var validator = new UpdateShopCommandValidator();

        // Act
        var validation = await validator.ValidateAsync(command);
        await handler.Handle(command, CancellationToken.None);
        
        var result =
            await _context.Shops.SingleOrDefaultAsync(x => x.Id == command.Id && x.Name == name && x.Address == null);

        // Assert
        validation.IsValid.ShouldBeTrue();
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task UpdateShopCommand_SuccessOnRoleModerator()
    {
        // Arrange
        var name = "new name";

        var command = new UpdateShopCommand()
        {
            Id = ShopDb.ShopToUpdateId,
            Name = name
        };

        var userInfoService = UserInfoServiceMock.GetModeratorMock().Object;
        var handler = new UpdateShopCommandHandler(_context, userInfoService);
        var validator = new UpdateShopCommandValidator();
        
        // Act
        var validation = await validator.ValidateAsync(command, CancellationToken.None);
        await handler.Handle(command, CancellationToken.None);
        var result = 
            await _context.Shops.SingleOrDefaultAsync(x => x.Id == command.Id && x.Name == name && x.Address == null);

        // Assert
        validation.IsValid.ShouldBeTrue();
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task UpdateShopCommand_FailOnWrongId()
    {
        // Arrange
        var command = new UpdateShopCommand()
        {
            Id = Guid.NewGuid()
        };

        var userInfoService = UserInfoServiceMock.GetAdminMock().Object;
        var handler = new UpdateShopCommandHandler(_context, userInfoService);
        var validator = new UpdateShopCommandValidator();
        
        // Act
        var validationResult = await validator.ValidateAsync(command, CancellationToken.None);
        
        // Assert
        validationResult.IsValid.ShouldBeTrue();
        Should.Throw<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateShopCommand_FailOnUserRole()
    {
        // Arrange
        var name = "new name";

        var command = new UpdateShopCommand()
        {
            Id = ShopDb.ShopToUpdateId,
            Name = name
        };
        
        var userInfoService = UserInfoServiceMock.GetUserMock().Object;
        var handler = new UpdateShopCommandHandler(_context, userInfoService);
        var validator = new UpdateShopCommandValidator();

        // Act
        var validation = await validator.ValidateAsync(command, CancellationToken.None);
        
        // Assert
        validation.IsValid.ShouldBeTrue();
        Should.Throw<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
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
        var validation1 = await validator.ValidateAsync(command1, CancellationToken.None);
        var validation2 = await validator.ValidateAsync(command2, CancellationToken.None);

        // Assert
        validation1.IsValid.ShouldBeFalse();
        validation2.IsValid.ShouldBeFalse();
    }
}