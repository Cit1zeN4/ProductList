using System.Data;
using FluentValidation;

namespace ProductList.Application.Logic.Shop.Commands.CreateShop;

public sealed class CreateShopCommandValidator : AbstractValidator<CreateShopCommand>
{
    public CreateShopCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(256);
        RuleFor(x => x.Address).MaximumLength(512);
    }
}