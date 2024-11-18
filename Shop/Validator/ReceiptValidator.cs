using FluentValidation;
using Restoran.Entity;

public class ReceiptValidator : AbstractValidator<Receipt>
{
    public ReceiptValidator()
    {
        RuleFor(receipt => receipt.Cost)
            .GreaterThan(0).WithMessage("Receipt cost must be greater than zero.");

        RuleFor(receipt => receipt.Calories)
            .GreaterThan(0).WithMessage("Receipt calories must be greater than zero.");

        RuleFor(receipt => receipt.Time)
            .NotEmpty().WithMessage("Receipt time must not be empty.");

        RuleFor(receipt => receipt.Orders)
            .NotNull().WithMessage("Orders must not be null.");
    }
}
