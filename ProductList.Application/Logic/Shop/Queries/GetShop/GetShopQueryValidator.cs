using FluentValidation;

namespace ProductList.Application.Logic.Shop.Queries.GetShop;

public class GetShopQueryValidator : AbstractValidator<GetShopQuery>
{
    public GetShopQueryValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}