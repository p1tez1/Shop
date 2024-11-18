using FluentValidation;
using Restoran.Entity;
public class SimpleDishValidator : AbstractValidator<SimpleDish>
{
     public SimpleDishValidator()
     {
         RuleFor(dish => dish.Name).NotEmpty().WithMessage("Dish name must not be empty.");
         RuleFor(dish => dish.Cost).GreaterThan(0).WithMessage("Dish cost must be greater than zero.");
         RuleFor(dish => dish.Cal).GreaterThan(0).WithMessage("Dish calories must be greater than zero.");
         RuleFor(dish => dish.Amount).GreaterThan(0).WithMessage("Dish amount must be greater than zero.");
     }
}



