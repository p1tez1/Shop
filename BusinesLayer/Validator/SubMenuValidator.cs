using FluentValidation;
using Restoran.Entity;

public class SubMenuValidator : AbstractValidator<SubMenu>
{
    public SubMenuValidator()
    {
        RuleFor(subMenu => subMenu.Name)
            .NotEmpty().WithMessage("SubMenu name must not be empty.");

        RuleFor(subMenu => subMenu.MenuId)
            .NotEmpty().WithMessage("MenuId must not be empty.");

        RuleForEach(subMenu => subMenu.Dishes)
            .SetValidator(new DishValidator()); // Assuming you have a DishValidator
    }
}
