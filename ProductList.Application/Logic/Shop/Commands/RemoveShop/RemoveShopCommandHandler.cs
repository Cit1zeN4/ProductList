using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Commands.RemoveShop;

public sealed class RemoveShopCommandHandler(IProductListDbContext context, IUserInfoService user) : IRequestHandler<RemoveShopCommand, Unit>
{
    public async Task<Unit> Handle(RemoveShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await context.Shops.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (shop == null || user.Role != Roles.Admin)
            throw new NotFoundException(nameof(Shop), request.Id);

        context.Shops.Remove(shop);
        await context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}