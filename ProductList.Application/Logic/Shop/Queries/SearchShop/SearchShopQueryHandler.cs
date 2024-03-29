﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductList.Application.Common;
using ProductList.Application.Interfaces;

namespace ProductList.Application.Logic.Shop.Queries.SearchShop;

public sealed class SearchShopQueryHandler(IProductListDbContext context) : IRequestHandler<SearchShopQuery, DataList<Domain.Shop>>
{
    public async Task<DataList<Domain.Shop>> Handle(SearchShopQuery request, CancellationToken cancellationToken)
    {
        var query = context.Shops.AsQueryable();

        if (!string.IsNullOrEmpty(request.Search) || !string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(x =>
                EF.Functions.Like(x.Name, $"%{request.Search}%") ||
                EF.Functions.Like(x.Address, $"%{request.Search}%"));

        var totalCount = await query.CountAsync(cancellationToken);

        query = query.OrderByDescending(x => x.CreateAt);

        if (request.Skip.HasValue)
            query = query.Skip(request.Skip.Value);
        if (request.Take.HasValue)
            query = query.Take(request.Take.Value);

        var list = await query.ToListAsync(cancellationToken);

        return new DataList<Domain.Shop>() { TotalCount = totalCount, Records = list };
    }
}