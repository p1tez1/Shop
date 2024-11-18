using FluentValidation;
using Restoran.Entity;

public class DishValidator : AbstractValidator<Dish>
{
    public DishValidator()
    {
        RuleFor(dish => dish.NameDishes)
            .NotEmpty().WithMessage("Dish name must not be empty.");

        RuleFor(dish => dish.Description)
            .NotEmpty().WithMessage("Dish description must not be empty.");

        RuleFor(dish => dish.Cost)
            .GreaterThan(0).WithMessage("Dish cost must be greater than zero.");

        RuleFor(dish => dish.Calories)
            .GreaterThan(0).WithMessage("Dish calories must be greater than zero.");

        RuleFor(dish => dish.SubMenuId)
            .NotEmpty().WithMessage("SubMenuId must not be empty.");
    }
}
