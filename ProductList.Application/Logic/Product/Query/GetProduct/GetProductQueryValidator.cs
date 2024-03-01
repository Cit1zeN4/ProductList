using FluentValidation;

namespace ProductList.Application.Logic.Product.Query.GetProduct;

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        When(x => x.Id.HasValue && string.IsNullOrEmpty(x.Barcode), () =>
            {
                RuleFor(y => y.Barcode).Null();
                RuleFor(y => y.Id).NotNull().NotEqual(Guid.Empty);
            })
            .Otherwise(() =>
            {
                RuleFor(y => y.Id).Null();
                RuleFor(y => y.Barcode).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50);
            });

        RuleFor(y => y.Id).NotNull().NotEqual(Guid.Empty);
        RuleFor(y => y.Barcode).NotNull().NotEmpty().MinimumLength(1).MaximumLength(50);
    }
}