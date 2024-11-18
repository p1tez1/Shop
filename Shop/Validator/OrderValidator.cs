using FluentValidation;
using Restoran.Entity;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(order => order.Dish).NotEmpty().WithMessage("Order must have at least one dish.");
        RuleFor(order => order.Time).LessThanOrEqualTo(DateTime.Now).WithMessage("Order time must be in the past or present.");
        RuleForEach(order => order.Dish).SetValidator(new SimpleDishValidator());
    }
}
