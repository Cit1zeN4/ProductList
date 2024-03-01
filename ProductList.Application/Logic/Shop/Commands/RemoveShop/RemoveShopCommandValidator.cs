using FluentValidation;

namespace ProductList.Application.Logic.Shop.Commands.RemoveShop;

public sealed class RemoveShopCommandValidator : AbstractValidator<RemoveShopCommand>
{
    public RemoveShopCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}