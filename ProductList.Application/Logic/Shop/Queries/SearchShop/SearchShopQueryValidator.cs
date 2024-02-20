using FluentValidation;

namespace ProductList.Application.Logic.Shop.Queries.SearchShop;

public class SearchShopQueryValidator : AbstractValidator<SearchShopQuery>
{
    public SearchShopQueryValidator()
    {
        RuleFor(x => x.Search).MaximumLength(512);
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Take).GreaterThanOrEqualTo(1);
    }
}