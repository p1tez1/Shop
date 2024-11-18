using FluentValidation;
using Restoran.Entity;

public class MenuValidator : AbstractValidator<Menu>
{
    public MenuValidator()
    {
        RuleFor(menu => menu.Name).NotEmpty().WithMessage("Menu name must not be empty.");
        RuleFor(menu => menu.SubMenus).NotNull().WithMessage("SubMenus cannot be null.");
        RuleForEach(menu => menu.SubMenus).SetValidator(new SubMenuValidator());
    }
}
