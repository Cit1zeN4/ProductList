using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common.Exceptions;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Product.Query.GetProduct;

public class GetProductQueryHandler
    (IProductListDbContext context, IMapper mapper) : IRequestHandler<GetProductQuery, GetProductResponse>
{
    public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        Domain.Product? product = null;
        object key;

        if (request.Id.HasValue)
        {
            key = request.Id;
            product = await context.Products.FirstOrDefaultAsync(x => x.Id == request.Id,
                cancellationToken: cancellationToken);
        }
        else if (!string.IsNullOrEmpty(request.Barcode))
        {
            key = request.Barcode;
            product = await context.Products.FirstOrDefaultAsync(x => x.Barcode == request.Barcode,
                cancellationToken: cancellationToken);
        }
        else
            throw new AggregateException();

        if (product == null)
            throw new NotFoundException(nameof(Domain.Product), key);

        return mapper.Map<GetProductResponse>(product);
    }
}