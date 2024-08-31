using Domain.Entities;
using FluentValidation;

namespace Application.Customers.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.CustomerName)
                .NotEmpty().WithMessage("Customer name cannot be empty.")
                .MinimumLength(3).WithMessage("Customer name must be at least 3 characters long.");

            RuleFor(customer => customer.CustomerCode)
                .NotEmpty().WithMessage("Customer code cannot be empty.")
                .Matches("^[A-Z0-9]+$").WithMessage("Customer code must be alphanumeric.");

            RuleFor(customer => customer.CreatedAt)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Created date cannot be in the future.");

            RuleFor(customer => customer.CustomerAddress)
                .NotEmpty().WithMessage("Customer address cannot be empty.");
        }
    }
}
