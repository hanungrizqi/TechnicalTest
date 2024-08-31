using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string CustomerAddress { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
    }

    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(command => command.CustomerName)
                .NotEmpty().WithMessage("Customer name tidak boleh kosong.");
            //.MinimumLength(3).WithMessage("Customer name must be at least 3 characters long.");

            RuleFor(command => command.CustomerCode)
                .NotEmpty().WithMessage("Customer code tidak boleh kosong.");
                //.Matches("^[A-Z0-9]+$").WithMessage("Customer code must be alphanumeric.");

            RuleFor(command => command.CustomerAddress)
                .NotEmpty().WithMessage("Customer address tidak boleh kosong.");
        }
    }
}