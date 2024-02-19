using MediatR;

namespace ProductList.Application.Logic.Shop.Queries.GetShop;

public class GetShopQuery : IRequest<Domain.Shop>
{
    public Guid Id { get; set; }
}