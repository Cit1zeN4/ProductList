using MediatR;
using ProductList.Application.Common;

namespace ProductList.Application.Logic.Shop.Queries.SearchShop;

public class SearchShopQuery : IRequest<DataList<Domain.Shop>>
{
    public string Search { get; set; }
    public int? Take { get; set; }
    public int? Skip { get; set; }
}