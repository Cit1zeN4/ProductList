using MediatR;

namespace ProductList.Application.Logic.Shop.Commands.UpdateShop;

public sealed class UpdateShopCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}