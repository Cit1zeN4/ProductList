using FluentValidation;

namespace ProductList.Application.Logic.Shop.Commands.UpdateShop;

public sealed class UpdateShopCommandValidator : AbstractValidator<UpdateShopCommand>
{
    public UpdateShopCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(256);
        RuleFor(x => x.Address).MaximumLength(512);
    }
}