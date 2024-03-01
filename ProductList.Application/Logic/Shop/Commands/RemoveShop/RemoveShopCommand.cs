using MediatR;

namespace ProductList.Application.Logic.Shop.Commands.RemoveShop;

public sealed class RemoveShopCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}