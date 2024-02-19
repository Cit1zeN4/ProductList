using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Commands.RemoveShop;

public class RemoveShopCommandHandler : IRequestHandler<RemoveShopCommand, Unit>
{
    private readonly IProductListDbContext _context;
    private readonly IMapper _mapper;


    public RemoveShopCommandHandler(IProductListDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(RemoveShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await _context.Shops.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (shop == null)
            throw new NotFoundException(nameof(Shop), request.Id);

        _context.Shops.Remove(shop);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}