using MediatR;

namespace ProductList.Application.Logic.Shop.Commands.CraeteShop;

public class CreateShopCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Address { get; set; }
}