using MediatR;
using ProductList.Application.Logic.Product.Response;

namespace ProductList.Application.Logic.Product.Query.GetProduct;

public class GetProductQuery : IRequest<BaseProductResponse>
{
    public Guid? Id { get; set; }
    public string? Barcode { get; set; }
}