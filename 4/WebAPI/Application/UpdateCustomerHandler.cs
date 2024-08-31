using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Customers.Commands
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly _dbContext _context;

        public UpdateCustomerHandler(_dbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.FindAsync(request.Id);

            if (customer == null)
            {
                return false;
            }

            customer.CustomerCode = request.CustomerCode;
            customer.CustomerName = request.CustomerName;
            customer.CustomerAddress = request.CustomerAddress;
            customer.ModifiedBy = request.ModifiedBy;
            customer.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}