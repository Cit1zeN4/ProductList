using MediatR;

namespace ProductList.Application.Logic.Shop.Commands.RemoveShop;

public class RemoveShopCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}