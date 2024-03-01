using MediatR;

namespace ProductList.Application.Logic.Product.Query.GetProduct;

public class GetProductQuery : IRequest<GetProductResponse>
{
    public Guid? Id { get; set; }
    public string? Barcode { get; set; }
}