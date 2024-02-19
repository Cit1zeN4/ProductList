﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Queries.SearchShop;

public class SearchShopHandler : IRequestHandler<SearchShopQuery, DataList<Domain.Shop>>
{
    private readonly IProductListDbContext _context;
    private readonly IMapper _mapper;
    
    public SearchShopHandler(IProductListDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<DataList<Domain.Shop>> Handle(SearchShopQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Shops.Where(x => EF.Functions.Like(x.Name, $"%{request.Search}%"));
        var totalCount = await query.CountAsync(cancellationToken);

        if (request.Skip.HasValue)
            query = query.Skip(request.Skip.Value);
        if (request.Take.HasValue)
            query = query.Take(request.Take.Value);

        var list = await query.ToListAsync(cancellationToken);

        return new DataList<Domain.Shop>() { TotalCount = totalCount, Records = list };
    }
}