﻿using AutoMapper;
using ProductList.Application.Common.Mappings;
using ProductList.Domain;

namespace ProductList.Application.Logic.Product.Response;

public sealed class BaseProductResponse : IMapWith<Domain.Product>
{
    public Guid Id { get; set; }
    public string Barcode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Image> Images { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Product, BaseProductResponse>()
            .ForMember(dto => dto.Id,
                opt => opt.MapFrom(x => x.Id))
            .ForMember(dto => dto.Barcode,
                opt => opt.MapFrom(x => x.Barcode))
            .ForMember(dto => dto.Name,
                opt => opt.MapFrom(x => x.Name))
            .ForMember(dto => dto.Description,
                opt => opt.MapFrom(x => x.Description))
            .ForMember(dto => dto.Images,
                opt => opt.MapFrom(x => x.Images));
    }
}