using MediatR;

namespace ProductList.Application.Logic.Product.Query;

public class GetProductQuery : IRequest<GetProductResponse>
{
    public Guid? Id { get; set; }
    public string? Barcode { get; set; }
}