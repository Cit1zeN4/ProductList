using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Queries.GetShop;

public class GetShopQueryHandler : IRequestHandler<GetShopQuery, Domain.Shop>
{
    private readonly IProductListDbContext _context;
    private readonly IMapper _mapper;


    public GetShopQueryHandler(IProductListDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Domain.Shop> Handle(GetShopQuery request, CancellationToken cancellationToken)
    {
        var shop = await _context.Shops.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (shop == null)
            throw new NotFoundException(nameof(Domain.Shop), request.Id);
        
        return shop;
    }
}