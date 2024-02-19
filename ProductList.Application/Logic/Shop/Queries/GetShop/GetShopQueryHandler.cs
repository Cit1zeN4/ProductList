using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Queries.GetShop;

public class GetShopQueryHandler(IProductListDbContext context) : IRequestHandler<GetShopQuery, Domain.Shop>
{
    public async Task<Domain.Shop> Handle(GetShopQuery request, CancellationToken cancellationToken)
    {
        var shop = await context.Shops.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (shop == null)
            throw new NotFoundException(nameof(Domain.Shop), request.Id);
        
        return shop;
    }
}