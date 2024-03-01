using MediatR;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Commands.CreateShop;

public sealed class CreateShopCommandHandler(IProductListDbContext context) : IRequestHandler<CreateShopCommand, Guid>
{
    public async Task<Guid> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {
        var shop = new Domain.Shop()
        {
            Name = request.Name,
            Address = request.Address
        };

        await context.Shops.AddAsync(shop, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return shop.Id;
    }
}