using AutoMapper;
using MediatR;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Commands.CraeteShop;

public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, Guid>
{
    private readonly IProductListDbContext _context;
    private readonly IMapper _mapper;


    public CreateShopCommandHandler(IProductListDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Guid> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {
        var shop = new Domain.Shop()
        {
            Name = request.Name,
            Address = request.Address
        };

        await _context.Shops.AddAsync(shop, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return shop.Id;
    }
}