using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Commands.UpdateShop;

public class UpdateShopCommandHandler(IProductListDbContext context, IUserInfoService user) : IRequestHandler<UpdateShopCommand, Unit>
{
    public async Task<Unit> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await context.Shops.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (shop == null || user.Role == Roles.User)
            throw new NotFoundException(nameof(Shop), request.Id);

        shop.Name = request.Name;
        shop.Address = request.Address;

        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}