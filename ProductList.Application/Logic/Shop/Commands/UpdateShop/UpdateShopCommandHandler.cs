using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Commands.UpdateShop;

public class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand, Unit>
{
    private readonly IProductListDbContext _context;
    private readonly IMapper _mapper;


    public UpdateShopCommandHandler(IProductListDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
    {
        var shop = await _context.Shops.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (shop == null)
            throw new NotFoundException(nameof(Shop), request.Id);

        shop.Name = request.Name;
        shop.Address = request.Address;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}