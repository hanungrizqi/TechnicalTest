using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Customers.Commands
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly _dbContext _context;

        public CreateCustomerHandler(_dbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                CustomerCode = request.CustomerCode,
                CustomerName = request.CustomerName,
                CustomerAddress = request.CustomerAddress,
                CreatedBy = request.CreatedBy,
                CreatedAt = DateTime.Now
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}